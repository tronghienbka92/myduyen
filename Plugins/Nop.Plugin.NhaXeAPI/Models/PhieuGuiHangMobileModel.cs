using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.NhaXeAPI.Models
{
    public class PhieuGuiHangMobileModel : BaseNopEntityModel
    {
        public string MaPhieu { get; set; }
        public string NguoiGuiTen { get; set; }
        public string NguoiGuiDienThoai { get; set; }
        public string DiemGui { get; set; }
        public string NguoiNhanTen { get; set; }
        public string NguoiNhanDienThoai { get; set; }
        public string DiemTra { get; set; }
        public string TenHang { get; set; }
        public int SoTien { get; set; }

    }
}
