using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class NhaXeCauHinhMap : NopEntityTypeConfiguration<NhaXeCauHinh>
    {
        public NhaXeCauHinhMap()
        {
            this.ToTable("CV_NhaXeCauHinh");
            this.HasKey(c => c.Id);
            this.Property(u => u.Ma).HasMaxLength(50);
            this.Property(u => u.Ten).HasMaxLength(500);
            this.Ignore(u => u.kieudulieu);
            this.Ignore(u => u.MaCauHinh);
        }
    }
}
