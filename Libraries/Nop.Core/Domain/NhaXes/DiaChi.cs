using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Directory;
using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.NhaXes
{
    public class DiaChi : BaseEntity
    {
        public string DiaChi1 { get; set; }
        public string DiaChi2 { get; set; }
        public int ProvinceID { get; set; }
        public int? QuanHuyenID { get; set; }        
        public string MaBuuDien { get; set; }
        public string DienThoai { get; set; }
        public string Fax { get; set; }
        /// <summary>
        /// Thong tin dinh vi GPS
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// Thong tin dinh vi GPS
        /// </summary>
        public decimal Longitude { get; set; }

        public virtual StateProvince Province { get; set; }
        public virtual QuanHuyen Quanhuyen { get; set; }
    }
}
