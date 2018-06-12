using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.NhaXes;

namespace Nop.Core.Domain.ChuyenPhatNhanh
{
    public class PhieuChuyenPhatVanChuyen : BaseEntity
    {
        public int PhieuChuyenPhatId { get; set; }
        public virtual PhieuChuyenPhat phieuchuyenphat { get; set; }
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
        public decimal CuocVuotTuyen { get; set; }
    }
}
