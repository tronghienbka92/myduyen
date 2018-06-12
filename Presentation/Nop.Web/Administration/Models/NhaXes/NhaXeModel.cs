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
using Nop.Admin.Validators.NhaXes;

namespace Nop.Admin.Models.NhaXes
{
    [Validator(typeof(NhaXeValidator))]
    public class NhaXeModel : BaseNopEntityModel
    {
        public NhaXeModel()
        {
            
            ThongTinDiaChi = new DiaChiModel();
        }
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.MaNhaXe")]
        public string MaNhaXe { get; set; }
        
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.TenNhaXe")]
        public string TenNhaXe { get; set; }
        
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.GioiThieu")]
        [AllowHtml]
        public string GioiThieu { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.Logo")]
        public int LogoID { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.AnhDaiDien")]
        public int AnhDaiDienID { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.Email")]
        public string Email { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.DienThoai")]
        public string DienThoai { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.Fax")]
        public string Fax { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.HotLine")]
        public string HotLine { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.DiaChi")]
        public int DiaChiID { get; set; }
        public DiaChiModel ThongTinDiaChi { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.NguoiTao")]
        public int NguoiTaoID { get; set; }

       
    }
}