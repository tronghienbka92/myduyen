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
    public class MauVeXeModel : BaseNopEntityModel
    {
        public string MauVe { get; set; }
        public string KyHieu { get; set; }
        public string SoSeri { get; set; }
        public bool LuotDiHoacVe { get; set; }
        public string LuotDiOrVe { get; set; }
        public int? NhanVienId { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime NgayTao { get; set; }

        [UIHint("DateNullable")]
        public DateTime? NgayNhap { get; set; }

        [UIHint("DateNullable")]
        public DateTime? NgayGiaoVe { get; set; }
        public int? ChangId { get; set; }

        [UIHint("DateNullable")]
        public DateTime? NgayBan { get; set; }
        public int? LichTrinhId { get; set; }
        public string TenLichTrinh { get; set; }
        public int TrangThai { get; set; }
        public IList<SelectListItem> ddlTrangThai { get; set; }
        public string TrangThaiText { get; set; }
        public int? MenhGiaId { get; set; }
        public decimal MenhGia { get; set; }
        public int NhaXeId { get; set;}
        public string TenNhaXe { get; set; }
        public int SoLuong { get; set; }
        public int? NguoiGiaoId { get; set; }
        public string NguoiGiaoText { get; set; }
        public IList<SelectListItem> ddlmenhgias { get; set; }
        public IList<SelectListItem> ddlnhanvien { get; set; }
    }
}