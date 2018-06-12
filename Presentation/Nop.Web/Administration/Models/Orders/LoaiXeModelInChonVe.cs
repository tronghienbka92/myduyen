using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.NhaXes;

namespace Nop.Admin.Models.Orders
{
  
    public class LoaiXeModelInChonVe : BaseNopEntityModel
    {
        public LoaiXeModelInChonVe()
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
            /// <summary>
            /// 0: ap dung cho quan ly phoi ve, 1: ap dung cho chuyen ve
            /// </summary>
            public int PhanLoai { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.LoaiXe.SoDoGheXe.TenSoDo")]
            public string TenSoDo { get; set; }
            public string UrlImage { get; set; }
            public int SoLuongGhe { get; set; }
            public int KieuXeId { get; set; }
            public int SoCot { get; set; }
            public int SoHang { get; set; }
            /// <summary>
            /// Thong tin vi tri tren ma tran so do ghe co gia tri la 0, 1
            /// </summary>
            public int[,] MaTran { get; set; }
            //so tang 
            public int SoTang { get; set; }
            /// <summary>
            /// Thong tin ma tran phoi ve tang 1
            /// </summary>
            public PhoiVeAdvanceModel[,] PhoiVes1 { get; set; }
            /// <summary>
            /// thong tin phoi ve tang 2
            /// </summary>
            public PhoiVeAdvanceModel[,] PhoiVes2 { get; set; }
        }
        
        public class PhoiVeAdvanceModel 
        {
            public string KyHieu { get; set; }
            public PhoiVe Info { get; set; }
            public string TenKhachHang { get; set; }
            public string SoDienThoai { get; set; }
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