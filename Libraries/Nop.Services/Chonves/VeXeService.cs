using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.NhaXes;
using Nop.Services.Configuration;
using Nop.Services.Orders;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Shipping;
namespace Nop.Services.Chonves
{
    public class VeXeService : IVeXeService
    {

        #region Ctor
        private readonly IRepository<DiaDiem> _diadiemRepository;
        private readonly IRepository<LichTrinh> _lichtrinhRepository;
        private readonly IRepository<HanhTrinh> _hanhtrinhRepository;
        private readonly IRepository<NguonVeXe> _nguonvexeRepository;
        private readonly IRepository<NguonVeXeDiaDiem> _nguonvexeDiaDiemRepository;
        private readonly IRepository<TuyenVeXe> _tuyenvexeRepository;
        private readonly IRepository<SoDoGheXeQuyTac> _sodoghexequytacRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IOrderService _orderService;
        private readonly IWebHelper _webHelper;

        public VeXeService(ICacheManager cacheManager,
            IRepository<DiaDiem> diadiemRepository,
            IRepository<LichTrinh> lichtrinhRepository,
            IRepository<HanhTrinh> hanhtrinhRepository,
            IRepository<NguonVeXe> nguonvexeRepository,
            IRepository<NguonVeXeDiaDiem> nguonvexediadiemRepository,
            IRepository<TuyenVeXe> tuyenvexeRepository,
            IRepository<PhoiVe> phoiveRepository,
            IRepository<SoDoGheXeQuyTac> sodoghexequytacRepository,
            ISettingService settingService,
            IOrderService orderService,
            IWebHelper webHelper
            )
        {
            this._diadiemRepository = diadiemRepository;
            this._cacheManager = cacheManager;
            this._lichtrinhRepository = lichtrinhRepository;
            this._hanhtrinhRepository = hanhtrinhRepository;
            this._nguonvexeRepository = nguonvexeRepository;
            this._nguonvexeDiaDiemRepository = nguonvexediadiemRepository;
            this._tuyenvexeRepository = tuyenvexeRepository;
            this._sodoghexequytacRepository = sodoghexequytacRepository;
            this._settingService = settingService;
            this._orderService = orderService;
            this._webHelper = webHelper;
        }
        #endregion
        #region tim ve xe khach
        public int GetDiaDiemId(int nguonid, ENLoaiDiaDiem loai)
        {
            var diadiem = _diadiemRepository.Table.Where(c => c.NguonId == nguonid && c.LoaiId == (int)loai);
            if (diadiem.Count() > 0)
                return diadiem.First().Id;
            return 0;

        }
        public DiaDiem GetDiaDiemById(int Id)
        {
            if (Id == 0)
                return null;
            return _diadiemRepository.GetById(Id);
        }
        public List<DiaDiem> DiaDiemSearch(string keyword = "", int top = 20)
        {
            var query = _diadiemRepository.Table;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                var keywordkodau = CVCommon.convertToUnSign(keyword);
                query = query.Where(c => c.Ten.Contains(keyword) || c.TenKhongDau.Contains(keywordkodau));
            }

            query = query.OrderBy(c => c.Id);
            return query.Take(top).ToList();
        }
        int GetKhungGioId(DateTime dt)
        {
            if (dt.Hour >= 0 && dt.Hour < 12)
                return (int)ENKhungGio.Sang;
            if (dt.Hour >= 12 && dt.Hour < 19)
                return (int)ENKhungGio.Chieu;
            return (int)ENKhungGio.Toi;
        }
        public List<NguonVeXe> VeXeSearch(DateTime NgayDi,
            List<int> NhaXeIds = null,
            int DiemDonId = 0,
            int DiemDenId = 0,
            List<int> KhungGioIds = null,
            int top = 10)
        {
            var query = _nguonvexeRepository.Table.Where(c => !c.isDelete);
            query = query.Where(c => c.HienThi && c.ToWeb);
            if (NhaXeIds != null && NhaXeIds.Count > 0)
            {
                query = query.Where(c => NhaXeIds.Contains(c.NhaXeId));
            }
            if (DiemDonId > 0)
            {
                //lay thong tin lich trinh tu diem don
                var nguonves1 = _nguonvexeDiaDiemRepository.Table.Where(c => c.DiaDiemId == DiemDonId && c.isDiemXuatPhat).Select(c => c.NguonVeXeId).ToList();
                query = query.Where(c => nguonves1.Contains(c.Id));
            }
            if (DiemDenId > 0)
            {
                var nguonves2 = _nguonvexeDiaDiemRepository.Table.Where(c => c.DiaDiemId == DiemDenId && !c.isDiemXuatPhat).Select(c => c.NguonVeXeId).ToList();
                query = query.Where(c => nguonves2.Contains(c.Id));
            }
            //loai bo nhung ve co time close ban ve truc tuyen
            var _list = query.ToList().Where(c => DateTime.Now.AddHours(c.TimeCloseOnline) <= NgayDi.AddHours(c.ThoiGianDi.Hour).AddMinutes(c.ThoiGianDi.Minute)).ToList();
            var _listnew = _list;
            if (KhungGioIds != null && KhungGioIds.Count > 0)
            {
                _listnew = new List<NguonVeXe>();
                foreach(var nv in _list)
                {
                    int khunggioid = GetKhungGioId(nv.ThoiGianDi);
                    if (KhungGioIds.Contains(khunggioid))
                        _listnew.Add(nv);
                }
            }            

            if (top > 0)
                return _listnew.Take(top).ToList();
            return _listnew;

        }
        public NguonVeXe GetNguonVeXeById(int Id)
        {
            return _nguonvexeRepository.GetById(Id);
        }
        #endregion
        #region Tuyen Ve Xe

        public virtual List<TuyenVeXe> TuyenVeXeSearch(int TinhId, ENKieuXe kieuxe = ENKieuXe.All, int top = 10)
        {
            var items = _tuyenvexeRepository.Table.Where(c => c.HienThi && c.ToWeb);

            if (kieuxe != ENKieuXe.All)
            {
                items = items.Where(c => c.KieuXeId == (int)kieuxe);
            }
            if (TinhId > 0)
            {

            }
            return items.Take(top).ToList();
        }


        public virtual TuyenVeXe GetTuyenVeXeById(int Id)
        {
            return _tuyenvexeRepository.GetById(Id);
        }

        #endregion
        #region Phoi ve

        public int GetSoDoGheXeQuyTacID(int LoaiXeId, string KyHieu, int Tang)
        {
            var query = _sodoghexequytacRepository.Table.Where(c => c.LoaiXeId == LoaiXeId && c.Val == KyHieu && c.Tang == Tang && c.x>0 && c.y>0);
            if (query.Count() > 0)
            {
                return query.First().Id;
            }
            return 0;
        }

        #endregion

    }
}
