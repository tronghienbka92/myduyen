using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.ChuyenPhatNhanh
{
    public class PhieuChuyenPhatLog : BaseEntity
    {
     
        public String GhiChu { get; set; }
        public int PhieuChuyenPhatId { get; set; }
        public virtual PhieuChuyenPhat phieuchuyenphat { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
