using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class TuyenHanhTrinh : BaseEntity
    {
        public int NhaXeId { get; set; }
        public string Ten { get; set; }
        public string TenVietTat { get; set; }
    }
}
