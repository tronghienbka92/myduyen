using FluentValidation.Attributes;
using Nop.Core.Domain.Chonves;
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
    public class DanhSachChuyenDiModel
    {
        [UIHint("Date")]
        public DateTime NgayDi { get; set; }
        public int HanhTrinhId { get; set; }
        public List<SelectListItem> HanhTrinhs { get; set; }
        public int LoaiXeId { get; set; }
        public IList<SelectListItem> ListLoaiXes { get; set; }
        public int LichTrinhId { get; set; }
        public List<LichTrinh> LichTrinhs { get; set; }
        /// <summary>
        /// dung de tinh toan viec focus lich trinh hien tai tren trang
        /// </summary>
        public int LichTrinhStepId { get; set; }
       
        public bool IsQuyenTaoChuyen { get; set; }
       
        public int KhungGioId { get; set; }
        public ENKhungGio khunggio
        {
            get
            {
                return (ENKhungGio)KhungGioId;
            }
        }
        public IList<SelectListItem> khunggios { get; set; }

        public string ThongTinChuyenDi { get; set; }
       
        public List<XeXuatBenItemModel.NhanVienLaiPhuXe> AllLaiXePhuXes { get; set; }
        public List<XeXuatBenItemModel.XeVanChuyenInfo> AllXeInfo { get; set; }
        

    }
}