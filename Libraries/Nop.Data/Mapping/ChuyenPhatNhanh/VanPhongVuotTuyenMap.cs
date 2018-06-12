using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class VanPhongVuotTuyenMap : NopEntityTypeConfiguration<VanPhongVuotTuyen>
    {
        public VanPhongVuotTuyenMap()
        {
            this.ToTable("CV_VanPhongVuotTuyen");
            this.HasKey(c => c.Id);
            this.HasRequired(c => c.vanphonggui)
           .WithMany()
           .HasForeignKey(c => c.VanPhongGuiId);

            this.HasRequired(c => c.vanphonggiua)
           .WithMany()
           .HasForeignKey(c => c.VanPhongGiuaId);

            this.HasRequired(c => c.vanphongcuoi)
           .WithMany()
           .HasForeignKey(c => c.VanPhongCuoiId);
        }
    }
}
