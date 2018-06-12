using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.Chonves
{
    public class HopDong : BaseEntity
    {
        public String MaHopDong { get; set; }
        public String TenHopDong { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public DateTime? NgayKichHoat { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public int LoaiHopDongID { get; set; }
        public int TrangThaiID { get; set; }
        public int NguoiTaoID { get; set; }
        public int NguoiDuyetID { get; set; }
        public int KhachHangID { get; set; }
        public int NhaXeID { get; set; }
        public String ThongTin { get; set; }
        public ENTrangThaiHopDong TrangThai
        {
            get
            {
                return (ENTrangThaiHopDong)TrangThaiID;
            }
            set
            {
                TrangThaiID = (int)value;
            }
        }
       
        public  ENLoaiHopDong LoaiHopDong
        {
            get
            {
                return (ENLoaiHopDong)LoaiHopDongID;
            }
            set
            {
                LoaiHopDongID = (int)value;
            }
        }
        public virtual NhaXe ThongTinNhaXe { get; set; }
        public virtual Customer NguoiTao { get; set; }
        public virtual Customer NguoiDuyet { get; set; }
        public virtual Customer KhachHang { get; set; }

    }
}
