using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System.Collections.Generic;

namespace Nop.Admin.Models.ChonVes
{
    public class HopDongListModel : BaseNopModel
    {
      
        public HopDongListModel()
        {
            isManager = false;
            NguoiTaos = new List<SelectListItem>();
            ListTrangThai = new List<SelectListItem>();
        }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.List.MaHopDong")]
        [AllowHtml]
        public string TimMaHopDong { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.HopDong.List.TenHopDong")]
        [AllowHtml]
        public string TimTenHopDong { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.HopDong.List.NguoiTaoId")]
        public int NguoiTaoId { get; set; }
        public List<SelectListItem> NguoiTaos { get; set; }
          [NopResourceDisplayName("Admin.ChonVe.HopDong.TrangThaiID")]
        public int TrangThaiID { get; set; }
        public IList<SelectListItem> ListTrangThai { get; set; }
        public bool isManager { get; set; }
        
    }
}