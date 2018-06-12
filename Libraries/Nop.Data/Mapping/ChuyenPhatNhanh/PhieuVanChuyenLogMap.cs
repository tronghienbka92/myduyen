using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class PhieuVanChuyenLogMap : NopEntityTypeConfiguration<PhieuVanChuyenLog>
    {
        public PhieuVanChuyenLogMap()
        {
            this.ToTable("CV_PhieuVanChuyenLog");
            this.HasKey(c => c.Id);
            this.Property(p => p.TongCuoc).HasPrecision(18, 0);
            this.HasRequired(c => c.phieuvanchuyen)
                .WithMany(o=>o.nhatkyvanchuyens)
                .HasForeignKey(c => c.PhieuVanChuyenId);

            this.HasRequired(c => c.chuyendi)
              .WithMany()
              .HasForeignKey(c => c.ChuyenDiId);

            this.HasRequired(c => c.hanhtrinh)
              .WithMany()
              .HasForeignKey(c => c.HanhTrinhId);

            this.HasRequired(c => c.khuvuc)
              .WithMany()
              .HasForeignKey(c => c.KhuVucId);

            this.HasOptional(c => c.tuyen)
              .WithMany()
              .HasForeignKey(c => c.TuyenId);


            this.HasRequired(c => c.vanphonggui)
              .WithMany()
              .HasForeignKey(c => c.VanPhongGuiId);
            this.HasRequired(c => c.vanphongnhan)
             .WithMany()
             .HasForeignKey(c => c.VanPhongNhanId);

            this.HasRequired(c => c.NguoiNhan)
              .WithMany()
              .HasForeignKey(c => c.NguoiNhanId);

            this.HasRequired(c => c.NguoiGiao)
              .WithMany()
              .HasForeignKey(c => c.NguoiGiaoId);
        }
    }
}
