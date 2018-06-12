using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Customers;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.NhaXes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Web.Models.ChuyenPhatNhanh
{
    public class KhuVucVanPhongModel : BaseNopEntityModel
    {
        public String TenKhuVuc { get; set; }
        public List<VanPhongModel> vanphongs { get; set; }
    }
}