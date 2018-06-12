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
    public class BaoCaoLaiXePhuXeListModel
    {
        [UIHint("DateNullable")]
        public DateTime? TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DenNgay { get; set; }
        public string LaiPhuxe { get; set; }
        public class BaoCaoLaiXePhuXeModel:BaseNopEntityModel
        {
            public string HoVaTen { get; set; }
            public string CMT_Id { get; set; }
            public string DienThoai { get; set; }
            public string KieuNhanVienText { get; set; }
            public decimal TongDoanhThu { get; set; }
            public int TongChuyenDi { get; set; }
            public decimal TongLuong { get; set; }           
        }
        public class BaoCaoXeXuatBen:BaseNopEntityModel
        {
            public string TuyenXeChay { get; set; }
            public DateTime NgayDi { get; set; }
            public string LaiXe { get; set; }
            public string PhuXe { get; set; }
            public string BienSo { get; set; }
            public decimal TongDoanhThu { get; set; }

        }
    }
}