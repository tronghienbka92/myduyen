using Nop.Core.Domain.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Chat
{
    public class MessengerMap : NopEntityTypeConfiguration<Messenger>
    {
        public MessengerMap()
          {
              this.ToTable("CV_Messenger");
              this.HasKey(c => c.Id);             
              this.HasRequired(a => a.Convertation)
               .WithMany()
               .HasForeignKey(a => a.ConvertationId).WillCascadeOnDelete(false);
            
          }
    }
}
