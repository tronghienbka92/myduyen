using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.NhaXes
{
    public class NhanVienListModel
    {
        [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.TenNhanVien")] 
        public string TenNhanVien { get; set; }
    }
}