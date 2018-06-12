using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Admin.Models.Customers;
using Nop.Admin.Models.Discounts;
using Nop.Admin.Models.Stores;
using Nop.Admin.Validators.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Admin.Validators.ChonVes;

namespace Nop.Admin.Models.ChonVes
{
    [Validator(typeof(HopDongValidator))]
    public class HopDongModel : BaseNopEntityModel
    {
        public HopDongModel() {
            ListLoaiHopDong = new List<SelectListItem>();
            ListNhaXe = new List<SelectListItem>();    
        }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.MaHopDong")]
        public String MaHopDong { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.TenHopDong")]
        public String TenHopDong { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.NgayTao")]
        public DateTime NgayTao { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.NgayCapNhat")]
        public DateTime? NgayCapNhat { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.NgayKichHoat")]
        public DateTime? NgayKichHoat { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.NgayHetHan")]
        public DateTime? NgayHetHan { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.LoaiHopDongID")]
        public int LoaiHopDongID { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.HopDong.TrangThaiID")]
        public int TrangThaiID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.TrangThaiID")]
        public String TrangThaiText { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.HopDong.NguoiTaoID")]
        public int NguoiTaoID { get; set; }
         

        [NopResourceDisplayName("Admin.ChonVe.HopDong.NguoiDuyetID")]
        public int NguoiDuyetID { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.HopDong.KhachHangID")]
        public int KhachHangID { get; set; }
        public KhachHangModel KhachHang { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.HopDong.NhaXeID")]
        public int NhaXeID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.ThongTin")]
        [AllowHtml]
        public String ThongTin { get; set; }

        public IList<SelectListItem> ListLoaiHopDong { get; set; }
        public IList<SelectListItem> ListNhaXe { get; set; }

        public class KhachHangModel:BaseNopEntityModel
        {
            [NopResourceDisplayName("Admin.ChonVe.HopDong.KhachHang.Email")]
            public string Email { get; set; }

            [NopResourceDisplayName("Admin.ChonVe.HopDong.KhachHang.Fullname")]
            public string Fullname { get; set; }
            
        }

    }
}