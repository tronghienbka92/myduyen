using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.VeXeKhach;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class QuanlyPhoiVeModel : BaseNopEntityModel
    {
        public QuanlyPhoiVeModel()
        {
            ListHanhTrinh = new List<SelectListItem>();
            ListNguonVeXe = new List<SelectListItem>();
        }
        [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.HanhTrinhId")]
        public int HanhTrinhId { get; set; }
        public IList<SelectListItem> ListHanhTrinh { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.LichTrinhId")]
        public int NguonVeXeId { get; set; }
        public IList<SelectListItem> ListNguonVeXe { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.QuanLiPhoiVe.NgayDi")]
        [UIHint("Date")]
        public DateTime NgayDi { get; set; }
        public bool YeuCauHuy { get; set; }
        [UIHint("Date")]
        [NopResourceDisplayName("Ngày giao dịch")]
        public DateTime NgayGiaoDichVe { get; set; }
        public KhachHangDatMuaVeModel KhachHangInfoBase { get; set; }
        public class KhachHangDatMuaVeModel : BaseNopEntityModel
        {
            public KhachHangDatMuaVeModel()
            {
                NguonVeChonNhanh = new List<QuanlyPhoiVeModel.NguonVeXeItem>();
                isKhachVangLai = false;

            }
            public bool IsSuaTungVe { get; set; }
            public int PhoiVeId { get; set; }
            public int ChuyenDiId { get; set; }
            public int NguonVeXeIdDangChon { get; set; }
            public int ChangId { get; set; }
            public int SoLuong { get; set; }
            public bool isKhachVangLai { get; set; }

            public string NgayDiDangChon { get; set; }
            public string SearchKhachHang { get; set; }

            [NopResourceDisplayName("ChonVe.NhaXe.QuanLiPhoiVe.KhachHang.HoTen")]
            [Required]
            public string TenKhachHang { get; set; }
            [Required]
            [NopResourceDisplayName("ChonVe.NhaXe.QuanLiPhoiVe.KhachHang.DienThoai")]
            public string SoDienThoai { get; set; }
            [NopResourceDisplayName("ChonVe.NhaXe.QuanLyPhoiVe.KhachHang.DaThanhToan")]
            public bool DaThanhToan { get; set; }

            public int PhoiVeId_ChuyenVe { get; set; }

            [NopResourceDisplayName("ChonVe.NhaXe.HanhTrinh.LichTrinhId")]
            public int NguonVeXeId_ChuyenVe { get; set; }
            public IList<SelectListItem> ListNguonVeXe_ChuyenVe { get; set; }

            [NopResourceDisplayName("ChonVe.NhaXe.QuanLiPhoiVe.NgayDi")]
            [UIHint("DateChuyenVe")]
            public DateTime NgayDi_ChuyenVe { get; set; }
            /// <summary>
            /// Lay tat ca cac nguon ve con va cha de nghiep vu ban ve co the chon
            /// </summary>
            public IList<SelectListItem> _nguonves { get; set; }
            public IList<SelectListItem> _changs { get; set; }
            public List<QuanlyPhoiVeModel.NguonVeXeItem> NguonVeChonNhanh { get; set; }
            public string GhiChu { get; set; }
            public string ViTriLen { get; set; }
            public string ViTriXuong { get; set; }
            public bool IsForKid { get; set; }
            public string MaVe { get; set; }
            public int QuayBanVeId { get; set; }
            public IList<SelectListItem> quaybanves { get; set; }

            public int MauVeKyHieuId { get; set; }
            public IList<SelectListItem> maukyhieus { get; set; }
        }
        public class SoDoGheModel : BaseNopEntityModel
        {
            public string QuyTac { get; set; }
        }
        public class NguonVeXeItem : BaseNopEntityModel
        {
            public string MoTa { get; set; }
            public DateTime ThoiGianDi { get; set; }
            public DateTime ThoiGianDen { get; set; }
            public bool NguonVeChon { get; set; }
        }
        public class PhoiVeNoteMoel : BaseNopEntityModel
        {
            public int PhoiVeId { get; set; }
            public DateTime NgayTao { get; set; }
           
            public string Note { get; set; }
        }
    }
}