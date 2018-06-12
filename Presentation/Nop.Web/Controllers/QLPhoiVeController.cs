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
using Nop.Web.Models.QLPhoiVe;
using System.IO;
using Nop.Services.ExportImport;


namespace Nop.Web.Controllers
{
    public class QLPhoiVeController : BaseNhaXeController
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
        private readonly IBaoCaoService _baocaoService;
        private readonly IKeToanService _ketoanService;
        private readonly IExportManager _exportManager;

        public QLPhoiVeController(
            IKeToanService ketoanService,
            IBaoCaoService baocaoService,
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
            IGiaoDichKeVeXeService giaodichkeveService,
            IExportManager exportManager
            )
        {
            this._ketoanService = ketoanService;
            this._baocaoService = baocaoService;
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
            this._giaodichkeveService = giaodichkeveService;
            this._exportManager = exportManager;
        }
        #endregion
        #region Cac ham chung
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
        List<SelectListItem> PrepareHanhTrinhList(bool isAll = true, bool isChonHanhTrinh = false)
        {
            List<HanhTrinh> hanhtrinhs = new List<HanhTrinh>();
            if (isAll)
                hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId);
            else
                hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, _workContext.CurrentVanPhong.Id);
            var ddls = hanhtrinhs.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.MoTa;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();

            if (isChonHanhTrinh)
                ddls.Insert(0, new SelectListItem { Text = "--------------", Value = "0" });
            return ddls;
        }
        [NonAction]
        protected virtual void SoDoGheXeToSoDoGheXeModel(SoDoGheXe nvfrom, LoaiXeModel.SoDoGheXeModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.TenSoDo = nvfrom.TenSoDo;
            nvto.UrlImage = nvfrom.TenSoDo;
            nvto.SoLuongGhe = nvfrom.SoLuongGhe;
            nvto.KieuXeId = nvfrom.KieuXeId;
            nvto.SoCot = nvfrom.SoCot;
            nvto.SoHang = nvfrom.SoHang;


        }
        [NonAction]
        protected virtual void GetTenChang(PhoiVe phoive, ENPhanLoaiPhoiVe LoaiPhoiVe, LoaiXeModel.PhoiVeAdvanceModel model)
        {
            var _nguonve = phoive.nguonvexe;
            if (phoive.NguonVeXeConId > 0)
                _nguonve = phoive.nguonvexecon;
            var chang = _hanhtrinhService.GetHanhTrinhGiaVeId(phoive.ChangId.GetValueOrDefault(0));
            if (chang != null)
            {
                var TenTinhDon = chang.DiemDon.TenDiemDon;
                var TenTinhDen = chang.DiemDen.TenDiemDon;
                //var TenTinhDon = _hanhtrinhService.GetStateProvinceByNguon(_nguonve.DiemDon.NguonId).Abbreviation;
                //var TenTinhDen = _hanhtrinhService.GetStateProvinceByNguon(_nguonve.DiemDen.NguonId).Abbreviation;
                //if (LoaiPhoiVe == ENPhanLoaiPhoiVe.IN_PHOI_VE)
                //{
                //    TenTinhDon = chang.DiemDon.TenDiemDon;
                //    TenTinhDen = chang.DiemDen.TenDiemDon;
                //}
                model.TenChang = string.Format("{0}-{1}", TenTinhDon, TenTinhDen);
                model.MaMau = chang.DiemDen.MaMau;
            }
            else
            {
                model.TenChang = "";
            }

        }
        [NonAction]
        protected virtual string GetKhachDonDuong(PhoiVe phoive)
        {

            if (phoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
                return "DD";
            else
                return "";
        }
        ActionResult TrangThaiKhongHopLe()
        {
            return Json("Trạng thái không hợp lệ", JsonRequestBehavior.AllowGet);
        }
        ActionResult KhongSoHuu()
        {
            return Json("Vé không sở hữu", JsonRequestBehavior.AllowGet);
        }
        List<SelectListItem> PrepareListTuyen(bool isChonTuyen = true)
        {
            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            var ddls = tuyens.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.Ten;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();

            if (isChonTuyen)
                ddls.Insert(0, new SelectListItem { Text = "--------Chọn--------", Value = "0" });
            return ddls;
        }
        #endregion
        #region "Thong tin ghe"
        PhoiVe GetPhoiVeTrongChuyen(List<PhoiVe> phoives, SoDoGheXeQuyTac vitri, HistoryXeXuatBen chuyendi)
        {
            var pv = phoives.Where(c => c.SoDoGheXeQuyTacId == vitri.Id).FirstOrDefault();
            if (pv == null)
            {
                pv = new PhoiVe();
                pv.NgayDi = chuyendi.NgayDi;
                pv.NguonVeXeId = chuyendi.NguonVeId;
                pv.nguonvexe = chuyendi.NguonVeInfo;
                pv.TrangThai = ENTrangThaiPhoiVe.ConTrong;
                pv.sodoghexequytac = vitri;
                pv.SoDoGheXeQuyTacId = vitri.Id;
                return pv;
            }
            return pv;
        }
        /// <summary>
        /// Lay thong tin so do ghe xe
        /// </summary>
        /// <param name="NguonVeXeId"></param>
        /// <param name="NgayDi"></param>
        /// <param name="PhanLoai">0: phoi ve, 1:dung cho chuyen ve, 2: in phoive</param>
        /// <param name="TangIndex"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _TabSoDoXe(int ChuyenDiId, int? TangIndex, int? PhanLoai)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            //lay thong tin chuyen di
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            //lấy thong tin nguồn xe            
            if (chuyendi == null)
                return AccessDeniedView();

            var loaixe = _xeinfoService.GetById(chuyendi.NguonVeInfo.LoaiXeId);
            if (loaixe == null)
                return AccessDeniedView();          
            //var nhaxe = this._workContext.CurrentNhaXe;
            var sodoghe = _xeinfoService.GetSoDoGheXeById(loaixe.SoDoGheXeID);
            var modelsodoghe = new LoaiXeModel.SoDoGheXeModel();
            if (_workContext.CurrentVanPhong.IsYeuCauDuyetHuy)
            {
                modelsodoghe.CanYeuCauHuy = true;
            }
            modelsodoghe.chuyendihientai = chuyendi.toModel(_localizationService);
            var nguonves = _hanhtrinhService.GetallHanhTrinhGiaVe(chuyendi.HanhTrinhId, _workContext.NhaXeId, 0, 0,0);
            modelsodoghe.MangChangId = nguonves.Select(c => c.Id).ToArray();
            //modelsodoghe.MangChangId = chuyendi.HanhTrinh.DiemDons.OrderBy(c => c.ThuTu).Select(c => c.Id).ToArray();
            //modelsodoghe.t
            modelsodoghe.PhanLoai = (ENPhanLoaiPhoiVe)PhanLoai.GetValueOrDefault(0);
            SoDoGheXeToSoDoGheXeModel(sodoghe, modelsodoghe);
            // lay cac diem don , tao ma tran thong ke ket qua
            var diemdons = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(chuyendi.HanhTrinhId).Where(c => c.KhoangCach > 0).OrderBy(c => c.ThuTu).ToList();//.Select(c => c.Id).ToArray();
            var diemdonids = diemdons.Select(c => c.DiemDonId).ToArray();
            modelsodoghe.SoDiemDon = diemdons.Count();
            modelsodoghe.TongKet = new LoaiXeModel.TongKetPhoiToArrayModel[modelsodoghe.SoDiemDon + 1, modelsodoghe.SoCot + 2];
            for (int m = 0; m < modelsodoghe.SoDiemDon + 1; m++)
            {
                for (int n = 0; n < modelsodoghe.SoCot + 2; n++)
                {
                    modelsodoghe.TongKet[m, n] = new LoaiXeModel.TongKetPhoiToArrayModel();
                    if (m < modelsodoghe.SoDiemDon && n < modelsodoghe.SoCot + 1)
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
            //lay thong tin phoi ve
            var phoives = _phoiveService.GetPhoiVeByChuyenDi(chuyendi.Id);
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

                            modelsodoghe.PhoiVes1[s.y, s.x].Info = GetPhoiVeTrongChuyen(phoives, s, chuyendi);
                            if (modelsodoghe.PhoiVes1[s.y, s.x].Info.customer != null)
                            {
                                var ViTriGhe = modelsodoghe.PhoiVes1[s.y, s.x];
                                GetTenChang(ViTriGhe.Info, modelsodoghe.PhanLoai, ViTriGhe);
                                ViTriGhe.LoaiKhach = GetKhachDonDuong(ViTriGhe.Info);

                                ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToTien(_priceFormatter);
                                //lay so luong ma khach hang nay da dat trong chuyen
                                ViTriGhe.SoLuong = phoives.Count(c => c.CustomerId == ViTriGhe.Info.CustomerId);
                                ViTriGhe.TenNguoiDatVe = _nhanvienService.GetById(ViTriGhe.Info.NguoiDatVeId.Value).HoVaTen;
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
                                    if (indexdiemdon >= 0)
                                    {
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
                    else
                    {
                        modelsodoghe.PhoiVes2[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsodoghe.PhoiVes2[s.y, s.x].KyHieu = s.Val;
                        if (s.y >= 1 && s.x >= 1)
                        {
                            modelsodoghe.PhoiVes2[s.y, s.x].Info = GetPhoiVeTrongChuyen(phoives, s, chuyendi); ;
                            if (modelsodoghe.PhoiVes2[s.y, s.x].Info.customer != null)
                            {
                                var ViTriGhe = modelsodoghe.PhoiVes2[s.y, s.x];
                                GetTenChang(ViTriGhe.Info, modelsodoghe.PhanLoai, ViTriGhe);
                                ViTriGhe.LoaiKhach = GetKhachDonDuong(ViTriGhe.Info);
                                ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToTien(_priceFormatter);
                                ViTriGhe.TenNguoiDatVe = _nhanvienService.GetById(ViTriGhe.Info.NguoiDatVeId.Value).HoVaTen;
                                //lay so luong ma khach hang nay da dat trong chuyen
                                ViTriGhe.SoLuong = phoives.Count(c => c.CustomerId == ViTriGhe.Info.CustomerId);

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
                                    if (indexdiemdon >= 0)
                                    {
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
            }
            //selected tab
            SaveSelectedTabIndex(TangIndex);
            return PartialView(modelsodoghe);
        }
        // so do ghe doc

        #endregion
        #region Quan ly phoi ve
        public ActionResult Index()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var model = new QLPhoiVeModel();
            model.NgayDi = DateTime.Now;
            model.ListHanhTrinh = PrepareHanhTrinhList(false);
            if (model.ListHanhTrinh.Count > 0)
                model.HanhTrinhId = Convert.ToInt32(model.ListHanhTrinh[0].Value);
            model.KhungGioId = (int)CommonHelper.KhungGioHienTai();
            model.khunggios = this.GetCVEnumSelectList<ENKhungGio>(_localizationService, model.KhungGioId);
            //lay tat ca nhan vien
            model.AllLaiXePhuXes = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, new ENKieuNhanVien[] { ENKieuNhanVien.LaiXe, ENKieuNhanVien.PhuXe }).Select(c =>
            {
                return new XeXuatBenItemModel.NhanVienLaiPhuXe(c.Id, c.ThongTin());
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
            //lay tat ca thong tin xe
            model.AllXeInfo = _xeinfoService.GetAllXeInfoByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                return new XeXuatBenItemModel.XeVanChuyenInfo(c.Id, c.BienSo);
            }).ToList();
            return View(model);
        }
        public ActionResult _TabChuyenDi(int HanhTrinhId, int KhungGioId, DateTime NgayDi, string ThongTinKhachHang, int LoaiXeId)
        {
            var model = new QLPhoiVeModel();
            model.HanhTrinhId = HanhTrinhId;
            model.LoaiXeId = LoaiXeId;
            model.NgayDi = NgayDi;
            model.KhungGioId = KhungGioId;
            model.isTaoChuyen = this.isRightAccess(_permissionService, StandardPermissionProvider.CVQLChuyen);
            //lay thong tin chuyen di
            model.chuyendis = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, NgayDi, HanhTrinhId, (ENKhungGio)KhungGioId, ThongTinKhachHang, true, LoaiXeId).Select(c => { return c.toModel(_localizationService); }).ToList();
            if (model.chuyendis.Count > 0)
            {
                //lay thong tin chuyen di gan nhat
                foreach (var cd in model.chuyendis)
                {
                    var _thoigiandi = DateTime.Now.Date.AddHours(cd.NgayDi.Hour).AddMinutes(cd.NgayDi.Minute);
                    model.ChuyenDiId = cd.Id;
                    model.NguonVeId = cd.NguonVeId;
                    if (_thoigiandi > DateTime.Now)
                    {
                        break;
                    }
                }
            }

            return PartialView(model);
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
        private void ClearChonGeSession(string sessionNew="")
        {
            if (String.IsNullOrEmpty(sessionNew))
                Session["ChonGheGroup"] = null;
            else
                Session["ChonGheGroup"] = sessionNew;
        }
        [HttpPost]
        public ActionResult ChonGheDatCho(int ChuyenDiId, string KiHieuGhe, int tang)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            if (string.IsNullOrEmpty(KiHieuGhe) || tang == 0)
                return Loi();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            var item = new PhoiVe();
            item.ChuyenDiId = chuyendi.Id;
            item.NguonVeXeId = chuyendi.NguonVeId;
            item.NgayDi = chuyendi.NgayDi;
            item.SoDoGheXeQuyTacId = _vexeService.GetSoDoGheXeQuyTacID(chuyendi.NguonVeInfo.LoaiXeId, KiHieuGhe, tang);
            if (item.SoDoGheXeQuyTacId > 0)
            {
                item.TrangThai = ENTrangThaiPhoiVe.DatCho;
                item.isChonVe = false;//giao dich cua nha xe
                item.NguoiDatVeId = _workContext.CurrentNhanVien.Id;
                item.CustomerId = _workContext.CurrentCustomer.Id;
                item.SessionId = GetChonGeSession();
                item.GiaVeHienTai = chuyendi.NguonVeInfo.GiaVeHienTai;
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
            if (phoive.SessionId != GetChonGeSession(false) && phoive.NguoiDatVeId != _workContext.CurrentNhanVien.Id)
                return KhongSoHuu();
            _phoiveService.DeletePhoiVe(phoive);
            return ThanhCong();
        }
        public ActionResult KhachHangDatMuaVe(int ChuyenDiId, int? PhoiVeId = 0, bool IsSuaTungVe=false)
        {
            
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            var model = new QuanlyPhoiVeModel.KhachHangDatMuaVeModel();
            model.IsSuaTungVe = IsSuaTungVe;
            model.PhoiVeId = PhoiVeId.Value;
            model.ChuyenDiId = ChuyenDiId;
            model.NguonVeXeIdDangChon = chuyendi.NguonVeId;
            model.NgayDiDangChon = chuyendi.NgayDi.ToString("yyyy-MM-dd");
            //neu co PhoiVeId
            if (PhoiVeId.HasValue && PhoiVeId > 0)
            {
                //chon session cua phoi ve hien tai de sua thong tin
                var phoiveitem = _phoiveService.GetPhoiVeById(PhoiVeId.Value);
                if (phoiveitem != null && (phoiveitem.TrangThai == ENTrangThaiPhoiVe.ChoXuLy || phoiveitem.TrangThai == ENTrangThaiPhoiVe.DaThanhToan || phoiveitem.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang))
                {
                    var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(phoiveitem.CustomerId);
                    model.Id = khachhang.Id;
                    model.isKhachVangLai = phoiveitem.CustomerId == CommonHelper.KhachVangLaiId;
                    model.IsForKid = phoiveitem.IsForKid;
                    model.TenKhachHang = khachhang.HoTen;
                    model.SearchKhachHang = khachhang.SearchInfo;
                    model.DaThanhToan = phoiveitem.TrangThai == ENTrangThaiPhoiVe.DaThanhToan || phoiveitem.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang;
                    model.SoDienThoai = khachhang.DienThoai;
                    model.ViTriLen = phoiveitem.ViTriLenXe;
                    model.ViTriXuong = phoiveitem.ViTriXuongXe;
                    model.GhiChu = phoiveitem.GhiChu;
                    model.ChangId = phoiveitem.ChangId.GetValueOrDefault(0);
                    //cap nhat lai session
                    //huy tat ca ve dang chon de dat
                    var _sessionid = GetChonGeSession(false);
                    var phoives = _phoiveService.GetPhoiVeDatChoBySession(_sessionid).Select(c => c.Id).ToArray();
                    var phoiveids = string.Join(",", phoives);
                    if (!string.IsNullOrEmpty(phoiveids))
                    {
                        HuyVe(phoiveids);
                    }
                    //thiet dat session moi
                    ClearChonGeSession(phoiveitem.SessionId);


                }
            }


            var loaxeid = chuyendi.NguonVeInfo.LoaiXeId;

            var nguonves = _hanhtrinhService.GetallHanhTrinhGiaVe(chuyendi.HanhTrinhId, _workContext.NhaXeId, 0, 0,
                loaxeid);
            model._changs = nguonves.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.ToMoTaHanhTrinhGiaVe(),
                Selected=c.Id==model.ChangId
            }).ToList();
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
            model.quaybanves = vanphongs.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = string.Format("", c.TenVanPhong, c.Ma)
            }).ToList();
            
            return PartialView(model);

        }


        [HttpPost]
        public ActionResult ThanhToanGiuCho(int ChuyenDiId, int ChangId, string TenKhachHang, string SoDienThoai, bool DaThanhToan, int Id, string GhiChu, bool IsForKid, bool isKhachVangLai, int soLuong, string ViTriLen, string ViTriXuong,int PhoiVeId,bool isSuaTungVe)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

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
            //dat ve theo so luong
            if (soLuong > 0)
            {
                //xem trong chuyen con bao nhieu ghe trong
                var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
                var phoives = _phoiveService.GetPhoiVeByChuyenDiId(ChuyenDiId);
                var arrquytac = phoives.Select(c => c.sodoghexequytac).ToArray();
                var ghexequytac = _xeinfoService.GetAllSoDoGheXeQuyTac(chuyendi.NguonVeInfo.LoaiXeId).Where(c => !arrquytac.Contains(c) && c.Val != "").OrderBy(c => c.Tang).ThenBy(c => Convert.ToInt32(c.Val));
                var slghetrong = chuyendi.NguonVeInfo.loaixe.sodoghe.SoLuongGhe - phoives.Count();
                //neu soluong<=ghetrong-> cho dat, nguoc lai out
                if (soLuong <= slghetrong)
                {
                    var arrghe = ghexequytac.Take(soLuong - 1);
                    foreach (var item in arrghe)
                    {
                        ChonGheDatCho(ChuyenDiId, item.Val, item.Tang);
                        _phoiveService.NhaXeThanhToanGiuChoPhoiVeTheoChuyen(_workContext.NhaXeId, ChuyenDiId, ChangId, GetChonGeSession(false), _workContext.CurrentNhanVien.Id, DaThanhToan, CustomerId, GhiChu, IsForKid, ViTriLen, ViTriXuong);
                        ClearChonGeSession();
                    }
                }

            }
            else
            {
                if (!isSuaTungVe)
                    _phoiveService.NhaXeThanhToanGiuChoPhoiVeTheoChuyen(_workContext.NhaXeId, ChuyenDiId, ChangId, GetChonGeSession(false), _workContext.CurrentNhanVien.Id, DaThanhToan, CustomerId, GhiChu, IsForKid, ViTriLen, ViTriXuong);
                else
                {
                    var item = _phoiveService.GetPhoiVeById(PhoiVeId);
                    item.Id = PhoiVeId;
                    item.ChangId = ChangId;
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
                    _phoiveService.UpdatePhoiVe(item);
                }
                ClearChonGeSession();
            }

            return ThanhCong();
        }

        [HttpPost]
        public ActionResult DatVeNhanh(int ChuyenDiId, int changid, string NgayDi)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            _phoiveService.NhaXeThanhToanNhanhTheoChuyen(ChuyenDiId, changid, GetChonGeSession(false), _workContext.CurrentNhanVien.Id);
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
                model.NguonVeXeId_ChuyenVe = phoive.ChuyenDiId.Value;
                model.TenKhachHang = khachhang.HoTen;
                model.SoDienThoai = khachhang.DienThoai;
                model.NgayDi_ChuyenVe = phoive.NgayDi;
                model.DaThanhToan = false;
                if (phoive.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
                    model.DaThanhToan = true;
                //tim thoi gian gan nhat voi lich trinh xe dang co
                var chuyendis = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, phoive.NgayDi, HanhTrinhId, ENKhungGio.All, "", false);
                model.ListNguonVeXe_ChuyenVe = chuyendis.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = string.Format("{0} - ({1}:{2})", c.NguonVeInfo.ThoiGianDi.ToString("HH:mm"), c.NguonVeInfo.LichTrinhInfo.MaLichTrinh, c.NguonVeInfo.TenLoaiXe),
                    Selected = c.Id == model.NguonVeXeId_ChuyenVe
                }).ToList();

                return PartialView(model);
            }
            return AccessDeniedView();
        }

        [HttpPost]
        public ActionResult ChonChuyenVe(int PhoiVeId, int ChuyenDiId, string KiHieuGhe, int Tang, bool DaThanhToan)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            //lay thong tin phoi ve
            var _phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            //dat ve moi
            var item = new PhoiVe();
            item.ChuyenDiId = ChuyenDiId;
            item.NguonVeXeId = chuyendi.NguonVeId;
            item.NgayDi = chuyendi.NgayDi;
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
        public ActionResult HuyVe(string PhoiVeIds)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            if (string.IsNullOrEmpty(PhoiVeIds))
                return Loi();
            var arrDatVeId = PhoiVeIds.Split(',');
            foreach (var s in arrDatVeId)
            {
                var phoive = _phoiveService.GetPhoiVeById(Convert.ToInt32(s));
                //khong o trang thai giu cho hoac khong phai cua nha xe dat cho
                if (phoive.isChonVe)
                    return KhongSoHuu();
                //neu o trang thai dat cho
                if (phoive.TrangThai == ENTrangThaiPhoiVe.DatCho && phoive.SessionId == GetChonGeSession(false) && phoive.NguoiDatVeId == _workContext.CurrentNhanVien.Id)
                {
                    _phoiveService.DeletePhoiVe(phoive);
                    //return ThanhCong();
                }
                else
                {
                    //dang gan ve thi ko dc huy
                    if (phoive.VeXeItemId.HasValue)
                    {
                        return TrangThaiKhongHopLe();
                    }
                    if (phoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy || phoive.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
                    {
                        _phoiveService.HuyPhoiVe(phoive);
                        //return ThanhCong();
                    }
                }
                
            }
            return ThanhCong();
            
            
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
            if (_phoiveitem.VeXeItemId.HasValue)
            {
                var _vexeitem = _giaodichkeveService.GetVeXeItemById(_phoiveitem.VeXeItemId.Value);
                if (_vexeitem != null)
                {
                    model.QuayBanVeId = _vexeitem.VanPhongId.Value;
                    model.MauVeKyHieuId = _vexeitem.MauVeKyHieuId;
                    model.MaVe = _vexeitem.SoSeri;
                }
            }
            model.quaybanves = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = string.Format("{0}({1})", c.TenVanPhong, c.Ma),
                Selected = c.Id == model.QuayBanVeId
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
        public ActionResult GanSeriVe(int Id, int QuayBanVeId, int MauVeKyHieuId, string MaVe)
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
                return c.toModel();
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
        #region Thiet lap thong lai phu xe
        [HttpPost]
        public ActionResult ThietDatChuyenDi_Luu(string laiphuxeids, int XeVanChuyenId, int ChuyenDiId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            if (chuyendi != null)
            {
                // xóa hết các lái xe cũ
                chuyendi.LaiPhuXes.Clear();
                _nhaxeService.DeleteHistoryXeXuatBenNhanVien(chuyendi.Id);
                //up date lai xe mơi               
                int[] idlaiphuxes = Array.ConvertAll(laiphuxeids.Split(','), s => int.Parse(s));
                for (int i = 0; i < idlaiphuxes.Length; i++)
                {
                    var nhanvien = _nhanvienService.GetById(idlaiphuxes[i]);
                    if (nhanvien != null)
                    {
                        var _nhanvienxuatben = new HistoryXeXuatBen_NhanVien();
                        _nhanvienxuatben.NhanVien_Id = nhanvien.Id;
                        _nhanvienxuatben.HistoryXeXuatBen_Id = chuyendi.Id;
                        if (i == 0)
                            _nhanvienxuatben.KieuNhanVien = ENKieuNhanVien.LaiXe;
                        else
                            _nhanvienxuatben.KieuNhanVien = ENKieuNhanVien.PhuXe;
                        chuyendi.LaiPhuXes.Add(_nhanvienxuatben);
                    }

                }
                chuyendi.TrangThai = ENTrangThaiXeXuatBen.DANG_DI;
                chuyendi.XeVanChuyenId = XeVanChuyenId;
                _nhaxeService.UpdateHistoryXeXuatBen(chuyendi);
                //luu log
                TaoNhatKyChuyenDi(chuyendi.Id, "Thiết đặt xe xuất bến", ENTrangThaiXeXuatBen.DANG_DI);
            }

            //lay lai thong tin
            chuyendi = _nhaxeService.GetHistoryXeXuatBenId(chuyendi.Id);
            return Json(chuyendi.toModel(_localizationService));

        }
        [HttpPost]
        public ActionResult ThietDatChuyenDi_Huy(int ChuyenDiId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var xexuatben = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            if (xexuatben == null)
                return Loi();
            xexuatben.XeVanChuyenId = null;
            xexuatben.LaiPhuXes.Clear();
            _nhaxeService.DeleteHistoryXeXuatBenNhanVien(xexuatben.Id);
            xexuatben.TrangThai = ENTrangThaiXeXuatBen.CHO_XUAT_BEN;
            _nhaxeService.UpdateHistoryXeXuatBen(xexuatben);
            //luu log
            TaoNhatKyChuyenDi(ChuyenDiId, "Hủy thiết đặt xe xuất bến", ENTrangThaiXeXuatBen.CHO_XUAT_BEN);
            return ThanhCong();
        }
        [HttpPost]
        public ActionResult HuyChuyenDi(int ChuyenDiId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var xexuatben = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            if (xexuatben == null)
                return Json("NO");
            var phoives = _phoiveService.GetPhoiVeByChuyenDi(ChuyenDiId, true);
            if (phoives.Count > 0)
            {
                return Json("NO");
            }
            var vexe = _giaodichkeveService.GetVeXeItems(xexuatben.Id);
            if (vexe.Count() > 0)
            {
                return Json("NO");
            }
            //kiem tra xem co thong tin tai chinh ko
            //var _taichinh = _giaodichkeveService.GetChuyenDiTaiChinhByLuotId(Id);
            //if (_taichinh != null)
            //{
            //    return Json("NO");
            //}
            xexuatben.TrangThai = ENTrangThaiXeXuatBen.HUY;
            _nhaxeService.UpdateHistoryXeXuatBen(xexuatben);
            //luu log
            TaoNhatKyChuyenDi(ChuyenDiId, "Hủy chuyến đi", ENTrangThaiXeXuatBen.HUY);
            return ThanhCong();
        }
        void TaoNhatKyChuyenDi(int ChuyenDiId, string note, ENTrangThaiXeXuatBen trangthai)
        {
            var _log = new HistoryXeXuatBenLog();
            _log.NguoiTaoId = _workContext.CurrentNhanVien.Id;
            _log.NgayTao = DateTime.Now;
            _log.TrangThai = trangthai;
            _log.GhiChu = note;
            _log.XeXuatBenId = ChuyenDiId;
            _nhaxeService.InsertHistoryXeXuatBenLog(_log);

        }
        /// <summary>
        /// create by Mss.Mai
        /// cho phep chon gio và loai xe de tao chuyen
        /// </summary>
        /// <param name="HanhTrinhId"></param>
        /// <param name="LoaiXeId"></param>
        /// <returns></returns>
        public ActionResult _TaoChuyenDi(int HanhTrinhId, int LoaiXeId, DateTime NgayDi)
        {

            var model = new XeXuatBenItemModel();
            model.HanhTrinhId = HanhTrinhId;
            model.ThoiGianThuc = DateTime.Now;
            var hanhtrinh = _hanhtrinhService.GetHanhTrinhById(HanhTrinhId);
            model.TuyenXeChay = hanhtrinh.MoTa;
            model.NgayDi = NgayDi.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
            model.LoaiXeId = LoaiXeId;
            var loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId);
            model.loaixes = loaixes.Select(c =>
            {
                var item = new SelectListItem();
                item.Value = c.Id.ToString();
                item.Text = c.TenLoaiXe;
                item.Selected = (c.Id == model.LoaiXeId);
                return item;
            }).ToList();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult TaoMoiChuyenDi(int HanhTrinhId, string ThoiGianDi, int LoaiXeId, DateTime NgayDi)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            if (HanhTrinhId == 0)
                return AccessDeniedView();
            if (LoaiXeId == 0)
                return AccessDeniedView();
            var chuyendi = new HistoryXeXuatBen();
            chuyendi.NhaXeId = _workContext.NhaXeId;
            chuyendi.HanhTrinhId = HanhTrinhId;
            DateTime _thoigiandi = Convert.ToDateTime(ThoiGianDi);
            //lay nguon ve gan voi thoi gian chon
            var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(HanhTrinhId).Select(c => c.Id).ToList();
            var nguonves = _hanhtrinhService.GetAllNguonVeXeByHanhTrinhLoaiXe(lichtrinhs, LoaiXeId).OrderBy(c => c.ThoiGianDi).ToList();
            if (nguonves.Count() == 0)
                return Json("hien tai chua co lich trinh ", JsonRequestBehavior.AllowGet);
            NguonVeXe _nguonvexe = null;
            _thoigiandi = NgayDi.Date.AddHours(_thoigiandi.Hour).AddMinutes(_thoigiandi.Minute);
            _nguonvexe = nguonves.First();
            foreach (var nv in nguonves)
            {

                DateTime fromDate = NgayDi.Date.AddHours(nv.ThoiGianDi.Hour).AddMinutes(nv.ThoiGianDi.Minute);

                //neu nam trong khoang thi break
                if (_thoigiandi >= fromDate)
                {
                    _nguonvexe = nv;
                    break;
                }
            }
            if (_nguonvexe == null)
            {
                return Json("Khung gio khong co trong lich trinh", JsonRequestBehavior.AllowGet);
            }

            chuyendi.NguonVeId = _nguonvexe.Id;
            chuyendi.NgayTao = DateTime.Now;
            chuyendi.TrangThai = ENTrangThaiXeXuatBen.CHO_XUAT_BEN;
            chuyendi.NguoiTaoId = _workContext.CurrentNhanVien.Id;
            chuyendi.NgayDi = _thoigiandi;
            _nhaxeService.InsertHistoryXeXuatBen(chuyendi);
            TaoNhatKyChuyenDi(chuyendi.Id, "Tạo mới thông tin chuyến đi", chuyendi.TrangThai);
            return ThanhCong();
        }
        #endregion
        #region Chuyen di tai chinh: thu chi , doanh thu, hoa hong
        ChuyenDiTaiChinhModel toChuyenDiTaiChinhModel(ChuyenDiTaiChinh entity)
        {
            var model = new ChuyenDiTaiChinhModel();
            model.Id = entity.Id;
            model.isCP1 = entity.isCP1;
            model.isCPText = "";
            model.NguoiTaoId = entity.NguoiTaoId;
            model.TenNguoiTao = entity.nguoitao.HoVaTen;
            model.LuotDiId = entity.LuotDiId;
            model.ThucThu = entity.ThucThu;
            model.DinhMucDau = entity.DinhMucDau;
            model.isCapNhat = entity.isCapNhat;
            model.ThucDo = entity.ThucDo;
            model.GiaDau = entity.GiaDau;
            model.VeQuay = entity.VeQuay;
            model.LuotVeId = entity.LuotVeId.GetValueOrDefault(0);
            model.NgayTao = entity.NgayTao;
            model.XeVanChuyenId = entity.XeVanChuyenId.GetValueOrDefault();
            if (entity.xevanchuyen != null)
                model.BienSoXe = entity.xevanchuyen.BienSo;
            //add by lent 15-12-2015
            //them thong tin nguoi thu, nguoi nop, ngay giao dich
            model.NguoiNopId = entity.NguoiNopId.GetValueOrDefault(0);
            if (entity.nguoinop != null)
                model.tennguoinop = entity.nguoinop.HoVaTen;
            model.NguoiThuId = entity.NguoiThuId.GetValueOrDefault(0);
            if (entity.nguoithu != null)
                model.tennguoithu = entity.nguoithu.HoVaTen;
            model.NgayGiaoDich = entity.NgayGiaoDich;
            //kiem tra thong tin tinh luong

            foreach (var item in entity.GiaoDichThuChis)
            {

                var itemmodel = toChuyenDiTaiChinhThuChiModel(item);
                model.GiaoDichThuChis.Add(itemmodel);
            }
            //thong tin doanh thu
            model.DTLuotDi = _phoiveService.GetPhoiVeByChuyenDi(entity.luotdi.Id, true).Sum(c => c.GiaVeHienTai);
            //   model.DTLuotDi = _giaodichkeveService.GetVeXeBanTrenXe(_workContext.NhaXeId, entity.luotdi).Sum(c=>c.GiaVe);
            return model;

        }
        ChuyenDiTaiChinhModel.ChuyenDiTaiChinhThuChiModel toChuyenDiTaiChinhThuChiModel(ChuyenDiTaiChinhThuChi entity)
        {
            var model = new ChuyenDiTaiChinhModel.ChuyenDiTaiChinhThuChiModel();
            model.Id = entity.Id;
            model.ChuyenDiTaiChinhId = entity.ChuyenDiTaiChinhId;
            model.loaithuchi = entity.loaithuchi;

            model.LoaiThuChiText = entity.loaithuchi.ToCVEnumText(_localizationService);
            model.SoTien = entity.SoTien / 1000;
            if (model.LoaiThuChiId > 100)
                model.SoTien = -model.SoTien;
            model.GhiChu = entity.GhiChu;

            return model;

        }
        ChuyenDiTaiChinhModel.ChuyenDiTaiChinhTongHop ToChuyenDiTaiChinh(ChuyenDiTaiChinhModel model)
        {
            var _item = new ChuyenDiTaiChinhModel.ChuyenDiTaiChinhTongHop();
            _item.VeQuay = model.VeQuay;
            _item.VeLaiXe = model.DTLuotDi - model.VeQuay;
            var khoanchis = model.GiaoDichThuChis.Where(c => c.LoaiThuChiId > 100).ToList();
            var khoanthus = model.GiaoDichThuChis.Where(c => c.LoaiThuChiId < 100).ToList();
            var tongchi = khoanchis.Sum(c => c.SoTien) * 1000 - khoanthus.Sum(c => c.SoTien) * 1000;
            _item.DoanhThu = model.DTLuotDi;
            _item.TongChi = tongchi;
            _item.ThucThu = model.DTLuotDi - tongchi;
            return _item;
        }
        public ActionResult InDSDonTra(int ChuyenDiId, bool istra)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new ChuyenDiModel();
            model.istra = istra;
            model.TenVanPhong = _workContext.CurrentVanPhong.Ma;
            if (ChuyenDiId > 0)
            {
                model.chuyendihientai = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId).toModel(_localizationService);
                var phoives = _phoiveService.GetPhoiVeByChuyenDiId(ChuyenDiId);
                if (istra)
                    phoives = phoives.Where(c => string.IsNullOrEmpty(c.ViTriXuongXe) == false).ToList();
                else
                    phoives = phoives.Where(c => string.IsNullOrEmpty(c.ViTriLenXe) == false).ToList();
                model.phoives = phoives.Select(c =>
                  {
                      var _item = new PhoiVeModel();
                      _item = c.toModel();
                      var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(c.CustomerId);
                      if (khachhang != null)
                      {
                          _item.TenKhachHang = khachhang.HoTen;
                          _item.SoDienThoai = khachhang.DienThoai;
                      }
                      return _item;
                  }).ToList();
            }
            return View(model);
        }
        public ActionResult _TabChiPhiChuyenDi(int ChuyenDiId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var model = new ChuyenDiTaiChinhModel();
            var chuyeditc = _giaodichkeveService.GetChuyenDiTaiChinhByLuotId(ChuyenDiId);

            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            var chuyendis = new int[] { ChuyenDiId };
            if (chuyeditc != null)
            {


                chuyeditc.VeQuay = _baocaoService.GetTongDoanhThuTheoChuyenDi(chuyendis);
                if (chuyeditc.NguoiNopId == null && chuyendi.LaiPhuXes.Count > 0)
                    chuyeditc.NguoiNopId = chuyendi.LaiPhuXes.First().NhanVien_Id;
                _giaodichkeveService.UpdateChuyenDiTaiChinh(chuyeditc);
                _customerActivityService.InsertActivityNhaXe("Chuyến đi tài chính được chỉnh sửa bởi : {0}", _workContext.CurrentCustomer.Email);
                model = toChuyenDiTaiChinhModel(chuyeditc);
                // tong hop luot ve
                model.LuotVeTC = ToChuyenDiTaiChinh(model);
                //lay thong tin cua cua chuyen di
                var luotdi = _nhaxeService.GetChuyenDiByChuyenVe(ChuyenDiId);
                model.LuotDiTC = null;
                if (luotdi != null)
                {
                    var chuyenditcDi = _giaodichkeveService.GetChuyenDiTaiChinhByLuotId(luotdi.Id);
                    if (chuyenditcDi != null)
                    {
                        var CDTCDiModel = toChuyenDiTaiChinhModel(chuyenditcDi);
                        model.LuotDiTC = new ChuyenDiTaiChinhModel.ChuyenDiTaiChinhTongHop();
                        model.LuotDiTC = ToChuyenDiTaiChinh(CDTCDiModel);
                        //tong hop
                        model.TongChuyenDiTaiChinh.VeQuay = model.LuotVeTC.VeQuay + model.LuotDiTC.VeQuay;
                        model.TongChuyenDiTaiChinh.VeLaiXe = model.LuotVeTC.VeLaiXe + model.LuotDiTC.VeLaiXe;
                        model.TongChuyenDiTaiChinh.DoanhThu = model.LuotVeTC.DoanhThu + model.LuotDiTC.DoanhThu;
                        model.TongChuyenDiTaiChinh.TongChi = model.LuotVeTC.TongChi + model.LuotDiTC.TongChi;
                        model.TongChuyenDiTaiChinh.ThucThu = model.LuotVeTC.ThucThu + model.LuotDiTC.ThucThu;
                    }
                }
            }
            else
            {
                //neu chua co thi tao moi    
                chuyeditc = new ChuyenDiTaiChinh();
                chuyeditc.NhaXeId = _workContext.NhaXeId;
                chuyeditc.NguoiTaoId = _workContext.CurrentNhanVien.Id;
                chuyeditc.LuotDiId = ChuyenDiId;
                chuyeditc.XeVanChuyenId = chuyendi.XeVanChuyenId;
                //add thong tin mac dinh
                chuyeditc.NguoiThuId = _workContext.CurrentNhanVien.Id;
                if (chuyendi.LaiPhuXes.Count > 0)
                    chuyeditc.NguoiNopId = chuyendi.LaiPhuXes.First().NhanVien_Id;
                chuyeditc.NgayGiaoDich = DateTime.Now.Date;
                _giaodichkeveService.InsertChuyenDiTaiChinh(chuyeditc);
                _customerActivityService.InsertActivityNhaXe("Chuyến đi tài chính được tạo bởi bởi : {0}", _workContext.CurrentCustomer.Email);

                _giaodichkeveService.UpdateChuyenDiTaiChinh(chuyeditc);
                // thong tin giao dich thu chi
                //chi
                var chitienan = _nhaxeService.GetGiaTriCauHinh(_workContext.NhaXeId, ENNhaXeCauHinh.TIEN_AN_LAI_PHU_XE);
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_NHA_HANG, -chitienan));
                var chicauduong = _nhaxeService.GetGiaTriCauHinh(_workContext.NhaXeId, ENNhaXeCauHinh.TIEN_CAU_DUONG);
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_CAU_DUONG, -chicauduong));
                var chiquadem = _nhaxeService.GetGiaTriCauHinh(_workContext.NhaXeId, ENNhaXeCauHinh.TIEN_GUI_XE_QUA_DEM);
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.GUI_XE_QUA_DEM, -chiquadem));
                var chibenxe = _nhaxeService.GetGiaTriCauHinh(_workContext.NhaXeId, ENNhaXeCauHinh.TIEN_PHOI_LENH);
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_BEN_XE, -chibenxe));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_TIEN_DAU));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_CONG_AN));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_DANG_KIEM));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_KHAC));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_SUA_CHUA_XE));

                var chuyeditcnew = _giaodichkeveService.GetChuyenDiTaiChinhById(chuyeditc.Id);
                chuyeditc.VeQuay = _baocaoService.GetTongDoanhThuTheoChuyenDi(chuyendis);
                _giaodichkeveService.UpdateChuyenDiTaiChinh(chuyeditc);
                model = toChuyenDiTaiChinhModel(chuyeditcnew);

            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _CapNhapChuyenDiTaiChinh(ChuyenDiTaiChinhModel model, string GhiChu)
        {

            if (model.isCapNhat)
            {
                if (!this.isQuanLyAccess(_workContext))
                    return Json("DACAPNHAT", JsonRequestBehavior.AllowGet);
            }

            string[] arrinfo = model.ListThuChi.Split('|');
            for (int i = 0; i < arrinfo.Length; i = i + 2)
            {
                if (string.IsNullOrWhiteSpace(arrinfo[i]))
                {
                    arrinfo[i] = "0";
                }
                if (string.IsNullOrWhiteSpace(arrinfo[i + 1]))
                {
                    arrinfo[i + 1] = "0";
                }
                int LoaiThuChiId = Convert.ToInt32(arrinfo[i]);
                Decimal SoTien = Convert.ToDecimal(arrinfo[i + 1]) * 1000;
                var item = _giaodichkeveService.GetChuyenDiTaiChinhThuChiById(model.Id, LoaiThuChiId);
                if (item == null)
                    return Loi();
                if (LoaiThuChiId < 100)
                    item.SoTien = SoTien;
                else
                    item.SoTien = -SoTien;
                if (item.loaithuchi == ENLoaiTaiChinhThuChi.CHI_SUA_CHUA_XE)
                {
                    item.GhiChu = GhiChu;
                }

                _giaodichkeveService.UpdateChuyenDiTaiChinhThuChi(item);

            }
            //xe thuong phat dau           

            var Chuyenditc = _giaodichkeveService.GetChuyenDiTaiChinhById(model.Id);


            var chiphi = Chuyenditc.GiaoDichThuChis.Sum(c => c.SoTien);
            Chuyenditc.VeQuay = _baocaoService.GetTongDoanhThuTheoChuyenDi(new int[] { Chuyenditc.LuotDiId });
            Chuyenditc.XeVanChuyenId = Chuyenditc.luotdi.XeVanChuyenId;
            Chuyenditc.ThucThu = Chuyenditc.VeQuay + chiphi;
            Chuyenditc.isCapNhat = true;
            //cap nhat them thong tin
            if (model.NguoiThuId > 0)
                Chuyenditc.NguoiThuId = model.NguoiThuId;
            else
                Chuyenditc.NguoiThuId = null;
            if (model.NguoiNopId > 0)
                Chuyenditc.NguoiNopId = model.NguoiNopId;
            else
                Chuyenditc.NguoiNopId = null;
            Chuyenditc.NgayGiaoDich = model.NgayGiaoDich;
            _giaodichkeveService.UpdateChuyenDiTaiChinh(Chuyenditc);
            TaoThuChiChuyenDi(Chuyenditc);
            return ThanhCong();
        }
        void TaoThuChiChuyenDi(ChuyenDiTaiChinh Chuyenditc)
        {
            //tao thong tin thu chi chuyen di
            //add by lent : 18/12/2016
            //xoa thong tin trc do
            var chiphi = Chuyenditc.GiaoDichThuChis.Sum(c => c.SoTien);
            _ketoanService.DeleteThuChiChuyenDi(Chuyenditc.luotdi.Id);
            //them thong tin doanh thu
            var thuchuyendi = new ThuChi();
            thuchuyendi.NgayTao = DateTime.Now;
            thuchuyendi.ChuyenDiId = Chuyenditc.luotdi.Id;
            thuchuyendi.NgayGiaoDich = Chuyenditc.NgayGiaoDich.HasValue ? Chuyenditc.NgayGiaoDich.Value : DateTime.Now.Date;
            thuchuyendi.isChi = false;
            thuchuyendi.GiaTri = Chuyenditc.VeQuay;
            thuchuyendi.LoaiThuChiId = 2;//doanh thu ban ve

            thuchuyendi.NguoiNopId = Chuyenditc.NguoiNopId;
            thuchuyendi.NguoiThuId = Chuyenditc.NguoiThuId;
            thuchuyendi.NguoiTaoId = _workContext.CurrentNhanVien.Id;
            thuchuyendi.NhaXeId = _workContext.NhaXeId;
            thuchuyendi.DienGiai = "Thu tiền bán vé";
            _ketoanService.Insert(thuchuyendi);
            //them thong tin chi phi
            var chichuyendi = new ThuChi();
            chichuyendi.NgayTao = DateTime.Now;
            chichuyendi.ChuyenDiId = Chuyenditc.luotdi.Id;
            chichuyendi.NgayGiaoDich = Chuyenditc.NgayGiaoDich.HasValue ? Chuyenditc.NgayGiaoDich.Value : DateTime.Now.Date;
            chichuyendi.isChi = true;
            chichuyendi.GiaTri = Math.Abs(chiphi);
            chichuyendi.LoaiThuChiId = 3;//doanh thu ban ve
            chichuyendi.NguoiNopId = Chuyenditc.NguoiThuId;
            chichuyendi.NguoiThuId = Chuyenditc.NguoiNopId;
            chichuyendi.NguoiTaoId = _workContext.CurrentNhanVien.Id;
            chichuyendi.NhaXeId = _workContext.NhaXeId;
            chichuyendi.DienGiai = "Chi tiền chuyến xe";
            _ketoanService.Insert(chichuyendi);
        }
        [HttpPost]
        public ActionResult HuyCapNhatChiPhi(int Id)
        {
            if (Id == 0)
                return Loi();

            var chuyenditc = _giaodichkeveService.GetChuyenDiTaiChinhById(Id);
            //xoa thong tin trc do
            _ketoanService.DeleteThuChiChuyenDi(chuyenditc.luotdi.Id);
            _giaodichkeveService.DeleteAllChuyenDiTaiChinhThuChi(chuyenditc.GiaoDichThuChis.ToList());
            _giaodichkeveService.DeleteChuyenDiTaiChinh(chuyenditc);

            return ThanhCong();
        }
        #endregion
        #region Phoi ve bo sung
        public ActionResult PhoiVeBoSung_List(int ChuyenDiId)
        {
            //lay tat ca thong tin ve theo chuyen di roi format theo list
            var items = new List<PhoiVeBoSungModel>();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            var vexeitems = _giaodichkeveService.GetVeXeBanTrenXe(_workContext.NhaXeId, chuyendi);
            var changids = vexeitems.Select(c => c.ChangId).Distinct().ToArray();
            var nguonves = _hanhtrinhService.GetallHanhTrinhGiaVe(chuyendi.HanhTrinhId, _workContext.NhaXeId).Where(c => changids.Contains(c.Id)).ToList();
            foreach (var chang in nguonves)
            {
                var vexeitemchangs = vexeitems.Where(c => c.ChangId == chang.Id).ToList();
                var quyens = vexeitemchangs.Select(c => c.SoQuyen).Distinct().ToList();
                foreach (var soquyen in quyens)
                {
                    var vexeitemchangs_quyen = vexeitemchangs.Where(c => c.SoQuyen == soquyen).OrderBy(c => c.SoSeriNum).ToList();
                    var vexeitem = vexeitemchangs_quyen.First();
                    var item = new PhoiVeBoSungModel();
                    item.NhanVienId = vexeitem.NhanVienId.GetValueOrDefault(0);
                    item.SoQuyen = soquyen;
                    item.ChangId = chang.Id;
                    item.TenChang = chang.ToMoTaHanhTrinhGiaVe();
                    item.TenMau = string.Format("{0} - {1}", vexeitem.MauVe, vexeitem.KyHieu);
                    item.MauVeId = vexeitem.MauVeKyHieuId;
                    item.TenNhanVien = vexeitem.nhanvien.HoVaTen;
                    item.SeriFrom = vexeitem.SoSeri;
                    item.SoLuong = vexeitemchangs_quyen.Count;
                    item.SeriTo = vexeitemchangs_quyen[vexeitemchangs_quyen.Count - 1].SoSeri;
                    items.Add(item);
                }
            }
            var gridModel = new DataSourceResult
            {
                Data = items,
                Total = items.Count
            };

            return Json(gridModel);
        }
        public ActionResult _PhoiVeBoSung_ThemMoi(int ChuyenDiId)
        {
            var model = new PhoiVeBoSungModel();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            model.ChuyenDiId = chuyendi.Id;
            model.HanhTrinhId = chuyendi.HanhTrinhId;
            var nguonves = _hanhtrinhService.GetallHanhTrinhGiaVe(chuyendi.HanhTrinhId, _workContext.NhaXeId);
            model.changs = nguonves.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.ToMoTaHanhTrinhGiaVe()
            }).ToList();
            model.maukyhieus = _giaodichkeveService.GetAllMauVeKyHieu(_workContext.NhaXeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = string.Format("{0}-{1}", c.MauVe, c.KyHieu),
            }).ToList();
            model.nhanviens = chuyendi.LaiPhuXes.Select(c => new SelectListItem
            {
                Value = c.NhanVien_Id.ToString(),
                Text = c.nhanvien.ThongTin(false)
            }).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult PhoiVeBoSung_ThongTinSeri(PhoiVeBoSungModel model)
        {

            var nguonves = _hanhtrinhService.GetallHanhTrinhGiaVe(model.HanhTrinhId, _workContext.NhaXeId).Where(c => c.Id == model.ChangId).FirstOrDefault();
            //lay thong tin so seri from den to
            var vexeitems = _giaodichkeveService.GetTonVeXeItemsByNhanVien(model.NhanVienId, nguonves.GiaVe, model.MauVeId, model.SoLuong);
            var quyenids = vexeitems.Select(c => c.SoQuyen).Distinct();
            model.SoLuong = 0;
            if (quyenids.Count() > 0)
            {
                var quyenid = quyenids.First();
                var _vexeitems = vexeitems.Where(c => c.SoQuyen == quyenid).OrderBy(c => c.SoSeriNum).ToList();

                model.SoLuong = _vexeitems.Count;
                model.SeriFrom = _vexeitems[0].SoSeri;
                model.SeriTo = _vexeitems[_vexeitems.Count - 1].SoSeri;
            }
            return Json(model);
        }
        [HttpPost]
        public ActionResult PhoiVeBoSung_ThemMoi(PhoiVeBoSungModel model)
        {
            string _serigiamgia = "";
            if (!string.IsNullOrEmpty(model.SeriGiamGia))
            {
                _serigiamgia = model.SeriGiamGia.Replace(" ", "");
            }
            _giaodichkeveService.PhoiVeBoSungThemMoi(model.ChuyenDiId, model.NhanVienId, model.ChangId, model.MauVeId, Convert.ToInt32(model.SeriFrom), Convert.ToInt32(model.SeriTo), _serigiamgia);
            return ThanhCong();
        }
        [HttpPost]
        public ActionResult PhoiVeBoSung_Huy(PhoiVeBoSungModel model)
        {
            _giaodichkeveService.PhoiVeBoSungHuy(model.ChuyenDiId, model.NhanVienId, model.ChangId, model.MauVeId, Convert.ToInt32(model.SeriFrom), Convert.ToInt32(model.SeriTo));
            return ThanhCong();
        }
        #endregion
        #region Danh sach chuyen di
        public ActionResult QLChuyenDi()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var model = new QLChuyenDiModel();
            model.ListTuyens = PrepareListTuyen();
            model.DenNgay = DateTime.Now;
            model.TuNgay = DateTime.Now.AddDays(-14);
            return View(model);
        }
        [HttpPost]
        public ActionResult _DanhSachChuyenDi(QLChuyenDiModel model)
        {
            //
            int[] hanhtrinhids = null;
            if (model.TuyenId > 0)
            {
                hanhtrinhids = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, model.TuyenId).Select(c => c.Id).ToArray();
            }
            var xexuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, -1, hanhtrinhids, null, model.TuNgay, model.DenNgay);
            if (!string.IsNullOrEmpty(model.KeySearch))
            {
                xexuatbens = xexuatbens.Where(c => c.LaiPhuXes.Any(l => l.nhanvien.HoVaTen.Contains(model.KeySearch)) || (c.XeVanChuyenId > 0 && c.xevanchuyen.BienSo.Contains(model.KeySearch))).ToList();
            }
            model.lschuyendi = xexuatbens.Select(c =>
            {
                var item = new QLChuyenDiModel.ChuyenDiModel();
                item.xexuatben = c.toModel(_localizationService);
                item.chuyenditc = _giaodichkeveService.GetChuyenDiTaiChinhByLuotId(c.Id);
                item.DoanhThu = _phoiveService.GetPhoiVeByChuyenDi(c.Id, true).Sum(p => p.GiaVeHienTai);//_giaodichkeveService.GetVeXeBanTrenXe(_workContext.NhaXeId, c).Sum(dt => dt.GiaVe);
                return item;
            }).ToList();
            return PartialView(model);
        }
        public ActionResult _DoanhThuChang(int Id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new XeXuatBenItemModel();
            model.Id = Id;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult GetDoanhThuChang(int Id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var xexuatben = _nhaxeService.GetHistoryXeXuatBenId(Id);
            var changs = _hanhtrinhService.GetallHanhTrinhGiaVe(xexuatben.HanhTrinhId, _workContext.NhaXeId, 0, 0, xexuatben.xevanchuyen.LoaiXeId);
            var dtchang = new List<DoanhThuChang>();
            foreach (var item in changs)
            {
                var m = new DoanhThuChang();
                m.ChangId = item.Id;
                m.TenChang = item.ToMoTaHanhTrinhGiaVe();
                m.SoKhach = _baocaoService.GetTongDoanhThuChangTheoChuyenDi(item.Id, new int[] { Id });
                dtchang.Add(m);
            }
            dtchang = dtchang.Where(c => c.SoKhach > 0).ToList();
            var gridModel = new DataSourceResult
            {
                Data = dtchang.ToList(),
                Total = dtchang.Count
            };

            return Json(gridModel);
        }
        [HttpPost]
        public ActionResult _DoanhThuDiem(int Id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var xexuatben = _nhaxeService.GetHistoryXeXuatBenId(Id);
            var diemdons = _hanhtrinhService.GetAllDiemDonByNhaXeId(_workContext.NhaXeId);
            var dtdiem = new List<DoanhThuDiem>();
            foreach (var item in diemdons)
            {
                var m = new DoanhThuDiem();
                m.TenDiem = item.TenDiemDon;
                m.SoKhachLen = _baocaoService.GetSoKhachLenTaiDiemTheoChuyenDi(item.Id, Id);
                m.SoKhachXuong = _baocaoService.GetSoKhachXuongTaiDiemTheoChuyenDi(item.Id, Id);
                dtdiem.Add(m);
            }
            dtdiem = dtdiem.Where(c => c.SoKhachLen > 0 || c.SoKhachXuong > 0).ToList();
            var gridModel = new DataSourceResult
            {
                Data = dtdiem.ToList(),
                Total = dtdiem.Count
            };

            return Json(gridModel);
        }
        public ActionResult ExportExcelLenhPhu(int Id)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            try
            {
                var xexuatben = _nhaxeService.GetHistoryXeXuatBenId(Id);
                //lay thong ti khach di xe
                var changs = _hanhtrinhService.GetallHanhTrinhGiaVe(xexuatben.HanhTrinhId, _workContext.NhaXeId, 0, 0, xexuatben.xevanchuyen.LoaiXeId);
                var dtchang = new List<DoanhThuChang>();
                foreach (var item in changs)
                {
                    var m = new DoanhThuChang();
                    m.ChangId = item.Id;
                    m.TenChang = item.ToMoTaHanhTrinhGiaVe();
                    m.SoKhach = _baocaoService.GetTongDoanhThuChangTheoChuyenDi(item.Id, new int[] { Id });
                    dtchang.Add(m);
                }
                dtchang = dtchang.Where(c => c.SoKhach > 0).ToList();
                var diemdons = _hanhtrinhService.GetAllDiemDonByNhaXeId(_workContext.NhaXeId);
                var dtdiem = new List<DoanhThuDiem>();
                foreach (var item in diemdons)
                {
                    var m = new DoanhThuDiem();
                    m.TenDiem = item.TenDiemDon;
                    m.SoKhachLen = _baocaoService.GetSoKhachLenTaiDiemTheoChuyenDi(item.Id, Id);
                    m.SoKhachXuong = _baocaoService.GetSoKhachXuongTaiDiemTheoChuyenDi(item.Id, Id);
                    dtdiem.Add(m);
                }
                dtdiem = dtdiem.Where(c => c.SoKhachLen > 0 || c.SoKhachXuong > 0).ToList();
                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    _exportManager.ExportdtdiemToXlsx(stream, dtchang, dtdiem, xexuatben.NgayDi, xexuatben.xevanchuyen.BienSo);
                    bytes = stream.ToArray();

                }

                return File(bytes, "text/xls", "Doanh thu chặng" + xexuatben.xevanchuyen.BienSo + "_" + xexuatben.NgayDi.ToString("ddMMyyyy HH:mm") + ".xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }
        #endregion
        #region chuyen so do
        public ActionResult _ChuyenSoDo(int ChuyenId)
        {
            var model = new XeXuatBenItemModel();
            model.Id = ChuyenId;
            var _chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenId);
            model.LoaiXeId = _chuyendi.NguonVeInfo.LoaiXeId;
            var loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId);
            model.loaixes = loaixes.Select(c =>
            {
                var item = new SelectListItem();
                item.Value = c.Id.ToString();
                item.Text = c.TenLoaiXe;
                item.Selected = (c.Id == model.LoaiXeId);
                return item;
            }).ToList();
            if (_chuyendi.XeVanChuyenId > 0)
            {
                model.XeVanChuyenId = _chuyendi.XeVanChuyenId.Value;
                model.BienSo = _chuyendi.xevanchuyen.BienSo;
            }
            model.AllXeInfo = _xeinfoService.GetAllXeInfoByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var m = new XeXuatBenItemModel.XeVanChuyenInfo(c.Id, c.BienSo);
                m.LoaiXeId = c.LoaiXeId;
                return m;
            }).ToList();
            return PartialView(model);
        }
        /// <summary>
        /// chuyen so do ghe: tao chuyen moi , update nguonveid, sodo,chuyendi
        /// </summary>
        /// <param name="HanhTrinhId"></param>
        /// <param name="NguonVeId"></param>
        /// <param name="NgayDi"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChuyenSoDoghe(int ChuyenId, int XeVanChuyenId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            if (XeVanChuyenId == 0 || ChuyenId == 0)
                return Loi();
            var _chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenId);
            //count so ghe
            var phoives = _phoiveService.GetPhoiVeByChuyenDiId(ChuyenId);
            //kiem tra so ghe trong
            var SlgheOld = _chuyendi.NguonVeInfo.loaixe;
            var xe = _xeinfoService.GetXeInfoById(XeVanChuyenId);

            var loaixe = _xeinfoService.GetById(xe.LoaiXeId);
            //kiem tra so ghe rong 
            if (phoives.Count() > loaixe.sodoghe.SoLuongGhe)
                return Json("Không đủ ghế trống ", JsonRequestBehavior.AllowGet);

            //tao chuyen moi, cung nguonve voi chuyen ban dau
            var chuyendi = new HistoryXeXuatBen();
            chuyendi.NhaXeId = _workContext.NhaXeId;
            chuyendi.HanhTrinhId = _chuyendi.HanhTrinhId;
            chuyendi.XeVanChuyenId = XeVanChuyenId;
            var _nguonve = _hanhtrinhService.GetNguonVeXeById(_chuyendi.NguonVeId);
            // lay nguon ve cung hanh trinh,  gio voi nguon ve hien tai va loai xe da chon
            var nguonvenew = _hanhtrinhService.GetNguonVeXeByloaixe(chuyendi.HanhTrinhId, _nguonve.ThoiGianDi, xe.LoaiXeId);
            if (nguonvenew == null)
                return Json("KhongNguonVe", JsonRequestBehavior.AllowGet);
            chuyendi.NguonVeId = nguonvenew.Id;
            chuyendi.NgayTao = DateTime.Now;
            chuyendi.TrangThai = ENTrangThaiXeXuatBen.CHO_XUAT_BEN;
            chuyendi.NguoiTaoId = _workContext.CurrentNhanVien.Id;
            chuyendi.NgayDi = _chuyendi.NgayDi;
            _nhaxeService.InsertHistoryXeXuatBen(chuyendi);
            //update phoive
            foreach (var item in phoives)
            {
                item.ChuyenDiId = chuyendi.Id;
                //lay so do ghe con trong
                var sdgquytac = _xeinfoService.GetAllSoDoGheXeQuyTac(xe.LoaiXeId).Where(c => c.x > 0 && c.y > 0 && c.Val != "").ToList();
                foreach (var s in sdgquytac)
                {
                    var phoive = _phoiveService.GetPhoiVe(nguonvenew.Id, s, chuyendi.NgayDi, true);
                    if (phoive.TrangThai == ENTrangThaiPhoiVe.ConTrong)
                    {
                        //update phoive
                        // kiem tra chang, loai xe, gia ve
                        var hanhtrinhgiave = _hanhtrinhService.GetallHanhTrinhGiaVe(item.changgiave.HanhTrinhId, item.changgiave.NhaXeId, item.changgiave.DiemDonId, item.changgiave.DiemDenId, xe.LoaiXeId);
                        if (hanhtrinhgiave.Count() > 0)
                        {
                            item.GiaVeHienTai = hanhtrinhgiave.First().GiaVe;
                        }
                        item.SoDoGheXeQuyTacId = phoive.SoDoGheXeQuyTacId;
                        item.NguonVeXeId = nguonvenew.Id;
                        _phoiveService.UpdatePhoiVe(item);

                        break;
                    }
                }
            }
            return ThanhCong();
        }
        #endregion
    }
}