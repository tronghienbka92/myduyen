using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class VeXeQuyenMap : NopEntityTypeConfiguration<VeXeQuyen>
    {
        public VeXeQuyenMap()
        {
            this.ToTable("CV_VeXeQuyen");
            this.HasKey(c => c.Id);
            this.Property(u => u.ThongTin).HasMaxLength(200);           
        }
    }
}
