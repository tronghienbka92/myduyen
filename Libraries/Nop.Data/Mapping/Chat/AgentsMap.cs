using Nop.Core.Domain.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Chat
{
    public class AgentsMap : NopEntityTypeConfiguration<Agents>
    {
          public AgentsMap()
          {
              this.ToTable("CV_Agents");
              this.HasKey(c => c.Id);
              this.Property(u => u.NickName).HasMaxLength(500);             
             
              this.HasRequired(a => a.Customer)
               .WithMany()
               .HasForeignKey(a => a.CustomerId).WillCascadeOnDelete(false);
            
          }
        
    }
}
