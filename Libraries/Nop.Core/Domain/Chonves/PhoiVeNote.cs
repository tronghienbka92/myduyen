using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Chonves
{
    public class PhoiVeNote : BaseEntity
    {
        public int PhoiVeId { get; set; }
        public virtual PhoiVe PhoiVe { get; set; }
        public string Note { get; set; }
        public DateTime NgayTao { get; set; }
       
    }
}
