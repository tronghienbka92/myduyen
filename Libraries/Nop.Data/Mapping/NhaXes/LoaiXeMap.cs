using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class LoaiXeMap : NopEntityTypeConfiguration<LoaiXe>
    {
        public LoaiXeMap()
        {
            this.ToTable("CV_LoaiXe");
            this.HasKey(c => c.Id);
            this.Property(c => c.TenLoaiXe).HasMaxLength(200);
            this.HasRequired(c => c.sodoghe)
            .WithMany()
            .HasForeignKey(c => c.SoDoGheXeID);
            this.Ignore(c => c.KieuXe);


        }
    }
}
