using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Chat
{
    public class ListAgentsModel : BaseNopModel
    {
         [NopResourceDisplayName("Tên NVCSKH")]
        public string NameAgents { get; set; }
    }
}