using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.ChonVes
{
    public class TuyenVeXeMap : NopEntityTypeConfiguration<TuyenVeXe>
    {
        public TuyenVeXeMap()
        {
            this.ToTable("CV_TuyenVeXe");
            this.HasKey(c => c.Id);
            this.Property(u => u.PriceOld).HasPrecision(18, 0);
            this.Property(u => u.PriceNew).HasPrecision(18, 0);

            this.Ignore(u => u.KieuXe);
        }
    }
}
