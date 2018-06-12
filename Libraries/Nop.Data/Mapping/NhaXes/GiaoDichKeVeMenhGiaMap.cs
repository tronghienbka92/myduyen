using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class GiaoDichKeVeMenhGiaMap : NopEntityTypeConfiguration<GiaoDichKeVeMenhGia>
    {
        public GiaoDichKeVeMenhGiaMap()
        {
            this.ToTable("CV_GiaoDichKeVeMenhGia");
            this.HasKey(c => c.Id);
            this.Property(u => u.GhiChu).HasMaxLength(500);
            this.Property(u => u.SeriFrom).HasMaxLength(50);
            this.HasOptional(c => c.quanly)
           .WithMany()
           .HasForeignKey(c => c.QuanLyMauVeKyHieuId);
            this.HasRequired(c => c.giaodichkeve)
             .WithMany(u=>u.kevemenhgias)
             .HasForeignKey(c => c.GiaoDichKeVeId);

            this.HasRequired(c => c.menhgia)
             .WithMany()
             .HasForeignKey(c => c.MenhGiaId);
            this.Ignore(c => c.ActionType);
            this.Ignore(c => c.SeriNumFrom);
            
        }
    }
}
