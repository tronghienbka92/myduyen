using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Common;
using Nop.Web.Models.Customer;
using Nop.Web.Models.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.VeXeKhach
{
    public class ThanhToanVeXeModel
    {
        public ThanhToanVeXeModel()
        {
            diachigiaohang = new AddressModel();
        }
        public AddressModel diachigiaohang { get; set; }
        public string HinhThucThanhToan { get; set; }
        public string BankCode { get; set; }      
        public NhaXe nhaxeinfo { get; set; }
        public NguonVeXe nguonvexeinfo { get; set; }        
        public DateTime NgayDi { get; set; }
        public DateTime NgayVe { get; set; }        
        public string KyHieuGhe { get; set; }
        public string ErrorSentOrder { get; set; }
        public List<PhoiVe> phoiveinfos { get; set; }
        public decimal TongTien { get; set; }
        public string MaXacThuc { get; set; }
        public string MaVe { get; set; }


    }
    public class XacNhanDienThoaiModel
    {
        public string MaXacNhan { get; set; }
         
    }
}