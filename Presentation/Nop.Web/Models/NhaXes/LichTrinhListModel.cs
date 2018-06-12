using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class LichTrinhListModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.MaLichTrinh")]
        public string MaLichTrinh { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.TenLichTrinh")]
        public string TenLichTrinh { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.HanhTrinhId")]        
        public int HanhTrinhId { get; set; }
        public string HanhTrinhText { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.ThoiGianDi")]
        public DateTime ThoiGianDi { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.ThoiGianDen")]
        public DateTime ThoiGianDen { get; set; }
        public List<SelectListItem> HanhTrinhs { get; set; }
    }
}