using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Seo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Services.Seo;
using Nop.Core.Domain.Directory;

namespace Nop.Services.NhaXes
{
    public class HanhTrinhService : IHanhTrinhService
    {
        private readonly IRepository<DiemDon> _diemdonRepository;
        private readonly IRepository<HanhTrinh> _HanhTrinhRepository;
        private readonly IRepository<StateProvince> _StateProvinceRepository;
        private readonly IRepository<HanhTrinhDiemDon> _HanhTrinhDiemDonRepository;
        private readonly IRepository<HanhTrinhDiemChot> _HanhTrinhDiemChotRepository;
        private readonly IRepository<LichTrinh> _LichTrinhRepository;
        private readonly IRepository<LichTrinhGiaVe> _LichTrinhGiaVeRepository;
        private readonly IRepository<NguonVeXe> _NguonVeXeRepository;
        private readonly IRepository<NhaXe> _nhaxeRepository;
        private readonly IRepository<DiaChi> _diachiRepository;
        private readonly IRepository<LoaiXe> _LoaiXeRepository;
        private readonly IRepository<DiaDiem> _DiaDiemRepository;
        private readonly IRepository<NguonVeXeDiaDiem> _NguonVeXeDiaDiemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<UrlRecord> _urlRecordRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<TuyenVeXe> _tuyenvexeRepository;
        private readonly IRepository<HanhTrinhGiaVe> _hanhtrinhgiaveRepository;
        private readonly IRepository<TuyenHanhTrinh> _tuyenhanhtrinhRepository;

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string URLRECORD_PATTERN_KEY = "Nop.urlrecord.";

        public HanhTrinhService(IRepository<HanhTrinhDiemChot> HanhTrinhDiemChotRepository,
            IRepository<TuyenHanhTrinh> tuyenhanhtrinhRepository,
            IRepository<DiemDon> diemdonRepository,
            IRepository<HanhTrinh> HanhTrinhRepository,
            IRepository<HanhTrinhDiemDon> HanhTrinhDiemDonRepository,
            IRepository<LichTrinh> LichTrinhRepository,
            IRepository<LichTrinhGiaVe> LichTrinhGiaVeRepository,
            IRepository<NguonVeXe> NguonVeXeRepository,
            IRepository<NhaXe> nhaxeRepository,
            IRepository<DiaChi> diachiRepository,
            IRepository<LoaiXe> LoaiXeRepository,
            IRepository<DiaDiem> DiaDiemRepository,
             IRepository<StateProvince> StateProvinceRepository,
            IRepository<NguonVeXeDiaDiem> NguonVeXeDiaDiemRepository,
            IRepository<Product> productRepository,
            IRepository<UrlRecord> urlRecordRepository,
            ICacheManager cacheManager,
            IRepository<TuyenVeXe> tuyenvexeRepository,
             IRepository<HanhTrinhGiaVe> hanhtrinhgiaveRepository
            )
        {
            this._HanhTrinhDiemChotRepository = HanhTrinhDiemChotRepository;
            this._tuyenhanhtrinhRepository = tuyenhanhtrinhRepository;
            this._diemdonRepository = diemdonRepository;
            this._HanhTrinhRepository = HanhTrinhRepository;
            this._HanhTrinhDiemDonRepository = HanhTrinhDiemDonRepository;
            this._LichTrinhRepository = LichTrinhRepository;
            this._LichTrinhGiaVeRepository = LichTrinhGiaVeRepository;
            this._NguonVeXeRepository = NguonVeXeRepository;
            this._nhaxeRepository = nhaxeRepository;
            this._diachiRepository = diachiRepository;
            this._LoaiXeRepository = LoaiXeRepository;
            this._DiaDiemRepository = DiaDiemRepository;
            this._StateProvinceRepository = StateProvinceRepository;
            this._NguonVeXeDiaDiemRepository = NguonVeXeDiaDiemRepository;
            this._productRepository = productRepository;
            this._urlRecordRepository = urlRecordRepository;
            this._cacheManager = cacheManager;
            this._tuyenvexeRepository = tuyenvexeRepository;
            this._hanhtrinhgiaveRepository = hanhtrinhgiaveRepository;
        }
        #region Diem Don
        public virtual PagedList<DiemDon> GetAllDiemDon(int NhaXeId = 0, string _keyword = "",
         int pageIndex = 0,
         int pageSize = int.MaxValue)
        {
            var query = _diemdonRepository.Table;
            if (!String.IsNullOrWhiteSpace(_keyword))
                query = query.Where(m => m.TenDiemDon.Contains(_keyword));
            query = query.Where(m => m.NhaXeId == NhaXeId);
            query = query.OrderBy(m => m.Id);
            return new PagedList<DiemDon>(query, pageIndex, pageSize);
        }
        public virtual List<DiemDon> GetAllDiemDonByNhaXeId(int NhaXeId)
        {
            var query = _diemdonRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId);
            return query.ToList();
        }
        public virtual List<DiemDon> GetAllDiemDonByNhaXeId(int NhaXeId, ENLoaiDiemDon[] loais = null)
        {
            var query = _diemdonRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId);
            if (loais != null && loais.Length > 0)
            {
                int[] _loais = loais.Cast<int>().ToArray();
                query = query.Where(m => _loais.Contains(m.LoaiDiemDonId));
            }
            return query.ToList();
        }
        public virtual DiemDon GetDiemDonById(int itemId)
        {
            if (itemId == 0)
                return null;
            return _diemdonRepository.GetById(itemId);
        }
        public virtual void InsertDiemDon(DiemDon _item)
        {
            if (_item == null)
                throw new ArgumentNullException("DiemDon");
            _diemdonRepository.Insert(_item);
        }
        public virtual void UpdateDiemDon(DiemDon _item)
        {
            if (_item == null)
                throw new ArgumentNullException("DiemDon");

            _diemdonRepository.Update(_item);
        }
        public virtual void DeleteDiemDon(DiemDon _item)
        {
            if (_item == null)
                throw new ArgumentNullException("DiemDon");
            _diemdonRepository.Delete(_item);
        }
        #endregion
        #region HanhTrinh
        public virtual PagedList<HanhTrinh> GetAllHanhTrinh(int NhaXeId = 0, string _keyword = "",
         int pageIndex = 0,
         int pageSize = int.MaxValue)
        {
            var query = _HanhTrinhRepository.Table;
            if (!String.IsNullOrWhiteSpace(_keyword))
                query = query.Where(m => m.MaHanhTrinh.Contains(_keyword));
            query = query.Where(m => m.NhaXeId == NhaXeId);
            query = query.OrderBy(m => m.Id);
            return new PagedList<HanhTrinh>(query, pageIndex, pageSize);
        }
        public virtual List<HanhTrinh> GetAllHanhTrinhByNhaXeId(int NhaXeId, int VanPhongId = 0, int TuyenId = 0)
        {
            var query = _HanhTrinhRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId);
            if (VanPhongId > 0)
                query = query.Where(m => m.VanPhongs.Count(vp => vp.Id == VanPhongId) > 0);
            if (TuyenId>0)
                query = query.Where(m => m.TuyenHanhTrinhId == TuyenId);
            return query.ToList();
        }
        public virtual List<HanhTrinh> GetAllHanhTrinhByNhaXeId(int NhaXeId, int[] VanPhongIds)
        {
            var query = _HanhTrinhRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId);
            if (VanPhongIds != null)
                query = query.Where(m => m.VanPhongs.Count(vp => VanPhongIds.Contains(vp.Id)) > 0);
            return query.ToList();
        }
        public virtual HanhTrinh GetHanhTrinhById(int itemId)
        {
            if (itemId == 0)
                return null;
            return _HanhTrinhRepository.GetById(itemId);
        }
        public virtual bool InsertHanhTrinh(HanhTrinh _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinh");
            var existitem = _HanhTrinhRepository.Table.Where(c => c.MaHanhTrinh == _item.MaHanhTrinh).Count();
            if (existitem == 0)
            {
                _HanhTrinhRepository.Insert(_item);
                return true;
            }
            return false;

        }
        public virtual bool UpdateHanhTrinh(HanhTrinh _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinh");
            var existitem = _HanhTrinhRepository.Table.Where(c => c.MaHanhTrinh == _item.MaHanhTrinh && c.Id != _item.Id).Count();
            if (existitem == 0)
            {
                _HanhTrinhRepository.Update(_item);
                return true;
            }
            return false;
        }
        public virtual void DeleteHanhTrinh(HanhTrinh _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinh");
            DeleteHanhTrinhDiemDon(_item.Id);
            _HanhTrinhRepository.Delete(_item);
        }

        //////////////////////////Hanh trinh diem don

        public virtual List<HanhTrinhDiemDon> GetAllHanhTrinhDiemDonByHanhTrinhId(int HanhTrinhId)
        {
            var query = _HanhTrinhDiemDonRepository.Table;
            query = query.Where(m => m.HanhTrinhId == HanhTrinhId)
                .OrderBy(m => m.ThuTu);
            return query.ToList();
        }
        public virtual HanhTrinhDiemDon GetHanhTrinhDiemDonById(int itemId)
        {
            if (itemId == 0)
                return null;
            return _HanhTrinhDiemDonRepository.GetById(itemId);
        }
        public virtual bool InsertHanhTrinhDiemDon(HanhTrinhDiemDon _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinhDiemDon");
            var existitem = _HanhTrinhDiemDonRepository.Table.Where(c => c.HanhTrinhId == _item.HanhTrinhId && c.DiemDonId == _item.DiemDonId).Count();
            if (existitem == 0)
            {
                _HanhTrinhDiemDonRepository.Insert(_item);
                return true;
            }
            return false;

        }
        public virtual bool UpdateHanhTrinhDiemDon(HanhTrinhDiemDon _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinhDiemDon");
            var existitem = _HanhTrinhDiemDonRepository.Table.Where(c => c.HanhTrinhId == _item.HanhTrinhId && c.DiemDonId == _item.DiemDonId && c.Id != _item.Id).Count();
            if (existitem == 0)
            {
                _HanhTrinhDiemDonRepository.Update(_item);
                return true;
            }
            return false;

        }
        public virtual void DeleteHanhTrinhDiemDon(HanhTrinhDiemDon _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinhDiemDon");
            _HanhTrinhDiemDonRepository.Delete(_item);
        }
        public virtual void DeleteHanhTrinhDiemDon(int HanhTrinhId)
        {
            var items = GetAllHanhTrinhDiemDonByHanhTrinhId(HanhTrinhId);
            foreach (var item in items)
            {
                DeleteHanhTrinhDiemDon(item);
            }
        }
        public DiemDon GetDiemDonByHanhTrinhDiemDonId(int itemId)
        {
            var item = GetHanhTrinhDiemDonById(itemId);
            return GetDiemDonById(item.DiemDonId);
        }
        /////////////////////DIEM CHOT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HanhTrinhId"></param>
        /// <returns></returns>
        public virtual List<HanhTrinhDiemChot> GetAllHanhTrinhDiemChotByHanhTrinhId(int HanhTrinhId)
        {
            var query = _HanhTrinhDiemChotRepository.Table;
            query = query.Where(m => m.HanhTrinhId == HanhTrinhId)
                .OrderBy(m => m.ThuTu);
            return query.ToList();
        }
        public virtual HanhTrinhDiemChot GetHanhTrinhDiemChotById(int itemId)
        {
            if (itemId == 0)
                return null;
            return _HanhTrinhDiemChotRepository.GetById(itemId);
        }
        public virtual bool InsertHanhTrinhDiemChot(HanhTrinhDiemChot _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinhDiemChot");
            var existitem = _HanhTrinhDiemChotRepository.Table.Where(c => c.HanhTrinhId == _item.HanhTrinhId && c.DiemDonId == _item.DiemDonId).Count();
            if (existitem == 0)
            {
                _HanhTrinhDiemChotRepository.Insert(_item);
                return true;
            }
            return false;

        }
        public virtual bool UpdateHanhTrinhDiemChot(HanhTrinhDiemChot _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinhDiemChot");
            var existitem = _HanhTrinhDiemChotRepository.Table.Where(c => c.HanhTrinhId == _item.HanhTrinhId && c.DiemDonId == _item.DiemDonId && c.Id != _item.Id).Count();
            if (existitem == 0)
            {
                _HanhTrinhDiemChotRepository.Update(_item);
                return true;
            }
            return false;

        }
        public virtual void DeleteHanhTrinhDiemChot(HanhTrinhDiemChot _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinhDiemChot");
            _HanhTrinhDiemChotRepository.Delete(_item);
        }
        public virtual void DeleteHanhTrinhDiemChot(int HanhTrinhId)
        {
            var items = GetAllHanhTrinhDiemChotByHanhTrinhId(HanhTrinhId);
            foreach (var item in items)
            {
                DeleteHanhTrinhDiemChot(item);
            }
        }
       
        #endregion
        #region LichTrinh
        public virtual PagedList<LichTrinh> GetAllLichTrinh(int NhaXeId = 0, int HanhTrinhId = 0,
         int pageIndex = 0,
         int pageSize = int.MaxValue)
        {
            var query = _LichTrinhRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId);
            if (HanhTrinhId > 0)
                query = query.Where(m => m.HanhTrinhId == HanhTrinhId);
            query = query.OrderBy(m => m.Id);
            return new PagedList<LichTrinh>(query, pageIndex, pageSize);
        }
        public List<NguonVeXe> GetAllNguonVeXeByHanhTrinhLoaiXe(List<int> LichTrinhIds, int LoaiXeId)
        {
            var query = _NguonVeXeRepository.Table.Where(c => !c.isDelete && c.HienThi && LichTrinhIds.Contains(c.LichTrinhId) && c.LoaiXeId == LoaiXeId);
            return query.ToList();
        }
        public virtual List<LichTrinh> GetAllLichTrinhByHanhTrinhId(int HanhTrinhId, int NhaXeId = 0)
        {
            var query = _LichTrinhRepository.Table;
            if (NhaXeId > 0)
                query = query.Where(m => m.NhaXeId == NhaXeId);
            if (HanhTrinhId > 0)
                query = query.Where(m => m.HanhTrinhId == HanhTrinhId);

            return query.ToList();
        }
        public virtual LichTrinh GetLichTrinhById(int itemId)
        {
            if (itemId == 0)
                return null;
            return _LichTrinhRepository.GetById(itemId);
        }
        public virtual bool InsertLichTrinh(LichTrinh _item)
        {
            if (_item == null)
                throw new ArgumentNullException("LichTrinh");
            var existitem = 0;
            if (existitem == 0)
            {
                _LichTrinhRepository.Insert(_item);
                return true;
            }
            return false;

        }

        /// <summary>
        /// Lay id tu danh muc dia diem
        /// </summary>
        /// <param name="nguonid"></param>
        /// <param name="loai"></param>
        /// <returns></returns>
        private int GetDiaDiemId(int nguonid, ENLoaiDiaDiem loai)
        {
            var diadiem = _DiaDiemRepository.Table.Where(c => c.NguonId == nguonid && c.LoaiId == (int)loai);
            if (diadiem.Count() > 0)
                return diadiem.First().Id;
            return 0;

        }
        public virtual bool UpdateLichTrinh(LichTrinh _item)
        {
            if (_item == null)
                throw new ArgumentNullException("LichTrinh");
            var existitem = 0;
            if (existitem == 0)
            {
                _LichTrinhRepository.Update(_item);
                //update loai xe cua nguon ve theo lich trinh
                var nguonve = GetNguonVeXeToanTuyen(_item.Id);
                nguonve.LoaiXeId = _item.LoaiXeId;
                nguonve.ThoiGianDi = _item.ThoiGianDi;
                nguonve.ThoiGianDen = _item.ThoiGianDen;
                _NguonVeXeRepository.Update(nguonve);
                return true;
            }
            return false;
        }
        public virtual void DeleteLichTrinh(LichTrinh _item)
        {
            if (_item == null)
                throw new ArgumentNullException("LichTrinh");
            DeleteLichTrinhGiaVe(_item.Id);
            _LichTrinhRepository.Delete(_item);
        }

        //////////////////////////Hanh trinh diem don

        public virtual List<LichTrinhGiaVe> GetLichTrinhGiaVeByLichTrinhId(int LichTrinhId)
        {
            var query = _LichTrinhGiaVeRepository.Table;
            query = query.Where(m => m.LichTrinhID == LichTrinhId)
                .OrderBy(m => m.Id);
            return query.ToList();
        }
        public virtual LichTrinhGiaVe GetLichTrinhGiaVeById(int itemId)
        {
            if (itemId == 0)
                return null;
            return _LichTrinhGiaVeRepository.GetById(itemId);
        }
        public virtual bool InsertLichTrinhGiaVe(LichTrinhGiaVe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("LichTrinhGiaVe");
            var existitem = _LichTrinhGiaVeRepository.Table.Where(c => c.LichTrinhID == _item.LichTrinhID && c.DiemDon1_Id == _item.DiemDon1_Id && c.DiemDon2_Id == _item.DiemDon2_Id).Count();
            if (existitem == 0)
            {
                _LichTrinhGiaVeRepository.Insert(_item);
                return true;
            }
            return false;

        }
        public virtual bool UpdateLichTrinhGiaVe(LichTrinhGiaVe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("LichTrinhGiaVe");
            var existitem = _LichTrinhGiaVeRepository.Table.Where(c => c.LichTrinhID == _item.LichTrinhID && c.DiemDon1_Id == _item.DiemDon1_Id && c.DiemDon2_Id == _item.DiemDon2_Id && c.Id != _item.Id).Count();
            if (existitem == 0)
            {
                _LichTrinhGiaVeRepository.Update(_item);
                return true;
            }
            return false;

        }
        public virtual void DeleteLichTrinhGiaVe(LichTrinhGiaVe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("LichTrinhGiaVe");
            _LichTrinhGiaVeRepository.Delete(_item);
        }
        public virtual void DeleteLichTrinhGiaVe(int LichTrinhId)
        {
            var items = GetLichTrinhGiaVeByLichTrinhId(LichTrinhId);
            _LichTrinhGiaVeRepository.Delete(items);
        }
        #endregion
        #region Nguon ve xe
        public List<NguonVeXe> GetAllNguonVeXeByVeGoc(int NhaXeId, int NguonVeXeGocId)
        {
            var query = _NguonVeXeRepository.Table.Where(c => !c.isDelete && c.HienThi && c.NhaXeId == NhaXeId && (c.Id == NguonVeXeGocId || c.ParentId == NguonVeXeGocId));
            return query.ToList();
        }
       
        public List<NguonVeXe> GetAllNguonVeXeByHanhTrinh(List<int> LichTrinhIds)
        {
            var query = _NguonVeXeRepository.Table.Where(c => !c.isDelete && c.HienThi && LichTrinhIds.Contains(c.LichTrinhId));
            return query.ToList();
        }
        public List<NguonVeXe> GetAllNguonVeXe(int NhaXeId = 0, int LichTrinhId = 0, int HanhTrinhId = 0, int DiemDonGocId = 0, int DiemDenGocId = 0, ENKhungGio khunggio = ENKhungGio.All)
        {
            var khungtg = new KhungThoiGian(khunggio);
            var query = _NguonVeXeRepository.Table.Where(c => !c.isDelete
                  && c.ThoiGianDi.Hour >= khungtg.GioTu
                   && c.ThoiGianDi.Hour < khungtg.GioDen
                );
            if (NhaXeId > 0)
                query = query.Where(c => c.NhaXeId == NhaXeId);
            if (LichTrinhId > 0)
                query = query.Where(c => c.LichTrinhId == LichTrinhId);
            if (HanhTrinhId > 0)
            {
                var lichtrinhids = _LichTrinhRepository.Table.Where(c => c.HanhTrinhId == HanhTrinhId).Select(c => c.Id).ToList();
                query = query.Where(c => lichtrinhids.Contains(c.LichTrinhId));
            }
            if (DiemDonGocId > 0)
                query = query.Where(c => c.DiemDonGocId == DiemDonGocId);
            if (DiemDenGocId > 0)
                query = query.Where(c => c.DiemDenGocId == DiemDenGocId);

            return query.ToList().OrderBy(t => t.ThoiGianDiHienTai).ToList();
        }
        public List<NguonVeXe> GetAllNguonVeXeToXuatBen(int NhaXeId = 0, int LichTrinhId = 0, int HanhTrinhId = 0)
        {
            var query = _NguonVeXeRepository.Table.Where(c => !c.isDelete && c.ParentId == 0);
            if (NhaXeId > 0)
                query = query.Where(c => c.NhaXeId == NhaXeId);
            if (LichTrinhId > 0)
                query = query.Where(c => c.LichTrinhId == LichTrinhId);
            if (HanhTrinhId > 0)
            {
                var lichtrinhids = _LichTrinhRepository.Table.Where(c => c.HanhTrinhId == HanhTrinhId).Select(c => c.Id).ToList();
                query = query.Where(c => lichtrinhids.Contains(c.LichTrinhId));
            }
            if (HanhTrinhId == -1)
            {
                var nguonvelist = query.ToList();
                DateTime fromdate = DateTime.Now.AddMinutes(-30);
                DateTime todate = DateTime.Now.AddMinutes(30);
                nguonvelist = nguonvelist.Where(
                  (c => c.ThoiGianDi.TimeOfDay >= fromdate.TimeOfDay && c.ThoiGianDi.TimeOfDay <= todate.TimeOfDay)
                  ).ToList();
                return nguonvelist;

            }

            return query.ToList();
        }
        public NguonVeXe GetNguonVeXeById(int itemId)
        {
            return _NguonVeXeRepository.GetById(itemId);
        }
        /// <summary>
        /// Lấy nguồn vé toàn tuyến cho lịch trình
        /// </summary>
        /// <param name="_item"></param>
        public virtual NguonVeXe GetNguonVeXeToanTuyen(int LichTrinhId)
        {
            if (LichTrinhId == 0)
                return null;
            var query = from c in _NguonVeXeRepository.Table
                        orderby c.Id
                        where c.LichTrinhId == LichTrinhId && c.HienThi && c.ParentId == 0
                        select c;
            var NguonVeXe = query.FirstOrDefault();
            return NguonVeXe;

        }
        public void DeletePhysicalNguonVe(NguonVeXe item)
        {
            if (item.ParentId != 0)
            {
                item.ProductInfo.Deleted = true;
                _productRepository.Update(item.ProductInfo);
                item.isDelete = true;
                item.HienThi = false;
                item.ToWeb = false;
                _NguonVeXeRepository.Update(item);
                //xoa nguon ve dia diem
                var nguonvediadiems = _NguonVeXeDiaDiemRepository.Table.Where(c => c.NguonVeXeId == item.Id).ToList();
                _NguonVeXeDiaDiemRepository.Delete(nguonvediadiems);

            }

        }

        public void InsertNguonVeGoc(LichTrinh _item)
        {
            // tao nguon ve goc
            var nhaxe = _nhaxeRepository.GetById(_item.NhaXeId);
            var loaixe = _LoaiXeRepository.GetById(_item.LoaiXeId);
            var hanhtrinhdiemdons = GetAllHanhTrinhDiemDonByHanhTrinhId(_item.HanhTrinhId);
            var diemdon1 = GetDiemDonById(hanhtrinhdiemdons[0].DiemDonId);
            var diachi1 = _diachiRepository.GetById(diemdon1.DiaChiId);
            var diemdon2 = GetDiemDonById(hanhtrinhdiemdons[hanhtrinhdiemdons.Count - 1].DiemDonId);
            var diachi2 = _diachiRepository.GetById(diemdon2.DiaChiId);
            var nguonve = new NguonVeXe();
            nguonve.NhaXeId = _item.NhaXeId;
            nguonve.LichTrinhId = _item.Id;
            //lay diem don la thong tin Tinh
            nguonve.DiemDonId = GetDiaDiemId(diachi1.ProvinceID, ENLoaiDiaDiem.Tinh);
            nguonve.DiemDenId = GetDiaDiemId(diachi2.ProvinceID, ENLoaiDiaDiem.Tinh);
            nguonve.DiemDonGocId = diemdon1.Id;
            nguonve.DiemDenGocId = diemdon2.Id;
            nguonve.TenDiemDon = diachi1.Province.Name;
            nguonve.TenDiemDen = diachi2.Province.Name;

            nguonve.ThoiGianDi = _item.ThoiGianDi;
            nguonve.LoaiXeId = _item.LoaiXeId;
            nguonve.ThoiGianDen = nguonve.ThoiGianDi.AddHours(Convert.ToDouble(_item.SoGioChay));
            nguonve.TimeCloseOnline = _item.TimeCloseOnline;
            nguonve.TimeOpenOnline = _item.TimeOpenOnline;
            nguonve.TenNhaXe = nhaxe.TenNhaXe;
          
            nguonve.TenLoaiXe = loaixe.TenLoaiXe;
            nguonve.GiaVeHienTai = _item.GiaVeToanTuyen;
            //tao ra thong tin san pham
           TaoNguonVeProduct(nguonve, _item.GiaVeToanTuyen);
            //tao nguon ve theo tuyen,
            //TaoTuyenVeXe(diachi1.Province, diachi2.Province, _item.GiaVeToanTuyen, loaixe.KieuXe);
            //tao cac danh muc cac diem don de co phuc vu tim kiem
            //tinh
           
            NguonVeXeDiaDiemInsert(nguonve.Id, true, nguonve.DiemDonId);
            NguonVeXeDiaDiemInsert(nguonve.Id, false, nguonve.DiemDenId);

            //ben xe
            if (diemdon1.BenXeId != null && diemdon1.BenXeId > 0)
            {
                NguonVeXeDiaDiemInsert(nguonve.Id, true, GetDiaDiemId(diemdon1.BenXeId.GetValueOrDefault(0), ENLoaiDiaDiem.BenXe));
            }
            if (diemdon2.BenXeId != null && diemdon2.BenXeId > 0)
            {
                NguonVeXeDiaDiemInsert(nguonve.Id, false, GetDiaDiemId(diemdon2.BenXeId.GetValueOrDefault(0), ENLoaiDiaDiem.BenXe));
            }
            //quan, huyen
            if (diachi1.QuanHuyenID != null && diachi1.QuanHuyenID > 0)
            {
                NguonVeXeDiaDiemInsert(nguonve.Id, true, GetDiaDiemId(diachi1.QuanHuyenID.GetValueOrDefault(0), ENLoaiDiaDiem.QuanHuyen));
            }
            if (diachi2.QuanHuyenID != null && diachi2.QuanHuyenID > 0)
            {
                NguonVeXeDiaDiemInsert(nguonve.Id, false, GetDiaDiemId(diachi2.QuanHuyenID.GetValueOrDefault(0), ENLoaiDiaDiem.QuanHuyen));
            }


        }
        public void InsertNguonVecon(LichTrinh _item, NguonVeXe nguonve)
        {
            // kiem tra xem co phai la nguon ve goc, co diem don goc va diem den goc trung nhau
            var hanhtrinhdiemdons = GetAllHanhTrinhDiemDonByHanhTrinhId(_item.HanhTrinhId);
            var diemdon1 = GetDiemDonById(hanhtrinhdiemdons[0].DiemDonId);
            var diemdon2 = GetDiemDonById(hanhtrinhdiemdons[hanhtrinhdiemdons.Count - 1].DiemDonId);
            if (diemdon1.Id != nguonve.DiemDonGocId || diemdon2.Id != nguonve.DiemDenGocId)
            {
                // neu khong phai la nguon ve goc thi insert
                var nhaxe = _nhaxeRepository.GetById(_item.NhaXeId);
                var loaixe = _LoaiXeRepository.GetById(_item.LoaiXeId);
                var diemdoncon1 = GetDiemDonById(nguonve.DiemDonGocId);
                var diemdoncon2 = GetDiemDonById(nguonve.DiemDenGocId);
                var diachi1 = _diachiRepository.GetById(diemdoncon1.DiaChiId);
                var diachi2 = _diachiRepository.GetById(diemdoncon2.DiaChiId);
                nguonve.NhaXeId = _item.NhaXeId;
                nguonve.LichTrinhId = _item.Id;
                nguonve.ThoiGianDi = _item.ThoiGianDi;
                nguonve.LoaiXeId = _item.LoaiXeId;
                nguonve.ThoiGianDen = nguonve.ThoiGianDi.AddHours(Convert.ToDouble(_item.SoGioChay));
                nguonve.TimeCloseOnline = _item.TimeCloseOnline;
                nguonve.TimeOpenOnline = _item.TimeOpenOnline;
                nguonve.TenNhaXe = nhaxe.TenNhaXe;
                nguonve.TenLoaiXe = loaixe.TenLoaiXe;
                nguonve.DiemDonId = GetDiaDiemId(diachi1.ProvinceID, ENLoaiDiaDiem.Tinh);
                nguonve.DiemDenId = GetDiaDiemId(diachi2.ProvinceID, ENLoaiDiaDiem.Tinh);
                nguonve.TenDiemDon = diachi1.Province.Name;
                nguonve.TenDiemDen = diachi2.Province.Name;
                //tao ra thong tin san pham
                TaoNguonVeProduct(nguonve, _item.GiaVeToanTuyen);
                //tao nguon ve theo tuyen,
                TaoTuyenVeXe(diachi1.Province, diachi2.Province, _item.GiaVeToanTuyen, loaixe.KieuXe);
                //tao cac danh muc cac diem don de co phuc vu tim kiem
                //tinh
                NguonVeXeDiaDiemInsert(nguonve.Id, true, nguonve.DiemDonId);
                NguonVeXeDiaDiemInsert(nguonve.Id, false, nguonve.DiemDenId);

                //ben xe
                if (diemdoncon1.BenXeId != null && diemdoncon1.BenXeId > 0)
                {
                    NguonVeXeDiaDiemInsert(nguonve.Id, true, GetDiaDiemId(diemdoncon1.BenXeId.GetValueOrDefault(0), ENLoaiDiaDiem.BenXe));
                }
                if (diemdoncon2.BenXeId != null && diemdoncon2.BenXeId > 0)
                {
                    NguonVeXeDiaDiemInsert(nguonve.Id, false, GetDiaDiemId(diemdoncon2.BenXeId.GetValueOrDefault(0), ENLoaiDiaDiem.BenXe));
                }
                //quan, huyen
                if (diachi1.QuanHuyenID != null && diachi1.QuanHuyenID > 0)
                {
                    NguonVeXeDiaDiemInsert(nguonve.Id, true, GetDiaDiemId(diachi1.QuanHuyenID.GetValueOrDefault(0), ENLoaiDiaDiem.QuanHuyen));
                }
                if (diachi2.QuanHuyenID != null && diachi2.QuanHuyenID > 0)
                {
                    NguonVeXeDiaDiemInsert(nguonve.Id, false, GetDiaDiemId(diachi2.QuanHuyenID.GetValueOrDefault(0), ENLoaiDiaDiem.QuanHuyen));
                }
            }


        }
        private void TaoTuyenVeXe(StateProvince Province1, StateProvince Province2, Decimal price, ENKieuXe kieuxe)
        {

            var checkitems = _tuyenvexeRepository.Table.Where(c => c.Province1Id == Province1.Id && c.Province2Id == Province2.Id && c.KieuXeId == (int)kieuxe).ToList();
            var item = new TuyenVeXe();
            if (checkitems.Count > 0)
            {
                item = checkitems.First();
                //chi update neu gia < gia hien tai
                if (item.PriceNew > price)
                {
                    item.PriceNew = price;
                    _tuyenvexeRepository.Update(item);
                }
            }
            else
            {
                item.Province1Id = Province1.Id;
                item.Province2Id = Province2.Id;
                item.PriceNew = price;
                item.HienThi = true;
                item.ToWeb = true;
                item.ThuTu = 0;
                item.KieuXe = kieuxe;
                _tuyenvexeRepository.Insert(item);
                string seourlname = string.Format("Vé xe tuyến {0} {1} {2}", Province1.Name, Province2.Name, item.Id, item.Id);
                seourlname = Chonves.CVCommon.convertToUnSign(seourlname);
                SaveSlug(item, item.ValidateSeName("", seourlname, true), 0);
            }
        }
        private void NguonVeXeDiaDiemInsert(int NguonVeXeId, bool isXuatPhat, int DiaDiemId)
        {
            var item = new NguonVeXeDiaDiem();
            item.NguonVeXeId = NguonVeXeId;
            item.isDiemXuatPhat = isXuatPhat;
            item.DiaDiemId = DiaDiemId;
            _NguonVeXeDiaDiemRepository.Insert(item);
        }
        private void NguonVeXeInsert(NguonVeXe item)
        {
            _NguonVeXeRepository.Insert(item);
        }

        public void UpdateNguonVeXe(NguonVeXe item)
        {
            item.ProductInfo.Published = item.HienThi;
            _productRepository.Update(item.ProductInfo);
            _NguonVeXeRepository.Update(item);
        }
        public void DeleteNguonVeXe(NguonVeXe item)
        {
            _NguonVeXeRepository.Delete(item);
        }
        #region Product
        void TaoNguonVeProduct(NguonVeXe nguonve, Decimal price)
        {
            var product = new Product();

            product.Name = string.Format("Vé xe {2} tuyến {0} - {1}", nguonve.TenDiemDon, nguonve.TenDiemDen, nguonve.TenNhaXe);
            product.FullDescription = string.Format("{0} - {1} - {2}", product.Name, nguonve.TenLoaiXe, nguonve.TenNhaXe);
            product.CreatedOnUtc = DateTime.Now;
            product.UpdatedOnUtc = DateTime.Now;
            product.Price = price;
            product.Published = false;
            product.ProductType = ProductType.SimpleProduct;
            product.VisibleIndividually = true;
            product.ShortDescription = product.FullDescription;
            product.ProductTemplateId = 1;
            product.AllowCustomerReviews = true;
            product.UnlimitedDownloads = true;
            product.MaxNumberOfDownloads = 10;
            product.DownloadActivationType = DownloadActivationType.WhenOrderIsPaid;
            product.RecurringCycleLength = 100;
            product.RecurringTotalCycles = 10;
            product.RentalPriceLength = 1;
            product.IsShipEnabled = true;
            product.StockQuantity = 10000;
            product.NotifyAdminForQuantityBelow = 1;
            product.OrderMinimumQuantity = 1;
            product.OrderMaximumQuantity = 10000;
            _productRepository.Insert(product);
            //tao thong tin seo cho product
            SaveSlug(product, product.ValidateSeName("", Chonves.CVCommon.convertToUnSign(product.Name), true), 0);
            //insert du lieu nguon ve
            nguonve.ProductId = product.Id;
            nguonve.GiaVeHienTai = price;
            NguonVeXeInsert(nguonve);

        }
        void SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity, ISlugSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var query = from ur in _urlRecordRepository.Table
                        where ur.EntityId == entityId &&
                        ur.EntityName == entityName &&
                        ur.LanguageId == languageId
                        orderby ur.Id descending
                        select ur;
            var allUrlRecords = query.ToList();
            var activeUrlRecord = allUrlRecords.FirstOrDefault(x => x.IsActive);

            if (activeUrlRecord == null && !string.IsNullOrWhiteSpace(slug))
            {
                //find in non-active records with the specified slug
                var nonActiveRecordWithSpecifiedSlug = allUrlRecords
                    .FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase) && !x.IsActive);
                if (nonActiveRecordWithSpecifiedSlug != null)
                {
                    //mark non-active record as active
                    nonActiveRecordWithSpecifiedSlug.IsActive = true;
                    UpdateUrlRecord(nonActiveRecordWithSpecifiedSlug);
                }
                else
                {
                    //new record
                    var urlRecord = new UrlRecord
                    {
                        EntityId = entity.Id,
                        EntityName = entityName,
                        Slug = slug,
                        LanguageId = languageId,
                        IsActive = true,
                    };
                    InsertUrlRecord(urlRecord);
                }
            }

            if (activeUrlRecord != null && string.IsNullOrWhiteSpace(slug))
            {
                //disable the previous active URL record
                activeUrlRecord.IsActive = false;
                UpdateUrlRecord(activeUrlRecord);
            }

            if (activeUrlRecord != null && !string.IsNullOrWhiteSpace(slug))
            {
                //is it the same slug as in active URL record?
                if (activeUrlRecord.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase))
                {
                    //yes. do nothing
                    //P.S. wrote this way for more source code readability
                }
                else
                {
                    //find in non-active records with the specified slug
                    var nonActiveRecordWithSpecifiedSlug = allUrlRecords
                        .FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase) && !x.IsActive);
                    if (nonActiveRecordWithSpecifiedSlug != null)
                    {
                        //mark non-active record as active
                        nonActiveRecordWithSpecifiedSlug.IsActive = true;
                        UpdateUrlRecord(nonActiveRecordWithSpecifiedSlug);

                        //disable the previous active URL record
                        activeUrlRecord.IsActive = false;
                        UpdateUrlRecord(activeUrlRecord);
                    }
                    else
                    {
                        //insert new record
                        //we do not update the existing record because we should track all previously entered slugs
                        //to ensure that URLs will work fine
                        var urlRecord = new UrlRecord
                        {
                            EntityId = entity.Id,
                            EntityName = entityName,
                            Slug = slug,
                            LanguageId = languageId,
                            IsActive = true,
                        };
                        InsertUrlRecord(urlRecord);

                        //disable the previous active URL record
                        activeUrlRecord.IsActive = false;
                        UpdateUrlRecord(activeUrlRecord);
                    }

                }
            }
        }
        /// <summary>
        /// Inserts an URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void InsertUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException("urlRecord");

            _urlRecordRepository.Insert(urlRecord);

            //cache
            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }

        /// <summary>
        /// Updates the URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void UpdateUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException("urlRecord");

            _urlRecordRepository.Update(urlRecord);

            //cache
            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }
        #endregion
        #endregion
        #region province
        public virtual StateProvince GetStateProvinceByNguon(int NguonId)
        {
            if (NguonId == 0)
                return null;
            return _StateProvinceRepository.GetById(NguonId);
        }
        #endregion
        #region Diem Don
        public List<HanhTrinhGiaVe> GetallHanhTrinhGiaVe(int HanhTrinhId = 0, int NhaXeId = 0,int DiemDonId=0,int DiemDenId=0,int LoaiXeId=0)
        
        {
            var query = _hanhtrinhgiaveRepository.Table.Where(c=>!c.IsDelete);
            if (HanhTrinhId > 0)
            {
                query = query.Where(c => c.HanhTrinhId == HanhTrinhId);
            }
            if (NhaXeId > 0)
            {
                query = query.Where(c => c.NhaXeId == NhaXeId);
            }
            if (DiemDonId > 0)
            {
                query = query.Where(c => c.DiemDonId == DiemDonId);
            }
            if (DiemDenId > 0)
            {
                query = query.Where(c => c.DiemDenId == DiemDenId);
            }
            if (LoaiXeId > 0)
            {
                query = query.Where(c => c.LoaiXeId == LoaiXeId);
            }
            return query.ToList();
        }

        public HanhTrinhGiaVe GetHanhTrinhGiaVeId(int itemId)
        {
            if (itemId == 0)
                return null;
            return _hanhtrinhgiaveRepository.GetById(itemId);
        }
        public void InsertHanhTrinhGiaVe(HanhTrinhGiaVe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinhGiaVe");
            _hanhtrinhgiaveRepository.Insert(_item);
        }
        public void UpdateHanhTrinhGiaVe(HanhTrinhGiaVe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinhGiaVe");

            _hanhtrinhgiaveRepository.Update(_item);
        }
        public void DeleteHanhTrinhGiaVe(HanhTrinhGiaVe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HanhTrinhGiaVe");
            _hanhtrinhgiaveRepository.Delete(_item);
        }       
       
        #endregion
        #region Tuyen hanh trinh
        public List<TuyenHanhTrinh> GetAllTuyenByNhaXeId(int NhaXeId)
        {
            return _tuyenhanhtrinhRepository.Table.Where(c => c.NhaXeId == NhaXeId).ToList();
        }
        public TuyenHanhTrinh GetTuyenById(int itemId)
        {
            return _tuyenhanhtrinhRepository.GetById(itemId);
        }
        public void InsertTuyen(TuyenHanhTrinh _item)
        {
            if (_item == null)
                throw new ArgumentNullException("TuyenHanhTrinh");
            _tuyenhanhtrinhRepository.Insert(_item);
        }
        public void UpdateTuyen(TuyenHanhTrinh _item)
        {
            if (_item == null)
                throw new ArgumentNullException("TuyenHanhTrinh");
            _tuyenhanhtrinhRepository.Insert(_item);
        }
        public void DeleteTuyen(TuyenHanhTrinh _item)
        {
            if (_item == null)
                throw new ArgumentNullException("TuyenHanhTrinh");
            _tuyenhanhtrinhRepository.Insert(_item);
        }
        #endregion
        public NguonVeXe GetNguonVeXeByloaixe(int HanhTrinhId, DateTime GioDi, int LoaiXeId)
        {
            var query = _NguonVeXeRepository.Table.Where(c => !c.isDelete && c.ParentId == 0
                && c.LoaiXeId == LoaiXeId &&
                c.LichTrinhInfo.HanhTrinhId == HanhTrinhId &&
                c.ThoiGianDi.Hour == GioDi.Hour
                && c.ThoiGianDi.Minute == GioDi.Minute);
            if (query.Count() == 0)
                return null;

            return query.ToList().First();
        }
    }
}
