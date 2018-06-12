using FluentValidation.Attributes;
using Nop.Core.Domain.Chonves;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.NhaXes;
using Nop.Web.Validators.NhaXes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.VeXeKhach
{


    public class NguonVeXeModel : BaseNopEntityModel
    {
        public NguonVeXeModel()
        {
            ListHangHoaInNguonVe = new List<PhieuGuiHangModel.HangHoaModel>();
            PhuTrachChuyen1 = new PhuTrachChuyenModel();
            PhuTrachChuyen2 = new PhuTrachChuyenModel();
            PhuTrachChuyen3 = new PhuTrachChuyenModel();
        }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.NgayDi")]
        public DateTime NgayDi { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.NgayDi")]
        public string NgayDiInPhoi { get; set; }
        public NhaXeBasicModel NhaXeInfo { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.DiemDonId")]
        public int DiemDonId { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.SoGioChay")]
        public Decimal SoGioChay { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.TenDiemDon")]
        public string TenDiemDon { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.DiemDenId")]
        public int DiemDenId { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.TenDiemDen")]
        public string TenDiemDen { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.TenNhaXe")]
        public string TenNhaXe { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.LichTrinhId")]
        public int LichTrinhId { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.TimeCloseOnline")]
        public int TimeCloseOnline { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.TimeOpenOnline")]
        public int TimeOpenOnline { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.ThoiGianDi")]
        public DateTime ThoiGianDi { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.ThoiGianDi")]
        public string GioDi { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.ThoiGianDen")]
        public string GioDen { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.ThoiGianDen")]
        public DateTime ThoiGianDen { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.GiaVeMoi")]
        public decimal GiaVeMoi { get; set; }
        public string GiaVeMoiText { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.GiaVeCu")]
        public decimal GiaVeCu { get; set; }
        public string GiaVeCuText { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.LoaiXeId")]
        public int LoaiXeId { get; set; }
        public string TenLoaiXe { get; set; }
        public PhuTrachChuyenModel PhuTrachChuyen1 { get; set; }
        public PhuTrachChuyenModel PhuTrachChuyen2 { get; set; }
        public PhuTrachChuyenModel PhuTrachChuyen3 { get; set; }
        public string LaiXe { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.VeXeKhach.HienThi")]
        public bool HienThi { get; set; }
        public bool ToWeb { get; set; }
        public int TotalPackage { get; set; }
        public List<PhieuGuiHangModel.HangHoaModel> ListHangHoaInNguonVe { get; set; }

        //su dung hco viec dat ve online
        public int ParentId { get; set; }
        public long NgayDiTick { get; set; }
        public decimal TongTien { get; set; }
        public string KyHieuGhe { get; set; }
        public List<PhoiVe> phoives { get; set; }
        public class NhaXeBasicModel : BaseNopEntityModel
        {
            public string TenNhaXe { get; set; }
            public int LogoId { get; set; }
            public string LogoUrl { get; set; }
        }

        public class PhuTrachChuyenModel : BaseNopEntityModel
        {
            public string HoTen { get; set; }
            public string CMT { get; set; }
           
        }

    }
}