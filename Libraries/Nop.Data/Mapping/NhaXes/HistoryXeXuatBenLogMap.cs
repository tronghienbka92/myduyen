using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class HistoryXeXuatBenLogMap : NopEntityTypeConfiguration<HistoryXeXuatBenLog>
    {
        public HistoryXeXuatBenLogMap()
        {
            this.ToTable("CV_HistoryXeXuatBen_Log");
            this.HasKey(c => c.Id);
            this.Property(u => u.GhiChu).HasMaxLength(500);
            this.HasRequired(c => c.xexuatben)
             .WithMany(x=>x.NhatKys)
             .HasForeignKey(c => c.XeXuatBenId);

            this.HasRequired(c => c.NguoiTao)
           .WithMany()
           .HasForeignKey(c => c.NguoiTaoId);

            this.Ignore(c => c.TrangThai);
        }
    }
}
