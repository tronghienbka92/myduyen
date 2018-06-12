using FluentValidation.Attributes;
using Nop.Admin.Validators.ChonVes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Models.ChonVes
{
    [Validator(typeof(QuanHuyenValidator))]
    public class QuanHuyenModel : BaseNopEntityModel
    {
        public QuanHuyenModel()
        {
            AvailableStates = new List<SelectListItem>();       
        }
         [NopResourceDisplayName("Admin.ChonVe.QuanHuyen.Ten")]
        public string Ten { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.QuanHuyen.Ma")]
        public string Ma { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.QuanHuyen.ProvinceID")]
        public int ProvinceID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.QuanHuyen.ProvinceID")]
        public string TenTinh { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }
    }
}