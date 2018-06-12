using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class LoaiThuChiMap : NopEntityTypeConfiguration<LoaiThuChi>
    {
        public LoaiThuChiMap()
        {
            this.ToTable("CV_LoaiThuChi");
            this.HasKey(c => c.Id);
            this.Property(c => c.Ten).HasMaxLength(200);

        }
    }
}
