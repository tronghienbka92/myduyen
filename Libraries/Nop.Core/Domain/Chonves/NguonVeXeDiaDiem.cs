using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Chonves
{
    public class NguonVeXeDiaDiem : BaseEntity
    {
        public int DiaDiemId { get; set; }
        public int NguonVeXeId { get; set; }
        public bool isDiemXuatPhat { get; set; }
    }
}
