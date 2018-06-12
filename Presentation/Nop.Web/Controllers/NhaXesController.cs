using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Tax;
using Nop.Services.Authentication;
using Nop.Services.Authentication.External;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Extensions;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.UI.Captcha;
using Nop.Web.Models.Common;
using Nop.Web.Models.Customer;
using WebGrease.Css.Extensions;
using Nop.Web.Models.NhaXes;
using Nop.Core.Data;
using Nop.Services.NhaXes;
using Nop.Core.Caching;
using Nop.Core.Domain.News;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Services.Chonves;
using Nop.Services.Security;
using Nop.Core.Domain.Security;
using System.Globalization;
using Nop.Services.Catalog;
using Nop.Web.Models.VeXeKhach;
using Nop.Core.Domain.Chonves;
using Nop.Services.ChuyenPhatNhanh;
using Nop.Web.Models.ChuyenPhatNhanh;
using Nop.Core.Domain.ChuyenPhatNhanh;


namespace Nop.Web.Controllers
{
    public class NhaXesController : BaseNhaXeController
    {
        #region Khoi Tao
        private readonly IStateProvinceService _stateProvinceService;
        private readonly INhaXeService _nhaxeService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly IPhieuGuiHangService _phieuguihangService;
        private readonly IHangHoaService _hanghoaService;
        private readonly ICustomerService _customerService;
        private readonly IChonVeService _chonveService;
        private readonly IDiaChiService _diachiService;
        private readonly INhanVienService _nhanvienService;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly CustomerSettings _customerSettings;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreService _storeService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IXeInfoService _xeinfoService;
        private readonly IHanhTrinhService _hanhtrinhService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IBenXeService _benxeService;
        private readonly IVeXeService _vexeService;
        private readonly IPhoiVeService _phoiveService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IAuthenticationService _authenticationService;
        private readonly INhaXeCustomerService _nhaxecustomerService;
        private readonly IGiaoDichKeVeXeService _giaodichkeveService;
        private readonly IPhieuChuyenPhatService _phieuchuyenphatService;

        public NhaXesController(
            IStateProvinceService stateProvinceService,
            INhaXeService nhaxeService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IPictureService pictureService,
            IPhieuGuiHangService phieuguihangService,
            IHangHoaService hanghoaService,
            ICustomerService customerService,
            IChonVeService chonveService,
            IDiaChiService diachiService,
            INhanVienService nhanvienService,
            IPermissionService permissionService,
            IDateTimeHelper dateTimeHelper,
            CustomerSettings customerSettings,
            DateTimeSettings dateTimeSettings,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerActivityService customerActivityService,
            IGenericAttributeService genericAttributeService,
            IStoreService storeService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IXeInfoService xeinfoService,
            IHanhTrinhService hanhtrinhService,
            IPriceFormatter priceFormatter,
            IBenXeService benxeService,
            IVeXeService vexeService,
            IPhoiVeService phoiveService,
            IShoppingCartService shoppingCartService,
            IAuthenticationService authenticationService,
            INhaXeCustomerService nhaxecustomerService,
             IPhieuChuyenPhatService phieuchuyenphatService,
            IGiaoDichKeVeXeService giaodichkeveService

            )
        {
            this._phieuchuyenphatService = phieuchuyenphatService;
            this._giaodichkeveService = giaodichkeveService;
            this._stateProvinceService = stateProvinceService;
            this._nhaxeService = nhaxeService;
            this._hanghoaService = hanghoaService;
            this._phieuguihangService = phieuguihangService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._customerService = customerService;
            this._chonveService = chonveService;
            this._diachiService = diachiService;
            this._nhanvienService = nhanvienService;
            this._permissionService = permissionService;
            this._dateTimeHelper = dateTimeHelper;
            this._customerSettings = customerSettings;
            this._dateTimeSettings = dateTimeSettings;
            this._customerRegistrationService = customerRegistrationService;
            this._customerActivityService = customerActivityService;
            this._genericAttributeService = genericAttributeService;
            this._storeService = storeService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._xeinfoService = xeinfoService;
            this._hanhtrinhService = hanhtrinhService;
            this._priceFormatter = priceFormatter;
            this._benxeService = benxeService;
            this._vexeService = vexeService;
            this._phoiveService = phoiveService;
            this._shoppingCartService = shoppingCartService;
            this._authenticationService = authenticationService;
            this._nhaxecustomerService = nhaxecustomerService;


        }
        #endregion
        #region Common
        [NonAction]
        protected virtual string GetLabel(string _name)
        {
            return _localizationService.GetResource(string.Format("ChonVe.NhaXe.{0}", _name));
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetQuanHuyenByProvinceId(string ProvinceId,
            bool? addSelectStateItem, bool? addAsterisk)
        {
            //permission validation is not required here


            // This action method gets called via an ajax request
            if (String.IsNullOrEmpty(ProvinceId))
                throw new ArgumentNullException("ProvinceId");

            var quanhuyens = _diachiService.GetQuanHuyenByProvinceId(Convert.ToInt32(ProvinceId));
            var result = (from s in quanhuyens
                          select new { id = s.Id, name = s.Ten }).ToList();
            if (addAsterisk.HasValue && addAsterisk.GetValueOrDefault(true))
                result.Insert(0, new { id = 0, name = GetLabel("QuanHuyen.SelectQuanHuyen") });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        protected virtual string GetTenChang(PhoiVe phoive, ENPhanLoaiPhoiVe LoaiPhoiVe)
        {
            var _nguonve = phoive.nguonvexe;
            if (phoive.NguonVeXeConId > 0)
                _nguonve = phoive.nguonvexecon;
            var chang = _hanhtrinhService.GetHanhTrinhGiaVeId(phoive.ChangId.GetValueOrDefault(0));
            if(chang!=null)
            {
                var TenTinhDon = _hanhtrinhService.GetStateProvinceByNguon(_nguonve.DiemDon.NguonId).Abbreviation;
                var TenTinhDen = _hanhtrinhService.GetStateProvinceByNguon(_nguonve.DiemDen.NguonId).Abbreviation;
                if (LoaiPhoiVe == ENPhanLoaiPhoiVe.IN_PHOI_VE)
                {
                    TenTinhDon = chang.DiemDon.TenDiemDon;
                    TenTinhDen = chang.DiemDen.TenDiemDon;
                }
                return string.Format("{0}-{1}", TenTinhDon, TenTinhDen);
            }
            else
            {
                return "";
            }
            
        }
        [NonAction]
        protected virtual string GetKhachDonDuong(PhoiVe phoive)
        {

            if (phoive.TrangThai==ENTrangThaiPhoiVe.ChoXuLy)
                return "DD";
            else
                return "";
        }
        public ActionResult VitriBanDoPopUp()
        {
            var model = new DiaChiInfoModel();
            return View(model);
        }
        [NonAction]
        private void DiaChiInfoPrepare(DiaChiInfoModel model)
        {
            var states = _stateProvinceService.GetStateProvincesByCountryId(CountryID);
            if (states.Count > 0)
            {
                foreach (var s in states)
                {
                    model.AvailableStates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.ProvinceID) });
                }
                int ProvinceID = Convert.ToInt32(model.AvailableStates[0].Value);
                if (model.Id > 0 && model.ProvinceID > 0)
                {
                    ProvinceID = model.ProvinceID;
                }
                var quanhuyens = _diachiService.GetQuanHuyenByProvinceId(ProvinceID);
                model.AvailableQuanHuyens.Add(new SelectListItem { Text = GetLabel("QuanHuyen.SelectQuanHuyen"), Value = "0", Selected = (model.QuanHuyenID == 0) });
                foreach (var s in quanhuyens)
                {
                    model.AvailableQuanHuyens.Add(new SelectListItem { Text = s.Ten, Value = s.Id.ToString(), Selected = (s.Id == model.QuanHuyenID) });
                }
            }

        }

        ActionResult TrangThaiKhongHopLe()
        {
            return Json(GetLabel("QuanLyPhoiVe.TrangThaiKhongHopLe"), JsonRequestBehavior.AllowGet);
        }
        ActionResult KhongSoHuu()
        {
            return Json(GetLabel("QuanLyPhoiVe.KhongSoHuu"), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region phoi ve
        #region "Cac ham xu ly chung"
        List<QuanlyPhoiVeModel.NguonVeXeItem> GetNguonVeXeByHanhTrinhId(int HanhTrinhID)
        {
            var items = _hanhtrinhService.GetAllNguonVeXe(_workContext.NhaXeId, 0, HanhTrinhID).Where(c => c.HienThi && c.ParentId == 0);
            var nguonves = items
                .Select(c =>
                {

                    var item = new QuanlyPhoiVeModel.NguonVeXeItem();
                    item.Id = c.Id;
                    item.ThoiGianDen = c.ThoiGianDen;
                    item.ThoiGianDi = c.ThoiGianDi;
                    item.MoTa = string.Format("{0}-{1} - ({2}:{3})", c.ThoiGianDi.ToString("HH:mm"), c.ThoiGianDen.ToString("HH:mm"), c.LichTrinhInfo.MaLichTrinh, c.TenLoaiXe);
                    return item;
                })
                .ToList();
            return nguonves;
        }
        public ActionResult GetNguonVeXeForDropDownList(int HanhTrinhID)
        {
            return Json(GetNguonVeXeByHanhTrinhId(HanhTrinhID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetNguonVeXeCon(int HanhTrinhId,int LoaiXeId)
        {
            var nguonves = _hanhtrinhService.GetallHanhTrinhGiaVe(HanhTrinhId, _workContext.NhaXeId,0,0,LoaiXeId);
            var nguonvecons = nguonves.Select(c => new
            {
                Id = c.Id,
                ThongTin = c.ToMoTaHanhTrinhGiaVe()
            }).ToList();
            return Json(nguonvecons, JsonRequestBehavior.AllowGet);
        }
        List<SelectListItem> PrepareNguonVeXeList(List<QuanlyPhoiVeModel.NguonVeXeItem> lstnguonve, int NguonVeXeId)
        {
            var nguonvexes = lstnguonve.Select(c =>
            {
                var item = new SelectListItem();
                item.Value = c.Id.ToString();
                item.Text = c.MoTa;
                item.Selected = (c.Id == NguonVeXeId);
                return item;
            }).ToList();
            return nguonvexes;
        }
        [NonAction]
        void PhoiVeToModel(PhoiVe e, PhoiVeModel m)
        {
            m.Id = e.Id;
            m.NguonVeXeId = e.NguonVeXeId;
            //neu co nguon ve xe con, tuc la khach hang dang chon tuyen con de dat ve
            //chuyen doi thong tin gia ve gia ve cua tuyen con
            m.GiaVe = e.GiaVeHienTai;
            if (e.NguonVeXeConId > 0)
            {
                var nguonvecon = _vexeService.GetNguonVeXeById(e.NguonVeXeConId);
                m.GiaVe = nguonvecon.GiaVeHienTai;
            }
            m.GiaVeText = _priceFormatter.FormatPrice(m.GiaVe, true, false);
            m.NgayDi = e.NgayDi;
            var nguonve = _vexeService.GetNguonVeXeById(e.NguonVeXeId);
            m.TenHanhTrinh = string.Format(" {0}- {1}", nguonve.TenDiemDon, nguonve.TenDiemDen);
            m.TenLichTrinh = string.Format("{0} - {1}", nguonve.ThoiGianDi.ToString("HH:mm"), nguonve.ThoiGianDen.ToString("HH:mm"));
            m.TrangThaiId = e.TrangThaiId;
            m.CustomerId = e.CustomerId;
            m.SoDoGheXeQuyTacId = e.SoDoGheXeQuyTacId;
            m.KyHieuGhe = e.sodoghexequytac.Val;
            m.Tang = e.sodoghexequytac.Tang;
            m.isChonVe = e.isChonVe;
            m.NgayTao = e.NgayTao;
            m.NgayUpd = e.NgayUpd;
            m.SessionId = e.SessionId;

        }
        #endregion

        #region "Thong tin ghe"
        /// <summary>
        /// Lay thong tin so do ghe xe
        /// </summary>
        /// <param name="NguonVeXeId"></param>
        /// <param name="NgayDi"></param>
        /// <param name="PhanLoai">0: phoi ve, 1:dung cho chuyen ve, 2: in phoive</param>
        /// <param name="TangIndex"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _GetInfoSoDoGheXe(int NguonVeXeId, string NgayDi, int? PhanLoai, int? TangIndex)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            //lấy thong tin nguồn xe            
            var nguonvexe = _hanhtrinhService.GetNguonVeXeById(NguonVeXeId);
            if (nguonvexe == null)
                return AccessDeniedView();

            var loaixe = _xeinfoService.GetById(nguonvexe.LoaiXeId);
            if (loaixe == null)
                return AccessDeniedView();

            //var nhaxe = this._workContext.CurrentNhaXe;
            var sodoghe = _xeinfoService.GetSoDoGheXeById(loaixe.SoDoGheXeID);
            var modelsodoghe = new LoaiXeModel.SoDoGheXeModel();
            if (_workContext.CurrentVanPhong.IsYeuCauDuyetHuy)
            {
                modelsodoghe.CanYeuCauHuy = true;
            }

            modelsodoghe.PhanLoai = (ENPhanLoaiPhoiVe)PhanLoai.GetValueOrDefault(0);
            SoDoGheXeToSoDoGheXeModel(sodoghe, modelsodoghe);
            // lay cac diem don , tao ma tran thong ke ket qua
            var lichtrinh = _hanhtrinhService.GetLichTrinhById(nguonvexe.LichTrinhId);
            var diemdons = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(lichtrinh.HanhTrinhId).Where(c => c.KhoangCach > 0).OrderBy(c => c.ThuTu).ToList();//.Select(c => c.Id).ToArray();
            var diemdonids = diemdons.Select(c => c.DiemDonId).ToArray();
            modelsodoghe.SoDiemDon = diemdons.Count();
            modelsodoghe.TongKet = new LoaiXeModel.TongKetPhoiToArrayModel[modelsodoghe.SoDiemDon+1, modelsodoghe.SoCot + 2];
            for (int m = 0; m < modelsodoghe.SoDiemDon+1; m++)
            {
                for (int n = 0; n < modelsodoghe.SoCot + 2; n++)
                {
                    modelsodoghe.TongKet[m, n] = new LoaiXeModel.TongKetPhoiToArrayModel();  
                    if(m<modelsodoghe.SoDiemDon && n<modelsodoghe.SoCot+1)
                    {
                        modelsodoghe.TongKet[m, n].DiemDonId = diemdons[m].DiemDonId;
                        modelsodoghe.TongKet[m, n].TenDiemDon = diemdons[m].diemdon.TenDiemDon;
                        modelsodoghe.TongKet[m, n].SoKhachXuong = 0;
                    }
                    
                   

                }
            }
            //Lấy thông tin ma tran
            var sodoghevitris = _xeinfoService.GetAllSoDoGheViTri(sodoghe.Id);
            var sodoghequytacs = _xeinfoService.GetAllSoDoGheXeQuyTac(loaixe.Id);

            modelsodoghe.MaTran = new int[modelsodoghe.SoHang, modelsodoghe.SoCot];
            modelsodoghe.PhoiVes1 = new LoaiXeModel.PhoiVeAdvanceModel[modelsodoghe.SoHang + 1, modelsodoghe.SoCot + 1];
            modelsodoghe.SoTang = 1;
            if (sodoghe.KieuXe == ENKieuXe.GiuongNam)
            {
                modelsodoghe.SoTang = 2;
                modelsodoghe.PhoiVes2 = new LoaiXeModel.PhoiVeAdvanceModel[modelsodoghe.SoHang + 1, modelsodoghe.SoCot + 1];
            }
            foreach (var s in sodoghevitris)
            {
                modelsodoghe.MaTran[s.y, s.x] = 1;
            }

            DateTime _ngaydi = Convert.ToDateTime(NgayDi);
            if (sodoghequytacs != null && sodoghequytacs.Count > 0)
            {
                foreach (var s in sodoghequytacs)
                {
                    if (s.Tang == 1)
                    {
                        modelsodoghe.PhoiVes1[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsodoghe.PhoiVes1[s.y, s.x].KyHieu = s.Val;
                        if (s.y >= 1 && s.x >= 1)
                        {

                            modelsodoghe.PhoiVes1[s.y, s.x].Info = _phoiveService.GetPhoiVe(NguonVeXeId, s, _ngaydi, true);
                            if (modelsodoghe.PhoiVes1[s.y, s.x].Info.customer != null)
                            {
                                var ViTriGhe = modelsodoghe.PhoiVes1[s.y, s.x];
                                ViTriGhe.TenChang = GetTenChang(ViTriGhe.Info, modelsodoghe.PhanLoai);
                                ViTriGhe.LoaiKhach = GetKhachDonDuong(ViTriGhe.Info);

                                ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToTien(_priceFormatter);
                                int idkhachhang = ViTriGhe.Info.customer.Id;
                                if (idkhachhang == CommonHelper.KhachVangLaiId)
                                {
                                    var _khachhang = _customerService.GetCustomerById(idkhachhang);
                                    ViTriGhe.TenKhachHang = _khachhang.GetFullName();
                                    ViTriGhe.SoDienThoai = null;

                                }

                                else
                                {
                                    var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(idkhachhang);
                                    if (khachhang != null)
                                    {
                                        ViTriGhe.TenKhachHang = khachhang.HoTen;
                                        ViTriGhe.SoDienThoai = khachhang.DienThoai;

                                    }
                                }
                                if (ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang || ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaThanhToan)
                                {

                                    var _nguonve = ViTriGhe.Info.nguonvexe;
                                    if (ViTriGhe.Info.NguonVeXeConId > 0)
                                        _nguonve = ViTriGhe.Info.nguonvexecon;

                                    int indexdiemdon = Array.IndexOf(diemdonids, ViTriGhe.Info.changgiave.DiemDenId);                              
                                   
                                    modelsodoghe.TongKet[indexdiemdon, s.x].SoKhachXuong ++;
                                    modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue = ViTriGhe.Info.GiaVeHienTai;
                                    modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                    modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue.ToTien(_priceFormatter);
                                    modelsodoghe.TongKet[indexdiemdon, modelsodoghe.SoCot + 1].TongKhach++;
                                    //tong tien toan bo
                                    modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                    modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue.ToTien(_priceFormatter);
                                    modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongKhach++;
                                }


                            }
                        }

                    }
                    else
                    {
                        modelsodoghe.PhoiVes2[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsodoghe.PhoiVes2[s.y, s.x].KyHieu = s.Val;
                        if (s.y >= 1 && s.x >= 1)
                        {
                            modelsodoghe.PhoiVes2[s.y, s.x].Info = _phoiveService.GetPhoiVe(NguonVeXeId, s, _ngaydi, true);
                            if (modelsodoghe.PhoiVes2[s.y, s.x].Info.customer != null)
                            {
                                var ViTriGhe = modelsodoghe.PhoiVes2[s.y, s.x];
                                ViTriGhe.TenChang = GetTenChang(ViTriGhe.Info, modelsodoghe.PhanLoai);
                                ViTriGhe.LoaiKhach = GetKhachDonDuong(ViTriGhe.Info);
                                ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToTien(_priceFormatter);
                                int idkhachhang = ViTriGhe.Info.customer.Id;
                                if (idkhachhang == CommonHelper.KhachVangLaiId)
                                {
                                    var _khachhang = _customerService.GetCustomerById(idkhachhang);
                                    ViTriGhe.TenKhachHang = _khachhang.GetFullName();
                                    ViTriGhe.SoDienThoai = null;

                                }

                                else
                                {
                                    var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(idkhachhang);
                                    if (khachhang != null)
                                    {
                                        ViTriGhe.TenKhachHang = khachhang.HoTen;
                                        ViTriGhe.SoDienThoai = khachhang.DienThoai;

                                    }
                                }
                                if (ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang || ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaThanhToan)
                                {



                                    int indexdiemdon = Array.IndexOf(diemdonids, ViTriGhe.Info.ChangId.Value);

                                    modelsodoghe.TongKet[indexdiemdon, s.x].SoKhachXuong++;
                                    modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue = ViTriGhe.Info.GiaVeHienTai;
                                    modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                    modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue.ToTien(_priceFormatter);
                                    modelsodoghe.TongKet[indexdiemdon, modelsodoghe.SoCot + 1].TongKhach++;
                                    //tong tien toan bo
                                    modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                    modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue.ToTien(_priceFormatter);
                                    modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongKhach++;
                                }

                            }

                        }
                    }
                }
            }
            //selected tab
            SaveSelectedTabIndex(TangIndex);
            return PartialView(modelsodoghe);
        }
        // so do ghe doc

        #endregion
        #region lich su phoi ve
        public ActionResult LichSuPhoiVe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new QuanlyPhoiVeModel();
            model.NgayGiaoDichVe = DateTime.Now;
            model.ListHanhTrinh = PrepareHanhTrinhList(false);
            if (model.ListHanhTrinh.Count > 0)
            {
                model.HanhTrinhId = Convert.ToInt32(model.ListHanhTrinh[0].Value);
                //tim thoi gian gan nhat voi lich trinh xe dang co
                var nguonvexes = GetNguonVeXeByHanhTrinhId(model.HanhTrinhId);
                model.NguonVeXeId = 0;
                if (nguonvexes.Count > 0)
                {
                    foreach (var nv in nguonvexes)
                    {
                        if (nv.ThoiGianDi.Hour > DateTime.Now.Hour)
                        {
                            model.NguonVeXeId = nv.Id;
                            break;
                        }
                    }
                }
                if (model.NguonVeXeId == 0)
                    model.NgayGiaoDichVe = DateTime.Now;
                if (nguonvexes.Count > 0)
                    model.NguonVeXeId = nguonvexes[0].Id;
                model.ListNguonVeXe = PrepareNguonVeXeList(nguonvexes, model.NguonVeXeId);
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult LichSuPhoiVe(DataSourceRequest command, DateTime NgayGiaoDich, int NguonVeXeId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var items = _phoiveService.GetLichSuPhoiVe(NgayGiaoDich, NguonVeXeId,
                command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var m = new QuanlyPhoiVeModel.PhoiVeNoteMoel();
                    m.PhoiVeId = x.PhoiVeId;
                    m.NgayTao = x.NgayTao;
                    m.Note = x.Note;
                    m.Id = x.Id;
                    return m;
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }
        #endregion
        #region "Quan ly phoi ve"
        public ActionResult QuanLyPhoiVe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            return RedirectToRoute("QLPhoiVe");

            //var model = new QuanlyPhoiVeModel();
            //model.NgayDi = DateTime.Now;
            //model.ListHanhTrinh = PrepareHanhTrinhList(false);
            //if (model.ListHanhTrinh.Count > 0)
            //{
            //    model.HanhTrinhId = Convert.ToInt32(model.ListHanhTrinh[0].Value);
            //    //tim thoi gian gan nhat voi lich trinh xe dang co
            //    var nguonvexes = GetNguonVeXeByHanhTrinhId(model.HanhTrinhId);
            //    model.NguonVeXeId = 0;
            //    if (nguonvexes.Count > 0)
            //    {
            //        foreach (var nv in nguonvexes)
            //        {
            //            if (nv.ThoiGianDi.Hour > DateTime.Now.Hour)
            //            {
            //                model.NguonVeXeId = nv.Id;
            //                break;
            //            }
            //        }
            //    }
            //    if (model.NguonVeXeId == 0)
            //        model.NgayDi = DateTime.Now.AddDays(1);
            //    if (nguonvexes.Count > 0)
            //        model.NguonVeXeId = nguonvexes[0].Id;
            //    model.ListNguonVeXe = PrepareNguonVeXeList(nguonvexes, model.NguonVeXeId);
            //}

            //return View(model);
        }

        #endregion
     
        #region "Dat mua ve"
        private string GetChonGeSession(bool isCreate = true)
        {
            if (Session["ChonGheGroup"] == null && isCreate)
            {
                Session["ChonGheGroup"] = Guid.NewGuid().ToString();
            }
            if (Session["ChonGheGroup"] == null)
            {
                return "";
            }
            return Session["ChonGheGroup"].ToString();
        }
        private void ClearChonGeSession()
        {
            Session["ChonGheGroup"] = null;
        }
        [HttpPost]
        public ActionResult ChonGheDatCho(int NguonVeXeID, string NgayDi, string KiHieuGhe, int tang)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            if (string.IsNullOrEmpty(KiHieuGhe) || tang == 0)
                return Loi();

            var item = new PhoiVe();
            item.NguonVeXeId = NguonVeXeID;           
            item.NgayDi = Convert.ToDateTime(NgayDi);
            var nguonvexe = _vexeService.GetNguonVeXeById(item.NguonVeXeId);
            item.SoDoGheXeQuyTacId = _vexeService.GetSoDoGheXeQuyTacID(nguonvexe.LoaiXeId, KiHieuGhe, tang);
            if (item.SoDoGheXeQuyTacId > 0)
            {
                item.TrangThai = ENTrangThaiPhoiVe.DatCho;
                item.isChonVe = false;//giao dich cua nha xe
                item.NguoiDatVeId = _workContext.CurrentNhanVien.Id;
                item.CustomerId = _workContext.CurrentCustomer.Id;
                item.SessionId = GetChonGeSession();
                item.GiaVeHienTai = nguonvexe.GiaVeHienTai;
                if (_phoiveService.DatVe(item))
                {
                    return ThanhCong();
                }
            }
            return Loi();
        }
        [HttpPost]
        public ActionResult HuyGheDatCho(int PhoiVeId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            //khong o trang thai dat cho hoac khac session hoac khac nguoi dang dat
            if (phoive.TrangThai != ENTrangThaiPhoiVe.DatCho)
                return Loi();
            if (phoive.SessionId != GetChonGeSession(false) || phoive.NguoiDatVeId != _workContext.CurrentNhanVien.Id)
                return KhongSoHuu();
            _phoiveService.DeletePhoiVe(phoive);
            return ThanhCong();
        }
        public ActionResult KhachHangDatMuaVe(int NguonVeXeID, string NgayDi)
        {
            var model = new QuanlyPhoiVeModel.KhachHangDatMuaVeModel();
            model.NguonVeXeIdDangChon = NguonVeXeID;          
            model.NgayDiDangChon = NgayDi;
            var nguonve=_hanhtrinhService.GetNguonVeXeById(NguonVeXeID);
          
            var nguonves = _hanhtrinhService.GetallHanhTrinhGiaVe(nguonve.LichTrinhInfo.HanhTrinhId,_workContext.NhaXeId
                );
            model._changs = nguonves.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.ToMoTaHanhTrinhGiaVe(),               
            }).ToList();
            var vanphongs=_nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
            model.quaybanves = vanphongs.Select(c => new SelectListItem
            {
                Value=c.Id.ToString(),
                Text=string.Format("",c.TenVanPhong,c.Ma)
            }).ToList();
            return PartialView(model);

        }
        public ActionResult CbbListKhachHangInNhaXe(string SearchKhachhang)
        {
            var khachhangs = _phoiveService.GetKhachHangInNhaXe(SearchKhachhang, _workContext.NhaXeId).Select(m =>
            {
                var item = new CustomerNhaXeModel();
                item.Id = m.Id;
                item.HoTen = m.HoTen;
                item.DienThoai = m.DienThoai;
                item.SearchInfo = m.SearchInfo;
                item.DiaChiLienHe = m.DiaChiLienHe;
                return item;
            }).ToList();
            if (khachhangs.Count == 0)
            {
                var item = new CustomerNhaXeModel();
                item.Id = -1;
                item.SearchInfo = SearchKhachhang;
                item.HoTen = SearchKhachhang;
                item.DienThoai = "";
                item.DiaChiLienHe = "";
                khachhangs.Add(item);
            }

            return Json(khachhangs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ThanhToanGiuCho(int NguonVeId, int ChangId, string NgayDiDangChon, string TenKhachHang, string SoDienThoai, bool DaThanhToan, int Id, string GhiChu, bool IsForKid, bool isKhachVangLai)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            
            if (string.IsNullOrEmpty(NgayDiDangChon))
                return Loi();
            int CustomerId = 0;
            if (!isKhachVangLai)
            {
                if (string.IsNullOrEmpty(TenKhachHang))
                    return Loi();
                if (string.IsNullOrEmpty(SoDienThoai))
                    return Loi();
                if (Id > 0)
                {
                    //cap nhat thong tin khach hang
                    var khachhang = _nhaxecustomerService.GetNhaXeCustomerById(Id);
                    khachhang.HoTen = TenKhachHang;
                    khachhang.DienThoai = SoDienThoai;
                    _nhaxecustomerService.UpdateNhaXeCustomer(khachhang);
                    CustomerId = khachhang.CustomerId;
                }
                else
                {
                    //them moi thong tin khach hang                
                    var khachhang = _nhaxecustomerService.CreateNew(_workContext.NhaXeId, TenKhachHang, SoDienThoai, "");
                    //dathanhtoan->daGiaoHang. chưa thanh toan->dang xu ly            
                    CustomerId = khachhang.CustomerId;
                }
            }
            else
                CustomerId = CommonHelper.KhachVangLaiId;

            _phoiveService.NhaXeThanhToanGiuChoPhoiVe(_workContext.NhaXeId, NguonVeId, ChangId, GetChonGeSession(false), _workContext.CurrentCustomer.Id, DaThanhToan, CustomerId, GhiChu, IsForKid);
            ClearChonGeSession();
            return ThanhCong();
        }
        public ActionResult _DatVeNhanhChonNguonVe(int NguonVeXeID, string NgayDi)
        {
            var model = new QuanlyPhoiVeModel.KhachHangDatMuaVeModel();
            model.NguonVeXeIdDangChon = NguonVeXeID;
            model.NgayDiDangChon = NgayDi;

            var nguonves = _hanhtrinhService.GetAllNguonVeXeByVeGoc(_workContext.NhaXeId, NguonVeXeID);
            model._nguonves = nguonves.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.ToMoTa(),
                Selected = NguonVeXeID == c.Id
            }).ToList();
            return PartialView(model);


        }
        [HttpPost]
        public ActionResult DatVeNhanh(int NguonVeId,int changid, string NgayDi)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            _phoiveService.NhaXeThanhToanNhanh(NguonVeId,changid, GetChonGeSession(false), _workContext.CurrentNhanVien.Id);
            ClearChonGeSession();
            return ThanhCong();

        }

        #endregion
        #region "Huy hoac chuyen ghe"
        public ActionResult KhachHangChuyenVe(int PhoiVeId, int HanhTrinhId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            //kiem tra tinh hop le cua phoi ve
            if (phoive.isChonVe)
                return AccessDeniedView();
            if (phoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy || phoive.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
            {
                var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(phoive.CustomerId);
                var model = new QuanlyPhoiVeModel.KhachHangDatMuaVeModel();
                model.PhoiVeId_ChuyenVe = PhoiVeId;
                model.NguonVeXeId_ChuyenVe = phoive.NguonVeXeId;
                model.TenKhachHang = khachhang.HoTen;
                model.SoDienThoai = khachhang.DienThoai;
                model.NgayDi_ChuyenVe = phoive.NgayDi;
                model.DaThanhToan = false;
                if (phoive.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
                    model.DaThanhToan = true;
                //tim thoi gian gan nhat voi lich trinh xe dang co

                var nguonvexes = GetNguonVeXeByHanhTrinhId(HanhTrinhId);
                model.ListNguonVeXe_ChuyenVe = PrepareNguonVeXeList(nguonvexes, model.NguonVeXeId_ChuyenVe);

                return PartialView(model);
            }
            return AccessDeniedView();
        }

        [HttpPost]
        public ActionResult ChonChuyenVe(int PhoiVeId, int NguonVeXeID, string NgayDi, string KiHieuGhe, int Tang, bool DaThanhToan)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            //lay thong tin phoi ve
            var _phoive = _phoiveService.GetPhoiVeById(PhoiVeId);

            //dat ve moi
            var item = new PhoiVe();
            item.NguonVeXeId = NguonVeXeID;
            item.NgayDi = Convert.ToDateTime(NgayDi);
            var nguonvexe = _vexeService.GetNguonVeXeById(item.NguonVeXeId);
            item.SoDoGheXeQuyTacId = _vexeService.GetSoDoGheXeQuyTacID(nguonvexe.LoaiXeId, KiHieuGhe, Tang);
            if (item.SoDoGheXeQuyTacId > 0)
            {
                ENTrangThaiPhoiVe trangthai = ENTrangThaiPhoiVe.ChoXuLy;
                if (DaThanhToan)
                    trangthai = ENTrangThaiPhoiVe.DaGiaoHang;
                item.isChonVe = false;//giao dich cua nha xe
                item.NguoiDatVeId = _workContext.CurrentNhanVien.Id;
                item.CustomerId = _phoive.CustomerId;
                item.SessionId = GetChonGeSession();
                item.GiaVeHienTai = _phoive.GiaVeHienTai;
                item.ChangId = _phoive.ChangId;
                item.MaVe = _phoive.MaVe;
                item.GhiChu = _phoive.GhiChu;

                if (_phoiveService.DatVe(item, trangthai))
                {
                    //huy ve cu
                    _phoiveService.HuyPhoiVe(_phoive);
                    ClearChonGeSession();
                    return ThanhCong();
                }
            }


            return Loi();

        }

        [HttpPost]
        public ActionResult HuyVe(int PhoiVeId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            //khong o trang thai giu cho hoac khong phai cua nha xe dat cho
            if (phoive.isChonVe)
                return KhongSoHuu();
            //neu o trang thai dat cho
            if (phoive.TrangThai == ENTrangThaiPhoiVe.DatCho && phoive.SessionId == GetChonGeSession(false) && phoive.NguoiDatVeId == _workContext.CurrentNhanVien.Id)
            {
                _phoiveService.DeletePhoiVe(phoive);
                return ThanhCong();
            }

            if (phoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy || phoive.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
            {
                _phoiveService.HuyPhoiVe(phoive);
                return ThanhCong();
            }
            return TrangThaiKhongHopLe();
        }
        [HttpPost]
        public ActionResult ThanhToanGiaoVe(int PhoiVeId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            //khong o trang thai giu cho hoac khong phai cua nha xe dat cho
            if (phoive.isChonVe)
                return KhongSoHuu();

            if (phoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
            {
                _phoiveService.ThanhToanGiaoVe(phoive);
                return ThanhCong();
            }
            return TrangThaiKhongHopLe();
        }
        #endregion
        #endregion
        #region Nha Xe

        bool checkLoginNhaXeInfo(int customerId)
        {
            NhaXe currentNhaXe = _nhaxeService.GetNhaXeByCustommerId(customerId);
            if (currentNhaXe != null)
                Session["NhaXeID"] = currentNhaXe.Id;
            else
            {
                Session["NhaXeID"] = 0;
                return false;
            }

            return true;
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
        public ActionResult PageNotFound()
        {
            return View();
        }
        public ActionResult Index()
        {
            Customer _cuscur = _authenticationService.GetAuthenticatedCustomer();
            if (_cuscur != null)
            {
                if (checkLoginNhaXeInfo(_cuscur.Id))
                    return RedirectToAction("QuanLyPhoiVe");
                _authenticationService.SignOut();
            }
            var model = new LoginModel();
            model.UsernamesEnabled = _customerSettings.UsernamesEnabled;
            model.CheckoutAsGuest = false;
            model.DisplayCaptcha = false;
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (_customerSettings.UsernamesEnabled && model.Username != null)
                {
                    model.Username = model.Username.Trim();
                }
                var loginResult = _customerRegistrationService.ValidateCustomer(_customerSettings.UsernamesEnabled ? model.Username : model.Email, model.Password);
                switch (loginResult)
                {
                    case CustomerLoginResults.Successful:
                        {
                            var customer = _customerSettings.UsernamesEnabled ? _customerService.GetCustomerByUsername(model.Username) : _customerService.GetCustomerByEmail(model.Email);


                            if (checkLoginNhaXeInfo(customer.Id))
                            {
                                //migrate shopping cart
                                _shoppingCartService.MigrateShoppingCart(_workContext.CurrentCustomer, customer, true);

                                //sign in new customer
                                _authenticationService.SignIn(customer, model.RememberMe);
                                //activity log
                                _customerActivityService.InsertActivity("PublicStore.Login", _localizationService.GetResource("ActivityLog.PublicStore.Login"), customer);

                                if (String.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                    return RedirectToAction("QuanLyPhoiVe");
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                //khong phai tai khoan nha xe
                                ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials"));
                            }
                            break;


                        }
                    case CustomerLoginResults.CustomerNotExist:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.CustomerNotExist"));
                        break;
                    case CustomerLoginResults.Deleted:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.Deleted"));
                        break;
                    case CustomerLoginResults.NotActive:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.NotActive"));
                        break;
                    case CustomerLoginResults.NotRegistered:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.NotRegistered"));
                        break;
                    case CustomerLoginResults.WrongPassword:
                    default:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials"));
                        break;
                }
            }

            //If we got this far, something failed, redisplay form
            model.UsernamesEnabled = _customerSettings.UsernamesEnabled;
            model.DisplayCaptcha = false;
            return View(model);
        }

        public ActionResult Info()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var nhaxe = _workContext.CurrentNhaXe;
            if (nhaxe == null || nhaxe.isDelete)
                return AccessDeniedView();
            var model = new NhaXeInfoModel();
            //default values           
            NhaXeInfoModelPrepare(model, nhaxe);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Info(NhaXeInfoModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var nhaxe = _workContext.CurrentNhaXe;
            if (nhaxe == null || nhaxe.isDelete)
                //No manufacturer found with the specified id
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                int prevLogoID = nhaxe.LogoID;
                int prevAnhDaiDienID = nhaxe.AnhDaiDienID;
                var diachi = _diachiService.GetById(nhaxe.DiaChiID);
                //mat thong tin dia chi
                if (diachi != null)
                {
                    diachi = model.ThongTinDiaChi.ToEntity(diachi);
                    diachi.Id = nhaxe.DiaChiID;
                    _diachiService.Update(diachi);
                }
                else
                {
                    diachi = model.ThongTinDiaChi.ToEntity(diachi);
                    _diachiService.Insert(diachi);
                    nhaxe.DiaChiID = diachi.Id;
                }

                nhaxe.GioiThieu = model.GioiThieu;
                nhaxe.DieuKhoanGuiHang = model.DieuKhoanGuiHang;
                nhaxe.LogoID = model.LogoID;
                nhaxe.AnhDaiDienID = model.AnhDaiDienID;
                nhaxe.Email = model.Email;
                nhaxe.DienThoai = model.DienThoai;
                nhaxe.Fax = model.Fax;
                nhaxe.HotLine = model.HotLine;
                _nhaxeService.UpdateNhaXe(nhaxe);

                //delete an old picture (if deleted or updated)
                if (prevLogoID > 0 && prevLogoID != nhaxe.LogoID)
                {
                    var prevPicture = _pictureService.GetPictureById(prevLogoID);
                    if (prevPicture != null)
                        _pictureService.DeletePicture(prevPicture);
                }
                if (prevAnhDaiDienID > 0 && prevAnhDaiDienID != nhaxe.AnhDaiDienID)
                {
                    var prevPicture = _pictureService.GetPictureById(prevAnhDaiDienID);
                    if (prevPicture != null)
                        _pictureService.DeletePicture(prevPicture);
                }
                SuccessNotification(GetLabel("Update.Success"));

                //selected tab
                SaveSelectedTabIndex();

                return RedirectToAction("Info");
            }


            return View(model);
        }
        [NonAction]
        protected virtual void NhaXeInfoModelPrepare(NhaXeInfoModel model, NhaXe nhaxe)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (nhaxe == null)
                throw new ArgumentNullException("nhaxe");

            model.Id = nhaxe.Id;
            model.TenNhaXe = nhaxe.TenNhaXe;
            model.MaNhaXe = nhaxe.MaNhaXe;
            model.GioiThieu = nhaxe.GioiThieu;
            model.LogoID = nhaxe.LogoID;
            model.AnhDaiDienID = nhaxe.AnhDaiDienID;
            model.Email = nhaxe.Email;
            model.DienThoai = nhaxe.DienThoai;
            model.Fax = nhaxe.Fax;
            model.HotLine = nhaxe.HotLine;
            model.DieuKhoanGuiHang = nhaxe.DieuKhoanGuiHang;
            model.DiaChiID = nhaxe.DiaChiID;
            model.ThongTinDiaChi = _diachiService.GetById(model.DiaChiID).ToModel();
            DiaChiInfoPrepare(model.ThongTinDiaChi);
        }
        //NhaXePictureAdd

        #endregion
        #region Nhaxe pictures

        public ActionResult NhaXePictureAdd(int pictureId, int displayOrder)
        {

            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            if (pictureId == 0)
                throw new ArgumentException();


            //a vendor should have access only to his products
            NhaXe nhaxe = _workContext.CurrentNhaXe;

            _nhaxeService.InsertNhaXePicture(new NhaXePicture
            {
                Picture_Id = pictureId,
                NhaXe_Id = nhaxe.Id,
                DisplayOrder = displayOrder,
            });

            _pictureService.SetSeoFilename(pictureId, _pictureService.GetPictureSeName(nhaxe.TenNhaXe));

            return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult NhaXePictureList(DataSourceRequest command)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var nhaxePictures = _nhaxeService.GetNhaXePicturesByNhaXeId(_workContext.NhaXeId);
            var nhaxePicturesModel = nhaxePictures
                .Select(x => new NhaXeInfoModel.NhaXePictureModel
                {
                    Id = x.Id,
                    NhaXe_Id = x.NhaXe_Id,
                    Picture_Id = x.Picture_Id,
                    PictureUrl = _pictureService.GetPictureUrl(x.Picture_Id),
                    DisplayOrder = x.DisplayOrder
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = nhaxePicturesModel,
                Total = nhaxePicturesModel.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult NhaXePictureUpdate(NhaXeInfoModel.NhaXePictureModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var nhaxePicture = _nhaxeService.GetNhaXePictureById(model.Id);
            if (nhaxePicture == null)
                throw new ArgumentException("No nhaxe picture found with the specified id");
            nhaxePicture.DisplayOrder = model.DisplayOrder;
            _nhaxeService.UpdateNhaXePicture(nhaxePicture);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult NhaXePictureDelete(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var nhaxePicture = _nhaxeService.GetNhaXePictureById(id);
            if (nhaxePicture == null)
                throw new ArgumentException("No nhaxe picture found with the specified id");

            var pictureId = nhaxePicture.Picture_Id;
            _nhaxeService.DeleteNhaXePicture(nhaxePicture);
            var picture = _pictureService.GetPictureById(pictureId);
            _pictureService.DeletePicture(picture);
            return new NullJsonResult();
        }

        #endregion
        #region Van Phong

        public ActionResult VanPhongList()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var model = new VanPhongListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult VanPhongList(DataSourceRequest command, VanPhongListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var vanphongs = _nhaxeService.GetAllVanPhong(_workContext.NhaXeId, model.TimTenVanPhong,
                command.Page - 1, command.PageSize, false);
            var gridModel = new DataSourceResult
            {

                Data = vanphongs.Select(x => new VanPhongModel
                {
                    Id = x.Id,
                    TenVanPhong = x.TenVanPhong,
                    Ma = x.Ma,
                    KieuVanPhongID = x.KieuVanPhongID,
                    KieuVanPhongText = x.KieuVanPhong.ToCVEnumText(_localizationService),
                    DienThoaiDatVe = x.DienThoaiDatVe,
                    DienThoaiGuiHang = x.DienThoaiGuiHang,
                    DiaChiID = x.DiaChiID,
                    DiaChiText = x.diachiinfo.ToText()
                }),
                Total = vanphongs.TotalCount
            };

            return Json(gridModel);
        }
        /// <summary>
        /// Tim van phong theo ten nhan vien
        /// </summary>
        /// <param name="command"></param>
        /// <param name="model"></param>
        /// <param name="TenNhanVien"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VanPhongListNhanVien(string TenNhanVien)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var vanphongsroot = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
            var vanphongs = new List<VanPhong>();
            if (!string.IsNullOrEmpty(TenNhanVien))
            {
                var vanphongids = _nhanvienService.GetAll(_workContext.NhaXeId, TenNhanVien).Select(c => c.VanPhongID).Distinct().ToList();
                vanphongs = vanphongsroot.Where(c => vanphongids.Contains(c.Id)).ToList();
            }
            else
                vanphongs = vanphongsroot;


            var gridModel = new DataSourceResult
            {

                Data = vanphongs.Select(x => new VanPhongModel
                {
                    Id = x.Id,
                    TenVanPhong = x.TenVanPhong,
                    Ma = x.Ma,
                    KieuVanPhongID = x.KieuVanPhongID,
                    KieuVanPhongText = x.KieuVanPhong.ToCVEnumText(_localizationService),
                    DienThoaiDatVe = x.DienThoaiDatVe,
                    DienThoaiGuiHang = x.DienThoaiGuiHang,
                    DiaChiID = x.DiaChiID,
                    DiaChiText = x.diachiinfo.ToText()
                }),
                Total = vanphongs.Count
            };

            return Json(gridModel);
        }
        public ActionResult VanPhongTao()
        {
            var model = new VanPhongModel();
            VanPhongModelPrepare(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult VanPhongTao(VanPhongModel model, bool continueEditing, FormCollection form)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var diachi = model.ThongTinDiaChi.ToEntity(null);
                diachi.Latitude = form.GetValue("ThongTinDiaChi.Latitude").AttemptedValue.ToDecimal();
                diachi.Longitude = form.GetValue("ThongTinDiaChi.Longitude").AttemptedValue.ToDecimal();
                _diachiService.Insert(diachi);
                var vanphong = new VanPhong();
                vanphong.NhaXeId = _workContext.NhaXeId;
                vanphong.TenVanPhong = model.TenVanPhong;
                vanphong.Ma = model.Ma;
                vanphong.KieuVanPhongID = model.KieuVanPhongID;
                vanphong.DienThoaiDatVe = model.DienThoaiDatVe;
                vanphong.DienThoaiGuiHang = model.DienThoaiGuiHang;
                vanphong.DiaChiID = diachi.Id;
                vanphong.IsYeuCauDuyetHuy = model.IsYeuCauDuyetHuy;
                if (model.KhuVucId > 0)
                    vanphong.KhuVucId = model.KhuVucId;
                else
                    vanphong.KhuVucId = null;
                _nhaxeService.InsertVanPhong(vanphong);

                return continueEditing ? RedirectToAction("VanPhongSua", new { id = vanphong.Id }) : RedirectToAction("VanPhongList");

            }

            return View(model);
        }
        public ActionResult VanPhongSua(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var vanphong = _nhaxeService.GetVanPhongById(id);
            if (vanphong == null || vanphong.isDelete)
                //No manufacturer found with the specified id
                return RedirectToAction("VanPhongList");
            var model = new VanPhongModel();
            model.Id = vanphong.Id;
            model.TenVanPhong = vanphong.TenVanPhong;
            model.Ma = vanphong.Ma;
            model.KieuVanPhongID = vanphong.KieuVanPhongID;
            model.DienThoaiDatVe = vanphong.DienThoaiDatVe;
            model.DienThoaiGuiHang = vanphong.DienThoaiGuiHang;
            model.DiaChiID = vanphong.DiaChiID;
            model.IsYeuCauDuyetHuy = vanphong.IsYeuCauDuyetHuy;
            model.KhuVucId = vanphong.KhuVucId.GetValueOrDefault(0);
            model.SelectedToVanChuyenIds = vanphong.tovanchuyens.Select(c => c.ToVanChuyenId).ToArray();
            //default values           
            VanPhongModelPrepare(model);

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult VanPhongSua(VanPhongModel model, bool continueEditing, FormCollection form)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var vanphong = _nhaxeService.GetVanPhongById(model.Id);
            if (vanphong == null || vanphong.isDelete)
                //No manufacturer found with the specified id
                return RedirectToAction("VanPhongList");

            if (ModelState.IsValid)
            {
                var diachi = _diachiService.GetById(vanphong.DiaChiID);
                if (diachi != null)
                {
                    diachi = model.ThongTinDiaChi.ToEntity(diachi);
                    diachi.Latitude = form.GetValue("ThongTinDiaChi.Latitude").AttemptedValue.ToDecimal();
                    diachi.Longitude = form.GetValue("ThongTinDiaChi.Longitude").AttemptedValue.ToDecimal();
                    diachi.Id = vanphong.DiaChiID;
                    _diachiService.Update(diachi);
                }
                else
                {
                    diachi = model.ThongTinDiaChi.ToEntity(diachi);
                    diachi.Latitude = form.GetValue("ThongTinDiaChi.Latitude").AttemptedValue.ToDecimal();
                    diachi.Longitude = form.GetValue("ThongTinDiaChi.Longitude").AttemptedValue.ToDecimal();
                    _diachiService.Insert(diachi);
                    vanphong.DiaChiID = diachi.Id;
                }

                vanphong.TenVanPhong = model.TenVanPhong;
                vanphong.Ma = model.Ma;
                vanphong.KieuVanPhongID = model.KieuVanPhongID;
                vanphong.DienThoaiDatVe = model.DienThoaiDatVe;
                vanphong.DienThoaiGuiHang = model.DienThoaiGuiHang;
                vanphong.IsYeuCauDuyetHuy = model.IsYeuCauDuyetHuy;
                _nhaxeService.UpdateVanPhong(vanphong);

                var alltovanchuyens = _phieuchuyenphatService.GetAllToVanChuyen(_workContext.NhaXeId);

                foreach (var tvc in alltovanchuyens)
                {

                    if (model.SelectedToVanChuyenIds != null && model.SelectedToVanChuyenIds.Contains(tvc.Id))
                    {
                        //new role
                        if (vanphong.tovanchuyens.Select(c => c.tovanchuyen).Count(cr => cr.Id == tvc.Id) == 0)
                        {
                            var _item = new ToVanChuyenVanPhong();
                            _item.ToVanChuyenId = tvc.Id;
                            _item.VanPhongId = vanphong.Id;
                            _phieuchuyenphatService.InsertToVanChuyenVanPhong(_item);
                        }

                    }
                    else
                    {
                        //remove role
                        if (vanphong.tovanchuyens.Select(c => c.tovanchuyen).Count(cr => cr.Id == tvc.Id) > 0)
                        {
                            var _item = _phieuchuyenphatService.GetToVanChuyenVanPhong(vanphong.Id, tvc.Id);
                            _phieuchuyenphatService.DeleteToVanChuyenVanPhong(_item);
                        }

                    }
                }

                _nhaxeService.UpdateVanPhong(vanphong);


                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("VanPhongSua", vanphong.Id);
                }
                return RedirectToAction("VanPhongList");
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult VanPhongXoa(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVThongTinNhaXe))
                return AccessDeniedView();

            var vanphong = _nhaxeService.GetVanPhongById(id);
            if (vanphong == null || vanphong.isDelete)
                //No manufacturer found with the specified id
                return RedirectToAction("VanPhongList");

            _nhaxeService.DeleteVanPhong(vanphong);

            return RedirectToAction("VanPhongList");
        }
        [NonAction]
        protected virtual void VanPhongModelPrepare(VanPhongModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (model.DiaChiID > 0)
            {
                var diachi = _diachiService.GetById(model.DiaChiID);
                model.ThongTinDiaChi = diachi.ToModel();
            }
            DiaChiInfoPrepare(model.ThongTinDiaChi);

            model.KieuVanPhongs = this.GetCVEnumSelectList<ENKieuVanPhong>(_localizationService, model.KieuVanPhongID);
            var dllkhuvucs = _phieuchuyenphatService.GetAllKhuVuc(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.TenKhuVuc;
                item.Value = c.Id.ToString();
                item.Selected = c.Id == model.KhuVucId;
                return item;
            }).ToList();
            dllkhuvucs.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = "-----------Chọn khu vực ---------"
            });
            model.khuvucs = dllkhuvucs;
            model.AllToVanChuyens = _phieuchuyenphatService.GetAllToVanChuyen(_workContext.NhaXeId).Select(c =>
            {
                var tvc = new ToVanChuyenModel();
                tvc.Id = c.Id;
                tvc.TenTo = c.TenTo;
                tvc.MoTa = c.MoTa;
                return tvc;
            }).ToList();
        }

        [HttpPost]
        public ActionResult VitriBanDoPopUp(DiaChiInfoModel model)
        {
            return View(model);
        }
        #endregion
        #region Nhan Vien

        public ActionResult NhanVienList()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVNhanVien))
                return AccessDeniedView();
            var model = new NhanVienListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult NhanVienList(int VanPhongId, string TenNhanVien)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVNhanVien))
                return AccessDeniedView();

            var nhanviens = _nhanvienService.GetAllByVanPhongId(VanPhongId, TenNhanVien);
            var gridModel = new DataSourceResult
            {
                Data = nhanviens.Select(x =>
                {
                    var m = new NhanVienModel();
                    NhanVienToNhanVienModel(x, m);
                    return m;
                }),
                Total = nhanviens.Count
            };

            return Json(gridModel);
        }
        public ActionResult NhanVienTao()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVNhanVien))
                return AccessDeniedView();

            var model = new NhanVienModel();
            NhanVienModelPrepare(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult NhanVienTao(NhanVienModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVNhanVien))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var diachi = model.ThongTinDiaChi.ToEntity(null);
                _diachiService.Insert(diachi);
                var nhanvien = new NhanVien();
                NhanVienModelToNhanVien(model, nhanvien);
                nhanvien.NhaXeID = _workContext.NhaXeId;
                nhanvien.DiaChiID = diachi.Id;
                _nhanvienService.Insert(nhanvien);
                SuccessNotification(GetLabel("NhanVien.themmoithanhcong"));
                return continueEditing ? RedirectToAction("NhanVienSua", new { id = nhanvien.Id }) : RedirectToAction("NhanVienList");
            }

            return View(model);
        }
        public ActionResult NhanVienSua(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVNhanVien))
                return AccessDeniedView();

            var nhanvien = _nhanvienService.GetById(id);
            if (nhanvien == null || nhanvien.isDelete)
                //No manufacturer found with the specified id
                return RedirectToAction("NhanVienList");
            var model = new NhanVienModel();
            NhanVienToNhanVienModel(nhanvien, model);
            //default values           
            NhanVienModelPrepare(model);

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult NhanVienSua(NhanVienModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVNhanVien))
                return AccessDeniedView();

            var nhanvien = _nhanvienService.GetById(model.Id);
            if (nhanvien == null || nhanvien.isDelete)
                //No manufacturer found with the specified id
                return RedirectToAction("NhanVienList");

            if (ModelState.IsValid)
            {

                NhanVienModelToNhanVien(model, nhanvien);
                _nhanvienService.Update(nhanvien);

                var diachi = _diachiService.GetById(nhanvien.DiaChiID);
                diachi = model.ThongTinDiaChi.ToEntity(diachi);
                diachi.Id = nhanvien.DiaChiID;
                _diachiService.Update(diachi);


                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("NhanVienSua", nhanvien.Id);
                }
                return RedirectToAction("NhanVienList");
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult NhanVienXoa(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVNhanVien))
                return AccessDeniedView();

            var nhanvien = _nhanvienService.GetById(id);
            if (nhanvien == null || nhanvien.isDelete)
                //No manufacturer found with the specified id
                return RedirectToAction("NhanVienList");

            _nhanvienService.Delete(nhanvien);

            return RedirectToAction("NhanVienList");
        }
        [NonAction]
        protected virtual void NhanVienModelToNhanVien(NhanVienModel nvfrom, NhanVien nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.HoVaTen = nvfrom.HoVaTen;
            nvto.Email = nvfrom.Email;
            nvto.NgaySinh = nvfrom.NgaySinh;
            nvto.KieuNhanVienID = nvfrom.KieuNhanVienID;
            nvto.CMT_Id = nvfrom.CMT_Id;
            nvto.CMT_NgayCap = nvfrom.CMT_NgayCap;
            nvto.CMT_NoiCap = nvfrom.CMT_NoiCap;
            nvto.GioiTinhID = nvfrom.GioiTinhID;
            nvto.TrangThaiID = nvfrom.TrangThaiID;
            nvto.NgayBatDauLamViec = nvfrom.NgayBatDauLamViec;
            nvto.NgayNghiViec = nvfrom.NgayNghiViec;
            nvto.VanPhongID = nvfrom.VanPhongID;
            nvto.DienThoai = nvfrom.DienThoai;
            //nvto.SoGiayPhepLaiXe = nvfrom.SoGiayPhepLaiXe.Trim();
            //nvto.NgayCapGiayPhep = nvfrom.NgayCapGiayPhep;
        }
        [NonAction]
        protected virtual void NhanVienToNhanVienModel(NhanVien nvfrom, NhanVienModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.HoVaTen = nvfrom.HoVaTen;
            nvto.Email = nvfrom.Email;
            nvto.NgaySinh = nvfrom.NgaySinh;
            nvto.KieuNhanVienID = nvfrom.KieuNhanVienID;
            nvto.KieuNhanVienText = nvfrom.KieuNhanVien.GetLocalizedEnum(_localizationService, _workContext);
            nvto.CMT_Id = nvfrom.CMT_Id;
            nvto.CMT_NgayCap = nvfrom.CMT_NgayCap;
            nvto.CMT_NoiCap = nvfrom.CMT_NoiCap;
            nvto.GioiTinhID = nvfrom.GioiTinhID;
            nvto.GioiTinhText = nvfrom.GioiTinh.GetLocalizedEnum(_localizationService, _workContext);
            nvto.DiaChiID = nvfrom.DiaChiID;
            nvto.TrangThaiID = nvfrom.TrangThaiID;
            nvto.TrangThaiText = nvfrom.TrangThai.GetLocalizedEnum(_localizationService, _workContext);
            nvto.NgayBatDauLamViec = nvfrom.NgayBatDauLamViec;
            nvto.NgayNghiViec = nvfrom.NgayNghiViec;
            nvto.CustomerID = nvfrom.CustomerID;
            nvto.NhaXeID = nvfrom.NhaXeID;
            nvto.DienThoai = nvfrom.DienThoai;
            nvto.VanPhongID = nvfrom.VanPhongID;
            if (nvto.CustomerID > 0)
            {
                nvto.CustomerActionText = GetLabel("TaiKhoan.CapNhat");
            }
            else
            {
                nvto.CustomerActionText = GetLabel("TaiKhoan.TaoMoi");
            }
        }
        [NonAction]
        protected virtual void NhanVienModelPrepare(NhanVienModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (model.DiaChiID > 0)
            {
                var diachi = _diachiService.GetById(model.DiaChiID);
                model.ThongTinDiaChi = diachi.ToModel();
            }

            var states = _stateProvinceService.GetStateProvincesByCountryId(CountryID);
            if (states.Count > 0)
            {
                foreach (var s in states)
                {
                    model.ThongTinDiaChi.AvailableStates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.ThongTinDiaChi.ProvinceID) });
                }
            }
            int ProvinceID = Convert.ToInt32(model.ThongTinDiaChi.AvailableStates[0].Value);
            if (model.DiaChiID > 0 && model.ThongTinDiaChi.ProvinceID > 0)
            {
                ProvinceID = model.ThongTinDiaChi.ProvinceID;
            }
            var quanhuyens = _diachiService.GetQuanHuyenByProvinceId(ProvinceID);
            foreach (var s in quanhuyens)
            {
                model.ThongTinDiaChi.AvailableQuanHuyens.Add(new SelectListItem { Text = s.Ten, Value = s.Id.ToString(), Selected = (s.Id == model.ThongTinDiaChi.QuanHuyenID) });
            }

            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
            if (vanphongs.Count > 0)
            {
                model.VanPhongs.Add(new SelectListItem { Text = GetLabel("VanPhong.Chon"), Value = "0", Selected = (0 == model.VanPhongID) });
                foreach (var s in vanphongs)
                {
                    model.VanPhongs.Add(new SelectListItem { Text = s.TenVanPhong, Value = s.Id.ToString(), Selected = (s.Id == model.VanPhongID) });
                }
            }
            model.KieuNhanViens = this.GetCVEnumSelectList<ENKieuNhanVien>(_localizationService, model.KieuNhanVienID);
            model.TrangThais = this.GetCVEnumSelectList<ENTrangThaiNhanVien>(_localizationService, model.TrangThaiID);
            model.GioiTinhs = this.GetCVEnumSelectList<ENGioiTinh>(_localizationService, model.GioiTinhID);

        }

        ////////////////////////////TAI KHOAN VA PHAN QUYEN
        public ActionResult TaiKhoanList()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return AccessDeniedView();
            ViewBag.NhanVienId = _workContext.CurrentNhanVien.Id;
            return NhanVienList();
        }

        [HttpPost]
        public ActionResult TaiKhoanList(int VanPhongId, string TenNhanVien)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return AccessDeniedView();

            var nhanviens = _nhanvienService.GetAllByVanPhongId(VanPhongId, TenNhanVien);
            var gridModel = new DataSourceResult
            {
                Data = nhanviens.Select(x =>
                {
                    var m = new NhanVienModel();
                    NhanVienToNhanVienModel(x, m);
                    return m;
                }),
                Total = nhanviens.Count()
            };

            return Json(gridModel);

        }
        [NonAction]
        protected virtual TaiKhoanModel TaiKhoanListModelPrepare(Customer customer)
        {
            return new TaiKhoanModel
            {
                Id = customer.Id,
                Email = customer.IsRegistered() ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest"),
                Username = customer.Username,
                FullName = customer.GetFullName(),
                Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company),
                Phone = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone),
                ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode),
                Active = customer.Active,
                CreatedOn = _dateTimeHelper.ConvertToUserTime(customer.CreatedOnUtc, DateTimeKind.Utc),
                LastActivityDate = _dateTimeHelper.ConvertToUserTime(customer.LastActivityDateUtc, DateTimeKind.Utc),
            };
        }


        public ActionResult TaiKhoanSua(int NhanVienId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return AccessDeniedView();
            //id cua nhan vien
            var nhanvien = _nhanvienService.GetById(NhanVienId);
            Customer customer = null;
            var model = new TaiKhoanModel();
            if (nhanvien.CustomerID > 0)
            {
                customer = _customerService.GetCustomerById(nhanvien.CustomerID.GetValueOrDefault());
            }
            else
            {
                model.Email = nhanvien.Email;
                model.DateOfBirth = nhanvien.NgaySinh;
                int _pos = nhanvien.HoVaTen.LastIndexOf(' ');
                string _ten = nhanvien.HoVaTen.Substring(_pos).Trim();
                string _ho = nhanvien.HoVaTen.Substring(0, _pos).Trim();
                model.FirstName = _ten;
                model.LastName = _ho;

            }
            model.NhanVienId = NhanVienId;
            TaiKhoanModelPrepare(model, customer, false);            
            model.Active = true;
            return View(model);


        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public ActionResult TaiKhoanSua(TaiKhoanModel model, bool continueEditing, FormCollection form)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return AccessDeniedView();
            //kiem tra viec tao moi tai khoan
            if (!String.IsNullOrWhiteSpace(model.Email))
            {
                var cust2 = _customerService.GetCustomerByEmail(model.Email);
                if (cust2 != null && !cust2.Deleted && cust2.Id != model.Id)
                    ModelState.AddModelError("", "Email is already registered");
            }
            if (!String.IsNullOrWhiteSpace(model.Username) & _customerSettings.UsernamesEnabled)
            {
                var cust2 = _customerService.GetCustomerByUsername(model.Username);
                if (cust2 != null && cust2.Deleted && cust2.Id != model.Id)
                    ModelState.AddModelError("", "Username is already registered");
            }


            //validate customer roles
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            var newCustomerRoles = new List<CustomerRole>();

            foreach (var customerRole in allCustomerRoles)
            {
                //register
                if (customerRole.SystemName == SystemCustomerRoleNames.Registered)
                {
                    newCustomerRoles.Add(customerRole);
                    continue;
                }
                if (model.SelectedCustomerRoleIds != null && model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                    newCustomerRoles.Add(customerRole);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    //lay tat ca thong tin van phong
                    var allvanphong = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
                    var nhanvien = _nhanvienService.GetById(model.NhanVienId);
                    //cap nhat van phong quan ly cho nhan vien
                    foreach (var vp in allvanphong)
                    {

                        if (model.SelectedNhanVienVanPhongIds != null && model.SelectedNhanVienVanPhongIds.Contains(vp.Id))
                        {
                            //new role
                            if (nhanvien.VanPhongs.Count(cr => cr.Id == vp.Id) == 0)
                                nhanvien.VanPhongs.Add(vp);
                        }
                        else
                        {
                            //remove role
                            if (nhanvien.VanPhongs.Count(cr => cr.Id == vp.Id) > 0)
                                nhanvien.VanPhongs.Remove(vp);
                        }
                    }

                    Customer customer = null;
                    if (model.Id > 0)
                    {
                        customer = _customerService.GetCustomerById(model.Id);
                        customer.AdminComment = model.AdminComment;
                        customer.IsTaxExempt = model.IsTaxExempt;
                        customer.Active = model.Active;
                        //email
                        if (!String.IsNullOrWhiteSpace(model.Email))
                        {
                            _customerRegistrationService.SetEmail(customer, model.Email);
                        }
                        else
                        {
                            customer.Email = model.Email;
                        }

                        //username
                        if (_customerSettings.UsernamesEnabled && _customerSettings.AllowUsersToChangeUsernames)
                        {
                            if (!String.IsNullOrWhiteSpace(model.Username))
                            {
                                _customerRegistrationService.SetUsername(customer, model.Username);
                            }
                            else
                            {
                                customer.Username = model.Username;
                            }
                        }



                        //vendor
                        customer.VendorId = model.VendorId;
                        //form fields
                        if (_dateTimeSettings.AllowCustomersToSetTimeZone)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.TimeZoneId, model.TimeZoneId);
                        if (_customerSettings.GenderEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender);
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
                        if (_customerSettings.DateOfBirthEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, model.DateOfBirth);
                        if (_customerSettings.CompanyEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Company, model.Company);
                        if (_customerSettings.StreetAddressEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress, model.StreetAddress);
                        if (_customerSettings.StreetAddress2Enabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress2, model.StreetAddress2);
                        if (_customerSettings.ZipPostalCodeEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.ZipPostalCode, model.ZipPostalCode);
                        if (_customerSettings.CityEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.City, model.City);
                        if (_customerSettings.CountryEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CountryId, model.CountryId);
                        if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
                        if (_customerSettings.PhoneEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
                        if (_customerSettings.FaxEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Fax, model.Fax);




                        //customer roles
                        foreach (var customerRole in allCustomerRoles)
                        {

                            if ((model.SelectedCustomerRoleIds != null && model.SelectedCustomerRoleIds.Contains(customerRole.Id)) || customerRole.Id == 3)
                            {
                                //new role
                                if (customer.CustomerRoles.Count(cr => cr.Id == customerRole.Id) == 0)
                                    customer.CustomerRoles.Add(customerRole);
                            }
                            else
                            {
                                //remove role
                                if (customer.CustomerRoles.Count(cr => cr.Id == customerRole.Id) > 0)
                                    customer.CustomerRoles.Remove(customerRole);
                            }
                        }
                        _customerService.UpdateCustomer(customer);
                        SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.Updated"));
                    }
                    else
                    {
                        customer = new Customer
                        {
                            CustomerGuid = Guid.NewGuid(),
                            Email = model.Email,
                            Username = model.Username == null ? model.Email : model.Username,
                            VendorId = model.VendorId,
                            AdminComment = model.AdminComment,
                            IsTaxExempt = model.IsTaxExempt,
                            Active = model.Active,
                            CreatedOnUtc = DateTime.UtcNow,
                            LastActivityDateUtc = DateTime.UtcNow,
                        };
                        _customerService.InsertCustomer(customer);
                        //form fields
                        if (_dateTimeSettings.AllowCustomersToSetTimeZone)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.TimeZoneId, model.TimeZoneId);
                        if (_customerSettings.GenderEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender);
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
                        if (_customerSettings.DateOfBirthEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, model.DateOfBirth);
                        if (_customerSettings.CompanyEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Company, model.Company);
                        if (_customerSettings.StreetAddressEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress, model.StreetAddress);
                        if (_customerSettings.StreetAddress2Enabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress2, model.StreetAddress2);
                        if (_customerSettings.ZipPostalCodeEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.ZipPostalCode, model.ZipPostalCode);
                        if (_customerSettings.CityEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.City, model.City);
                        if (_customerSettings.CountryEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CountryId, model.CountryId);
                        if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
                        if (_customerSettings.PhoneEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
                        if (_customerSettings.FaxEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Fax, model.Fax);



                        //password
                        if (!String.IsNullOrWhiteSpace(model.Password))
                        {
                            var changePassRequest = new ChangePasswordRequest(model.Email, false, _customerSettings.DefaultPasswordFormat, model.Password);
                            var changePassResult = _customerRegistrationService.ChangePassword(changePassRequest);
                            if (!changePassResult.Success)
                            {
                                foreach (var changePassError in changePassResult.Errors)
                                    ErrorNotification(changePassError);
                            }
                        }
                        //customer roles
                        foreach (var customerRole in newCustomerRoles)
                        {
                            customer.CustomerRoles.Add(customerRole);
                        }
                        _customerService.UpdateCustomer(customer);
                        //activity log
                        _customerActivityService.InsertActivity("AddNewCustomer", _localizationService.GetResource("ActivityLog.AddNewCustomer"), customer.Id);

                        SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.Added"));
                        //cap nhat thong tin lien ket tai khoan
                        nhanvien.CustomerID = customer.Id;
                        
                    }
                    _nhanvienService.Update(nhanvien);



                    if (continueEditing)
                    {
                        //selected tab
                        SaveSelectedTabIndex();
                        return RedirectToAction("TaiKhoanSua", new { NhanVienId = model.NhanVienId });
                    }
                    return RedirectToAction("TaiKhoanList");
                }
                catch (Exception exc)
                {
                    ErrorNotification(exc.Message, false);
                }
            }


            //If we got this far, something failed, redisplay form
            TaiKhoanModelPrepare(model, null, true);
            return View(model);
        }
        [HttpPost, ActionName("TaiKhoanSua")]
        [FormValueRequired("changepassword")]
        public ActionResult ChangePassword(TaiKhoanModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return AccessDeniedView();

            var customer = _customerService.GetCustomerById(model.Id);
            if (customer == null)
                //No customer found with the specified id
                return RedirectToAction("TaiKhoanList");

            if (ModelState.IsValid)
            {

                var changePassRequest = new ChangePasswordRequest(model.Email,
                    false, _customerSettings.DefaultPasswordFormat, model.Password);
                var changePassResult = _customerRegistrationService.ChangePassword(changePassRequest);
                if (changePassResult.Success)
                    SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.PasswordChanged"));
                else
                    foreach (var error in changePassResult.Errors)
                        ErrorNotification(error);
            }

            return RedirectToAction("TaiKhoanSua", new { NhanVienId = model.NhanVienId });
        }

        [HttpPost]
        public ActionResult TaiKhoanXoa(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return AccessDeniedView();

            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
                //No customer found with the specified id
                return RedirectToAction("TaiKhoanList");

            try
            {
                _customerService.DeleteCustomer(customer);
                var nhanvien = _nhanvienService.GetByCustomerId(id);
                if (nhanvien != null)
                {
                    nhanvien.CustomerID = null;
                    _nhanvienService.Update(nhanvien);
                }
                //remove newsletter subscription (if exists)
                foreach (var store in _storeService.GetAllStores())
                {
                    var subscription = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, store.Id);
                    if (subscription != null)
                        _newsLetterSubscriptionService.DeleteNewsLetterSubscription(subscription);
                }

                //activity log
                _customerActivityService.InsertActivity("DeleteCustomer", _localizationService.GetResource("ActivityLog.DeleteCustomer"), customer.Id);

                SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.Deleted"));
                return RedirectToAction("TaiKhoanList");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc.Message);
                return RedirectToAction("TaiKhoanList");
            }
        }
        [HttpPost]
        public ActionResult ListActivityLog(DataSourceRequest command, int customerId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return AccessDeniedView();

            var activityLog = _customerActivityService.GetAllActivities(null, null, customerId, 0, command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = activityLog.Select(x =>
                {
                    var m = new TaiKhoanModel.ActivityLogModel
                    {
                        Id = x.Id,
                        ActivityLogTypeName = x.ActivityLogType.Name,
                        Comment = x.Comment,
                        CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc)
                    };
                    return m;

                }),
                Total = activityLog.TotalCount
            };

            return Json(gridModel);
        }
        [NonAction]
        protected virtual void TaiKhoanModelPrepare(TaiKhoanModel model, Customer customer, bool excludeProperties)
        {
            if (customer != null)
            {
                model.Id = customer.Id;
                if (!excludeProperties)
                {
                    model.Email = customer.Email;
                    model.Username = customer.Username;
                    model.VendorId = customer.VendorId;
                    model.AdminComment = customer.AdminComment;
                    model.IsTaxExempt = customer.IsTaxExempt;
                    model.Active = customer.Active;
                    model.AffiliateId = customer.AffiliateId;
                    model.TimeZoneId = customer.GetAttribute<string>(SystemCustomerAttributeNames.TimeZoneId);
                    model.VatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber);
                    model.VatNumberStatusNote = ((VatNumberStatus)customer.GetAttribute<int>(SystemCustomerAttributeNames.VatNumberStatusId))
                        .GetLocalizedEnum(_localizationService, _workContext);
                    model.CreatedOn = _dateTimeHelper.ConvertToUserTime(customer.CreatedOnUtc, DateTimeKind.Utc);
                    model.LastActivityDate = _dateTimeHelper.ConvertToUserTime(customer.LastActivityDateUtc, DateTimeKind.Utc);
                    model.LastIpAddress = customer.LastIpAddress;
                    model.LastVisitedPage = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastVisitedPage);

                    model.SelectedCustomerRoleIds = customer.CustomerRoles.Select(cr => cr.Id).ToArray();

                    //form fields
                    model.FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                    model.LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
                    model.Gender = customer.GetAttribute<string>(SystemCustomerAttributeNames.Gender);
                    model.DateOfBirth = customer.GetAttribute<DateTime?>(SystemCustomerAttributeNames.DateOfBirth);
                    model.Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company);
                    model.StreetAddress = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress);
                    model.StreetAddress2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2);
                    model.ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode);
                    model.City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City);
                    model.CountryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId);
                    model.StateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId);
                    model.Phone = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                    model.Fax = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax);
                }
            }

            model.UsernamesEnabled = false;


            //customer roles

            model.AvailableCustomerRoles = _customerService
                .GetAllCustomerRoles(false)
                .Where(cr => cr.SystemName.Contains("HTNhaXe") && !cr.SystemName.Contains("HTNhaXeManager"))
                .Select(cr =>
                {
                    var tkr = new TaiKhoanRoleModel();
                    TaiKhoanRoleToModelPrepare(cr, tkr);
                    return tkr;
                })
                .ToList();

            model.DisplayRewardPointsHistory = false;

            model.AddRewardPointsValue = 0;
            model.AddRewardPointsMessage = "Some comment here...";
            model.AvailableNhanVienVanPhongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var m = new VanPhongModel();
                m.Id = c.Id;
                m.TenVanPhong = string.Format("{0} - {1}", c.Ma, c.TenVanPhong);
                return m;
            }).ToList();
            if (model.NhanVienId > 0)
            {
                var _nhanvien = _nhanvienService.GetById(model.NhanVienId);
                model.SelectedNhanVienVanPhongIds = _nhanvien.VanPhongs.Select(c => c.Id).ToArray();
            }


        }
        [NonAction]
        protected virtual void TaiKhoanRoleToModelPrepare(CustomerRole rolefrom, TaiKhoanRoleModel roleto)
        {
            roleto.Id = rolefrom.Id;
            roleto.Name = rolefrom.Name;
            roleto.IsSystemRole = rolefrom.IsSystemRole;
            roleto.FreeShipping = rolefrom.FreeShipping;
            roleto.TaxExempt = rolefrom.TaxExempt;
            roleto.Active = rolefrom.Active;
            roleto.SystemName = rolefrom.SystemName;
            roleto.PurchasedWithProductId = rolefrom.PurchasedWithProductId;
        }
        [NonAction]
        protected virtual void TaiKhoanRoleFromModelPrepare(TaiKhoanRoleModel rolefrom, CustomerRole roleto)
        {
            roleto.Id = rolefrom.Id;
            roleto.Name = rolefrom.Name;
            roleto.IsSystemRole = rolefrom.IsSystemRole;
            roleto.FreeShipping = rolefrom.FreeShipping;
            roleto.TaxExempt = rolefrom.TaxExempt;
            roleto.Active = rolefrom.Active;
            roleto.SystemName = rolefrom.SystemName;
            roleto.PurchasedWithProductId = rolefrom.PurchasedWithProductId;
        }
        #endregion
        #region Quan ly loai xe
        public ActionResult LoaiXeList()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();
            var model = new LoaiXeListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult LoaiXeList(DataSourceRequest command, LoaiXeListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var LoaiXes = _xeinfoService.GetAll(_workContext.NhaXeId, model.TenLoaiXe,
                command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = LoaiXes.Select(x =>
                {
                    var m = new LoaiXeModel();
                    LoaiXeToLoaiXeModel(x, m);
                    return m;
                }),
                Total = LoaiXes.TotalCount
            };

            return Json(gridModel);
        }
        public ActionResult LoaiXeTao()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var model = new LoaiXeModel();
            LoaiXeModelPrepare(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult LoaiXeTao(LoaiXeModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var loaixe = new LoaiXe();
                LoaiXeModelToLoaiXe(model, loaixe);
                loaixe.NhaXeId = _workContext.NhaXeId;
                _xeinfoService.Insert(loaixe);
                SuccessNotification(GetLabel("LoaiXe.themmoithanhcong"));

                //insert thong tin quy tac va thong tin ghe
                //tang;i;j;val|tang;i;j;val
                string[] arrquytacstr = model.SoDoGheXeQuyTacResult.Split('|');
                foreach (string s in arrquytacstr)
                {
                    if (String.IsNullOrEmpty(s.Trim()))
                        continue;

                    string[] arritem = s.Split(';');
                    var item = new SoDoGheXeQuyTac();
                    item.LoaiXeId = loaixe.Id;
                    item.Tang = Convert.ToInt32(arritem[0]);
                    item.y = Convert.ToInt32(arritem[1]);
                    item.x = Convert.ToInt32(arritem[2]);
                    item.Val = arritem[3];
                    _xeinfoService.InsertSoDoGheXeQuyTac(item);
                    if (item.x > 0 && item.y > 0)
                    {
                        var gheitem = new GheItem();
                        gheitem.KyHieuGhe = item.Val;
                        gheitem.Tang = item.Tang;
                        gheitem.LoaiXeId = item.LoaiXeId;
                        var _vitrixy = _xeinfoService.GetSoDoGheXeViTri(loaixe.SoDoGheXeID, item.x - 1, item.y - 1);
                        gheitem.SoDoGheXeViTriId = _vitrixy.Id;
                        _xeinfoService.InsertGheItem(gheitem);
                    }
                }

                return continueEditing ? RedirectToAction("LoaiXeSua", new { id = loaixe.Id }) : RedirectToAction("LoaiXeList");
            }
            LoaiXeModelPrepare(model);
            return View(model);
        }
        public ActionResult LoaiXeSua(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var loaixe = _xeinfoService.GetById(id);
            if (loaixe == null)
                //No manufacturer found with the specified id
                return RedirectToAction("LoaiXeList");
            var model = new LoaiXeModel();
            LoaiXeToLoaiXeModel(loaixe, model);
            //default values           
            LoaiXeModelPrepare(model);

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult LoaiXeSua(LoaiXeModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var loaixe = _xeinfoService.GetById(model.Id);
            if (loaixe == null)
                //No manufacturer found with the specified id
                return RedirectToAction("LoaiXeList");

            if (ModelState.IsValid)
            {

                LoaiXeModelToLoaiXe(model, loaixe);
                _xeinfoService.Update(loaixe);
                //cap nhat thong tin ghe
                //xoa truoc luc tao
                _xeinfoService.DeleteGheAndSoDoGheXeQuyTac(loaixe.Id);
                //insert thong tin quy tac va thong tin ghe
                //tang;i;j;val|tang;i;j;val
                string[] arrquytacstr = model.SoDoGheXeQuyTacResult.Split('|');
                foreach (string s in arrquytacstr)
                {
                    if (String.IsNullOrEmpty(s.Trim()))
                        continue;

                    string[] arritem = s.Split(';');
                    var item = new SoDoGheXeQuyTac();
                    item.LoaiXeId = loaixe.Id;
                    item.Tang = Convert.ToInt32(arritem[0]);
                    item.y = Convert.ToInt32(arritem[1]);
                    item.x = Convert.ToInt32(arritem[2]);
                    item.Val = arritem[3];
                    _xeinfoService.InsertSoDoGheXeQuyTac(item);
                    if (item.x > 0 && item.y > 0)
                    {
                        var gheitem = new GheItem();
                        gheitem.KyHieuGhe = item.Val;
                        gheitem.Tang = item.Tang;
                        gheitem.LoaiXeId = item.LoaiXeId;
                        var _vitrixy = _xeinfoService.GetSoDoGheXeViTri(loaixe.SoDoGheXeID, item.x - 1, item.y - 1);
                        gheitem.SoDoGheXeViTriId = _vitrixy.Id;
                        _xeinfoService.InsertGheItem(gheitem);
                    }
                }
                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("LoaiXeSua", loaixe.Id);
                }
                return RedirectToAction("LoaiXeList");
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult LoaiXeXoa(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var loaixe = _xeinfoService.GetById(id);
            if (loaixe == null)
                //No manufacturer found with the specified id
                return RedirectToAction("LoaiXeList");

            _xeinfoService.Delete(loaixe);

            return RedirectToAction("LoaiXeList");
        }
        [NonAction]
        protected virtual void LoaiXeModelToLoaiXe(LoaiXeModel nvfrom, LoaiXe nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.TenLoaiXe = nvfrom.TenLoaiXe;
            nvto.MaLoaiXe = nvfrom.MaLoaiXe;
            nvto.KieuXeID = nvfrom.KieuXeID;
            nvto.SoDoGheXeID = nvfrom.SoDoGheXeID;
            nvto.IsWC = nvfrom.IsWC;
            nvto.IsTV = nvfrom.IsTV;
            nvto.IsWifi = nvfrom.IsWifi;
            nvto.IsDieuHoa = nvfrom.IsDieuHoa;
            nvto.IsNuocUong = nvfrom.IsNuocUong;
            nvto.IsKhanLanh = nvfrom.IsKhanLanh;
            nvto.IsThucAn = nvfrom.IsThucAn;
        }
        [NonAction]
        protected virtual void LoaiXeToLoaiXeModel(LoaiXe nvfrom, LoaiXeModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.TenLoaiXe = nvfrom.TenLoaiXe;
            nvto.MaLoaiXe = nvfrom.MaLoaiXe;
            nvto.KieuXeID = nvfrom.KieuXeID;
            nvto.KieuXeText = nvfrom.KieuXe.GetLocalizedEnum(_localizationService, _workContext);
            nvto.SoDoGheXeID = nvfrom.SoDoGheXeID;
            var _sdgx = _xeinfoService.GetSoDoGheXeById(nvto.SoDoGheXeID);
            nvto.SoDoGheXeText = GetLabel(_sdgx.TenSoDo);
            nvto.IsWC = nvfrom.IsWC;
            nvto.IsTV = nvfrom.IsTV;
            nvto.IsWifi = nvfrom.IsWifi;
            nvto.IsDieuHoa = nvfrom.IsDieuHoa;
            nvto.IsNuocUong = nvfrom.IsNuocUong;
            nvto.IsKhanLanh = nvfrom.IsKhanLanh;
            nvto.IsThucAn = nvfrom.IsThucAn;
        }
        [NonAction]
        protected virtual void LoaiXeModelPrepare(LoaiXeModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.KieuXes = this.GetCVEnumSelectList<ENKieuXe>(_localizationService, model.KieuXeID);
            var sodoghexes = _xeinfoService.GetAllSoDoGheXe((int)ENKieuXe.GheNgoi);
            if (sodoghexes.Count > 0)
            {
                foreach (var s in sodoghexes)
                {
                    var sdg = new LoaiXeModel.SoDoGheXeModel();
                    sdg.Id = s.Id;
                    sdg.TenSoDo = GetLabel(s.TenSoDo);
                    sdg.UrlImage = s.TenSoDo;
                    model.SoDoGheXes.Add(sdg);
                }
            }
            if (model.Id > 0)
            {
                model.GheItems = _xeinfoService.GetAllGheItem(model.Id).Select(c =>
                {
                    var gheitemmodel = new LoaiXeModel.GheItemModel();
                    GheItemToGheItemModel(c, gheitemmodel);
                    return gheitemmodel;
                }).ToList();
                model.SoDoGheXeQuyTacs = _xeinfoService.GetAllSoDoGheXeQuyTac(model.Id).Select(c =>
                {
                    var item = new LoaiXeModel.SoDoGheXeQuyTacModel();
                    item.Id = c.Id;
                    item.Val = c.Val;
                    item.x = c.x;
                    item.y = c.y;
                    item.Tang = c.Tang;
                    item.LoaiXeId = c.LoaiXeId;
                    return item;
                }).ToList();
            }
            if (model.SoDoGheXeID > 0)
            {
                var sodoghexe = _xeinfoService.GetSoDoGheXeById(model.SoDoGheXeID);
                SoDoGheXeToSoDoGheXeModel(sodoghexe, model.CurrentSoDoGheXe);
            }

        }
        [NonAction]
        protected virtual void GheItemModelToGheItem(LoaiXeModel.GheItemModel nvfrom, GheItem nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.LoaiXeId = nvfrom.LoaiXeId;
            nvto.KyHieuGhe = nvfrom.KyHieuGhe;
            nvto.Tang = nvfrom.Tang;
            nvto.SoDoGheXeViTriId = nvfrom.SoDoGheXeViTriId;


        }
        [NonAction]
        protected virtual void GheItemToGheItemModel(GheItem nvfrom, LoaiXeModel.GheItemModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.LoaiXeId = nvfrom.LoaiXeId;
            nvto.KyHieuGhe = nvfrom.KyHieuGhe;
            nvto.Tang = nvfrom.Tang;
            nvto.SoDoGheXeViTriId = nvfrom.SoDoGheXeViTriId;

        }

        [HttpPost]
        public ActionResult GetSoDoGheXe(DataSourceRequest command, LoaiXeModel model, int KieuXeID)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var items = _xeinfoService.GetAllSoDoGheXe(KieuXeID);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var m = new LoaiXeModel.SoDoGheXeModel();
                    SoDoGheXeToSoDoGheXeModel(x, m);
                    return m;
                }),
                Total = items.Count
            };

            return Json(gridModel);
        }
        [NonAction]
        protected virtual void SoDoGheXeToSoDoGheXeModel(SoDoGheXe nvfrom, LoaiXeModel.SoDoGheXeModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.TenSoDo = GetLabel(nvfrom.TenSoDo);
            nvto.UrlImage = nvfrom.TenSoDo;
            nvto.SoLuongGhe = nvfrom.SoLuongGhe;
            nvto.KieuXeId = nvfrom.KieuXeId;
            nvto.SoCot = nvfrom.SoCot;
            nvto.SoHang = nvfrom.SoHang;


        }
        //
        public ActionResult GetSoDoGheXeById(int SoDoGheXeID, int LoaiXeID)
        {
            var sodoghe = _xeinfoService.GetSoDoGheXeById(SoDoGheXeID);
            var modelsdg = new LoaiXeModel.SoDoGheXeModel();
            SoDoGheXeToSoDoGheXeModel(sodoghe, modelsdg);
            //lay thong tin ma tran
            var sodoghexevitris = _xeinfoService.GetAllSoDoGheViTri(sodoghe.Id);
            var quytacs = new List<SoDoGheXeQuyTac>();
            int _loaixeid = Convert.ToInt32(LoaiXeID);
            if (_loaixeid > 0)
            {
                var loaixe = _xeinfoService.GetById(_loaixeid);
                if (loaixe.SoDoGheXeID == Convert.ToInt32(SoDoGheXeID))
                    quytacs = _xeinfoService.GetAllSoDoGheXeQuyTac(Convert.ToInt32(LoaiXeID));
            }

            modelsdg.MaTran = new int[modelsdg.SoHang, modelsdg.SoCot];
            modelsdg.PhoiVes1 = new LoaiXeModel.PhoiVeAdvanceModel[modelsdg.SoHang + 1, modelsdg.SoCot + 1];
            if (sodoghe.KieuXe == ENKieuXe.GiuongNam)
            {
                modelsdg.SoTang = 2;
                modelsdg.PhoiVes2 = new LoaiXeModel.PhoiVeAdvanceModel[modelsdg.SoHang + 1, modelsdg.SoCot + 1];
            }

            foreach (var s in sodoghexevitris)
            {
                modelsdg.MaTran[s.y, s.x] = 1;
            }
            for (int i = 0; i < modelsdg.SoHang + 1; i++)
            {
                for (int j = 0; j < modelsdg.SoCot + 1; j++)
                {
                    modelsdg.PhoiVes1[i, j] = new LoaiXeModel.PhoiVeAdvanceModel();
                    if (modelsdg.SoTang == 2)
                    {
                        modelsdg.PhoiVes2[i, j] = new LoaiXeModel.PhoiVeAdvanceModel();
                    }
                }

            }
            //
            if (quytacs != null && quytacs.Count > 0)
            {
                foreach (var s in quytacs)
                {
                    if (s.Tang == 1)
                    {
                        modelsdg.PhoiVes1[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsdg.PhoiVes1[s.y, s.x].KyHieu = s.Val;
                    }
                    else
                    {
                        modelsdg.PhoiVes2[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsdg.PhoiVes2[s.y, s.x].KyHieu = s.Val;
                    }

                }
            }
            else
            {

                //A
                int _A = 65;
                for (int i = 1; i < modelsdg.SoHang + 1; i++)
                {

                    modelsdg.PhoiVes1[i, 0].KyHieu = Convert.ToString((char)(_A + i - 1));
                    if (sodoghe.KieuXe == ENKieuXe.GiuongNam)
                    {
                        modelsdg.PhoiVes2[i, 0].KyHieu = Convert.ToString((char)(_A + i - 1));
                    }

                }
                for (int i = 1; i < modelsdg.SoCot + 1; i++)
                {
                    modelsdg.PhoiVes1[0, i].KyHieu = i.ToString();
                    if (sodoghe.KieuXe == ENKieuXe.GiuongNam)
                        modelsdg.PhoiVes2[0, i].KyHieu = i.ToString();

                }

                for (int i = 1; i < modelsdg.SoHang + 1; i++)
                {
                    for (int j = 1; j < modelsdg.SoCot + 1; j++)
                    {
                        //lay hang gan voi cot = ky hieu ghe
                        modelsdg.PhoiVes1[i, j].KyHieu = string.Format("{0}{1}", modelsdg.PhoiVes1[i, 0].KyHieu, modelsdg.PhoiVes1[0, j].KyHieu);
                        if (sodoghe.KieuXe == ENKieuXe.GiuongNam)
                            modelsdg.PhoiVes2[i, j].KyHieu = string.Format("{0}{1}", modelsdg.PhoiVes2[i, 0].KyHieu, modelsdg.PhoiVes2[0, j].KyHieu);
                    }
                }

            }
            return PartialView(modelsdg);

        }
        #endregion
        #region Quan ly xe
        public ActionResult XeInfoList()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();
            var model = new XeInfoListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult XeInfoList(DataSourceRequest command, XeInfoListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var items = _xeinfoService.GetAllXeInfo(_workContext.NhaXeId, model.TenXe,
                command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var m = new XeInfoModel();
                    XeInfoToXeInfoModel(x, m);
                    return m;
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }
        public ActionResult XeInfoTao()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var model = new XeInfoModel();
            XeInfoModelPrepare(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult XeInfoTao(XeInfoModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = new XeVanChuyen();
                XeInfoModelToXeInfo(model, item);
                item.NhaXeId = _workContext.NhaXeId;
                _xeinfoService.InsertXeInfo(item);
                SuccessNotification(GetLabel("XeInfo.themmoithanhcong"));

                return continueEditing ? RedirectToAction("XeInfoSua", new { id = item.Id }) : RedirectToAction("XeInfoList");
            }

            return View(model);
        }
        public ActionResult XeInfoSua(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var item = _xeinfoService.GetXeInfoById(id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("XeInfoList");
            var model = new XeInfoModel();
            XeInfoToXeInfoModel(item, model);
            //default values           
            XeInfoModelPrepare(model);

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult XeInfoSua(XeInfoModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var item = _xeinfoService.GetXeInfoById(model.Id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("XeInfoList");

            if (ModelState.IsValid)
            {

                XeInfoModelToXeInfo(model, item);
                _xeinfoService.UpdateXeInfo(item);

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("XeInfoSua", item.Id);
                }
                return RedirectToAction("XeInfoList");
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult XeInfoXoa(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVXeVanChuyen))
                return AccessDeniedView();

            var item = _xeinfoService.GetXeInfoById(id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("XeInfoList");

            _xeinfoService.DeleteXeInfo(item);

            return RedirectToAction("XeInfoList");
        }
        [NonAction]
        protected virtual void XeInfoModelToXeInfo(XeInfoModel nvfrom, XeVanChuyen nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.TenXe = nvfrom.TenXe;
            nvto.TrangThaiXeId = nvfrom.TrangThaiXeId;
            nvto.LoaiXeId = nvfrom.LoaiXeId;
            nvto.BienSo = nvfrom.BienSo;
            nvto.DienThoai = nvfrom.DienThoai;

            nvto.NgayDangKiem = nvfrom.NgayDangKiem;
            nvto.PhuHieuXe = nvfrom.PhuHieuXe;
            nvto.NgayDangKy = nvfrom.NgayDangKy;
            nvto.BaoHiem = nvfrom.BaoHiem;
        }
        [NonAction]
        protected virtual void XeInfoToXeInfoModel(XeVanChuyen nvfrom, XeInfoModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.TenXe = nvfrom.TenXe;
            nvto.TrangThaiXeId = nvfrom.TrangThaiXeId;
            nvto.TrangThaiXeText = nvfrom.TrangThaiXe.GetLocalizedEnum(_localizationService, _workContext);
            nvto.LoaiXeId = nvfrom.LoaiXeId;
            if (nvto.LoaiXeId > 0)
                nvto.LoaiXeText = _xeinfoService.GetById(nvto.LoaiXeId).TenLoaiXe;
            nvto.BienSo = nvfrom.BienSo;
            nvto.DienThoai = nvfrom.DienThoai;
            nvto.NgayDangKiem = nvfrom.NgayDangKiem;
            nvto.PhuHieuXe = nvfrom.PhuHieuXe;
            nvto.NgayDangKy = nvfrom.NgayDangKy;
            nvto.BaoHiem = nvfrom.BaoHiem;
        }
        [NonAction]
        protected virtual void XeInfoModelPrepare(XeInfoModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.TrangThaiXes = this.GetCVEnumSelectList<ENTrangThaiXe>(_localizationService, model.TrangThaiXeId);
            model.LoaiXes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.TenLoaiXe;
                item.Value = c.Id.ToString();
                item.Selected = c.Id == model.LoaiXeId;
                return item;
            }).ToList();

        }
        #endregion
        #region Diem Don

        public ActionResult DiemDonList()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var model = new DiemDonListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult DiemDonList(DataSourceRequest command, DiemDonListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var items = _hanhtrinhService.GetAllDiemDon(_workContext.NhaXeId, model.TenDiemDon,
                command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var m = new DiemDonModel();
                    DiemDonToDiemDonModel(x, m);
                    return m;
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }
        public ActionResult BenXeList(string TenBenXe)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var items = _benxeService.Search(TenBenXe).Select(x =>
            {
                var item = x;
                item.TenBenXe = GetLabel("BenXe") + " " + item.TenBenXe;
                return item;
            });
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DiemDonTao()
        {
            var model = new DiemDonModel();
            DiemDonModelPrepare(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult DiemDonTao(DiemDonModel model, bool continueEditing, FormCollection form)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var diachi = model.ThongTinDiaChi.ToEntity(null);
                diachi.Latitude = form.GetValue("ThongTinDiaChi.Latitude").AttemptedValue.ToDecimal();
                diachi.Longitude = form.GetValue("ThongTinDiaChi.Longitude").AttemptedValue.ToDecimal();
                _diachiService.Insert(diachi);
                var item = new DiemDon();
                DiemDonModelToDiemDon(model, item);
                item.NhaXeId = _workContext.NhaXeId;
                item.DiaChiId = diachi.Id;
                _hanhtrinhService.InsertDiemDon(item);
                SuccessNotification(GetLabel("DiemDon.themmoithanhcong"));
                return continueEditing ? RedirectToAction("DiemDonSua", new { id = item.Id }) : RedirectToAction("DiemDonList");
            }

            return View(model);
        }
        public ActionResult DiemDonSua(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetDiemDonById(id);
            //No manufacturer found with the specified id
            if (item == null)
                return RedirectToAction("DiemDonList");
            var model = new DiemDonModel();
            DiemDonToDiemDonModel(item, model);
            //default values           
            DiemDonModelPrepare(model);

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult DiemDonSua(DiemDonModel model, bool continueEditing, FormCollection form)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetDiemDonById(model.Id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("DiemDonList");

            if (ModelState.IsValid)
            {
                var diachi = _diachiService.GetById(item.DiaChiId);
                if (diachi != null)
                {
                    diachi = model.ThongTinDiaChi.ToEntity(diachi);
                    diachi.Id = item.DiaChiId;
                    diachi.Latitude = form.GetValue("ThongTinDiaChi.Latitude").AttemptedValue.ToDecimal();
                    diachi.Longitude = form.GetValue("ThongTinDiaChi.Longitude").AttemptedValue.ToDecimal();
                    _diachiService.Update(diachi);
                }
                else
                {
                    diachi = model.ThongTinDiaChi.ToEntity(diachi);
                    diachi.Latitude = form.GetValue("ThongTinDiaChi.Latitude").AttemptedValue.ToDecimal();
                    diachi.Longitude = form.GetValue("ThongTinDiaChi.Longitude").AttemptedValue.ToDecimal();
                    _diachiService.Insert(diachi);
                    item.DiaChiId = diachi.Id;
                }

                DiemDonModelToDiemDon(model, item);
                _hanhtrinhService.UpdateDiemDon(item);




                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("DiemDonSua", item.Id);
                }
                return RedirectToAction("DiemDonList");
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult DiemDonXoa(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetDiemDonById(id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("DiemDonList");

            _hanhtrinhService.DeleteDiemDon(item);

            return RedirectToAction("DiemDonList");
        }
        [NonAction]
        protected virtual void DiemDonModelToDiemDon(DiemDonModel nvfrom, DiemDon nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.TenDiemDon = nvfrom.TenDiemDon;
            nvto.LoaiDiemDonId = nvfrom.LoaiDiemDonId;
            nvto.VanPhongId = nvfrom.VanPhongId;
            //nvto.DiaChiId = nvfrom.DiaChiId;
            nvto.BenXeId = nvfrom.BenXeId;
            nvto.MaMau = nvfrom.MaMau;

        }
        [NonAction]
        protected virtual void DiemDonToDiemDonModel(DiemDon nvfrom, DiemDonModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.TenDiemDon = nvfrom.TenDiemDon;
            nvto.MaMau = nvfrom.MaMau;
            nvto.LoaiDiemDonId = nvfrom.LoaiDiemDonId;
            nvto.LoaiDiemDonText = nvfrom.LoaiDiemDon.GetLocalizedEnum(_localizationService, _workContext);
            nvto.VanPhongId = nvfrom.VanPhongId.GetValueOrDefault(0);
            if (nvfrom.vanphong != null)
            {

                nvto.VanPhongText = nvfrom.vanphong.TenVanPhong;
            }

            nvto.DiaChiId = nvfrom.DiaChiId;
            nvto.BenXeId = nvfrom.BenXeId.GetValueOrDefault(0);
            if (nvfrom.benxe != null)
            {

                nvto.BenXeText = nvfrom.benxe.TenBenXe;
            }


        }
        [NonAction]
        protected virtual void DiemDonModelPrepare(DiemDonModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (model.DiaChiId > 0)
            {
                var diachi = _diachiService.GetById(model.DiaChiId);
                model.ThongTinDiaChi = diachi.ToModel();
            }
            DiaChiInfoPrepare(model.ThongTinDiaChi);

            model.LoaiDiemDons = this.GetCVEnumSelectList<ENLoaiDiemDon>(_localizationService, model.LoaiDiemDonId);
            model.VanPhongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.TenVanPhong;
                item.Value = c.Id.ToString();
                item.Selected = c.Id == model.VanPhongId;
                return item;
            }).ToList();

            model.VanPhongs.Insert(0, new SelectListItem { Text = GetLabel("DiemDon.ChonVanPhong"), Value = "0" });




        }

        #endregion
        #region Hanh Trinh

        public ActionResult HanhTrinhList()
        {

          
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();
            var model = new HanhTrinhListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult HanhTrinhList(DataSourceRequest command, HanhTrinhListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var items = _hanhtrinhService.GetAllHanhTrinh(_workContext.NhaXeId, model.MaHanhTrinh);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var m = new HanhTrinhModel();
                    HanhTrinhToHanhTrinhModel(x, m);
                    return m;
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }
        public ActionResult HanhTrinhTao()
        {
            var model = new HanhTrinhModel();
            HanhTrinhModelPrepare(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult HanhTrinhTao(HanhTrinhModel model, bool continueEditing, FormCollection form)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = new HanhTrinh();
                HanhTrinhModelToHanhTrinh(model, item);
                item.NhaXeId = _workContext.NhaXeId;
                if (_hanhtrinhService.InsertHanhTrinh(item))
                {
                    SuccessNotification(GetLabel("HanhTrinh.themmoithanhcong"));
                    return continueEditing ? RedirectToAction("HanhTrinhSua", new { id = item.Id }) : RedirectToAction("HanhTrinhList");
                }
                ErrorNotification(GetLabel("HanhTrinh.trungmahanhtrinh"));

            }

            return View(model);
        }
        public ActionResult HanhTrinhSua(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetHanhTrinhById(id);
            //No manufacturer found with the specified id
            if (item == null)
                return RedirectToAction("HanhTrinhList");
            var model = new HanhTrinhModel();
            HanhTrinhToHanhTrinhModel(item, model);
            //default values           
            HanhTrinhModelPrepare(model);
            HanhTrinhPrepareDiemDon(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult HanhTrinhSua(HanhTrinhModel model, bool continueEditing, FormCollection form)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetHanhTrinhById(model.Id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("HanhTrinhList");

            //if (ModelState.IsValid)
            //{
            var allvanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);

            foreach (var vp in allvanphongs)
            {

                if (model.SelectedVanPhongIds != null && model.SelectedVanPhongIds.Contains(vp.Id))
                {
                    //new role
                    if (item.VanPhongs.Count(cr => cr.Id == vp.Id) == 0)
                        item.VanPhongs.Add(vp);
                }
                else
                {
                    //remove role
                    if (item.VanPhongs.Count(cr => cr.Id == vp.Id) > 0)
                        item.VanPhongs.Remove(vp);
                }
            }

            HanhTrinhModelToHanhTrinh(model, item);

            if (_hanhtrinhService.UpdateHanhTrinh(item))
            {
                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("HanhTrinhSua", item.Id);
                }
                return RedirectToAction("HanhTrinhList");

            }
            ErrorNotification(GetLabel("HanhTrinh.trungmahanhtrinh"));
            //}


            return View(model);
        }
        [HttpPost]
        public ActionResult HanhTrinhXoa(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetHanhTrinhById(id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("HanhTrinhList");

            _hanhtrinhService.DeleteHanhTrinh(item);

            return RedirectToAction("HanhTrinhList");
        }
        [NonAction]
        protected virtual void HanhTrinhModelToHanhTrinh(HanhTrinhModel nvfrom, HanhTrinh nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.MaHanhTrinh = nvfrom.MaHanhTrinh;
            nvto.MoTa = nvfrom.MoTa;
            nvto.TongKhoangCach = nvfrom.TongKhoangCach;
            nvto.TuyenHanhTrinhId = nvfrom.TuyenId;

        }
        [NonAction]
        protected virtual void HanhTrinhToHanhTrinhModel(HanhTrinh nvfrom, HanhTrinhModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.MaHanhTrinh = nvfrom.MaHanhTrinh;
            nvto.MoTa = nvfrom.MoTa;
            nvto.TongKhoangCach = nvfrom.TongKhoangCach;
            nvto.SelectedVanPhongIds = nvfrom.VanPhongs.Select(c => c.Id).ToArray();
            nvto.TuyenId = nvfrom.TuyenHanhTrinhId.GetValueOrDefault(0);

        }
        [NonAction]
        protected virtual void HanhTrinhModelPrepare(HanhTrinhModel model)
        {
            var alldiemdons = _hanhtrinhService.GetAllDiemDonByNhaXeId(_workContext.NhaXeId);
            model.AvailableDiemDons = alldiemdons.Where(b=>b.LoaiDiemDonId !=(int)ENLoaiDiemDon.DiemChot).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.TenDiemDon;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            model.AvailableDiemChots = alldiemdons.Where(b => b.LoaiDiemDonId == (int)ENLoaiDiemDon.DiemChot).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.TenDiemDon;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            var items = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(model.Id);
            model.DiemDons = items.Select(c =>
            {
                var diemdonmodel = new HanhTrinhModel.HanhTrinhDiemDonModel();
                HanhTrinhDiemDonToHanhTrinhDiemDonModel(c, diemdonmodel);
                return diemdonmodel;
            }).ToList();
            var itemchots = _hanhtrinhService.GetAllHanhTrinhDiemChotByHanhTrinhId(model.Id);
            model.DiemChots = itemchots.Select(c =>
            {
                var diemdonmodel = new HanhTrinhModel.HanhTrinhDiemDonModel();
                HanhTrinhDiemDonToHanhTrinhDiemDonModel(c, diemdonmodel);
                return diemdonmodel;
            }).ToList();

            model.AllDiemDonStartEnds = alldiemdons.Where(c => c.LoaiDiemDon == ENLoaiDiemDon.BenXuatPhatKetThuc).Select(x =>
            {
                var m = new DiemDonModel();
                DiemDonToDiemDonModel(x, m);
                return m;
            }).ToList();
            model.AllDiemDonMids = alldiemdons.Where(c => c.LoaiDiemDon == ENLoaiDiemDon.GiuaHanhTrinh).Select(x =>
            {
                var m = new DiemDonModel();
                DiemDonToDiemDonModel(x, m);
                return m;
            }).ToList();

            model.AllVanPhongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var vp = new VanPhongModel();
                vp.Id = c.Id;
                vp.TenVanPhong = c.TenVanPhong;
                vp.Ma = c.Ma;
                return vp;
            }).ToList();
            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            model.ListTuyens = tuyens.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.Ten;
                item.Value = c.Id.ToString();
                item.Selected = c.Id == model.TuyenId;
                return item;
            }).ToList();
           
        }
        [HttpPost]
        public ActionResult UpdateGiaVeHanhTrinhChang(HanhTrinhModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();
            var lichtrinh = _hanhtrinhService.GetLichTrinhById(model.Id);
            //tang;i;j;val|tang;i;j;val
            string[] arrGiaVe = model.GiaVeResult.Split('|');
            foreach (string s in arrGiaVe)
            {
                if (String.IsNullOrEmpty(s.Trim()))
                    continue;

                string[] arritem = s.Split(';');
                var fromid = Convert.ToInt32(arritem[0]);
                var toid = Convert.ToInt32(arritem[1]);
                var hanhtrinhgiaveid = Convert.ToInt32(arritem[2]);
                var loaixeid = Convert.ToInt32(arritem[3]);
                var giave = Convert.ToDecimal(arritem[4]);
                // kiem tra gia ve hanh trinh da ton tai
                if (hanhtrinhgiaveid > 0)
                {
                    var hanhtrinhgiave = _hanhtrinhService.GetHanhTrinhGiaVeId(hanhtrinhgiaveid);
                    // gia  ve =0-> delete, >0 thi update


                    if (giave > 0)
                    {
                        //update hanhtrinhgiave                                   
                        hanhtrinhgiave.GiaVe = giave;
                        _hanhtrinhService.UpdateHanhTrinhGiaVe(hanhtrinhgiave);
                    }
                    else
                    {
                        //chuyen ve trang thai xoa
                        hanhtrinhgiave.IsDelete = true;
                        _hanhtrinhService.UpdateHanhTrinhGiaVe(hanhtrinhgiave);
                    }
                }
                else
                {
                    // chua co ma gia ve >0=> insert hanh trinh gia ve
                    if (giave > 0)
                    {
                        var hanhtrinhgiave = new HanhTrinhGiaVe();
                        hanhtrinhgiave.DiemDonId = fromid;
                        hanhtrinhgiave.DiemDenId = toid;
                        hanhtrinhgiave.LoaiXeId = loaixeid;
                        hanhtrinhgiave.GiaVe = giave;
                        hanhtrinhgiave.HanhTrinhId = model.Id;
                        hanhtrinhgiave.NhaXeId = _workContext.NhaXeId;
                        _hanhtrinhService.InsertHanhTrinhGiaVe(hanhtrinhgiave);
                    }


                }

            }

            return Json("ok");
        }
        [NonAction]
        protected virtual void HanhTrinhDiemDonModelToHanhTrinhDiemDon(HanhTrinhModel.HanhTrinhDiemDonModel nvfrom, HanhTrinhDiemDon nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.HanhTrinhId = nvfrom.HanhTrinhId;
            nvto.DiemDonId = nvfrom.DiemDonId;
            nvto.ThuTu = nvfrom.ThuTu;
            nvto.KhoangCach = nvfrom.KhoangCach;
        }
        [NonAction]
        protected virtual void HanhTrinhDiemDonModelToHanhTrinhDiemChot(HanhTrinhModel.HanhTrinhDiemDonModel nvfrom, HanhTrinhDiemChot nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.HanhTrinhId = nvfrom.HanhTrinhId;
            nvto.DiemDonId = nvfrom.DiemDonId;
            nvto.ThuTu = nvfrom.ThuTu;
            nvto.KhoangCach = nvfrom.KhoangCach;
        }
        [NonAction]
        protected virtual void HanhTrinhDiemDonToHanhTrinhDiemDonModel(HanhTrinhDiemDon nvfrom, HanhTrinhModel.HanhTrinhDiemDonModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.HanhTrinhId = nvfrom.HanhTrinhId;
            nvto.DiemDonId = nvfrom.DiemDonId;
            nvto.DiemDonText = _hanhtrinhService.GetDiemDonById(nvto.DiemDonId).TenDiemDon;
            nvto.ThuTu = nvfrom.ThuTu;
            nvto.KhoangCach = nvfrom.KhoangCach;

        }
        [NonAction]
        protected virtual void HanhTrinhDiemDonToHanhTrinhDiemDonModel(HanhTrinhDiemChot nvfrom, HanhTrinhModel.HanhTrinhDiemDonModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.HanhTrinhId = nvfrom.HanhTrinhId;
            nvto.DiemDonId = nvfrom.DiemDonId;
            nvto.DiemDonText = _hanhtrinhService.GetDiemDonById(nvto.DiemDonId).TenDiemDon;
            nvto.ThuTu = nvfrom.ThuTu;
            nvto.KhoangCach = nvfrom.KhoangCach;

        }
        #endregion

        #region Hanh trinh diem don

        [HttpPost]
        public ActionResult HanhTrinhDiemDonList(DataSourceRequest command, int HanhTrinhId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();


            var items = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(HanhTrinhId);
            var diemdons = items
                .Select(c =>
                {
                    var item = new HanhTrinhModel.HanhTrinhDiemDonModel();
                    HanhTrinhDiemDonToHanhTrinhDiemDonModel(c, item);
                    return item;
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = diemdons,
                Total = diemdons.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult HanhTrinhDiemDonInsert(HanhTrinhModel.HanhTrinhDiemDonModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = new HanhTrinhDiemDon();
            if (model.Id > 0)
                item = _hanhtrinhService.GetHanhTrinhDiemDonById(model.Id);
            HanhTrinhDiemDonModelToHanhTrinhDiemDon(model, item);
            if (item.Id == 0)
            {
                if (_hanhtrinhService.InsertHanhTrinhDiemDon(item))
                    return new NullJsonResult();
            }
            else
            {
                if (_hanhtrinhService.UpdateHanhTrinhDiemDon(item))
                    return new NullJsonResult();
            }

            return this.Json(new DataSourceResult
            {
                Errors = GetLabel("HanhTrinh.DiemDon.TonTai")
            });
        }

        [HttpPost]
        public ActionResult HanhTrinhDiemDonUpdate(HanhTrinhModel.HanhTrinhDiemDonModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetHanhTrinhDiemDonById(model.Id);
            if (item == null)
                throw new ArgumentException("No HanhTrinhDiemDon mapping found with the specified id");
            HanhTrinhDiemDonModelToHanhTrinhDiemDon(model, item);
            if (_hanhtrinhService.UpdateHanhTrinhDiemDon(item))
                return new NullJsonResult();
            return this.Json(new DataSourceResult
            {
                Errors = GetLabel("HanhTrinh.DiemDon.TonTai")
            });

        }

        [HttpPost]
        public ActionResult HanhTrinhDiemDonDelete(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetHanhTrinhDiemDonById(id);
            if (item == null)
                throw new ArgumentException("No HanhTrinhDiemDon mapping found with the specified id");
            _hanhtrinhService.DeleteHanhTrinhDiemDon(item);
            return new NullJsonResult();
        }
        public ActionResult HanhTrinhLineView(int id, int w_of_l)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();
            var item = _hanhtrinhService.GetHanhTrinhById(id);
            //No manufacturer found with the specified id
            if (item == null)
                return RedirectToAction("HanhTrinhList");
            var model = new HanhTrinhModel();
            HanhTrinhToHanhTrinhModel(item, model);
            //default values           
            HanhTrinhModelPrepare(model);
            model.WIDTH_OF_LINE = w_of_l;
            return PartialView(model);
        }
        #endregion
        #region Hanh trinh diem chot

        [HttpPost]
        public ActionResult HanhTrinhDiemChotList(DataSourceRequest command, int HanhTrinhId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();


            var items = _hanhtrinhService.GetAllHanhTrinhDiemChotByHanhTrinhId(HanhTrinhId);
            var DiemChots = items
                .Select(c =>
                {
                    var item = new HanhTrinhModel.HanhTrinhDiemDonModel();
                    HanhTrinhDiemDonToHanhTrinhDiemDonModel(c, item);
                    return item;
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = DiemChots,
                Total = DiemChots.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult HanhTrinhDiemChotInsert(HanhTrinhModel.HanhTrinhDiemDonModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = new HanhTrinhDiemChot();
            if (model.Id > 0)
                item = _hanhtrinhService.GetHanhTrinhDiemChotById(model.Id);
            HanhTrinhDiemDonModelToHanhTrinhDiemChot(model, item);
            if (item.Id == 0)
            {
                if (_hanhtrinhService.InsertHanhTrinhDiemChot(item))
                    return new NullJsonResult();
            }
            else
            {
                if (_hanhtrinhService.UpdateHanhTrinhDiemChot(item))
                    return new NullJsonResult();
            }

            return this.Json(new DataSourceResult
            {
                Errors = GetLabel("HanhTrinh.DiemChot.TonTai")
            });
        }

        [HttpPost]
        public ActionResult HanhTrinhDiemChotUpdate(HanhTrinhModel.HanhTrinhDiemDonModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetHanhTrinhDiemChotById(model.Id);
            if (item == null)
                throw new ArgumentException("No HanhTrinhDiemChot mapping found with the specified id");
            HanhTrinhDiemDonModelToHanhTrinhDiemChot(model, item);
            if (_hanhtrinhService.UpdateHanhTrinhDiemChot(item))
                return new NullJsonResult();
            return this.Json(new DataSourceResult
            {
                Errors = GetLabel("HanhTrinh.DiemChot.TonTai")
            });

        }

        [HttpPost]
        public ActionResult HanhTrinhDiemChotDelete(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetHanhTrinhDiemChotById(id);
            if (item == null)
                throw new ArgumentException("No HanhTrinhDiemChot mapping found with the specified id");
            _hanhtrinhService.DeleteHanhTrinhDiemChot(item);
            return new NullJsonResult();
        }
        
        #endregion
        #region LichTrinh
        List<SelectListItem> PrepareHanhTrinhList(bool isAll = true, bool isChonHanhTrinh = false)
        {
            List<HanhTrinh> hanhtrinhs = new List<HanhTrinh>();
            
            if (isAll)
                hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId);
            else
            {
                var vanphongids=_workContext.CurrentNhanVien.VanPhongs.Select(c => c.Id).ToArray();
                if (vanphongids.Length == 0)
                    vanphongids = new int[] { _workContext.CurrentNhanVien.VanPhongID.GetValueOrDefault(0) };
                hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, vanphongids);
            }
                
            var ddls = hanhtrinhs.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = string.Format("{0} ({1})", c.MoTa, c.MaHanhTrinh);
                item.Value = c.Id.ToString();
                return item;
            }).ToList();

            if (isChonHanhTrinh)
                ddls.Insert(0, new SelectListItem { Text = GetLabel("LichTrinh.ChonHanhTrinh"), Value = "0" });
            return ddls;
        }
        public ActionResult LichTrinhList()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();
            var model = new LichTrinhListModel();
            model.HanhTrinhs = PrepareHanhTrinhList(true);

            return View(model);
        }
        [HttpPost]
        public ActionResult LichTrinhList(DataSourceRequest command, LichTrinhListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var items = _hanhtrinhService.GetAllLichTrinh(_workContext.NhaXeId, model.HanhTrinhId,
                command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var m = new LichTrinhModel();
                    LichTrinhToLichTrinhModel(x, m);

                    return m;
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }
        public ActionResult LichTrinhTao(int HanhTrinhId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();
            //lay thong tin hanh trinh
            var hanhtrinh = _hanhtrinhService.GetHanhTrinhById(HanhTrinhId);
            if (hanhtrinh == null)
                return RedirectToAction("LichTrinhList");
            if (hanhtrinh.NhaXeId != _workContext.NhaXeId)
                return RedirectToAction("LichTrinhList");

            var model = new LichTrinhModel();
            model.HanhTrinhId = hanhtrinh.Id;
            LichTrinhModelPrepare(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult LichTrinhTao(LichTrinhModel model, bool continueEditing, int HanhTrinhId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = new LichTrinh();
            LichTrinhModelToLichTrinh(model, item);
            item.NhaXeId = _workContext.NhaXeId;
            var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(HanhTrinhId);
            if (lichtrinhs.Count() == 0)
                item.IsLichTrinhGoc = true;
            if (_hanhtrinhService.InsertLichTrinh(item))
            {
                // them nguon ve goc cho lich trinh
                _hanhtrinhService.InsertNguonVeGoc(item);
                SuccessNotification(GetLabel("LichTrinh.themmoithanhcong"));
                return continueEditing ? RedirectToAction("LichTrinhSua", new { id = item.Id }) : RedirectToAction("LichTrinhList");
            }

            ErrorNotification(GetLabel("LichTrinh.TonTai"));
            return View(model);
        }
        public ActionResult LichTrinhSua(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetLichTrinhById(id);
            //No manufacturer found with the specified id
            if (item == null)
                return RedirectToAction("LichTrinhList");
            var model = new LichTrinhModel();
            LichTrinhToLichTrinhModel(item, model);
            //default values           
            LichTrinhModelPrepare(model);
            LichTrinhPrepareDiemDon(model);

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult LichTrinhSua(LichTrinhModel model, bool continueEditing, FormCollection form)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetLichTrinhById(model.Id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("LichTrinhList");

            LichTrinhModelToLichTrinh(model, item);
            if (_hanhtrinhService.UpdateLichTrinh(item))
            {
                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();
                    return RedirectToAction("LichTrinhSua", item.Id);
                }
                return RedirectToAction("LichTrinhList");
            }

            ErrorNotification(GetLabel("LichTrinh.TonTai"));

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateGiaVeChang(LichTrinhModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();
            var lichtrinh = _hanhtrinhService.GetLichTrinhById(model.Id);
            //tang;i;j;val|tang;i;j;val
            string[] arrquytacstr = model.NguonVeResult.Split('|');
            foreach (string s in arrquytacstr)
            {
                if (String.IsNullOrEmpty(s.Trim()))
                    continue;

                string[] arritem = s.Split(';');
                var fromid = Convert.ToInt32(arritem[0]);
                var toid = Convert.ToInt32(arritem[1]);
                var nguonveid = Convert.ToInt32(arritem[2]);
                var giave = Convert.ToDecimal(arritem[3]);
                // kiem tra nguon ve da ton tai
                if (nguonveid > 0)
                {
                    var nguonve = _hanhtrinhService.GetNguonVeXeById(nguonveid);
                    // gia nguon ve =0-> delete, >0 thi update
                    if (giave > 0)
                    {
                        //update nguonve                                   
                        nguonve.GiaVeHienTai = giave;
                        _hanhtrinhService.UpdateNguonVeXe(nguonve);
                    }
                    else
                    {
                        //xoa nguon ve, neu la nguon ve toan tuyen thi khong duoc xoa
                        _hanhtrinhService.DeletePhysicalNguonVe(nguonve);
                    }
                }
                else
                {
                    // chua co ma gia nguonve >0=> insert nguonve
                    if (giave > 0)
                    {
                        var nguonve = new NguonVeXe();
                        nguonve.DiemDonGocId = fromid;
                        nguonve.DiemDenGocId = toid;
                        nguonve.GiaVeHienTai = giave;
                        _hanhtrinhService.InsertNguonVecon(lichtrinh, nguonve);
                    }


                }

            }

            return Json("ok");
        }
        [HttpPost]
        public ActionResult KhoaLichTrinh(int LichTrinhId, bool Khoa)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetLichTrinhById(LichTrinhId);
            if (item == null)
                return Loi();
            if (Khoa)
                item.KhoaLichTrinh = true;
            else item.KhoaLichTrinh = false;
            _hanhtrinhService.UpdateLichTrinh(item);
            var nguonves = _hanhtrinhService.GetAllNguonVeXe(_workContext.NhaXeId, LichTrinhId);
            foreach (var _item in nguonves)
            {
                if (Khoa)
                {
                    _item.HienThi = false;
                    _item.ToWeb = false;
                }
                else
                {
                    _item.HienThi = true;
                    _item.ToWeb = true;
                }
                _hanhtrinhService.UpdateNguonVeXe(_item);
            }
            return ThanhCong();
        }
        [HttpPost]
        public ActionResult LichTrinhXoa(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetLichTrinhById(id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("LichTrinhList");

            _hanhtrinhService.DeleteLichTrinh(item);

            return RedirectToAction("LichTrinhList");
        }
        [NonAction]
        protected virtual void LichTrinhModelToLichTrinh(LichTrinhModel nvfrom, LichTrinh nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.MaLichTrinh = nvfrom.MaLichTrinh;
            nvto.HanhTrinhId = nvfrom.HanhTrinhId;
            nvto.LoaiXeId = nvfrom.LoaiXeId;
            nvto.ThoiGianDi = nvfrom.ThoiGianDi;
            nvto.SoGioChay = nvfrom.SoGioChay;
            nvto.ThoiGianDen = nvfrom.ThoiGianDi.AddHours(Convert.ToDouble(nvto.SoGioChay));
            nvto.TimeOpenOnline = nvfrom.TimeOpenOnline;
            nvto.TimeCloseOnline = nvfrom.TimeCloseOnline;
            nvto.GiaVeToanTuyen = nvfrom.GiaVeToanTuyen;

        }
        [NonAction]
        protected virtual void LichTrinhToLichTrinhModel(LichTrinh nvfrom, LichTrinhModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.MaLichTrinh = nvfrom.MaLichTrinh;
            nvto.HanhTrinhId = nvfrom.HanhTrinhId;
            if (nvto.HanhTrinhId > 0)
            {
                var hanhtrinh = _hanhtrinhService.GetHanhTrinhById(nvto.HanhTrinhId);
                if (hanhtrinh != null)
                    nvto.HanhTrinhText = string.Format("{0}({1})", hanhtrinh.MoTa, hanhtrinh.MaHanhTrinh);
            }

            nvto.LoaiXeId = nvfrom.LoaiXeId;
            if (nvto.LoaiXeId > 0)
                nvto.TenLoaiXe = _xeinfoService.GetById(nvto.LoaiXeId).TenLoaiXe;
            nvto.SoGioChay = nvfrom.SoGioChay;
            nvto.ThoiGianDi = nvfrom.ThoiGianDi;
            nvto.ThoiGianDen = nvfrom.ThoiGianDen;
            nvto.TimeOpenOnline = nvfrom.TimeOpenOnline;
            nvto.TimeCloseOnline = nvfrom.TimeCloseOnline;
            nvto.KhoaLichTrinh = nvfrom.KhoaLichTrinh;
            nvto.GiaVeToanTuyen = nvfrom.GiaVeToanTuyen;
            nvto.GiaVeToanTuyenText = nvto.GiaVeToanTuyen.ToTien(_priceFormatter);

        }
        [NonAction]
        protected virtual void LichTrinhModelPrepare(LichTrinhModel model)
        {
            model.LoaiXes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.TenLoaiXe;
                item.Value = c.Id.ToString();
                item.Selected = c.Id == model.LoaiXeId;
                return item;
            }).ToList();
            if (model.HanhTrinhId > 0)
            {
                model.AvailableDiemDons = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(model.HanhTrinhId).Select(c =>
                {
                    var item = new SelectListItem();
                    var diemdon = _hanhtrinhService.GetDiemDonById(c.DiemDonId);
                    item.Text = diemdon.TenDiemDon;
                    item.Value = c.Id.ToString();
                    return item;
                }).ToList();
                var hanhtrinh = _hanhtrinhService.GetHanhTrinhById(model.HanhTrinhId);
                model.HanhTrinhText = string.Format("{0}({1})", hanhtrinh.MoTa, hanhtrinh.MaHanhTrinh);
            }

        }

        [NonAction]
        protected virtual void LichTrinhPrepareDiemDon(LichTrinhModel model)
        {

            if (model.HanhTrinhId > 0)
            {
                var listdiemdon = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(model.HanhTrinhId).ToList();
                model.SoDiemDon = listdiemdon.Count();
                var hanhtrinh = _hanhtrinhService.GetHanhTrinhById(model.HanhTrinhId);
                model.HanhTrinhText = string.Format("{0}({1})", hanhtrinh.MoTa, hanhtrinh.MaHanhTrinh);
                model.NguonVes = new LichTrinhModel.NguonVeModelToArray[model.SoDiemDon + 1, model.SoDiemDon + 1];

                var arrIdDiemDon = listdiemdon.Select(c => c.DiemDonId).ToArray();
                for (int i = 0; i < model.SoDiemDon + 1; i++)
                {
                    for (int j = 0; j < model.SoDiemDon + 1; j++)
                    {
                        if (i == 0 && j > 0)
                        {
                            model.NguonVes[0, j] = new LichTrinhModel.NguonVeModelToArray();
                            model.NguonVes[0, j].ToDiemDonId = arrIdDiemDon[j - 1];
                            var diemdon = _hanhtrinhService.GetDiemDonById(arrIdDiemDon[j - 1]);
                            model.NguonVes[0, j].TenToDiemDon = diemdon.TenDiemDon;

                        }
                        if (j == 0 && i > 0)
                        {
                            model.NguonVes[i, 0] = new LichTrinhModel.NguonVeModelToArray();
                            model.NguonVes[i, 0].FromDiemDonId = arrIdDiemDon[i - 1];
                            var diemdon = _hanhtrinhService.GetDiemDonById(arrIdDiemDon[i - 1]);
                            model.NguonVes[i, 0].TenFromDiemDon = diemdon.TenDiemDon;

                        }


                        if (j >= i && i > 0 && j > 0)
                        {
                            model.NguonVes[i, j] = new LichTrinhModel.NguonVeModelToArray();
                            model.NguonVes[i, j].FromDiemDonId = arrIdDiemDon[i - 1];
                            model.NguonVes[i, j].ToDiemDonId = arrIdDiemDon[j - 1];
                            var nguonve = _hanhtrinhService.GetAllNguonVeXe(0, model.Id, 0, arrIdDiemDon[i - 1], arrIdDiemDon[j - 1]).ToList();
                            if (nguonve.Count() > 0)
                            {

                                model.NguonVes[i, j].GiaNguonVe = nguonve.First().GiaVeHienTai;
                                model.NguonVes[i, j].NguonVeXeId = nguonve.First().Id;

                            }



                        }

                    }

                }

            }


        }
        [NonAction]
        protected virtual void HanhTrinhPrepareDiemDon(HanhTrinhModel model)
        {

            if (model.Id > 0)
            {
                var listdiemdon = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(model.Id).ToList();
                model.SoDiemDon = listdiemdon.Count();
              model.Loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId);
              model.SoLoaiXe = model.Loaixes.Count();
                var hanhtrinh = _hanhtrinhService.GetHanhTrinhById(model.Id);

                model.HanhTrinhGiaVes = new HanhTrinhModel.HanhTrinhGiaVeModelToArray[model.SoDiemDon + 1, model.SoDiemDon + 1];

                var arrIdDiemDon = listdiemdon.Select(c => c.DiemDonId).ToArray();

                for (int i = 0; i < model.SoDiemDon + 1; i++)
                {
                    for (int j = 0; j < model.SoDiemDon + 1; j++)
                    {
                        if (i == 0 && j > 0)
                        {
                            model.HanhTrinhGiaVes[0, j] = new HanhTrinhModel.HanhTrinhGiaVeModelToArray();
                            model.HanhTrinhGiaVes[0, j].ToDiemDonId = arrIdDiemDon[j - 1];
                            var diemdon = _hanhtrinhService.GetDiemDonById(arrIdDiemDon[j - 1]);
                            model.HanhTrinhGiaVes[0, j].TenToDiemDon = diemdon.TenDiemDon;

                        }
                        if (j == 0 && i > 0)
                        {
                            model.HanhTrinhGiaVes[i, 0] = new HanhTrinhModel.HanhTrinhGiaVeModelToArray();
                            model.HanhTrinhGiaVes[i, 0].FromDiemDonId = arrIdDiemDon[i - 1];
                            var diemdon = _hanhtrinhService.GetDiemDonById(arrIdDiemDon[i - 1]);
                            model.HanhTrinhGiaVes[i, 0].TenFromDiemDon = diemdon.TenDiemDon;

                        }


                        if (j >= i && i > 0 && j > 0)
                        {
                             model.HanhTrinhGiaVes[i, j] = new HanhTrinhModel.HanhTrinhGiaVeModelToArray();
                                model.HanhTrinhGiaVes[i, j].FromDiemDonId = arrIdDiemDon[i - 1];
                                model.HanhTrinhGiaVes[i, j].ToDiemDonId = arrIdDiemDon[j - 1];
                                foreach (var item in model.Loaixes)
                                {
                                    var _hanhtrinhgiave = new HanhTrinhModel.HanhTrinhGiaVeModel();
                                    _hanhtrinhgiave.LoaiXeId = item.Id;
                                    _hanhtrinhgiave.MaLoaiXe = item.MaLoaiXe;
                                    var hanhtrinhgiave = _hanhtrinhService.GetallHanhTrinhGiaVe(model.Id, _workContext.NhaXeId, arrIdDiemDon[i - 1], arrIdDiemDon[j - 1], item.Id).ToList();
                                    if (hanhtrinhgiave.Count() > 0)
                                    {

                                        _hanhtrinhgiave.GiaNguonVe = hanhtrinhgiave.First().GiaVe;
                                        _hanhtrinhgiave.HanhTrinhGiaVeId = hanhtrinhgiave.First().Id;

                                    }
                                    model.HanhTrinhGiaVes[i, j].hanhtrinhgiaves.Add(_hanhtrinhgiave);
                                }
                               
                            
                            



                        }

                    }

                }

            }


        }
        [NonAction]
        protected virtual void XepLichXuatBenPrepare(NguonVeListModel model)
        {

            if (model.HanhTrinhId > 0)
            {
                var nguonvegocs = _hanhtrinhService.GetAllNguonVeXe(0, 0, model.HanhTrinhId).Where(c => c.ParentId == 0);
                var SoNguonVe = nguonvegocs.ToList().Count();
                model.SoNguonVe = SoNguonVe;
                var arrIdNguonVe = nguonvegocs.Select(c => c.Id).ToArray();
                int countday = Convert.ToInt32((model.NgayDiTo - model.NgayDiFrom).TotalDays);
                model.SoNgay = countday;
                var Days = Enumerable.Range(0, model.NgayDiTo.Subtract(model.NgayDiFrom).Days + 1)
                     .Select(d => model.NgayDiFrom.AddDays(d));
                var arrDay = Days.ToArray();
                model.XepXeTheoNgay = new NguonVeListModel.DateModel[SoNguonVe + 1, countday];
                for (int i = 0; i < SoNguonVe + 1; i++)
                {
                    for (int j = 0; j < countday; j++)
                    {
                        if (i == 0 && j > 0)
                        {
                            model.XepXeTheoNgay[0, j] = new NguonVeListModel.DateModel();
                            model.XepXeTheoNgay[0, j].NgayXuatBen = arrDay[j - 1];

                        }
                        if (j == 0 && i > 0)
                        {
                            model.XepXeTheoNgay[i, 0] = new NguonVeListModel.DateModel();
                            model.XepXeTheoNgay[i, 0].NguonVeId = arrIdNguonVe[i - 1];
                            var _nguonve = _hanhtrinhService.GetNguonVeXeById(arrIdNguonVe[i - 1]);
                            model.XepXeTheoNgay[i, 0].GioLichTrinh = string.Format("{0}", _nguonve.ThoiGianDi.ToString("HH:mm"));
                        }

                        if (i > 0 && j > 0)
                        {
                            model.XepXeTheoNgay[i, j] = new NguonVeListModel.DateModel();
                            model.XepXeTheoNgay[i, j].NgayXuatBen = arrDay[j - 1];
                            model.XepXeTheoNgay[i, j].NguonVeId = arrIdNguonVe[i - 1];
                            var xexuatben = _nhaxeService.GetHistoryXeXuatBenByNguonVeId(arrIdNguonVe[i - 1], arrDay[j - 1]);
                            if (xexuatben != null)
                            {

                                model.XepXeTheoNgay[i, j].XeVanChuyenId = xexuatben.XeVanChuyenId.GetValueOrDefault(0);
                                model.XepXeTheoNgay[i, j].BienSo = xexuatben.xevanchuyen.BienSo;

                            }
                        }

                    }

                }
            }


        }
        [NonAction]
        protected virtual void LichTrinhGiaVeModelToLichTrinhGiaVe(LichTrinhModel.LichTrinhGiaVeModel nvfrom, LichTrinhGiaVe nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.LichTrinhID = nvfrom.LichTrinhID;
            nvto.DiemDon1_Id = nvfrom.DiemDon1_Id;
            nvto.DiemDon2_Id = nvfrom.DiemDon2_Id;
            nvto.GiaVe = nvfrom.GiaVe;
        }
        [NonAction]
        protected virtual void LichTrinhGiaVeToLichTrinhGiaVeModel(LichTrinhGiaVe nvfrom, LichTrinhModel.LichTrinhGiaVeModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.LichTrinhID = nvfrom.LichTrinhID;
            nvto.DiemDon1_Id = nvfrom.DiemDon1_Id;
            nvto.DiemDon1Text = _hanhtrinhService.GetDiemDonByHanhTrinhDiemDonId(nvto.DiemDon1_Id).TenDiemDon;
            nvto.DiemDon2_Id = nvfrom.DiemDon2_Id;
            nvto.DiemDon2Text = _hanhtrinhService.GetDiemDonByHanhTrinhDiemDonId(nvto.DiemDon2_Id).TenDiemDon;
            nvto.GiaVe = nvfrom.GiaVe;
            nvto.GiaVeText = nvto.GiaVe.ToTien(_priceFormatter);
        }
        #endregion
        #region Lich Trinh Gia Ve

        [HttpPost]
        public ActionResult LichTrinhGiaVeList(DataSourceRequest command, int LichTrinhID)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();


            var items = _hanhtrinhService.GetLichTrinhGiaVeByLichTrinhId(LichTrinhID);
            var giaves = items
                .Select(c =>
                {
                    var item = new LichTrinhModel.LichTrinhGiaVeModel();
                    LichTrinhGiaVeToLichTrinhGiaVeModel(c, item);
                    return item;
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = giaves,
                Total = giaves.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult LichTrinhGiaVeInsert(LichTrinhModel.LichTrinhGiaVeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = new LichTrinhGiaVe();
            LichTrinhGiaVeModelToLichTrinhGiaVe(model, item);
            if (_hanhtrinhService.InsertLichTrinhGiaVe(item))
                return new NullJsonResult();
            return this.Json(new DataSourceResult
            {
                Errors = GetLabel("LichTrinh.GiaVe.TonTai")
            });
        }

        [HttpPost]
        public ActionResult LichTrinhGiaVeUpdate(LichTrinhModel.LichTrinhGiaVeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetLichTrinhGiaVeById(model.Id);
            if (item == null)
                throw new ArgumentException("No LichTrinhGiaVe mapping found with the specified id");
            LichTrinhGiaVeModelToLichTrinhGiaVe(model, item);
            if (_hanhtrinhService.UpdateLichTrinhGiaVe(item))
                return new NullJsonResult();
            return this.Json(new DataSourceResult
            {
                Errors = GetLabel("LichTrinh.GiaVe.TonTai")
            });

        }

        [HttpPost]
        public ActionResult LichTrinhGiaVeDelete(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetLichTrinhGiaVeById(id);
            if (item == null)
                throw new ArgumentException("No LichTrinhGiaVe mapping found with the specified id");
            _hanhtrinhService.DeleteLichTrinhGiaVe(item);
            return new NullJsonResult();
        }

        #endregion
        #region Nguon ve xe
        public ActionResult NguonVeList()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var model = new NguonVeListModel();

            model.HanhTrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = string.Format("{0} ({1})", c.MoTa, c.MaHanhTrinh);
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            model.HanhTrinhs.Insert(0, new SelectListItem { Text = GetLabel("LichTrinh.ChonHanhTrinh"), Value = "0" });
            return View(model);
        }
        [HttpPost]
        public ActionResult NguonVeList(DataSourceRequest command, NguonVeListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();


            var items = _hanhtrinhService.GetAllNguonVeXe(_workContext.NhaXeId, 0, model.HanhTrinhId);
            var nguonves = items
                .Select(c =>
                {
                    var item = new NguonVeXeModel();
                    NguonVeXeToMode(c, item);
                    item.SoGioChay = _hanhtrinhService.GetLichTrinhById(c.LichTrinhId).SoGioChay;
                    return item;
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = nguonves,
                Total = nguonves.Count
            };

            return Json(gridModel);
        }
        [HttpPost]
        public ActionResult NguonVeXeList(DataSourceRequest command, int LichTrinhID)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();


            var items = _hanhtrinhService.GetAllNguonVeXe(_workContext.NhaXeId, LichTrinhID);
            var nguonves = items
                .Select(c =>
                {
                    var item = new NguonVeXeModel();
                    NguonVeXeToMode(c, item);
                    item.SoGioChay = Convert.ToDecimal((c.ThoiGianDen - c.ThoiGianDi).TotalHours);
                    return item;
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = nguonves,
                Total = nguonves.Count
            };

            return Json(gridModel);
        }


        [HttpPost]
        public ActionResult NguonVeXeUpdate(NguonVeXeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var item = _hanhtrinhService.GetNguonVeXeById(model.Id);
            if (item == null)
                throw new ArgumentException("No NguonVeXe mapping found with the specified id");
            else
            {
                item.HienThi = model.HienThi;
                item.ToWeb = model.ToWeb;
                _hanhtrinhService.UpdateNguonVeXe(item);
            }

            return new NullJsonResult();
        }


        [NonAction]
        void NguonVeXeToMode(NguonVeXe e, NguonVeXeModel m)
        {
            m.Id = e.Id;
            m.NhaXeInfo = new NguonVeXeModel.NhaXeBasicModel();
            m.NhaXeInfo.Id = e.NhaXeId;
            m.NhaXeInfo.TenNhaXe = e.TenNhaXe;
            m.DiemDonId = e.DiemDonId;
            m.DiemDenId = e.DiemDenId;
            m.LichTrinhId = e.LichTrinhId;
            m.TimeCloseOnline = e.TimeCloseOnline;
            m.TimeOpenOnline = e.TimeOpenOnline;
            m.ThoiGianDi = e.ThoiGianDi;
            m.ThoiGianDen = e.ThoiGianDen;
            m.GiaVeMoi = e.ProductInfo.Price;
            m.GiaVeMoiText = m.GiaVeMoi.ToTien(_priceFormatter);
            m.GiaVeCu = e.ProductInfo.OldPrice;
            m.GiaVeCuText = m.GiaVeCu.ToTien(_priceFormatter);
            m.LoaiXeId = e.LoaiXeId;
            m.TenDiemDon = e.TenDiemDon;
            m.TenDiemDen = e.TenDiemDen;
            m.TenLoaiXe = e.TenLoaiXe;
            m.HienThi = e.HienThi;
            m.ToWeb = e.ToWeb;

        }
        #endregion
        #region ve xe xuat ben
        XeXuatBenItemModel PrepareXeXuatBenModel(int NguonVeXeId, DateTime ngaydi)
        {
            var historyxexuatben = _nhaxeService.GetHistoryXeXuatBenByNguonVeId(NguonVeXeId, ngaydi);
            if (historyxexuatben != null)
            {
                return historyxexuatben.toModel(_localizationService);
            }
            var model = new XeXuatBenItemModel();
            var _nguonve = _hanhtrinhService.GetNguonVeXeById(NguonVeXeId);
            model.NguonVeId = _nguonve.Id;
            model.TuyenXeChay = _nguonve.GetHanhTrinh();
            model.HanhTrinhId = _nguonve.LichTrinhInfo.HanhTrinhId;
            model.NgayDi = ngaydi;
            model.GioDi = _nguonve.ThoiGianDi.ToString("HH:mm");
            model.GioDen = _nguonve.ThoiGianDen.ToString("HH:mm");
            return model;
        }
      
        public ActionResult PrinfPhoiVe(int id, string NgayXeXuatBen)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            return View(PrepareXeXuatBenModel(id, Convert.ToDateTime(NgayXeXuatBen)));
        }
        public ActionResult PrinfHangHoaTheoXe(int Id)
        {          
            return View();
        }

        public ActionResult AddLaiXeHoacPhuXe(int Id, string NgayDi)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new XeXuatBenItemModel();
            model.NguonVeId = Id;
            model.NgayDi = Convert.ToDateTime(NgayDi);
            var xexuatben = _nhaxeService.GetHistoryXeXuatBenByNguonVeId(Id, model.NgayDi);
            if (xexuatben != null)
            {
                model = xexuatben.toModel(_localizationService);
            }

            model.tatcalaivaphuxes = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, new ENKieuNhanVien[] { ENKieuNhanVien.LaiXe, ENKieuNhanVien.PhuXe }).Select(c =>
            {
                return new XeXuatBenItemModel.NhanVienLaiPhuXe(c.Id, c.ThongTin(false));
            }).ToList();

            return View(model);
        }
        public ActionResult AddNhatKyXuatBen(int Id)
        {
            var model = new XeXuatBenModel();
            var item = _nhaxeService.GetHistoryXeXuatBenId(Id);
            if (item != null)
                model.GhiChu = item.GhiChu;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddNhatKyXuatBen(XeXuatBenModel model)
        {
            var item = _nhaxeService.GetHistoryXeXuatBenId(model.Id);
            if (item != null)
            {
                item.GhiChu = model.GhiChu;
                _nhaxeService.UpdateHistoryXeXuatBen(item);
                return ThanhCong();
            }
            return Loi();
        }
        
        [HttpPost]
        public ActionResult AddLaiXeHoacPhuXe(string laiphuxeids, int XeVanChuyenId, string NgayXuatBen, int NguonVeId, int TrangThaiId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var _ngayxuatben = Convert.ToDateTime(NgayXuatBen);
            var historyxexuatben = _nhaxeService.GetHistoryXeXuatBenByNguonVeId(NguonVeId, _ngayxuatben);
            if (historyxexuatben != null)
            {
                // xóa hết các lái xe cũ
                historyxexuatben.LaiPhuXes.Clear();
                _nhaxeService.DeleteHistoryXeXuatBenNhanVien(historyxexuatben.Id);
                //up date lai xe mơi               
                int[] idlaiphuxes = Array.ConvertAll(laiphuxeids.Split(','), s => int.Parse(s));
                for (int i = 0; i < idlaiphuxes.Length; i++)
                {
                    var nhanvien = _nhanvienService.GetById(idlaiphuxes[i]);
                    if (nhanvien != null)
                    {
                        var _nhanvienxuatben = new HistoryXeXuatBen_NhanVien();
                        _nhanvienxuatben.NhanVien_Id = nhanvien.Id;
                        _nhanvienxuatben.HistoryXeXuatBen_Id = historyxexuatben.Id;
                        if (i == 0)
                            _nhanvienxuatben.KieuNhanVien = ENKieuNhanVien.LaiXe;
                        else
                            _nhanvienxuatben.KieuNhanVien = ENKieuNhanVien.PhuXe;
                        historyxexuatben.LaiPhuXes.Add(_nhanvienxuatben);
                    }

                }
                historyxexuatben.XeVanChuyenId = XeVanChuyenId;
                _nhaxeService.UpdateHistoryXeXuatBen(historyxexuatben);
            }
            else
            {
                historyxexuatben = new HistoryXeXuatBen();
                historyxexuatben.NguonVeId = NguonVeId;
                historyxexuatben.NhaXeId = _workContext.NhaXeId;
                int[] idlaiphuxes = Array.ConvertAll(laiphuxeids.Split(','), s => int.Parse(s));
                for (int i = 0; i < idlaiphuxes.Length; i++)
                {
                    var nhanvien = _nhanvienService.GetById(idlaiphuxes[i]);
                    if (nhanvien != null)
                    {
                        var _nhanvienxuatben = new HistoryXeXuatBen_NhanVien();
                        _nhanvienxuatben.NhanVien_Id = nhanvien.Id;
                        _nhanvienxuatben.HistoryXeXuatBen_Id = historyxexuatben.Id;
                        if (i == 0)
                            _nhanvienxuatben.KieuNhanVien = ENKieuNhanVien.LaiXe;
                        else
                            _nhanvienxuatben.KieuNhanVien = ENKieuNhanVien.PhuXe;
                        historyxexuatben.LaiPhuXes.Add(_nhanvienxuatben);
                    }
                }
                historyxexuatben.XeVanChuyenId = XeVanChuyenId;
                historyxexuatben.NgayDi = _ngayxuatben;
                historyxexuatben.NgayTao = DateTime.Now;
                historyxexuatben.TrangThaiId = TrangThaiId;
                historyxexuatben.NguoiTaoId = _workContext.CurrentNhanVien.Id;
                var nguonve = _hanhtrinhService.GetNguonVeXeById(NguonVeId);
                historyxexuatben.NgayDi = historyxexuatben.NgayDi.AddHours(nguonve.ThoiGianDi.Hour).AddMinutes(nguonve.ThoiGianDi.Minute);
                historyxexuatben.HanhTrinhId = nguonve.LichTrinhInfo.HanhTrinhId;
                _nhaxeService.InsertHistoryXeXuatBen(historyxexuatben);

            }
            //lay lai thong tin
            historyxexuatben = _nhaxeService.GetHistoryXeXuatBenId(historyxexuatben.Id);
            return Json(historyxexuatben.toModel(_localizationService));

        }
        public ActionResult XeXuatBenInfo()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();

            var model = new NguonVeListModel();
            model.NgayDi = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public ActionResult XeXuatBenInfo(DataSourceRequest command, NguonVeListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();


            var items = _hanhtrinhService.GetAllNguonVeXe(_workContext.NhaXeId, 0, model.HanhTrinhId).Where(c => c.ParentId == 0);
            var nguonves = items
                .Select(c =>
                {
                    return PrepareXeXuatBenModel(c.Id, model.NgayDi);
                }).ToList();

            var gridModel = new DataSourceResult
            {
                Data = nguonves,
                Total = nguonves.Count
            };

            return Json(gridModel);
        }
        public ActionResult BienSoList(string BienSoText)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return AccessDeniedView();

            var items = _nhaxeService.GetAllBienSoXeByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.BienSo;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult XepLichXeXuatBen(int hanhtrinhid = 0, string ngaydifrom = "", string ngaydito = "")
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new NguonVeListModel();
            var _listhanhtrinh = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId);
            model.HanhTrinhs = _listhanhtrinh.Select(c =>
             {
                 var item = new SelectListItem();
                 item.Text = string.Format("{0} ({1})", c.MoTa, c.MaHanhTrinh);
                 item.Value = c.Id.ToString();
                 return item;
             }).ToList();
            model.BienSoXes = _nhaxeService.GetAllBienSoXeByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.BienSo;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            var _item = new SelectListItem();
            _item.Text = "Chọn biển số";
            _item.Value = "0";
            if (model.HanhTrinhId == 0 && ngaydifrom == "" && ngaydito == "")
            {
                model.NgayDiFrom = DateTime.Now;
                model.NgayDiTo = DateTime.Now.AddDays(7);
                model.BienSoXes.Insert(0, _item);
                if (_listhanhtrinh.Count() > 0)
                {

                    model.HanhTrinhId = _listhanhtrinh.First().Id;
                }

            }
            else
            {
                model.NgayDiFrom = Convert.ToDateTime(ngaydifrom);
                model.NgayDiTo = Convert.ToDateTime(ngaydito);
                model.HanhTrinhId = hanhtrinhid;

            }
            if (model.NgayDiFrom.AddDays(1) < model.NgayDiTo && model.NgayDiFrom.AddDays(15) > model.NgayDiTo)
            {
                XepLichXuatBenPrepare(model);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult XepLichXeXuatBen(NguonVeListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            string[] arrquytacstr = model.XeXuatBenResult.Split('|');
            //ngayxuatben;nguonveid,xevanchuyenid
            foreach (string s in arrquytacstr)
            {
                if (String.IsNullOrEmpty(s.Trim()))
                    continue;

                string[] arritem = s.Split(';');
                string ngayxb = arritem[0].Insert(2, "/");
                string ngayxb2 = ngayxb.Insert(5, "/");
                var ngayxuatben = Convert.ToDateTime(ngayxb2);
                var nguonveid = Convert.ToInt32(arritem[1]);
                var xevanchuyen = _xeinfoService.GetXeInfoByBienSo(_workContext.NhaXeId, arritem[2]);

                if (xevanchuyen != null)
                {
                    //kiem tra neu ton tai thi chinh sua, nguoc lai thi chen moi
                    var xexuatben = _nhaxeService.GetHistoryXeXuatBenByNguonVeId(nguonveid, ngayxuatben);
                    if (xexuatben != null)
                    {
                        xexuatben.XeVanChuyenId = xevanchuyen.Id;
                        _nhaxeService.UpdateHistoryXeXuatBen(xexuatben);
                    }
                    else
                    {
                        var _xexuatben = new HistoryXeXuatBen();
                        _xexuatben.NhaXeId = _workContext.NhaXeId;
                        _xexuatben.XeVanChuyenId = xevanchuyen.Id;
                        _xexuatben.TrangThaiId = (int)ENTrangThaiXeXuatBen.CHO_XUAT_BEN;
                        _xexuatben.NgayDi = ngayxuatben;
                        _xexuatben.NguoiTaoId = _workContext.CurrentNhanVien.Id;
                        _xexuatben.NguonVeId = nguonveid;
                        _xexuatben.NgayTao = DateTime.Now;
                        _nhaxeService.InsertHistoryXeXuatBen(_xexuatben);
                    }
                }
            }
            return Json("ok");
        }


        public ActionResult PhuTrachChuyen()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new NguonVeListModel();

            model.HanhTrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = string.Format("{0} ({1})", c.MoTa, c.MaHanhTrinh);
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            model.HanhTrinhs.Insert(0, new SelectListItem { Text = GetLabel("LichTrinh.ChonHanhTrinh"), Value = "0" });
            return View(model);
        }
        [HttpPost]
        public ActionResult PhuTrachChuyen(DataSourceRequest command, NguonVeListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();


            var items = _hanhtrinhService.GetAllNguonVeXe(_workContext.NhaXeId, 0, model.HanhTrinhId).Where(c => !c.isDelete && c.ParentId == 0);
            var nguonves = items
                .Select(c =>
                {
                    var item = new NguonVeXeModel();
                    NguonVeXeToMode(c, item);
                    var phutrachchuyens = _nhaxecustomerService.GetAllPhuTrachChuyenByNguonVeId(c.Id);
                    switch (phutrachchuyens.Count)
                    {
                        case 1:
                            item.PhuTrachChuyen1.HoTen = string.Format("{0}-({1})", phutrachchuyens[0].PhuTrachChuyen.HoVaTen, phutrachchuyens[0].PhuTrachChuyen.CMT_Id);
                            break;
                        case 2:
                            item.PhuTrachChuyen1.HoTen = string.Format("{0}-({1})", phutrachchuyens[1].PhuTrachChuyen.HoVaTen, phutrachchuyens[1].PhuTrachChuyen.CMT_Id);
                            item.PhuTrachChuyen2.HoTen = string.Format("{0}-({1})", phutrachchuyens[1].PhuTrachChuyen.HoVaTen, phutrachchuyens[1].PhuTrachChuyen.CMT_Id);
                            break;
                        case 3:
                            item.PhuTrachChuyen1.HoTen = string.Format("{0}-({1})", phutrachchuyens[2].PhuTrachChuyen.HoVaTen, phutrachchuyens[2].PhuTrachChuyen.CMT_Id);
                            item.PhuTrachChuyen2.HoTen = string.Format("{0}-({1})", phutrachchuyens[2].PhuTrachChuyen.HoVaTen, phutrachchuyens[2].PhuTrachChuyen.CMT_Id);
                            item.PhuTrachChuyen3.HoTen = string.Format("{0}-({1})", phutrachchuyens[2].PhuTrachChuyen.HoVaTen, phutrachchuyens[2].PhuTrachChuyen.CMT_Id);
                            break;
                    }


                    return item;
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = nguonves,
                Total = nguonves.Count
            };

            return Json(gridModel);
        }
        public ActionResult _PhuTrachChuyen(int Id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new XeXuatBenModel();
            model.NguonVeId = Id;
            model.LaiXePhuXes = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, new ENKieuNhanVien[] { ENKieuNhanVien.LaiXe, ENKieuNhanVien.PhuXe }).Select(c =>
            {
                var item = new XeXuatBenModel.LaiXePhuXeModel();
                item.TenLaiXe = string.Format("{0} ({1})", c.HoVaTen, c.CMT_Id);
                item.Id = c.Id;
                var phutrachchuyen = _nhaxecustomerService.GetPhuTrachChuyenByNhanVienId(c.Id, model.NguonVeId);
                if (phutrachchuyen != null)
                {
                    item.LaiXeCheckbox = true;
                }
                return item;
            }).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _PhuTrachChuyen(DataSourceRequest command, XeXuatBenModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            //xoa het tat ca lai xe ton tai truc do
            _nhaxecustomerService.DeletePhuTrachChuyen(model.NguonVeId);
            //inset nhung lai xe moi vao trung gian
            foreach (var item in model.LaiXePhuXes)
            {
                if (item.LaiXeCheckbox == true)
                {
                    var _item = new NhanVienPhuTrachChuyen();
                    _item.NguonVeXeID = model.NguonVeId;
                    _item.NhanVienID = item.Id;
                    _nhaxecustomerService.InsertPhuTrachChuyen(_item);
                }

            }
            return RedirectToAction("PhuTrachChuyen");
        }
        #endregion
        #region gán seri
        public ActionResult _GanSoSeri(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var _phoiveitem = _phoiveService.GetPhoiVeById(Id);
            var model = new QuanlyPhoiVeModel.KhachHangDatMuaVeModel();
            model.Id = _phoiveitem.Id;
            model.MaVe = "";            
            model.QuayBanVeId = 0;
            model.MauVeKyHieuId = 0;
            if(_phoiveitem.VeXeItemId.HasValue)
            {
                var _vexeitem = _giaodichkeveService.GetVeXeItemById(_phoiveitem.VeXeItemId.Value);
                if(_vexeitem!=null)
                {
                    model.QuayBanVeId = _vexeitem.VanPhongId.Value;
                    model.MauVeKyHieuId = _vexeitem.MauVeKyHieuId;
                    model.MaVe = _vexeitem.SoSeri;
                }
            }
            model.quaybanves = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId).Select(c=>new SelectListItem{
                Value=c.Id.ToString(),
                Text=string.Format("{0}({1})", c.TenVanPhong,c.Ma),
                Selected=c.Id==model.QuayBanVeId
            }).ToList();
            model.maukyhieus = _giaodichkeveService.GetAllMauVeKyHieu(_workContext.NhaXeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = string.Format("{0}-{1}", c.MauVe, c.KyHieu),
                Selected = c.Id == model.MauVeKyHieuId
            }).ToList();
            return PartialView(model);
        }
         [HttpPost]
        public ActionResult GanSeriVe(int Id,int QuayBanVeId,int MauVeKyHieuId, string MaVe)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            if (_giaodichkeveService.GanSoSerial(Id, _workContext.CurrentNhanVien.Id, QuayBanVeId, MauVeKyHieuId, MaVe))
                return ThanhCong();
            return Loi();
        }
        

        #endregion
        #region quản lý hủy vé
        public ActionResult ListVeYeuCauHuy()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLHuy))
                return AccessDeniedView();

            var model = new PhoiVeModel();
            model.NgayDisearch = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public ActionResult ListVeYeuCauHuy(DataSourceRequest command, PhoiVeModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLHuy))
                return AccessDeniedView();

            var array = _phoiveService.GetPhoiVeYeuCauHuy(_workContext.CurrentVanPhong.Id);
            var phoives = array.Select(c =>
            {
                var phoivemodel = new PhoiVeModel();
                PhoiVeToModel(c, phoivemodel);
                return phoivemodel;
            }).ToList();

            var gridModel = new DataSourceResult
            {
                Data = phoives,
                Total = phoives.Count
            };

            return Json(gridModel);


        }
        [HttpPost]
        public ActionResult YeuCauHuyVe(int PhoiVeId, string LyDoHuy)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLHuy))
                return AccessDeniedView();
            var item = _phoiveService.GetPhoiVeById(PhoiVeId);
            if (item != null)
            {
                item.GhiChu = item.GhiChu + LyDoHuy;
                item.IsRequireCancel = true;
                _phoiveService.UpdatePhoiVe(item);
                return ThanhCong();
            }
            return Loi();
        }
       

        #endregion
        #region Quan ly thong tin khach hang nha xe
        public ActionResult CustomersNhaXe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new CustomerNhaXeModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CustomersNhaXe(DataSourceRequest command, ListCustomerNhaXeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var items = _nhaxecustomerService.GetAllCustomer(_workContext.NhaXeId, model.HoTen,
                command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var m = new CustomerNhaXeModel();
                    NhaXecusEntityToModel(x, m);
                    return m;
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }
        [NonAction]
        protected virtual void NhaXecusEntityToModel(NhaXeCustomer ent, CustomerNhaXeModel model)
        {
            model.Id = ent.Id;
            model.NhaXeId = ent.NhaXeId;
            if (ent.nhaxe != null)
                model.TenNhaXe = ent.nhaxe.TenNhaXe;
            model.HoTen = ent.HoTen;
            model.DienThoai = ent.DienThoai;
            model.SearchInfo = ent.SearchInfo;
            model.DiaChiLienHe = ent.DiaChiLienHe;
        }

        #endregion
        #region Chot khach
        ChotKhachModel toChotKhachModel(ChotKhach entity)
        {
            var model = new ChotKhachModel();
            model.Id = entity.Id;
            model.Ma = entity.Ma;
            model.NgayChot = entity.NgayChot;
            model.NguoiChotId = entity.NguoiChotId;
            model.nguoichot = string.Format("{0}({1})", entity.nguoichot.HoVaTen, entity.nguoichot.CMT_Id);
            model.DiemDonId = entity.DiemDonId;
            model.diemchot = entity.diemchot.TenDiemDon;
            model.ViTriChot = entity.ViTriChot;
            model.HistoryXeXuatBenId = entity.HistoryXeXuatBenId;
            model.historychuyenxe = string.Format("{0}-{1} ({2})", entity.historychuyenxe.NguonVeInfo.DiemDon.Ten, entity.historychuyenxe.NguonVeInfo.DiemDen.Ten, entity.historychuyenxe.xevanchuyen.BienSo);
            model.SoLuongPhanMem = entity.SoLuongPhanMem;
            model.SoLuongThucTe = entity.SoLuongThucTe;
            model.ViTriChot = entity.ViTriChot;
            model.Latitude = entity.Latitude;
            model.Longitude = entity.Longitude;
            return model;
        }
        public ActionResult ChotKhachList()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new ChotKhachListModel();
            var diemchots = _hanhtrinhService.GetAllDiemDonByNhaXeId(_workContext.NhaXeId, new ENLoaiDiemDon[] { ENLoaiDiemDon.DiemChot });
            var ddls = diemchots.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.TenDiemDon;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            ddls.Insert(0, new SelectListItem
            {
                Text = "----Chọn điểm chốt----",
                Value = "0",
                Selected = true
            });
            model.diemchots = ddls;
            return View(model);

        }
        [HttpPost]
        public ActionResult ChotKhachList(ChotKhachListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return null;
            var giaodichs = _nhaxeService.GetChotKhachs(_workContext.NhaXeId, model.MaGiaoDich, 0, model.NguoiChotId, model.DiemChotId, model.TuNgay, model.DenNgay);
            var models = giaodichs.Select(c =>
            {
                return toChotKhachModel(c);
            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = models,
                Total = models.Count
            };
            return Json(gridModel);
        }
        public ActionResult ChotKhachPhuTrach()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChotKhachPhuTrach(DataSourceRequest command)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var nhanviens = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, null);
            var diemchots = _hanhtrinhService.GetAllDiemDonByNhaXeId(_workContext.NhaXeId, new ENLoaiDiemDon[] { ENLoaiDiemDon.DiemChot });
            var diemchotmodels = diemchots.Select(c =>
            {
                var model = new DiemChotKhachModel();
                model.Id = c.Id;
                model.TenDiemDon = c.TenDiemDon;
                model.DiaChiId = c.DiaChiId;
                model.DiaChiText = _diachiService.GetById(c.DiaChiId).ToText();
                model.ThongTinThanhTra = "";
                var nhanvientheochot = nhanviens.Where(n => n.DiemDons.Any(d=>d.Id == c.Id)).ToList();
                foreach (var nv in nhanvientheochot)
                {
                    model.ThongTinThanhTra = model.ThongTinThanhTra + string.Format("{0}({1}){2}", nv.HoVaTen, nv.CMT_Id, "<br />");
                }
                return model;
            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = diemchotmodels,
                Total = diemchotmodels.Count
            };

            return Json(gridModel);
        }
        public ActionResult CBBNhanVienCho(string SearchKhachhang)
        {
            var khachhangs = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, new ENKieuNhanVien[] { ENKieuNhanVien.ChotXe}).Select(c =>
            {
                var item = new CustomerNhaXeModel();
                item.Id = c.Id;
                item.HoTen = c.ThongTin(false);
                return item;
            }).ToList();

            return Json(khachhangs, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _ChotKhachPhuTrach(int Id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new DiemChotKhachModel();
            model.Id = Id;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult ChotKhachNhanVienList(DataSourceRequest command, int DiemChotId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();

            var nhanviens = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, null).Where(n => n.DiemDons.Any(c=>c.Id == DiemChotId)).ToList();
            var nhanvienmodels = nhanviens.Select(c =>
            {
                var model = new NhanVienModel();
                NhanVienToNhanVienModel(c, model);
                return model;

            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = nhanvienmodels,
                Total = nhanvienmodels.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult ChotKhachNhanVienUpdate(int NhanVienId, int DiemChotId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var nhanvien = _nhanvienService.GetById(NhanVienId);
            if (nhanvien == null)
                throw new ArgumentException("No nhan vien ");
            var _diemdon = _hanhtrinhService.GetDiemDonById(DiemChotId);
            if(_diemdon!=null)
            {
                if(!nhanvien.DiemDons.Any(c=>c.Id==DiemChotId))
                {
                    nhanvien.DiemDons.Add(_diemdon);
                    _nhanvienService.Update(nhanvien);
                }
            }            

            return new NullJsonResult();
        }
        [HttpPost]
        public ActionResult ChotKhachNhanVienXoa(int NhanVienId, int DiemChotId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var nhanvien = _nhanvienService.GetById(NhanVienId);
            if (nhanvien == null)
                throw new ArgumentException("No nhan vien ");
            var _diemdon = nhanvien.DiemDons.Where(c=>c.Id==DiemChotId).FirstOrDefault();
            if (_diemdon != null)
            {
                nhanvien.DiemDons.Remove(_diemdon);
                _nhanvienService.Update(nhanvien);
            }

            return new NullJsonResult();
        }
        #endregion
        #region xe xuat ben new

        public ActionResult ListXeXuatBen()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();

            var model = new XeXuatBenInforModel();
            model.NgayDi = DateTime.Now;

            model.HanhTrinhs = PrepareHanhTrinhList(false);
            model.KhungGioId = (int)CommonHelper.KhungGioHienTai();
            model.khunggios = this.GetCVEnumSelectList<ENKhungGio>(_localizationService, model.KhungGioId);
            //model.HanhTrinhs.Insert(0, new SelectListItem { Text = GetLabel("LichTrinh.ChonHanhTrinh"), Value = "0" });
            //lay tat ca nhan vien
            model.AllLaiXePhuXes = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, new ENKieuNhanVien[] { ENKieuNhanVien.LaiXe, ENKieuNhanVien.PhuXe }).Select(c =>
            {
                return new XeXuatBenItemModel.NhanVienLaiPhuXe(c.Id, c.ThongTin());
            }).ToList();
            var loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId);
            model.ListLoaiXes = loaixes.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.TenLoaiXe;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            if (model.ListLoaiXes.Count > 1)
                model.LoaiXeId = Convert.ToInt32(model.ListLoaiXes[1].Value);
            //lay tat ca thong tin xe
            model.AllXeInfo = _xeinfoService.GetAllXeInfoByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                return new XeXuatBenItemModel.XeVanChuyenInfo(c.Id, c.BienSo);
            }).ToList();
            return View(model);
        }
        XeXuatBenItemModel PrepareXeXuatBenModelNew(int NguonVeId, DateTime NgayDi)
        {
            var historyxexuatben = _nhaxeService.GetHistoryXeXuatBenByNguonVeId(NguonVeId, NgayDi);
            if (historyxexuatben != null)
            {
                return historyxexuatben.toModel(_localizationService);
            }
            else
            {
                var nguonve = _hanhtrinhService.GetNguonVeXeById(NguonVeId);
                var model = new XeXuatBenItemModel();
                model.NguonVeId = NguonVeId;
                model.NgayDi = NgayDi;
                model.TuyenXeChay = nguonve.GetHanhTrinh();
                model.GioDi = nguonve.ThoiGianDi.ToString("HH:mm");
                model.GioDen = nguonve.ThoiGianDen.ToString("HH:mm");
                return model;
            }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _GetListXeXuatBenInfo(XeXuatBenInforModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();

            //var khunggio = new KhungThoiGian((ENKhungGio)KhungGioId);
            DateTime _NgayDi = Convert.ToDateTime(model.NgayDi);
            model.NguonVeAll = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, _NgayDi, model.HanhTrinhId, (ENKhungGio)model.KhungGioId, "", true, model.LoaiXeId).Select(c => { return c.toModel(_localizationService); }).ToList();
            model.NguonVeTop = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, _NgayDi, model.HanhTrinhId, (ENKhungGio)model.KhungGioId, "", true, model.LoaiXeId, true).Select(c => { return c.toModel(_localizationService); }).ToList();
            return PartialView(model);

        }
        [HttpPost]
        public ActionResult CapNhatTrangThaiXeXuatBen(int Id, int TrangThaiId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var xexuatben = _nhaxeService.GetHistoryXeXuatBenId(Id);
            if (xexuatben == null)
                return Loi();
            var trangthai = (ENTrangThaiXeXuatBen)TrangThaiId;
            xexuatben.TrangThai = trangthai;
            _nhaxeService.UpdateHistoryXeXuatBen(xexuatben);

            var _log = new HistoryXeXuatBenLog();
            _log.NguoiTaoId = _workContext.CurrentNhanVien.Id;
            _log.NgayTao = DateTime.Now;
            _log.TrangThai = trangthai;
            switch (trangthai)
            {
                case ENTrangThaiXeXuatBen.DANG_DI:
                    {
                        _log.GhiChu = "Xe xuất bến, bắt đầu hành trình";
                        break;
                    }
                case ENTrangThaiXeXuatBen.KET_THUC:
                    {
                        _log.GhiChu = "Xe về bến, kết thúc hành trình";
                        break;
                    }                
                case ENTrangThaiXeXuatBen.HUY:
                    {
                        _log.GhiChu = "Hủy xe xuất bến";
                        break;
                    }
            }
            _log.XeXuatBenId = Id;
            _nhaxeService.InsertHistoryXeXuatBenLog(_log);

            return ThanhCong();
        }
        public ActionResult _NhatKyXeXuatBen(int Id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new XeXuatBenItemModel();
            model.Id = Id;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult GetNhatKyXeXuatBen(int Id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var xexuatben = _nhaxeService.GetHistoryXeXuatBenId(Id);
            var model = xexuatben.toModel(_localizationService);
            var gridModel = new DataSourceResult
            {
                Data = model.nhatkys,
                Total = model.nhatkys.Count
            };

            return Json(gridModel);
        }
        #endregion
        #region Bang Dieu chuyen
        public ActionResult BangDieuChuyen()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new DanhSachChuyenDiModel();
            model.NgayDi = DateTime.Now;
            //model.KhungGioId = (int)CommonHelper.KhungGioHienTai();
            model.khunggios = this.GetCVEnumSelectList<ENKhungGio>(_localizationService, model.KhungGioId);
            //lay tat ca nhan vien
            model.AllLaiXePhuXes = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, new ENKieuNhanVien[] { ENKieuNhanVien.LaiXe, ENKieuNhanVien.PhuXe }).Select(c =>
            {
                return new XeXuatBenItemModel.NhanVienLaiPhuXe(c.Id, c.ThongTin());
            }).ToList();
            //lay tat ca thong tin xe
            model.AllXeInfo = _xeinfoService.GetAllXeInfoByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                return new XeXuatBenItemModel.XeVanChuyenInfo(c.Id, c.BienSo);
            }).ToList();
            // loc theo loai xe
            var loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId);
            model.ListLoaiXes = loaixes.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.TenLoaiXe;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            if (model.ListLoaiXes.Count > 1)
                model.LoaiXeId = Convert.ToInt32(model.ListLoaiXes[1].Value);
            return View(model);
        }
        [HttpGet]
        public ActionResult _BangDieuChuyen(DanhSachChuyenDiModel model)
        {
            var modelnew = new BangDieuChuyenModel();
            //build thong tin bang dieu chuyen
            modelnew.NgayDi = model.NgayDi;
            //lay thong tin hanh trinh, de lang hang
            var hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId);

            KhungThoiGian khungtg = null;
            if (model.khunggio != ENKhungGio.All)
            {
                khungtg = new KhungThoiGian(model.khunggio);

            }
            //tao bang dieu chuyen
            modelnew.arrBangDieuChuyen = new List<BangDieuChuyenModel.BanDieuChuyenHanhTrinh>();
            foreach (var ht in hanhtrinhs)
            {
                var htchuyendis = new BangDieuChuyenModel.BanDieuChuyenHanhTrinh();
                htchuyendis.hanhtrinhinfo = ht;
                htchuyendis.LichTrinhItems = new List<BangDieuChuyenModel.BangDieuChuyenItem>();
                //lay thong tin lich trinh, de lam cot
                var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(ht.Id, _workContext.NhaXeId).Where(c => c.LoaiXeId == model.LoaiXeId);
                var lichtrinhids = lichtrinhs.Select(c => c.Id).ToList();
                if (khungtg != null)
                {
                    lichtrinhids = lichtrinhs.Where(c => c.ThoiGianDi.Hour >= khungtg.GioTu
                                && c.ThoiGianDi.Hour < khungtg.GioDen).Select(c => c.Id).ToList();
                }
                var nguonves = _hanhtrinhService.GetAllNguonVeXeByHanhTrinh(lichtrinhids).OrderBy(c => c.ThoiGianDiHienTai).ToList();
                foreach (var nv in nguonves)
                {
                    var item = new BangDieuChuyenModel.BangDieuChuyenItem(nv.Id, ht.Id, ht.MaHanhTrinh, nv.LichTrinhId, nv.ThoiGianDi.ToString("HH:mm"));
                    //lay tat ca chuyen di trong lich trinh nay
                    var chuyendis = _nhaxeService.GetHistoryXeXuatBensByNguonVeId(nv.Id, model.NgayDi).OrderBy(c => c.NgayDi).ToList();

                    item.chuyendis = chuyendis.Select(c =>
                    {
                        return c.toModel(_localizationService);
                    }).ToList();
                    htchuyendis.LichTrinhItems.Add(item);
                }
                modelnew.arrBangDieuChuyen.Add(htchuyendis);
            }

            return PartialView(modelnew);
        }

        #endregion
    }
}