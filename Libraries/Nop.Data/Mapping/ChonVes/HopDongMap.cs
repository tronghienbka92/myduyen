using Nop.Core.Domain.Chonves;

namespace Nop.Data.Mapping.ChonVes
{
    public class HopDongMap : NopEntityTypeConfiguration<HopDong>
    {
        public HopDongMap()
        {
            this.ToTable("CV_HopDong");
            this.HasKey(c => c.Id);
            this.Property(u => u.MaHopDong).HasMaxLength(100);
            this.Property(u => u.TenHopDong).HasMaxLength(1000);
            this.Property(u => u.ThongTin).HasColumnType("ntext");

            this.Ignore(dd => dd.TrangThai);
            this.Ignore(dd => dd.LoaiHopDong);
        }
    }
}
