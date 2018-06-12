using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Chat
{
   public class Convertation:BaseEntity
    {
       public int? AgentsId { get; set; }
       public virtual Agents NhanVienCSKH { get; set; }
       public string SessionConvertation { get; set; }
       public int? CustomerId { get; set; }     
       public DateTime NgayTao { get; set; }
    }
}
