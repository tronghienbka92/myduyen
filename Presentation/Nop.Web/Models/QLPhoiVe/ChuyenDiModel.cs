using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Models.NhaXes;
namespace Nop.Web.Models.QLPhoiVe
{
    public class QLPhoiVeModel
    {
        [UIHint("Date")]
        public DateTime NgayDi { get; set; }
        public int LoaiXeId { get; set; }
        public IList<SelectListItem> ListLoaiXes { get; set; }
        public int HanhTrinhId { get; set; }
        public IList<SelectListItem> ListHanhTrinh { get; set; }
        public int NguonVeId { get; set; }
        public int ChuyenDiId { get; set; }
        public XeXuatBenItemModel chuyendihientai { get; set; }
        public List<XeXuatBenItemModel> chuyendis { get; set; }
        public string ThongTinKhachHang { get; set; }
        public int KhungGioId { get; set; }
        public ENKhungGio khunggio
        {
            get
            {
                return (ENKhungGio)KhungGioId;
            }
        }
        public IList<SelectListItem> khunggios { get; set; }
        public List<XeXuatBenItemModel.NhanVienLaiPhuXe> AllLaiXePhuXes { get; set; }
        public List<XeXuatBenItemModel.XeVanChuyenInfo> AllXeInfo { get; set; }
        public bool isTaoChuyen { get; set; }
    }
}