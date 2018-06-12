using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class HanhTrinhGiaVeMap : NopEntityTypeConfiguration<HanhTrinhGiaVe>
    {
        public HanhTrinhGiaVeMap()
        {
            this.ToTable("CV_HanhTrinhGiaVe");
            this.HasKey(u => u.Id);
            this.Property(u => u.GiaVe).HasPrecision(18, 0);
            this.HasRequired(c => c.HanhTrinh)
            .WithMany()
            .HasForeignKey(c => c.HanhTrinhId);

            this.HasRequired(c => c.DiemDon)
           .WithMany()
           .HasForeignKey(c => c.DiemDonId);

            this.HasRequired(c => c.DiemDen)
           .WithMany()
           .HasForeignKey(c => c.DiemDenId);
            this.HasOptional(c => c.loaixe)
        .WithMany()
        .HasForeignKey(c => c.LoaiXeId);
           
        }
    }
}
