using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.ChonVes
{
    public class DiaDiemMap : NopEntityTypeConfiguration<DiaDiem>
    {
        public DiaDiemMap()
        {
            this.ToTable("CV_DiaDiem");
            this.HasKey(c => c.Id);
            this.Property(u => u.Ten).HasMaxLength(500);
            this.Property(u => u.TenKhongDau).HasMaxLength(500);
            this.Ignore(dd => dd.Loai);
        }
    }
}
