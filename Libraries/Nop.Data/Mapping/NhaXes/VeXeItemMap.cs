using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.NhaXes
{
    public class VeXeItemMap : NopEntityTypeConfiguration<VeXeItem>
    {
        public VeXeItemMap()
        {
            this.ToTable("CV_VeXeItem");
            this.HasKey(c => c.Id);
            this.Property(u => u.MauVe).HasMaxLength(100);
            this.Property(u => u.KyHieu).HasMaxLength(100);
            this.Property(u => u.SoSeri).HasMaxLength(50);
            this.Property(u => u.GiaVe).HasPrecision(18,0);
            this.HasOptional(c => c.nhanvien)
             .WithMany()
             .HasForeignKey(c => c.NhanVienId);

            this.HasOptional(c => c.nguonve)
             .WithMany()
             .HasForeignKey(c => c.NguonVeXeId);

            this.HasRequired(c => c.menhgia)
           .WithMany()
           .HasForeignKey(c => c.MenhGiaId);

            this.HasOptional(c => c.quyenve)
             .WithMany()
             .HasForeignKey(c => c.QuyenId);

            this.HasOptional(c => c.changgiave)
           .WithMany()
           .HasForeignKey(c => c.ChangId);

            this.HasOptional(c => c.xexuatben)
          .WithMany()
          .HasForeignKey(c => c.XeXuatBenId);

            this.Ignore(c => c.TrangThai);
            
            this.Ignore(c => c.CanDelete);   
           
        }
    }
}
