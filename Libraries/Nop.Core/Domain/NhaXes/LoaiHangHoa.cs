using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class LoaiHangHoa : BaseEntity
    {
        public string Ten { get; set; }
        public int NhaXeId { get; set; }
    }
}
