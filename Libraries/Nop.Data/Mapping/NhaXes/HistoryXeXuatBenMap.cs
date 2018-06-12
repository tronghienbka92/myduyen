using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class HistoryXeXuatBenMap : NopEntityTypeConfiguration<HistoryXeXuatBen>
    {
        public HistoryXeXuatBenMap()
        {
            this.ToTable("CV_HistoryXeXuatBen");
            this.HasKey(c => c.Id);
            this.Property(u => u.GhiChu).HasMaxLength(4000);
            this.HasOptional(c => c.xevanchuyen)
             .WithMany()
             .HasForeignKey(c => c.XeVanChuyenId);

            this.HasRequired(c => c.NguonVeInfo)
              .WithMany()
              .HasForeignKey(c => c.NguonVeId);
            this.HasRequired(c => c.NguoiTao)
             .WithMany()
             .HasForeignKey(c => c.NguoiTaoId);

            this.HasRequired(c => c.HanhTrinh)
              .WithMany()
              .HasForeignKey(c => c.HanhTrinhId);

            this.Ignore(c => c.TrangThai);
        }
    }
}
