using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class HanhTrinhMap : NopEntityTypeConfiguration<HanhTrinh>
    {
        public HanhTrinhMap()
        {
            this.ToTable("CV_HanhTrinh");
            this.HasKey(c => c.Id);
            this.Property(u => u.MaHanhTrinh).HasMaxLength(50);
            this.Property(u => u.MoTa).HasMaxLength(500);
            this.HasMany(c => c.VanPhongs)
              .WithMany()
              .Map(m => m.ToTable("CV_VanPhong_HanhTrinh_Mapping"));
            this.HasOptional(p => p.tuyen)
               .WithMany()
               .HasForeignKey(p => p.TuyenHanhTrinhId);
           
        }
    }
}
