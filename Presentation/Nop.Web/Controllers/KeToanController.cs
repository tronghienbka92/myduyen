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
using System.Data;
using System.Reflection;
using System.IO;
using Nop.Services.ExportImport;

namespace Nop.Web.Controllers
{
    public class KeToanController : BaseNhaXeController
    {
        #region Khoi Tao
        private readonly IStateProvinceService _stateProvinceService;
        private readonly INhaXeService _nhaxeService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IDiaChiService _diachiService;
        private readonly INhanVienService _nhanvienService;
        private readonly IPermissionService _permissionService;
        private readonly IHanhTrinhService _hanhtrinhService;
        private readonly IVeXeService _vexeService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IPhoiVeService _phoiveService;
        private readonly IPhieuGuiHangService _phieuguihangService;
        private readonly IHangHoaService _hanghoaService;
        private readonly IXeInfoService _xeinfoService;
        private readonly IExportManager _exportManager;
        private readonly IGiaoDichKeVeXeService _giaodichkeveService;
        private readonly IKeToanService _ketoanService;

        public KeToanController(IKeToanService ketoanService,
            IGiaoDichKeVeXeService giaodichkeveService,
            IExportManager exportManager,
            IStateProvinceService stateProvinceService,
            INhaXeService nhaxeService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            ICustomerService customerService,
            IDiaChiService diachiService,
            INhanVienService nhanvienService,
            IPermissionService permissionService,
            IHanhTrinhService hanhtrinhService,
             IVeXeService vexeService,
            IPriceFormatter priceFormatter,
            IPhoiVeService phoiveService,
            IPhieuGuiHangService phieuguihangService,
            IHangHoaService hanghoaService,
            IXeInfoService xeinfoService
            )
        {
            this._giaodichkeveService = giaodichkeveService;
            this._exportManager = exportManager;
            this._ketoanService = ketoanService;
            this._hanghoaService = hanghoaService;
            this._phieuguihangService = phieuguihangService;
            this._stateProvinceService = stateProvinceService;
            this._nhaxeService = nhaxeService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._customerService = customerService;
            this._diachiService = diachiService;
            this._nhanvienService = nhanvienService;
            this._permissionService = permissionService;
            this._hanhtrinhService = hanhtrinhService;
            this._vexeService = vexeService;
            this._priceFormatter = priceFormatter;
            this._phoiveService = phoiveService;
            this._xeinfoService = xeinfoService;

        }
        #endregion
        #region Common
        void ThuChiModelPrepare(ThuChiModel model)
        {
            var loaithuchis = _ketoanService.GetAllLoaiThuChi(_workContext.NhaXeId);
            model.loaithuchis = loaithuchis.Select(c =>
            {
                var item = new SelectListItem();
                item.Value = c.Id.ToString();
                item.Text = c.Ten;
                item.Selected = (c.Id == model.LoaiThuChiId);
                return item;
            }).ToList();
        }
        void ThuChiModelToThuChi(ThuChiModel nvfrom, ThuChi nvto)
        {
            nvto.Id = nvfrom.Id;
            //nvto.Ma = nvfrom.Ma;
            //nvto.NgayTao = nvfrom.NgayTao;
            nvto.NgayGiaoDich = nvfrom.NgayGiaoDich;
            
            nvto.LoaiThuChiId = nvfrom.LoaiThuChiId;
            nvto.DienGiai = nvfrom.DienGiai;
            nvto.GiaTri = nvfrom.GiaTri;
            if (nvfrom.NguoiNopId > 0)
                nvto.NguoiNopId = nvfrom.NguoiNopId;
            else
                nvto.NguoiNopId = null;
            if (nvfrom.NguoiThuId > 0)
                nvto.NguoiThuId = nvfrom.NguoiThuId;
            else
                nvto.NguoiThuId = null;
            //nvto.NguoiTaoId = nvfrom.NguoiTaoId;
            nvto.isChi = nvfrom.isChi;
        }
        void ThuChiToThuChiModel(ThuChi nvfrom, ThuChiModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.Ma = nvfrom.Ma;
            nvto.NgayTao = nvfrom.NgayTao;
            nvto.NgayGiaoDich = nvfrom.NgayGiaoDich;
            nvto.ChuyenDiId = nvfrom.ChuyenDiId.GetValueOrDefault(0);
            nvto.thongtinchuyendi = nvfrom.chuyendi != null ? nvfrom.chuyendi.toMoTa() : "";
            nvto.NgayDi = nvfrom.chuyendi != null ? nvfrom.chuyendi.NgayDi : DateTime.Now;
            nvto.LoaiThuChiId = nvfrom.LoaiThuChiId;
            nvto.tenloaithuchi = nvfrom.loaithuchi.Ten;
            nvto.DienGiai = nvfrom.DienGiai;
            nvto.GiaTri = nvfrom.GiaTri;
            nvto.NguoiNopId = nvfrom.NguoiNopId.GetValueOrDefault(0);
            nvto.tennguoinop = nvfrom.nguoinop != null ? nvfrom.nguoinop.HoVaTen : "";
            nvto.NguoiThuId = nvfrom.NguoiThuId.GetValueOrDefault(0);
            nvto.tennguoithu = nvfrom.nguoithu != null ? nvfrom.nguoithu.HoVaTen : "";
            nvto.NguoiTaoId = nvfrom.NguoiTaoId;
            nvto.tennguoitao = nvfrom.nguoitao.HoVaTen;
            nvto.isChi = nvfrom.isChi;
            nvto.isChiText = nvfrom.isChi ? "Chi" : "Thu";
        }
        #endregion
        #region Giao dich thu chi
        public ActionResult LoaiThuChiTimKiem(string Ten)
        {
            var items = _ketoanService.GetAllLoaiThuChi(_workContext.NhaXeId).Where(c => c.Ten.Contains(Ten)).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult List()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new QLThuChiModel();                        
            return View(model);
        }
        [HttpPost]
        public ActionResult _DanhSachThuChi(QLThuChiModel model)
        {
           bool? isChi=null;
            if(model.isChi>=0)
            {
                isChi=model.isChi==1?true : false;
            }
            model.TonDauKy = _ketoanService.GetTonDauKy(_workContext.NhaXeId, model.TuNgay);
            var danhsachs = _ketoanService.GetAllThuChi(_workContext.NhaXeId, model.LoaiThuChiId, isChi, model.TuNgay, model.DenNgay, model.KeySearch);
            model.thuchis = danhsachs.Select(c =>
            {
                var m = new ThuChiModel();
                ThuChiToThuChiModel(c, m);
                return m;
            }).ToList();
            return PartialView(model);
        }
        public ActionResult _ChiTiet(int Id)
        {
            var model = new ThuChiModel();
            var item = _ketoanService.GetThuChiById(Id);
            ThuChiToThuChiModel(item, model);
            return PartialView(model);
        }

        public ActionResult ThuChiTao(bool? isChi)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var model = new ThuChiModel();
            ThuChiModelPrepare(model);
            model.isChi = isChi.HasValue ?isChi.Value: true;
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult ThuChiTao(ThuChiModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                //kiem tra loai thu chi
                if(model.LoaiThuChiId==0)
                {
                    if(string.IsNullOrEmpty(model.tenloaithuchi))
                    {
                        ErrorNotification("Tên loại thu chi không được trống");
                        return View(model);
                    }
                    var ltc = new LoaiThuChi();
                    ltc.Ten = model.tenloaithuchi;
                    ltc.NhaXeId = _workContext.NhaXeId;
                    _ketoanService.Insert(ltc);
                    model.LoaiThuChiId = ltc.Id;
                }
                var item = new ThuChi();
                ThuChiModelToThuChi(model, item);
                item.NgayTao = DateTime.Now;
                item.NguoiTaoId = _workContext.CurrentNhanVien.Id;
                item.NhaXeId = _workContext.NhaXeId;
                _ketoanService.Insert(item);
                SuccessNotification("Tạo mới thành công");
                return continueEditing ? RedirectToAction("ThuChiSua", new { id = item.Id }) : RedirectToAction("List");
            }
            ThuChiModelPrepare(model);
            return View(model);
        }
        public ActionResult ThuChiSua(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var item = _ketoanService.GetThuChiById(id);
            if (item == null)
                return RedirectToAction("List");
            var model = new ThuChiModel();
            ThuChiToThuChiModel(item, model);
            //default values           
            ThuChiModelPrepare(model);

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult ThuChiSua(ThuChiModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var item = _ketoanService.GetThuChiById(model.Id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                if (model.LoaiThuChiId == 0)
                {
                    if (string.IsNullOrEmpty(model.tenloaithuchi))
                    {
                        ErrorNotification("Tên loại thu chi không được trống");
                        return View(model);
                    }
                    var ltc = new LoaiThuChi();
                    ltc.Ten = model.tenloaithuchi;
                    ltc.NhaXeId = _workContext.NhaXeId;
                    _ketoanService.Insert(ltc);
                    model.LoaiThuChiId = ltc.Id;
                }

                ThuChiModelToThuChi(model, item);
                _ketoanService.Update(item);
                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("ThuChiSua", item.Id);
                }
                return RedirectToAction("List");
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult ThuChiXoa(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var item = _ketoanService.GetThuChiById(id);
            if (item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("List");

            _ketoanService.Delete(item);

            return RedirectToAction("List");
        }
        #endregion
    }
}