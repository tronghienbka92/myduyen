using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class GheItemMap : NopEntityTypeConfiguration<GheItem>
    {
        public GheItemMap()
        {
            this.ToTable("CV_GheItem");
            this.HasKey(c => c.Id);
            this.Property(c => c.KyHieuGhe).HasMaxLength(20);

        }
    }
}
