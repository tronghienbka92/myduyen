using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class DiaChiMap : NopEntityTypeConfiguration<DiaChi>
    {
        public DiaChiMap()
        {
            this.ToTable("CV_DiaChi");
            this.HasKey(c => c.Id);
            this.Property(u => u.DiaChi1).HasMaxLength(1000);
            this.Property(u => u.DiaChi2).HasMaxLength(1000);
            this.Property(u => u.MaBuuDien).HasMaxLength(20);
            this.Property(u => u.DienThoai).HasMaxLength(100);
            this.Property(u => u.Fax).HasMaxLength(100);
            this.Property(p => p.Latitude).HasPrecision(18, 4);
            this.Property(p => p.Longitude).HasPrecision(18, 4);

            this.HasRequired(p => p.Province)
                .WithMany()
                .HasForeignKey(p => p.ProvinceID);
            this.HasOptional(p => p.Quanhuyen)
               .WithMany()
               .HasForeignKey(p => p.QuanHuyenID);
        }
    }
}
