using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class PhieuGuiHangMap : NopEntityTypeConfiguration<PhieuGuiHang>
    {
        public PhieuGuiHangMap()
        {
            this.ToTable("CV_PhieuGuiHang");
            this.HasKey(c => c.Id);  
            
            this.Property(u => u.MaPhieu).HasMaxLength(100);
            this.Property(u => u.DiemGui).HasMaxLength(500);
            this.Property(u => u.DiemTra).HasMaxLength(500);
            this.Property(u => u.GhiChu).HasMaxLength(2000);           
            this.HasRequired(c => c.nguoitao)
                .WithMany()
                .HasForeignKey(c => c.NguoiTaoId);

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

            this.HasOptional(c => c.XeXuatBen)
               .WithMany()
               .HasForeignKey(c => c.XeXuatBenId);
            this.HasOptional(c => c.NhanVienThuTien)
              .WithMany()
              .HasForeignKey(c => c.NhanVienThuTienId);
            this.Ignore(c => c.TinhTrangVanChuyen);
            this.Ignore(c => c.TongTienCuoc);
            this.Ignore(c => c.TongKhoiLuong);
            this.Ignore(c => c.TongSoKien);
           

        }
    }
}
