using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class ThuChi : BaseEntity
    {
        public ThuChi()
        {
            NgayTao = DateTime.Now;
        }
        public string Ma { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public int? ChuyenDiId { get; set; }
        public virtual HistoryXeXuatBen chuyendi { get; set; }
        public int LoaiThuChiId { get; set; }
        public virtual LoaiThuChi loaithuchi { get; set; }
        public string DienGiai { get; set; }
        public decimal GiaTri { get; set; }
        public int? NguoiNopId { get; set; }
        public virtual NhanVien nguoinop { get; set; }
        public int? NguoiThuId { get; set; }
        public virtual NhanVien nguoithu { get; set; }
        public int NguoiTaoId { get; set; }
        public virtual NhanVien nguoitao { get; set; }
        public bool isChi { get; set; }
        public int NhaXeId { get; set; }
    }
}
