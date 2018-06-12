using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.NhaXes
{
    public class LoaiXe:BaseEntity
    {
        public int NhaXeId { get; set; }        
        public string TenLoaiXe { get; set; }
        public string MaLoaiXe { get; set; }
        public int KieuXeID { get; set; }
        public int SoDoGheXeID { get; set; }
        public virtual SoDoGheXe sodoghe { get; set; }
        //cac tien ich
        public bool IsWC { get; set; }
        public bool IsTV { get; set; }
        public bool IsWifi { get; set; }
        public bool IsDieuHoa { get; set; }
        public bool IsNuocUong { get; set; }
        public bool IsKhanLanh { get; set; }
        public bool IsThucAn { get; set; }
        
        public ENKieuXe KieuXe
        {
            get
            {
                return (ENKieuXe)KieuXeID;
            }
            set
            {
                KieuXeID = (int)value;
            }
        }
       
        
        
    }
}
