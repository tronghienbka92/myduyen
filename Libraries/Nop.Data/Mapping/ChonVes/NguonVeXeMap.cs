using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.ChonVes
{
    public class NguonVeXeMap: NopEntityTypeConfiguration<NguonVeXe>
    {
        public NguonVeXeMap()
        {
            this.ToTable("CV_NguonVeXe");
            this.HasKey(c => c.Id);
            this.Property(u => u.TenNhaXe).HasMaxLength(500);
            this.Property(u => u.TenLoaiXe).HasMaxLength(200);
            this.Property(u => u.TenDiemDon).HasMaxLength(200);
            this.Property(u => u.TenDiemDen).HasMaxLength(200);
            this.Property(u => u.GiaVeHienTai).HasPrecision(18, 0);
            //this.Property(u => u.GiaVeMoi).HasPrecision(18, 0);            
            this.HasRequired(a => a.loaixe)
               .WithMany()
               .HasForeignKey(a => a.LoaiXeId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.LichTrinhInfo)
              .WithMany()
              .HasForeignKey(a => a.LichTrinhId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DiemDon)
             .WithMany()
             .HasForeignKey(a => a.DiemDonId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DiemDen)
             .WithMany()
             .HasForeignKey(a => a.DiemDenId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DiemDonGoc)
            .WithMany()
            .HasForeignKey(a => a.DiemDonGocId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DiemDenGoc)
            .WithMany()
            .HasForeignKey(a => a.DiemDenGocId).WillCascadeOnDelete(false);
           
        }
    }
}
