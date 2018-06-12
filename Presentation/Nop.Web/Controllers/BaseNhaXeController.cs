using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Infrastructure;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Seo;
using Nop.Core.Domain.NhaXes;
using System;

namespace Nop.Web.Controllers
{
    [CheckAffiliate]
    [StoreClosed]
    [PublicStoreAllowNavigation]
    [LanguageSeoCode]
    [NopHttpsRequirement(SslRequirement.NoMatter)]
    [WwwRequirement]
    public abstract partial class BaseNhaXeController : BaseController
    {
        public const int CountryID = 230;
        /// <summary>
        /// Initialize controller
        /// </summary>
        /// <param name="requestContext">Request context</param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //set work context to admin mode
            //if (Session["NhaXeId"]!=null)
                //EngineContext.Current.Resolve<IWorkContext>().NhaXeId = Convert.ToInt32(Session["NhaXeId"]);
            base.Initialize(requestContext);
        }

        /// <summary>
        /// On exception
        /// </summary>
        /// <param name="filterContext">Filter context</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
                LogException(filterContext.Exception);
            base.OnException(filterContext);
        }

        /// <summary>
        /// Access denied view
        /// </summary>
        /// <returns>Access denied view</returns>
        protected ActionResult AccessDeniedView()
        {
            return RedirectToAction("AccessDenied", "NhaXes");
        }
        protected ActionResult AccessDeniedPartialView()
        {
            return RedirectToAction("PartialViewAccessDenied", "NhaXes");
        }

        protected ActionResult AccessNoView()
        {
            return RedirectToAction("PagNotFound", "NhaXes");
        }
        /// <summary>
        /// Save selected TAB index
        /// </summary>
        /// <param name="index">Idnex to save; null to automatically detect it</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected void SaveSelectedTabIndex(int? index = null, bool persistForTheNextRequest = true)
        {
            //keep this method synchronized with
            //"GetSelectedTabIndex" method of \Nop.Web.Framework\ViewEngines\Razor\WebViewPage.cs
            if (!index.HasValue)
            {
                int tmp;
                if (int.TryParse(this.Request.Form["selected-tab-index"], out tmp))
                {
                    index = tmp;
                }
            }
            if (index.HasValue)
            {
                string dataKey = "nop.selected-tab-index";
                if (persistForTheNextRequest)
                {
                    TempData[dataKey] = index;
                }
                else
                {
                    ViewData[dataKey] = index;
                }
            }
        }
        protected ENKieuDuLieu getKieuDuLieu(ENNhaXeCauHinh code)
        {
            switch (code)
            {
                case ENNhaXeCauHinh.KY_GUI_DVGT_CONG_KENH:
                case ENNhaXeCauHinh.KY_GUI_DVGT_GIA_TRI:
                case ENNhaXeCauHinh.KY_GUI_DVGT_DIEN_TU_DE_VO:
                    return ENKieuDuLieu.PHAN_TRAM;
                case ENNhaXeCauHinh.KY_GUI_DVGT_NHE_CONG_KENH:
                    return ENKieuDuLieu.SO;
                default:
                    return ENKieuDuLieu.KY_TU;
            }
        }
        protected ENKieuDuLieu getKieuDuLieu(string code)
        {
            ENNhaXeCauHinh ma = (ENNhaXeCauHinh)Enum.Parse(typeof(ENNhaXeCauHinh), code);
            return getKieuDuLieu(ma);            
        }
        protected ActionResult ThanhCong()
        {
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        protected ActionResult DaCoNguoiDatCho()
        {
            return Json("DaDat", JsonRequestBehavior.AllowGet);
        }
        protected ActionResult Loi()
        {
            return Json("LOI", JsonRequestBehavior.AllowGet);
        }
        protected ActionResult Loi(string msg)
        {
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}