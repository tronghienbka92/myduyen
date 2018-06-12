using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class HangHoaMap : NopEntityTypeConfiguration<HangHoa>
    {
        public HangHoaMap()
        {
            this.ToTable("CV_HangHoa");
            this.HasKey(c => c.Id);

            this.Property(u => u.CanNang).HasPrecision(18, 1);
            this.Property(u => u.GiaTri).HasPrecision(18, 0);
            this.Property(u => u.GiaCuoc).HasPrecision(18, 0);
            this.Ignore(u => u.LoaiHangHoa);

            this.HasRequired(p => p.phieuguihang)
                .WithMany(u=>u.HangHoas)
                .HasForeignKey(p => p.PhieuGuiHangId);

        }
    }
}
