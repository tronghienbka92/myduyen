using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.NhaXes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
     [Validator(typeof(NhanVienValidator))]
    public class NhanVienModel : BaseNopEntityModel
    {
         public NhanVienModel()
         {
             ThongTinDiaChi = new DiaChiInfoModel();
             KieuNhanViens = new List<SelectListItem>();
             GioiTinhs = new List<SelectListItem>();
             VanPhongs = new List<SelectListItem>();
             TrangThais = new List<SelectListItem>();
         }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.HoVaTen")] 
         public string HoVaTen { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.Email")] 
         public string Email { get; set; }
         [UIHint("DateNullable")]
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.NgaySinh")]
         public DateTime? NgaySinh { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.KieuNhanVienID")]
         public int KieuNhanVienID { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.KieuNhanVienID")]
         public string KieuNhanVienText { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.CMT_Id")]
         public string CMT_Id { get; set; }
         [UIHint("DateNullable")]
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.CMT_NgayCap")]
         public DateTime? CMT_NgayCap { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.CMT_NoiCap")]
         public string CMT_NoiCap { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.GioiTinhID")]
         public int GioiTinhID { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.GioiTinhID")]
         public string GioiTinhText { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.DiaChiID")]
         public int DiaChiID { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.TrangThaiID")]
         public int TrangThaiID { get; set; }
         [NopResourceDisplayName("Điện thoại")]
         public string DienThoai { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.TrangThaiID")]
         public string TrangThaiText { get; set; }
         [UIHint("DateNullable")]
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.NgayBatDauLamViec")]
         public DateTime? NgayBatDauLamViec { get; set; }
         [UIHint("DateNullable")]
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.NgayNghiViec")]
         public DateTime? NgayNghiViec { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.CustomerID")]
         public int? CustomerID { get; set; }
         public String CustomerActionText { get; set; }

         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.NhaXeID")]
         public int NhaXeID { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.VanPhongID")]
         public int? VanPhongID { get; set; }

         public DiaChiInfoModel ThongTinDiaChi { get; set; }
         public IList<SelectListItem> KieuNhanViens { get; set; }
         public IList<SelectListItem> GioiTinhs { get; set; }
         public IList<SelectListItem> VanPhongs { get; set; }
         public IList<SelectListItem> TrangThais { get; set; }

         
    }
}