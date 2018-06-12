using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.NhaXes
{
    public class HanhTrinhListModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.MaHanhTrinh")]        
        public String MaHanhTrinh { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.MoTa")]
        public string MoTa { get; set; }
    }
}