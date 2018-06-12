using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class LoaiHangHoaMap : NopEntityTypeConfiguration<LoaiHangHoa>
    {
        public LoaiHangHoaMap()
        {
            this.ToTable("CV_LoaiHangHoa");
            this.HasKey(c => c.Id);
            this.Property(u => u.Ten).HasMaxLength(500);
        }
    }
}
