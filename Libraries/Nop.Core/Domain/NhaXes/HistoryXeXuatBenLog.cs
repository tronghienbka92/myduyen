using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class HistoryXeXuatBenLog : BaseEntity
    {
        public int XeXuatBenId { get; set; }
        public virtual HistoryXeXuatBen xexuatben { get; set; }
        public int TrangThaiId { get; set; }
        public ENTrangThaiXeXuatBen TrangThai
        {
            get
            {
                return (ENTrangThaiXeXuatBen)TrangThaiId;
            }
            set
            {
                TrangThaiId = (int)value;
            }
        }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
        public int NguoiTaoId { get; set; }
        public virtual NhanVien NguoiTao { get; set; }
    }
}
