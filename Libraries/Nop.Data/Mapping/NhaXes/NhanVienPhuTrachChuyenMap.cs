using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
namespace Nop.Data.Mapping.NhaXes
{
    public class NhanVienPhuTrachChuyenMap : NopEntityTypeConfiguration<NhanVienPhuTrachChuyen>
    {
        public NhanVienPhuTrachChuyenMap()
        {
            this.ToTable("CV_NhanVienPhuTrachChuyen");
            this.HasKey(c => c.Id);

            this.HasRequired(c => c.PhuTrachChuyen)
             .WithMany()
             .HasForeignKey(c => c.NhanVienID);
            this.HasRequired(c => c.NguonVe)
            .WithMany()
            .HasForeignKey(c => c.NguonVeXeID);
        }

    }
}
