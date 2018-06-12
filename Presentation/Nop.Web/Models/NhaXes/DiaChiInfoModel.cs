using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;


namespace Nop.Web.Models.NhaXes
{
    public class DiaChiInfoModel : BaseNopEntityModel
    {
        public DiaChiInfoModel()
        {            
            AvailableStates = new List<SelectListItem>();
            AvailableQuanHuyens = new List<SelectListItem>();        
        }

        [NopResourceDisplayName("Admin.ChonVe.DiaChi.DiaChi1")]
        public string DiaChi1 { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.DiaChi.DiaChi2")]
        public string DiaChi2 { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.DiaChi.ProvinceID")]
        public int ProvinceID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.DiaChi.QuanHuyenID")]
        public int? QuanHuyenID { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.DiaChi.MaBuuDien")]
        public string MaBuuDien { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.DiaChi.DienThoai")]
        public string DienThoai { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.DiaChi.Fax")]
        public string Fax { get; set; }
        /// <summary>
        /// Thong tin dinh vi GPS
        /// </summary>
        [NopResourceDisplayName("Admin.ChonVe.DiaChi.Latitude")]
        public String Latitude { get; set; }
        /// <summary>
        /// Thong tin dinh vi GPS
        /// </summary>
        [NopResourceDisplayName("Admin.ChonVe.DiaChi.Longitude")]
        public String Longitude { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }
        public IList<SelectListItem> AvailableQuanHuyens { get; set; }
    }
}