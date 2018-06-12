using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.Customer
{
    public class RegisterCustomerModel:BaseNopEntityModel
    {
        
           
           
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.Email")]
          
            public string Email { get; set; }
           
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.FullNameKhachhang")]          
            public string Fullname { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.Fields.Phone")]

            public string Phone { get; set; }
            public string ReturnUrl { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }   
           
            
          
            
       }
}