using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.Extensions;
using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class VeXeChuyenDiVeModel : XeXuatBenItemModel
    {
        public string TenLaiXe1 { get; set; }
        public string TenLaiXe2 { get; set; }
        public decimal TongDoanhThu { get; set; }
    }
}