using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.NhaXes;

namespace Nop.Core.Domain.ChuyenPhatNhanh
{
    public class PhieuVanChuyenLog : BaseEntity
    {
        public PhieuVanChuyenLog()
        {
            NgayTao = DateTime.Now;
        }
        public int NguoiGiaoId { get; set; }
        public virtual NhanVien NguoiGiao { get; set; }
        public int NguoiNhanId { get; set; }
        public virtual NhanVien NguoiNhan { get; set; }
        public int PhieuVanChuyenId { get; set; }
        public virtual PhieuVanChuyen phieuvanchuyen { get; set; }
        public int ChuyenDiId { get; set; }
        public virtual HistoryXeXuatBen chuyendi { get; set; }
        public int HanhTrinhId { get; set; }
        public virtual HanhTrinh hanhtrinh { get; set; }
        public int? TuyenId { get; set; }
        public virtual TuyenHanhTrinh tuyen { get; set; }
        public int VanPhongGuiId { get; set; }
        public virtual VanPhong vanphonggui { get; set; }
        public int VanPhongNhanId { get; set; }
        public virtual VanPhong vanphongnhan { get; set; }
        public int KhuVucId { get; set; }
        public virtual KhuVuc khuvuc { get; set; }
        public decimal TongCuoc { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
    }
}
