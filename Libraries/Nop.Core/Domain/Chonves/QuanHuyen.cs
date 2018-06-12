using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Chonves
{
    
    public class QuanHuyen : BaseEntity
    {
        public string Ten { get; set; }
        public string Ma { get; set; }
        public int ProvinceID { get; set; }
    }
}
