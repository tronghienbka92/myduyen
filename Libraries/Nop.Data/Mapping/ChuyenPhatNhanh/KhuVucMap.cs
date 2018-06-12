using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class KhuVucMap : NopEntityTypeConfiguration<KhuVuc>
    {
        public KhuVucMap()
        {
            this.ToTable("CV_KhuVuc");
            this.HasKey(c => c.Id);
            this.Property(u => u.TenVietTat).HasMaxLength(50);

          
        }
    }
}
