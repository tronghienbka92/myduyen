using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class BangGiaCuoc : BaseEntity
    {
        public int NhaXeId { get; set; }
        public int LoaiHangHoaId { get; set; }
        public virtual LoaiHangHoa loaihanghoa { get; set; }
        public string GhiChu { get; set; }
        public Decimal DonViFrom { get; set; }
        public Decimal DonViTo { get; set; }
        public Decimal GiaCuoc { get; set; }
        public Decimal GiaCuocMoRong { get; set; }
    }
}
