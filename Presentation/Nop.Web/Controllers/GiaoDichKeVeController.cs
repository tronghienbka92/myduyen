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
    public class GiaoDichKeVeController : BaseNhaXeController
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

        public GiaoDichKeVeController(
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
            IGiaoDichKeVeXeService giaodichkeveService
            )
        {
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

        }
        #endregion
        #region Cac ham chung
        GiaoDichKeVeModel toModel(GiaoDichKeVe entity)
        {
            if (entity == null) return null;
            var model = new GiaoDichKeVeModel();
            model.Id = entity.Id;
            model.PhanLoaiId = entity.PhanLoaiId;
            model.PhanLoaiText = entity.PhanLoai.ToCVEnumText(_localizationService);
            model.Ma = entity.Ma;
            model.NgayKe = entity.NgayKe;
            model.NgayTao = entity.NgayTao;
            model.GhiChu = entity.GhiChu;
            model.NguoiGiaoId = entity.NguoiGiaoId;
            if (entity.nguoigiao != null)
                model.tennguoigiao = entity.nguoigiao.HoVaTen;
            model.NguoiNhanId = entity.NguoiNhanId.GetValueOrDefault(0);

            if (entity.nguoinhan != null)
                model.tennguoinhan = entity.nguoinhan.HoVaTen;
            model.VanPhongId = entity.VanPhongId.GetValueOrDefault(0);
            if (entity.quaybanve != null)
                model.VanPhongText = entity.quaybanve.TenVanPhong;
            model.LoaiVe = entity.LoaiVe;
            model.LoaiVeText = entity.LoaiVe.ToCVEnumText(_localizationService);
            model.TrangThai = entity.TrangThai;
            model.TrangThaiText = entity.TrangThai.ToCVEnumText(_localizationService);
            model.SessionId = entity.SessionId;
            model.NhaXeId = entity.NhaXeId;
            if (entity.nhaxe != null)
                model.TenNhaXe = entity.nhaxe.TenNhaXe;
            if (model.TrangThai == ENGiaoDichKeVeTrangThai.DANG_CHINH_SUA)
                model.isEdit = true;
            else if (model.TrangThai == ENGiaoDichKeVeTrangThai.HOAN_THANH)
            {
                //neu la admin thi co the sua
                if (this.isQuanLyAccess(_workContext))
                    model.isEdit = true;

            }

            return model;
        }
        GiaoDichKeVeMenhGiaModel toModel(GiaoDichKeVeMenhGia entity, int STT)
        {
            var model = new GiaoDichKeVeMenhGiaModel();
            model.Id = entity.Id;
            model.STT = STT;
            model.MenhGiaId = entity.MenhGiaId;
            model.NguoiNhanId = entity.NguoiNhanId;
            model.VanPhongId = entity.VanPhongId.GetValueOrDefault(0);
            model.LoaiVeId = entity.LoaiVeId;
            model.MenhGia = entity.menhgia.MenhGia;
            model.SoLuong = entity.SoLuong;
            model.SeriFrom = entity.SeriFrom;
            if (model.SoLuong > 0)
                model.SeriTo = string.Format("{0}", entity.SeriNumFrom + entity.SoLuong - 1).PadLeft(7, '0');
            if (string.IsNullOrEmpty(model.SeriFrom))
                model.SeriTo = "";
            model.isVeMoi = entity.isVeMoi;
            model.GhiChu = entity.GhiChu;
            model.ActionType = entity.ActionType;
            model.QuanLyMauVeKyHieuId = entity.QuanLyMauVeKyHieuId;
            model.isVeDi = entity.isVeDi;
            if (entity.quanly != null)
                model.MauVe = string.Format("{0}-{1}", entity.quanly.MauVe, entity.quanly.KyHieu);
            return model;
        }

        #endregion
        #region Giao dich ke ve
        public ActionResult List()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return AccessDeniedView();
            var model = new GiaoDichKeVeListModel();
            model.PhanLoaiId = -1;
            model.phanloais = this.GetCVEnumSelectList<ENGiaoDichKeVePhanLoai>(_localizationService, -1);
            model.phanloais.Insert(0, new SelectListItem
            {
                Value = "-1",
                Text = "---Loại giao dịch---",
                Selected = true
            });
            return View(model);
        }
        [HttpPost]
        public ActionResult ListGiaoDichKeVe(GiaoDichKeVeListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return null;
            var giaodichs = _giaodichkeveService.GetAllGiaoDichKeVe(_workContext.NhaXeId, model.PhanLoaiId, model.MaGiaoDich, model.NguoiGiaoId, model.NguoiNhanId, model.TuNgay, model.DenNgay);
            var models = giaodichs.Select(c =>
            {
                return toModel(c);
            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = models,
                Total = models.Count
            };
            return Json(gridModel);
        }

        // xem chi tiet giao dich ve menh gia
        public ActionResult ChiTietGiaoDich(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return AccessDeniedView();
            var giaodichkeve = _giaodichkeveService.GetGiaoDichKeVeById(id);
            var model = toModel(giaodichkeve);
            return PartialView(model);

        }

        [HttpPost]
        public ActionResult ListGiaoDichKeVeMenhGia(int GiaoDichId, int isVeDi)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return AccessDeniedView();
            var item = _giaodichkeveService.GetGiaoDichKeVeById(GiaoDichId);
            if (item == null)
                return null;
            bool _isVeDi = isVeDi == 1;
            int STT = 0;
            var models = item.kevemenhgias.Where(c => c.isVeDi == _isVeDi).OrderBy(c => c.menhgia.MenhGia).ThenBy(d => d.Id).Select(c =>
            {
                STT++;
                return toModel(c, STT);
            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = models,
                Total = models.Count
            };
            return Json(gridModel);
        }
        public ActionResult CBBNhanVienNhanVe(string SearchKhachhang)
        {
            var khachhangs = _nhanvienService.GetAllForGiaoDichKeVe(0, SearchKhachhang, _workContext.NhaXeId).Select(c =>
            {
                var item = new CustomerNhaXeModel();
                item.Id = c.Id;
                item.HoTen = c.ThongTin(false);
                return item;
            }).ToList();

            return Json(khachhangs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InPhieuKeVe(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return AccessDeniedView();
            var giaodichkeve = _giaodichkeveService.GetGiaoDichKeVeById(id);
            var model = toModel(giaodichkeve);
            int STT = 0;
            var veluotdi = giaodichkeve.kevemenhgias.OrderBy(c => c.menhgia.MenhGia).ThenBy(d => d.Id).Select(c =>
            {
                STT++;
                return toModel(c, STT);
            }).ToList();           
            model.veluotdi = veluotdi;
            return View(model);

        }

        public ActionResult TaoKeVe(int? id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return AccessDeniedView();
            var item = _giaodichkeveService.GetGiaoDichKeVeById(id.GetValueOrDefault(0));
            //kiem tra neu la admin va trang thai giao dich dang la hoan thanh thi co the sua
            if (item != null && item.PhanLoai == ENGiaoDichKeVePhanLoai.KE_VE && item.TrangThai == ENGiaoDichKeVeTrangThai.HOAN_THANH && this.isQuanLyAccess(_workContext))
            {
                //cho phep sua
                //do nothing
            }
            else
                if (item == null || (item != null && item.TrangThai == ENGiaoDichKeVeTrangThai.HUY))
                {
                    item = new GiaoDichKeVe();
                    //tao thong tin tam, o trang thai moi tao
                    item.SessionId = Guid.NewGuid().ToString();
                    item.NguoiGiaoId = _workContext.CurrentNhanVien.Id;
                    item.NguoiNhanId = null;
                    item.VanPhongId = null;
                    item.LoaiVeId = null;
                    item.NgayKe = DateTime.Now;
                    item.NhaXeId = _workContext.NhaXeId;
                    item.PhanLoai = ENGiaoDichKeVePhanLoai.KE_VE;
                    item = _giaodichkeveService.Insert(item);

                }
            if (item.PhanLoai == ENGiaoDichKeVePhanLoai.TRA_VE)
            {
                return RedirectToAction("TraVe", new { id = item.Id });
            }
            var model = toModel(item);
            model.VanPhongs = _workContext.CurrentNhanVien.VanPhongs.Select(c => new SelectListItem
            {
                Text = c.TenVanPhong,
                Value = c.Id.ToString(),
                Selected = c.Id == model.VanPhongId
            }).ToList();
            model.VanPhongs.Insert(0, new SelectListItem { Text = "---Chọn quầy bán vé---", Value = "0" });
            model.ddlLoaiVes = this.GetCVEnumSelectList<ENLoaiVeXeItem>(_localizationService, model.LoaiVeId);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult TaoKeVe(GiaoDichKeVeModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return AccessDeniedView();
            var item = _giaodichkeveService.GetGiaoDichKeVeById(model.Id);
            if (ModelState.IsValid)
            {
                //giao dich da hoan thanh, nhung do co loi trong qua trinh ke ve, nen co the thuc hien lai
                if (continueEditing == false || item.TrangThai == ENGiaoDichKeVeTrangThai.HOAN_THANH)
                {
                    item.TrangThai = ENGiaoDichKeVeTrangThai.HOAN_THANH;
                }
                else
                {
                    item.NgayKe = model.NgayKe;
                    item.TrangThai = ENGiaoDichKeVeTrangThai.DANG_CHINH_SUA;
                    item.NguoiNhanId = model.NguoiNhanId;
                    if (model.VanPhongId > 0)
                        item.VanPhongId = model.VanPhongId;
                    else
                        item.VanPhongId = null;
                    item.LoaiVeId = model.LoaiVeId;
                }

                _giaodichkeveService.Update(item);

                if (item.TrangThai == ENGiaoDichKeVeTrangThai.HOAN_THANH)
                {
                    _giaodichkeveService.FinishGiaoDichKeVe(item.Id);
                }
                SuccessNotification("Cập nhật thông tin giao dịch kê vé thành công!");
                _customerActivityService.InsertActivityNhaXe("Tạo mới giao dịch kê vé xe : {0}", item.Ma);
                return continueEditing ? RedirectToAction("TaoKeVe", new { id = item.Id }) : RedirectToAction("List");
            }

            model = toModel(item);

            return View(model);
        }

        public ActionResult TraVe(int? id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return AccessDeniedView();
            var item = _giaodichkeveService.GetGiaoDichKeVeById(id.GetValueOrDefault(0));
            if (item == null || (item != null && (item.TrangThai == ENGiaoDichKeVeTrangThai.HUY || item.TrangThai == ENGiaoDichKeVeTrangThai.HOAN_THANH)))
            {
                item = new GiaoDichKeVe();
                //tao thong tin tam, o trang thai moi tao
                item.SessionId = Guid.NewGuid().ToString();
                item.NguoiGiaoId = _workContext.CurrentNhanVien.Id;
                item.NguoiNhanId = _workContext.CurrentNhanVien.Id;
                item.VanPhongId = null;
                item.LoaiVeId = null;
                item.NgayKe = DateTime.Now;
                item.NhaXeId = _workContext.NhaXeId;
                item.PhanLoai = ENGiaoDichKeVePhanLoai.TRA_VE;
                item = _giaodichkeveService.Insert(item);

            }
            if (item.PhanLoai == ENGiaoDichKeVePhanLoai.KE_VE)
            {
                return RedirectToAction("TaoKeVe", new { id = item.Id });
            }
            var model = toModel(item);
            if (!id.HasValue)
            {
                model.tennguoigiao = "";
                model.NguoiGiaoId = 0;
            }
            model.ddlLoaiVes = this.GetCVEnumSelectList<ENLoaiVeXeItem>(_localizationService, model.LoaiVeId);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult TraVe(GiaoDichKeVeModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return AccessDeniedView();
            var item = _giaodichkeveService.GetGiaoDichKeVeById(model.Id);
            if (ModelState.IsValid)
            {

                if (continueEditing == false)
                {
                    item.TrangThai = ENGiaoDichKeVeTrangThai.HOAN_THANH;
                }
                else
                {
                    item.NgayKe = model.NgayKe;
                    item.TrangThai = ENGiaoDichKeVeTrangThai.DANG_CHINH_SUA;
                    item.NguoiGiaoId = model.NguoiGiaoId;
                    item.LoaiVeId = model.LoaiVeId;
                }


                _giaodichkeveService.Update(item);

                if (item.TrangThai == ENGiaoDichKeVeTrangThai.HOAN_THANH)
                {
                    _giaodichkeveService.FinishGiaoDichKeVe(item.Id);
                }
                SuccessNotification("Cập nhật thông tin giao dịch trả vé thành công!");
                _customerActivityService.InsertActivityNhaXe("Tạo mới giao dịch trả vé xe : {0}", item.Ma);
                return continueEditing ? RedirectToAction("TraVe", new { id = item.Id }) : RedirectToAction("List");
            }

            model = toModel(item);

            return View(model);
        }

        [HttpPost]
        public ActionResult XoaKeVe(int id)
        {

            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return AccessDeniedView();

            var item = _giaodichkeveService.GetGiaoDichKeVeById(id);
            if (item == null)
                return RedirectToAction("List");
            item.TrangThai = ENGiaoDichKeVeTrangThai.HUY;
            _giaodichkeveService.Update(item);
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult TraVeTheoMenhGia(int id)
        {
            var kevemenhgia = _giaodichkeveService.GetGiaoDichKeVeMenhGiaById(id);
            _customerActivityService.InsertActivityNhaXe("Trả vé theo mệnh giá {3}: '{0}:{1} ->SL: {2}'", kevemenhgia.QuanLyMauVeKyHieuId, kevemenhgia.SeriFrom, kevemenhgia.SoLuong, kevemenhgia.menhgia.MenhGia);
            _giaodichkeveService.TraVeTheoMenhGia(id);
            return Json("ok");
        }
        public ActionResult _ChuyenVeTheoMenhGia(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return null;
            var model = new GiaoDichKeVeMenhGiaModel();
            model.Id = id;
            if (id > 0)
            {
                var giaodichkevemenhgia = _giaodichkeveService.GetGiaoDichKeVeMenhGiaById(id);
                model = toModel(giaodichkevemenhgia, 0);
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ChuyenVeTheoMenhGia(int id, int NhanVienNhanId)
        {
            if (id > 0)
            {
                var kevemenhgia = _giaodichkeveService.GetGiaoDichKeVeMenhGiaById(id);
                _customerActivityService.InsertActivityNhaXe("Chuyển vé theo mệnh giá {3}: '{0}:{1} ->SL: {2}' cho nhân viên id={4}", kevemenhgia.QuanLyMauVeKyHieuId, kevemenhgia.SeriFrom, kevemenhgia.SoLuong, kevemenhgia.menhgia.MenhGia, NhanVienNhanId);
                _giaodichkeveService.ChuyenVeTheoMenhGia(id, NhanVienNhanId);
            }
            else if (id < 0)
            {
                var giaodichtrave = _giaodichkeveService.GetGiaoDichKeVeById(Math.Abs(id));
                if (giaodichtrave == null)
                    return Loi();
                _customerActivityService.InsertActivityNhaXe("Chuyển hết vé của nhân viên '{0}' cho nhân viên id={1} (theo giao dịch: {2}) ", giaodichtrave.nguoigiao.HoVaTen, NhanVienNhanId, giaodichtrave.Ma);
                _giaodichkeveService.ChuyenVeTheoMenhGia(id, NhanVienNhanId);
            }
            return Json("ok");
        }
        [HttpPost]
        public ActionResult _GetVeChoNhanVienHienTai(int isVeDi)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return null;

            bool _isVeDi = isVeDi == 1;
            List<GiaoDichKeVeMenhGia> items = new List<GiaoDichKeVeMenhGia>();
            var menhgias = _giaodichkeveService.GetAllMenhGia(_workContext.NhaXeId);
            //tao thong tin ve di
            foreach (var mg in menhgias)
            {
                //kiem tra thong tin ton trc do
                var menhgiakeves = _giaodichkeveService.GetTonGiaoDichKeVeMenhGia(_workContext.CurrentNhanVien.Id, mg.Id, _isVeDi, _workContext.CurrentVanPhong.Id, (int)ENLoaiVeXeItem.VeQuay);

                foreach (var mgkv in menhgiakeves)
                {
                    items.Add(mgkv);
                }

            }
            int STT = 0;
            var models = items.Select(c =>
            {
                return toModel(c, STT);
            }).ToList();


            var gridModel = new DataSourceResult
            {
                Data = models,
                Total = models.Count
            };
            return Json(gridModel);
        }



        [HttpPost]
        public ActionResult ListKeVeMenhGia(int GiaoDichId, int isVeDi, int NguoiNhanId, int VanPhongId, int LoaiVeId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return null;
            var item = _giaodichkeveService.GetGiaoDichKeVeById(GiaoDichId);
            if (item == null)
                return null;
            bool _isVeDi = isVeDi == 1;
            var models = item.kevemenhgias.Where(c => c.isVeDi == _isVeDi);
            //kiem tra thong tin co ton tai menh gia trong giao dich nay khong
            //neu chua ton tai thi tao ra thong tin giao dich ke ve menh gia
            if (models.Count() == 0)
            {
                ////tao thong tin bang menh gia
                ////lay thong tin menh gia
                var menhgias = _giaodichkeveService.GetAllMenhGia(_workContext.NhaXeId);
                //tao thong tin ve di
                foreach (var mg in menhgias)
                {
                    //kiem tra thong tin ton trc do
                    var menhgiakeves = _giaodichkeveService.GetTonGiaoDichKeVeMenhGia(NguoiNhanId, mg.Id, _isVeDi, VanPhongId, LoaiVeId);
                    if (menhgiakeves.Count == 0)
                    {
                        //neu khong co thi tao moi
                        var menhgiakeve = new GiaoDichKeVeMenhGia();
                        menhgiakeve.GiaoDichKeVeId = item.Id;
                        menhgiakeve.MenhGiaId = mg.Id;
                        menhgiakeve.isVeDi = _isVeDi;
                        menhgiakeve.ActionType = ENGiaoDichKeVeMenhGiaAction.SUA;
                        menhgiakeve.SoLuong = 100;
                        menhgiakeve.SeriFrom = "";
                        menhgiakeve.NguoiNhanId = NguoiNhanId;
                        menhgiakeve.VanPhongId = VanPhongId;
                        menhgiakeve.LoaiVeId = LoaiVeId;
                        menhgiakeve.isVeMoi = true; //true la ve moi
                        _giaodichkeveService.InsertGiaoDichKeVeMenhGia(menhgiakeve);
                    }
                    else
                    {
                        foreach (var mgkv in menhgiakeves)
                        {
                            mgkv.GiaoDichKeVeId = item.Id;
                            _giaodichkeveService.InsertGiaoDichKeVeMenhGia(mgkv);
                        }
                    }
                }

            }

            int STT = 0;


            var arritem = models
           .OrderBy(c=>c.menhgia.MenhGia).ThenBy(d => d.Id).Select(c =>
           {
               STT++;
               return toModel(c, STT);
           }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = arritem,
                Total = arritem.Count
            };
            return Json(gridModel);
        }


        [NonAction]
        protected virtual void GiaoDichKeVeMenhGiaModelToEntity(GiaoDichKeVeMenhGia ent, GiaoDichKeVeMenhGiaModel model)
        {
            ent.Id = model.Id;
            ent.MenhGiaId = model.MenhGiaId;
            ent.SoLuong = model.SoLuong;
            ent.SeriFrom = model.SeriFrom;
            ent.GhiChu = model.GhiChu;
            ent.NguoiNhanId = model.NguoiNhanId;
            ent.isVeDi = model.isVeDi;
        }
        [NonAction]
        protected virtual void PrepareForListMauVeKyHieu(GiaoDichKeVeMenhGiaModel model)
        {
            var mauso = _giaodichkeveService.GetAllMauVeKyHieu(_workContext.NhaXeId);
            model.mauves = mauso.Select(c =>
            {
                return new SelectListItem { Value = c.Id.ToString(), Text = string.Format("{0} -- {1}", c.MauVe, c.KyHieu), Selected = c.Id == model.QuanLyMauVeKyHieuId };
            }).ToList();

        }

        //sua thong tin giao dich ke ve menh gia khi them moi
        public ActionResult SuaKeVeMenhGia(int id, int isNew)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return null;
            var giaodichkevemenhgia = _giaodichkeveService.GetGiaoDichKeVeMenhGiaById(id);
            if (giaodichkevemenhgia == null)
                return AccessDeniedView();

            var model = toModel(giaodichkevemenhgia, 0);
            model.isNew = isNew;
            if (isNew == 1)
            {
                model.SoLuong = 100;
                model.SeriFrom = "";
                model.SeriTo = "";
            }

            PrepareForListMauVeKyHieu(model);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult UpdateGiaoDichKeVeMenhGia(GiaoDichKeVeMenhGiaModel model)
        {
            var giaodichkeve = _giaodichkeveService.GetGiaoDichKeVeMenhGiaById(model.Id);
            var mauvekyhieu = _giaodichkeveService.GetMauVeById(model.QuanLyMauVeKyHieuId.GetValueOrDefault(0));
            if (model.isNew == 0)
            {
                giaodichkeve.GhiChu = model.GhiChu;
                giaodichkeve.SoLuong = model.SoLuong;
                giaodichkeve.SeriFrom = model.SeriFrom.PadLeft(7, '0');
                giaodichkeve.QuanLyMauVeKyHieuId = model.QuanLyMauVeKyHieuId;
                //kiem tra ton tai so seri nay trong csdl hay chua

                if (_giaodichkeveService.isExistVeXeItem(_workContext.NhaXeId, mauvekyhieu.MauVe, mauvekyhieu.KyHieu, giaodichkeve.SeriFrom, giaodichkeve.SoLuong))
                    return Json("false");
                bool ret = false;
                if (model.Id > 0)
                    ret = _giaodichkeveService.UpdateGiaoDichKeVeMenhGia(giaodichkeve);
                else
                    ret = _giaodichkeveService.InsertGiaoDichKeVeMenhGia(giaodichkeve);
                if (ret)
                {
                    return Json("ok");
                }
                return Json("false");
            }

            var item = new GiaoDichKeVeMenhGia();
            item.NguoiNhanId = giaodichkeve.NguoiNhanId;
            item.VanPhongId = giaodichkeve.VanPhongId;
            item.LoaiVeId = giaodichkeve.LoaiVeId;
            item.GiaoDichKeVeId = giaodichkeve.GiaoDichKeVeId;
            item.MenhGiaId = giaodichkeve.MenhGiaId;
            item.isVeDi = giaodichkeve.isVeDi;
            item.SoLuong = model.SoLuong;
            item.SeriFrom = model.SeriFrom.PadLeft(7, '0');
            item.GhiChu = model.GhiChu;
            item.QuanLyMauVeKyHieuId = model.QuanLyMauVeKyHieuId;
            item.ActionType = ENGiaoDichKeVeMenhGiaAction.SUA_VA_XOA;
            item.isVeMoi = true;
            if (_giaodichkeveService.isExistVeXeItem(_workContext.NhaXeId, mauvekyhieu.MauVe, mauvekyhieu.KyHieu, item.SeriFrom, item.SoLuong))
                return Json("false");

            var kq = _giaodichkeveService.InsertGiaoDichKeVeMenhGia(item);
            if (kq)
            {
                _customerActivityService.InsertActivityNhaXe("{0}", "khách hàng" + _workContext.CurrentCustomer.Id + "kê vé mệnh giá từ seri " + item.SeriFrom + " với số lượng " + item.SoLuong);
                return Json("ok");
            }
            return Json("false");
        }


        //them moi giao dich ke ve menh gia 
        public ActionResult CreateGiaoDichKeVeMenhGia(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return null;
            var giaodichkevemenhgia = _giaodichkeveService.GetGiaoDichKeVeMenhGiaById(id);

            var model = toModel(giaodichkevemenhgia, 0);
            model.SoLuong = 100;
            PrepareForListMauVeKyHieu(model);

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult CreateKeVeMenhGia(GiaoDichKeVeMenhGiaModel model)
        {
            var giaodichkeve = _giaodichkeveService.GetGiaoDichKeVeMenhGiaById(model.Id);

            var item = new GiaoDichKeVeMenhGia();
            item.NguoiNhanId = giaodichkeve.NguoiNhanId;
            item.VanPhongId = giaodichkeve.VanPhongId;
            item.LoaiVeId = giaodichkeve.LoaiVeId;
            item.GiaoDichKeVeId = giaodichkeve.GiaoDichKeVeId;
            item.MenhGiaId = giaodichkeve.MenhGiaId;
            item.isVeDi = giaodichkeve.isVeDi;
            item.SoLuong = model.SoLuong;
            item.SeriFrom = model.SeriFrom;
            item.GhiChu = model.GhiChu;
            item.QuanLyMauVeKyHieuId = model.QuanLyMauVeKyHieuId;
            item.ActionType = ENGiaoDichKeVeMenhGiaAction.SUA_VA_XOA;
            item.isVeMoi = true;
            var mauvekyhieu = _giaodichkeveService.GetMauVeById(model.QuanLyMauVeKyHieuId.GetValueOrDefault(0));
            if (_giaodichkeveService.isExistVeXeItem(_workContext.NhaXeId, mauvekyhieu.MauVe, mauvekyhieu.KyHieu, item.SeriFrom, item.SoLuong))
                return Json("false");

            var kq = _giaodichkeveService.InsertGiaoDichKeVeMenhGia(item);
            if (kq)
            {
                _customerActivityService.InsertActivityNhaXe("{0}", "khách hàng" + _workContext.CurrentCustomer.Id + "kê vé mệnh giá từ seri " + item.SeriFrom + " với số lượng " + item.SoLuong);
                return Json("ok");
            }

            return Json("false");
        }
        //tieu ve
        public ActionResult TieuVe(int id, int NhanvienId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe))
                return null;
            var giaodichkevemenhgia = _giaodichkeveService.GetGiaoDichKeVeMenhGiaById(id);


            var model = new GiaoDichKeVeMenhGiaModel();

            model.MenhGiaId = giaodichkevemenhgia.MenhGiaId;
            model.MenhGia = giaodichkevemenhgia.menhgia.MenhGia;

            model.NguoiNhanId = NhanvienId;
            return PartialView(model);
        }
        [HttpPost]

        public ActionResult TieuVe(GiaoDichKeVeMenhGiaModel model)
        {



            var giaodichkeve = _giaodichkeveService.GetGiaoDichKeVeMenhGiaById(model.Id);
            if (giaodichkeve.SoLuong >= model.SoLuong)
            {
                giaodichkeve.SoLuong = giaodichkeve.SoLuong - model.SoLuong;
                int serifrom = giaodichkeve.SeriNumFrom + model.SoLuong;
                string serifromtext = "0";
                if (serifrom != 0)
                {
                    serifromtext = string.Format("{0}", serifrom).PadLeft(7, '0');
                }
                giaodichkeve.SeriFrom = serifromtext;
                if (Convert.ToInt32(giaodichkeve.SeriFrom) == 0)
                    _giaodichkeveService.UpdateGiaoDichKeVeMenhGia(giaodichkeve);
                _customerActivityService.InsertActivityNhaXe("tiêu vé trên giao dịch mệnh giá thành công : '{0}'", giaodichkeve.Id);
                return Json("ok");
            }
            else
            {
                return Json("false");
            }



        }
        [HttpPost]
        public ActionResult XoaKeVeMenhGia(int id)
        {
            var kevemenhgia = _giaodichkeveService.GetGiaoDichKeVeMenhGiaById(id);
            _giaodichkeveService.DeleteGiaoDichKeVeMenhGia(kevemenhgia);
            return Json("ok");
        }

        public ActionResult ListVeXeItem()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe) || !this.isQuanLyAccess(_workContext))
                return AccessDeniedView();
            var model = new ListVeXeItemModel();
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
            model.dllVanPhongs = vanphongs.Select(c => new SelectListItem
            {
                Text = c.TenVanPhong,
                Value = c.Id.ToString()
            }).ToList();
            model.dllVanPhongs.Insert(0, new SelectListItem { Text = "---Chọn quầy bán vé---", Value = "0", Selected = true });
            model.ddlLoaiVes = this.GetCVEnumSelectList<ENLoaiVeXeItem>(_localizationService, model.LoaiVeId);
            model.dlltrangthais = this.GetCVEnumSelectList<ENVeXeItemTrangThai>(_localizationService, model.LoaiVeId);
            var mauves = _giaodichkeveService.GetAllMauVeKyHieu(_workContext.NhaXeId);
            model.ddlmauves = mauves.Select(c => new SelectListItem
            {
                Text = string.Format("{0}-{1}", c.MauVe, c.KyHieu),
                Value = c.Id.ToString()
            }).ToList();
            model.ddlmauves.Insert(0, new SelectListItem { Text = "---Chọn mẫu vé---", Value = "0", Selected = true });

            return View(model);
        }
        [HttpPost]
        public ActionResult ListVeXeItem(ListVeXeItemModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe) || !this.isQuanLyAccess(_workContext))
                return AccessDeniedView();
            var vexes = _giaodichkeveService.GetVeXeItems(_workContext.NhaXeId, model.NguoiNhanId, model.trangthai, 0, model.VanPhongId, model.MauVeId, model.ThongTin, model.NumRow);
            var vexemodels = vexes.Select(c =>
            {
                return c.toModel(_localizationService);
            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = vexemodels,
                Total = vexemodels.Count
            };
            return Json(gridModel);
        }
        [HttpPost]
        public ActionResult ChuyenTrangThaiChuaBan(string VeXeIds)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe) || !this.isQuanLyAccess(_workContext))
                return AccessDeniedView();
            string[] arridstr = VeXeIds.Split(',');
            int[] arrids = Array.ConvertAll(arridstr, s => int.Parse(s));
            _giaodichkeveService.UpdateVeSangChuaBan(arrids);
            return ThanhCong();
        }
        [HttpPost]
        public ActionResult ChuyenTrangThaiDaBan(string VeXeIds)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe) || !this.isQuanLyAccess(_workContext))
                return AccessDeniedView();
            string[] arridstr = VeXeIds.Split(',');
            int[] arrids = Array.ConvertAll(arridstr, s => int.Parse(s));
            _giaodichkeveService.UpdateVeSangDaBan(arrids);
            return ThanhCong();
        }
        [HttpPost]
        public ActionResult XoaVeXe(string VeXeIds)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLKeVe) || !this.isQuanLyAccess(_workContext))
                return AccessDeniedView();
            string[] arridstr = VeXeIds.Split(',');
            int[] arrids = Array.ConvertAll(arridstr, s => int.Parse(s));

            _giaodichkeveService.DeleteVeXe(arrids);
            return ThanhCong();
        }
        #endregion

        #region Quan ly mau ve va ky hieu ve
        [NonAction]
        protected virtual void MauVeKyHieuEntityToModel(QuanLyMauVeKyHieu ent, QuanLyMauVeKyHieuModel model)
        {
            model.Id = ent.Id;
            model.MauVe = ent.MauVe;
            model.KyHieu = ent.KyHieu;
            model.NhaXeId = ent.NhaXeId;
        }
        [NonAction]
        protected virtual void MauVeKyHieuModelToEntity(QuanLyMauVeKyHieuModel model, QuanLyMauVeKyHieu ent)
        {
            ent.Id = model.Id;
            ent.MauVe = model.MauVe;
            ent.KyHieu = model.KyHieu;

        }
        public ActionResult ListMauVeKyHieu()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return null;
            return View();
        }

        public JsonpResult ListMauVe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return null;
            var items = _giaodichkeveService.GetAllMauVeKyHieu(_workContext.NhaXeId);
            var itemmodels = items.Select(c =>
            {
                var model = new QuanLyMauVeKyHieuModel();

                MauVeKyHieuEntityToModel(c, model);
                return model;
            }).ToList();
            return this.Jsonp(itemmodels);
        }

        public JsonpResult CreateMauVe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return null;
            var models = this.DeserializeObject<IEnumerable<QuanLyMauVeKyHieuModel>>("models");

            if (models != null)
            {
                foreach (var model in models)
                {
                    var item = new QuanLyMauVeKyHieu();
                    item.NhaXeId = _workContext.NhaXeId;
                    MauVeKyHieuModelToEntity(model, item);

                    var kq = _giaodichkeveService.InsertQuanLyMauVeKyHieu(item);
                    if (kq)
                    {
                        _customerActivityService.InsertActivityNhaXe("Thêm mới thành công : '{0}'", item.MauVe);
                    }
                }
            }
            return this.Jsonp(models);
        }

        public JsonpResult EditMauVe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVTaiKhoan))
                return null;
            var models = this.DeserializeObject<IEnumerable<QuanLyMauVeKyHieuModel>>("models");
            if (models != null)
            {
                foreach (var model in models)
                {
                    var item = _giaodichkeveService.GetMauVeById(model.Id);
                    item.NhaXeId = _workContext.NhaXeId;
                    MauVeKyHieuModelToEntity(model, item);
                    var kq = _giaodichkeveService.UpdateQuanLyMauVeKyHieu(item);
                    if (kq)
                    {
                        _customerActivityService.InsertActivityNhaXe("Cập nhật thành công : '{0}'", item.MauVe);
                        SuccessNotification("Cập nhật thành công");
                    }

                }
            }
            return this.Jsonp(models);
        }
        public JsonpResult ListMenhGiaVe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return null;
            var items = _giaodichkeveService.GetAllMenhGia(_workContext.NhaXeId);
            return this.Jsonp(items);
        }

        public JsonpResult CreateMenhGiaVe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return null;
            var models = this.DeserializeObject<IEnumerable<MenhGiaVe>>("models");

            if (models != null)
            {
                foreach (var model in models)
                {
                    var menhgia = _giaodichkeveService.GetMenhGiaVeByGia(model.MenhGia);
                    if(menhgia==null)
                    {
                        var item = new MenhGiaVe();
                        item.NhaXeId = _workContext.NhaXeId;
                        item.MenhGia = model.MenhGia;
                        _giaodichkeveService.InsertMenhGiaVe(item);
                        _customerActivityService.InsertActivityNhaXe("Thêm mới mệnh giá : '{0}'", item.MenhGia.ToTien(_priceFormatter));
                        SuccessNotification("Thêm mới mệnh giá thành công");
                    }
                   
                }
            }
            return this.Jsonp(models);
        }

        public JsonpResult EditMenhGiaVe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return null;
            var models = this.DeserializeObject<IEnumerable<MenhGiaVe>>("models");
            if (models != null)
            {
                foreach (var model in models)
                {
                    var item = _giaodichkeveService.GetMenhGiaVeById(model.Id);
                    item.MenhGia = model.MenhGia;
                    _giaodichkeveService.UpdateMenhGiaVe(item);
                    _customerActivityService.InsertActivityNhaXe("Cập nhật mệnh giá : '{0}'", item.MenhGia.ToTien(_priceFormatter));
                    SuccessNotification("Cập nhật mệnh giá thành công");

                }
            }
            return this.Jsonp(models);
        }
        public JsonpResult DeleteMenhGiaVe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHanhTrinh))
                return null;
            var models = this.DeserializeObject<IEnumerable<MenhGiaVe>>("models");
            if (models != null)
            {
                foreach (var model in models)
                {
                    var item = _giaodichkeveService.GetMenhGiaVeById(model.Id);
                    _giaodichkeveService.DeleteMenhGiaVe(item);

                    _customerActivityService.InsertActivityNhaXe("Xóa mệnh giá : '{0}'", item.MenhGia.ToTien(_priceFormatter));
                    SuccessNotification("Xóa mệnh giá thành công");

                }
            }
            return this.Jsonp(models);
        }

        #endregion
        #region Cap nhat ban ve theo chuyen
        private List<QuanLyMauVeKyHieu> _maukyhieu;
        List<QuanLyMauVeKyHieu> maukyhieu
        {
            get
            {
                if (_maukyhieu != null)
                    return _maukyhieu;
                _maukyhieu = _giaodichkeveService.GetAllMauVeKyHieu(_workContext.NhaXeId);
                return _maukyhieu;
            }
        }
        public ActionResult ChuyenXeDiVeList()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new XeXuatBenInforModel();
            model.NgayDi = DateTime.Now;
            model.KhungGioId = 0;
            model.khunggios = this.GetCVEnumSelectList<ENKhungGio>(_localizationService, model.KhungGioId);
            return View(model);
        }
        [HttpPost]
        public ActionResult GetAllChuyenDi(XeXuatBenInforModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            //lay tat ca chuyen di trong ngay
            var ListChuyen = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, model.NgayDi,0, model.khunggio, model.ThongTin).Select(c =>
            {

                var _m = c.toModelVeChuyen(_localizationService);
                _m.TenLaiXe1 = c.ThongTinLaiPhuXe(0);
                _m.TenLaiXe2 = c.ThongTinLaiPhuXe(1);
                var vexes = _giaodichkeveService.GetVeXeItems(c.Id);
                _m.SoNguoi = vexes.Count;
                _m.TongDoanhThu = vexes.Sum(v => v.menhgia.MenhGia);
                return _m;

            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = ListChuyen,
                Total = ListChuyen.Count
            };
            return Json(gridModel);
        }
        [HttpPost]
        public ActionResult GetChuyenVe(int ChuyenDiId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var c = _nhaxeService.GetChuyenVeByChuyenDi(ChuyenDiId);
            var models = new List<VeXeChuyenDiVeModel>();
            if (c != null)
            {
                var _m = c.toModelVeChuyen(_localizationService);
                _m.TenLaiXe1 = c.ThongTinLaiPhuXe(0);
                _m.TenLaiXe2 = c.ThongTinLaiPhuXe(1);
                var vexes = _giaodichkeveService.GetVeXeItems(c.Id);
                _m.SoNguoi = vexes.Count;
                _m.TongDoanhThu = vexes.Sum(v => v.menhgia.MenhGia);
                models.Add(_m);
            }

            var gridModel = new DataSourceResult
            {
                Data = models,
                Total = models.Count
            };
            return Json(gridModel);
        }
        string VexeItemToText(List<VeXeItem> vexes, string ngancach = ". ")
        {
            string ret = "";
            //lay thong tin mau
            foreach (var ma in maukyhieu)
            {
                string ret1 = "";
                foreach (var vx in vexes)
                {
                    if (vx.MauVe.Equals(ma.MauVe) && vx.KyHieu.Equals(ma.KyHieu))
                    {
                        if (string.IsNullOrEmpty(ret1))
                            ret1 = string.Format("{0}{1}-{2}: {3}", "", vx.MauVe, vx.KyHieu, vx.SoSeri);
                        else
                            ret1 = ret1 + string.Format("{0}{1}", ngancach, vx.SoSeri);
                    }
                }
                if (!string.IsNullOrEmpty(ret1))
                {
                    ret = ret + ret1 + "\r\n";
                }
            }
            return ret;
        }
        /// <summary>
        /// Chuyen noi dung cu phap seri sang mang seri
        /// Item=[Mau]-[KyHieu]-[Seri]
        /// </summary>
        /// <param name="strdata"></param>
        /// <returns></returns>
        string[] TextToVeXeItemSeri(string strdata)
        {
            var arrseri = new List<String>();
            try
            {
                strdata = strdata.Replace("\r", "");
                string[] arrdata = strdata.Split('\n');
                foreach (var ves in arrdata)
                {
                    string[] arrves = ves.Split(':');
                    string mauvakh = arrves[0].Trim();
                    string[] seris = arrves[1].Trim().Split('.');
                    foreach (var s in seris)
                    {
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            if (s.Contains("/"))
                            {
                                //co so luong: serial1: 8
                                string[] arrsubseri = s.Split('/');
                                int sl;
                                if (int.TryParse(arrsubseri[1], out sl))
                                {
                                    for (int i = 0; i < sl; i++)
                                    {
                                        int serinum = Convert.ToInt32(arrsubseri[0]) + i;
                                        string _subseri = serinum.ToString().PadLeft(7, '0');
                                        arrseri.Add(string.Format("{0}-{1}", mauvakh, _subseri));
                                    }
                                }
                            }
                            else if (s.Contains("-"))
                            {
                                //tu seri den seri
                                string[] arrsubseri = s.Split('-');
                                int serifrom = Convert.ToInt32(arrsubseri[0]);
                                int serito = Convert.ToInt32(arrsubseri[1]);
                                for (int i = serifrom; i <= serito; i++)
                                {
                                    string _subseri = i.ToString().PadLeft(7, '0');
                                    arrseri.Add(string.Format("{0}-{1}", mauvakh, _subseri));
                                }
                            }
                            else //khong theo cu phap
                                arrseri.Add(string.Format("{0}-{1}", mauvakh, s));
                        }

                    }
                }
            }
            catch
            {

            }
            return arrseri.ToArray();

        }
        HanhTrinhGiaVe GetHanhTrinhGiaVe(List<HanhTrinhGiaVe> arrSrc, int DiemDonId, int DiemDenId)
        {
            foreach (var ht in arrSrc)
            {
                if (ht.DiemDonId == DiemDonId && ht.DiemDenId == DiemDenId)
                {
                    return ht;
                }
            }
            return null;
        }
        CapNhatVeChuyenModel.VeXeChuyen CreateVeXeChuyen(HistoryXeXuatBen chuyenxe)
        {
            var ret = new CapNhatVeChuyenModel.VeXeChuyen();
            if (chuyenxe == null)
                return ret;
            ret.ChuyenId = chuyenxe.Id;
            ret.chuyenxe = chuyenxe;
            //lay thong tin ve ban o quay di
            //da so huu
            ret.arrVeXeQuay = _giaodichkeveService.GetVeXeBanTaiQuay(_workContext.NhaXeId, chuyenxe, true);
            ret.SeriVeQuayText = VexeItemToText(ret.arrVeXeQuay);


            //lay thong tin ve da ban tren xe
            ret.arrVeXeTrenXe = _giaodichkeveService.GetVeXeBanTrenXe(_workContext.NhaXeId, chuyenxe);

            //lay thong tin chang
            var htgiaves = _hanhtrinhService.GetallHanhTrinhGiaVe(ret.chuyenxe.HanhTrinhId);
            ret.arrDiemDon = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(ret.chuyenxe.HanhTrinhId);
            ret.arrVeXeChang = new CapNhatVeChuyenModel.VeXeChang[ret.arrDiemDon.Count, ret.arrDiemDon.Count];
            for (int i = 0; i < ret.arrDiemDon.Count; i++)
            {
                for (int j = 0; j < ret.arrDiemDon.Count; j++)
                {
                    var htgv = GetHanhTrinhGiaVe(htgiaves, ret.arrDiemDon[i].DiemDonId, ret.arrDiemDon[j].DiemDonId);
                    if (htgv != null)
                    {
                        ret.arrVeXeChang[i, j] = new CapNhatVeChuyenModel.VeXeChang();
                        ret.arrVeXeChang[i, j].changgiave = htgv;
                        ret.arrVeXeChang[i, j].ChangId = htgv.Id;
                        //lay thong tin ve theo chang                        
                        ret.arrVeXeChang[i, j].arrVeXe = ret.arrVeXeTrenXe.Where(c => c.ChangId == htgv.Id).ToList();
                        ret.arrVeXeChang[i, j].SeriVeXeText = VexeItemToText(ret.arrVeXeChang[i, j].arrVeXe);
                        ret.arrVeXeChang[i, j].SoLuong = ret.arrVeXeChang[i, j].arrVeXe.Count;
                    }

                }
            }
            //Neu chua co ve so huu, thi lay nhung ve ban o quay gan nhat
            //thong tin chuyen xe chua cap nhat
            if (String.IsNullOrEmpty(ret.SeriVeQuayText) && ret.arrVeXeTrenXe.Count == 0)
            {
                //lay toi da 50 ve
                var vexequay1 = _giaodichkeveService.GetVeXeBanTaiQuay(_workContext.NhaXeId, chuyenxe).Take(50).OrderBy(c => c.SoSeriNum).ToList();
                ret.SeriVeQuayText = VexeItemToText(vexequay1);
            }
            return ret;
        }
        void PreProcessVeXeChuyen(CapNhatVeChuyenModel.VeXeChuyen chuyenxe, string vequayseri, string slve)
        {
            if (chuyenxe.chuyenxe == null)
                return;
            if (!string.IsNullOrEmpty(vequayseri))
            {
                string[] arrvequay = TextToVeXeItemSeri(vequayseri);
                string SeriKhongHopLe;
                chuyenxe.arrVeXeQuay = _giaodichkeveService.KiemTraBanVeTaiQuayTheoXe(_workContext.NhaXeId, chuyenxe.chuyenxe, arrvequay, out SeriKhongHopLe);
                chuyenxe.SeriKhongHopLe = SeriKhongHopLe;
                chuyenxe.SeriVeQuayText = VexeItemToText(chuyenxe.arrVeXeQuay);
                _giaodichkeveService.InsertXeXuatBenVeXeItem(chuyenxe.chuyenxe.Id, 0, chuyenxe.arrVeXeQuay.Select(c => c.Id).ToArray());
            }
            else
            {
                chuyenxe.SeriVeQuayText = "";
                chuyenxe.arrVeXeQuay.Clear();
            }


            //cat so luong ve tren giay di duong
            var arrVeDaCo = new List<VeXeItem>();
            string[] arrdata = slve.Split('|');
            for (int i = 0; i < arrdata.Length; i = i + 2)
            {
                if (string.IsNullOrEmpty(arrdata[i + 1]))
                    continue;
                var changid = Convert.ToInt32(arrdata[i]);
                int soluong;
                if (int.TryParse(arrdata[i + 1], out soluong))
                {
                    if (soluong > 0)
                    {
                        foreach (var changitem in chuyenxe.arrVeXeChang)
                        {
                            if (changitem == null)
                                continue;
                            if (changitem.ChangId == changid && changitem.SoLuong == 0)
                            {
                                changitem.SoLuong = soluong;
                                //lay thong ti so seri
                                changitem.arrVeXe = _giaodichkeveService.GetVeXeDuKienBanTrenXe(_workContext.NhaXeId, chuyenxe.chuyenxe, changid, soluong, arrVeDaCo);
                                chuyenxe.arrVeXeTrenXe.AddRange(changitem.arrVeXe);
                                changitem.SeriVeXeText = VexeItemToText(changitem.arrVeXe);
                                //them ve da co
                                arrVeDaCo.AddRange(changitem.arrVeXe);
                                //them vao temp
                                _giaodichkeveService.InsertXeXuatBenVeXeItem(chuyenxe.chuyenxe.Id, changid, changitem.arrVeXe.Select(c => c.Id).ToArray());
                                break;
                            }
                        }
                    }
                }
                else
                {
                    //seri ve co mau so va ky hieu
                    foreach (var changitem in chuyenxe.arrVeXeChang)
                    {
                        if (changitem == null)
                            continue;
                        if (changitem.ChangId == changid)
                        {
                            //lay thong ti so seri
                            string[] arrvetrenxe = TextToVeXeItemSeri(arrdata[i + 1]);
                            if (arrvetrenxe.Length > 0)
                            {
                                string SeriKhongHopLe;
                                changitem.arrVeXe = _giaodichkeveService.KiemTraBanVeTrenXe(_workContext.NhaXeId, chuyenxe.chuyenxe, changid, arrvetrenxe, out SeriKhongHopLe);
                                chuyenxe.arrVeXeTrenXe.AddRange(changitem.arrVeXe);
                                changitem.SeriVeXeText = VexeItemToText(changitem.arrVeXe);
                                changitem.SoLuong = changitem.arrVeXe.Count;
                                chuyenxe.SeriKhongHopLe = chuyenxe.SeriKhongHopLe + SeriKhongHopLe;
                                //them ve da co
                                arrVeDaCo.AddRange(changitem.arrVeXe);
                                //them vao temp
                                _giaodichkeveService.InsertXeXuatBenVeXeItem(chuyenxe.chuyenxe.Id, changid, changitem.arrVeXe.Select(c => c.Id).ToArray());
                            }
                            break;
                        }
                    }
                }
            }

        }
        void ProcessVeXeChuyen(CapNhatVeChuyenModel.VeXeChuyen chuyenxe, string vequayseri, string slve)
        {
            if (chuyenxe.chuyenxe == null)
                return;
            //cap nhat ban ve tai quay
            if (!string.IsNullOrEmpty(vequayseri))
            {
                string[] arrvequay = TextToVeXeItemSeri(vequayseri);
                chuyenxe.arrVeXeQuay = _giaodichkeveService.CapNhatBanVeTaiQuayTheoXe(_workContext.NhaXeId, chuyenxe.chuyenxe, arrvequay);
                chuyenxe.SeriVeQuayText = VexeItemToText(chuyenxe.arrVeXeQuay);
            }
            else
            {
                chuyenxe.SeriVeQuayText = "";
                chuyenxe.arrVeXeQuay.Clear();
            }

            //cat seri ve tren giay di duong
            string[] arrdata = slve.Split('|');
            for (int i = 0; i < arrdata.Length; i = i + 2)
            {
                var changid = Convert.ToInt32(arrdata[i]);
                string[] arrvetrenxe = TextToVeXeItemSeri(arrdata[i + 1]);

                if (arrvetrenxe.Length > 0)
                {
                    foreach (var changitem in chuyenxe.arrVeXeChang)
                    {
                        if (changitem == null)
                            continue;
                        if (changitem.ChangId == changid)
                        {
                            //lay thong ti so seri
                            changitem.arrVeXe = _giaodichkeveService.CapNhatBanVeTrenXe(_workContext.NhaXeId, chuyenxe.chuyenxe, changid, arrvetrenxe);
                            changitem.SeriVeXeText = VexeItemToText(changitem.arrVeXe);
                            changitem.SoLuong = changitem.arrVeXe.Count;
                            break;
                        }
                    }
                }
            }
        }
        void XoaVeTheoChuyenDi(HistoryXeXuatBen chuyenxe)
        {
            if (chuyenxe == null)
                return;
            var vequays = _giaodichkeveService.GetVeXeBanTaiQuay(_workContext.NhaXeId, chuyenxe, true).Select(c => c.Id).ToArray();
            _giaodichkeveService.UpdateVeSangDaBan(vequays);
            var vetrenxes = _giaodichkeveService.GetVeXeBanTrenXe(_workContext.NhaXeId, chuyenxe).Select(c => c.Id).ToArray();
            _giaodichkeveService.UpdateVeSangChuaBan(vetrenxes);
        }
        public ActionResult CapNhatVeChuyen(int ChuyenId, int isReset)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedPartialView();

            var model = new CapNhatVeChuyenModel();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenId);
            var chuyenve = _nhaxeService.GetChuyenVeByChuyenDi(ChuyenId);
            //update tat ca cac ve di kem voi chuyen xe nay ve trang thai chua su dung
            if (isReset == 1)
            {
                XoaVeTheoChuyenDi(chuyendi);
                XoaVeTheoChuyenDi(chuyenve);
            }
            model.chuyendi = CreateVeXeChuyen(chuyendi);
            model.chuyenve = CreateVeXeChuyen(chuyenve);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _CapNhatVeChuyenKetThuc(int ChuyenId, string vequaydi, string vequayve, string slvedi, string slveve, bool isdongy)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedPartialView();
            var model = new CapNhatVeChuyenModel();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenId);
            var chuyenve = _nhaxeService.GetChuyenVeByChuyenDi(ChuyenId);
            model.chuyendi = CreateVeXeChuyen(chuyendi);
            model.chuyenve = CreateVeXeChuyen(chuyenve);
            if (!isdongy)
            {
                //xoa du lieu
                _giaodichkeveService.DeleteXeXuatBenVeXeItem(chuyendi.Id, chuyenve.Id);
                PreProcessVeXeChuyen(model.chuyendi, vequaydi, slvedi);
                PreProcessVeXeChuyen(model.chuyenve, vequayve, slveve);
            }
            else
            {
                //cap nhat du lieu
                ProcessVeXeChuyen(model.chuyendi, vequaydi, slvedi);
                ProcessVeXeChuyen(model.chuyenve, vequayve, slveve);
                _customerActivityService.InsertActivityNhaXe("Cập nhật vé theo chuyến đi {0}", chuyendi.ToText());
            }


            return PartialView(model);
        }
        [HttpPost]
        public ActionResult FinishXuatBenVeXeItem(int ChuyenDiId, int ChuyenVeId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedPartialView();
            _giaodichkeveService.FinishXuatBenVeXeItem(ChuyenDiId, ChuyenVeId);
            _customerActivityService.InsertActivityNhaXe("Cập nhật vé theo chuyến đi có Id= {0} và {1}", ChuyenDiId, ChuyenVeId);
            return ThanhCong();
        }
        #endregion

    
    }
}