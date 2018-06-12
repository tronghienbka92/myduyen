using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class SoDoGheXeQuyTacMap : NopEntityTypeConfiguration<SoDoGheXeQuyTac>
    {
        public SoDoGheXeQuyTacMap()
        {
            this.ToTable("CV_SoDoGheXeQuyTac");
            this.HasKey(c => c.Id);
            this.Property(c => c.Val).HasMaxLength(20);         
        }
    }
}
