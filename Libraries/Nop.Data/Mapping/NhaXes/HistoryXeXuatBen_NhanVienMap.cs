using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class HistoryXeXuatBen_NhanVienMap : NopEntityTypeConfiguration<HistoryXeXuatBen_NhanVien>
    {
        public HistoryXeXuatBen_NhanVienMap()
        {
            this.ToTable("CV_HistoryXeXuatBen_NhanVien_Mapping");
            this.HasKey(c => c.Id);

            this.HasRequired(c => c.nhanvien)
             .WithMany()
             .HasForeignKey(c => c.NhanVien_Id);

            this.HasRequired(c => c.xexuatben)
              .WithMany(o=>o.LaiPhuXes)
              .HasForeignKey(c => c.HistoryXeXuatBen_Id);
           

            this.Ignore(c => c.KieuNhanVien);
        }
    }
}
