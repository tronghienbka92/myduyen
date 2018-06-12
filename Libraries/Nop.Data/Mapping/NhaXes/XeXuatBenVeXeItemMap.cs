using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class XeXuatBenVeXeItemMap : NopEntityTypeConfiguration<XeXuatBenVeXeItem>
    {
        public XeXuatBenVeXeItemMap()
        {
            this.ToTable("CV_XeXuatBenVeXeItem_Temp");
            this.HasKey(c => c.Id);
        }
    }
}
