using FluentValidation.Attributes;
using Nop.Core.Domain.NhaXes;
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
      [Validator(typeof(PhieuGuiHangValidator))]
    public class PhieuGuiHangModel : BaseNopEntityModel
    {
        public PhieuGuiHangModel()
         {
            
             VanPhongs = new List<SelectListItem>();
             NguoiKiemTraHangs = new List<SelectListItem>();
             NguonVes = new List<SelectListItem>();
             XeXuatBens = new List<SelectListItem>();
             ListHangHoaInPhieuGui = new List<HangHoaModel>();
             ListXeXuatBen = new List<XeXuatBenModel>();
             VanPhongGui = new VanPhongModel();
             VanPhongNhan = new VanPhongModel();
             NguoiGui = new KhachHangModel();
             NguoiNhan = new KhachHangModel();
             HangHoa = new HangHoaModel();
             SoLuongHang = 1;
         }

        [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.MaPhieu")]
        public string MaPhieu { get; set; }       
        public bool NhanHang { get; set; }
        public string TenNguoiKiemTraHang { get; set; }
        public string TenNguonVe { get; set; }
        public string ChuoiPhieuGuiHangId { get; set; }
        public string TotalPackage { get; set; }
        public string TongCanNang { get; set; }
        public string TongGiaTriHangHoa { get; set; }
        public string HangHoaInfo { get; set; }
        public string TongCuocPhi { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.NguoiKiemTraHangId")]
        public int NguoiKiemTraHangId { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.DaThuCuoc")]
        public bool DaThuCuoc { get; set; }
        public bool CanPrinfPhieuGui { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.NguonVeId")]
        public int? NguonVeId { get; set; }
         public int SoLuongHang { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.XeXuatBenId")]
        public int? XeXuatBenId { get; set; }
         [NopResourceDisplayName("Ghi chú phiếu hàng")]
         public string GhiChu { get; set; }
         public string ThanhToan { get; set; }
         public bool HasXeXuatBen { get; set; }
         public DateTime NgayTao { get; set; }
          [NopResourceDisplayName("Ngày thanh toán")]
          [UIHint("DateNgayThanhToan")]
         public DateTime? NgayThanhToan { get; set; }
         public int? NhanVienThuTienId { get; set; }
         public DateTime NgayDi { get; set; }
         public DateTime NgayNhan { get; set; }
         public string TenXeXuatBen { get; set; }
      
          [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.TinhTrangVanChuyen")]
        public string TinhTrangVanChuyen { get; set; }     
        public string LogoNhaXe { get; set; }
        public string DieuKhoanGuiHang { get; set; }
        public string TenNhaXe { get; set; }
        public string DiaChiNhaXe { get; set; }       
        public string SdtNhaXe { get; set; }
        public VanPhongModel VanPhongGui { get; set; }
        public VanPhongModel VanPhongNhan { get; set; }  
        public KhachHangModel NguoiGui { get; set; }
        public HangHoaModel HangHoa { get; set; }
        public KhachHangModel NguoiNhan { get; set; }
        public HistoryXeXuatBen XeXuatBen { get; set; }      
        public List<HangHoaModel> ListHangHoaInPhieuGui { get; set; }
        public List<XeXuatBenModel> ListXeXuatBen { get; set; }
        public IList<SelectListItem> VanPhongs { get; set; }
        public IList<SelectListItem> NguoiKiemTraHangs { get; set; }
        public IList<SelectListItem> NguonVes { get; set; }
        public IList<SelectListItem> XeXuatBens { get; set; }
        public class VanPhongModel : BaseNopEntityModel
        {
            
            
            [NopResourceDisplayName("Văn phòng nhận")]
            public string TenVanPhong { get; set; }
          
        }
           [Validator(typeof(KhachHangValidator))]
        public class KhachHangModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.HoTen")]
            public string HoTen { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.SoDienThoai")]
            public string SoDienThoai { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.DiaChi")]
            public string DiaChi { get; set; }
        }
        public class XeXuatBenModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.TenXeXuatBen")]
            public string TenXeXuatBen { get; set; }
            public bool ChonXe { get; set; }
            
        }
           [Validator(typeof(HangHoaValidator))]
        public class HangHoaModel:BaseNopEntityModel
        {
               public HangHoaModel()
               {
                   SoLuong = 1;
               }

            [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.TenHangHoa")]
           
            public string TenHangHoa { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.CanNang")]
            [UIHint("DecimalThapPhan")]
            public decimal CanNang { get; set; }
            public string CanNangText { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.LoaiHangHoa")]
            public string LoaiHangHoa { get; set; }
            public bool ChonLoaiHangHoa { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.LoaiHangHoaId")]
            public int LoaiHangHoaId { get; set; }
            public int MaPhieuGuiId { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.GiaTri")]
            public decimal GiaTri { get; set; }
            
            [NopResourceDisplayName("Số lượng")]
            [Range(1, 100)]
            public int SoLuong { get; set; }
            public string GiaTriText { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.HangHoa.GiaCuoc")]
            public decimal GiaCuoc { get; set; }
            public string GiaCuocText { get; set; }
            [NopResourceDisplayName("Ghi chú hàng hóa")]
            public string GhiChu { get; set; }
            public IList<SelectListItem> LoaiHangHoas { get; set; }
        }
    }
}