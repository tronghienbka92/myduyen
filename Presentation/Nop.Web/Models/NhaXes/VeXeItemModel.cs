using FluentValidation.Attributes;
using Nop.Core.Domain.NhaXes;
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
    public class VeXeItemModel : BaseNopEntityModel
    {
        public string MauVe { get; set; }
        public string KyHieu { get; set; }
        public string SoSeri { get; set; }
        public bool isVeDi { get; set; }
        public string isVeDiText { get; set; }
        public int? NhanVienId { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayNhap { get; set; }
        public DateTime? NgayGiaoVe { get; set; }
        public int? NguonVeXeId { get; set; }
        public int? ChangId { get; set; }
        public string TenChang { get; set; }
        public DateTime? NgayBan { get; set; }
        public int TrangThaiId { get; set; }
        public string TrangThaiText { get; set; }
        public int MenhGiaId { get; set; }
        public decimal MenhGia { get; set; }
        public string GiaVe { get; set; }
        public string ThongTinXuatBen { get; set; }
        
    }
}