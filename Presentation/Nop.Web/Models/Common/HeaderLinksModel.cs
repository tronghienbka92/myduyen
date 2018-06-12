using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Models.Common
{
    public partial class HeaderLinksModel : BaseNopModel
    {
        public bool IsAuthenticated { get; set; }
        public string CustomerEmailUsername { get; set; }
        public string CustomerFullname { get; set; }
        
        public bool ShoppingCartEnabled { get; set; }
        public int ShoppingCartItems { get; set; }
        
        public bool WishlistEnabled { get; set; }
        public int WishlistItems { get; set; }

        public bool AllowPrivateMessages { get; set; }
        public string UnreadPrivateMessages { get; set; }
        public string AlertMessage { get; set; }
        public LoginModel khachhang { get; set; }
        public class LoginModel : BaseNopModel
        {
             [NopResourceDisplayName("Account.Login.Fields.Email")]
            [Required]
            public string Email { get; set; }
             [NopResourceDisplayName("Account.Login.Fields.Password")]
             [Required]
            public string Password { get; set; }
             [NopResourceDisplayName("Account.HeaderLink.Fields.RememberMe")]
            public bool RememberMe { get; set; }
        }
    }
}