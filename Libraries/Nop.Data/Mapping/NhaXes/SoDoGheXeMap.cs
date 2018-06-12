using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;


namespace Nop.Data.Mapping.NhaXes
{
    public class SoDoGheXeMap : NopEntityTypeConfiguration<SoDoGheXe>
    {
        public SoDoGheXeMap()
        {
            this.ToTable("CV_SoDoGheXe");
            this.HasKey(c => c.Id);
            this.Property(c => c.TenSoDo).HasMaxLength(200);
            this.Ignore(c => c.KieuXe);
        }
    }
}
