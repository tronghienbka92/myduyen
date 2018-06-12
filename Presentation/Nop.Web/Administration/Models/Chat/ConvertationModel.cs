using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Chat
{
    public class ConvertationModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Khách hàng")]
        public string CustomerSession { get; set; }
        public string TenKhachHang { get; set; }

      
        public int AgentsId { get; set; }
        public bool IsNewConvertation { get; set; }
        public bool IsfirstLoad { get; set; }
        public string AgentsNickName { get; set; }

        [NopResourceDisplayName("Ngày tạo")]
        public DateTime NgayTao { get; set; }
        public string NgayTaoText { get; set; }
    }
}