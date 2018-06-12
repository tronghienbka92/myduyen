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
    public partial class DoanhThuNhanVienLuotModel : BaseNopModel
    {
        public DoanhThuNhanVienLuotModel()
        {
            ListLoai1 = new List<SelectListItem>();
            
            ListQuy = new List<SelectListItem>();
            ListMonth = new List<SelectListItem>();
            ListYear = new List<SelectListItem>();
        }
        [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.HanhTrinhId")]
        public int HanhTrinhId { get; set; }
        public int ThangId { get; set; }
        public int NamId { get; set; }
        public int QuyId { get; set; }
        public int Loai1Id { get; set; }
        public string SearchName { get; set; }
        public int SoNhanVien { get; set; }
        public int SoNhan { get; set; }
        public IList<SelectListItem> ListLoai1 { get; set; }
       
        public IList<SelectListItem> ListQuy { get; set; }
        public IList<SelectListItem> ListMonth { get; set; }
        public IList<SelectListItem> ListYear { get; set; }
       
        public NhanVienDateModel[,] DoanhThuLuot { get; set; }

        public class NhanVienDateModel : BaseNopEntityModel
        {

            
            public int Nhan { get; set; }
            public int soLuot { get; set; }
            public decimal DoanhThu { get; set; }
            public int NhanVienId { get; set; }
            public string TenNhanVien { get; set; }
        }
        public class DateModel : BaseNopEntityModel
        {
            public int nhan { get; set; }

            public int day { get; set; }

            public int month { get; set; }
            public int year { get; set; }
        }
      
    }

    
  
}