using FluentValidation.Attributes;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.ChuyenPhatNhanh;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Web.Models.ChuyenPhatNhanh
{
    [Validator(typeof(PhieuChuyenPhatValidator))]
    public class PhieuChuyenPhatModel : BaseNopEntityModel
    {
        public PhieuChuyenPhatModel()
        {
            NguoiGui = new KhachHang();
            NguoiNhan = new KhachHang();
            nhatkyvanchuyens = new List<PhieuChuyenPhatVanChuyenModel>();
        }
        public string MaPhieu { get; set; }
        public int NhaXeId { get; set; }
        public int NguoiGuiId { get; set; }
        public int SoNgayTon { get; set; }
       
       
        public string NguoiGuiText { get; set; }
        public KhachHang NguoiGui { get; set; }
        public int NguoiNhanId { get; set; }
        public string NguoiNhanText { get; set; }
        public KhachHang NguoiNhan { get; set; }
        public int VanPhongGuiId { get; set; }
        public string VanPhongGuiText { get; set; }
        public int VanPhongNhanId { get; set; }
        public string VanPhongNhanText { get; set; }
        public string KhuVucText { get; set; }
        public string TenHang { get; set; }
        public decimal CuocPhi { get; set; }
        public decimal CuocTanNoi { get; set; }
        public decimal CuocNhanTanNoi { get; set; }
        public decimal CuocCapToc { get; set; }
        public decimal CuocGiaTri { get; set; }
        public decimal TongCuocDaThanhToan { get; set; }
        public int PhieuVanChuyenId { get; set; }
        public string SoLenh { get; set; }
        public decimal CuocVuotTuyen { get; set; }
        public int NhanVienGiaoDichId { get; set; }
        public string TenNhanvienGiaoDich { get; set; }
        public string NVGiaoDichText { get; set; }
        public int NhanVienNhanHangId { get; set; }
        public string TenNhanvienNhanHang { get; set; }
        public string NVNhanHangText { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayUpdate { get; set; }

        [UIHint("DateNgayTaoPhieuHang")]
        public DateTime NgayNhanHang { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public DateTime? NgayDenVanPhongNhan { get; set; }
        public int TrangThaiId { get; set; }


        public string GhiChu { get; set; }
        public string TrangThaiText { get; set; }

        public ENTrangThaiChuyenPhat TrangThai
        {
            get
            {
                return (ENTrangThaiChuyenPhat)this.TrangThaiId;
            }
            set
            {
                this.TrangThaiId = (int)value;
            }
        }
        public int ToVanChuyenNhanId { get; set; }
        public string TenToVanChuyenNhan { get; set; }
        public int NguoiVanChuyenNhanId { get; set; }
        public string TenNguoiVanChuyenNhan { get; set; }
        public int ToVanChuyenTraId { get; set; }
        public string TenToVanChuyenTra { get; set; }
        public int NguoiVanChuyenTraId { get; set; }
        public string TenNguoiVanChuyenTra { get; set; }
        public int DaSMS { get; set; }
        public decimal TongTienCuoc { get; set; }

        public List<KhuVucVanPhongModel> khuvucvanphongs { get; set; }
        public int LoaiPhieuId { get; set; }
        public ENLoaiPhieuChuyenPhat LoaiPhieu
        {
            get
            {
                return (ENLoaiPhieuChuyenPhat)this.LoaiPhieuId;
            }
            set
            {
                this.LoaiPhieuId = (int)value;
            }
        }
        public string ThongTinXe { get; set; }
       
        public IList<SelectListItem> loaiphieus { get; set; }
        public List<PhieuChuyenPhatVanChuyenModel> nhatkyvanchuyens { get; set; }
        public List<PhieuChuyenPhatLog> nhatkys { get; set; }
       
        public bool isVuotTuyen { get; set; }
        public class PhieuChuyenPhatVanChuyenModel : BaseNopEntityModel
        {
            public int PhieuChuyenPhatId { get; set; }
            public int PhieuVanChuyenId { get; set; }
            public string PhieuVanChuyenText { get; set; }
            public int ChuyenDiId { get; set; }
            public string BienSo { get; set; }
            public string LaiXe { get; set; }
            public string PhuXe { get; set; }
            public string ThongTinChuyen { get; set; }
            public DateTime NgayDi { get; set; }
            public int HanhTrinhId { get; set; }
            public string hanhtrinhText { get; set; }
            public int TuyenId { get; set; }
            public string tuyenText { get; set; }
            public int VanPhongGuiId { get; set; }
            public string vanphongguiText { get; set; }
            public int VanPhongNhanId { get; set; }
            public string vanphongnhanText { get; set; }
            public int KhuVucId { get; set; }
            public string khuvucText { get; set; }
            public decimal TongCuoc { get; set; }
            public decimal CuocVuotTuyen { get; set; }
        }
    }
}
