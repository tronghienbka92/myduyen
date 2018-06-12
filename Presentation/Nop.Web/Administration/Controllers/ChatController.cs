using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Admin.Extensions;
using Nop.Core.Domain.Chat;
using Nop.Core.Domain.Customers;
using Nop.Services.Chat;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Security;
using Nop.Admin.Models.Chat;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Controllers;
using Nop.Admin.Models.ChonVes;

namespace Nop.Admin.Controllers
{
    public class ChatController : BaseAdminController
    {
        private readonly IChatService _chatService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IPermissionService _permissionService;
        public ChatController(IChatService chatService,
            IWorkContext workContext,
             ICustomerService customerService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerAttributeService customerAttributeService,
            IGenericAttributeService genericAttributeService,
            ICustomerRegistrationService customerRegistrationService,
            IPermissionService permissionService
            )
        {
            this._chatService = chatService;
            this._workContext = workContext;
            this._customerService = customerService;
            this._customerAttributeParser = customerAttributeParser;
            this._customerAttributeService = customerAttributeService;
            this._genericAttributeService = genericAttributeService;
            this._customerRegistrationService = customerRegistrationService;
            this._permissionService = permissionService;
        }
        [HttpGet]
        public ActionResult GetHistoryConvertation(int ConvertationId, int MessengerLastId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();
            var convertation = _chatService.GetConvertationById(ConvertationId);
            if (convertation != null)
            {

                var messengers = _chatService.GetMessengerNew(convertation.Id, MessengerLastId);
                if (messengers.Count() == 0)
                    return Json("nomsg", JsonRequestBehavior.AllowGet);
                var _messengers = messengers.Select(c => new
                {
                    Id = c.Id,
                    Text = c.Text,
                    Isagents = c.IsAgents,
                }).ToList();
                return Json(_messengers, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("noconver", JsonRequestBehavior.AllowGet);
        }

        public ActionResult WindowsChatAgents(int ConvertationId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();
            var model = new WindowChatAgentsModel();
            model.ConvertationId = ConvertationId;
            model.MessengerLastId = 0;
            return View(model);

        }
        [HttpPost]
        public ActionResult WindowsChatAgents(string messger, int ConvertationId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();
            if (!string.IsNullOrWhiteSpace(messger) && ConvertationId > 0)
            {
                var conver = _chatService.GetConvertationById(ConvertationId);
                var agents = _chatService.GetAgentsByCustomerId(_workContext.CurrentCustomer.Id);
                conver.AgentsId = agents.Id;
                _chatService.UpdateConvertation(conver);
                var msg = new Messenger();
                msg.Text = messger;
                msg.TimeSent = DateTime.Now;
                msg.IsAgents = true;
                msg.IsView = true;
                msg.ConvertationId = ConvertationId;
                _chatService.InsertMessenger(msg);
                var arrconver = _chatService.GetMessengerNew(ConvertationId, 0);
                foreach (var item in arrconver)
                {
                    if (!item.IsView)
                    {
                        item.IsView = true;
                        _chatService.UpdateMessenger(item);
                    }

                }
                return Json("ok");
            }
            return Json("rong");
        }
        #region quan ly agents
        public ActionResult ListAgents()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();

            var model = new ListAgentsModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ListAgents(DataSourceRequest command, ListAgentsModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();

            var arragents = _chatService.GetallAgents(model.NameAgents);

            var gridModel = new DataSourceResult
            {
                Data = arragents.Select(x =>
                {
                    var m = new AgentsModel();
                    m.Id = x.Id;
                    PrepareAgentstoModel(x, m);
                    return m;
                }),
                Total = arragents.Count()
            };

            return Json(gridModel);
        }
        public ActionResult CreateAgents()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();
            var model = new AgentsModel();
            preparecustomer(model);
            return View(model);
        }
        protected virtual void PrepareAgentstoModel(Agents from, AgentsModel to)
        {

            to.NickName = from.NickName;
            to.AvartaId = from.AvartaId;
            to.CustomerId = from.CustomerId;
            to.NgayTao = from.NgayTao;
        }
        protected virtual void PrepareAgentstoEntity(Agents to, AgentsModel from)
        {

            to.NickName = from.NickName;
            to.AvartaId = from.AvartaId;
            to.CustomerId = from.CustomerId;
            to.NgayTao = from.NgayTao;
        }
        protected virtual void preparecustomer(AgentsModel model)
        {
            model.Customer = new AgentsModel.KhachHangModel();
            if (model.CustomerId > 0)
            {
                var custommer = _customerService.GetCustomerById(model.CustomerId);
                if (custommer != null)
                {
                    model.Customer.Id = custommer.Id;
                    model.Customer.Email = custommer.Email;
                    model.Customer.Fullname = custommer.GetFullName();
                }

            }
        }
       
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult CreateAgents(AgentsModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();

            var agents = new Agents();
            PrepareAgentstoEntity(agents, model);
            var _nhanvien = _customerService.GetCustomerByEmail(model.Customer.Email);
            agents.CustomerId = _nhanvien.Id;
            _chatService.InsertAgents(agents);
            return continueEditing ? RedirectToAction("EditAgents", new { id = agents.Id }) : RedirectToAction("ListAgents");

            //return View(model);
        }
        public ActionResult EditAgents(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();
            var _agents = _chatService.GetAgentsById(id);
            if (_agents == null)
                //No manufacturer found with the specified id
                return RedirectToAction("ListAgents");
            var model = new AgentsModel();
            PrepareAgentstoModel(_agents, model);
            preparecustomer(model);
            //default values           

            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult EditAgents(AgentsModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();

            var _agents = _chatService.GetAgentsById(model.Id);
            if (_agents == null)
                //No manufacturer found with the specified id
                return RedirectToAction("ListAgents");

            if (ModelState.IsValid)
            {
                _agents = new Agents();
                PrepareAgentstoEntity(_agents, model);
                _chatService.UpdateAgents(_agents);


                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("EditAgents", _agents.Id);
                }
                return RedirectToAction("ListAgents");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteAgents(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();

            var _agents = _chatService.GetAgentsById(id);
            if (_agents == null)
                //No manufacturer found with the specified id
                return RedirectToAction("ListAgents");

            _chatService.DeleteAgents(_agents);

            return RedirectToAction("ListAgents");
        }
        #endregion
        #region convertaion
        public ActionResult ListConvertation()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();

            var model = new ConvertationModel();

            return View(model);
        }

        public ActionResult _ListConvertation()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();
            var agent = _chatService.GetAgentsByCustomerId(_workContext.CurrentCustomer.Id);
            if (agent == null)
            {
                return AccessDeniedView();
            }
            var arrconvertation = _chatService.GetallConvertation(agent.Id);            
            var _arrconver = arrconvertation.Select(c =>
                {
                    var m = new ConvertationModel();
                    m.Id = c.Id;
                    m.AgentsNickName = "Chưa trả lời";
                    if (c.AgentsId != null)
                    {
                        var agents = _chatService.GetAgentsById(c.AgentsId.Value);
                        m.AgentsNickName = agents.NickName;
                    }
                    m.NgayTaoText = c.NgayTao.ToString();
                    m.TenKhachHang = "Khách vãng lai";
                    if (c.CustomerId != null)
                    {
                        var _customer = _customerService.GetCustomerById(c.CustomerId.Value);
                        m.TenKhachHang = _customer.Email;
                    }
                    m.IsNewConvertation = false;
                    var arrmessenger = _chatService.GetallMessengerByConvertationId(c.Id);
                    foreach (var _item in arrmessenger)
                    {
                        if (!_item.IsView)
                        {
                            m.IsNewConvertation = true;                           
                            break;
                        }
                    }
                    return m;
                });
            _arrconver = _arrconver.OrderByDescending(m => m.IsNewConvertation).Take(10).ToList();
            return Json(_arrconver, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Load_Historyconversation()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVChat))
                return AccessDeniedView();
            var agent = _chatService.GetAgentsByCustomerId(_workContext.CurrentCustomer.Id);
            if (agent == null)
            {
                return AccessDeniedView();
            }
            var arrconvertation = _chatService.GetallConversation_History(agent.Id);
            var _arrconver = arrconvertation.Select(c =>
            {
                var m = new ConvertationModel();
                m.Id = c.Id;
                m.AgentsNickName = "Chưa trả lời";
                if (c.AgentsId != null)
                {
                    var agents = _chatService.GetAgentsById(c.AgentsId.Value);
                    m.AgentsNickName = agents.NickName;
                }
                m.NgayTaoText = c.NgayTao.ToString();
                m.TenKhachHang = "Khách vãng lai";
                if (c.CustomerId != null)
                {
                    var _customer = _customerService.GetCustomerById(c.CustomerId.Value);
                    m.TenKhachHang = _customer.Email;
                }                    
                return m;
            });           
            return Json(_arrconver, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}