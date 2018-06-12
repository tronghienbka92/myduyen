using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Models.ChonVes
{
    public class QuanHuyenListModel
    {
        public QuanHuyenListModel()
        {
            AvailableStates = new List<SelectListItem>();
        }
        [NopResourceDisplayName("Admin.ChonVe.QuanHuyen.List.ProvinceID")]
        public int ProvinceID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.QuanHuyen.List.TenQuanHuyen")]
        public string TenQuanHuyen { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }
    }
}