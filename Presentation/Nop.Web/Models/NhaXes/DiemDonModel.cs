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
     [Validator(typeof(DiemDonValidator))]
    public class DiemDonModel : BaseNopEntityModel
    {
         public DiemDonModel()
         {
             ThongTinDiaChi = new DiaChiInfoModel();
             LoaiDiemDons = new List<SelectListItem>();
             VanPhongs = new List<SelectListItem>();
             
         }

        [NopResourceDisplayName("ChonVe.NhaXe.DiemDon.TenDiemDon")]      
        public string TenDiemDon { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.DiemDon.LoaiDiemDonId")]
        public int LoaiDiemDonId { get; set; }
        public string LoaiDiemDonText { get; set; }
        public IList<SelectListItem> LoaiDiemDons { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.DiemDon.DiaChiID")]
        public int DiaChiId { get; set; }
        public DiaChiInfoModel ThongTinDiaChi { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.DiemDon.VanPhongId")]
        public int VanPhongId { get; set; }
        [NopResourceDisplayName("Mã màu")]
        public string MaMau { get; set; }
        public string VanPhongText { get; set; }

        public IList<SelectListItem> VanPhongs { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.DiemDon.BenXeId")]
        public int BenXeId { get; set; }
        public string BenXeText { get; set; }        

        

    }
}