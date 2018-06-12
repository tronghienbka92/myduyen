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
    public class NguoiVanChuyenModel : BaseNopEntityModel
    {
        public string HoVaTen { get; set; }
        public string DienThoai { get; set; }
        public string CMT { get; set; }
        public int ToVanChuyenId { get; set; }
        public IList<SelectListItem> tovanchuyens { get; set; }
    }
}