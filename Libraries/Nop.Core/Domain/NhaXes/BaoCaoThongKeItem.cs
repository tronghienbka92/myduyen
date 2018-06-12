using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class ThongKeItem 
    {
        public string Nhan { get; set; }
        public string NhanSapXep { get; set; }
        public int TrangThaiPhoiVeId { get; set; }
        public ENTrangThaiPhoiVe TrangThai
        {
            get
            {
                return (ENTrangThaiPhoiVe)TrangThaiPhoiVeId;
            }
            set
            {
                TrangThaiPhoiVeId = (int)value;
            }
        }
        public Decimal GiaTri { get; set; }
        public int SoLuong { get; set; }
        public Decimal GiaTri1 { get; set; }
        public Decimal GiaTri2 { get; set; }
        public int ItemId { get; set; }
        public DateTime ItemDataDate { get; set; }
        public int ItemDataYear { get; set; }
        public int ItemDataMonth { get; set; }
        public int ItemDataDay { get; set; }
    }
    public class DoanhThuChang
    {
        public string TenChang { get; set; }
        public string TenLoaiXe { get; set; }
        public string TenHanhTrinh { get; set; }
        public int ChangId { get; set; }
        public int SoKhach { get; set; }
        public Decimal DoanhThu { get; set; }
    }
    public class DoanhThuDiem
    {
        public string TenDiem { get; set; }

        public int SoKhachLen { get; set; }
        public int SoKhachXuong { get; set; }
    }
    public class DoanhThuItem
    {
        public int Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public Decimal DoanhThu { get; set; }
       
    }
    public class DoanhThuTheoXeItem
    {
        public int NguonVeXeId { get; set; }
        public string ThongTinChuyenDi { get; set; }
        public int XeId { get; set; }
        public string Nhan { get; set; }
        public string NhanSapXep { get; set; }
        public Decimal GiaTri { get; set; }
        public int SoLuong { get; set; }
        public string KyHieuGhe { get; set; }
        public int TrangThaiPhoiVeId { get; set; }
        public ENTrangThaiPhoiVe TrangThai
        {
            get
            {
                return (ENTrangThaiPhoiVe)TrangThaiPhoiVeId;
            }
            set
            {
                TrangThaiPhoiVeId = (int)value;
            }
        }
        public Decimal GiaTri1 { get; set; }
        public Decimal GiaTri2 { get; set; }
        public Nop.Core.Domain.Chonves.NguonVeXe NguonVeXe { get; set; }
        public int ItemId { get; set; }
        public DateTime ItemDataDate { get; set; }
        public int ItemDataYear { get; set; }
        public int ItemDataMonth { get; set; }
        public int ItemDataDay { get; set; }
        public decimal GiaVe { get; set; }
    }
    public class KhachHangMuaVeItem
    {
        public int CustomerId { get; set; }
        public Customers.Customer customer { get; set; }
        public Nop.Core.Domain.Chonves.NguonVeXe nguonve { get; set; }
        public int NguonVeXeId { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string ThongTinChuyenDi { get; set; }
        public int TrangThaiPhoiVeId { get; set; }
        public ENTrangThaiPhoiVe TrangThai
        {
            get
            {
                return (ENTrangThaiPhoiVe)TrangThaiPhoiVeId;
            }
            set
            {
                TrangThaiPhoiVeId = (int)value;
            }
        }
        public string KyHieuGhe { get; set; }
        public bool isChonVe { get; set; }
        public decimal GiaVe { get; set; }
        
        public DateTime NgayDi { get; set; }
        public int SoLuot { get; set; }
    }
    public class VeQuayVipEntity
    {
        public string CamPhaNgay { get; set; }
        public int Quyen { get; set; }
        public string BienSoXe { get; set; }
        public string TenLaiXe { get; set; }
        public string TenNTV { get; set; }
        public decimal SoSeriNum { get; set; }
        public decimal MenhGia { get; set; }
        public string Luot { get; set; }

        public string TenNVThanhToan { get; set; }
    }
    public class ThongKeVeTiepVienDaDung
    {

        public int SoQuyen { get; set; }
        public int SeriFrom { get; set; }
        public int SeriTo { get; set; }
        public decimal MenhGia { get; set; }
        public DateTime NgayNhan { get; set; }
        public string TenNguoiNhan { get; set; }
        public int SoLuongCon { get; set; }
        public int SeriConFrom { get; set; }
        public int SeriConTo { get; set; }
        public bool isCon { get; set; }
        public List<NgayDungItem> SoLuongDungs { get; set; }

    }
    public class NgayDungItem
    {
        public int Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public int SoVe { get; set; }
        public int SoQuyen { get; set; }
    }
    public class ThongKeLuotXuatBenItem
    {
        public int[] NhanVienIds { get; set; }
        public int SoLuot { get; set; }
        public decimal DoanhThu { get; set; }
        public int Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }


    }
    
}
