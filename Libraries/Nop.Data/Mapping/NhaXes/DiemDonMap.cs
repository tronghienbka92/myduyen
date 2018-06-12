using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class DiemDonMap : NopEntityTypeConfiguration<DiemDon>
    {
        public DiemDonMap()
        {
            this.ToTable("CV_DiemDon");
            this.HasKey(c => c.Id);
            this.Property(u => u.TenDiemDon).HasMaxLength(500);

            this.HasOptional(c => c.vanphong)
               .WithMany()
               .HasForeignKey(c => c.VanPhongId);
            this.HasOptional(c => c.benxe)
               .WithMany()
               .HasForeignKey(c => c.BenXeId);

            this.Ignore(dd => dd.LoaiDiemDon);
        }
    }
}
