using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class ThuChiMap : NopEntityTypeConfiguration<ThuChi>
    {
        public ThuChiMap()
        {
            this.ToTable("CV_ThuChi");
            this.HasKey(c => c.Id);
            this.Property(c => c.Ma).HasMaxLength(50);
            this.Property(c => c.DienGiai).HasMaxLength(2000);
            this.Property(p => p.GiaTri).HasPrecision(18, 0);

            this.HasOptional(c => c.chuyendi)
            .WithMany()
            .HasForeignKey(c => c.ChuyenDiId);

            this.HasRequired(c => c.loaithuchi)
            .WithMany()
            .HasForeignKey(c => c.LoaiThuChiId);

            this.HasOptional(c => c.nguoinop)
            .WithMany()
            .HasForeignKey(c => c.NguoiNopId);

            this.HasOptional(c => c.nguoithu)
            .WithMany()
            .HasForeignKey(c => c.NguoiThuId);

            this.HasRequired(c => c.nguoitao)
            .WithMany()
            .HasForeignKey(c => c.NguoiTaoId);


        }
    }
}
