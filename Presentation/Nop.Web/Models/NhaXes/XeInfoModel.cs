using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.NhaXes;

namespace Nop.Web.Models.NhaXes
{
    [Validator(typeof(XeInfoValidator))]
    public class XeInfoModel : BaseNopEntityModel
    {
        public XeInfoModel()
        {
            TrangThaiXes = new List<SelectListItem>();
            LoaiXes = new List<SelectListItem>();
        }
        [NopResourceDisplayName("ChonVe.NhaXe.XeInfo.TenXe")]
        public string TenXe { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.XeInfo.LoaiXeId")]
        public int LoaiXeId { get; set; }
        public string LoaiXeText { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.XeInfo.BienSo")]
        public string BienSo { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.XeInfo.DienThoai")]
        public string DienThoai { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.XeInfo.TrangThaiXeId")]
        public int TrangThaiXeId { get; set; }
        public string TenHanhTrinh { get; set; }
        public int SoNguoi { get; set; }       
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        public string NgayGPSText { get; set; }
        public int NguonVeXeId { get; set; }
        public string NgayDi{ get; set; }
        public string TrangThaiXeText { get; set; }
        public IList<SelectListItem> TrangThaiXes { get; set; }
        public IList<SelectListItem> LoaiXes { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayDangKiem { get; set; }
        public string PhuHieuXe { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayDangKy { get; set; }
        public string BaoHiem { get; set; }
       
      
    }
}