using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.ChonVes
{
    public class PhoiVeMap : NopEntityTypeConfiguration<PhoiVe>
    {
        public PhoiVeMap()
        {
            this.ToTable("CV_PhoiVe");
            this.HasKey(c => c.Id);
            this.Property(u => u.SessionId).HasMaxLength(200);
            this.Property(u => u.GiaVeHienTai).HasPrecision(18, 0);
            this.Property(u => u.GhiChu).HasMaxLength(500);
            this.Property(u => u.ViTriLenXe).HasMaxLength(500);
            this.Property(u => u.ViTriXuongXe).HasMaxLength(500);

            this.HasRequired(a => a.sodoghexequytac)
             .WithMany()
             .HasForeignKey(a => a.SoDoGheXeQuyTacId).WillCascadeOnDelete(false);
            this.HasOptional(a => a.nguoidatve)
            .WithMany()
            .HasForeignKey(a => a.NguoiDatVeId).WillCascadeOnDelete(false);

            this.HasOptional(a => a.changgiave)
               .WithMany()
               .HasForeignKey(a => a.ChangId);

            this.HasOptional(a => a.ChuyenDi)
            .WithMany()
            .HasForeignKey(a => a.ChuyenDiId);
            this.Ignore(u => u.TrangThai);

        }
    }
}
