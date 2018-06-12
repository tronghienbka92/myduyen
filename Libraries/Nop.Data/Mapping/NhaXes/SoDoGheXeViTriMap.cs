using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class SoDoGheXeViTriMap: NopEntityTypeConfiguration<SoDoGheXeViTri>
    {
        public SoDoGheXeViTriMap()
        {
            this.ToTable("CV_SoDoGheXeViTri");
            this.HasKey(c => c.Id);            
        }
    }
}
