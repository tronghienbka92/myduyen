using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Models.NhaXes;
using Nop.Web.Models.VeXeKhach;
namespace Nop.Web.Models.QLPhoiVe
{
    public class ChuyenDiModel
    {
        
        public int ChuyenDiId { get; set; }
        public string TenVanPhong { get; set; }
        public bool istra { get; set; }
        public XeXuatBenItemModel chuyendihientai { get; set; }
        public List<PhoiVeModel> phoives { get; set; }
      
    }
}