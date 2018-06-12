using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Web.Models.ChuyenPhatNhanh
{
    public class InPhieuModel : BaseNopEntityModel
    {

        public InPhieuModel()
        {

            NgayTraHang = DateTime.Now;
        }
       
        public IList<PhieuChuyenPhatModel> PhieuChuyenPhats { get; set; }
        /// <summary>
        /// isXepPhieu=true sap xep phieu bien nhan theo vung id, id desc
        /// isXepPhieu=false sap xep phieu bien nhan id desc
        /// </summary>
        public DateTime NgayTraHang { get; set; }
        public string TenVanPhongNhan { get; set; }
        public string SoPhieu { get; set; }
    }
}
