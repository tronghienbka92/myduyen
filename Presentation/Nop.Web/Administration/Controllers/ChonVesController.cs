using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Admin.Extensions;
using Nop.Admin.Models.Catalog;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Services.Chonves;
using Nop.Admin.Models.ChonVes;
using Nop.Core;
using Nop.Services.NhaXes;
using Nop.Services.Directory;
using Nop.Core.Domain.Chonves;
using Nop.Admin.Models.NhaXes;

namespace Nop.Admin.Controllers
{
    public class ChonVesController : BaseAdminController
    {
        #region "Khoi Tao"
        private readonly IStateProvinceService _stateProvinceService;
        private readonly INhaXeService _nhaxeService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly IChonVeService _chonveService;
        private readonly ICustomerService _customerService;
        private readonly IBenXeService _benxeService;
        private readonly IDiaChiService _diachiService;


        public ChonVesController(IChonVeService chonveService,
            IStateProvinceService stateProvinceService,
            INhaXeService nhaxeService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            IPictureService pictureService,
            ICustomerService customerService,
             IBenXeService benxeService,
            IDiaChiService diachiService
            )
        {
            this._chonveService = chonveService;
            this._stateProvinceService = stateProvinceService;
            this._nhaxeService = nhaxeService;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._customerService = customerService;
            this._benxeService = benxeService;
            this._diachiService = diachiService;

        }
        #endregion
        #region Common
        protected virtual IList<SelectListItem> GetEnumSelectList<T>(int valseleded)
        {
            return (Enum.GetValues(typeof(T)).Cast<int>().Select(e => new SelectListItem() { Text = _localizationService.GetResource(string.Format("Enums.{0}.{1}.{2}", typeof(T).Namespace, typeof(T).Name, Enum.GetName(typeof(T), e))), Value = e.ToString(), Selected = (e == valseleded) })).ToList();
        }
        [NonAction]
        protected virtual string GetLabel(string _name)
        {
            return _localizationService.GetResource(string.Format("ChonVe.QuanTri.{0}", _name));
        }
        [NonAction]
        protected List<SelectListItem> GetListOfProvince(bool addFirst = false, int itemselected = 0)
        {
            var states = _stateProvinceService.GetStateProvincesByCountryId(NhaXesController.CountryID);
            var liststates = new List<SelectListItem>();
            if (addFirst)
                liststates.Add(new SelectListItem { Text = GetLabel("ChonTinh"), Value = "0" });
            if (states.Count > 0)
            {
                foreach (var s in states)
                {
                    liststates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = itemselected == s.Id });
                }
            }
            return liststates;
        }
        [NonAction]
        private void DiaChiInfoPrepare(DiaChiModel model)
        {
            var states = _stateProvinceService.GetStateProvincesByCountryId(NhaXesController.CountryID);
            if (states.Count > 0)
            {
                foreach (var s in states)
                {
                    model.AvailableStates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.ProvinceID) });
                }
                int ProvinceID = Convert.ToInt32(model.AvailableStates[0].Value);
                if (model.Id > 0 && model.ProvinceID > 0)
                {
                    ProvinceID = model.ProvinceID.GetValueOrDefault(0);
                }
                var quanhuyens = _diachiService.GetQuanHuyenByProvinceId(ProvinceID);
                model.AvailableQuanHuyens.Add(new SelectListItem { Text = GetLabel("ChonQuanHuyen"), Value = "0", Selected = (model.QuanHuyenID == 0) });
                foreach (var s in quanhuyens)
                {
                    model.AvailableQuanHuyens.Add(new SelectListItem { Text = s.Ten, Value = s.Id.ToString(), Selected = (s.Id == model.QuanHuyenID) });
                }
            }

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
            result.Insert(0, new { id = 0, name = GetLabel("ChonQuanHuyen") });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Hop Dong"
        // GET: HopDongs
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListHopDong()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();

            var model = new HopDongListModel();
            //neu la giam doc
            if (_permissionService.Authorize(StandardPermissionProvider.CVSaleManager))
            {
                //add them nguoi tao la cac nhan vien sale
                model.isManager = true;
                model.NguoiTaos.Add(new SelectListItem { Text = GetLabel("ChonSale"), Value = "0" });
                //lay thong tin nguoi tao
                var nguoitaoids = _chonveService.GetNguoiTaoIds();
                foreach (var id in nguoitaoids)
                {
                    var nguoitao = _customerService.GetCustomerById(id);
                    if (nguoitao != null)
                    {
                        model.NguoiTaos.Add(new SelectListItem { Text = nguoitao.GetFullName(), Value = nguoitao.Id.ToString() });
                    }
                }
                PrepareListTrangThai(model);
            }
            else
            {
                //khong hien 
                model.isManager = false;
                model.NguoiTaos.Add(new SelectListItem { Text = _workContext.CurrentCustomer.GetFullName(), Value = _workContext.CurrentCustomer.Id.ToString() });
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ListHopDong(DataSourceRequest command, HopDongListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();
            int nguoitaoid = _workContext.CurrentCustomer.Id;
            ENTrangThaiHopDong trangthaiid = ENTrangThaiHopDong.TatCa;
            if (_permissionService.Authorize(StandardPermissionProvider.CVSaleManager))
            {
                nguoitaoid = model.NguoiTaoId;
                trangthaiid =(ENTrangThaiHopDong) model.TrangThaiID;
            }


            var hopdongs = _chonveService.GetAllHopDong(model.TimMaHopDong, model.TimTenHopDong,
                command.Page - 1, command.PageSize, false, nguoitaoid, trangthaiid);

            var gridModel = new DataSourceResult
            {
                Data = hopdongs.Select(x =>
                {
                    var m = x.ToModel();
                    m.TrangThaiText = x.TrangThai.GetLocalizedEnum(_localizationService, _workContext);
                    return m;
                }),
                Total = hopdongs.TotalCount
            };

            return Json(gridModel);
        }
        public ActionResult TaoHopDong()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();
            var model = new HopDongModel();
            //default values           
            PrepareListLoaiHopDong(model);
            PrepareListNhaXe(model);
            PrepareHopDongKhachHang(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult TaoHopDong(HopDongModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                var hopdong = new HopDong();
                hopdong = model.ToEntity(hopdong);
                hopdong.NguoiTaoID = _workContext.CurrentCustomer.Id;
                hopdong.NgayTao = DateTime.UtcNow;
                hopdong.NgayCapNhat = null;
                hopdong.NgayHetHan = null;
                hopdong.NgayKichHoat = null;
                hopdong.TrangThai = Core.Domain.Chonves.ENTrangThaiHopDong.Moi;
                _chonveService.InsertHopDong(hopdong);
                return continueEditing ? RedirectToAction("SuaHopDong", new { id = hopdong.Id }) : RedirectToAction("ListHopDong");
            }

            return View(model);
        }
        public ActionResult SuaHopDong(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();
            var hopdong = _chonveService.GetHopDongById(id);
            if (hopdong == null || hopdong.TrangThai != Core.Domain.Chonves.ENTrangThaiHopDong.Moi || hopdong.NguoiTaoID != _workContext.CurrentCustomer.Id)
                //No manufacturer found with the specified id
                return RedirectToAction("ListHopDong");
            var model = hopdong.ToModel();
            //default values           
            PrepareListLoaiHopDong(model);
            PrepareListNhaXe(model);
            PrepareHopDongKhachHang(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult SuaHopDong(HopDongModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();

            var hopdong = _chonveService.GetHopDongById(model.Id);
            if (hopdong == null || hopdong.TrangThai != Core.Domain.Chonves.ENTrangThaiHopDong.Moi || hopdong.NguoiTaoID != _workContext.CurrentCustomer.Id)
                //No manufacturer found with the specified id
                return RedirectToAction("ListHopDong");

            if (ModelState.IsValid)
            {
                hopdong = model.ToEntity(hopdong);

                //var customer = _customerService.GetCustomerByEmail(model.KhachHang.Email);
                //if (customer != null && !customer.Deleted)
                //{
                //    hopdong.KhachHangID = customer.Id;
                //}

                hopdong.NgayCapNhat = DateTime.UtcNow;
                _chonveService.UpdateHopDong(hopdong);


                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("SuaHopDong", hopdong.Id);
                }
                return RedirectToAction("ListHopDong");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult XoaHopDong(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();

            var hopdong = _chonveService.GetHopDongById(id);
            if (hopdong == null || hopdong.TrangThai == Core.Domain.Chonves.ENTrangThaiHopDong.Huy || hopdong.NguoiTaoID != _workContext.CurrentCustomer.Id)
                //No manufacturer found with the specified id
                return RedirectToAction("ListHopDong");

            _chonveService.DeleteHopDong(hopdong);

            return RedirectToAction("ListHopDong");
        }
        [HttpPost]
        public ActionResult HuyHopDong(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();

            var hopdong = _chonveService.GetHopDongById(Id);
            if (hopdong == null || hopdong.TrangThai != Core.Domain.Chonves.ENTrangThaiHopDong.Moi || hopdong.NguoiTaoID != _workContext.CurrentCustomer.Id)
                //No manufacturer found with the specified id
                return RedirectToAction("ListHopDong");

            _chonveService.DeleteHopDong(hopdong);

            return Json("ok");
        }
        [HttpPost]
        public ActionResult KetThucHopDong(int Id)
        {           
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();

            var hopdong = _chonveService.GetHopDongById(Id);
            var model = new XuLyHopDongModel();
            if (_permissionService.Authorize(StandardPermissionProvider.CVSaleManager))
            {
                model.isManager = true;

            }
            if (hopdong == null || hopdong.TrangThai == Core.Domain.Chonves.ENTrangThaiHopDong.Huy || model.isManager==false)
                //No manufacturer found with the specified id
                return RedirectToAction("ListHopDong");

            hopdong.TrangThai = Core.Domain.Chonves.ENTrangThaiHopDong.KetThuc;
            _chonveService.UpdateHopDong(hopdong);

            return Json("ok");
        }
        public ActionResult ChiTietHopDong(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();
            var model = new XuLyHopDongModel();
            if (_permissionService.Authorize(StandardPermissionProvider.CVSaleManager))
            {
                model.isManager = true;
                PrepareListGiaHan(model);
            }
            model.IdCurrent = _workContext.CurrentCustomer.Id;
            var hopdong = _chonveService.GetHopDongById(id);
            if (hopdong == null)
            {
                throw new ArgumentException(" no result found with th specified id");
            }
            if (hopdong.NguoiTaoID == _workContext.CurrentCustomer.Id || model.isManager == true)
            {
                model.Id = hopdong.Id;
                model.MaHopDong = hopdong.MaHopDong;
                model.TenHopDong = hopdong.TenHopDong;
                model.LoaiHopDongID = hopdong.LoaiHopDongID;
                model.LoaiHopDongText = hopdong.LoaiHopDong.GetLocalizedEnum(_localizationService, _workContext);
                model.TrangThaiID = hopdong.TrangThaiID;
                model.TrangThaiText = hopdong.TrangThai.GetLocalizedEnum(_localizationService, _workContext);
                model.NhaXeID = hopdong.NhaXeID;
                model.NhaXeText = _nhaxeService.GetNhaXeById(model.NhaXeID).TenNhaXe;
                model.ThongTin = hopdong.ThongTin;
                model.TrangThaiID = hopdong.TrangThaiID;
                model.NguoiTaoID = hopdong.NguoiTaoID;
        }
          
            return View(model);
        }
        [HttpPost]
        public ActionResult GuiDuyetHopDong(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();
            var item = _chonveService.GetHopDongById(Id);
            if (item == null || item.TrangThai != Core.Domain.Chonves.ENTrangThaiHopDong.Moi || item.NguoiTaoID != _workContext.CurrentCustomer.Id)
            {
                return RedirectToAction("ListHopDong");
            }
            item.TrangThai = Core.Domain.Chonves.ENTrangThaiHopDong.DangChoDuyet;      
            _chonveService.UpdateHopDong(item);
            return Json("ok");
        }
        [HttpPost]
        public ActionResult DongYDuyetHopDong(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();
            var model = new XuLyHopDongModel();
            var item = _chonveService.GetHopDongById(Id);
            if (_permissionService.Authorize(StandardPermissionProvider.CVSaleManager))
            {
                model.isManager = true;
                
            }
            if (item == null || item.TrangThai != Core.Domain.Chonves.ENTrangThaiHopDong.DangChoDuyet || model.isManager==false)
            {
                return RedirectToAction("ListHopDong");
            }

            item.TrangThai = Core.Domain.Chonves.ENTrangThaiHopDong.DaDuyet;
            item.NguoiDuyetID = _workContext.CurrentCustomer.Id;
            item.NgayKichHoat = DateTime.Now;
            _chonveService.UpdateHopDong(item);
            return Json("ok");
        }
        [HttpPost]
        public ActionResult KhongDuyetHopDong(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();
            var model = new XuLyHopDongModel();
            var item = _chonveService.GetHopDongById(Id);
            if (_permissionService.Authorize(StandardPermissionProvider.CVSaleManager))
            {
                model.isManager = true;
                
            }
            if (item == null || item.TrangThai != Core.Domain.Chonves.ENTrangThaiHopDong.DangChoDuyet||model.isManager==false)
            {
                return RedirectToAction("ListHopDong");
            }
            item.TrangThai = Core.Domain.Chonves.ENTrangThaiHopDong.Moi;
            _chonveService.UpdateHopDong(item);
            return Json("OK");
        }
        [HttpPost]
        public ActionResult KiemTraLaiHopDong(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();
            var item = _chonveService.GetHopDongById(Id);
            if (item == null || item.TrangThai != Core.Domain.Chonves.ENTrangThaiHopDong.DangChoDuyet||item.NguoiTaoID!=_workContext.CurrentCustomer.Id)
            {
                return RedirectToAction("ListHopDong");
            }
            item.TrangThai = Core.Domain.Chonves.ENTrangThaiHopDong.Moi;
            _chonveService.UpdateHopDong(item);
            return Json("OK");
        }
        [HttpPost]
        public ActionResult GiaHanHopDong(int Id, int GiaHanID)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLHopDong))
                return AccessDeniedView();
            var model = new XuLyHopDongModel();
            var item = _chonveService.GetHopDongById(Id);
            if (_permissionService.Authorize(StandardPermissionProvider.CVSaleManager))
            {
                model.isManager = true;
               
            }
            if (item == null || item.TrangThai != Core.Domain.Chonves.ENTrangThaiHopDong.DaDuyet||model.isManager==false)
            {
                return RedirectToAction("ListHopDong");
            }
            else
            {
                item.NgayHetHan = item.NgayHetHan.GetValueOrDefault(DateTime.Now).AddMonths(GiaHanID);
                _chonveService.UpdateHopDong(item);
            }
            return Json("OK");
        }
     
        [NonAction]
        protected virtual void PrepareListLoaiHopDong(HopDongModel model)
        {
            model.ListLoaiHopDong = GetEnumSelectList<ENLoaiHopDong>(model.LoaiHopDongID);
        }
        [NonAction]
        protected virtual void PrepareListTrangThai(HopDongListModel model)
        {
            model.ListTrangThai = GetEnumSelectList<ENTrangThaiHopDong>(model.TrangThaiID);
        }
        [NonAction]
        protected virtual void PrepareListGiaHan(XuLyHopDongModel model)
        {
            model.ListGiaHan = GetEnumSelectList<ENGiaHanHopDong>(model.GiaHanID);
        }
        [NonAction]
        protected virtual void PrepareListNhaXe(HopDongModel model)
        {
            var nhaxes = _nhaxeService.GetAllNhaXe("", 0, 100, false, _workContext.CurrentCustomer.Id);
            if (nhaxes.Count > 0)
            {
                foreach (var s in nhaxes)
                {
                    model.ListNhaXe.Add(new SelectListItem { Text = string.Format("{0} - {1}", s.MaNhaXe, s.TenNhaXe), Value = s.Id.ToString(), Selected = (s.Id == model.NhaXeID) });
                }
            }
        }
        [NonAction]
        protected virtual void PrepareHopDongKhachHang(HopDongModel model)
        {
            model.KhachHang = new HopDongModel.KhachHangModel();
            if (model.KhachHangID > 0)
            {
                var custommer = _customerService.GetCustomerById(model.KhachHangID);
                model.KhachHang.Id = custommer.Id;
                model.KhachHang.Email = custommer.Email;
                model.KhachHang.Fullname = custommer.GetFullName();
            }

        }
        #endregion

        #region Ben Xe
        [NonAction]
        void BenXeModelToBenXe(BenXeModel itemfrom, BenXe itemto)
        {
            itemto.Id = itemfrom.Id;
            itemto.TenBenXe = itemfrom.TenBenXe;
            itemto.DiaChiId = itemfrom.DiaChiId;
            itemto.HienThi = itemfrom.HienThi;
            itemto.PictureId = itemfrom.PictureId;
            itemto.MoTa = itemfrom.MoTa;

        }
        [NonAction]
        void BenXeToBenXeModel(BenXe itemfrom, BenXeModel itemto)
        {
            itemto.Id = itemfrom.Id;
            itemto.TenBenXe = itemfrom.TenBenXe;
            itemto.DiaChiId = itemfrom.DiaChiId;
            itemto.HienThi = itemfrom.HienThi;
            itemto.PictureId = itemfrom.PictureId;
            itemto.PictureUrl = _pictureService.GetPictureUrl(itemfrom.PictureId);
            itemto.MoTa = itemfrom.MoTa;
            var diachi = _diachiService.GetById(itemfrom.DiaChiId);
            if (diachi != null)
            {
                string strhuyen = "";
                if (diachi.QuanHuyenID > 0)
                {
                    var quanhuyen = _diachiService.GetQuanHuyenById(diachi.QuanHuyenID.GetValueOrDefault(0));
                    if (quanhuyen != null)
                        strhuyen = "- " + quanhuyen.Ten + " ";
                }
                itemto.DiaChiText = string.Format("{0} {1}- {2}", diachi.DiaChi1, strhuyen, diachi.Province.Name);
            }


        }
        public ActionResult BenXeList()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();
            var model = new BenXeListModel();
            model.AvailableStates = GetListOfProvince(true);
            return View(model);
        }
        [HttpPost]
        public ActionResult BenXeList(DataSourceRequest command, BenXeListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            var BenXes = _benxeService.GetAll(model.ProvinceID, model.TenBenXe, command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = BenXes.Select(x =>
                {
                    var item = new BenXeModel();
                    BenXeToBenXeModel(x, item);
                    return item;
                }),
                Total = BenXes.TotalCount
            };

            return Json(gridModel);
        }
        public ActionResult BenXeTao()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();
            var model = new BenXeModel();
            BenXeModelPrepare(model);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing"), ValidateInput(false)]
        public ActionResult BenXeTao(BenXeModel model, bool continueEditing, FormCollection form)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var diachi = model.ThongTinDiaChi.ToEntity(null);
                diachi.Latitude = form.GetValue("ThongTinDiaChi.Latitude").AttemptedValue.ToDecimal();
                diachi.Longitude = form.GetValue("ThongTinDiaChi.Longitude").AttemptedValue.ToDecimal();
                _diachiService.Insert(diachi);
                var item = new BenXe();
                BenXeModelToBenXe(model, item);
                item.DiaChiId = diachi.Id;
                _benxeService.Insert(item);
                SuccessNotification(GetLabel("BenXe.themmoithanhcong"));
                return continueEditing ? RedirectToAction("BenXeSua", new { id = item.Id }) : RedirectToAction("BenXeList");
            }

            return View(model);
        }
        public ActionResult BenXeSua(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            var item = _benxeService.GetById(id);
            if (item == null || item.isDelete)
                return RedirectToAction("BenXeList");
            var model = new BenXeModel();
            BenXeToBenXeModel(item, model);
            //default values           
            BenXeModelPrepare(model);

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing"), ValidateInput(false)]
        public ActionResult BenXeSua(BenXeModel model, bool continueEditing, FormCollection form)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            var item = _benxeService.GetById(model.Id);
            if (item == null || item.isDelete)
                return RedirectToAction("BenXeList");

            if (ModelState.IsValid)
            {
                var diachi = _diachiService.GetById(item.DiaChiId);
                BenXeModelToBenXe(model, item);
                if (diachi != null)
                {
                    diachi = model.ThongTinDiaChi.ToEntity(diachi);
                    diachi.Latitude = form.GetValue("ThongTinDiaChi.Latitude").AttemptedValue.ToDecimal();
                    diachi.Longitude = form.GetValue("ThongTinDiaChi.Longitude").AttemptedValue.ToDecimal();
                    diachi.Id = item.DiaChiId;
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

                _benxeService.Update(item);




                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("BenXeSua", item.Id);
                }
                return RedirectToAction("BenXeList");
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult BenXeXoa(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            var item = _benxeService.GetById(id);
            if (item == null || item.isDelete)
                return RedirectToAction("BenXeList");

            _benxeService.Delete(item);
            return RedirectToAction("BenXeList");
        }
        [NonAction]
        protected virtual void BenXeModelPrepare(BenXeModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (model.DiaChiId > 0)
            {
                var diachi = _diachiService.GetById(model.DiaChiId);
                model.ThongTinDiaChi = diachi.ToModel();
            }
            DiaChiInfoPrepare(model.ThongTinDiaChi);

        }

        #endregion
    }
}