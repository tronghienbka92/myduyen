using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.ChuyenPhatNhanh;
using Nop.Web.Validators.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
     [Validator(typeof(VanPhongValidator))]
    public class VanPhongModel : BaseNopEntityModel
    {
         public VanPhongModel()
         {
             ThongTinDiaChi = new DiaChiInfoModel();
             KieuVanPhongs = new List<SelectListItem>();
         }
         public string Ma { get; set; }
         [NopResourceDisplayName("ChonVe.NhaXe.VanPhong.TenVanPhong")]      
        public string TenVanPhong { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VanPhong.KieuVanPhongID")]      
        public int KieuVanPhongID { get; set; }
        [NopResourceDisplayName("Yêu cầu duyệt hủy")]
        public bool IsYeuCauDuyetHuy { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VanPhong.KieuVanPhongID")]
        public string KieuVanPhongText { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VanPhong.DienThoaiDatVe")]
        public string DienThoaiDatVe { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VanPhong.DienThoaiGuiHang")]
        public string DienThoaiGuiHang { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.VanPhong.DiaChiID")]
        public int DiaChiID { get; set; }
        public string DiaChiText { get; set; }
        public DiaChiInfoModel ThongTinDiaChi { get; set; }
        public IList<SelectListItem> KieuVanPhongs { get; set; }
        public int KhuVucId { get; set; }
        public IList<SelectListItem> khuvucs { get; set; }
        public int[] SelectedToVanChuyenIds { get; set; }
        public List<ToVanChuyenModel> AllToVanChuyens { get; set; }

    }
}