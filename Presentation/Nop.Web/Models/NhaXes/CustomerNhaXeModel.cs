using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.VeXeKhach;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Nop.Core.Domain.NhaXes;

namespace Nop.Web.Models.NhaXes

{
    public class CustomerNhaXeModel : BaseNopEntityModel
    {

        [NopResourceDisplayName("quanLyPhoiVe.NhaXe.KhachHang.HoTen")]
        public string HoTen { get; set; }
        [NopResourceDisplayName("quanLyPhoiVe.NhaXe.KhachHang.Sdt")]
        public string DienThoai { get; set; }
        [NopResourceDisplayName("quanLyPhoiVe.NhaXe.KhachHang.SearchInfo")]
        public string SearchInfo { get; set; }
        [NopResourceDisplayName("quanLyPhoiVe.NhaXe.KhachHang.CustomerId")]
        public int CustomerId { get; set; }
        [NopResourceDisplayName("quanLyPhoiVe.NhaXe.KhachHang.NhaXeId")]
        public int NhaXeId { get; set; }
        public string DiaChiLienHe { get; set; }
        public string TenNhaXe { get; set; }
        
     
    }
}
