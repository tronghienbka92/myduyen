using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class NhanVienMap : NopEntityTypeConfiguration<NhanVien>
    {
        public NhanVienMap()
        {
            this.ToTable("CV_NhanVien");
            this.HasKey(c => c.Id);
            this.Property(u => u.HoVaTen).HasMaxLength(200);
            this.Property(u => u.Email).HasMaxLength(500);
            this.Property(u => u.CMT_Id).HasMaxLength(30);
            this.Property(u => u.CMT_NoiCap).HasMaxLength(200);
            this.HasRequired(c => c.diachilienlac)
               .WithMany()
               .HasForeignKey(c => c.DiaChiID);

            this.HasMany(c => c.VanPhongs)
          .WithMany()
          .Map(m => m.ToTable("CV_NhanVien_VanPhong_Mapping"));

            this.HasMany(c => c.DiemDons)
       .WithMany()
       .Map(m => m.ToTable("CV_NhanVien_DiemDon_Mapping"));

            this.Ignore(c => c.KieuNhanVien);
            this.Ignore(c => c.GioiTinh);
            this.Ignore(c => c.TrangThai);
            this.Ignore(c => c.TenVaHo);
        }
    }
}
