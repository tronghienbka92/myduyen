using System;
using System.Linq;
using System.Collections.Generic;
using Nop.Core.Domain.ChuyenPhatNhanh;


namespace Nop.Core.Domain.NhaXes
{
    public class HanhTrinh:BaseEntity
    {
        
        public int NhaXeId { get; set; }
        public string MaHanhTrinh { get; set; }
        /// <summary>
        /// thong tin mo ta hanh trinh duoi dang text
        /// </summary>
        public string MoTa { get; set; }

        /// <summary>
        /// Chieu dai hanh trinh tinh theo km
        /// </summary>
        public int TongKhoangCach { get; set; }
       

        private ICollection<HanhTrinhDiemDon> _diemdons;
        public virtual ICollection<HanhTrinhDiemDon> DiemDons
        {
            get { return _diemdons ?? (_diemdons = new List<HanhTrinhDiemDon>()); }
            protected set { _diemdons = value; }
        }
        private ICollection<HanhTrinhDiemChot> _diemchots;
        public virtual ICollection<HanhTrinhDiemChot> DiemChots
        {
            get { return _diemchots ?? (_diemchots = new List<HanhTrinhDiemChot>()); }
            protected set { _diemchots = value; }
        }
        private ICollection<VanPhong> _vanphongs;
        public virtual ICollection<VanPhong> VanPhongs
        {
            get { return _vanphongs ?? (_vanphongs = new List<VanPhong>()); }
            protected set { _vanphongs = value; }
        }
        public bool isTuyenDi { get; set; }
        public int? TuyenHanhTrinhId { get; set; }
        public virtual TuyenHanhTrinh tuyen { get; set; }
    }
}
