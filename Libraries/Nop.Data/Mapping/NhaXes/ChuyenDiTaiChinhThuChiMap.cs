using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class ChuyenDiTaiChinhThuChiMap : NopEntityTypeConfiguration<ChuyenDiTaiChinhThuChi>
    {
        public ChuyenDiTaiChinhThuChiMap()
        {
            this.ToTable("CV_ChuyenDiTaiChinhThuChi");
            this.HasKey(c => c.Id);
            this.Property(u => u.GhiChu).HasMaxLength(500);
            this.Property(p => p.SoTien).HasPrecision(18, 0);

            this.HasRequired(c => c.chuyenditaichinh)
              .WithMany(o=>o.GiaoDichThuChis)
              .HasForeignKey(c => c.ChuyenDiTaiChinhId);

            this.Ignore(c => c.loaithuchi);
            
        }
    }
}
