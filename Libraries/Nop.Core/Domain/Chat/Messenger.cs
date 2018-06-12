using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Chat
{
   public class Messenger:BaseEntity
    {
       public int ConvertationId { get; set; }
       public virtual Convertation Convertation { get; set; }
       public DateTime TimeSent { get; set; }
       public bool IsAgents { get; set; }
       public bool IsView { get; set; }
       public string Text { get; set; }
    }
}
