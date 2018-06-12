using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class TuyenHanhTrinhMap : NopEntityTypeConfiguration<TuyenHanhTrinh>
    {
        public TuyenHanhTrinhMap()
        {
            this.ToTable("CV_TuyenHanhTrinh");
            this.HasKey(c => c.Id);
            this.Property(u => u.Ten).HasMaxLength(2000);
        }
    }
}
