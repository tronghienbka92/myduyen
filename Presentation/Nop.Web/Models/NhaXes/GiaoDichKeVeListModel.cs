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
    public class GiaoDichKeVeListModel
    {
        public GiaoDichKeVeListModel()
        {
            phanloais = new List<SelectListItem>(); 
        }
        public int PhanLoaiId { get; set; }
        public IList<SelectListItem> phanloais { get; set; }
        public string MaGiaoDich { get; set; }
        public int NguoiGiaoId { get; set; }
        public int NguoiNhanId { get; set; }

        [UIHint("DateNullable")]
        public DateTime? TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DenNgay { get; set; }
     
    }
}