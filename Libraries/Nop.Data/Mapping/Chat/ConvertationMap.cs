using Nop.Core.Domain.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Chat
{
    class ConvertationMap : NopEntityTypeConfiguration<Convertation>
    {
        public ConvertationMap()
          {
              this.ToTable("CV_Convertation");
              this.HasKey(c => c.Id);
              this.Property(u => u.SessionConvertation).HasMaxLength(500);             
            
              this.HasOptional(a => a.NhanVienCSKH)
               .WithMany()
               .HasForeignKey(a => a.AgentsId).WillCascadeOnDelete(false);
             
          }
    }
}
