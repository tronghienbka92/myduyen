using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class PhieuVanChuyenMap : NopEntityTypeConfiguration<PhieuVanChuyen>
    {
        public PhieuVanChuyenMap()
        {
            this.ToTable("CV_PhieuVanChuyen");
            this.HasKey(c => c.Id);
            this.Property(u => u.SoLenh).HasMaxLength(50);
          
            this.HasRequired(c => c.KhuVucDen)
              .WithMany()
              .HasForeignKey(c => c.KhuVucDenId);
            this.HasRequired(c => c.vanphong)
              .WithMany()
              .HasForeignKey(c => c.VanPhongId);
            this.Ignore(c => c.TrangThai);
            this.Ignore(c => c.LoaiPhieuVanChuyen);
        }
    }
}
