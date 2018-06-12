using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Models.ChonVes
{
    public class BenXeListModel
    {
        public BenXeListModel()
        {
            AvailableStates = new List<SelectListItem>();
        }
        [NopResourceDisplayName("ChonVe.BenXe.ProvinceID")]
        public int ProvinceID { get; set; }
        [NopResourceDisplayName("ChonVe.BenXe.TenBenXe")]
        public string TenBenXe { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }
    }
}