using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class LichTrinhGiaVeMap : NopEntityTypeConfiguration<LichTrinhGiaVe>
    {
        public LichTrinhGiaVeMap()
        {
            this.ToTable("CV_LichTrinhGiaVe");
            this.HasKey(u => u.Id);
            this.Property(u => u.GiaVe).HasPrecision(18, 0);
           
        }
    }
}
