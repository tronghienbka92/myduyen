using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
   
    public class DinhViXeModel : BaseNopEntityModel
    {
        public DinhViXeModel()
         {
             ThongTinDiaChi = new DiaChiInfoModel();
             arrxeinfo = new List<XeInfoModel>();
            
         }
        
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
        public string TuyenXeChay { get; set; }
        public string GioDiText { get; set; }
        public string GioDenText { get; set; }
        public string Revenue { get; set; }
        public int SoNguoi { get; set; }
        public string BienSo { get; set; }
        
        [NopResourceDisplayName("ChonVe.NhaXe.DiemDon.DiaChiID")]
        public int DiaChiId { get; set; }
        public int NguonVeXeId { get; set; }
        public DateTime NgayDi { get; set; }
        public DiaChiInfoModel ThongTinDiaChi { get; set; }
        public List<XeInfoModel> arrxeinfo { get; set; }
        
       
    }
}