using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;
using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.NhaXes
{
    public class NhaXe : BaseEntity
    {
        public string MaNhaXe { get; set; }
        public string TenNhaXe { get; set; }
        public string GioiThieu { get; set; }
        public int LogoID { get; set; }
        public int AnhDaiDienID { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }        
        public string Fax { get; set; }
        public string HotLine { get; set; }
        public int DiaChiID { get; set; }
        public bool isDelete { get; set; }
        public int NguoiTaoID { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        public string DieuKhoanGuiHang { get; set; }
    }
}
