using FluentValidation.Attributes;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Nop.Web.Models.NhaXes
{
    
    public class VeXeQuyenListModel
    {
        public int HanhTrinhGiaVeId { get; set; }
        public int QuyenId { get; set; }
        public List<SelectListItem> vexequyens { get; set; }
    }
}