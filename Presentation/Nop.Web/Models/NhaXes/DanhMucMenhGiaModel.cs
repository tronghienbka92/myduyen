using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.NhaXes
{
    public class DanhMucMenhGiaModel : BaseNopEntityModel
    {
        public decimal MenhGia { get; set; }
        public int NhaXeId { get; set; }
        public bool LuotDiHoacVe { get; set; }
        public string LuotDiOrVe { get; set; }
        public string SoSeri { get; set; }
        public string SoLuong { get; set; }
    }
}