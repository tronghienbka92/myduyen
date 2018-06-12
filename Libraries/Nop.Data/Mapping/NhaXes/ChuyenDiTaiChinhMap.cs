using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class ChuyenDiTaiChinhMap : NopEntityTypeConfiguration<ChuyenDiTaiChinh>
    {
        public ChuyenDiTaiChinhMap()
        {
            this.ToTable("CV_ChuyenDiTaiChinh");
            this.HasKey(c => c.Id);
            this.HasRequired(c => c.nguoitao)
              .WithMany()
              .HasForeignKey(c => c.NguoiTaoId);
            this.HasOptional(c => c.xevanchuyen)
              .WithMany()
              .HasForeignKey(c => c.XeVanChuyenId);
            this.HasRequired(c => c.luotdi)
              .WithMany()
              .HasForeignKey(c => c.LuotDiId);
            this.HasOptional(c => c.luotve)
              .WithMany()
              .HasForeignKey(c => c.LuotVeId);
            this.HasOptional(c => c.ChuyenDiTCLaiXe)
             .WithMany()
             .HasForeignKey(c => c.ChuyenDiTCLaiXeId);

            this.HasOptional(c => c.ChuyenDiTCPhuXe)
            .WithMany()
            .HasForeignKey(c => c.ChuyenDiTCPhuXeId);
             this.Property(p => p.DinhMucDau).HasPrecision(18, 3);
            this.Property(p => p.ThucDo).HasPrecision(18,3);
            this.Property(c => c.GiaDau).HasPrecision(18,3);

            this.HasOptional(c => c.nguoithu)
              .WithMany()
              .HasForeignKey(c => c.NguoiThuId);
            this.HasOptional(c => c.nguoinop)
              .WithMany()
              .HasForeignKey(c => c.NguoiNopId);


           
        }
    }
}
