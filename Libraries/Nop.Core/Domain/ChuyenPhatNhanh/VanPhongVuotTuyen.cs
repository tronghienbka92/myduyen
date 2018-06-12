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
    public class VanPhongVuotTuyen : BaseEntity
    {
        public int VanPhongGuiId { get; set; }
        public virtual VanPhong vanphonggui { get; set; }
        public int VanPhongGiuaId { get; set; }
        public virtual VanPhong vanphonggiua { get; set; }
        public int VanPhongCuoiId { get; set; }
        public virtual VanPhong vanphongcuoi { get; set; }
    }
}
