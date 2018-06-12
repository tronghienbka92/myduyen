using Nop.Core;
using Nop.Core.Domain.Chat;
using Nop.Core.Domain.Customers;
using Nop.Services.Chat;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Web.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Controllers
{
    public class ChatController : BasePublicController
    {
        private readonly IChatService _chatService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        public ChatController(IChatService chatService,
            IWorkContext workContext,
             ICustomerService customerService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerAttributeService customerAttributeService,
            IGenericAttributeService genericAttributeService,
            ICustomerRegistrationService customerRegistrationService)
        {
            this._chatService = chatService;
            this._workContext = workContext;
            this._customerService = customerService;
            this._customerAttributeParser = customerAttributeParser;
            this._customerAttributeService = customerAttributeService;
            this._genericAttributeService = genericAttributeService;
            this._customerRegistrationService = customerRegistrationService;
        }
        [HttpPost]
        public ActionResult GetHistoryConvertation(int MessengerLastId)
        {

            if (Session["convertation"] != null)
            {
                var convertation = _chatService.GetConvertationBySessionCustomer(Session["convertation"].ToString());
                if (convertation != null)
                {
                    var messengers = _chatService.GetMessengerNew(convertation.Id, MessengerLastId);
                    if (messengers.Count() == 0)
                        return Json("nomsg");
                    var _messengers = messengers.Select(c => new
                    {
                        Id = c.Id,
                        Text = c.Text,
                        Isagents = c.IsAgents,
                    }).ToList();
                    return Json(_messengers, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json("noconver");

            }
            else
                return Json("noconver");
        }
        public ActionResult Chat()
        {

            return View();
        }
        public ActionResult WindowsChat()
        {
            var model = new WindowsChatModel();
            model.MessengerLastId = 0;
            return View(model);
        }


        [HttpPost]
        public ActionResult WindowsChat(string messger)
        {
            if (!string.IsNullOrWhiteSpace(messger))
            {

                //var sessioncustomer = _workContext.CurrentCustomer.Id;
                var msg = new Messenger();
                msg.Text = messger;
                msg.TimeSent = DateTime.Now;
                msg.IsAgents = false;
                msg.IsView = false;
                if (Session["convertation"] == null)
                {
                    var conver = new Convertation();
                    conver.NgayTao = DateTime.Now;
                    Session["convertation"] = Guid.NewGuid();
                    conver.SessionConvertation = Session["convertation"].ToString();
                    if (_workContext.CurrentCustomer.IsRegistered())
                    {
                        conver.CustomerId = _workContext.CurrentCustomer.Id;
                    }

                    _chatService.InsertConvertation(conver);
                    msg.ConvertationId = conver.Id;
                }
                else
                {
                    var _convertation = _chatService.GetConvertationBySessionCustomer(Session["convertation"].ToString());
                    if (_convertation == null)
                    {
                        var conver = new Convertation();
                        conver.NgayTao = DateTime.Now;
                        Session["convertation"] = Guid.NewGuid();
                        conver.SessionConvertation = Session["convertation"].ToString();
                        if (_workContext.CurrentCustomer.IsRegistered())
                        {
                            conver.CustomerId = _workContext.CurrentCustomer.Id;
                        }
                        _chatService.InsertConvertation(conver);
                        msg.ConvertationId = conver.Id;
                    }
                    else
                    {
                        msg.ConvertationId = _convertation.Id;
                    }

                }


                _chatService.InsertMessenger(msg);
                return Json("ok");
            }
            return Json("rong");
        }

    }
}