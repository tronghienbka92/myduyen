using FluentValidation.Attributes;
using Nop.Core.Domain.Chonves;
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
    public class ListGiayDiDuongModel : BaseNopEntityModel
    {
        public ListGiayDiDuongModel()
        {
            LuotDi = new GiayDiDuongModel() ;
              LuotVe = new GiayDiDuongModel() ;
           
            
        }
        public GiayDiDuongModel LuotDi { get; set; }
        public GiayDiDuongModel LuotVe { get; set; }
        public string NgayDi { get; set; }
        public string BienSo { get; set; }
        public string TenLaiXe { get; set; }
        public string TenNTV { get; set; }
    }
    public class GiayDiDuongModel : BaseNopEntityModel
    {

        public GiayDiDuongModel()
        {
            DiemDons = new List<HanhTrinhDiemDonModel>();
            AddHanhTrinhDiemDonModel = new HanhTrinhDiemDonModel();
            AvailableDiemDons = new List<SelectListItem>();
            
        }

        public string TenHanhTrinh { get; set; }
        
        public string TenLaiXe { get; set; }
        public string TenNTV { get; set; }
        public int SoDiemDon { get; set; }
        public string NgayDi { get; set; }
        public bool IsLuotDi { get; set; }
        public HanhTrinhDiemDonModel AddHanhTrinhDiemDonModel { get; set; }      
        public HanhTrinhGiaVeModelToArray[,] HanhTrinhGiaVes { get; set; }
        public List<HanhTrinhDiemDonModel> DiemDons { get; set; }
        public IList<SelectListItem> AvailableDiemDons { get; set; }
        public int TongSoLuong { get; set; }
        public NguonVeXe nguonvexe { get; set; }
        
        public string TongDoanhThu { get; set; }
        public class HanhTrinhGiaVeModelToArray : BaseNopEntityModel
        {

            public int FromDiemDonId { get; set; }
            public int ToDiemDonId { get; set; }
            public decimal GiaNguonVe { get; set; }
            public int NguonVeXeId { get; set; }
            public int HanhTrinhGiaVeId { get; set; }
            public string TenFromDiemDon { get; set; }
            public string TenToDiemDon { get; set; }
            public int SoLuongVe { get; set; }
            public string DoanhThu { get; set; }


        }

        public class HanhTrinhDiemDonModel : BaseNopEntityModel
        {
           
            public int HanhTrinhId { get; set; }

            [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.DiemDon.DiemDonId")]
            public int DiemDonId { get; set; }
            public string DiemDonText { get; set; }            
            [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.DiemDon.ThuTu")]
            public int ThuTu { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.DiemDon.KhoangCach")]
            public int KhoangCach { get; set; }
           
        }
       

    }
}