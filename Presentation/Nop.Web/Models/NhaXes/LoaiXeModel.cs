using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.NhaXes;
using Nop.Web.Models.VeXeKhach;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.NhaXes;

namespace Nop.Web.Models.NhaXes
{
    [Validator(typeof(LoaiXeValidator))]
    public class LoaiXeModel : BaseNopEntityModel
    {
        public LoaiXeModel()
        {
            KieuXes = new List<SelectListItem>();
            SoDoGheXes = new List<SoDoGheXeModel>();
            GheItems = new List<GheItemModel>();
            SoDoGheXeQuyTacs = new List<SoDoGheXeQuyTacModel>();
            CurrentSoDoGheXe = new SoDoGheXeModel();
        }
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.NhaXeId")]
        public int NhaXeId { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.TenLoaiXe")]
        public string TenLoaiXe { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.KieuXeID")]
        public int KieuXeID { get; set; }
         [NopResourceDisplayName("Mã loại xe")]
        public string MaLoaiXe { get; set; }
        public string KieuXeText { get; set; }
        public IList<SelectListItem> KieuXes { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.SoDoGheXeID")]
        public int SoDoGheXeID { get; set; }
        public string SoDoGheXeText { get; set; }
        public SoDoGheXeModel CurrentSoDoGheXe { get; set; }
        public IList<SoDoGheXeModel> SoDoGheXes { get; set; }
        //cac tien ich
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.IsWC")]
        public bool IsWC { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.IsTV")]
        public bool IsTV { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.IsWifi")]
        public bool IsWifi { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.IsDieuHoa")]
        public bool IsDieuHoa { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.IsNuocUong")]
        public bool IsNuocUong { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.IsKhanLanh")]
        public bool IsKhanLanh { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.IsThucAn")]
        public bool IsThucAn { get; set; }

        public IList<GheItemModel> GheItems { get; set; }
        public IList<SoDoGheXeQuyTacModel> SoDoGheXeQuyTacs { get; set; }
        /// <summary>
        /// luu thong tin mang gia tri theo quy tac
        /// </summary>
        public string SoDoGheXeQuyTacResult { get; set; }


        public class GheItemModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.GheItem.LoaiXeId")]
            public int LoaiXeId { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.GheItem.KyHieuGhe")]
            public string KyHieuGhe { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.GheItem.Tang")]
            public int Tang { get; set; }
            public int SoDoGheXeViTriId { get; set; }
            
        }
        public class SoDoGheXeModel:BaseNopEntityModel
        {
           
            public ENPhanLoaiPhoiVe PhanLoai { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.SoDoGheXe.TenSoDo")]
            public string TenSoDo { get; set; }
            public string UrlImage { get; set; }
            public bool CanYeuCauHuy { get; set; }
            public int SoLuongGhe { get; set; }
            public int SoDiemDon { get; set; }
            public int KieuXeId { get; set; }
            public int SoCot { get; set; }
            public int SoHang { get; set; }
            public int[] MangChangId { get; set; }
            /// <summary>
            /// Thong tin vi tri tren ma tran so do ghe co gia tri la 0, 1
            /// </summary>
            public int[,] MaTran { get; set; }
            //so tang 
            public int SoTang { get; set; }
            public TongKetPhoiToArrayModel[,] TongKet { get; set; }
            /// <summary>
            /// Thong tin ma tran phoi ve tang 1
            /// </summary>
            public PhoiVeAdvanceModel[,] PhoiVes1 { get; set; }
            /// <summary>
            /// thong tin phoi ve tang 2
            /// </summary>
            public PhoiVeAdvanceModel[,] PhoiVes2 { get; set; }
            public XeXuatBenItemModel chuyendihientai { get; set; }
        }
        public class TongKetPhoiToArrayModel
        {
            public int DiemDonId { get; set; }
            public string TenDiemDon { get; set; }
            public string TongTienText { get; set; }
            public decimal TongTienValue { get; set; }
            public int SoKhachXuong { get; set; }
            public int TongKhach { get; set; }
        }
        public class PhoiVeAdvanceModel 
        {
            public string KyHieu { get; set; }
            public PhoiVe Info { get; set; }
            public bool IsCurrentCustomer { get; set; }           
            public string TenKhachHang { get; set; }
            public string TenNguoiDatVe { get; set; }
            public string SoDienThoai { get; set; }
            public string GiaVe { get; set; }
            public string MaMau { get; set; }
            public string TenChang { get; set; }
            public string LoaiKhach { get; set; }    
            public string MaSoVe { get; set; }
            public int SoLuong { get; set; }

        }
          
        public class SoDoGheXeQuyTacModel:BaseNopEntityModel
        {            
            public string Val { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public int Tang { get; set; }
            public int LoaiXeId { get; set; }
        }
    }
}