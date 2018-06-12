using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class GiaoDichKeVeMap : NopEntityTypeConfiguration<GiaoDichKeVe>
    {
        public GiaoDichKeVeMap()
        {
            this.ToTable("CV_GiaoDichKeVe");
            this.HasKey(c => c.Id);
            this.Property(u => u.Ma).HasMaxLength(100);
            this.Property(u => u.GhiChu).HasMaxLength(1000);
            this.Property(u => u.SessionId).HasMaxLength(100);

            this.HasRequired(c => c.nguoigiao)
             .WithMany()
             .HasForeignKey(c => c.NguoiGiaoId);

            this.HasRequired(c => c.nguoinhan)
             .WithMany()
             .HasForeignKey(c => c.NguoiNhanId);

            this.HasOptional(c => c.quaybanve)
            .WithMany()
            .HasForeignKey(c => c.VanPhongId);

            this.Ignore(c => c.TrangThai);
            this.Ignore(c => c.PhanLoai);
            this.Ignore(c => c.LoaiVe);
           
        }
    }
}
