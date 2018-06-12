using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class ToVanChuyenVanPhongMap : NopEntityTypeConfiguration<ToVanChuyenVanPhong>
    {
        public ToVanChuyenVanPhongMap()
        {
            this.ToTable("CV_VanPhong_ToVanChuyen_Mapping");
            this.HasKey(c => c.Id);
            this.HasRequired(c => c.vanphong)
               .WithMany(o=>o.tovanchuyens)
               .HasForeignKey(c => c.VanPhongId);

            this.HasRequired(c => c.tovanchuyen)
             .WithMany(o=>o.tovanchuyenvps)
             .HasForeignKey(c => c.ToVanChuyenId);


        }
    }
}
