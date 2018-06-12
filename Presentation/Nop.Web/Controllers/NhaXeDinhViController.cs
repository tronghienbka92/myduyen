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


namespace Nop.Web.Controllers
{
    public class NhaXeDinhViController : BaseNhaXeController
    {
        #region Khoi Tao
        private readonly IStateProvinceService _stateProvinceService;
        private readonly INhaXeService _nhaxeService;
        private readonly IPhoiVeService _phoiveService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly IPhieuGuiHangService _phieuguihangService;
        private readonly IHangHoaService _hanghoaService;
        private readonly ICustomerService _customerService;
        private readonly IDiaChiService _diachiService;
        private readonly INhanVienService _nhanvienService;
        private readonly IPermissionService _permissionService;
        private readonly IXeInfoService _xeinfoService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IHanhTrinhService _hanhtrinhService;
        private readonly IBenXeService _benxeService;
        private readonly INhaXeCustomerService _nhaxecustomerService;
        public NhaXeDinhViController(IStateProvinceService stateProvinceService,
            INhaXeService nhaxeService,
             IPhoiVeService phoiveService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IPriceFormatter priceFormatter,
            IPictureService pictureService,
             IPhieuGuiHangService phieuguihangService,
            IHangHoaService hanghoaService,
            ICustomerService customerService,
            IDiaChiService diachiService,
            INhanVienService nhanvienService,
            IPermissionService permissionService,
            IXeInfoService xeinfoService,
            IHanhTrinhService hanhtrinhService,
            IBenXeService benxeService,
            INhaXeCustomerService nhaxecustomerService
            )
        {
            this._stateProvinceService = stateProvinceService;
            this._nhaxeService = nhaxeService;
            this._phoiveService = phoiveService;
            this._priceFormatter = priceFormatter;
            this._hanghoaService = hanghoaService;
            this._phieuguihangService = phieuguihangService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._customerService = customerService;
            this._diachiService = diachiService;
            this._nhanvienService = nhanvienService;
            this._permissionService = permissionService;
            this._xeinfoService = xeinfoService;
            this._hanhtrinhService = hanhtrinhService;
            this._benxeService = benxeService;
            this._nhaxecustomerService = nhaxecustomerService;
        }
        #endregion
        #region NonAction
        [NonAction]
        protected virtual string GetLabel(string _name)
        {
            return _localizationService.GetResource(string.Format("ChonVe.NhaXe.{0}", _name));
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
            nvto.Latitude = nvfrom.Latitude;
            nvto.Longitude = nvfrom.Longitude;
            nvto.NgayGPSText = nvfrom.NgayGPS.ToString();
        }
        #endregion
        #region Dinh vi xe
        public ActionResult BanDoDinhViXe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new BanDoDinhViXeModel();           
            return View(model);
        }
        public ActionResult ThongTinPhoiVe(int NguonVeXeId, string ngaydi)
        {

            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var model = new DinhViXeModel();
            var _ngaydi = Convert.ToDateTime(ngaydi);
            model.NguonVeXeId = NguonVeXeId;
            var _nguonve = _hanhtrinhService.GetNguonVeXeById(NguonVeXeId);
            if (_nguonve != null)
            {
                model.GioDiText = _nguonve.ThoiGianDi.ToString("HH:mm");
                model.GioDenText = _nguonve.ThoiGianDen.ToString("HH:mm");
                model.TuyenXeChay = _nguonve.GetHanhTrinh();
            }
            var _historyxexuatben = _nhaxeService.GetHistoryXeXuatBenByNguonVeId(NguonVeXeId, _ngaydi);
            if (_historyxexuatben != null)
            {
                model.TenLaiXe1 = _historyxexuatben.ThongTinLaiPhuXe();
                model.TenLaiXe2 = _historyxexuatben.ThongTinLaiPhuXe(1);
                model.TenLaiXe3 = _historyxexuatben.ThongTinLaiPhuXe(2);
                model.BienSo = _historyxexuatben.xevanchuyen.BienSo;
            }

            model.NgayDi = _ngaydi;
            model.SoNguoi = _phoiveService.GetAllSoNguoi(NguonVeXeId, _ngaydi);
            model.Revenue = _phoiveService.GetRevenueHistoryXeXuatBen(NguonVeXeId, _ngaydi).ToTien(_priceFormatter);
            return View(model);
        }
         public ActionResult GetListXeInfo(string ThongTin)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var arrxevanchuyen = _xeinfoService.GetAllXeInfoByNhaXeId(_workContext.CurrentNhaXe.Id).Where(c => c.Longitude>0 && c.Latitude>0 & c.BienSo.Contains(ThongTin));
            var xeids = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, DateTime.Now).Where(c => c.XeVanChuyenId > 0 && c.LaiPhuXes.Any(t=>t.nhanvien.HoVaTen.Contains(ThongTin))).Select(c => c.XeVanChuyenId.GetValueOrDefault(0)).ToArray();
             if(xeids.Count()>0)
             {
                 arrxevanchuyen = arrxevanchuyen.Where(c => xeids.Contains(c.Id));
             }
           var arrxeinfo = arrxevanchuyen.Select(c =>
            {
                var _item = new XeInfoModel();
                XeInfoToXeInfoModel(c, _item);
                var _historyxexuatben = _xeinfoService.DinhVi_GetHistoryXeXuatBenByXeVanChuyen(c.Id);
                if (_historyxexuatben!=null)
                {
                    _item.NguonVeXeId = _historyxexuatben.NguonVeId;
                    _item.NgayDi = _historyxexuatben.NgayDi.ToString();
                }
               
                return _item;
            }).ToList();
           return Json(arrxeinfo, JsonRequestBehavior.AllowGet);

        }
         public ActionResult GetLatlogNew()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var arrxevanchuyen = _xeinfoService.GetAllXeInfoByNhaXeId(_workContext.CurrentNhaXe.Id);
            var arrxeinfo = arrxevanchuyen.Select(c =>
            {
                var _item = new XeInfoModel();
                XeInfoToXeInfoModel(c, _item);               

                return _item;
            }).ToList();
            return Json(arrxeinfo, JsonRequestBehavior.AllowGet);
        }
       
         [AcceptVerbs(HttpVerbs.Get)]
         public ActionResult GetSoDoGheXeInfo(int NguonVeXeId, string NgayDi, int? TangIndex)
         {

             if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
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
             SoDoGheXeToSoDoGheXeModel(sodoghe, modelsodoghe);
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
                                 int idkhachhang = ViTriGhe.Info.customer.Id;
                                 if (idkhachhang == CommonHelper.KhachVangLaiId)
                                 {
                                     var _khachhang = _customerService.GetCustomerById(idkhachhang);
                                     ViTriGhe.TenKhachHang = _khachhang.GetFullName();
                                     ViTriGhe.SoDienThoai = "";
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
                                 int idkhachhang = ViTriGhe.Info.customer.Id;
                                 if (idkhachhang == CommonHelper.KhachVangLaiId)
                                 {
                                     var _khachhang = _customerService.GetCustomerById(idkhachhang);
                                     ViTriGhe.TenKhachHang = _khachhang.GetFullName();
                                     ViTriGhe.SoDienThoai = "";
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

                             }

                         }
                     }
                 }
             }
             //selected tab
             SaveSelectedTabIndex(TangIndex);
             return PartialView(modelsodoghe);
         }
        #endregion
    }
}