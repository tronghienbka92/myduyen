using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.ChonVes
{
    public class PhoiVeNoteMap : NopEntityTypeConfiguration<PhoiVeNote>
    {
        public PhoiVeNoteMap()
        {
            this.ToTable("CV_PhoiVeNote");
            this.HasKey(c => c.Id);          

            this.HasRequired(a => a.PhoiVe)
             .WithMany()
             .HasForeignKey(a => a.PhoiVeId).WillCascadeOnDelete(false);
           
            
        }
    }
}
