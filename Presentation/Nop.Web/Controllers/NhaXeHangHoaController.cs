using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Tax;
using Nop.Services.Authentication;
using Nop.Services.Authentication.External;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Extensions;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.UI.Captcha;
using Nop.Web.Models.Common;
using Nop.Web.Models.Customer;
using WebGrease.Css.Extensions;
using Nop.Web.Models.NhaXes;
using Nop.Core.Data;
using Nop.Services.NhaXes;
using Nop.Core.Caching;
using Nop.Core.Domain.News;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Services.Chonves;
using Nop.Services.Security;
using Nop.Core.Domain.Security;
using System.Globalization;
using Nop.Services.Catalog;
using Nop.Web.Models.VeXeKhach;
using Nop.Core.Domain.Chonves;


namespace Nop.Web.Controllers
{
    public class NhaXeHangHoaController : BaseNhaXeController
    {
        #region Khoi Tao
        private readonly IStateProvinceService _stateProvinceService;
        private readonly INhaXeService _nhaxeService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly IPhieuGuiHangService _phieuguihangService;
        private readonly IHangHoaService _hanghoaService;
        private readonly ICustomerService _customerService;
        private readonly IDiaChiService _diachiService;
        private readonly INhanVienService _nhanvienService;
        private readonly IPermissionService _permissionService;
        private readonly IXeInfoService _xeinfoService;
        private readonly IHanhTrinhService _hanhtrinhService;
        private readonly IBenXeService _benxeService;
        private readonly INhaXeCustomerService _nhaxecustomerService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly CustomerSettings _customerSettings;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IGenericAttributeService _genericAttributeService;
        public NhaXeHangHoaController(IStateProvinceService stateProvinceService,
            INhaXeService nhaxeService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IPictureService pictureService,
             IPhieuGuiHangService phieuguihangService,
            IHangHoaService hanghoaService,
            ICustomerService customerService,
            IDiaChiService diachiService,
            INhanVienService nhanvienService,
            IPermissionService permissionService,
            IXeInfoService xeinfoService,
            IHanhTrinhService hanhtrinhService,
            IBenXeService benxeService,
            INhaXeCustomerService nhaxecustomerService,
            IPriceFormatter priceFormatter,
            CustomerSettings customerSettings,
            ICustomerRegistrationService customerRegistrationService,
            IGenericAttributeService genericAttributeService
            )
        {
            this._genericAttributeService = genericAttributeService;
            this._customerRegistrationService = customerRegistrationService;
            this._customerSettings = customerSettings;
            this._priceFormatter = priceFormatter;
            this._stateProvinceService = stateProvinceService;
            this._nhaxeService = nhaxeService;
            this._hanghoaService = hanghoaService;
            this._phieuguihangService = phieuguihangService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._customerService = customerService;
            this._diachiService = diachiService;
            this._nhanvienService = nhanvienService;
            this._permissionService = permissionService;
            this._xeinfoService = xeinfoService;
            this._hanhtrinhService = hanhtrinhService;
            this._benxeService = benxeService;
            this._nhaxecustomerService = nhaxecustomerService;
        }
        #endregion
         
        #region ham chung
        
        
      
        [NonAction]
        protected virtual void PhieuGuiHangModelToPhieuGuiHang(PhieuGuiHang nvto, PhieuGuiHangModel nvfrom)
        {
            nvto.VanPhongNhanId = nvfrom.VanPhongNhan.Id;
            nvto.NguoiKiemTraHangId = nvfrom.NguoiKiemTraHangId;
            nvto.DaThuCuoc = nvfrom.DaThuCuoc;
            nvto.GhiChu = nvfrom.GhiChu;
        }
        public virtual void HangHoaModelToHangHoa(PhieuGuiHangModel.HangHoaModel model, HangHoa hanghoa)
        {
            hanghoa.TenHangHoa = model.TenHangHoa;
            hanghoa.LoaiHangHoaId = model.LoaiHangHoaId;
            hanghoa.CanNang = model.CanNang;
            hanghoa.GiaTri = model.GiaTri;
            hanghoa.GiaCuoc = model.GiaCuoc;
            hanghoa.GhiChu = model.GhiChu;
            hanghoa.SoLuong = model.SoLuong;
            
        }
        
       
        [NonAction]
        protected virtual void PhieuGuiHangPrepareModel(int idphieugui, PhieuGuiHangModel model)
        {
            //idphieugui=0 là tạo phiếu, khác 0 là sửa phiếu

            //Danh sách văn phòng không chứa văn phòng hiện tại
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
            if (vanphongs.Count > 0)
            {
                foreach (var s in vanphongs)
                {
                    if (s.Id != _workContext.CurrentVanPhong.Id)
                        model.VanPhongs.Add(new SelectListItem { Text = s.TenVanPhong, Value = s.Id.ToString(), Selected = (s.Id == model.VanPhongNhan.Id) });
                }
            }
            // danh sách nhân viên trong văn phòng hiện tại
            var kiemtrahangs = _nhanvienService.GetAllByVanPhongId(_workContext.CurrentVanPhong.Id);
            if (kiemtrahangs.Count > 0)
            {
                foreach (var s in kiemtrahangs)
                {
                    model.NguoiKiemTraHangs.Add(new SelectListItem { Text = string.Format("{0}-{1}", s.HoVaTen, s.CMT_Id), Value = s.Id.ToString(), Selected = (s.Id == model.NguoiKiemTraHangId) });
                }
            }


        }
        
        NhaXeCustomer CapNhatKhachHang(string TenKhachHang,string SoDienThoai,string DiaChiLienHe,int KhachHangId=0)
        {
            if(KhachHangId>0)
            {
                var khachhangupd = _nhaxecustomerService.GetNhaXeCustomerById(KhachHangId);
                khachhangupd.HoTen = TenKhachHang;
                khachhangupd.DienThoai = SoDienThoai;
                khachhangupd.DiaChiLienHe = DiaChiLienHe;
                _nhaxecustomerService.UpdateNhaXeCustomer(khachhangupd);
                return khachhangupd;
            }
           
            //insert bang customernhaxe
            return _nhaxecustomerService.CreateNew(_workContext.NhaXeId, TenKhachHang, SoDienThoai, DiaChiLienHe);
        }
        #endregion
        #region Quản lý gửi hàng

        public ActionResult QLGuiHang()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            var model = new HoaDonListModel();
            model.NgayTao = DateTime.Now;
            //Danh sách văn phòng không chứa văn phòng hiện tại
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
            model.VanPhongs.Insert(0, new SelectListItem { Text = "Chọn văn phòng nhận", Value = "0" });
            if (vanphongs.Count > 0)
            {
                foreach (var s in vanphongs)
                {
                    if (s.Id != _workContext.CurrentVanPhong.Id)
                        model.VanPhongs.Add(new SelectListItem { Text = s.TenVanPhong, Value = s.Id.ToString(), Selected = (s.Id == model.VanPhongNhanId) });
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult QLGuiHang(DataSourceRequest command, HoaDonListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var items = _phieuguihangService.GetAllPhieuGuiHang(
                NhaXeId: _workContext.NhaXeId,
                vanphongguid: _workContext.CurrentVanPhong.Id,
                _maphieu: model.MaPhieu,
                _tennguoigui: model.TenNguoiGui,
                TinhTrangVanChuyenId: ENTinhTrangVanChuyen.ChuaVanChuyen,
                ngaytao: model.NgayTao,
                vanphongnhanid: model.VanPhongNhanId,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var hanghoas = _hanghoaService.GetAllHangHoaByPhieuGuiHangId(x.Id);
                    var m = x.ToModel(_localizationService, _priceFormatter, hanghoas);                            
                    return m;
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult PhieuGuiHangTao()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            var model = new PhieuGuiHangModel();

            PhieuGuiHangPrepareModel(0, model);
            model.NgayThanhToan = DateTime.Now;
            model.HangHoa.LoaiHangHoaId = (int)ENLoaiHangHoa.XopDeVo;
            model.HangHoa.LoaiHangHoas = this.GetCVEnumSelectList<ENLoaiHangHoa>(_localizationService,model.HangHoa.LoaiHangHoaId);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult PhieuGuiHangTao(PhieuGuiHangModel model, bool continueEditing)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                var phieugui = new PhieuGuiHang();
                //them nguoi gui
                var nguoigui = CapNhatKhachHang(model.NguoiGui.HoTen, model.NguoiGui.SoDienThoai, model.NguoiGui.DiaChi, model.NguoiGui.Id);
                //them nguoi nhan
                var nguoinhan = CapNhatKhachHang(model.NguoiNhan.HoTen, model.NguoiNhan.SoDienThoai, model.NguoiNhan.DiaChi, model.NguoiNhan.Id);
                //them phieu gui hàng               
                phieugui.NhaXeId = _workContext.NhaXeId;
                phieugui.NguoiGuiId = nguoigui.Id;
                phieugui.NguoiNhanId = nguoinhan.Id;
                phieugui.VanPhongGuiId = _workContext.CurrentVanPhong.Id;

                phieugui.NguoiTaoId = _workContext.CurrentNhanVien.Id;
                phieugui.TinhTrangVanChuyenId = (int)ENTinhTrangVanChuyen.ChuaVanChuyen;
                phieugui.NgayTao = DateTime.Now;
                phieugui.NgayUpdate = DateTime.Now;
                PhieuGuiHangModelToPhieuGuiHang(phieugui, model);
                if (model.DaThuCuoc)
                {
                    phieugui.NgayThanhToan = model.NgayThanhToan;
                    phieugui.NhanVienThuTienId = _workContext.CurrentNhanVien.Id;
                }
                    
                _phieuguihangService.InsertPhieuGuiHang(phieugui);
                //them hàng hóa
                var hanghoa = new HangHoa();
                HangHoaModelToHangHoa(model.HangHoa, hanghoa);
                hanghoa.PhieuGuiHangId = phieugui.Id;
                _hanghoaService.InsertHangHoa(hanghoa);
                SuccessNotification("Thêm mới phiếu gửi hàng thành công");
                return continueEditing ? RedirectToAction("PhieuGuiSua", new { id = phieugui.Id }) : RedirectToAction("QLGuiHang");

            }
            return View(model);

        }
        public ActionResult PhieuGuiSua(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var phieugui = _phieuguihangService.GetPhieuGuiById(id);
            if (phieugui == null || phieugui.TinhTrangVanChuyen == ENTinhTrangVanChuyen.Huy)
                //No manufacturer found with the specified id
                return RedirectToAction("QLGuiHang");
            var hanghoa = _hanghoaService.GetAllHangHoaByPhieuGuiHangId(id).ToList();
            var model = phieugui.ToModel(_localizationService, _priceFormatter, hanghoa);
            //prepare for edit
            PhieuGuiHangPrepareModel(id, model);            
            model.NgayThanhToan = DateTime.Now;
            model.HangHoa.LoaiHangHoas = this.GetCVEnumSelectList<ENLoaiHangHoa>(_localizationService,model.HangHoa.LoaiHangHoaId);
            return View(model);
        }
        public ActionResult PhieuGuiChiTiet(int Id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var phieugui = _phieuguihangService.GetPhieuGuiById(Id);
            if (phieugui == null || phieugui.TinhTrangVanChuyen == ENTinhTrangVanChuyen.Huy)
                //No manufacturer found with the specified id
                return RedirectToAction("QLGuiHang");
            var hanghoa = _hanghoaService.GetAllHangHoaByPhieuGuiHangId(Id).ToList();
            var model = phieugui.ToModel(_localizationService,_priceFormatter,hanghoa,true);
            return View(model);
        }
        

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult PhieuGuiSua(PhieuGuiHangModel model, bool continueEditing)
        {
         


            var phieugui = _phieuguihangService.GetPhieuGuiById(model.Id);
            if (phieugui == null || phieugui.TinhTrangVanChuyen == ENTinhTrangVanChuyen.Huy)
                //No manufacturer found with the specified id
                return RedirectToAction("QLGuiHang");

            if (ModelState.IsValid)
            {
                //them nguoi gui
                var nguoigui = CapNhatKhachHang(model.NguoiGui.HoTen, model.NguoiGui.SoDienThoai, model.NguoiGui.DiaChi, model.NguoiGui.Id);
                //them nguoi nhan
                var nguoinhan = CapNhatKhachHang(model.NguoiNhan.HoTen, model.NguoiNhan.SoDienThoai, model.NguoiNhan.DiaChi, model.NguoiNhan.Id);
                //update phieu gui hang
                PhieuGuiHangModelToPhieuGuiHang(phieugui, model);
                if(!model.DaThuCuoc)
                {
                    phieugui.NgayThanhToan = null;
                    phieugui.NhanVienNhanHangId = null;
                }
                else
                {
                    phieugui.NgayThanhToan = model.NgayThanhToan;
                    phieugui.NhanVienThuTienId = _workContext.CurrentNhanVien.Id;
                }
                _phieuguihangService.UpdatePhieuGuiHang(phieugui);
                // update hang hoa
                if (model.HangHoa.Id>0)
                {
                var hanghoa = _hanghoaService.GetHangHoaById(model.HangHoa.Id);
               
                    HangHoaModelToHangHoa(model.HangHoa, hanghoa);
                    _hanghoaService.UpdateHangHoa(hanghoa);
                }


                if (continueEditing)
                {
                    return RedirectToAction("PhieuGuiSua", new { id = phieugui.Id });
                }
                return RedirectToAction("QLGuiHang");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PhieuGuiXoa(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var phieugui = _phieuguihangService.GetPhieuGuiById(id);
            if (phieugui == null || phieugui.TinhTrangVanChuyen == ENTinhTrangVanChuyen.Huy)
                //No manufacturer found with the specified id
                return RedirectToAction("QLGuiHang");

            _phieuguihangService.DeletePhieuGuiHang(phieugui);

            return RedirectToAction("QLGuiHang");
        }
        [HttpPost]
        public ActionResult GetHangHoaInPhieuGui(DataSourceRequest command, int phieuguihangid)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var items = _hanghoaService.GetAllHangHoaByPhieuGuiHangId(phieuguihangid);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {                  
                    return x.ToModel(_localizationService,_priceFormatter);
                }),
                Total = items.Count()
            };

            return Json(gridModel);
        }
        [HttpPost]
        public ActionResult HangHoaInfoDelete(int id)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var hanghoa = _hanghoaService.GetHangHoaById(id);
            if (hanghoa == null)
                throw new ArgumentException("No LichTrinhGiaVe mapping found with the specified id");
            _hanghoaService.DeleteHangHoa(hanghoa);
            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult HangHoaInfoUpdate(PhieuGuiHangModel.HangHoaModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var item = _hanghoaService.GetHangHoaById(model.Id);
            if (item == null)
                throw new ArgumentException("No hang hoa mapping found with the specified id");
            HangHoaModelToHangHoa(model, item);
            _hanghoaService.UpdateHangHoa(item);
            return new NullJsonResult();

        }
        public ActionResult HangHoaInfo(int phieuguihangid)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            var model = new PhieuGuiHangModel.HangHoaModel();
            model.LoaiHangHoaId = (int)ENLoaiHangHoa.XopDeVo;
            model.MaPhieuGuiId = phieuguihangid;
            model.LoaiHangHoas = this.GetCVEnumSelectList<ENLoaiHangHoa>(_localizationService,model.LoaiHangHoaId);
            return View(model);
        }

        [HttpPost]
        public ActionResult HangHoaInfo(PhieuGuiHangModel.HangHoaModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            var hanghoa = new HangHoa();
            if (ModelState.IsValid)
            {
                HangHoaModelToHangHoa(model, hanghoa);
                hanghoa.PhieuGuiHangId = model.MaPhieuGuiId;
                _hanghoaService.InsertHangHoa(hanghoa);
                return Json("ok");

            }
            return Json("");

        }
        
        #endregion
        #region kho hàng
        public ActionResult KhoHang()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            var model = new HoaDonListModel();
            model.NgayTao = DateTime.Now;
            model.TrangThaiVanChuyenId = (int)ENTinhTrangVanChuyen.ChuaVanChuyen;
            //Danh sách văn phòng không chứa văn phòng hiện tại
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
            model.VanPhongs.Insert(0, new SelectListItem { Text = "Chọn văn phòng ", Value = "0" });
            if (vanphongs.Count > 0)
            {
                foreach (var s in vanphongs)
                {
                    if (s.Id != _workContext.CurrentVanPhong.Id)
                        model.VanPhongs.Add(new SelectListItem { Text = s.TenVanPhong, Value = s.Id.ToString(), Selected = (s.Id == model.VanPhongNhanId) });
                }
            }
            //get so luong phieu gui theo trạng thái van chuyen
            model.CountPhieuChuaGui = _phieuguihangService.GetAllPhieuGuiHang(NhaXeId: _workContext.NhaXeId, vanphongguid: _workContext.CurrentVanPhong.Id,
               TinhTrangVanChuyenId: ENTinhTrangVanChuyen.ChuaVanChuyen).Count();
            model.CountPhieuDaNhan = _phieuguihangService.GetAllPhieuGuiHang(NhaXeId: _workContext.NhaXeId,
             TinhTrangVanChuyenId: ENTinhTrangVanChuyen.NhanHang, vanphongnhanid: _workContext.CurrentVanPhong.Id).Count();
            return View(model);
        }
        [HttpPost]
        public ActionResult KhoHang(DataSourceRequest command, HoaDonListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            if (model.TrangThaiVanChuyenId == (int)ENTinhTrangVanChuyen.ChuaVanChuyen)
                model.VanPhongGuiId = _workContext.CurrentVanPhong.Id;
            if (model.TrangThaiVanChuyenId == (int)ENTinhTrangVanChuyen.NhanHang)
                model.VanPhongNhanId = _workContext.CurrentVanPhong.Id;
            var items = _phieuguihangService.GetAllPhieuGuiHang(
                NhaXeId: _workContext.NhaXeId,
                vanphongguid: model.VanPhongGuiId,
                _maphieu: model.MaPhieu,
                _tennguoigui: model.TenNguoiGui,
                TinhTrangVanChuyenId: (ENTinhTrangVanChuyen)model.TrangThaiVanChuyenId,
                vanphongnhanid: model.VanPhongNhanId,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var hanghoas = _hanghoaService.GetAllHangHoaByPhieuGuiHangId(x.Id);
                    var m = x.ToModel(_localizationService, _priceFormatter, hanghoas);
                    return m;
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult ListXeXuatBen(string ids)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            var model = new PhieuGuiHangModel();
            model.ChuoiPhieuGuiHangId = ids;
            var items = _nhaxeService.GetAllXeXuatBenByNgayDi(_workContext.NhaXeId,DateTime.Now).Distinct();
            if (items.Count() == 0)
                model.HasXeXuatBen = false;
            else
            {
                model.XeXuatBenId = items.First().Id;
                model.HasXeXuatBen = true;
                model.ListXeXuatBen = items.Select(c =>
                {
                    var item = new PhieuGuiHangModel.XeXuatBenModel();
                    item.Id = c.Id;
                    var nguonve = _hanhtrinhService.GetNguonVeXeById(c.NguonVeId);

                    item.TenXeXuatBen = string.Format("{0}-{1} - {2}-{3} -{4} ({5})", nguonve.ThoiGianDi.ToString("HH:mm"), nguonve.ThoiGianDen.ToString("HH:mm"),nguonve.LichTrinhInfo.HanhTrinhInfo.MoTa , c.NgayDi.ToString("dd/MM/yyyy"), c.xevanchuyen.TenXe, c.xevanchuyen.BienSo);
                    return item;
                }).ToList();
            }

            return View(model);

        }
        //gán hàng hóa theo xe: gán xe xuất bên id và tình trạng vận chuyển sang đang vận chuyển
        [HttpPost]
        public ActionResult ListXeXuatBen(PhieuGuiHangModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var phieuguihangs = new List<PhieuGuiHang>();
            if (model.ChuoiPhieuGuiHangId != null)
            {
                var ids = model.ChuoiPhieuGuiHangId
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                phieuguihangs.AddRange(_phieuguihangService.GetPhieuGuiHangsByIds(ids));
            }

            foreach (var item in phieuguihangs)
            {
                item.XeXuatBenId = model.XeXuatBenId;
                
                item.TinhTrangVanChuyenId = (int)ENTinhTrangVanChuyen.DangVanChuyen;
                _phieuguihangService.UpdatePhieuGuiHang(item);
            }
            return RedirectToAction("KhoHang");
        }
        // trả hàng cho khách: chuyển sang trạng thái kết thúc

        [HttpPost]
        public ActionResult TraHangChoKhach(string ids)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var phieuguihangs = new List<PhieuGuiHang>();
            if (ids != null)
            {
                var phieuguis = ids
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                phieuguihangs.AddRange(_phieuguihangService.GetPhieuGuiHangsByIds(phieuguis));
            }

            foreach (var item in phieuguihangs)
            {
               
                item.TinhTrangVanChuyenId = (int)ENTinhTrangVanChuyen.KetThuc;
                if (!item.DaThuCuoc)
                {
                    item.DaThuCuoc = true;
                    item.NgayThanhToan = DateTime.Now;
                    item.NhanVienThuTienId = _workContext.CurrentNhanVien.Id;
                }
                    
                _phieuguihangService.UpdatePhieuGuiHang(item);
            }
            return Json("");
        }
        #endregion
        #region nhận hàng

        public ActionResult QLNhanHang()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            var model = new HoaDonListModel();
            model.NgayTao = DateTime.Now;
            //Danh sách văn phòng không chứa văn phòng hiện tại
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
            model.VanPhongs.Insert(0, new SelectListItem { Text = "Chọn văn phòng ", Value = "0" });
            if (vanphongs.Count > 0)
            {
                foreach (var s in vanphongs)
                {
                    if (s.Id != _workContext.CurrentVanPhong.Id)
                        model.VanPhongs.Add(new SelectListItem { Text = s.TenVanPhong, Value = s.Id.ToString(), Selected = (s.Id == model.VanPhongNhanId) });
                }
            }
            model.XeXuatBens.Insert(0, new SelectListItem { Text = "Chọn xe ", Value = "0" });
            var xexuatbens = _phieuguihangService.GetAll(_workContext.NhaXeId,VanPhongNhanId: _workContext.CurrentVanPhong.Id,TinhTrangVanChuyenId:ENTinhTrangVanChuyen.DangVanChuyen).Select(c => c.XeXuatBen).Distinct();
            foreach(var item in xexuatbens)
            {
                var nguonve = _hanhtrinhService.GetNguonVeXeById(item.NguonVeId);

                var ThongTinChuyen = string.Format("{0}-{1}-{2}-{3} ({4})", nguonve.ThoiGianDi.ToString("HH:mm"),
               nguonve.ThoiGianDen.ToString("HH:mm"), item.NgayDi.ToString("dd/MM/yyyy"),
              item.xevanchuyen.TenXe, item.xevanchuyen.BienSo);
                model.XeXuatBens.Add(new SelectListItem { Text = ThongTinChuyen, Value = item.Id.ToString(), Selected = (item.Id == model.XeXuatBenId) });
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult QLNhanHang(DataSourceRequest command, HoaDonListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var items = _phieuguihangService.GetAllPhieuGuiHang(
                NhaXeId: _workContext.NhaXeId,
                vanphongguid: model.VanPhongGuiId,
                _maphieu: model.MaPhieu,
                _tennguoigui: model.TenNguoiGui,
                TinhTrangVanChuyenId: ENTinhTrangVanChuyen.DangVanChuyen,
                vanphongnhanid: _workContext.CurrentVanPhong.Id,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var hanghoas = _hanghoaService.GetAllHangHoaByPhieuGuiHangId(x.Id);
                    var m = x.ToModel(_localizationService, _priceFormatter, hanghoas);
                    
                    return m;
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }
        [HttpPost]
        public ActionResult NhanVienNhanHang(string ids)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var phieuguihangs = new List<PhieuGuiHang>();
            if (ids != null)
            {
                var phieuguis = ids
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                phieuguihangs.AddRange(_phieuguihangService.GetPhieuGuiHangsByIds(phieuguis));
            }
            var nhanvien = _workContext.CurrentNhanVien;
            foreach (var item in phieuguihangs)
            {

                item.TinhTrangVanChuyenId = (int)ENTinhTrangVanChuyen.NhanHang;
                item.NhanVienNhanHangId = nhanvien.Id;                
                _phieuguihangService.UpdatePhieuGuiHang(item);
            }
            return Json("");
        }
      
        #endregion
        #region lịch sử phiếu hàng
        public ActionResult HistoryPhieuGui()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            var model = new HoaDonListModel();
            model.TrangThaiVanChuyenId = (int)ENTinhTrangVanChuyen.DangVanChuyen;//để lấy điều kiện lọc đầu tiên là đang vận chuyển
            //Danh sách văn phòng không chứa văn phòng hiện tại
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
            model.VanPhongs.Insert(0, new SelectListItem { Text = "Chọn văn phòng ", Value = "0" });
            if (vanphongs.Count > 0)
            {
                foreach (var s in vanphongs)
                {
                    if (s.Id != _workContext.CurrentVanPhong.Id)
                        model.VanPhongs.Add(new SelectListItem { Text = s.TenVanPhong, Value = s.Id.ToString(), Selected = (s.Id == model.VanPhongNhanId) });
                }
            }
            //get so luong phieu gui theo trạng thái van chuyen
            model.CountPhieuDangGui = _phieuguihangService.GetAllPhieuGuiHang(NhaXeId: _workContext.NhaXeId, vanphongguid: _workContext.CurrentVanPhong.Id,
               TinhTrangVanChuyenId: ENTinhTrangVanChuyen.DangVanChuyen).Count();
            model.CountPhieuDaNhan = _phieuguihangService.GetAllPhieuGuiHang(NhaXeId: _workContext.NhaXeId,vanphongguid: _workContext.CurrentVanPhong.Id,
             TinhTrangVanChuyenId: ENTinhTrangVanChuyen.NhanHang).Count();
            model.CountPhieuKetThuc = _phieuguihangService.GetAllPhieuGuiHang(NhaXeId: _workContext.NhaXeId,
           TinhTrangVanChuyenId: ENTinhTrangVanChuyen.KetThuc, vanphongnhanid: _workContext.CurrentVanPhong.Id).Count();
           
            return View(model);
        }
        [HttpPost]
        public ActionResult HistoryPhieuGui(DataSourceRequest command, HoaDonListModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext,_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            if (model.TrangThaiVanChuyenId == (int)ENTinhTrangVanChuyen.DangVanChuyen || model.TrangThaiVanChuyenId == (int)ENTinhTrangVanChuyen.NhanHang)
                model.VanPhongGuiId = _workContext.CurrentVanPhong.Id;
            if (model.TrangThaiVanChuyenId == (int)ENTinhTrangVanChuyen.KetThuc)
                model.VanPhongNhanId = _workContext.CurrentVanPhong.Id;
            var items = _phieuguihangService.GetAllPhieuGuiHang(
                NhaXeId: _workContext.NhaXeId,
                vanphongguid:model.VanPhongGuiId,
                _tennguoigui:model.TenNguoiGui,
                _maphieu: model.MaPhieu,                
                TinhTrangVanChuyenId: (ENTinhTrangVanChuyen)model.TrangThaiVanChuyenId,               
                vanphongnhanid: model.VanPhongNhanId,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var hanghoas = _hanghoaService.GetAllHangHoaByPhieuGuiHangId(x.Id);
                    var m = x.ToModel(_localizationService, _priceFormatter, hanghoas);
                    return m;
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }
        #endregion
    }
}