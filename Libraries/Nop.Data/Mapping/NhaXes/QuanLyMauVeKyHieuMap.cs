using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.NhaXes;

namespace Nop.Data.Mapping.NhaXes
{
    public class QuanLyMauVeKyHieuMap : NopEntityTypeConfiguration<QuanLyMauVeKyHieu>
    {
        public QuanLyMauVeKyHieuMap()
        {
            this.ToTable("CV_QuanLyMauVeKyHieu");
            this.HasKey(c => c.Id);
            this.Property(u => u.MauVe).HasMaxLength(100);
            this.Property(u => u.KyHieu).HasMaxLength(100);
        }
    }
}
