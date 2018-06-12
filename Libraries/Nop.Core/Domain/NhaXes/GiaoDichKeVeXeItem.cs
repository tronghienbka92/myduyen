using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Nop.Core.Domain.NhaXes
{
    public class GiaoDichKeVeXeItem : BaseEntity
    {
        public int GiaoDichKeVeId { get; set; }
        public int VeXeItemId { get; set; }
        public virtual VeXeItem vexeitem { get; set; }
        public int GiaoDichKeVeMenhGiaId { get; set; }
        public virtual GiaoDichKeVeMenhGia kevemenhgia { get; set; }
    

    }
}
