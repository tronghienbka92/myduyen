using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class BangGiaCuocHanhTrinhMap : NopEntityTypeConfiguration<BangGiaCuocHanhTrinh>
    {
        public BangGiaCuocHanhTrinhMap()
        {
            this.ToTable("CV_BangGiaCuoc_HanhTrinh_Mapping");
            this.HasKey(c => c.Id);
            this.Property(p => p.GiaCuoc).HasPrecision(18, 0);
            this.Property(p => p.GiaCuocMoRong).HasPrecision(18, 0);

            this.HasRequired(p => p.hanhtrinh)
                .WithMany()
                .HasForeignKey(p => p.HanhTrinhId);
            this.HasRequired(p => p.banggiacuoc)
                .WithMany()
                .HasForeignKey(p => p.BangGiaCuocId);

            this.HasOptional(p => p.diemdon1)
               .WithMany()
               .HasForeignKey(p => p.DiemDonId1);
            this.HasOptional(p => p.diemdon2)
               .WithMany()
               .HasForeignKey(p => p.DiemDonId2);
        }
    }
}
