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
    public class HoaDonListModel : BaseNopEntityModel
    {
        public HoaDonListModel()
        {
            ListPhieuGuiHang = new List<PhieuGuiHangModel>();
            VanPhongs = new List<SelectListItem>();
            TinhTrangVanChuyens = new List<SelectListItem>();
            TinhTrangThaiToan = new List<SelectListItem>();
            XeXuatBens = new List<SelectListItem>();
        }
        [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.MaPhieu")]
        public string MaPhieu { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.TenNguoiGui")]
        public string TenNguoiGui { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.HangHoaInfo")]
        public string HangHoaInfo { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.TenNguoiNhan")]
        public string TenNguoiNhan { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.NgayTao")]
        [UIHint("DateNgayTaoPhieuHang")]
        public DateTime NgayTao { get; set; }
        [NopResourceDisplayName("Văn phòng nhận")]
        public int VanPhongNhanId { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.VanPhongGuiId")]
        public int VanPhongGuiId { get; set; }
         [NopResourceDisplayName("Thông tin chuyến")]
         public int XeXuatBenId { get; set; }
        public int CountPhieuChuaGui { get; set; }
        public int CountPhieuDangGui { get; set; }
        public int CountPhieuKetThuc { get; set; }
        public int CountPhieuDaNhan { get; set; }
      
        [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.TrangThaiVanChuyenId")]
        public int TrangThaiVanChuyenId { get; set; }
        public bool CoHang { get; set; }
        public PhieuGuiHangModel PhieuGui { get; set; }
       
        public IList<SelectListItem> TinhTrangThaiToan { get; set; }
        public IList<SelectListItem> VanPhongs { get; set; }
        public IList<SelectListItem> XeXuatBens { get; set; }
        public IList<SelectListItem> TinhTrangVanChuyens { get; set; }
        public List<PhieuGuiHangModel> ListPhieuGuiHang { get; set; }
       
       
    }
}