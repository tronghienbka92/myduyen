using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class SoDoGheXeQuyTac:BaseEntity
    {
        public string Val { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int Tang { get; set; }
        public int LoaiXeId { get; set; }
        
    }
}
