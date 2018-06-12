using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System.Collections.Generic;
using System;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.NhaXes;

namespace Nop.Web.Models.NhaXeBaoCao
{
    //[Validator(typeof(LoginValidator))]
    public partial class BCDoanhThuModel : BaseNopModel
    {
        public BCDoanhThuModel()
        {
            ListHanhTrinh = new List<SelectListItem>();
            ListLoaiHanhTrinh = new List<SelectListItem>();
        }
        [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.HanhTrinhId")]
        public int HanhTrinhId { get; set; }
        public int LoaiHanhTrinhId { get; set; }
        public IList<SelectListItem> ListHanhTrinh { get; set; }
        public IList<SelectListItem> ListLoaiHanhTrinh { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.QuanLiPhoiVe.NgayDi")]
        [UIHint("DateBirthday")]
        public DateTime NgayDi { get; set; }
       
        public bool IsPrintGiayDiDuong { get; set; }
        public bool IsBaQuay { get; set; }
        public bool IsCp1 { get; set; }
        public string ThongTin { get; set; }

    }

    
    public partial class DoanhThuItemModel : BaseNopModel
    {

        public int XeVanChuyenId { get; set; }
        public int XeXuatBenId { get; set; }
        public string BienSo { get; set; }
        public string GioXuatBen { get; set; }
        public string GioDen { get; set; }
        public int LaiXeId { get; set; }
        public string TenLaiXe { get; set; }
        public string TenNTV { get; set; }
        public int SoLuong { get; set; }
        public decimal TongDoanhThu { get; set; }
        
    }
    public partial class DoanhThuMenhGia : BaseNopModel
    {

        public string MenhGia { get; set; }
        public int SoLuong { get; set; }
        public decimal TongDoanhThu { get; set; }

    }
    public class ThongKeVeDaSuDungModel:BaseNopModel
    {
        public ThongKeVeDaSuDungModel()
        {
            ListVeTheoLoai = new List<VeQuayVipEntity>();
            ListLoaiHanhTrinh = new List<SelectListItem>();
            TinhTrangQuyens = new List<SelectListItem>();
            ListMonth = new List<SelectListItem>();
            ListYear = new List<SelectListItem>();
        }
        [UIHint("DateBirthday")]
        public DateTime NgayDi { get; set; }
        public List<VeQuayVipEntity> ListVeTheoLoai { get; set; }
        public IList<SelectListItem> ListLoaiHanhTrinh { get; set; }
        public int LoaiVeId { get; set; }
        public int LoaiHanhTrinhId { get; set; }
        public IList<SelectListItem> ListLoaiVe { get; set; }
        public int TinhTrangQuyenId { get; set; }
        public string ThongTin { get; set; }
        public IList<SelectListItem> TinhTrangQuyens { get; set; }
        public List<ThongKeVeTiepVienDaDung> VeTiepVienDaDung { get; set; }
        public int ThangId { get; set; }
        public int NamId { get; set; }
        public IList<SelectListItem> ListMonth { get; set; }
        public IList<SelectListItem> ListYear { get; set; }
    }

  
}