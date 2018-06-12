using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Orders;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.NhaXes
{
    public class PhoiVeService : IPhoiVeService
    {
        #region Ctor
        private readonly IRepository<NguonVeXe> _nguonvexeRepository;
        private readonly IRepository<VanPhong> _vanphongRepository;
        private readonly IRepository<LichTrinh> _lichtrinhRepository;
        private readonly IRepository<NhanVien> _nhanvienRepository;
        private readonly IRepository<PhoiVe> _phoiveRepository;
        private readonly IRepository<SoDoGheXeQuyTac> _sodoghexequytacRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<NhaXeCustomer> _nhaxecustomerRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IOrderService _orderService;
        private readonly IWebHelper _webHelper;
        private readonly INhaXeService _nhaxeService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<HistoryXeXuatBen> _hisRepository;
        private readonly IRepository<HanhTrinhGiaVe> _hanhtrinhgiaveRepository;
        private readonly IRepository<XeVanChuyen> _xevanchuyenRepository;
        private readonly IRepository<PhoiVeNote> _phoivenoteRepository;
        private readonly IRepository<NhaXeCauHinh> _nhaxecauhinhRepository;

        public PhoiVeService(ICacheManager cacheManager,
            IRepository<DiaDiem> diadiemRepository,
            IRepository<NhanVien> nhanvienRepository,
            IRepository<VanPhong> vanphongRepository,
            IRepository<Product> productRepository,
            IRepository<LichTrinh> lichtrinhRepository,
            IRepository<HanhTrinh> hanhtrinhRepository,
            IRepository<NguonVeXe> nguonvexeRepository,
            IRepository<Customer> customerRepository,
            IRepository<NhaXeCustomer> nhaxecustomerRepository,
            IRepository<NguonVeXeDiaDiem> nguonvexediadiemRepository,
            IRepository<TuyenVeXe> tuyenvexeRepository,
            IRepository<PhoiVe> phoiveRepository,
            IRepository<SoDoGheXeQuyTac> sodoghexequytacRepository,
            ISettingService settingService,
            IOrderService orderService,
            IWebHelper webHelper,
            INhaXeService nhaxeService,
            IWorkContext workContext,
            ICustomerService customerService,
            IRepository<HistoryXeXuatBen> hisRepository,
            IRepository<HanhTrinhGiaVe> hanhtrinhgiaveRepository,
            IRepository<XeVanChuyen> xevanchuyenRepository,
            IRepository<PhoiVeNote> phoivenoteRepository,
            IRepository<NhaXeCauHinh> nhaxecauhinhRepository
            )
        {
            this._nhaxecauhinhRepository = nhaxecauhinhRepository;
            this._vanphongRepository = vanphongRepository;
            this._nhanvienRepository = nhanvienRepository;
            this._productRepository = productRepository;
            this._cacheManager = cacheManager;
            this._nguonvexeRepository = nguonvexeRepository;
            this._phoiveRepository = phoiveRepository;
            this._sodoghexequytacRepository = sodoghexequytacRepository;
            this._settingService = settingService;
            this._orderService = orderService;
            this._webHelper = webHelper;
            this._nhaxeService = nhaxeService;
            this._workContext = workContext;
            this._customerService = customerService;
            this._customerRepository = customerRepository;
            this._nhaxecustomerRepository = nhaxecustomerRepository;
            this._lichtrinhRepository = lichtrinhRepository;
            this._hisRepository = hisRepository;
            this._xevanchuyenRepository = xevanchuyenRepository;
            this._phoivenoteRepository = phoivenoteRepository;
            this._hanhtrinhgiaveRepository = hanhtrinhgiaveRepository;

        }
        #endregion
        #region phoi ve
        private bool isTrangThaiDatCho(PhoiVe _item)
        {
            int _timeexpiredatve = _settingService.GetSettingByKey("chonvesettings.timeexpiredatchogiaodich", 2);
            if (_item.TrangThai == ENTrangThaiPhoiVe.DatCho && _item.NgayUpd.AddMinutes(_timeexpiredatve) > DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private bool isTrangThaiGiuCho(PhoiVe _item)
        {
            int _timeexpiregiuve = _settingService.GetSettingByKey("chonvesettings.timeexpiregiuchogiaodich", 15);
            if (_item.TrangThai == ENTrangThaiPhoiVe.GiuCho && _item.NgayUpd.AddMinutes(_timeexpiregiuve) > DateTime.Now)
            {
                return true;
            }
            return false;
        }
        private bool isQuaHanChoXuLy(PhoiVe _item)
        {
            int _timeexpiregiuve = _settingService.GetSettingByKey("chonvesettings.timeexpirechoxulygiaodich", 5);
            if (_item.TrangThai == ENTrangThaiPhoiVe.GiuCho && _item.NgayUpd.AddMinutes(_timeexpiregiuve) < DateTime.Now)
            {
                return true;
            }
            return false;
        }
        private bool isQuaHanDatCho(PhoiVe _item)
        {
            int _timeexpiredatve = _settingService.GetSettingByKey("chonvesettings.timeexpiredatchogiaodich", 2);
            if (_item.TrangThai == ENTrangThaiPhoiVe.DatCho && _item.NgayUpd.AddMinutes(_timeexpiredatve) < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private bool isQuaHanGiuCho(PhoiVe _item)
        {
            int _timeexpiregiuve = _settingService.GetSettingByKey("chonvesettings.timeexpiregiuchogiaodich", 15);
            if (_item.TrangThai == ENTrangThaiPhoiVe.GiuCho && _item.NgayUpd.AddMinutes(_timeexpiregiuve) < DateTime.Now)
            {
                return true;
            }
            return false;
        }
        public virtual NhaXeCustomer GetKhachVangLaiInNhaXe(int nhaxeid)
        { return null; }
        public virtual bool DatVe(PhoiVe item, ENTrangThaiPhoiVe trangthai = ENTrangThaiPhoiVe.DatCho)
        {
            //kiem tra xem da ton tai hay chua
            //var query = _phoiveRepository.Table.Where(c =>
            //    (c.NguonVeXeId == item.NguonVeXeId || c.NguonVeXeConId == item.NguonVeXeId)
            //    && c.SoDoGheXeQuyTacId == item.SoDoGheXeQuyTacId
            //    && c.NgayDi.Year == item.NgayDi.Year
            //    && c.NgayDi.Month == item.NgayDi.Month
            //    && c.NgayDi.Day == item.NgayDi.Day
            //    && c.TrangThaiId != (int)ENTrangThaiPhoiVe.Huy
            //    && c.TrangThaiId != (int)ENTrangThaiPhoiVe.KetThuc
            //    ).ToList();
            var query = _phoiveRepository.Table.Where(c =>
                c.ChuyenDiId==item.ChuyenDiId
                && c.SoDoGheXeQuyTacId == item.SoDoGheXeQuyTacId
                && c.TrangThaiId != (int)ENTrangThaiPhoiVe.Huy
                && c.TrangThaiId != (int)ENTrangThaiPhoiVe.KetThuc
                ).ToList();
            if (query.Count() > 0)
            {
                //kiem tra xem thoi gian tao 
                var _item = query.First();
                //neu dang co giao dich kiem tra cac trang thai hop le thi moi dc dat cho
                if (_item.TrangThai == ENTrangThaiPhoiVe.ChoXuLy
                    || _item.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang
                    || _item.TrangThai == ENTrangThaiPhoiVe.DaThanhToan)
                    return false;

                if (isTrangThaiDatCho(_item) && _item.SessionId != item.SessionId)
                {
                    return false;
                }
                if (isTrangThaiGiuCho(_item) && _item.SessionId != item.SessionId)
                {
                    return false;
                }
                _item.NgayDi = item.NgayDi;
                _item.NguonVeXeConId = item.NguonVeXeConId;
                _item.isChonVe = item.isChonVe;
                _item.TrangThai = trangthai;
                _item.CustomerId = item.CustomerId;
                _item.NguoiDatVeId = item.NguoiDatVeId;
                _item.OrderId = item.OrderId;
                _item.SessionId = item.SessionId;
                _item.NgayUpd = DateTime.Now;
                _item.GiaVeHienTai = item.GiaVeHienTai;
                _item.GhiChu = item.GhiChu;
                _item.ChangId = item.ChangId;
                _item.MaVe = item.MaVe;

                _phoiveRepository.Update(_item);
                return true;
            }
            item.TrangThai = trangthai;
            item.NgayTao = DateTime.Now;
            item.NgayUpd = DateTime.Now;
            item.SoLanInVe = 0;
            _phoiveRepository.Insert(item);
            if (string.IsNullOrEmpty(item.MaVe))
            {
                item.MaVe = (20160 + item.Id).ToString();
                _phoiveRepository.Update(item);
            }
            return true;
        }

        ENTrangThaiPhoiVe SetTrangThaiPhoiVe(PhoiVe _item)
        {
            if (_item.TrangThai == ENTrangThaiPhoiVe.Huy)
                return ENTrangThaiPhoiVe.ConTrong;

            if (isQuaHanDatCho(_item))
            {
                return ENTrangThaiPhoiVe.ConTrong;
            }

            if (isQuaHanGiuCho(_item))
            {
                return ENTrangThaiPhoiVe.ConTrong;
            }
            return _item.TrangThai;
        }
        public virtual List<PhoiVe> GetPhoiVeByChuyenDiId(int ChuyenId)
        {

            var query = _phoiveRepository.Table.Where(c => c.ChuyenDiId == ChuyenId
                && (c.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy
                || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaThanhToan || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang
                || c.TrangThaiId == (int)ENTrangThaiPhoiVe.KetThuc));
            return query.ToList();
        }
        public PhoiVe GetPhoiVe(int NguonVeXeId, SoDoGheXeQuyTac vitri, DateTime ngaydi, bool isNewIfNull = false)
        {

            var query = _phoiveRepository.Table.Where(c => c.NguonVeXeId == NguonVeXeId && c.SoDoGheXeQuyTacId == vitri.Id
                //&& (c.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang)
                  && c.NgayDi.Year == ngaydi.Year
                  && c.NgayDi.Month == ngaydi.Month
                  && c.NgayDi.Day == ngaydi.Day
                  && c.TrangThaiId != (int)ENTrangThaiPhoiVe.Huy
                  && c.TrangThaiId != (int)ENTrangThaiPhoiVe.KetThuc
                  ).ToList();
            if (query.Count() > 0)
            {
                var _item = query.First();
                _item.TrangThai = SetTrangThaiPhoiVe(_item);
                return _item;
            }
            if (isNewIfNull)
            {
                var pv = new PhoiVe();
                pv.NgayDi = ngaydi;
                pv.NguonVeXeId = NguonVeXeId;
                pv.nguonvexe = _nguonvexeRepository.GetById(NguonVeXeId);
                pv.TrangThai = ENTrangThaiPhoiVe.ConTrong;
                pv.sodoghexequytac = vitri;
                pv.SoDoGheXeQuyTacId = vitri.Id;
                return pv;
            }
            return null;
        }

        public virtual int GetAllSoNguoi(int NguonVeId, DateTime ngaydi)
        {
            int songuoi = 0;
            var phoives = _phoiveRepository.Table.Where(c => c.NguonVeXeId == NguonVeId
                && c.NgayDi.Year == ngaydi.Year
                && c.NgayDi.Month == ngaydi.Month
                && c.NgayDi.Day == ngaydi.Day
                 && c.TrangThaiId != (int)ENTrangThaiPhoiVe.Huy
                  && c.TrangThaiId != (int)ENTrangThaiPhoiVe.KetThuc
               );

            foreach (var item in phoives)
            {
                item.TrangThai = SetTrangThaiPhoiVe(item);
                if (item.TrangThai != ENTrangThaiPhoiVe.ConTrong)
                {
                    songuoi++;
                }
            }
            return songuoi;

        }
        public virtual void InsertPhoiVeNote(PhoiVeNote item)
        {
            if (item == null)
                throw new ArgumentNullException("PhoiVe");
            item.NgayTao = DateTime.Now;
            _phoivenoteRepository.Insert(item);

        }
        public virtual decimal GetRevenueHistoryXeXuatBen(int NguonVeId, DateTime ngaydi)
        {
            decimal revenue = 0;
            var arrphoive = _phoiveRepository.Table.Where(c => c.NguonVeXeId == NguonVeId
                && (c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang
                || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DatCho
                 || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaThanhToan
                  || c.TrangThaiId == (int)ENTrangThaiPhoiVe.GiuCho
                   || c.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy)
                && c.NgayDi.Year == ngaydi.Year
                && c.NgayDi.Month == ngaydi.Month
                && c.NgayDi.Day == ngaydi.Day
               );
            foreach (var item in arrphoive)
            {
                revenue = revenue + item.GiaVeHienTai;
            }
            return revenue;

        }
        public virtual PhoiVe GetPhoiVeById(int Id)
        {
            return _phoiveRepository.GetById(Id);
        }
        /// <summary>
        /// hoan tra ve trong order
        /// </summary>
        /// <param name="item"></param>
        public virtual bool CanDoiVe(PhoiVe item)
        {
            var order = _orderService.GetOrderById(item.OrderId);

            if ((DateTime.Now.AddHours(24) > item.NgayDi)
                || order.OrderStatus == OrderStatus.Pending
                || order.ShippingStatus == ShippingStatus.Shipped)
                return false;
            return true;
        }
        public virtual void DeletePhoiVe(PhoiVe item)
        {
            if (item == null)
                throw new ArgumentNullException("PhoiVe");
            _phoiveRepository.Delete(item);
            //var phoivenote = new PhoiVeNote();
            //phoivenote.PhoiVeId = item.Id;
            //phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã bị xóa bởi {3}",item.sodoghexequytac.Val,item.nguonvexe.ToMoTa(),item.NgayDi.Date,_workContext.CurrentNhanVien.HoVaTen);
            //InsertPhoiVeNote(phoivenote);
        }
        public virtual void UpdatePhoiVe(PhoiVe item)
        {
            if (item == null)
                throw new ArgumentNullException("PhoiVe");
            item.NgayUpd = DateTime.Now;
            _phoiveRepository.Update(item);
            var phoivenote = new PhoiVeNote();
            phoivenote.PhoiVeId = item.Id;
            if (_workContext.CurrentNhanVien!=null)
                phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã được update bởi {3}", item.sodoghexequytac.Val, item.nguonvexe.ToMoTa(), item.NgayDi.Date, _workContext.CurrentNhanVien.HoVaTen);
            else
                phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã được update bởi {3}", item.sodoghexequytac.Val, item.nguonvexe.ToMoTa(), item.NgayDi.Date, "Lái xe");
            InsertPhoiVeNote(phoivenote);

        }
        public virtual void HuyPhoiVe(PhoiVe item)
        {

            item.TrangThai = ENTrangThaiPhoiVe.Huy;
            item.NgayUpd = DateTime.Now;
            _phoiveRepository.Update(item);
            var phoivenote = new PhoiVeNote();
            phoivenote.PhoiVeId = item.Id;
            if (_workContext.CurrentNhanVien != null)
                phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã bị hủy bởi {3}", item.sodoghexequytac.Val, item.nguonvexe.ToMoTa(), item.NgayDi.Date, _workContext.CurrentNhanVien.HoVaTen);
            else
                phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã bị hủy bởi {3}", item.sodoghexequytac.Val, item.nguonvexe.ToMoTa(), item.NgayDi.Date, "Lái xe");
            InsertPhoiVeNote(phoivenote);
        }

        public virtual void DeletePhoiVe(int NguonVeXeId, string SessionId, int CustomerId, DateTime NgayDi)
        {
            //bo customerid, su dung session id de update
            //var items = _phoiveRepository.Table.Where(c => (c.NguonVeXeId == NguonVeXeId || c.NguonVeXeConId == NguonVeXeId) && c.SessionId == SessionId && (c.CustomerId == CustomerId) && c.TrangThaiId == (int)ENTrangThaiPhoiVe.DatCho).ToList();
            var items = _phoiveRepository.Table.Where(c => (c.NguonVeXeId == NguonVeXeId || c.NguonVeXeConId == NguonVeXeId) && c.SessionId == SessionId
                 && c.NgayDi.Year == NgayDi.Year
                && c.NgayDi.Month == NgayDi.Month
                && c.NgayDi.Day == NgayDi.Day
                && c.TrangThaiId == (int)ENTrangThaiPhoiVe.DatCho).ToList();
            _phoiveRepository.Delete(items);

        }

        public virtual bool GetPhoiVeByNguonVe(int NguonveId, string SessionId, int CustomerId, DateTime NgayDi)
        {
            var phoive = _phoiveRepository.Table.Where(c => (c.NguonVeXeId == NguonveId || c.NguonVeXeConId == NguonveId) && c.SessionId == SessionId
                && c.NgayDi.Year == NgayDi.Year && c.NgayDi.Month == NgayDi.Month && c.NgayDi.Day == NgayDi.Day && c.TrangThaiId == (int)ENTrangThaiPhoiVe.DatCho).ToList();
            if (phoive.Count() == 0)
                return false;
            return true;

        }
        public virtual bool GiuChoPhoiVe(int NguonVeXeId, string SessionId, int CustomerId, DateTime NgayDi)
        {
            //bo customerid, su dung session id de update
            //var items = _phoiveRepository.Table.Where(c =>(c.NguonVeXeId == NguonVeXeId || c.NguonVeXeConId == NguonVeXeId) && c.SessionId == SessionId && c.CustomerId == CustomerId && c.TrangThaiId == (int)ENTrangThaiPhoiVe.DatCho).ToList();
            var items = _phoiveRepository.Table.Where(c => (c.NguonVeXeId == NguonVeXeId || c.NguonVeXeConId == NguonVeXeId) && c.SessionId == SessionId
                && c.NgayDi.Year == NgayDi.Year
                && c.NgayDi.Month == NgayDi.Month
                && c.NgayDi.Day == NgayDi.Day
                && c.TrangThaiId == (int)ENTrangThaiPhoiVe.DatCho).ToList();
            if (items.Count() == 0)
                return false;

            foreach (var item in items)
            {
                item.NgayUpd = DateTime.Now;
                item.TrangThai = ENTrangThaiPhoiVe.GiuCho;
                _phoiveRepository.Update(item);
                var phoivenote = new PhoiVeNote();
                phoivenote.PhoiVeId = item.Id;
                phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã được đưa về trạng thái {3} bởi {4}", item.sodoghexequytac.Val, item.nguonvexe.ToMoTa(), item.NgayDi.Date, item.TrangThai, _workContext.CurrentNhanVien.HoVaTen);
                InsertPhoiVeNote(phoivenote);
            }
            return true;

        }
        public virtual void ThanhToanGiaoVe(PhoiVe item)
        {
            item.NgayUpd = DateTime.Now;
            item.TrangThai = ENTrangThaiPhoiVe.DaGiaoHang;
            _phoiveRepository.Update(item);
            var phoivenote = new PhoiVeNote();
            phoivenote.PhoiVeId = item.Id;
            phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã được đưa về trạng thái {3} bởi {4}", item.sodoghexequytac.Val, item.nguonvexe.ToMoTa(), item.NgayDi.Date, item.TrangThai, _workContext.CurrentNhanVien.HoVaTen);
            InsertPhoiVeNote(phoivenote);
        }

        public virtual List<NhaXeCustomer> GetKhachHangInNhaXe(string SearchKhachhang, int nhaxeid)
        {

            var query = _nhaxecustomerRepository.Table;
            if (nhaxeid > 0)
                query = query.Where(m => m.NhaXeId == nhaxeid && m.SearchInfo.Contains(SearchKhachhang));
            return query.ToList();
        }


        public virtual void NhaXeThanhToanGiuChoPhoiVe(int NhaXeId, int NguonVeId, int ChangId, string SessionId, int NguoiDatVeId, bool DaThanhToan, int CustomerId, string GhiChu, bool IsForKid)
        {
            var chang = _hanhtrinhgiaveRepository.GetById(ChangId);
            var items = _phoiveRepository.Table.Where(c => c.NguonVeXeId == NguonVeId
                && c.SessionId == SessionId && c.CustomerId == NguoiDatVeId && c.TrangThaiId == (int)ENTrangThaiPhoiVe.DatCho).ToList();
            var _giamgia = 1m;
            if (IsForKid)
            {
                //lay thong tin cau hinh giam gia
                var _cauhinhgiamgia = GetNhaXeCauHinhByCode(NhaXeId, ENNhaXeCauHinh.GIAM_GIA_CHO_TRE_EM);
                if (_cauhinhgiamgia == null)
                {
                    _giamgia = 0.5m;
                }
                else
                {
                    if (Decimal.TryParse(_cauhinhgiamgia.GiaTri, out _giamgia))
                    {
                        _giamgia = _giamgia / 100;
                    }
                    else
                    {
                        _giamgia = 0.5m;
                    }
                }
            }
            foreach (var item in items)
            {

                item.ChangId = ChangId;
                item.GiaVeHienTai = chang.GiaVe * _giamgia;
                item.CustomerId = CustomerId;
                item.NgayUpd = DateTime.Now;
                //item.SessionId = null;
                if (DaThanhToan)
                {
                    item.TrangThai = ENTrangThaiPhoiVe.DaGiaoHang;
                }
                else
                {
                    item.TrangThai = ENTrangThaiPhoiVe.ChoXuLy;
                }
                item.GhiChu = GhiChu;
                item.IsForKid = IsForKid;

                _phoiveRepository.Update(item);
                var phoivenote = new PhoiVeNote();
                phoivenote.PhoiVeId = item.Id;
                phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã được đưa về trạng thái {3} bởi {4}", item.sodoghexequytac.Val, chang.HanhTrinh.MoTa, item.NgayDi.Date, item.TrangThai, _workContext.CurrentNhanVien.HoVaTen);
                InsertPhoiVeNote(phoivenote);
            }

        }
        public virtual void NhaXeThanhToanGiuChoPhoiVeTheoChuyen(int NhaXeId, int ChuyenDiId, int ChangId, string SessionId, int NguoiDatVeId, bool DaThanhToan, int CustomerId, string GhiChu, bool IsForKid, string ViTriLen = "", string ViTriXuong = "")
        {
            var chang = _hanhtrinhgiaveRepository.GetById(ChangId);
            var items = _phoiveRepository.Table.Where(c => c.ChuyenDiId == ChuyenDiId
                && c.SessionId == SessionId && c.TrangThaiId != (int)ENTrangThaiPhoiVe.Huy).ToList();
            var _giamgia = 1m;
            if (IsForKid)
            {
                //lay thong tin cau hinh giam gia
                var _cauhinhgiamgia = GetNhaXeCauHinhByCode(NhaXeId, ENNhaXeCauHinh.GIAM_GIA_CHO_TRE_EM);
                if (_cauhinhgiamgia == null)
                {
                    _giamgia = 0.5m;
                }
                else
                {
                    if (Decimal.TryParse(_cauhinhgiamgia.GiaTri, out _giamgia))
                    {
                        _giamgia = _giamgia / 100;
                    }
                    else
                    {
                        _giamgia = 0.5m;
                    }
                }
            }
            foreach (var item in items)
            {

                item.ChangId = ChangId;
                item.GiaVeHienTai = chang.GiaVe * _giamgia;
                item.CustomerId = CustomerId;
                item.NgayUpd = DateTime.Now;
                //item.SessionId = null;
                if (DaThanhToan)
                {
                    item.TrangThai = ENTrangThaiPhoiVe.DaGiaoHang;
                }
                else
                {
                    item.TrangThai = ENTrangThaiPhoiVe.ChoXuLy;
                }
                item.GhiChu = GhiChu;
                item.IsForKid = IsForKid;
                item.ViTriLenXe = ViTriLen;
                item.ViTriXuongXe = ViTriXuong;
                _phoiveRepository.Update(item);                
                var phoivenote = new PhoiVeNote();
                phoivenote.PhoiVeId = item.Id;
                phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã được đưa về trạng thái {3} bởi {4}", item.sodoghexequytac.Val, chang.HanhTrinh.MoTa, item.NgayDi.Date, item.TrangThai, _workContext.CurrentNhanVien.HoVaTen);
                InsertPhoiVeNote(phoivenote);
            }
        }
        public virtual void NhaXeThanhToanNhanh(int NguonVeId, int changid, string SessionId, int NguoiDatVeId)
        {

            //kiem tra xem NguonVeXeId co phai la nguon ve con ko ?   
            var chang = _hanhtrinhgiaveRepository.GetById(changid);
            var items = _phoiveRepository.Table.Where(c => (c.NguonVeXeId == NguonVeId)
                && c.SessionId == SessionId && c.NguoiDatVeId == NguoiDatVeId && c.TrangThaiId == (int)ENTrangThaiPhoiVe.DatCho).ToList();
            foreach (var item in items)
            {

                item.ChangId = changid;
                item.GiaVeHienTai = chang.GiaVe;

                item.CustomerId = CommonHelper.KhachVangLaiId; 
                item.NguoiDatVeId = NguoiDatVeId;
                item.NgayUpd = DateTime.Now;
                item.TrangThai = ENTrangThaiPhoiVe.DaGiaoHang;
                //item.SessionId = null;
                item.GhiChu = "";
                item.isChonVe = false;

                _phoiveRepository.Update(item);
                var phoivenote = new PhoiVeNote();
                phoivenote.PhoiVeId = item.Id;
                phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã được đưa về trạng thái {3} bởi {4}", item.sodoghexequytac.Val, chang.HanhTrinh.MoTa, item.NgayDi.Date, item.TrangThai, _workContext.CurrentNhanVien.HoVaTen);
                InsertPhoiVeNote(phoivenote);
            }

        }
        public virtual void NhaXeThanhToanNhanhTheoChuyen(int ChuyenDiId, int changid, string SessionId, int NguoiDatVeId)
        {

            //kiem tra xem NguonVeXeId co phai la nguon ve con ko ?   
            var chang = _hanhtrinhgiaveRepository.GetById(changid);
            var items = _phoiveRepository.Table.Where(c => (c.ChuyenDiId == ChuyenDiId)
                && c.SessionId == SessionId && c.NguoiDatVeId == NguoiDatVeId && c.TrangThaiId == (int)ENTrangThaiPhoiVe.DatCho).ToList();
            foreach (var item in items)
            {

                item.ChangId = changid;
                item.GiaVeHienTai = chang.GiaVe;

                item.CustomerId = CommonHelper.KhachVangLaiId; 
                item.NguoiDatVeId = NguoiDatVeId;
                item.NgayUpd = DateTime.Now;
                item.TrangThai = ENTrangThaiPhoiVe.DaGiaoHang;
                //item.SessionId = null;
                item.GhiChu = "";
                item.isChonVe = false;

                _phoiveRepository.Update(item);
                var phoivenote = new PhoiVeNote();
                phoivenote.PhoiVeId = item.Id;
                phoivenote.Note = string.Format("Ghế {0} thuộc tuyến {1}, ngày đi {2} đã được đưa về trạng thái {3} bởi {4}", item.sodoghexequytac.Val, chang.HanhTrinh.MoTa, item.NgayDi.Date, item.TrangThai, _workContext.CurrentNhanVien.HoVaTen);
                InsertPhoiVeNote(phoivenote);
            }

        }
        #endregion
        #region doanh thu
        public List<KhachHangMuaVeItem> GetDetailDoanhThu(int nhaxeid, DateTime ngaydi, int nhanvienid = 0)
        {
            var phoives = _phoiveRepository.Table
                .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { phoive = pv, nguonve = nv })

              .Where(c => c.nguonve.NhaXeId == nhaxeid && c.phoive.NgayDi.Year == ngaydi.Year
                  && c.phoive.NgayDi.Month == ngaydi.Month && c.phoive.NgayDi.Day == ngaydi.Day
                  && ((c.phoive.NguoiDatVeId.HasValue && c.phoive.NguoiDatVeId == nhanvienid) || nhanvienid == 0)
                  && (c.phoive.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang || c.phoive.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy)
                  )
                .Select(g => new KhachHangMuaVeItem
               {
                   CustomerId = g.phoive.CustomerId,
                   NguonVeXeId = g.phoive.NguonVeXeId,
                   customer = g.phoive.customer,
                   nguonve = g.nguonve,
                   TrangThaiPhoiVeId = g.phoive.TrangThaiId,
                   KyHieuGhe = g.phoive.sodoghexequytac.Val,
                   isChonVe = g.phoive.isChonVe,
                   GiaVe = g.phoive.GiaVeHienTai,
                   NgayDi = g.phoive.NgayDi

               }).ToList();

            foreach (var pv in phoives)
            {
                pv.SoDienThoai = pv.customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                pv.TenKhachHang = pv.customer.GetFullName();
                pv.ThongTinChuyenDi = string.Format("{0} -> {1} ({2})", pv.nguonve.TenDiemDon, pv.nguonve.TenDiemDen, pv.nguonve.ThoiGianDen.ToString("HH:mm"));
            }
            return phoives;
        }
        #region Bao cao doanh thu theo tuyen
        public List<DoanhThuTheoXeItem> GetDoanhThuTheoTuyen(DateTime tuNgay, DateTime denNgay, List<int> LichtrinhIds)
        {
            var phoives = _phoiveRepository.Table
             .Where(c => c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang && c.isChonVe == false
                && (c.NgayDi <= denNgay)
                     && (c.NgayDi >= tuNgay))
               .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })

                .Where(c => LichtrinhIds.Contains(c.NguonVeXe.LichTrinhId))
            .Select(c => new
            {
                NguonVeXeId = c.PhoiVe.NguonVeXeId,
                GiaVe = c.PhoiVe.GiaVeHienTai,
                NgayDi = c.PhoiVe.NgayDi,
                TrangThaiPhoiVeId = c.PhoiVe.TrangThaiId
            })
            .GroupBy(c => new { c.NgayDi })
            .Select(g => new DoanhThuTheoXeItem
            {
                ItemDataDate = g.Key.NgayDi,
                GiaTri = g.Sum(a => a.GiaVe),
                SoLuong = g.Count()

                //  NguonVeXe=g

            })
            .OrderByDescending(sx => sx.ItemDataDate)
            .ToList();
            var thongkexe = new List<DoanhThuTheoXeItem>();
            foreach (var pv in phoives)
            {
                pv.Nhan = pv.ItemDataDate.ToString("dd-MM-yyyy");
                pv.NhanSapXep = pv.ItemDataDate.ToString("yyyyMMdd");
                //  pv.ThongTinChuyenDi = string.Format("{0} -> {1} ({2})", pv.NguonVeXe.TenDiemDon, pv.NguonVeXe.TenDiemDen, pv.NguonVeXe.ThoiGianDen.ToString("HH:mm"));
                thongkexe.Add(pv);
            }
            return thongkexe;
        }
        public List<KhachHangMuaVeItem> GetKhachHangMuaVeTheoTuyen(int nhaxeid, DateTime tuNgay, DateTime denNgay, List<int> LichtrinhIds, int KhachHangId = 0)
        {
            if (KhachHangId == 0)
            {
                var phoives = _phoiveRepository.Table
              .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })
             .Where(c => c.NguonVeXe.NhaXeId == nhaxeid && (c.PhoiVe.NgayDi <= denNgay)
                    && (c.PhoiVe.NgayDi >= tuNgay)
                 && (c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang || c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy)
                 )
                 .GroupBy(gb => new { gb.PhoiVe.customer })
           .Select(g => new KhachHangMuaVeItem
           {
               CustomerId = g.Key.customer.Id,
               customer = g.Key.customer,
               SoLuot = g.Count()
           })
           .ToList();

                foreach (var pv in phoives)
                {
                    pv.SoDienThoai = pv.customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                    pv.TenKhachHang = pv.customer.GetFullName();
                    //pv.ThongTinChuyenDi = string.Format("{0} -> {1} ({2})", pv.nguonve.TenDiemDon, pv.nguonve.TenDiemDen, pv.nguonve.ThoiGianDen.ToString("HH:mm"));
                }
                return phoives;
            }
            else
            {
                var phoives = _phoiveRepository.Table
              .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })
             .Where(c => c.NguonVeXe.NhaXeId == nhaxeid && (c.PhoiVe.NgayDi <= denNgay)
                    && (c.PhoiVe.NgayDi >= tuNgay) && c.PhoiVe.CustomerId==KhachHangId
                 && (c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang || c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy)
                 )                 
           .Select(g => new KhachHangMuaVeItem
           {
               CustomerId = g.PhoiVe.CustomerId,
               NguonVeXeId = g.PhoiVe.NguonVeXeId,
               customer = g.PhoiVe.customer,
               nguonve = g.NguonVeXe,
               TrangThaiPhoiVeId = g.PhoiVe.TrangThaiId,
               KyHieuGhe = g.PhoiVe.sodoghexequytac.Val,
               isChonVe = g.PhoiVe.isChonVe,
               GiaVe = g.PhoiVe.GiaVeHienTai,
               NgayDi = g.PhoiVe.NgayDi
           })
           .ToList();

                foreach (var pv in phoives)
                {
                    pv.SoDienThoai = pv.customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                    pv.TenKhachHang = pv.customer.GetFullName();
                    pv.ThongTinChuyenDi = string.Format("{0} -> {1} ({2})", pv.nguonve.TenDiemDon, pv.nguonve.TenDiemDen, pv.nguonve.ThoiGianDen.ToString("HH:mm"));
                }
                return phoives;
            }

        }
        public List<KhachHangMuaVeItem> GetDetailDoanhThuTheoTuyen(DateTime ngaydi, int nhaxeid, int HanhTrinhId = 0)
        {
            var phoives = _phoiveRepository.Table
               .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })
               .Join(_hisRepository.Table, pv => pv.NguonVeXe.Id, hisxexuatben => hisxexuatben.NguonVeId, (pv, hisxexuatben) => new { PhoiVe = pv.PhoiVe, NguonVeXe = pv.NguonVeXe, HisXeXuatBen = hisxexuatben })
              .Where(c => c.NguonVeXe.NhaXeId == nhaxeid && (c.HisXeXuatBen.XeVanChuyenId == HanhTrinhId || HanhTrinhId == 0) && c.PhoiVe.NgayDi.Year == ngaydi.Year
                  && c.PhoiVe.NgayDi.Month == ngaydi.Month && c.PhoiVe.NgayDi.Day == ngaydi.Day
                  && (c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang || c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy)
                  )
            .Select(g => new KhachHangMuaVeItem
            {
                CustomerId = g.PhoiVe.CustomerId,
                NguonVeXeId = g.PhoiVe.NguonVeXeId,
                customer = g.PhoiVe.customer,
                nguonve = g.NguonVeXe,
                TrangThaiPhoiVeId = g.PhoiVe.TrangThaiId,
                KyHieuGhe = g.PhoiVe.sodoghexequytac.Val,
                isChonVe = g.PhoiVe.isChonVe,
                GiaVe = g.PhoiVe.GiaVeHienTai,
                NgayDi = g.PhoiVe.NgayDi
            })
            .ToList();

            foreach (var pv in phoives)
            {
                pv.SoDienThoai = pv.customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                pv.TenKhachHang = pv.customer.GetFullName();
                pv.ThongTinChuyenDi = string.Format("{0} -> {1} ({2})", pv.nguonve.TenDiemDon, pv.nguonve.TenDiemDen, pv.nguonve.ThoiGianDen.ToString("HH:mm"));
            }
            return phoives;
        }
        public List<ThongKeItem> GetDoanhThuTheoChang(int HanhTrinhId, DateTime NgayBan)
        {

            var phoives = _phoiveRepository.Table
               .Where(c => (c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaThanhToan)
                    && c.isChonVe == false
                    && (c.NgayDi.Year == NgayBan.Year)
                     && (c.NgayDi.Month == NgayBan.Month)
                     && (c.NgayDi.Day == NgayBan.Day))
             .Select(c => new
             {
                 NguonVeConId = c.NguonVeXeConId,
                 GiaVe = c.GiaVeHienTai,

             })
             .GroupBy(c => new { c.NguonVeConId })
             .Select(g => new ThongKeItem
             {
                 ItemId = g.Key.NguonVeConId,
                 GiaTri = g.Sum(a => a.GiaVe),
                 SoLuong = g.Count()
             })
             .OrderByDescending(sx => sx.ItemId)
             .ToList();
            var tknhanvien = new List<ThongKeItem>();
            foreach (var item in phoives)
            {
                item.Nhan = item.GiaTri.ToString();
                item.NhanSapXep = item.GiaTri.ToString();

                tknhanvien.Add(item);
            }
            return tknhanvien;

        }
        #endregion
        #region doanh thu theo xe
        public List<DoanhThuTheoXeItem> GetDoanhThuBanVeTungXeTheoNgay(DateTime tuNgay, DateTime denNgay, int nhaxeid, int XeId)
        {
            var phoives = _phoiveRepository.Table
               .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })
               .Join(_hisRepository.Table, pv => pv.PhoiVe.NguonVeXeId, hisxexuatben => hisxexuatben.NguonVeId, (pv, hisxexuatben) => new { PhoiVe = pv.PhoiVe, NguonVeXe = pv.NguonVeXe, HisXeXuatBen = hisxexuatben })
              .Where(c => c.NguonVeXe.NhaXeId == nhaxeid && c.HisXeXuatBen.XeVanChuyenId == XeId
                   && (c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang || c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy)
                   && c.PhoiVe.isChonVe == false
                   && (c.PhoiVe.NgayDi <= denNgay)
                   && (c.PhoiVe.NgayDi >= tuNgay))
            .Select(c => new
            {
                NguonVeXeId = c.PhoiVe.NguonVeXeId,
                XeId = c.HisXeXuatBen.XeVanChuyenId,
                GiaVe = c.PhoiVe.GiaVeHienTai,
                NgayDi = c.PhoiVe.NgayDi,
                TrangThaiPhoiVeId = c.PhoiVe.TrangThaiId
            })
            .GroupBy(c => new { c.NgayDi })
            .Select(g => new DoanhThuTheoXeItem
            {
                ItemDataDate = g.Key.NgayDi,
                GiaTri = g.Sum(a => a.GiaVe),
                SoLuong = g.Count()

                //  NguonVeXe=g

            })
            .OrderByDescending(sx => sx.ItemDataDate)
            .ToList();
            var thongkexe = new List<DoanhThuTheoXeItem>();
            foreach (var pv in phoives)
            {
                pv.Nhan = pv.ItemDataDate.ToString("dd-MM-yyyy");
                pv.NhanSapXep = pv.ItemDataDate.ToString("yyyyMMdd");
                //  pv.ThongTinChuyenDi = string.Format("{0} -> {1} ({2})", pv.NguonVeXe.TenDiemDon, pv.NguonVeXe.TenDiemDen, pv.NguonVeXe.ThoiGianDen.ToString("HH:mm"));
                thongkexe.Add(pv);
            }
            return thongkexe;
        }
        public List<KhachHangMuaVeItem> GetDetailDoanhThuBanVeTungXeTheoNgay(DateTime ngaydi, int nhaxeid, int XeId = 0)
        {
            var phoives = _phoiveRepository.Table
               .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })
               .Join(_hisRepository.Table, pv => pv.NguonVeXe.Id, hisxexuatben => hisxexuatben.NguonVeId, (pv, hisxexuatben) => new { PhoiVe = pv.PhoiVe, NguonVeXe = pv.NguonVeXe, HisXeXuatBen = hisxexuatben })
              .Where(c => c.NguonVeXe.NhaXeId == nhaxeid && (c.HisXeXuatBen.XeVanChuyenId == XeId || XeId == 0) && c.PhoiVe.NgayDi.Year == ngaydi.Year
                  && c.PhoiVe.NgayDi.Month == ngaydi.Month && c.PhoiVe.NgayDi.Day == ngaydi.Day
                  && (c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang || c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy)
                  )
            .Select(g => new KhachHangMuaVeItem
            {
                CustomerId = g.PhoiVe.CustomerId,
                NguonVeXeId = g.PhoiVe.NguonVeXeId,
                customer = g.PhoiVe.customer,
                nguonve = g.NguonVeXe,
                TrangThaiPhoiVeId = g.PhoiVe.TrangThaiId,
                KyHieuGhe = g.PhoiVe.sodoghexequytac.Val,
                isChonVe = g.PhoiVe.isChonVe,
                GiaVe = g.PhoiVe.GiaVeHienTai,
                NgayDi = g.PhoiVe.NgayDi
            })
            .ToList();

            foreach (var pv in phoives)
            {
                pv.SoDienThoai = pv.customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                pv.TenKhachHang = pv.customer.GetFullName();
                pv.ThongTinChuyenDi = string.Format("{0} -> {1} ({2})", pv.nguonve.TenDiemDon, pv.nguonve.TenDiemDen, pv.nguonve.ThoiGianDen.ToString("HH:mm"));
            }
            return phoives;
        }

        #endregion
        public List<ThongKeItem> GetAllPhoiVe(int thang, int nam, int nhaxeid, ENBaoCaoChuKyThoiGian ChuKyThoiGianId)
        {

            if (ChuKyThoiGianId == ENBaoCaoChuKyThoiGian.HangThang)
                thang = 0;
            if (ChuKyThoiGianId == ENBaoCaoChuKyThoiGian.HangNam)
                nam = 0;

            var phoives = _phoiveRepository.Table
                .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })
                .Where(c => c.NguonVeXe.NhaXeId == nhaxeid && c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang
                    && (c.PhoiVe.NgayDi.Month == thang || thang == 0) && (c.PhoiVe.NgayDi.Year == nam || nam == 0))

             .Select(c => new
             {
                 Ngay = c.PhoiVe.NgayDi.Day,
                 Thang = c.PhoiVe.NgayDi.Month,
                 Nam = c.PhoiVe.NgayDi.Year,
                 TongDoanhThu = c.PhoiVe.GiaVeHienTai,
                 ischonve = c.PhoiVe.isChonVe,
             }).ToList();
            if (ChuKyThoiGianId == ENBaoCaoChuKyThoiGian.HangNgay)
            {
                var doanhthungay = phoives.GroupBy(c => c.Ngay).Select(g => new ThongKeItem
               {
                   Nhan = g.Key.ToString(),
                   NhanSapXep = g.Key.ToString(),
                   GiaTri = g.Sum(a => a.TongDoanhThu),
                   SoLuong = g.Count(),
                   GiaTri1 = g.Where(a => a.ischonve == true).Sum(a => a.TongDoanhThu),
                   GiaTri2 = g.Where(a => a.ischonve == false).Sum(a => a.TongDoanhThu)

               }).ToList();

                return doanhthungay;

            }
            if (ChuKyThoiGianId == ENBaoCaoChuKyThoiGian.HangThang)
            {
                var doanhthuthang = phoives.GroupBy(c => c.Thang).Select(g => new ThongKeItem
                  {
                      Nhan = g.Key.ToString(),
                      NhanSapXep = g.Key.ToString(),
                      GiaTri = g.Sum(a => a.TongDoanhThu),
                      SoLuong = g.Count(),
                      GiaTri1 = g.Where(a => a.ischonve == true).Sum(a => a.TongDoanhThu),
                      GiaTri2 = g.Where(a => a.ischonve == false).Sum(a => a.TongDoanhThu)
                  }).ToList();
                return doanhthuthang;
            }
            if (ChuKyThoiGianId == ENBaoCaoChuKyThoiGian.HangNam)
            {
                var doanhthunam = phoives.GroupBy(c => c.Nam).Select(g => new ThongKeItem
                   {
                       Nhan = g.Key.ToString(),
                       NhanSapXep = g.Key.ToString(),
                       GiaTri = g.Sum(a => a.TongDoanhThu),
                       SoLuong = g.Count(),
                       GiaTri1 = g.Where(a => a.ischonve == true).Sum(a => a.TongDoanhThu),
                       GiaTri2 = g.Where(a => a.ischonve == false).Sum(a => a.TongDoanhThu)
                   }).ToList();
                return doanhthunam;
            }

            return null;

        }

        public List<ThongKeItem> GetDoanhThuBanVeTheoNgay(DateTime tuNgay, DateTime denNgay, int nhaxeid, int VanPhongId,int LichTrinhId, int ChangId,int XeId )
        {

            var pvs = _phoiveRepository.Table
                .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })
                .Join(_nhanvienRepository.Table, pv => pv.PhoiVe.NguoiDatVeId, nhanvien => nhanvien.Id, (pv, nhanvien) => new { PhoiVe = pv.PhoiVe, NguonVeXe = pv.NguonVeXe, Nhanvien = nhanvien })
               .Where(c => c.NguonVeXe.NhaXeId == nhaxeid
                    && (c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang || c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy)
                    && c.PhoiVe.isChonVe == false
                    && (c.PhoiVe.NguoiDatVeId.HasValue && c.PhoiVe.NguoiDatVeId == c.Nhanvien.Id && c.Nhanvien.VanPhongID == VanPhongId)
                    && (c.PhoiVe.NgayDi <= denNgay)
                     && (c.PhoiVe.NgayDi >= tuNgay));
            if(LichTrinhId>0)
            {
                pvs=pvs.Where(c=>c.NguonVeXe.LichTrinhId==LichTrinhId);

            }
            if (ChangId > 0)
                pvs = pvs.Where(c => c.PhoiVe.ChangId == ChangId);
            if(XeId>0)
                pvs=pvs.Where(c=>c.PhoiVe.ChuyenDi!=null && c.PhoiVe.ChuyenDi.XeVanChuyenId==XeId);
            var phoives = pvs
             .Select(c => new
             {
                 NhanVienId = c.PhoiVe.NguoiDatVeId.Value,
                 GiaVe = c.PhoiVe.GiaVeHienTai,
                 NgayDi = c.PhoiVe.NgayDi,
                 TrangThaiPhoiVeId = c.PhoiVe.TrangThaiId
             })
             .GroupBy(c => new { c.NgayDi })
             .Select(g => new ThongKeItem
             {
                 ItemDataDate = g.Key.NgayDi,
                 GiaTri = g.Sum(a => a.GiaVe),
                 SoLuong = g.Count()
             })
             .OrderByDescending(sx => sx.ItemDataDate)
             .ToList();
            var tknhanvien = new List<ThongKeItem>();
            foreach (var item in phoives)
            {
                item.Nhan = item.ItemDataDate.ToString("dd-MM-yyyy");
                item.NhanSapXep = item.ItemDataDate.ToString("yyyyMMdd");
                tknhanvien.Add(item);
            }
            return tknhanvien;

        }



        public List<ThongKeItem> GetDoanhThuBanVeTheoNhanVien(int nhaxeid, int VanPhongId, DateTime NgayBan)
        {

            var phoives = _phoiveRepository.Table
                .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })
               .Where(c => c.NguonVeXe.NhaXeId == nhaxeid
                    && (c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang || c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.DaThanhToan)
                    && c.PhoiVe.isChonVe == false && c.PhoiVe.NguoiDatVeId.HasValue
                    && (c.PhoiVe.NgayDi.Year == NgayBan.Year)
                     && (c.PhoiVe.NgayDi.Month == NgayBan.Month)
                     && (c.PhoiVe.NgayDi.Day == NgayBan.Day))
             .Select(c => new
             {
                 NhanVienId = c.PhoiVe.NguoiDatVeId.Value,
                 GiaVe = c.PhoiVe.GiaVeHienTai,

             })
             .GroupBy(c => new { c.NhanVienId })
             .Select(g => new ThongKeItem
             {
                 ItemId = g.Key.NhanVienId,
                 GiaTri = g.Sum(a => a.GiaVe),
                 SoLuong = g.Count()
             })
             .OrderByDescending(sx => sx.ItemId)
             .ToList();
            var tknhanvien = new List<ThongKeItem>();
            foreach (var item in phoives)
            {
                item.Nhan = item.GiaTri.ToString();
                item.NhanSapXep = item.GiaTri.ToString();
                var checknhanvien = _nhanvienRepository.Table.Where(c => c.Id == item.ItemId && c.VanPhongID == VanPhongId).Count();
                if (checknhanvien > 0)
                    tknhanvien.Add(item);
            }
            return tknhanvien;

        }
        public List<ThongKeItem> GetDoanhThuBanVeTheoTrangThai(int nhaxeid, int VanPhongId, DateTime NgayBan, int NhanVienId)
        {

            var phoives = _phoiveRepository.Table
                .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })
               .Where(c => c.NguonVeXe.NhaXeId == nhaxeid
                    && (c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang || c.PhoiVe.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy)
                    && c.PhoiVe.isChonVe == false
                    && c.PhoiVe.NguoiDatVeId.Value == NhanVienId
                    && (c.PhoiVe.NgayDi.Year == NgayBan.Year)
                     && (c.PhoiVe.NgayDi.Month == NgayBan.Month)
                     && (c.PhoiVe.NgayDi.Day == NgayBan.Day))
             .Select(c => new
             {
                 TrangThaiPhoiVeId = c.PhoiVe.TrangThaiId,
                 GiaVe = c.PhoiVe.GiaVeHienTai,

             })
             .GroupBy(c => new { c.TrangThaiPhoiVeId })
             .Select(g => new ThongKeItem
             {
                 TrangThaiPhoiVeId = g.Key.TrangThaiPhoiVeId,
                 GiaTri = g.Sum(a => a.GiaVe),
                 SoLuong = g.Count()
             })
             .OrderByDescending(sx => sx.TrangThaiPhoiVeId)
             .ToList();
            var tknhanvien = new List<ThongKeItem>();
            foreach (var item in phoives)
            {
                item.Nhan = item.GiaTri.ToString();
                item.NhanSapXep = item.GiaTri.ToString();
                var checknhanvien = _nhanvienRepository.Table.Where(c => c.Id == NhanVienId && c.VanPhongID == VanPhongId).Count();
                if (checknhanvien > 0)
                    tknhanvien.Add(item);
            }
            return tknhanvien;

        }
        public virtual decimal GetTongDoanhThu(List<DoanhThuItem> doanhthus, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId)
        {
            decimal _doanhthu = decimal.Zero;
            foreach (var item in doanhthus)
            {
                switch (LoaiThoiGianId)
                {
                    case ENBaoCaoLoaiThoiGian.TheoThang:
                        {
                            if (item.Nam == nam && item.Thang == thang)
                            {
                                _doanhthu = _doanhthu + item.DoanhThu;

                            }
                            break;
                        }
                    case ENBaoCaoLoaiThoiGian.TheoQuy:
                        {
                            thang = 0;
                            if (item.Nam == nam)
                            {
                                switch (QuyId)
                                {
                                    case ENBaoCaoQuy.Quy1:
                                        if (item.Thang >= 1 && item.Thang <= 3)
                                            _doanhthu = _doanhthu + item.DoanhThu;
                                        break;
                                    case ENBaoCaoQuy.Quy2:
                                        if (item.Thang >= 4 && item.Thang <= 6)
                                            _doanhthu = _doanhthu + item.DoanhThu;
                                        break;
                                    case ENBaoCaoQuy.Quy3:
                                        if (item.Thang >= 7 && item.Thang <= 9)
                                            _doanhthu = _doanhthu + item.DoanhThu;
                                        break;
                                    case ENBaoCaoQuy.Quy4:
                                        if (item.Thang >= 10 && item.Thang <= 12)
                                            _doanhthu = _doanhthu + item.DoanhThu;
                                        break;
                                }
                            }
                            break;
                        }
                    case ENBaoCaoLoaiThoiGian.TheoNam:
                        {
                            QuyId = 0;
                            thang = 0;
                            if (item.Nam == nam)
                            {
                                _doanhthu = _doanhthu + item.DoanhThu;
                            }
                            break;
                        }
                }

            }
            return _doanhthu;
        }
        public virtual void ProcessTime(int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int Thang1, out int Thang2)
        {
            Thang1 = thang;
            Thang2 = thang;

            if (LoaiThoiGianId == ENBaoCaoLoaiThoiGian.TheoQuy)
            {
                switch (QuyId)
                {
                    case ENBaoCaoQuy.Quy1:
                        Thang1 = 1;
                        Thang2 = 3;
                        break;
                    case ENBaoCaoQuy.Quy2:
                        Thang1 = 4;
                        Thang2 = 6;
                        break;
                    case ENBaoCaoQuy.Quy3:
                        Thang1 = 7;
                        Thang2 = 9;
                        break;
                    case ENBaoCaoQuy.Quy4:
                        Thang1 = 10;
                        Thang2 = 12;
                        break;
                }
            }
            else if (LoaiThoiGianId == ENBaoCaoLoaiThoiGian.TheoNam)
            {
                Thang1 = 1;
                Thang2 = 12;
            }
        }
        public virtual decimal DoanhThuTuyen(List<int> LichtrinhIds, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int SoLuong)
        {
            int Thang1, Thang2;
            ProcessTime(thang, nam, QuyId, LoaiThoiGianId, out Thang1, out Thang2);
            var phoives = _phoiveRepository.Table.Where(c => c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang && c.isChonVe == false
                && c.NgayDi.Year == nam
                && (c.NgayDi.Month >= Thang1 && c.NgayDi.Month <= Thang2)
                )
               .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })

                .Where(c => LichtrinhIds.Contains(c.NguonVeXe.LichTrinhId))
                .Select(c => new DoanhThuItem
                {
                    Ngay = c.PhoiVe.NgayDi.Day,
                    Thang = c.PhoiVe.NgayDi.Month,
                    Nam = c.PhoiVe.NgayDi.Year,
                    DoanhThu = c.PhoiVe.GiaVeHienTai,
                }).ToList();
            SoLuong = phoives.Count;
            return GetTongDoanhThu(phoives, thang, nam, QuyId, LoaiThoiGianId);

        }
        public virtual decimal DoanhThuXeTheoTuyen(List<int> HisXeXuatBen, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int SoLuong)
        {
            int Thang1, Thang2;
            ProcessTime(thang, nam, QuyId, LoaiThoiGianId, out Thang1, out Thang2);
            var phoives = _phoiveRepository.Table.Where(c => c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang && c.isChonVe == false
                && c.NgayDi.Year == nam && (c.NgayDi.Month >= Thang1 && c.NgayDi.Month <= Thang2)).Join(_hisRepository.Table, pv => pv.NguonVeXeId, his => his.NguonVeId,
                (pv, his) => new { PhoiVe = pv, History = his }).Where(c => HisXeXuatBen.Contains(c.History.NguonVeId)).Select(c => new DoanhThuItem
                {
                    Ngay = c.PhoiVe.NgayDi.Day,
                    Thang = c.PhoiVe.NgayDi.Month,
                    Nam = c.PhoiVe.NgayDi.Year,
                    DoanhThu = c.PhoiVe.GiaVeHienTai,

                }).ToList();
            SoLuong = phoives.Count;
            return GetTongDoanhThu(phoives, thang, nam, QuyId, LoaiThoiGianId);
        }
        public virtual decimal DoanhThuTuyenCon(List<int> NguonVeIds, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int SoLuong)
        {
            int Thang1, Thang2;
            ProcessTime(thang, nam, QuyId, LoaiThoiGianId, out Thang1, out Thang2);
            var phoives = _phoiveRepository.Table.Where(c => c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang && c.isChonVe == false
                && c.NgayDi.Year == nam
                && (c.NgayDi.Month >= Thang1 && c.NgayDi.Month <= Thang2)
                && (NguonVeIds.Contains(c.NguonVeXeConId == 0 ? c.NguonVeXeId : c.NguonVeXeConId)))
                .Select(c => new DoanhThuItem
                {
                    Ngay = c.NgayDi.Day,
                    Thang = c.NgayDi.Month,
                    Nam = c.NgayDi.Year,
                    DoanhThu = c.GiaVeHienTai,
                }).ToList();
            SoLuong = phoives.Count;
            return GetTongDoanhThu(phoives, thang, nam, QuyId, LoaiThoiGianId);

        }
        public virtual decimal DoanhThuLichTrinh(int lichtrinhid, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int SoLuong)
        {
            int Thang1, Thang2;
            ProcessTime(thang, nam, QuyId, LoaiThoiGianId, out Thang1, out Thang2);
            var phoives = _phoiveRepository.Table.Where(c => c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang && c.isChonVe == false
                && c.NgayDi.Year == nam
                && (c.NgayDi.Month >= Thang1 && c.NgayDi.Month <= Thang2)
                )
               .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })

                .Where(c => c.NguonVeXe.LichTrinhId == lichtrinhid)
                .Select(c => new DoanhThuItem
                {
                    Ngay = c.PhoiVe.NgayDi.Day,
                    Thang = c.PhoiVe.NgayDi.Month,
                    Nam = c.PhoiVe.NgayDi.Year,
                    DoanhThu = c.PhoiVe.GiaVeHienTai
                }).ToList();
            SoLuong = phoives.Count;
            return GetTongDoanhThu(phoives, thang, nam, QuyId, LoaiThoiGianId);

        }

        public virtual decimal DoanhThuVanPhong(List<int> nhavienids, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int SoLuong)
        {
            int Thang1, Thang2;
            ProcessTime(thang, nam, QuyId, LoaiThoiGianId, out Thang1, out Thang2);
            var phoives = _phoiveRepository.Table.Where(c => c.NguoiDatVeId.HasValue && nhavienids.Contains(c.NguoiDatVeId.Value)
                && c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang
                && c.NgayDi.Year == nam
                && (c.NgayDi.Month >= Thang1 && c.NgayDi.Month <= Thang2)
                )
               .Join(_nguonvexeRepository.Table, pv => pv.NguonVeXeId, nv => nv.Id, (pv, nv) => new { PhoiVe = pv, NguonVeXe = nv })

                .Select(c => new DoanhThuItem
                {
                    Ngay = c.PhoiVe.NgayDi.Day,
                    Thang = c.PhoiVe.NgayDi.Month,
                    Nam = c.PhoiVe.NgayDi.Year,
                    DoanhThu = c.PhoiVe.GiaVeHienTai
                }).ToList();
            SoLuong = phoives.Count;
            return GetTongDoanhThu(phoives, thang, nam, QuyId, LoaiThoiGianId);
        }
        #endregion
        public virtual List<PhoiVe> GetPhoiVeByCustomer(int CustomerId)
        {
            var phoive = _phoiveRepository.Table.Where(c => c.CustomerId == CustomerId && c.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy).ToList();
            return phoive;
        }
        public virtual List<PhoiVe> GetPhoiVeDatChoBySession(object SessionId)
        {
            var phoives = _phoiveRepository.Table.Where(c => c.SessionId == SessionId.ToString() && c.TrangThaiId == (int)ENTrangThaiPhoiVe.DatCho).ToList().Where(c => !isQuaHanDatCho(c)).ToList();
            return phoives;
        }
        #region Don dat & Thanh toan
        public virtual List<PhoiVe> GetPhoiVeGiuChoBySession(object SessionId)
        {
            var phoives = _phoiveRepository.Table.Where(c => c.SessionId == SessionId.ToString() && c.TrangThaiId == (int)ENTrangThaiPhoiVe.GiuCho).ToList().Where(c => !isQuaHanGiuCho(c)).ToList();
            return phoives;
        }
        public virtual bool ThanhToan(string SessionId, int CustomerId, Address shippingAddress, out int OrderId)
        {
            OrderId = 0;
            var phoives = _phoiveRepository.Table.Where(c => (c.SessionId == SessionId || c.CustomerId == CustomerId) && c.TrangThaiId == (int)ENTrangThaiPhoiVe.GiuCho).ToList().Where(c => !isQuaHanGiuCho(c)).ToList();
            if (phoives.Count == 0)
                return false;

            decimal tongdondat = decimal.Zero;
            var product = phoives[0].getNguonVeXe().ProductInfo;
            tongdondat = phoives.Count * product.Price;

            var order = new Order
            {
                StoreId = 0,
                OrderGuid = Guid.NewGuid(),
                CustomerId = CustomerId,
                CustomerIp = _webHelper.GetCurrentIpAddress(),
                OrderSubtotalInclTax = tongdondat,
                OrderSubtotalExclTax = tongdondat,
                OrderSubTotalDiscountInclTax = tongdondat,
                OrderSubTotalDiscountExclTax = tongdondat,
                OrderShippingInclTax = decimal.Zero,
                OrderShippingExclTax = decimal.Zero,
                PaymentMethodAdditionalFeeInclTax = decimal.Zero,
                PaymentMethodAdditionalFeeExclTax = decimal.Zero,
                OrderTax = decimal.Zero,
                OrderTotal = tongdondat,
                RefundedAmount = decimal.Zero,
                OrderDiscount = decimal.Zero,
                OrderStatus = OrderStatus.Pending,
                PaymentStatus = Core.Domain.Payments.PaymentStatus.Pending,
                PaidDateUtc = null,
                BillingAddress = shippingAddress,
                ShippingAddress = shippingAddress,
                ShippingStatus = ShippingStatus.NotYetShipped,
                CreatedOnUtc = DateTime.UtcNow
            };
            _orderService.InsertOrder(order);
            OrderId = order.Id;
            var orderItem = new OrderItem
            {
                OrderItemGuid = Guid.NewGuid(),
                Order = order,
                ProductId = product.Id,
                UnitPriceInclTax = decimal.Zero,
                UnitPriceExclTax = decimal.Zero,
                PriceInclTax = product.Price,
                PriceExclTax = product.Price,
                OriginalProductCost = product.Price,
                Quantity = phoives.Count,
                DiscountAmountInclTax = decimal.Zero,
                DiscountAmountExclTax = decimal.Zero,
                DownloadCount = 0,
                IsDownloadActivated = false,
                LicenseDownloadId = 0
            };
            order.OrderItems.Add(orderItem);
            _orderService.UpdateOrder(order);
            order.OrderNotes.Add(new OrderNote
            {
                Note = "Đặt mua vé",
                DisplayToCustomer = false,
                CreatedOnUtc = DateTime.UtcNow
            });
            _orderService.UpdateOrder(order);
            foreach (var item in phoives)
            {
                item.TrangThai = ENTrangThaiPhoiVe.ChoXuLy;
                item.OrderId = order.Id;
                _phoiveRepository.Update(item);
            }
            return true;
        }
        public virtual List<PhoiVe> GetPhoiVeByOrderId(int OrderId)
        {

            var query = _phoiveRepository.Table.Where(c => c.OrderId == OrderId
                && (c.TrangThaiId == (int)ENTrangThaiPhoiVe.GiuCho || c.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy
                || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaThanhToan || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang
                || c.TrangThaiId == (int)ENTrangThaiPhoiVe.KetThuc));
            return query.ToList();
        }
        public virtual List<PhoiVe> GetPhoiVeByChuyenDi(int NguonVeXeId, DateTime NgayDi)
        {
            var query = _phoiveRepository.Table.Where(c => (c.NguonVeXeId == NguonVeXeId || c.NguonVeXeConId == NguonVeXeId)
                && (c.TrangThaiId == (int)ENTrangThaiPhoiVe.ChoXuLy
                || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaThanhToan || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang
                || c.TrangThaiId == (int)ENTrangThaiPhoiVe.KetThuc)
                 && c.NgayDi.Year == NgayDi.Year
                && c.NgayDi.Month == NgayDi.Month
                && c.NgayDi.Day == NgayDi.Day
                );
            return query.ToList();
        }
        public virtual List<PhoiVe> GetPhoiVeByChuyenDi(int ChuyenDiId, bool isThucTe = false)
        {
            if (isThucTe)
            {
                var query = _phoiveRepository.Table.Where(c => c.ChuyenDiId == ChuyenDiId
                && c.TrangThaiId != (int)ENTrangThaiPhoiVe.Huy && c.TrangThaiId != (int)ENTrangThaiPhoiVe.DatCho);
                return query.ToList();
            }
            else
            {
                var query = _phoiveRepository.Table.Where(c => c.ChuyenDiId == ChuyenDiId
                && c.TrangThaiId != (int)ENTrangThaiPhoiVe.Huy);
                return query.ToList();
            }
            
        }
        #endregion
        #region quản lý hủy vé
        public virtual List<PhoiVe> GetPhoiVeYeuCauHuy(int VanPhongId)
        {

            var phoives = _phoiveRepository.Table.Where(c => c.IsRequireCancel &&
                (c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaThanhToan || c.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang)
                 && c.TrangThaiId != (int)ENTrangThaiPhoiVe.Huy
                  && c.TrangThaiId != (int)ENTrangThaiPhoiVe.KetThuc
                  && c.NguoiDatVeId != null
               ).ToList();

            var arrphoive = new List<PhoiVe>();
            foreach (var item in phoives)
            {
                var checknhanvien = _nhanvienRepository.Table.Where(c => c.Id == item.NguoiDatVeId && c.VanPhongID == VanPhongId).Count();
                if (checknhanvien > 0)
                    arrphoive.Add(item);
            }
            return arrphoive.ToList();

        }
        #endregion

        public virtual List<NguonVeXe> GetAllNguonVeXeByLichTrinhId(int LichTrinhId)
        {
            var query = _nguonvexeRepository.Table.Where(c => c.LichTrinhId == LichTrinhId).ToList();
            return query;
        }

        public virtual List<HistoryXeXuatBen> GetAllHisByNguonVeId(int NguonVeId)
        {
            var query = _hisRepository.Table.Where(c => c.NguonVeId == NguonVeId && c.TrangThaiId == (int)ENTrangThaiXeXuatBen.KET_THUC).ToList();
            return query;
        }
        public virtual PagedList<PhoiVeNote> GetLichSuPhoiVe(DateTime? NgayGiaoDich = null, int NguonVeXeId = 0, int pageIndex = 0,
               int pageSize = int.MaxValue)
        {
            var query = _phoivenoteRepository.Table
                 .Join(_phoiveRepository.Table, pvn => pvn.PhoiVeId, nv => nv.Id, (pvn, nv) => new { PhoiVenote = pvn, phoive = nv })
               .Where(c => c.phoive.NguonVeXeId == NguonVeXeId
                    && c.PhoiVenote.NgayTao.Year == NgayGiaoDich.Value.Year
                    && c.PhoiVenote.NgayTao.Month == NgayGiaoDich.Value.Month
                    && c.PhoiVenote.NgayTao.Day == NgayGiaoDich.Value.Day)
                   .OrderByDescending(m => m.PhoiVenote.Id)
                     .Select(c => c.PhoiVenote);
            return new PagedList<PhoiVeNote>(query, pageIndex, pageSize);
        }
        #region Nha xe cau hinh
        private NhaXeCauHinh GetNhaXeCauHinhByCode(int NhaXeId, ENNhaXeCauHinh cauhinh)
        {
            string ma = cauhinh.ToString();
            return GetNhaXeCauHinhByCode(NhaXeId, ma);
        }
        private NhaXeCauHinh GetNhaXeCauHinhByCode(int NhaXeId, string cauhinh)
        {
            var query = _nhaxecauhinhRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId && m.Ma == cauhinh);
            return query.FirstOrDefault();
        }
        #endregion
    }
}
