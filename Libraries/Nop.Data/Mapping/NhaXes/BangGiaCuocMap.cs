using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class BangGiaCuocMap : NopEntityTypeConfiguration<BangGiaCuoc>
    {
        public BangGiaCuocMap()
        {
            this.ToTable("CV_BangGiaCuoc");
            this.HasKey(c => c.Id);
            this.Property(u => u.GhiChu).HasMaxLength(2000);
            this.Property(p => p.DonViFrom).HasPrecision(18, 2);
            this.Property(p => p.DonViTo).HasPrecision(18, 2);
            this.Property(p => p.GiaCuoc).HasPrecision(18, 0);
            this.Property(p => p.GiaCuocMoRong).HasPrecision(18, 0);

            this.HasRequired(p => p.loaihanghoa)
                .WithMany()
                .HasForeignKey(p => p.LoaiHangHoaId);
        }
    }
}
