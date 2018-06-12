using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class BaoCaoNhaXeModel : BaseNopEntityModel
    {
        public enum EN_LOAI_BAO_CAO
        {
            TONG_HOP_HANH_KHACH=1,
            LUOT_XE_CHAY=2,
            LUOT_XE_CHAY_NAM = 3,
            TONG_HOP_DOANH_THU_THANG = 4,
            TONG_HOP_HK_CL_DT = 5,
            CHI_TIET_CHI_PHI = 6,
            TONG_HOP_CHI_PHI = 7,
            TONG_HOP_CHUNG = 8,
            BAO_CAO_DT_CL_NHAN_VIEN = 9,
            SO_QUY_TIEN_MAT=10,
            DOANH_THU_CHANG=11,
            LUOT_NHAN_VIEN = 12,
            HANG_HOA_VAN_PHONG = 13,
            HANG_HOA_TONG_HOP = 14,
            BAO_CAO_PHIEU_VAN_CHUYEN = 15,
            PHIEU_VAN_CHUYEN_NGAY = 16,
            PHIEU_VAN_CHUYEN_THANG = 17,
            CHI_TIEU_THANG = 18,
            TO_VAN_CHUYEN_THANG = 19

        }
        public BaoCaoNhaXeModel()
        {
            ListLoai1 = new List<SelectListItem>();
            ListLoai2 = new List<SelectListItem>();
            ListQuy = new List<SelectListItem>();
            ListMonth = new List<SelectListItem>();
            ListYear = new List<SelectListItem>();
            VanPhongs = new List<SelectListItem>();
            Xe = new List<SelectListItem>();
            isShowSTT = true;
            addSumRight = true;
            idxColForSum = 1;
            addSumBottom = true;
            LoaiBaoCao = EN_LOAI_BAO_CAO.LUOT_XE_CHAY;
        }
        public EN_LOAI_BAO_CAO LoaiBaoCao {
            get {
                return (EN_LOAI_BAO_CAO)LoaiBaoCaoId;
            }
            set
            {
                LoaiBaoCaoId = (int)value;
            }
        }
        public int LoaiBaoCaoId { get; set; }
        public string FileNameExport
        {
            get
            {
                string _filenaname = LoaiBaoCao.ToString();
                _filenaname = _filenaname.ToLower();
                return _filenaname;
            }
        }
        [UIHint("DateNullable")]
        public DateTime NgayGuiHang { get; set; }
        public int Loai1Id { get; set; }
        public int Loai2Id { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.BaoCaoNhaXe.Quy")]
        public int QuyId { get; set; }
        [NopResourceDisplayName("ChonVe.NhaXe.BaoCaoNhaXe.Thang")]
        public int ThangId { get; set; }
        public int HanhTrinhId { get; set; }
        public IList<SelectListItem> HanhTrinhs { get; set; }
        
        [NopResourceDisplayName("ChonVe.NhaXe.BaoCaoNhaXe.Nam")]
        public int NamId { get; set; }
        [UIHint("DateNullable")]
        [NopResourceDisplayName("ChonVe.NhaXe.BaoCaoNhaXe.TuNgay")]
        public DateTime TuNgay { get; set; }
        [UIHint("DateNullable")]
        [NopResourceDisplayName("ChonVe.NhaXe.BaoCaoNhaXe.DenNgay")]
        public DateTime DenNgay { get; set; }

        [UIHint("DateTimeNullable")]
        [NopResourceDisplayName("ChonVe.NhaXe.BaoCaoNhaXe.TuNgay")]
        public DateTime TuNgayH { get; set; }
        [UIHint("DateTimeNullable")]
        [NopResourceDisplayName("ChonVe.NhaXe.BaoCaoNhaXe.DenNgay")]
        public DateTime DenNgayH { get; set; }

        [NopResourceDisplayName("ChonVe.NhaXe.BaoCaoNhaXe.ChonVe")]
        public bool isChonVe { get; set; }
        public string BienSoXe { get; set; }
        public string SoLenh { get; set; }

        public string NgayBan { get; set; }
        public int XeId { get; set; }
        public int VanPhongId { get; set; }
       
        public IList<SelectListItem> VanPhongs { get; set; }
        public int LichTrinhId { get; set; }

        public IList<SelectListItem> LichTrinhs { get; set; }
        public int ChangId { get; set; }
        public int LoaiXeId { get; set; }
        public IList<SelectListItem> Changs { get; set; }
        public IList<SelectListItem> LoaiXes { get; set; }
        public IList<SelectListItem> Xe { get; set; }
        public IList<SelectListItem> ListLoai1 { get; set; }
        public IList<SelectListItem> ListLoai2 { get; set; }
        public IList<SelectListItem> ListLoai3 { get; set; }
        public IList<SelectListItem> ListQuy { get; set; }
        public IList<SelectListItem> ListMonth { get; set; }
        public IList<SelectListItem> ListYear { get; set; }
        //add by lent 11/12/2016
        //them cac thuoc tinh de hien bao cao + export bao cao

        public string KeySearch { get; set; }
        public int isChi { get; set; }
        public int TuyenId { get; set; }
        public IList<SelectListItem> ListTuyens { get; set; }
        public String[] Title { get; set; }
        public List<String[]> TitleColSpan { get; set; }
        public string topPage { get; set; }
        public String[] headers { get; set; }
        public DataTable dataReport { get; set; }        
        public bool isShowSTT { get; set; }
        public bool addSumRight { get; set; }
        public int idxColForSum { get; set; }
        public bool addSumBottom { get; set; }

        public class TuyenXeVanChuyen
        {
            public TuyenHanhTrinh tuyen { get; set; }
            public List<HanhTrinh> hanhtrinhs { get; set; }
            public List<XeVanChuyen> xevanchuyens { get; set; }
            public List<HistoryXeXuatBen> xuatbens { get; set; }
            public decimal TongGiaTri { get; set; }
        }
        public class BaoCaoDoanhThuModel : ThongKeItem
        {
            public string ThoiGian { get; set; }
            public decimal TongDoanhThu { get; set; }
            public decimal DoanhThuChonVe { get; set; }
            public decimal DoanhThuNhaXe { get; set; }

        }
        public class BaoCaoDoanhThuNhanVienModel : ThongKeItem
        {
            public int NhanVienId { get; set; }
            public int NguonVeId { get; set; }
            public string TenNhanVien { get; set; }
            public string TrangThaiPhoiVeText { get; set; }
            public decimal TongDoanhThu { get; set; }
            public decimal DoanhThuChuaThanhToan { get; set; }
            public decimal DoanhThuChonVe { get; set; }
            public decimal DoanhThuNhaXe { get; set; }
            public string NgayBan { get; set; }
           
        }
        public class BaoCaoDoanhThuXeTungNgayModel : DoanhThuTheoXeItem
        {
           
            public string BienSo { get; set; }
            public string TrangThaiPhoiVeText { get; set; }
            public decimal TongDoanhThu { get; set; }
            public decimal DoanhThuXe { get; set; }
            public string NgayBan { get; set; }
            public string NgayDen { get; set; }
           
        }
        public class BaoCaoDetailDoanhThuKiGuiModel : ThongKeItem
        {
            public string NotPay { get; set; }
            public int NhanVienId { get; set; }
            public string NgayBan { get; set; }
        }
        public class KhachHangMuaVeModel
        {
            public int CustomerId { get; set; }
            public int NguonVeXeId { get; set; }
            public string TenKhachHang { get; set; }
            public string SoDienThoai { get; set; }
            public string ThongTinChuyenDi { get; set; }
            public int TrangThaiPhoiVeId { get; set; }
            public string TrangThaiPhoiVeText { get; set; }
            public string KyHieuGhe { get; set; }
            public bool isChonVe { get; set; }
            public decimal GiaVe { get; set; }
            public DateTime NgayDi { get; set; }
            public int SoLuot { get; set; }
        }

    }
}