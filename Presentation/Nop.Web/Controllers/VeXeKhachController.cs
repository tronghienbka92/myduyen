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
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Payments;


namespace Nop.Web.Controllers
{
    public class VeXeKhachController : BasePublicController
    {
        #region Khoi Tao
        private readonly IStateProvinceService _stateProvinceService;
        private readonly INhaXeService _nhaxeService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly ICustomerService _customerService;
        private readonly IChonVeService _chonveService;
        private readonly IDiaChiService _diachiService;
        private readonly INhanVienService _nhanvienService;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly CustomerSettings _customerSettings;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreService _storeService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IXeInfoService _xeinfoService;
        private readonly IHanhTrinhService _hanhtrinhService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IBenXeService _benxeService;
        private readonly IVeXeService _vexeService;
        private readonly AddressSettings _addressSettings;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IPhoiVeService _phoiveService;
        private readonly IOrderService _orderService;
        public VeXeKhachController(IStateProvinceService stateProvinceService,
            INhaXeService nhaxeService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IPictureService pictureService,
            ICustomerService customerService,
            IChonVeService chonveService,
            IDiaChiService diachiService,
            INhanVienService nhanvienService,
            IPermissionService permissionService,
            IDateTimeHelper dateTimeHelper,
            CustomerSettings customerSettings,
            DateTimeSettings dateTimeSettings,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerActivityService customerActivityService,
            IGenericAttributeService genericAttributeService,
            IStoreService storeService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IXeInfoService xeinfoService,
            IHanhTrinhService hanhtrinhService,
            IPriceFormatter priceFormatter,
            IBenXeService benxeService,
            IVeXeService vexeService,
            AddressSettings addressSettings,
            IAddressAttributeFormatter addressAttributeFormatter,
            IAddressService addressService,
            ICountryService countryService,
            IAddressAttributeParser addressAttributeParser,
            IAddressAttributeService addressAttributeService,
            IPhoiVeService phoiveService,
             IOrderService orderService

            )
        {
            this._phoiveService = phoiveService;
            this._addressAttributeParser = addressAttributeParser;
            this._addressAttributeService = addressAttributeService;
            this._stateProvinceService = stateProvinceService;
            this._nhaxeService = nhaxeService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._customerService = customerService;
            this._chonveService = chonveService;
            this._diachiService = diachiService;
            this._nhanvienService = nhanvienService;
            this._permissionService = permissionService;
            this._dateTimeHelper = dateTimeHelper;
            this._customerSettings = customerSettings;
            this._dateTimeSettings = dateTimeSettings;
            this._customerRegistrationService = customerRegistrationService;
            this._customerActivityService = customerActivityService;
            this._genericAttributeService = genericAttributeService;
            this._storeService = storeService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._xeinfoService = xeinfoService;
            this._hanhtrinhService = hanhtrinhService;
            this._priceFormatter = priceFormatter;
            this._benxeService = benxeService;
            this._vexeService = vexeService;
            this._addressSettings = addressSettings;
            this._addressAttributeFormatter = addressAttributeFormatter;
            this._addressService = addressService;
            this._countryService = countryService;
            this._orderService = orderService;
        }
        #endregion
        #region Common
        ActionResult ThanhCong()
        {
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        ActionResult Loi()
        {
            return Json(GetLabel("QuanLyPhoiVe.Loi"), JsonRequestBehavior.AllowGet);
        }
        ActionResult TrangThaiKhongHopLe()
        {
            return Json(GetLabel("QuanLyPhoiVe.TrangThaiKhongHopLe"), JsonRequestBehavior.AllowGet);
        }
        ActionResult KhongSoHuu()
        {
            return Json(GetLabel("QuanLyPhoiVe.KhongSoHuu"), JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        protected virtual IList<SelectListItem> GetEnumSelectList<T>(int valseleded)
        {
            return (Enum.GetValues(typeof(T)).Cast<int>().Select(e => new SelectListItem() { Text = _localizationService.GetResource(string.Format("Enums.{0}.{1}.{2}", typeof(T).Namespace, typeof(T).Name, Enum.GetName(typeof(T), e))), Value = e.ToString(), Selected = (e == valseleded) })).ToList();
        }
        [NonAction]
        protected virtual string GetLabel(string _name)
        {
            return _localizationService.GetResource(string.Format("ChonVe.NhaXe.{0}", _name));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetQuanHuyenByProvinceId(string ProvinceId,
            bool? addSelectStateItem, bool? addAsterisk)
        {
            //permission validation is not required here


            // This action method gets called via an ajax request
            if (String.IsNullOrEmpty(ProvinceId))
                throw new ArgumentNullException("ProvinceId");

            var quanhuyens = _diachiService.GetQuanHuyenByProvinceId(Convert.ToInt32(ProvinceId));
            var result = (from s in quanhuyens
                          select new { id = s.Id, name = s.Ten }).ToList();
            result.Insert(0, new { id = 0, name = GetLabel("QuanHuyen.SelectQuanHuyen") });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        protected virtual void SoDoGheXeToSoDoGheXeModel(SoDoGheXe nvfrom, LoaiXeModel.SoDoGheXeModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.TenSoDo = GetLabel(nvfrom.TenSoDo);
            nvto.UrlImage = nvfrom.TenSoDo;
            nvto.SoLuongGhe = nvfrom.SoLuongGhe;
            nvto.KieuXeId = nvfrom.KieuXeId;
            nvto.SoCot = nvfrom.SoCot;
            nvto.SoHang = nvfrom.SoHang;

        }


        [NonAction]
        private void DiaChiInfoPrepare(DiaChiInfoModel model)
        {
            var states = _stateProvinceService.GetStateProvincesByCountryId(NhaXesController.CountryID);
            if (states.Count > 0)
            {
                foreach (var s in states)
                {
                    model.AvailableStates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.ProvinceID) });
                }
                int ProvinceID = Convert.ToInt32(model.AvailableStates[0].Value);
                if (model.Id > 0 && model.ProvinceID > 0)
                {
                    ProvinceID = model.ProvinceID;
                }
                var quanhuyens = _diachiService.GetQuanHuyenByProvinceId(ProvinceID);
                model.AvailableQuanHuyens.Add(new SelectListItem { Text = GetLabel("QuanHuyen.SelectQuanHuyen"), Value = "0", Selected = (model.QuanHuyenID == 0) });
                foreach (var s in quanhuyens)
                {
                    model.AvailableQuanHuyens.Add(new SelectListItem { Text = s.Ten, Value = s.Id.ToString(), Selected = (s.Id == model.QuanHuyenID) });
                }
            }

        }
        [NonAction]

        void TuyenVeXeToMode(TuyenVeXe e, TuyenVeXeModel m)
        {
            m.Id = e.Id;
            m.Province1Id = e.Province1Id;
            m.TenTinhDi = e.Province1.Name;
            m.Province2Id = e.Province2Id;
            m.TenTinhDen = e.Province2.Name;
            m.PriceOld = e.PriceOld;
            m.PriceNew = e.PriceNew;
            m.PriceOldText = _priceFormatter.FormatPrice(m.PriceOld, true, false);
            m.PriceNewText = _priceFormatter.FormatPrice(m.PriceNew, true, false);
            m.SeName = e.GetSeName();
        }
        /// <summary>
        /// tim kiem thong tin diem xuat phat va diem den
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult DiaDiemSearch(string keyword)
        {
            //tim kiem thong tin ben xe
            var items = _vexeService.DiaDiemSearch(keyword);
            var diadiems = items.Select(c =>
            {
                return c.ToModel(_localizationService);
            }).ToList();
            return Json(diadiems, JsonRequestBehavior.AllowGet);
        }

        #endregion
        // GET: VeXeKhach
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult VeXeHome()
        {
            var model = new TimVeXeModel();
            model.khunggios = this.GetCVEnumSelectList<Nop.Core.Domain.Chonves.ENKhungGio>(_localizationService);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult VungVeHome()
        {
            var model = new TuyenVeXeHomeModel();
            model.TenTuyenXe1 = _stateProvinceService.GetStateProvinceById(TinhThanhConfig.HA_NOI).Name;
            model.tuyenxes1 = _vexeService.TuyenVeXeSearch(TinhThanhConfig.HA_NOI).Select(c =>
            {
                var item = new TuyenVeXeModel();
                TuyenVeXeToMode(c, item);
                return item;
            }).ToList();
            model.TenTuyenXe2 = _stateProvinceService.GetStateProvinceById(TinhThanhConfig.HO_CHI_MINH).Name;
            model.tuyenxes2 = _vexeService.TuyenVeXeSearch(TinhThanhConfig.HO_CHI_MINH).Select(c =>
            {
                var item = new TuyenVeXeModel();
                TuyenVeXeToMode(c, item);
                return item;
            }).ToList();
            model.TenTuyenXe3 = _stateProvinceService.GetStateProvinceById(TinhThanhConfig.DA_NANG).Name;
            model.tuyenxes3 = _vexeService.TuyenVeXeSearch(TinhThanhConfig.DA_NANG).Select(c =>
            {
                var item = new TuyenVeXeModel();
                TuyenVeXeToMode(c, item);
                return item;
            }).ToList();
            return PartialView(model);
        }

        public ActionResult TimVeXe(int khid, int ddid, string dt)
        {
            var model = new TimVeXeModel();
            model.DiemKhoiHanh.Id = khid;
            model.DiemDen.Id = ddid;
            model.NgayDi = Convert.ToDateTime(dt);
            var nguonvexes = _vexeService.VeXeSearch(model.NgayDi, null, model.DiemKhoiHanh.Id, model.DiemDen.Id, null, 20).Select(c =>
            {
                return c.ToModel(_priceFormatter);
            });
            model.NguoVeXes = nguonvexes.ToList();
            //lay nha xe theo thong tin cua nguon ve
            if (model.NhaXes.Count == 0)
            {
                model.NhaXes = model.NguoVeXes.Select(c => c.NhaXeInfo).Distinct().ToList();
            }
            return View(model);

        }
        /// <summary>
        /// chuan bi thong tin nha xe
        /// </summary>
        /// <param name="model"></param>
        void PrepareTimKiemModel(TimVeXeModel model)
        {
            if (model.DiemKhoiHanh.Id > 0)
                model.DiemKhoiHanh = _vexeService.GetDiaDiemById(model.DiemKhoiHanh.Id).ToModel(_localizationService);
            if (model.DiemDen.Id > 0)
                model.DiemDen = _vexeService.GetDiaDiemById(model.DiemDen.Id).ToModel(_localizationService);
        }
        public ActionResult TimKiem(int? khid, int? ddid, string dt, int? ktime)
        {
            var model = new TimVeXeModel();
            if (khid > 0)
                model.DiemKhoiHanh.Id = khid.GetValueOrDefault(0);
            if (ddid > 0)
                model.DiemDen.Id = ddid.GetValueOrDefault(0);
            PrepareTimKiemModel(model);
            model.NgayDi = Convert.ToDateTime(dt);
            model.KhungGioId = ktime.GetValueOrDefault(0);
            model.KhungGioIds = model.KhungGioId.ToString();
            var khunggioids = new List<int>();
            if (model.KhungGioId > 0)
                khunggioids.Add(model.KhungGioId);
            var nguonvexes = _vexeService.VeXeSearch(model.NgayDi, null, model.DiemKhoiHanh.Id, model.DiemDen.Id, khunggioids, 0).Select(c =>
            {
                return c.ToModel(_priceFormatter);
            });
            model.NguoVeXes = nguonvexes.ToList();
            //lay nha xe theo thong tin cua nguon ve
            model.NhaXes = new List<NguonVeXeModel.NhaXeBasicModel>();
            foreach (var nx in model.NguoVeXes.Select(c => c.NhaXeInfo).ToList())
            {
                if (!model.NhaXes.Exists(c => c.Id == nx.Id))
                {
                    model.NhaXes.Add(nx);
                }
            }
            return View(model);

        }
        [HttpPost]
        public ActionResult _KetQuaTimKiem(int? khid, int? ddid, long dt, string khunggioids, string nhaxeids, int sortid)
        {
            var model = new TimVeXeModel();
            if (khid > 0)
                model.DiemKhoiHanh.Id = khid.GetValueOrDefault(0);
            if (ddid > 0)
                model.DiemDen.Id = ddid.GetValueOrDefault(0);
            model.NgayDi = new DateTime(dt);
            List<int> arrnhaxeidselected = new List<int>();
            if (!String.IsNullOrEmpty(nhaxeids))
            {
                arrnhaxeidselected = nhaxeids.Split(',').Select(c =>
                {
                    return Convert.ToInt32(c);
                }).ToList();
            }
            var khunggioidsselected = new List<int>();
            if (!String.IsNullOrEmpty(khunggioids))
            {
                khunggioidsselected = khunggioids.Split(',').Select(c =>
                {
                    return Convert.ToInt32(c);
                }).ToList();
            }
            //lay thong tin 
            var nguonvexes = _vexeService.VeXeSearch(model.NgayDi, arrnhaxeidselected, model.DiemKhoiHanh.Id, model.DiemDen.Id, khunggioidsselected, 0).Select(c =>
            {

                return c.ToModel(_priceFormatter);
            });
            switch (sortid)
            {
                case 1:
                    {
                        model.NguoVeXes = nguonvexes.OrderBy(c => c.ThoiGianDi).ToList();
                        break;
                    }
                case 2:
                    {
                        model.NguoVeXes = nguonvexes.OrderByDescending(c => c.ThoiGianDi).ToList();
                        break;
                    }
                case 3:
                    {
                        model.NguoVeXes = nguonvexes.OrderBy(c => c.GiaVeMoi).ToList();
                        break;
                    }
                case 4:
                    {
                        model.NguoVeXes = nguonvexes.OrderByDescending(c => c.GiaVeMoi).ToList();
                        break;
                    }
            }

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult TimKiem(TimVeXeModel model)
        {
            PrepareTimKiemModel(model);
            //lay thong tin 
            var nguonvexes = _vexeService.VeXeSearch(model.NgayDi, null, model.DiemKhoiHanh.Id, model.DiemDen.Id, null, 0).Select(c =>
            {
                return c.ToModel(_priceFormatter);
            });
            model.NguoVeXes = nguonvexes.ToList();
            //lay nha xe theo thong tin cua nguon ve
            model.NhaXes = new List<NguonVeXeModel.NhaXeBasicModel>();
            foreach (var nx in model.NguoVeXes.Select(c => c.NhaXeInfo).ToList())
            {
                if (!model.NhaXes.Exists(c => c.Id == nx.Id))
                {
                    model.NhaXes.Add(nx);
                }
            }
            return View(model);
        }
        /// <summary>
        /// lay top tuyen ve xe de hien ra ngoai trang chu
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult TuyenVeXeTop()
        {
            var tuyenvexes = _vexeService.TuyenVeXeSearch(0, 0).Select(c =>
            {
                var item = new TuyenVeXeModel();
                TuyenVeXeToMode(c, item);
                return item;
            });
            return PartialView(tuyenvexes.ToList());
        }
        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult TuyenVeXeKhach(int TuyenVeXeId)
        {
            var tuyenxeinfo = _vexeService.GetTuyenVeXeById(TuyenVeXeId);
            var model = new TimVeXeModel();
            model.NgayDi = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            model.DiemKhoiHanh.Id = _vexeService.GetDiaDiemId(tuyenxeinfo.Province1Id, ENLoaiDiaDiem.Tinh);
            model.DiemDen.Id = _vexeService.GetDiaDiemId(tuyenxeinfo.Province2Id, ENLoaiDiaDiem.Tinh);
            model.KieuXeId = tuyenxeinfo.KieuXeId;
            //PrepareTimKiemModel(model);
            //int? khid, int? ddid, string dt,int? ktime
            return RedirectToAction("TimKiem", new { khid = model.DiemKhoiHanh.Id, ddid = model.DiemDen.Id, dt = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") });
        }
        #region Dat mua ve
        [AcceptVerbs(HttpVerbs.Get)]

        public ActionResult GetSoDoGheXeInfo(int NguonVeXeId, int ParentId, long NgayDi, int? TangIndex)
        {
            int nguonveid = NguonVeXeId;
            if (ParentId > 0)
                nguonveid = ParentId;
            //lấy thong tin nguồn xe            
            var nguonvexe = _hanhtrinhService.GetNguonVeXeById(nguonveid);
            if (nguonvexe == null)
                return new HttpUnauthorizedResult();

            var loaixe = _xeinfoService.GetById(nguonvexe.LoaiXeId);
            if (loaixe == null)
                return new HttpUnauthorizedResult();

            //var nhaxe = this.getCurrentNhaXe;
            var sodoghe = _xeinfoService.GetSoDoGheXeById(loaixe.SoDoGheXeID);
            var modelsodoghe = new LoaiXeModel.SoDoGheXeModel();
            modelsodoghe.PhanLoai = ENPhanLoaiPhoiVe.PHOI_VE;
            SoDoGheXeToSoDoGheXeModel(sodoghe, modelsodoghe);
            //Lấy thông tin ma tran
            var sodoghevitris = _xeinfoService.GetAllSoDoGheViTri(sodoghe.Id);
            var sodoghequytacs = _xeinfoService.GetAllSoDoGheXeQuyTac(loaixe.Id);

            modelsodoghe.MaTran = new int[modelsodoghe.SoHang, modelsodoghe.SoCot];
            modelsodoghe.PhoiVes1 = new LoaiXeModel.PhoiVeAdvanceModel[modelsodoghe.SoHang + 1, modelsodoghe.SoCot + 1];
            modelsodoghe.SoTang = 1;
            if (sodoghe.KieuXe == ENKieuXe.GiuongNam)
            {
                modelsodoghe.SoTang = 2;
                modelsodoghe.PhoiVes2 = new LoaiXeModel.PhoiVeAdvanceModel[modelsodoghe.SoHang + 1, modelsodoghe.SoCot + 1];
            }
            foreach (var s in sodoghevitris)
            {
                modelsodoghe.MaTran[s.y, s.x] = 1;
            }

            DateTime _ngaydi = new DateTime(NgayDi);
            if (sodoghequytacs != null && sodoghequytacs.Count > 0)
            {
                foreach (var s in sodoghequytacs)
                {
                    if (s.Tang == 1)
                    {

                        modelsodoghe.PhoiVes1[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsodoghe.PhoiVes1[s.y, s.x].KyHieu = s.Val;
                        if (s.y >= 1 && s.x >= 1)
                        {
                            //kiem tra thong tin vi tri phoi ve
                            modelsodoghe.PhoiVes1[s.y, s.x].Info = _phoiveService.GetPhoiVe(nguonveid, s, _ngaydi, true);
                            modelsodoghe.PhoiVes1[s.y, s.x].IsCurrentCustomer = true;
                            if (modelsodoghe.PhoiVes1[s.y, s.x].Info.TrangThai != ENTrangThaiPhoiVe.Huy && modelsodoghe.PhoiVes1[s.y, s.x].Info.TrangThai != ENTrangThaiPhoiVe.ConTrong)
                            {
                                if (modelsodoghe.PhoiVes1[s.y, s.x].Info.CustomerId != _workContext.CurrentCustomer.Id)
                                    modelsodoghe.PhoiVes1[s.y, s.x].IsCurrentCustomer = false;
                            }

                        }

                    }
                    else
                    {

                        modelsodoghe.PhoiVes2[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsodoghe.PhoiVes2[s.y, s.x].KyHieu = s.Val;
                        if (s.y >= 1 && s.x >= 1)
                        {
                            //kiem tra thong tin vi tri phoi ve
                            modelsodoghe.PhoiVes2[s.y, s.x].Info = _phoiveService.GetPhoiVe(nguonveid, s, _ngaydi, true);
                            modelsodoghe.PhoiVes2[s.y, s.x].IsCurrentCustomer = true;
                            if (modelsodoghe.PhoiVes2[s.y, s.x].Info.TrangThai != ENTrangThaiPhoiVe.Huy && modelsodoghe.PhoiVes2[s.y, s.x].Info.TrangThai != ENTrangThaiPhoiVe.ConTrong)
                            {
                                if (modelsodoghe.PhoiVes2[s.y, s.x].Info.CustomerId != _workContext.CurrentCustomer.Id)
                                    modelsodoghe.PhoiVes2[s.y, s.x].IsCurrentCustomer = false;
                            }
                        }
                    }
                }
            }
            //selected tab
            SaveSelectedTabIndex(TangIndex);
            return PartialView(modelsodoghe);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult KiemTraChoNgoi(int NguonVeXeId, int ParentId, long NgayDi, string KyHieuGhe, string Tang)
        {
            // This action method gets called via an ajax request
            if (String.IsNullOrEmpty(KyHieuGhe) || String.IsNullOrEmpty(Tang))
                throw new ArgumentNullException("KiemTraChoNgoi");
            if (Session["DAT_MUA_VE_XE_ID"] == null)
                return Loi();
            var item = new PhoiVe();
            item.NguonVeXeId = NguonVeXeId;
            //neu la ve con, thi lay ve cha lam phieu dat ve
            if (ParentId > 0)
            {
                item.NguonVeXeId = ParentId;
                item.NguonVeXeConId = NguonVeXeId;
            }

            item.NgayDi = new DateTime(NgayDi);
            var nguonvexe = _vexeService.GetNguonVeXeById(item.NguonVeXeId);
            item.SoDoGheXeQuyTacId = _vexeService.GetSoDoGheXeQuyTacID(nguonvexe.LoaiXeId, KyHieuGhe, Convert.ToInt32(Tang));
            if (item.SoDoGheXeQuyTacId > 0)
            {
                item.TrangThai = ENTrangThaiPhoiVe.DatCho;
                item.isChonVe = true; //giao dich nay cua chonve.vn 
                item.CustomerId = _workContext.CurrentCustomer.Id;
                item.SessionId = Session["DAT_MUA_VE_XE_ID"].ToString();
                item.GiaVeHienTai = nguonvexe.GiaVeHienTai;
                if (_phoiveService.DatVe(item))
                {
                    var model = nguonvexe.ToModel(_priceFormatter);
                    model.phoives = _phoiveService.GetPhoiVeDatChoBySession(Session["DAT_MUA_VE_XE_ID"]);
                    bool kq = _phoiveService.GetPhoiVeByNguonVe(item.NguonVeXeId, item.SessionId, item.CustomerId, item.NgayDi);
                    var nguonves = new List<NguonVeXeModel>();
                    if (kq)
                    {
                        foreach (var pv in model.phoives)
                        {
                            var result = new NguonVeXeModel();
                            if (string.IsNullOrEmpty(result.KyHieuGhe))
                                result.KyHieuGhe = pv.sodoghexequytac.Val;
                            else
                                result.KyHieuGhe += "," + pv.sodoghexequytac.Val;
                            result.TongTien += pv.GiaVeHienTai;
                            nguonves.Add(result);
                        }
                    }
                    return Json(nguonves.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            return Json("ERROR", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult HuyGheDatCho(int PhoiVeId)
        {
            if (Session["DAT_MUA_VE_XE_ID"] == null)
                return Loi();
            var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            //khong o trang thai dat cho hoac khac session hoac khac nguoi dang dat
            if (phoive.TrangThai != ENTrangThaiPhoiVe.DatCho)
                return Loi();
            if (phoive.SessionId != Session["DAT_MUA_VE_XE_ID"].ToString() || phoive.CustomerId != _workContext.CurrentCustomer.Id)
                return KhongSoHuu();

            _phoiveService.DeletePhoiVe(phoive);
            return ThanhCong();
        }
        [HttpPost]
        public ActionResult HuyChon(int NguonVeXeId, int ParentId, long NgayDi)
        {
            if (Session["DAT_MUA_VE_XE_ID"] == null)
                return Loi();
            _phoiveService.DeletePhoiVe(ParentId == 0 ? NguonVeXeId : ParentId, Session["DAT_MUA_VE_XE_ID"].ToString(), _workContext.CurrentCustomer.Id, new DateTime(NgayDi));
            return new NullJsonResult();
        }
        [NonAction]
        void PhoiVeToModel(PhoiVe e, PhoiVeModel m)
        {
            m.Id = e.Id;
            m.NguonVeXeId = e.NguonVeXeId;
            //neu co nguon ve xe con, tuc la khach hang dang chon tuyen con de dat ve
            //chuyen doi thong tin gia ve gia ve cua tuyen con
            m.GiaVe = e.getNguonVeXe().ProductInfo.Price;
            if (e.NguonVeXeConId > 0)
            {
                var nguonvecon = _vexeService.GetNguonVeXeById(e.NguonVeXeConId);
                m.GiaVe = nguonvecon.ProductInfo.Price;
            }
            m.GiaVeText = _priceFormatter.FormatPrice(m.GiaVe, true, false);
            m.NgayDi = e.NgayDi;
            m.TrangThaiId = e.TrangThaiId;
            m.CustomerId = e.CustomerId;
            m.SoDoGheXeQuyTacId = e.SoDoGheXeQuyTacId;
            m.KyHieuGhe = e.sodoghexequytac.Val;
            m.Tang = e.sodoghexequytac.Tang;
            m.isChonVe = e.isChonVe;
            m.NgayTao = e.NgayTao;
            m.NgayUpd = e.NgayUpd;
            m.SessionId = e.SessionId;

        }
        #endregion
        #region Nha xe
        public ActionResult DatMuaVeXe(int NguonVeXeId, long Ngay)
        {
            var item = _vexeService.GetNguonVeXeById(NguonVeXeId);
            if (item == null)
                return new HttpUnauthorizedResult();
            var model = item.ToModel(_priceFormatter);
            model.NgayDiTick = Ngay;
            Session["DAT_MUA_VE_XE_ID"] = Guid.NewGuid().ToString();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult GiuCho(int NguonVeXeId, int ParentId, long NgayDi)
        {
            if (Session["DAT_MUA_VE_XE_ID"] == null)
                return Loi();
            //giu cho 15 phut, va di toi trang thanh toan
            if (_phoiveService.GiuChoPhoiVe(ParentId == 0 ? NguonVeXeId : ParentId, Session["DAT_MUA_VE_XE_ID"].ToString(), _workContext.CurrentCustomer.Id, new DateTime(NgayDi)))
                return Json("OK", JsonRequestBehavior.AllowGet);
            return Loi();
        }

        #endregion
        #region Don dat & Thanh toan
        /// <summary>
        /// Thanh toan cac ve xe da chon, dua vao sesssion id de lay thong tin ve da chon mua       
        /// </summary>
        /// <returns></returns>
        public ActionResult VeXeThanhToan()
        {
            if (Session["DAT_MUA_VE_XE_ID"] == null)
                return RedirectToRoute("HomePage");
            //kiem tra dang nhap hay chua
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();
            //lay thong tin nguon ve xe, tu session id
            var model = new ThanhToanVeXeModel();
            model.MaXacThuc = "0";
            //thiet dat thong tin noi nhan
            var countries = new List<Nop.Core.Domain.Directory.Country>();
            countries.Add(_countryService.GetCountryById(NhaXesController.CountryID));

            var diachigiaohang = _workContext.CurrentCustomer.ShippingAddress;
            if (_workContext.CurrentCustomer.Addresses.Count > 0)
                diachigiaohang = _workContext.CurrentCustomer.Addresses.First();

            model.diachigiaohang.PrepareModel(
                  address: diachigiaohang,
                  excludeProperties: false,
                  addressSettings: _addressSettings,
                  localizationService: _localizationService,
                  stateProvinceService: _stateProvinceService,
                  diachiService: _diachiService,
                  addressAttributeFormatter: _addressAttributeFormatter,
                  loadCountries: () => countries);
            if (diachigiaohang == null)
            {
                model.diachigiaohang.FirstName = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                model.diachigiaohang.LastName = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
                model.diachigiaohang.FullName = string.Format("{0} {1}", model.diachigiaohang.FirstName, model.diachigiaohang.LastName);
                model.diachigiaohang.Email = _workContext.CurrentCustomer.Email;
                model.diachigiaohang.PhoneNumber = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
            }
            //lay thong tin ve xe
            model.phoiveinfos = _phoiveService.GetPhoiVeGiuChoBySession(Session["DAT_MUA_VE_XE_ID"]);

            //cho thanh toan qua lau, nen het han giu cho ve
            if (model.phoiveinfos.Count == 0)
                return RedirectToRoute("HomePage");
            model.nguonvexeinfo = _vexeService.GetNguonVeXeById(model.phoiveinfos[0].NguonVeXeId);
            model.nhaxeinfo = _nhaxeService.GetNhaXeById(model.nguonvexeinfo.NhaXeId);
            model.NgayDi = model.phoiveinfos[0].NgayDi;
            model.NgayDi = model.NgayDi.AddHours(model.nguonvexeinfo.ThoiGianDi.Hour).AddMinutes(model.nguonvexeinfo.ThoiGianDi.Minute);
            model.NgayVe = model.NgayDi.AddHours(Convert.ToDouble(model.nguonvexeinfo.LichTrinhInfo.SoGioChay));
            model.TongTien = decimal.Zero;
            model.KyHieuGhe = "";
            foreach (var pv in model.phoiveinfos)
            {
                if (string.IsNullOrEmpty(model.KyHieuGhe))
                    model.KyHieuGhe = pv.sodoghexequytac.Val;
                else
                    model.KyHieuGhe += "," + pv.sodoghexequytac.Val;
                model.TongTien += pv.GiaVeHienTai;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult VeXeThanhToan(ThanhToanVeXeModel model, FormCollection form)
        {
            if (Session["DAT_MUA_VE_XE_ID"] == null)
                return Loi();
            var customer = _workContext.CurrentCustomer;

            Address address = null;
            if (model.diachigiaohang.Id > 0)
                address = _addressService.GetAddressById(model.diachigiaohang.Id);
            address = model.diachigiaohang.ToEntity(address);
            address.CreatedOnUtc = DateTime.UtcNow;
            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;
            if (address.QuanHuyenId == 0)
                address.QuanHuyenId = null;
            if (address.Id > 0)
            {
                _addressService.UpdateAddress(address);
            }
            else
            {
                customer.Addresses.Add(address);
                _customerService.UpdateCustomer(customer);
            }
            int OrderId = 0;

            if (model.HinhThucThanhToan == "CHONVE")
            {
                // Đơn hàng do chonve.vn xử lý
                // chờ xác nhận điện thoại
                // return 
                vn.worldsms.wcf.APISMS apisms = new vn.worldsms.wcf.APISMS();

                var Sender = "CHONVE.VN";
                Random rdn = new Random();
                Session["maXacThuc"] = rdn.Next(1000, 9999);

                var Msg = "Ma xac thuc cua ban la: " + Session["maXacThuc"];

                var Phone = model.diachigiaohang.PhoneNumber;

                var Username = "chonve";

                var Password = "@#chonve";

                string result = apisms.PushMsg2Phone(Sender, Msg, Phone, Username, Password);
                int kq = Convert.ToInt32(result);
                if (kq == 1)
                {
                    return RedirectToAction("XacThucDatVe", "VeXeKhach");
                }
                else
                {
                    return RedirectToAction("VeXeThanhToan", "VeXeKhach");
                }
            }
            else
            {
                _phoiveService.ThanhToan(Session["DAT_MUA_VE_XE_ID"].ToString(), _workContext.CurrentCustomer.Id, address, out OrderId);
                //gửi đơn hàng cho ngân lượng.vn
                RequestInfo info = new RequestInfo();
                info.Merchant_id = CommonHelper.Merchant_Id;
                info.Merchant_password = CommonHelper.Merchant_Password;
                info.Receiver_email = "chonve.com.vn@gmail.com";
                info.cur_code = "vnd";
                info.bank_code = model.BankCode;
                info.Payment_method = model.HinhThucThanhToan;
                info.Order_code = OrderId.ToString();
                info.Total_amount = model.TongTien.ToString();
                info.time_limit = CommonHelper.Time_Limit;
                info.return_url = CommonHelper.Return_Url;
                info.cancel_url = CommonHelper.Cancel_Url;
                info.Buyer_fullname = model.diachigiaohang.FullName;
                info.Buyer_email = model.diachigiaohang.Email;
                info.Buyer_mobile = model.diachigiaohang.PhoneNumber;
                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseInfo result = objNLChecout.GetUrlCheckout(info, model.HinhThucThanhToan);

                if (result.Error_code == "00")
                {
                    Response.Redirect(result.Checkout_url);


                }
                else
                {
                    model.ErrorSentOrder = objNLChecout.GetErrorMessage(result.Error_code);
                    return View(model);

                }
            }
            return null;

        }
        public ActionResult XacThucDatVe()
        {
            if (Session["maXacThuc"] == null)
            {
                return RedirectToAction("VeXeThanhToan", "VeXeKhach");
            }
            var model = new ThanhToanVeXeModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult XacThucDatVe(ThanhToanVeXeModel model)
        {
            if (Session["maXacThuc"] == null)
            {
                return RedirectToAction("VeXeThanhToan", "VeXeKhach");
            }
            var customer = _workContext.CurrentCustomer;
            var countries = new List<Nop.Core.Domain.Directory.Country>();
            countries.Add(_countryService.GetCountryById(NhaXesController.CountryID));

            var diachigiaohang = _workContext.CurrentCustomer.ShippingAddress;
            if (_workContext.CurrentCustomer.Addresses.Count > 0)
                diachigiaohang = _workContext.CurrentCustomer.Addresses.First();

            model.diachigiaohang.PrepareModel(
                  address: diachigiaohang,
                  excludeProperties: false,
                  addressSettings: _addressSettings,
                  localizationService: _localizationService,
                  stateProvinceService: _stateProvinceService,
                  diachiService: _diachiService,
                  addressAttributeFormatter: _addressAttributeFormatter,
                  loadCountries: () => countries);
            if (diachigiaohang == null)
            {
                model.diachigiaohang.FirstName = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                model.diachigiaohang.LastName = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
                model.diachigiaohang.FullName = string.Format("{0} {1}", model.diachigiaohang.FirstName, model.diachigiaohang.LastName);
                model.diachigiaohang.Email = _workContext.CurrentCustomer.Email;
                model.diachigiaohang.PhoneNumber = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
            }
            model.phoiveinfos = _phoiveService.GetPhoiVeGiuChoBySession(Session["DAT_MUA_VE_XE_ID"]);
            int OrderId = 0;
            Address address = null;
            if (model.diachigiaohang.Id > 0)
                address = _addressService.GetAddressById(model.diachigiaohang.Id);
            address = model.diachigiaohang.ToEntity(address);
            address.CreatedOnUtc = DateTime.UtcNow;
            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;
            if (address.QuanHuyenId == 0)
                address.QuanHuyenId = null;

            if (model.MaXacThuc != null && model.MaXacThuc.Equals(Session["maXacThuc"].ToString()))
            {

                vn.worldsms.wcf.APISMS apisms = new vn.worldsms.wcf.APISMS();

                var phoive = _phoiveService.GetPhoiVeById(model.phoiveinfos[0].Id);

                var Sender = "CHONVE.VN";

                var Msg = "Chúc mừng " + model.diachigiaohang.FullName + " đã thanh toán thành công, mã vé của bạn là: " + phoive.MaVe;

                var Phone = address.PhoneNumber;

                var Username = "chonve";

                var Password = "@#chonve";

                string result = apisms.PushMsg2Phone(Sender, Msg, Phone, Username, Password);
               
                _phoiveService.ThanhToan(Session["DAT_MUA_VE_XE_ID"].ToString(), _workContext.CurrentCustomer.Id, address, out OrderId);
                SuccessNotification("Bạn đã thanh toán thành công");
                return RedirectToAction("ThanhToanThanhCong", "VeXeKhach");




            }

            int solannhap = 0;

            if (Session["SoLanXacThuc"] != null)
            {
                solannhap = Convert.ToInt32(Session["SoLanXacThuc"]);
            }
            solannhap++;
            if (solannhap >= 3)
            {

                return RedirectToAction("VeXeThanhToan", "VeXeKhach");
            }
            Session["SoLanXacThuc"] = solannhap;
            return View(model);
        }

        public ActionResult XacNhanDienThoai()
        {
            var model = new XacNhanDienThoaiModel();
            return View(model);
        }
        public ActionResult ThanhToanThanhCong()
        {
            int orderId = 0;
            var model = new ThanhToanVeXeModel();
            var _token = Request["token"];
            if (!string.IsNullOrWhiteSpace(_token))
            {
                RequestCheckOrder info = new RequestCheckOrder();
                info.Merchant_id = CommonHelper.Merchant_Id;
                info.Merchant_password = CommonHelper.Merchant_Password;
                info.Token = _token;
                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
                if (result.errorCode == "00")
                {
                    orderId = Convert.ToInt32(result.order_code);
                    model.HinhThucThanhToan = result.paymentMethod;
                    if (result.paymentMethod != "CHONVE")
                    {
                        var _order = _orderService.GetOrderById(orderId);
                        if (result.transactionStatus == "00")
                            _order.PaymentStatusId = (int)PaymentStatus.Paid;
                        else
                            _order.PaymentStatusId = (int)PaymentStatus.Authorized;
                        _orderService.UpdateOrder(_order);
                        var phoives = _phoiveService.GetPhoiVeByOrderId(orderId);
                        foreach (var item in phoives)
                        {
                            item.TrangThaiId = (int)ENTrangThaiPhoiVe.DaThanhToan;
                            _phoiveService.UpdatePhoiVe(item);
                        }
                    }
                    else
                    {
                        //update trang thai phoi vé về chưa thanh toán

                    }
                }
            }

            //thiet dat thong tin noi nhan
            var countries = new List<Nop.Core.Domain.Directory.Country>();
            countries.Add(_countryService.GetCountryById(NhaXesController.CountryID));

            var diachigiaohang = _workContext.CurrentCustomer.ShippingAddress;
            if (_workContext.CurrentCustomer.Addresses.Count > 0)
                diachigiaohang = _workContext.CurrentCustomer.Addresses.First();

            model.diachigiaohang.PrepareModel(
                  address: diachigiaohang,
                  excludeProperties: false,
                  addressSettings: _addressSettings,
                  localizationService: _localizationService,
                  stateProvinceService: _stateProvinceService,
                  diachiService: _diachiService,
                  addressAttributeFormatter: _addressAttributeFormatter,
                  loadCountries: () => countries);
            //lay thong tin ve xe
            var phoive = _phoiveService.GetPhoiVeByCustomer(_workContext.CurrentCustomer.Id);
            for (int i = 0; i < phoive.Count(); i++)
                orderId = phoive[i].OrderId;

            model.phoiveinfos = _phoiveService.GetPhoiVeByOrderId(orderId);
            //cho thanh toan qua lau, nen het han giu cho ve
            if (model.phoiveinfos.Count == 0)
                return RedirectToRoute("HomePage");
            model.nguonvexeinfo = _vexeService.GetNguonVeXeById(model.phoiveinfos[0].NguonVeXeId);
            model.nhaxeinfo = _nhaxeService.GetNhaXeById(model.nguonvexeinfo.NhaXeId);
            model.NgayDi = model.phoiveinfos[0].NgayDi;
            model.NgayDi = model.NgayDi.AddHours(model.nguonvexeinfo.ThoiGianDi.Hour).AddMinutes(model.nguonvexeinfo.ThoiGianDi.Minute);
            model.NgayVe = model.NgayDi.AddHours(Convert.ToDouble(model.nguonvexeinfo.LichTrinhInfo.SoGioChay));
            model.TongTien = decimal.Zero;
            model.KyHieuGhe = "";
            foreach (var pv in model.phoiveinfos)
            {
                if (string.IsNullOrEmpty(model.KyHieuGhe))
                    model.KyHieuGhe = pv.sodoghexequytac.Val;
                else
                    model.KyHieuGhe += "," + pv.sodoghexequytac.Val;
                model.TongTien += pv.GiaVeHienTai;
            }
            ViewBag.OrderId = orderId;
            return View(model);
        }
        #endregion

    }
}