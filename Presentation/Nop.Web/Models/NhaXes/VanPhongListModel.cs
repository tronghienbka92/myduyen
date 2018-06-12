using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.NhaXes
{
    public class VanPhongListModel
    {
        [NopResourceDisplayName("ChonVe.NhaXe.VanPhong.TimTenVanPhong")]        
        public String TimTenVanPhong { get; set; }
    }
}