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
    public class TuyenHanhTrinhModel : BaseNopEntityModel
    {
        public TuyenHanhTrinhModel()
        {
          
        }
        public int NhaXeId { get; set; }
        public string Ten { get; set; }
        public string TenVietTat { get; set; }
    }
}