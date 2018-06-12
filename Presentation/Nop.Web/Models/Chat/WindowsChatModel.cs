using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.Chat
{
    //[Validator(typeof(LoginValidator))]
    public partial class WindowsChatModel : BaseNopModel
    {
        [AllowHtml]
        public string Messenger { get; set; }
        public int MessengerLastId { get; set; }
       
    }
}