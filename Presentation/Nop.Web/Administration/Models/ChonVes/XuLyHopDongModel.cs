using Nop.Core.Domain.Chonves;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Models.ChonVes
{
    public class XuLyHopDongModel : BaseNopEntityModel
    {

        public XuLyHopDongModel()
        {
            ListGiaHan=new List<SelectListItem>();
          
        }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.MaHopDong")]
        public string MaHopDong { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.TenHopDong")]
        public string TenHopDong { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.NgayKetThuc")]
        public DateTime? NgayKetThuc { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.NgayKichHoat")]
        public DateTime? NgayKichHoat { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.NguoiTaoID")]
        public int NguoiTaoID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.TenNguoiTao")]
        public string TenNguoiTao { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.NguoiDuyetID")]
        public int NguoiDuyetID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.TenNguoiDuyet")]
        public string TenNguoiDuyet { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.TrangThaiID")]
        public int TrangThaiID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.TrangThaiText")]
        public string TrangThaiText { get; set; }
        public ENTrangThaiHopDong TrangThai
        {
            get 
            {
                return (ENTrangThaiHopDong)TrangThaiID;
            }
        }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.LoaiHopDongID")]
        public int LoaiHopDongID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.LoaiHopDongText")]
        public string LoaiHopDongText { get; set; }
        public ENLoaiHopDong LoaiHopDong
        {
            get
            {
                return (ENLoaiHopDong)LoaiHopDongID;
            }
        }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.NhaXeID")]
       
        public int NhaXeID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.NhaXeText")]
        public string NhaXeText { get; set; }
       
        [NopResourceDisplayName("Admin.ChonVe.HopDong.ThongTin")]
        [AllowHtml]
        public string ThongTin { get; set; }
        public bool isManager { get; set; }
        public int IdCurrent { get; set; }
          [NopResourceDisplayName("Admin.ChonVe.HopDong.GiaHanID")]
        public int GiaHanID { get; set; }
        public IList<SelectListItem> ListGiaHan { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.HopDong.KhachHangID")]
        public int KhachHangID { get; set; }
        public KhachHangModel KhachHang { get; set; }
        public class KhachHangBasic
        {
            public int Id { get; set; }
            
            public string Email { get; set; }
              
        }
        public class KhachHangModel : BaseNopEntityModel
        {
            public KhachHangModel()
            {
                this.AvailableCustomerRoles = new List<KhachHangRoleModel>();
            }
            [Required]
            [DataType(DataType.EmailAddress)]
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.Email")]
            public string Email { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.HopDong")]
          
            public int HopDongId { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.Fields.TenKhachHang")]
           
            public string Fullname { get; set; }
            [Required]
            [StringLength(60, MinimumLength = 8)]
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.ConfirmPassword")]
            [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password error.")]
            public string ConfirmPassword { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.FirstName")]
            [Required]
            public string FirstName { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.lastName")]
            [Required]
            public string LastName { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.Active")]
            public bool Active { get; set; }
            //customer roles
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.CustomerRoles")]
            public int CustomerRoleId { get; set; }
            public List<KhachHangRoleModel> AvailableCustomerRoles { get; set; }
          
            

        }
    }
}