using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.NhaXes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    [Validator(typeof(LichTrinhValidator))]
    public class LichTrinhModel : BaseNopEntityModel
    {
        public LichTrinhModel()
        {
            //AddLichTrinhGiaVeModel = new LichTrinhGiaVeModel();
            LoaiXes = new List<SelectListItem>();
            AvailableDiemDons = new List<SelectListItem>();
            TimeOpenOnline = 15;
            TimeCloseOnline = 3;
        }

        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.MaLichTrinh")]
        public string MaLichTrinh { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.HanhTrinhId")]
        public int HanhTrinhId { get; set; }
        public string HanhTrinhText { get; set; }
        public bool KhoaLichTrinh { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.LoaiXeId")]
        public int LoaiXeId { get; set; }
        public int SoDiemDon { get; set; }
        public string NguonVeResult { get; set; }       
        public string TenLoaiXe { get; set; }
        public List<SelectListItem> LoaiXes { get; set; }
        
        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.ThoiGianDi")]        
        public DateTime ThoiGianDi { get; set; }
        
        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.ThoiGianDen")]
        public DateTime ThoiGianDen { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.SoGioChay")]
        [UIHint("DecimalThapPhan")]
        public Decimal SoGioChay { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.TimeOpenOnline")]        
        public int TimeOpenOnline { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.TimeCloseOnline")]
        public int TimeCloseOnline { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.GiaVeToanTuyen")]        
        public Decimal GiaVeToanTuyen { get; set; }
        public string GiaVeToanTuyenText { get; set; }
       
        public  NguonVeModelToArray[,] NguonVes { get; set; }

        //thong ti diem don tu hanhtrinhid
        public IList<SelectListItem> AvailableDiemDons { get; set; }
       // public LichTrinhGiaVeModel AddLichTrinhGiaVeModel { get; set; }
        
        public class LichTrinhGiaVeModel : BaseNopEntityModel
        {
           
            public int LichTrinhID { get; set; }


            [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.GiaVe.DiemDon1_Id")]
            public int DiemDon1_Id { get; set; }
            public string DiemDon1Text { get; set; }      
            [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.GiaVe.DiemDon2_Id")]
            public int DiemDon2_Id { get; set; }
            public string DiemDon2Text { get; set; }      
            [NopResourceDisplayName("ChonVe.NhaXe.LichTrinh.GiaVe.GiaVe")]
            public Decimal GiaVe { get; set; }
            public string GiaVeText { get; set; }
           
           
        }
        public class NguonVeModelToArray : BaseNopEntityModel
        {

            public int FromDiemDonId { get; set; }
            public int ToDiemDonId { get; set; }
            public decimal GiaNguonVe { get; set; }
            public int NguonVeXeId { get; set; }
            public string TenFromDiemDon { get; set; }
            public string TenToDiemDon { get; set; }
           

        }
        

    }
}