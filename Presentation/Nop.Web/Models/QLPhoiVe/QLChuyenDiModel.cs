using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.NhaXes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.QLPhoiVe
{
    public class QLChuyenDiModel
    {
        [UIHint("DateNullable")]
        public DateTime TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime DenNgay { get; set; }
        public string KeySearch { get; set; }
        public int TuyenId { get; set; }
        public IList<SelectListItem> ListTuyens { get; set; }
        /// <summary>
        /// -1: tat ca, 0: chua thu, 1: da thu
        /// </summary>
        public int isCapNhat { get; set; }
        public List<ChuyenDiModel> lschuyendi { get; set; }
        public class ChuyenDiModel
        {  
            public ChuyenDiModel()
            {

            }
          public ChuyenDiModel(XeXuatBenItemModel chuyendi)
            {
                this.xexuatben = chuyendi;
            }
            public XeXuatBenItemModel xexuatben { get; set; }
            public ChuyenDiTaiChinh chuyenditc { get; set; }
            public decimal DoanhThu { get; set; }
        }
    }
}