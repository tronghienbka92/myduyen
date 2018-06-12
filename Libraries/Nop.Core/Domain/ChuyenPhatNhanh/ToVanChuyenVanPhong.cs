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
    public class ToVanChuyenVanPhong : BaseEntity
    {
      
        public int VanPhongId { get; set; }
        public virtual VanPhong vanphong { get; set; }
        public int ToVanChuyenId { get; set; }
        public virtual ToVanChuyen tovanchuyen { get; set; }
        
    }
}
