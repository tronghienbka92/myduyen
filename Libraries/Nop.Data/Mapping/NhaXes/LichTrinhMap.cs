using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class LichTrinhMap : NopEntityTypeConfiguration<LichTrinh>
    {
        public LichTrinhMap()
        {
            this.ToTable("CV_LichTrinh");
            this.HasKey(c => c.Id);
            this.Property(u => u.MaLichTrinh).HasMaxLength(500);
            this.Property(u => u.GiaVeToanTuyen).HasPrecision(18, 0);
            this.Property(u => u.SoGioChay).HasPrecision(18, 1);
            this.HasRequired(c => c.HanhTrinhInfo)
               .WithMany()
               .HasForeignKey(c => c.HanhTrinhId);
            this.HasRequired(c => c.loaixeinfo)
                   .WithMany()
                   .HasForeignKey(c => c.LoaiXeId);
        
        }
    }
}
