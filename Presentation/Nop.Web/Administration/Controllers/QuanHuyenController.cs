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

namespace Nop.Admin.Controllers
{
    public class QuanHuyenController : BaseAdminController
    {
        #region "Khoi Tao"
        private readonly IStateProvinceService _stateProvinceService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;
        private readonly IChonVeService _chonveService; 
        private readonly IQuanHuyenService _quanhuyenService;

        public QuanHuyenController(IChonVeService chonveService,
            IStateProvinceService stateProvinceService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            IQuanHuyenService quanhuyenService
            )
        {
            this._chonveService = chonveService;
            this._stateProvinceService = stateProvinceService;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._workContext = workContext;
            this._quanhuyenService = quanhuyenService;

        }
        #endregion
        // GET: QuanHuyen
        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();
            var model = new QuanHuyenListModel();
            var states = _stateProvinceService.GetStateProvincesByCountryId(NhaXesController.CountryID);
            if (states.Count > 0)
            {
                
                model.AvailableStates.Add(new SelectListItem { Text = _localizationService.GetResource("admin.common.all"), Value = "0", Selected = true });
                foreach (var s in states)
                {
                    model.AvailableStates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = false });
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult List(DataSourceRequest command, QuanHuyenListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            var quanhuyens = _quanhuyenService.GetAll(model.ProvinceID, model.TenQuanHuyen,
                command.Page - 1, command.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = quanhuyens.Select(x =>
                {
                    var m = new QuanHuyenModel();
                    QuanHuyenModelPrepare(m, x,false);
                    return m;
                }),
                Total = quanhuyens.TotalCount
            };

            return Json(gridModel);
        }
        public ActionResult Create(int ProvinceID=0)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            var model = new QuanHuyenModel();
            //default values           
            model.ProvinceID = ProvinceID;
            QuanHuyenModelPrepare(model, null, true);

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(QuanHuyenModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var quanhuyen = new QuanHuyen();
                quanhuyen.Ma = model.Ma;
                quanhuyen.Ten = model.Ten;
                quanhuyen.ProvinceID = model.ProvinceID;
                _quanhuyenService.Insert(quanhuyen);
                SuccessNotification(_localizationService.GetResource("Admin.chonve.quanhuyen.themmoithanhcong"));
                return continueEditing ? RedirectToAction("Edit", new { id = quanhuyen.Id }) : RedirectToAction("List");

            }

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            var _item = _quanhuyenService.GetById(id);
            if (_item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("List");
            var model = new QuanHuyenModel();
            QuanHuyenModelPrepare(model, _item, true);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(QuanHuyenModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            var _item = _quanhuyenService.GetById(model.Id);
            if (_item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                _item.Ma = model.Ma;
                _item.Ten = model.Ten;
                _item.ProvinceID = model.ProvinceID;
                _quanhuyenService.Update(_item);
                SuccessNotification(_localizationService.GetResource("Admin.chonve.quanhuyen.capnhatthanhcong"));

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("Edit", _item.Id);
                }
                return RedirectToAction("List");
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.QLDanhMuc))
                return AccessDeniedView();

            var _item = _quanhuyenService.GetById(id);
            if (_item == null)
                //No manufacturer found with the specified id
                return RedirectToAction("List");

            _quanhuyenService.Delete(_item);

            return RedirectToAction("List");
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetQuanHuyenByProvinceId(string ProvinceId,
            bool? addSelectStateItem, bool? addAsterisk)
        {
            //permission validation is not required here


            // This action method gets called via an ajax request
            if (String.IsNullOrEmpty(ProvinceId))
                throw new ArgumentNullException("ProvinceId");

            var quanhuyens = _quanhuyenService.GetAllByProvinceID(Convert.ToInt32(ProvinceId));
            var result = (from s in quanhuyens
                          select new { id = s.Id, name = s.Ten }).ToList();
            if (addAsterisk.HasValue && addAsterisk.Value)
            {
                //asterisk
                result.Insert(0, new { id = 0, name = "*" });
            }
            else
            {
                //some country is selected
                if (result.Count == 0)
                {
                    //country does not have states
                    result.Insert(0, new { id = 0, name = _localizationService.GetResource("Admin.ChonVe.QuanHuyen.SelectQuanHuyen") });
                }
                else
                {
                    //country has some states
                    if (addSelectStateItem.HasValue && addSelectStateItem.Value)
                    {
                        result.Insert(0, new { id = 0, name = _localizationService.GetResource("Admin.ChonVe.QuanHuyen.SelectQuanHuyen") });
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected virtual void QuanHuyenModelPrepare(QuanHuyenModel model, QuanHuyen quanhuyen, bool isEdit)
        {
            if (quanhuyen != null)
            {
                model.Id = quanhuyen.Id;
                model.Ten = quanhuyen.Ten;
                model.Ma = quanhuyen.Ma;
                model.ProvinceID = quanhuyen.ProvinceID;
                var provinceinfo = _stateProvinceService.GetStateProvinceById(model.ProvinceID);
                model.TenTinh = provinceinfo.Name;
            }
            if (isEdit)
            {
                var states = _stateProvinceService.GetStateProvincesByCountryId(NhaXesController.CountryID);
                if (states.Count > 0)
                {
                    foreach (var s in states)
                    {
                        model.AvailableStates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.ProvinceID) });
                    }
                }
            }
            

        }
    }
}