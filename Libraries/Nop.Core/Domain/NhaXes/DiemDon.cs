using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.NhaXes
{
    public class DiemDon : BaseEntity
    {
        public int NhaXeId { get; set; }
        public string TenDiemDon { get; set; }
        public string MaMau { get; set; }
        public int LoaiDiemDonId { get; set; }
        public int DiaChiId { get; set; }
        public int? VanPhongId { get; set; }
        public virtual VanPhong vanphong { get; set; }
        public int? BenXeId { get; set; }
        public virtual BenXe benxe { get; set; }
        public ENLoaiDiemDon LoaiDiemDon
        {
            get
            {
                return (ENLoaiDiemDon)LoaiDiemDonId;
            }
            set
            {
                LoaiDiemDonId = (int)value;
            }
        }
    }
}
