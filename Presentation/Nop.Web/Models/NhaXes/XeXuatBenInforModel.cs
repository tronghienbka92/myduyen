using System;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.NhaXes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core.Domain.Chonves;

namespace Nop.Web.Models.NhaXes
{
    public class XeXuatBenInforModel:BaseNopEntityModel
    {
        public XeXuatBenInforModel()
        {
            NguonVeAll = new List<XeXuatBenItemModel>();
            NguonVeTop = new List<XeXuatBenItemModel>();
            AllLaiXePhuXes = new List<XeXuatBenItemModel.NhanVienLaiPhuXe>();
            AllXeInfo = new List<XeXuatBenItemModel.XeVanChuyenInfo>();

        }

        [UIHint("Date")]
        [NopResourceDisplayName("Ngày đi")]
        public DateTime NgayDi { get; set; }
        public int LoaiXeId { get; set; }
        public IList<SelectListItem> ListLoaiXes { get; set; }
        public int HanhTrinhId { get; set; }
        public List<SelectListItem> HanhTrinhs { get; set; }
        public List<XeXuatBenItemModel> NguonVeAll { get; set; }
        public List<XeXuatBenItemModel> NguonVeTop { get; set; }
        public List<XeXuatBenItemModel.NhanVienLaiPhuXe> AllLaiXePhuXes { get; set; }
        public List<XeXuatBenItemModel.XeVanChuyenInfo> AllXeInfo { get; set; }
        
        public int KhungGioId { get; set; }
        public ENKhungGio khunggio
        {
            get
            {
                return (ENKhungGio)KhungGioId;
            }
        }
        public IList<SelectListItem> khunggios { get; set; }
        public string ThongTin { get; set; }
    }
}