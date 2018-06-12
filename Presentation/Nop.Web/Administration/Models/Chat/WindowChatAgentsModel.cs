using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Chat
{
    public class WindowChatAgentsModel : BaseNopEntityModel
    {
        public int ConvertationId { get; set; }
        public int MessengerLastId { get; set; }
    }
}