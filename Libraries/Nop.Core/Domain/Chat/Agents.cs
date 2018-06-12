using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Chat
{
    public class Agents : BaseEntity
    {
        public string NickName { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int AvartaId { get; set; }
        public DateTime NgayTao { get; set; }
        public bool IsDelete { get; set; }
    }
}
