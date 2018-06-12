using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.ChuyenPhatNhanh
{
    public class NguoiVanChuyen : BaseEntity
    {
        public string HoVaTen { get; set; }
        public string DienThoai { get; set; }
        public string CMT { get; set; }
        public int ToVanChuyenId { get; set; }
        public virtual ToVanChuyen tovanchuyen { get; set; }
    }
}
