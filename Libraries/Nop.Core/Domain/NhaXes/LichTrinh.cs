using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.NhaXes
{
    public class LichTrinh:BaseEntity
    {
        public LichTrinh()
        {
            KhoaLichTrinh = false;
        }
        public string MaLichTrinh { get; set; }
        public int NhaXeId { get; set; }
        public int HanhTrinhId { get; set; }
        public virtual HanhTrinh HanhTrinhInfo { get; set; }
        public int LoaiXeId { get; set; }
        public virtual LoaiXe loaixeinfo { get; set; }
        public DateTime ThoiGianDi { get; set; }
        public Decimal SoGioChay { get; set; }
        public DateTime ThoiGianDen { get; set; }
        

        /// <summary>
        /// Thoi han mo ban ve, tinh bang ngay, tinh tu thoi diem hien tai
        /// </summary>
        public int TimeOpenOnline { get; set; }
        /// <summary>
        /// Thoi han dong ban ve online, tinh bang h, tinh tu thoi diem hien tai
        /// </summary>
        public int TimeCloseOnline { get; set; }

        /// <summary>
        /// gia ve toan tuyen
        /// </summary>
        public Decimal GiaVeToanTuyen { get; set; }
        public bool KhoaLichTrinh { get; set; }

        public bool IsLichTrinhGoc { get; set; }
        
    }
}
