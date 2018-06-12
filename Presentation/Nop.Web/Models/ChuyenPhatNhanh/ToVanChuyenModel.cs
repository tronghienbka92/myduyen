using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.ChuyenPhatNhanh
{
    public class ToVanChuyenModel : BaseNopEntityModel
    {
        public ToVanChuyenModel()
        {
            nguoivanchuyens = new List<SelectListItem>();
            tovanchuyens = new List<SelectListItem>();
        }
        public string TenTo { get; set; }
        public string MoTa { get; set; }
        public int NguoiVanChuyenId { get; set; }
        public bool Select { get; set; }
        public IList<SelectListItem> nguoivanchuyens { get; set; }
        public int ToVanChuyenIdSelect { get; set; }
        public IList<SelectListItem> tovanchuyens { get; set; }
        public string phieubiennhanids { get; set; }
    }
}