using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class ToVanChuyenMap : NopEntityTypeConfiguration<ToVanChuyen>
    {
        public ToVanChuyenMap()
        {
            this.ToTable("CV_ToVanChuyen");
            this.HasKey(c => c.Id);
            this.Property(u => u.TenTo).HasMaxLength(500);
            this.Property(u => u.MoTa).HasMaxLength(500);
           
           
        }
    }
}
