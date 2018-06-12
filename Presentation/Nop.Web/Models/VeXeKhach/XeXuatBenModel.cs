using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.NhaXes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.VeXeKhach
{
    public class XeXuatBenModel : BaseNopEntityModel
    {

        public int NguonVeId { get; set; }
        public NguonVeXeModel nguonvexe { get; set; }
         [UIHint("Date")]
         [NopResourceDisplayName("Ngày đi")]
        public DateTime NgayDi { get; set; }
        public bool CanXuatBen { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.LaiXe1Id")]
        public int LaiXe1Id { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.LaiXe1Id")]
        public string TenLaiXe1 { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.LaiXe2Id")]
        public int LaiXe2Id { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.LaiXe2Id")]
        public string TenLaiXe2 { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.LaiXe3Id")]
        public int LaiXe3Id { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.LaiXe3Id")]
        public string TenLaiXe3 { get; set; }
        public string ChuoiPhieuGuiHangId { get; set; }
        public string Email { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.XeVanChuyenId")]
        public int XeVanChuyenId { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.SoGheDatCho")]
        public int SoNguoi { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.NguoiTaoId")]
        public int NguoiTaoId { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.NgayTao")]
        public DateTime NgayTao { get; set; }
         [UIHint("Date")]
        public DateTime NgayXuatBen { get; set; }
        [NopResourceDisplayName("Biển số xe")]
        public string TenBienSoXe { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.NguonVe.TuyenXeChay")]
        public string TuyenXeChay { get; set; }
        public List<SelectListItem> BienSoXes { get; set; }
        public List<SelectListItem> LaiXes { get; set; }


        public List<LaiXePhuXeModel> LaiXePhuXes { get; set; }
        public string[] LaiXePhuXesInNguonVe { get; set; }
        public LaiXePhuXeModel LaiXePhuXe { get; set; }
        public string GhiChu { get; set; }
        public List<PhieuGuiHangModel> phieuguihangs { get; set; }
        public class LaiXePhuXeModel:BaseNopEntityModel
        {
            public Boolean LaiXeCheckbox { get; set; }
            public string TenLaiXe { get; set; }
        }
       
    }
}