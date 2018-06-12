using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Admin.Extensions;
using Nop.Admin.Models.Catalog;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Services.Chonves;
using Nop.Admin.Models.ChonVes;
using Nop.Core;
using Nop.Services.NhaXes;
using Nop.Services.Directory;
using Nop.Core.Domain.Chonves;
using Nop.Admin.Models.NhaXes;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Core.Domain.NhaXes;

namespace Nop.Admin.Controllers
{
    public class NghiepVuLeTanController : BaseAdminController
    {
        #region "Khoi Tao"
        private readonly IStateProvinceService _stateProvinceService;
        private readonly INhanVienService _nhanvienService;
        private readonly INhaXeService _nhaxeService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly IChonVeService _chonveService;
        private readonly ICustomerService _customerService;
        private readonly IBenXeService _benxeService;
        private readonly IDiaChiService _diachiService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly CustomerSettings _customerSettings;


        public NghiepVuLeTanController(IChonVeService chonveService,
            IStateProvinceService stateProvinceService,
              INhaXeService nhaxeService,
            INhanVienService nhanvienService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            IPictureService pictureService,
            ICustomerService customerService,
             IBenXeService benxeService,
            IDiaChiService diachiService,
            IGenericAttributeService genericAttributeService,
            ICustomerRegistrationService CustomerRegistrationService,
             CustomerSettings customerSettings
            )
        {
            this._nhanvienService = nhanvienService;
            this._chonveService = chonveService;
            this._stateProvinceService = stateProvinceService;
            this._nhaxeService = nhaxeService;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._customerService = customerService;
            this._benxeService = benxeService;
            this._diachiService = diachiService;
            this._genericAttributeService = genericAttributeService;
            this._customerRegistrationService = CustomerRegistrationService;
            this._customerSettings = customerSettings;

        }
        #endregion
        #region common
        [NonAction]
        protected virtual string GetLabel(string _name)
        {
            return _localizationService.GetResource(string.Format("ChonVe.QuanTri.{0}", _name));
        }
        #endregion
        public ActionResult Index()
        {
            return View();
        }
        #region nghiep vu le tan
        public ActionResult ListHopDongDaDuyet()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVLeTanHopDong))
                return AccessDeniedView();
            var model = new HopDongListModel();
            //them danh sách sale
            model.NguoiTaos.Add(new SelectListItem { Text = GetLabel("ChonSale"), Value = "0" });
            //Lấy thông tin người tạo
            var nguoitaoids = _chonveService.GetNguoiTaoIds();
            foreach (var id in nguoitaoids)
            {
                var nguoitao = _customerService.GetCustomerById(id);
                if (nguoitao != null)
                {
                    model.NguoiTaos.Add(new SelectListItem { Text = nguoitao.GetFullName(), Value = nguoitao.Id.ToString() });
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ListHopDongDaDuyet(DataSourceRequest command, HopDongListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVLeTanHopDong))
                return AccessDeniedView();
            var hopdongs = _chonveService.GetAllHopDong(model.TimMaHopDong, model.TimTenHopDong,
                command.Page - 1, command.PageSize, false, model.NguoiTaoId, ENTrangThaiHopDong.DaDuyet);
            var gridModel = new DataSourceResult
            {
                Data = hopdongs.Select(x =>
                {
                    var m = x.ToModel();
                    m.TrangThaiText = x.TrangThai.GetLocalizedEnum(_localizationService, _workContext);
                    return m;
                }),
                Total = hopdongs.TotalCount
            };
            return Json(gridModel);
        }
        public ActionResult KhachHangList(string Email)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVLeTanHopDong))
                return AccessDeniedView();
            var items = _customerService.Search(Email).Select(x=>{
                var item = new XuLyHopDongModel.KhachHangBasic();
                item.Id = x.Id;
                item.Email = x.Email;
                return item;
            }).ToList();                   
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ChiTietHopDong(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVLeTanHopDong))
                return AccessDeniedView();          
            var model = new XuLyHopDongModel();
            var hopdong = _chonveService.GetHopDongById(Id);
            if (hopdong == null)
            {
                throw new ArgumentException(" no result found with th specified id");
            }
            model.Id = hopdong.Id;
            model.MaHopDong = hopdong.MaHopDong;
            model.TenHopDong = hopdong.TenHopDong;
            model.LoaiHopDongID = hopdong.LoaiHopDongID;
            model.LoaiHopDongText = hopdong.LoaiHopDong.GetLocalizedEnum(_localizationService, _workContext);
            model.TrangThaiID = hopdong.TrangThaiID;
            model.TrangThaiText = hopdong.TrangThai.GetLocalizedEnum(_localizationService, _workContext);
            model.NhaXeID = hopdong.NhaXeID;
            model.NhaXeText = _nhaxeService.GetNhaXeById(model.NhaXeID).TenNhaXe;
            model.ThongTin = hopdong.ThongTin;
            model.NgayKichHoat = hopdong.NgayKichHoat;
            model.TrangThaiID = hopdong.TrangThaiID;
            model.NguoiTaoID = hopdong.NguoiTaoID;
            model.TenNguoiTao = _customerService.GetCustomerById(model.NguoiTaoID).GetFullName();
            model.KhachHangID = hopdong.KhachHangID;          
            model.NguoiDuyetID = hopdong.NguoiDuyetID;
            model.TenNguoiDuyet = _customerService.GetCustomerById(model.NguoiDuyetID).GetFullName();
            prepareHopDongKhachHang(model);
            return View(model);
        }
        [HttpPost]
       
        public ActionResult ChiTietHopDong(int Id, string Email)
        {

            if (!_permissionService.Authorize(StandardPermissionProvider.CVLeTanHopDong))
                return AccessDeniedView();
            var hopdong = _chonveService.GetHopDongById(Id);
            var item = _customerService.GetCustomerByEmail(Email);
            if (hopdong == null || item == null)
            {
                return RedirectToAction("ListHopDongDaDuyet");
            }
            var khachhang=_customerService.GetCustomerById(hopdong.KhachHangID);
            if (hopdong.KhachHangID>0 && hopdong.KhachHangID!=item.Id)
            {
                
                var itemrole = new CustomerRole();
                foreach (var _item in khachhang.CustomerRoles)
                {
                    if (_item.SystemName == SystemCustomerRoleNames.HTNhaXeManager)
                        itemrole = _item;
                    
                }
                item.CustomerRoles.Remove(itemrole);
                _customerService.UpdateCustomer(khachhang);
            }
            hopdong.KhachHangID = item.Id;
            _chonveService.UpdateHopDong(hopdong);
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            var newCustomerRoles = new List<CustomerRole>();
            foreach (var customerRole in allCustomerRoles)
            {
                if (customerRole.SystemName == SystemCustomerRoleNames.Registered)
                    newCustomerRoles.Add(customerRole);
                if (customerRole.SystemName == SystemCustomerRoleNames.HTNhaXeManager)
                    newCustomerRoles.Add(customerRole);
            }
           
            //customerrole
            foreach (var _customerRole in newCustomerRoles)
            {
                item.CustomerRoles.Add(_customerRole);
            }
            _customerService.UpdateCustomer(item);
            return Json("OK");

        }
        public ActionResult TaoTaiKhoan(int _idhopdong)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVLeTanHopDong))
                return AccessDeniedView();
            var model = new XuLyHopDongModel.KhachHangModel();
            model.HopDongId = _idhopdong;
            return View(model);
        }
        [HttpPost]
        public ActionResult TaoTaiKhoan(XuLyHopDongModel.KhachHangModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVLeTanHopDong))
                return AccessDeniedView();           
            var khachhangrole = new CustomerRole();
            var customer = new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                Email = model.Email,             
                   Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
            };           
            var item = _customerService.GetCustomerByEmail(model.Email);
            if (item == null)
            {
                _customerService.InsertCustomer(customer);  
                
                
                //firstname,lastname
                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
                //mã hóa password
                if (!String.IsNullOrWhiteSpace(model.Password))
                {
                    var changePassRequest = new ChangePasswordRequest(model.Email, false, _customerSettings.DefaultPasswordFormat, model.Password);
                    var changePassResult = _customerRegistrationService.ChangePassword(changePassRequest);
                    if (!changePassResult.Success)
                    {
                        foreach (var changePassError in changePassResult.Errors)
                            ErrorNotification(changePassError);
                    }
                }
                //update khách hàng cho hop dong
                var _hopdong = _chonveService.GetHopDongById(model.HopDongId);

                if (_hopdong != null)
                {
                    if (_hopdong.KhachHangID > 0 && _hopdong.KhachHangID != customer.Id)
                    {
                        var khachhang = _customerService.GetCustomerById(_hopdong.KhachHangID);
                        var newitemrole = new CustomerRole();
                        foreach (var _item in khachhang.CustomerRoles)
                        {
                            if (_item.SystemName == SystemCustomerRoleNames.HTNhaXeManager)                            
                                newitemrole = _item;                             
                           
                        }
                        khachhang.CustomerRoles.Remove(newitemrole);
                        _customerService.UpdateCustomer(khachhang);
                    }
                    _hopdong.KhachHangID = customer.Id;
                    _chonveService.UpdateHopDong(_hopdong);
                }
                //customerrole
               
                var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
                var newCustomerRoles = new List<CustomerRole>();
                foreach (var customerRole in allCustomerRoles)
                {

                    if (customerRole.SystemName == SystemCustomerRoleNames.Registered)
                        newCustomerRoles.Add(customerRole);
                    if (customerRole.SystemName == SystemCustomerRoleNames.HTNhaXeManager)
                        newCustomerRoles.Add(customerRole);

                }
                foreach (var _customerRole in newCustomerRoles)
                {
                    customer.CustomerRoles.Add(_customerRole);
                }
                _customerService.UpdateCustomer(customer);
               //tạo văn phong với địa chỉ của nhà xe là trụ sở chính 
                var truso = new VanPhong();
                truso.NhaXeId = _hopdong.NhaXeID;
                var nhaxe = _nhaxeService.GetNhaXeById(_hopdong.NhaXeID);
                truso.TenVanPhong = nhaxe.TenNhaXe;
                truso.KieuVanPhongID =(int)ENKieuVanPhong.TruSo;                
                truso.DiaChiID = nhaxe.DiaChiID;
                _nhaxeService.InsertVanPhong(truso);
                // tao nhan vien thuoc van phong
                var nhanvien = new NhanVien();
                nhanvien.HoVaTen = nhaxe.TenNhaXe;
                nhanvien.Email = nhaxe.Email;
                nhanvien.CustomerID = customer.Id;
                nhanvien.KieuNhanVienID = (int)ENKieuNhanVien.QuanLy;           
                nhanvien.GioiTinhID =(int) ENGioiTinh.Nam;
                nhanvien.TrangThaiID = (int)ENTrangThaiNhanVien.DangLamViec;
                nhanvien.NgayBatDauLamViec = DateTime.Now;
               
                nhanvien.VanPhongID = truso.Id;
                nhanvien.NhaXeID = _hopdong.NhaXeID;
                nhanvien.DiaChiID = nhaxe.DiaChiID;
                _nhanvienService.Insert(nhanvien);
                  return RedirectToAction("ChiTietHopDong", new { id = model.HopDongId });               
            }
            return View();
        }       
        public ActionResult TaoTaiKhoanKhachHang()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVLeTanHopDong))
                return AccessDeniedView();
            var model = new XuLyHopDongModel.KhachHangModel();
            return View(model);
        }
        #endregion

        protected virtual void prepareHopDongKhachHang(XuLyHopDongModel model)
        {
            model.KhachHang = new XuLyHopDongModel.KhachHangModel();
            if (model.KhachHangID > 0)
            {
                var custommer = _customerService.GetCustomerById(model.KhachHangID);
                if (custommer != null)
                {
                    model.KhachHang.Id = custommer.Id;
                    model.KhachHang.Email = custommer.Email;
                    model.KhachHang.Fullname = custommer.GetFullName();
                }
              
            }
        }
        protected virtual string ValidateCustomerRoles(IList<CustomerRole> customerRoles)
        {
            if (customerRoles == null)
                throw new ArgumentNullException("customerRoles");

            //ensure a customer is not added to both 'Guests' and 'Registered' customer roles
            //ensure that a customer is in at least one required role ('Guests' and 'Registered')
            bool isInGuestsRole = customerRoles.FirstOrDefault(cr => cr.SystemName == SystemCustomerRoleNames.Guests) != null;
            bool isInRegisteredRole = customerRoles.FirstOrDefault(cr => cr.SystemName == SystemCustomerRoleNames.Registered) != null;
            if (isInGuestsRole && isInRegisteredRole)
                return "The customer cannot be in both 'Guests' and 'Registered' customer roles";
            if (!isInGuestsRole && !isInRegisteredRole)
                return "Add the customer to 'Guests' or 'Registered' customer role";

            //no errors
            return "";
        }
        
    }
}