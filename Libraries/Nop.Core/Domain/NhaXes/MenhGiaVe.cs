using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class MenhGiaVe : BaseEntity
    {
        public decimal MenhGia { get; set; }
        public int NhaXeId { get; set; }
    }
}
