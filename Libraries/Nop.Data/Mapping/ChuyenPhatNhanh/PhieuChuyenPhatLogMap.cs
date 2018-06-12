using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class PhieuChuyenPhatLogMap : NopEntityTypeConfiguration<PhieuChuyenPhatLog>
    {
        public PhieuChuyenPhatLogMap()
        {
            this.ToTable("CV_PhieuChuyenPhatLog");
            this.HasKey(c => c.Id);
            this.HasRequired(c => c.phieuchuyenphat)
                .WithMany(o => o.nhatkys)
                .HasForeignKey(c => c.PhieuChuyenPhatId);
           
        }
    }
}
