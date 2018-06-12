using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class VeXeQuyen : BaseEntity
    {
        public int NhaXeId { get; set; }
        public string ThongTin { get; set; }
        public int ThuTuBan { get; set; }
    }
}
