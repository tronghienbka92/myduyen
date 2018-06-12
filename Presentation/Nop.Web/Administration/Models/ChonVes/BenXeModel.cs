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
using Nop.Admin.Models.NhaXes;

namespace Nop.Admin.Models.ChonVes
{
    [Validator(typeof(BenXeValidator))]
    public class BenXeModel : BaseNopEntityModel
    {
        public BenXeModel()
        {
            ThongTinDiaChi = new DiaChiModel();
            HienThi = true;
        }
        [NopResourceDisplayName("ChonVe.BenXe.TenBenXe")]
        public string TenBenXe { get; set; }
        [NopResourceDisplayName("ChonVe.BenXe.DiaChiId")]
        public int DiaChiId { get; set; }
        public string DiaChiText { get; set; }        
        public DiaChiModel ThongTinDiaChi { get; set; }
        [UIHint("Picture")]
        [NopResourceDisplayName("ChonVe.BenXe.PictureId")]
        public int PictureId { get; set; }
        public string PictureUrl { get; set; }
        [NopResourceDisplayName("ChonVe.BenXe.HienThi")]
        public bool HienThi { get; set; }        
        [NopResourceDisplayName("ChonVe.BenXe.MoTa")]
        [AllowHtml]
        public string MoTa { get; set; }

    }
}