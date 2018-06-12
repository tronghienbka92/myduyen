using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class PhieuChuyenPhatMap : NopEntityTypeConfiguration<PhieuChuyenPhat>
    {
        public PhieuChuyenPhatMap()
        {
            this.ToTable("CV_PhieuChuyenPhat");
            this.HasKey(c => c.Id);  
            
            this.Property(u => u.MaPhieu).HasMaxLength(100);            
            this.Property(u => u.GhiChu).HasMaxLength(2000);           
            this.HasRequired(c => c.NVGiaoDich)
                .WithMany()
                .HasForeignKey(c => c.NhanVienGiaoDichId);

            this.HasRequired(c => c.VanPhongGui)
              .WithMany()
              .HasForeignKey(c => c.VanPhongGuiId);

            this.HasRequired(c => c.VanPhongNhan)
              .WithMany()
              .HasForeignKey(c => c.VanPhongNhanId);

            this.HasRequired(c => c.NguoiGui)
              .WithMany()
              .HasForeignKey(c => c.NguoiGuiId);          

            this.HasRequired(c => c.NguoiNhan)
              .WithMany()
              .HasForeignKey(c => c.NguoiNhanId);

            this.HasOptional(c => c.NVNhanHang)
               .WithMany()
               .HasForeignKey(c => c.NhanVienNhanHangId
               );
            this.HasOptional(c => c.tovanchuyentra)
              .WithMany()
              .HasForeignKey(c => c.ToVanChuyenTraId
              );
            this.HasOptional(c => c.tovanchuyennhan)
             .WithMany()
             .HasForeignKey(c => c.ToVanChuyenNhanId
             );
            this.HasOptional(c => c.nguoivanchuyentra)
             .WithMany()
             .HasForeignKey(c => c.NguoiVanChuyenTraId
             );
            this.HasOptional(c => c.nguoivanchuyennhan)
             .WithMany()
             .HasForeignKey(c => c.NguoiVanChuyenNhanId
             );
            this.HasOptional(c => c.phieuvanchuyen)
            .WithMany(o=>o.phieuchuyenphats)
            .HasForeignKey(c => c.PhieuVanChuyenId
            ); 
            this.Ignore(c => c.TrangThai);
            this.Ignore(c => c.LoaiPhieu);
            this.Ignore(c => c.NhomPhieu);
            this.Ignore(c => c.TongTienCuoc);
           
           

        }
    }
}
