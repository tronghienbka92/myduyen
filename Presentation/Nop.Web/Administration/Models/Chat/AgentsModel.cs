using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Chat
{
    public class AgentsModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Tên hiển thị")]
        public string NickName { get; set; }
        [NopResourceDisplayName("Chọn khách hàng")]
        public int CustomerId { get; set; }
        public KhachHangModel Customer { get; set; }
        [UIHint("Picture")]
        [NopResourceDisplayName("Ảnh đại diện")]
        public int AvartaId { get; set; }
        public DateTime NgayTao { get; set; }

        public bool IsDelete { get; set; }
        public class KhachHangModel : BaseNopEntityModel
        {
           
            [Required]
            [DataType(DataType.EmailAddress)]
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.Email")]
            public string Email { get; set; }
            

            [NopResourceDisplayName("Admin.Customers.Customers.Fields.TenKhachHang")]

            public string Fullname { get; set; }
           
            [Required]
            public string FirstName { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.lastName")]
            [Required]
            public string LastName { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.Active")]
            public bool Active { get; set; }
            //customer roles
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.CustomerRoles")]
            public int CustomerRoleId { get; set; }
           



        }
    }
}