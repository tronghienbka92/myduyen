using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class NguoiVanChuyenMap : NopEntityTypeConfiguration<NguoiVanChuyen>
    {
        public NguoiVanChuyenMap()
        {
            this.ToTable("CV_NguoiVanChuyen");
            this.HasKey(c => c.Id);
            this.Property(u => u.HoVaTen).HasMaxLength(200);
            this.Property(u => u.DienThoai).HasMaxLength(200);
            this.Property(u => u.CMT).HasMaxLength(50);
            this.HasRequired(c => c.tovanchuyen)
              .WithMany(o=>o.nguoivanchuyens)
              .HasForeignKey(c => c.ToVanChuyenId);
           
        }
    }
}
