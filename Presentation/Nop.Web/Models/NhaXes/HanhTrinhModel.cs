using FluentValidation.Attributes;
using Nop.Core.Domain.NhaXes;
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
    [Validator(typeof(HanhTrinhValidator))]
    public class HanhTrinhModel : BaseNopEntityModel
    {

        public HanhTrinhModel()
        {
            DiemDons = new List<HanhTrinhDiemDonModel>();
            AddHanhTrinhDiemDonModel = new HanhTrinhDiemDonModel();
            AvailableDiemDons = new List<SelectListItem>();
            AllVanPhongs = new List<VanPhongModel>();

        }

        [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.MaHanhTrinh")]
        public string MaHanhTrinh { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.MoTa")]
        public string MoTa { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.TongKhoangCach")]
        public int TongKhoangCach { get; set; }
        public string GiaVeResult { get; set; }
        public int SoDiemDon { get; set; }
        public int SoLoaiXe { get; set; }
        public HanhTrinhDiemDonModel AddHanhTrinhDiemDonModel { get; set; }
        public List<DiemDonModel> AllDiemDonStartEnds { get; set; }
        public List<LoaiXe> Loaixes { get; set; }
        public List<DiemDonModel> AllDiemDonMids { get; set; }
        public HanhTrinhGiaVeModelToArray[,] HanhTrinhGiaVes { get; set; }
        public List<HanhTrinhDiemDonModel> DiemDons { get; set; }
        public IList<SelectListItem> AvailableDiemDons { get; set; }
        public List<HanhTrinhDiemDonModel> DiemChots { get; set; }
        public IList<SelectListItem> AvailableDiemChots { get; set; }
        public int TuyenId { get; set; }
        public IList<SelectListItem> ListTuyens { get; set; }
        public class HanhTrinhGiaVeModelToArray : BaseNopEntityModel
        {
            public HanhTrinhGiaVeModelToArray()
            {

                hanhtrinhgiaves = new List<HanhTrinhGiaVeModel>();
            }
            public int FromDiemDonId { get; set; }
            public int ToDiemDonId { get; set; }


            public int NguonVeXeId { get; set; }

            public string TenFromDiemDon { get; set; }
            public string TenToDiemDon { get; set; }

            public List<HanhTrinhGiaVeModel> hanhtrinhgiaves { get; set; }

        }
        public class HanhTrinhGiaVeModel
        {
            public int LoaiXeId { get; set; }
            public string TenLoaiXe { get; set; }
            public string MaLoaiXe { get; set; }
            public int HanhTrinhGiaVeId { get; set; }
            public decimal GiaNguonVe { get; set; }
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
        public List<VanPhongModel> AllVanPhongs { get; set; }
        public int[] SelectedVanPhongIds { get; set; }
        public int WIDTH_OF_LINE { get; set; }

    }
}