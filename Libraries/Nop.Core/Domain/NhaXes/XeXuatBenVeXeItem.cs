using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class XeXuatBenVeXeItem:BaseEntity
    {
        public int XeXuatBenId { get; set; }
        public int VeXeItemId { get; set; }
        public int? ChangId { get; set; }
        
    }
}
