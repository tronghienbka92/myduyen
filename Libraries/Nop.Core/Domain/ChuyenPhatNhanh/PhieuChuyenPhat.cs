using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.ChuyenPhatNhanh
{
    public class PhieuChuyenPhat : BaseEntity
    {
        public PhieuChuyenPhat()
        {
            NgayTao = DateTime.Now;
            NgayUpdate = DateTime.Now;
        }
        public string MaPhieu { get; set; }
        public int NhaXeId { get; set; }
        public int NguoiGuiId { get; set; }
        public virtual KhachHang NguoiGui { get; set; }
        public int NguoiNhanId { get; set; }
        public virtual KhachHang NguoiNhan { get; set; }
        public int VanPhongGuiId { get; set; }
        public virtual VanPhong VanPhongGui { get; set; }
        public int VanPhongNhanId { get; set; }
        public virtual VanPhong VanPhongNhan { get; set; }
        public string TenHang { get; set; }
        public decimal CuocPhi { get; set; }
        public decimal CuocTanNoi { get; set; }
        public decimal CuocNhanTanNoi { get; set; }
        public decimal CuocCapToc { get; set; }
        public decimal CuocGiaTri { get; set; }
        public decimal TongCuocDaThanhToan { get; set; }
        public int? PhieuVanChuyenId { get; set; }
        public virtual PhieuVanChuyen phieuvanchuyen { get; set; }
        public decimal CuocVuotTuyen { get; set; }      
        public int NhanVienGiaoDichId { get; set; }
        public virtual NhanVien NVGiaoDich { get; set; }    
       
        public int? NhanVienNhanHangId { get; set; }
        public virtual NhanVien NVNhanHang { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayUpdate { get; set; }
        public DateTime NgayNhanHang { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public DateTime? NgayDenVanPhongNhan { get; set; } 
        public int TrangThaiId { get; set; }
      
       
        public string GhiChu { get; set; }

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

        public int? ToVanChuyenNhanId { get; set; }
        public virtual ToVanChuyen tovanchuyennhan { get; set; }
        public int? ToVanChuyenTraId { get; set; }
        public virtual ToVanChuyen tovanchuyentra { get; set; }
        public int? NguoiVanChuyenNhanId { get; set; }
        public virtual NguoiVanChuyen nguoivanchuyennhan { get; set; }
        public int? NguoiVanChuyenTraId { get; set; }
        public virtual NguoiVanChuyen nguoivanchuyentra { get; set; }
        public int DaSMS { get; set; }
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
        public decimal TongTienCuoc { 
            get{
                return CuocPhi+CuocTanNoi+CuocCapToc+CuocGiaTri+CuocVuotTuyen+CuocNhanTanNoi;
            } 
        }
        private ICollection<PhieuChuyenPhatLog> _nhatkys;
        public virtual ICollection<PhieuChuyenPhatLog> nhatkys
        {
            get { return _nhatkys ?? (_nhatkys = new List<PhieuChuyenPhatLog>()); }
            protected set { _nhatkys = value; }
        }
        private ICollection<PhieuChuyenPhatVanChuyen> _nhatkyvanchuyens;
        public virtual ICollection<PhieuChuyenPhatVanChuyen> nhatkyvanchuyens
        {
            get { return _nhatkyvanchuyens ?? (_nhatkyvanchuyens = new List<PhieuChuyenPhatVanChuyen>()); }
            protected set { _nhatkyvanchuyens = value; }
        }
        public int NhomPhieuId { get; set; }
        public ENNhomPhieuChuyenPhat NhomPhieu
        {
            get
            {
                return (ENNhomPhieuChuyenPhat)this.NhomPhieuId;
            }
            set
            {
                this.NhomPhieuId = (int)value;
            }
        }
    }
}
