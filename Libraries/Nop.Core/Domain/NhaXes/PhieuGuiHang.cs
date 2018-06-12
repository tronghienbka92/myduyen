using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class PhieuGuiHang : BaseEntity
    {
        public string MaPhieu { get; set; }
        public int NhaXeId { get; set; }
        public int NguoiGuiId { get; set; }
        public virtual NhaXeCustomer NguoiGui { get; set; }
        public int NguoiNhanId { get; set; }
        public virtual NhaXeCustomer NguoiNhan { get; set; }
        public int VanPhongGuiId { get; set; }
        public virtual VanPhong VanPhongGui { get; set; }
        public int VanPhongNhanId { get; set; }
        public virtual VanPhong VanPhongNhan { get; set; }
        public int NguoiKiemTraHangId { get; set; }
        public virtual NhanVien NguoiKiemTraHang { get; set; }
     
        public DateTime? NgayThanhToan { get; set; }
        public int? NhanVienThuTienId { get; set; }
        public virtual NhanVien NhanVienThuTien { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayUpdate { get; set; }

        public int NguoiTaoId { get; set; }


        public virtual NhanVien nguoitao { get; set; }
        public int? NhanVienNhanHangId { get; set; }
        public int TinhTrangVanChuyenId { get; set; }
      
        public int? XeXuatBenId { get; set; }
        public virtual HistoryXeXuatBen XeXuatBen { get; set; }
        public string GhiChu { get; set; }
        public bool DaThuCuoc { get; set; }
        public ENTinhTrangVanChuyen TinhTrangVanChuyen
        {
            get
            {
                return (ENTinhTrangVanChuyen)this.TinhTrangVanChuyenId;
            }
            set
            {
                this.TinhTrangVanChuyenId = (int)value;
            }
        }
        private ICollection<HangHoa> _hanghoas;
        public virtual ICollection<HangHoa> HangHoas
        {
            get { return _hanghoas ?? (_hanghoas = new List<HangHoa>()); }
            protected set { _hanghoas = value; }
        }
        public decimal TongTienCuoc { get; set; }
        public decimal TongKhoiLuong { get; set; }
        public decimal TongSoKien { get; set; }
        public String DiemGui { get; set; }
        public String DiemTra { get; set; }
    }
}
