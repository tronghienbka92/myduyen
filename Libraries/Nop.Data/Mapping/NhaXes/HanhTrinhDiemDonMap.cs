using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class HanhTrinhDiemDonMap : NopEntityTypeConfiguration<HanhTrinhDiemDon>
    {
        public HanhTrinhDiemDonMap()
        {
            this.ToTable("CV_HanhTrinh_DiemDon_Mapping");
            this.HasKey(c => c.Id);
            this.HasRequired(c => c.hanhtrinh)
               .WithMany(o => o.DiemDons)               
               .HasForeignKey(c => c.HanhTrinhId);
            
            this.HasRequired(c => c.diemdon)
                .WithMany()
                .HasForeignKey(c => c.DiemDonId);
        }
    }
    public class HanhTrinhDiemChotMap : NopEntityTypeConfiguration<HanhTrinhDiemChot>
    {
        public HanhTrinhDiemChotMap()
        {
            this.ToTable("CV_HanhTrinh_DiemChot_Mapping");
            this.HasKey(c => c.Id);
            this.HasRequired(c => c.hanhtrinh)
               .WithMany(o => o.DiemChots)
               .HasForeignKey(c => c.HanhTrinhId);

            this.HasRequired(c => c.diemdon)
                .WithMany()
                .HasForeignKey(c => c.DiemDonId);
        }
    }
}
