using Nop.Admin.Models.NhaXes;
using Nop.Services.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Admin.Extensions;
using Nop.Services.Customers;
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
using Nop.Services.Directory;
using Nop.Core;
using Nop.Services.Chonves;


namespace Nop.Admin.Controllers
{
    public class NhaXesController : BaseAdminController
    {
        public const int CountryID = 230;

        private readonly IStateProvinceService _stateProvinceService;
        private readonly INhaXeService _nhaxeService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly IChonVeService _chonveService;
        private readonly IDiaChiService _diachiService;
        

        public NhaXesController(IStateProvinceService stateProvinceService,
            INhaXeService nhaxeService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            IPictureService pictureService,
            IChonVeService chonveService,
            IDiaChiService diachiService
            )
        {
            this._stateProvinceService = stateProvinceService;
            this._nhaxeService = nhaxeService;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._chonveService = chonveService;
            this._diachiService = diachiService;

        }
        #region Nha Xe
        // GET: NhaXes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListNhaXe()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLNhaXe))
                return AccessDeniedView();

            var model = new NhaXeListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ListNhaXe(DataSourceRequest command, NhaXeListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLNhaXe))
                return AccessDeniedView();

            var nhaxes = _nhaxeService.GetAllNhaXe(model.TimTenNhaXe,
                command.Page - 1, command.PageSize, false, _workContext.CurrentCustomer.Id);
            var gridModel = new DataSourceResult
            {
                Data = nhaxes.Select(x => x.ToModel()),
                Total = nhaxes.TotalCount
            };

            return Json(gridModel);
        }
        public ActionResult TaoNhaXe()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLNhaXe))
                return AccessDeniedView();
            var model = new NhaXeModel();
            //default values           
            var states = _stateProvinceService.GetStateProvincesByCountryId(CountryID);
            if (states.Count > 0)
            {               
                foreach (var s in states)
                {
                    model.ThongTinDiaChi.AvailableStates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.ThongTinDiaChi.ProvinceID) });
                }
            }
            
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult TaoNhaXe(NhaXeModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLNhaXe))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {             
                var nhaxe = model.ToEntity();
                var diachi = model.ThongTinDiaChi.ToEntity();
                _diachiService.Insert(diachi);
                nhaxe.DiaChiID = diachi.Id;
                nhaxe.NguoiTaoID = _workContext.CurrentCustomer.Id;                
                _nhaxeService.InsertNhaXe(nhaxe);
                SuccessNotification(_localizationService.GetResource("Admin.chonve.nhaxe.themmoi"));
                return continueEditing ? RedirectToAction("SuaNhaXe", new { id = nhaxe.Id }) : RedirectToAction("ListNhaXe");

            }

            return View(model);
        }
        public ActionResult SuaNhaXe(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLNhaXe))
                return AccessDeniedView();
            var nhaxe = _nhaxeService.GetNhaXeById(id);
            if (nhaxe == null || nhaxe.isDelete || nhaxe.NguoiTaoID != _workContext.CurrentCustomer.Id)
                //No manufacturer found with the specified id
                return RedirectToAction("ListNhaXe");
            var model = nhaxe.ToModel();
            
            //default values           
            PrepareNhaXeModel(model);

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult SuaNhaXe(NhaXeModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLNhaXe))
                return AccessDeniedView();

            var nhaxe = _nhaxeService.GetNhaXeById(model.Id);
            if (nhaxe == null || nhaxe.isDelete)
                //No manufacturer found with the specified id
                return RedirectToAction("ListNhaXe");

            if (ModelState.IsValid)
            {
                int prevLogoID = nhaxe.LogoID;
                int prevAnhDaiDienID = nhaxe.AnhDaiDienID;
               
                //diachi.Id = nhaxe.DiaChiID;
                nhaxe = model.ToEntity(nhaxe);
                _nhaxeService.UpdateNhaXe(nhaxe);
                var diachi = _diachiService.GetById(nhaxe.DiaChiID);
                diachi = model.ThongTinDiaChi.ToEntity(diachi);
                diachi.Id = nhaxe.DiaChiID;
                _diachiService.Update(diachi);

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

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("SuaNhaXe", nhaxe.Id);
                }
                return RedirectToAction("ListNhaXe");
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult XoaNhaXe(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLNhaXe))
                return AccessDeniedView();

            var nhaxe = _nhaxeService.GetNhaXeById(id);
            if (nhaxe == null || nhaxe.isDelete)
                //No manufacturer found with the specified id
                return RedirectToAction("ListNhaXe");

            _nhaxeService.DeleteNhaXe(nhaxe);

            return RedirectToAction("ListNhaXe");
        }
        [NonAction]
        protected virtual void PrepareNhaXeModel(NhaXeModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            var diachi = _diachiService.GetById(model.DiaChiID);
            model.ThongTinDiaChi = diachi.ToModel();
            var states = _stateProvinceService.GetStateProvincesByCountryId(CountryID);
            if (states.Count > 0)
            {
                foreach (var s in states)
                {
                    model.ThongTinDiaChi.AvailableStates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.ThongTinDiaChi.ProvinceID) });
                }
            }
            if(model.DiaChiID>0 && diachi.ProvinceID>0)
            {
                var quanhuyens = _diachiService.GetQuanHuyenByProvinceId(diachi.ProvinceID);
                foreach (var s in quanhuyens)
                {
                    model.ThongTinDiaChi.AvailableQuanHuyens.Add(new SelectListItem { Text = s.Ten, Value = s.Id.ToString(), Selected = (s.Id == model.ThongTinDiaChi.QuanHuyenID) });
                }
            }

        }
        #endregion

    }
}