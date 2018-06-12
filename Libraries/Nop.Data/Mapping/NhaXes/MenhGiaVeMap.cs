using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class MenhGiaVeMap : NopEntityTypeConfiguration<MenhGiaVe>
    {
        public MenhGiaVeMap()
        {
            this.ToTable("CV_MenhGiaVe");
            this.HasKey(c => c.Id);
            this.Property(p => p.MenhGia).HasPrecision(18, 0);
        }
    }
}
