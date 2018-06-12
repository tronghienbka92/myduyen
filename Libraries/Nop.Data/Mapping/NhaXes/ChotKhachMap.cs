using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class ChotKhachMap : NopEntityTypeConfiguration<ChotKhach>
    {
        public ChotKhachMap()
        {
            this.ToTable("CV_ChotKhach");
            this.HasKey(c => c.Id);
            this.Property(u => u.Ma).HasMaxLength(50);
            this.Property(u => u.ViTriChot).HasMaxLength(500);
            this.Property(p => p.Latitude).HasPrecision(18, 9);
            this.Property(p => p.Longitude).HasPrecision(18, 9);
            this.HasRequired(c => c.nguoichot)
               .WithMany()
               .HasForeignKey(c => c.NguoiChotId);
            this.HasRequired(c => c.diemchot)
              .WithMany()
              .HasForeignKey(c => c.DiemDonId);
            this.HasRequired(c => c.historychuyenxe)
              .WithMany()
              .HasForeignKey(c => c.HistoryXeXuatBenId);
        }
    }
}
