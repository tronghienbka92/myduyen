using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.ChonVes
{
    public class QuanHuyenMap : NopEntityTypeConfiguration<QuanHuyen>
    {
        public QuanHuyenMap()
        {
            this.ToTable("CV_QuanHuyen");
            this.HasKey(c => c.Id);
            this.Property(u => u.Ten).HasMaxLength(200);
            this.Property(u => u.Ma).HasMaxLength(50);
        }
    }
}
