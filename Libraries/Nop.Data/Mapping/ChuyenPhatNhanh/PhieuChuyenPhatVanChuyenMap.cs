using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class PhieuChuyenPhatVanChuyenMap : NopEntityTypeConfiguration<PhieuChuyenPhatVanChuyen>
    {
        public PhieuChuyenPhatVanChuyenMap()
        {
            this.ToTable("CV_PhieuChuyenPhat_VanChuyen");
            this.HasKey(c => c.Id);
            this.Property(p => p.TongCuoc).HasPrecision(18, 0);
            this.Property(p => p.CuocVuotTuyen).HasPrecision(18, 0);
            this.HasRequired(c => c.phieuchuyenphat)
                .WithMany(o=>o.nhatkyvanchuyens)
                .HasForeignKey(c => c.PhieuChuyenPhatId);
            this.HasRequired(c => c.phieuvanchuyen)
                .WithMany()
                .HasForeignKey(c => c.PhieuVanChuyenId);
            this.HasRequired(c => c.chuyendi)
              .WithMany()
              .HasForeignKey(c => c.ChuyenDiId);

            this.HasRequired(c => c.hanhtrinh)
              .WithMany()
              .HasForeignKey(c => c.HanhTrinhId);

            this.HasRequired(c => c.khuvuc)
              .WithMany()
              .HasForeignKey(c => c.KhuVucId);          

            this.HasOptional(c => c.tuyen)
              .WithMany()
              .HasForeignKey(c => c.TuyenId);


            this.HasRequired(c => c.vanphonggui)
              .WithMany()
              .HasForeignKey(c => c.VanPhongGuiId);
            this.HasRequired(c => c.vanphongnhan)
             .WithMany()
             .HasForeignKey(c => c.VanPhongNhanId);
           

        }
    }
}
