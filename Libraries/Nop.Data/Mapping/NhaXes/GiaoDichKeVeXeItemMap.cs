using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class GiaoDichKeVeXeItemMap : NopEntityTypeConfiguration<GiaoDichKeVeXeItem>
    {
        public GiaoDichKeVeXeItemMap()
        {
            this.ToTable("CV_GiaoDichKeVeXeItem");
            this.HasKey(c => c.Id);

           
            this.HasRequired(p => p.kevemenhgia)
                .WithMany(u => u.vexeitems)
                .HasForeignKey(p => p.GiaoDichKeVeMenhGiaId);

            this.HasRequired(c => c.vexeitem)
             .WithMany()
             .HasForeignKey(c => c.VeXeItemId);
        }
    }
}
