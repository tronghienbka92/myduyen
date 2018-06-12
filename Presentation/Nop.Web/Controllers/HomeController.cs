using System.Web.Mvc;
using Nop.Web.Framework.Security;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Index()
        {
            //string hostHeader = Request.Headers["host"].ToLower();
            //if (hostHeader.Contains("nhaxe.chonve.vn"))
                return RedirectToAction("Index","NhaXes");
            //return View();
            
        }
        public ActionResult timkiem()
        {
            //return RedirectToAction("Index","NhaXes");
            return View();

        }
    }
}
