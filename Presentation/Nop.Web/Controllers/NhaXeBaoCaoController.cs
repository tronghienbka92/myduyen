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
using Nop.Web.Models.NhaXeBaoCao;
using System.IO;
using Nop.Services.ExportImport;


namespace Nop.Web.Controllers
{
    public class NhaXeBaoCaoController : BaseNhaXeController
    {
        #region Khoi Tao
        private readonly IStateProvinceService _stateProvinceService;
        private readonly INhaXeService _nhaxeService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IDiaChiService _diachiService;
        private readonly INhanVienService _nhanvienService;
        private readonly IPermissionService _permissionService;
        private readonly IHanhTrinhService _hanhtrinhService;
        private readonly IVeXeService _vexeService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IPhoiVeService _phoiveService;
        private readonly IPhieuGuiHangService _phieuguihangService;
        private readonly IHangHoaService _hanghoaService;
        private readonly IXeInfoService _xeinfoService;
        private readonly IBaoCaoService _baocaoService;
        private readonly IGiaoDichKeVeXeService _giaodichkeveService;
        private readonly IExportManager _exportManager;
        public NhaXeBaoCaoController(IStateProvinceService stateProvinceService,
            INhaXeService nhaxeService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            ICustomerService customerService,
            IDiaChiService diachiService,
            INhanVienService nhanvienService,
            IPermissionService permissionService,
            IHanhTrinhService hanhtrinhService,
             IVeXeService vexeService,
            IPriceFormatter priceFormatter,
            IPhoiVeService phoiveService,
            IPhieuGuiHangService phieuguihangService,
            IHangHoaService hanghoaService,
            IXeInfoService xeinfoService,
             IGiaoDichKeVeXeService giaodichkeveService,
            IBaoCaoService baocaoService,
             IExportManager exportManager
            )
        {
            this._baocaoService = baocaoService;
            this._hanghoaService = hanghoaService;
            this._phieuguihangService = phieuguihangService;
            this._stateProvinceService = stateProvinceService;
            this._nhaxeService = nhaxeService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._customerService = customerService;
            this._diachiService = diachiService;
            this._nhanvienService = nhanvienService;
            this._permissionService = permissionService;
            this._hanhtrinhService = hanhtrinhService;
            this._vexeService = vexeService;
            this._priceFormatter = priceFormatter;
            this._phoiveService = phoiveService;
            this._xeinfoService = xeinfoService;
            this._giaodichkeveService = giaodichkeveService;
            this._exportManager = exportManager;

        }
        #endregion
        #region Common
        [NonAction]
        protected virtual void PrepareThongKeItemToModel(ThongKeItem model, ThongKeItem item)
        {
            model.Nhan = item.Nhan;
            model.NhanSapXep = item.NhanSapXep;
            model.ItemId = item.ItemId;
            model.ItemDataDate = item.ItemDataDate;
            model.GiaTri = item.GiaTri;
            model.SoLuong = item.SoLuong;
            model.GiaTri1 = item.GiaTri1;
            model.GiaTri2 = item.GiaTri2;
        }
        [NonAction]
        protected virtual void PrepareListQuy(BaoCaoNhaXeModel model)
        {
            if (DateTime.Now.Month < 4)
                model.QuyId = 1;
            else if (DateTime.Now.Month < 7)
                model.QuyId = 2;
            else if (DateTime.Now.Month < 10)
                model.QuyId = 3;
            else
                model.QuyId = 4;
            model.ListQuy = this.GetCVEnumSelectList<ENBaoCaoQuy>(_localizationService, model.QuyId);
        }

        [NonAction]
        protected virtual void PrepareListNgayThangNam(BaoCaoNhaXeModel model)
        {
            model.ThangId = DateTime.Now.Month;
            model.NamId = DateTime.Now.Year;
            for (int i = 2015; i <= DateTime.Now.Year; i++)
            {
                model.ListYear.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = (i == model.NamId) });
            }
            for (int i = 1; i <= 12; i++)
            {
                model.ListMonth.Add(new SelectListItem { Text = "Tháng " + i.ToString(), Value = i.ToString(), Selected = (i == model.ThangId) });
            }
        }

        [NonAction]
        protected virtual IList<SelectListItem> GetListLoaiThoiGian()
        {
            return this.GetCVEnumSelectList<ENBaoCaoLoaiThoiGian>(_localizationService, 0);
        }
        [NonAction]
        protected virtual IList<SelectListItem> GetListChuKyThoiGian()
        {
            return this.GetCVEnumSelectList<ENBaoCaoChuKyThoiGian>(_localizationService, 0);
        }

        void PrepareListVanPhongModel(BaoCaoNhaXeModel model)
        {
            var lts = _hanhtrinhService.GetAllLichTrinh(_workContext.NhaXeId);
            model.LichTrinhs = lts.Select(c => new SelectListItem
            {
                Text = string.Format("{0}-{1}",c.ThoiGianDi.ToString("HH:mm"),c.ThoiGianDen.ToString("HH:mm")),
                Value = c.Id.ToString(),
            }).ToList();
            if (this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao))
            {
                var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
                model.VanPhongs = vanphongs.Select(c => new SelectListItem
                {
                    Text = c.TenVanPhong,
                    Value = c.Id.ToString(),
                }).ToList();
            }
            else
            {
                SelectListItem item = new SelectListItem();
                item.Text = _workContext.CurrentVanPhong.TenVanPhong;
                item.Value = _workContext.CurrentVanPhong.Id.ToString();
                item.Selected = true;
                model.VanPhongs.Add(item);
            }

        }

        void PrepareListXeModel(BaoCaoNhaXeModel model)
        {
            var xevanchuyen = _xeinfoService.GetAllXeVanChuyenByNhaXeId(_workContext.NhaXeId);
            model.Xe = xevanchuyen.Select(c => new SelectListItem
            {
                Text = c.BienSo,
                Value = c.Id.ToString(),
            }).ToList();
        }
        #endregion
        #region bao cao doanh thu

        public ActionResult BaoCaoDoanhThu()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var modeldoanhthu = new BaoCaoNhaXeModel();
            modeldoanhthu.ListLoai1 = GetListChuKyThoiGian();
            PrepareListNgayThangNam(modeldoanhthu);
            return View(modeldoanhthu);
        }

        [HttpPost]
        public ActionResult BaoCaoDoanhThu(DataSourceRequest command, BaoCaoNhaXeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();

            var items = _phoiveService.GetAllPhoiVe(model.ThangId, model.NamId, _workContext.NhaXeId, (ENBaoCaoChuKyThoiGian)model.Loai1Id);
            ENBaoCaoChuKyThoiGian loaiid = (ENBaoCaoChuKyThoiGian)model.Loai1Id;
            var doanhthus = items.Select(c =>
            {
                var _doanhthu = new BaoCaoNhaXeModel.BaoCaoDoanhThuModel();
                PrepareThongKeItemToModel(_doanhthu, c);
                switch (loaiid)
                {
                    case ENBaoCaoChuKyThoiGian.HangNgay:
                        {
                            _doanhthu.ThoiGian = string.Format("{0}/{1}/{2}", _doanhthu.Nhan, model.ThangId, model.NamId);
                            break;
                        }
                    case ENBaoCaoChuKyThoiGian.HangThang:
                        {
                            _doanhthu.ThoiGian = string.Format("{0}/{1}", _doanhthu.Nhan, model.NamId);
                            break;
                        }
                    case ENBaoCaoChuKyThoiGian.HangNam:
                        {
                            _doanhthu.ThoiGian = _doanhthu.Nhan;
                            break;
                        }
                }
                _doanhthu.TongDoanhThu = c.GiaTri;
                _doanhthu.DoanhThuChonVe = c.GiaTri1;
                _doanhthu.DoanhThuNhaXe = c.GiaTri2;
                return _doanhthu;
            }).ToList();


            var gridModel = new DataSourceResult
            {
                Data = doanhthus,
                Total = doanhthus.Count
            };

            return Json(gridModel);
        }
        public ActionResult _ChiTietDoanhThu(string thoigian)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.NgayBan = thoigian;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ChiTietDoanhThu(DataSourceRequest command, string thoigian)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            DateTime _ngaydi = Convert.ToDateTime(thoigian);
            var items = _phoiveService.GetDetailDoanhThu(_workContext.NhaXeId, _ngaydi).Select(c => new BaoCaoNhaXeModel.KhachHangMuaVeModel
            {
                CustomerId = c.CustomerId,
                NguonVeXeId = c.NguonVeXeId,
                KyHieuGhe = c.KyHieuGhe,
                isChonVe = c.isChonVe,
                GiaVe = c.GiaVe,
                NgayDi = c.NgayDi,
                SoDienThoai = c.SoDienThoai,
                TenKhachHang = c.TenKhachHang,
                ThongTinChuyenDi = c.ThongTinChuyenDi,

            }).ToList();

            var gridModel = new DataSourceResult
            {
                Data = items,
                Total = items.Count
            };

            return Json(gridModel);
        }


        public ActionResult ThongKeDoanhThu()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var modeldoanhthu = new BaoCaoNhaXeModel();
            modeldoanhthu.ListLoai1 = GetListChuKyThoiGian();
            PrepareListNgayThangNam(modeldoanhthu);
            return View(modeldoanhthu);
        }
        [HttpPost]
        public ActionResult BieuDoDoanhThu(DataSourceRequest command, BaoCaoNhaXeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var items = _phoiveService.GetAllPhoiVe(model.ThangId, model.NamId, _workContext.NhaXeId, (ENBaoCaoChuKyThoiGian)model.Loai1Id);
            return Json(items);
        }

        #endregion
        #region Doanh thu theo xe
        public ActionResult DoanhThuBanVeTheoXe()
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.TuNgay = DateTime.Now.AddMonths(-1);
            model.DenNgay = DateTime.Now;
            PrepareListXeModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult DoanhThuBanVeTheoXe(DataSourceRequest command, DateTime tuNgay, DateTime denNgay, int XeId)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var items = _phoiveService.GetDoanhThuBanVeTungXeTheoNgay(tuNgay, denNgay, _workContext.NhaXeId, XeId);
            var doanhthus = items.Select(c =>
            {
                var model = new BaoCaoNhaXeModel.BaoCaoDoanhThuXeTungNgayModel();
                model.NgayBan = c.Nhan;
                model.TongDoanhThu = c.GiaTri;
                model.ThongTinChuyenDi = c.ThongTinChuyenDi;
                model.NgayBan = c.ItemDataDate.ToString("yyyy-MM-dd");
                model.SoLuong = c.SoLuong;
                return model;
            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = doanhthus,
                Total = doanhthus.Count
            };
            return Json(gridModel);
        }

        public ActionResult _ChiTietDoanhThuTheoXe(int XeId, string NgayBan)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel.BaoCaoDoanhThuXeTungNgayModel();
            model.XeId = XeId;
            model.NgayBan = NgayBan;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _ChiTietDoanhThuTheoXe(DataSourceRequest command, int XeId, string NgayBan)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            DateTime _NgayBan = Convert.ToDateTime(NgayBan);
            var items = _phoiveService.GetDetailDoanhThuBanVeTungXeTheoNgay(_NgayBan, _workContext.NhaXeId, XeId).Select(c =>
            {

                var model = new BaoCaoNhaXeModel.KhachHangMuaVeModel();
                model.CustomerId = c.CustomerId;
                model.NguonVeXeId = c.NguonVeXeId;
                model.KyHieuGhe = c.KyHieuGhe;
                model.isChonVe = c.isChonVe;
                model.GiaVe = c.GiaVe;
                model.TrangThaiPhoiVeId = c.TrangThaiPhoiVeId;
                if (c.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
                    model.TrangThaiPhoiVeText = "Chưa thanh toán";
                if (c.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
                    model.TrangThaiPhoiVeText = "Đã thanh toán";
                model.NgayDi = c.NgayDi;
                model.SoDienThoai = c.SoDienThoai;
                model.TenKhachHang = c.TenKhachHang;
                model.ThongTinChuyenDi = c.ThongTinChuyenDi;
                return model;

            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = items,
                Total = items.Count
            };

            return Json(gridModel);
        }
        #endregion
        #region doanh thu theo nhan vien
        public ActionResult DoanhThuNhanvien()
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var modeldoanhthu = new BaoCaoNhaXeModel();
            modeldoanhthu.TuNgay = DateTime.Now.AddMonths(-1);
            modeldoanhthu.DenNgay = DateTime.Now;
            PrepareListVanPhongModel(modeldoanhthu);
            PrepareListXeModel(modeldoanhthu);
            return View(modeldoanhthu);
        }
        [HttpPost]
        public ActionResult DoanhThuBanVeTheoNgay(DataSourceRequest command, BaoCaoNhaXeModel model)
        {

            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var items = _phoiveService.GetDoanhThuBanVeTheoNgay(model.TuNgay, model.DenNgay, _workContext.NhaXeId, model.VanPhongId,model.LichTrinhId,model.ChangId,model.XeId);
            var doanhthus = items.Select(c =>
            {
                var _doanhthu = new BaoCaoNhaXeModel.BaoCaoDoanhThuNhanVienModel();
                PrepareThongKeItemToModel(_doanhthu, c);
                _doanhthu.NgayBan = c.Nhan;
                _doanhthu.TongDoanhThu = c.GiaTri;
                return _doanhthu;
            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = doanhthus,
                Total = doanhthus.Count
            };
            return Json(gridModel);
        }
        [HttpPost]
        public ActionResult DoanhThuBanVeTheoNhanVien(DataSourceRequest command, int VanPhongId, string NgayBan)
        {

            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            DateTime ngayban = Convert.ToDateTime(NgayBan);
            var items = _phoiveService.GetDoanhThuBanVeTheoNhanVien(_workContext.NhaXeId, VanPhongId, ngayban);
            var doanhthus = items.Select(c =>
            {
                var _doanhthu = new BaoCaoNhaXeModel.BaoCaoDoanhThuNhanVienModel();
                PrepareThongKeItemToModel(_doanhthu, c);
                _doanhthu.NhanVienId = c.ItemId;
                _doanhthu.NgayBan = NgayBan;
                _doanhthu.TenNhanVien = _nhanvienService.GetById(_doanhthu.NhanVienId).HoVaTen;
                _doanhthu.TongDoanhThu = c.GiaTri;
                return _doanhthu;
            }).ToList();


            var gridModel = new DataSourceResult
            {
                Data = doanhthus,
                Total = doanhthus.Count
            };

            return Json(gridModel);
        }
        [HttpPost]
        public ActionResult DoanhThuBanVeTheoTrangThai(DataSourceRequest command, int VanPhongId, string NgayBan, int NhanvienId)
        {

            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            DateTime ngayban = Convert.ToDateTime(NgayBan);
            var items = _phoiveService.GetDoanhThuBanVeTheoTrangThai(_workContext.NhaXeId, VanPhongId, ngayban, NhanvienId);
            var doanhthus = items.Select(c =>
            {
                var _doanhthu = new BaoCaoNhaXeModel.BaoCaoDoanhThuNhanVienModel();
                PrepareThongKeItemToModel(_doanhthu, c);
                _doanhthu.NhanVienId = NhanvienId;
                _doanhthu.NgayBan = NgayBan;
                _doanhthu.TrangThaiPhoiVeId = c.TrangThaiPhoiVeId;
                if (_doanhthu.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
                    _doanhthu.TrangThaiPhoiVeText = "Đã thanh toán";
                if (_doanhthu.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
                    _doanhthu.TrangThaiPhoiVeText = "Chưa thanh toán";
                _doanhthu.TenNhanVien = _nhanvienService.GetById(_doanhthu.NhanVienId).HoVaTen;
                _doanhthu.TongDoanhThu = c.GiaTri;
                return _doanhthu;
            }).ToList();


            var gridModel = new DataSourceResult
            {
                Data = doanhthus,
                Total = doanhthus.Count
            };

            return Json(gridModel);
        }
        public ActionResult _ChiTietDoanhThuNhanVien(int NhanVienId, string NgayBan)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel.BaoCaoDoanhThuNhanVienModel();
            model.NhanVienId = NhanVienId;
            model.NgayBan = NgayBan;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _ChiTietDoanhThuNhanVien(DataSourceRequest command, int NhanVienId, string NgayBan)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            DateTime _NgayBan = Convert.ToDateTime(NgayBan);
            var items = _phoiveService.GetDetailDoanhThu(_workContext.NhaXeId, _NgayBan, NhanVienId).Select(c =>
            {
                var model = new BaoCaoNhaXeModel.KhachHangMuaVeModel();
                model.CustomerId = c.CustomerId;
                model.NguonVeXeId = c.NguonVeXeId;
                model.KyHieuGhe = c.KyHieuGhe;
                model.isChonVe = c.isChonVe;
                model.GiaVe = c.GiaVe;
                model.TrangThaiPhoiVeId = c.TrangThaiPhoiVeId;
                if (c.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
                    model.TrangThaiPhoiVeText = "Chưa thanh toán";
                if (c.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
                    model.TrangThaiPhoiVeText = "Đã thanh toán";
                model.NgayDi = c.NgayDi;
                model.SoDienThoai = c.SoDienThoai;
                model.TenKhachHang = c.TenKhachHang;
                model.ThongTinChuyenDi = c.ThongTinChuyenDi;
                return model;

            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = items,
                Total = items.Count
            };

            return Json(gridModel);
        }
        #endregion
        #region thong ke theo hanh trinh
        public ActionResult DoanhThuTuyen()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var modeldoanhthu = new BaoCaoNhaXeModel();

            modeldoanhthu.ListLoai2 = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = string.Format("{0} ({1})", c.MoTa, c.MaHanhTrinh);
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            var _item = new SelectListItem();
            _item.Text = "Chọn hành trình";
            _item.Value = "0";
            modeldoanhthu.ListLoai2.Insert(0, _item);
            modeldoanhthu.ListLoai1 = GetListLoaiThoiGian();
            PrepareListNgayThangNam(modeldoanhthu);
            PrepareListQuy(modeldoanhthu);
            return View(modeldoanhthu);
        }
        public ActionResult BieuDoDoanhThuTuyen(DataSourceRequest command, BaoCaoNhaXeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var ListItem = new List<ThongKeItem>();
            if (model.Loai2Id == 0)
            {
                ListItem = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId)
                .Select(c =>
                {
                    var _doanhthu = new ThongKeItem();
                    _doanhthu.Nhan = c.MoTa;
                    var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(c.Id, _workContext.NhaXeId).ToList();
                    int _sl;
                    _doanhthu.GiaTri = _phoiveService.DoanhThuTuyen(lichtrinhs.Select(lt => lt.Id).ToList(), model.ThangId, model.NamId, (ENBaoCaoQuy)model.QuyId, (ENBaoCaoLoaiThoiGian)model.Loai1Id, out _sl);
                    _doanhthu.SoLuong = _sl;
                    return _doanhthu;
                }).ToList();
            }
            else
            {

                var _DiemDons = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(model.Loai2Id).Where(c => c.ThuTu > 0).OrderBy(c => c.ThuTu).Select(c => c.diemdon).ToList();

                foreach (var item in _DiemDons)
                {
                    var _doanhthu = new ThongKeItem();
                    _doanhthu.Nhan = item.TenDiemDon;
                    var _nguonveids = _hanhtrinhService.GetAllNguonVeXe(0, 0, model.Loai2Id, 0, item.Id).Select(c => c.Id).ToList();
                    int _sl;
                    _doanhthu.GiaTri = _phoiveService.DoanhThuTuyenCon(_nguonveids, model.ThangId, model.NamId, (ENBaoCaoQuy)model.QuyId, (ENBaoCaoLoaiThoiGian)model.Loai1Id, out _sl);
                    _doanhthu.SoLuong = _sl;
                    ListItem.Add(_doanhthu);
                }
            }
            return Json(ListItem.ToList());
        }
        public ActionResult DoanhThuLichTrinh()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var modeldoanhthu = new BaoCaoNhaXeModel();
            modeldoanhthu.ListLoai1 = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = string.Format("{0} ({1})", c.MoTa, c.MaHanhTrinh);
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            modeldoanhthu.ListLoai2 = GetListLoaiThoiGian();
            PrepareListNgayThangNam(modeldoanhthu);
            PrepareListQuy(modeldoanhthu);

            return View(modeldoanhthu);
        }
        public ActionResult BieuDoDoanhThuLichTrinh(DataSourceRequest command, BaoCaoNhaXeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(model.Loai1Id, _workContext.NhaXeId).Select(c =>
                {
                    var _doanhthu = new ThongKeItem();
                    _doanhthu.Nhan = string.Format("{0}-{1}", c.ThoiGianDi.ToShortTimeString(), c.ThoiGianDen.ToShortTimeString());
                    int _sl;
                    _doanhthu.GiaTri = _phoiveService.DoanhThuLichTrinh(c.Id, model.ThangId, model.NamId, (ENBaoCaoQuy)model.QuyId, (ENBaoCaoLoaiThoiGian)model.Loai2Id, out _sl);
                    _doanhthu.SoLuong = _sl;
                    return _doanhthu;
                }).ToList();


            return Json(lichtrinhs);


        }
        public ActionResult DoanhThuVanPhong()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var modeldoanhthu = new BaoCaoNhaXeModel();
            modeldoanhthu.ListLoai1 = GetListLoaiThoiGian();
            PrepareListNgayThangNam(modeldoanhthu);
            PrepareListQuy(modeldoanhthu);

            return View(modeldoanhthu);
        }
        public ActionResult BieuDoDoanhThuVanPhong(DataSourceRequest command, BaoCaoNhaXeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId).Select(c =>
                {
                    var _doanhthu = new ThongKeItem();
                    _doanhthu.Nhan = c.TenVanPhong;
                    int _sl;
                    var listnhavien = _nhanvienService.GetAllByVanPhongId(c.Id);
                    _doanhthu.GiaTri = _phoiveService.DoanhThuVanPhong(listnhavien.Select(nv => nv.Id).ToList(), model.ThangId, model.NamId, (ENBaoCaoQuy)model.QuyId, (ENBaoCaoLoaiThoiGian)model.Loai1Id, out _sl);
                    _doanhthu.SoLuong = _sl;
                    return _doanhthu;
                }).ToList();

            return Json(vanphongs);
        }
        #endregion
        #region Báo cáo doanh thu theo tuyến
        public ActionResult BaoCaoDoanhThuTuyen()
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.TuNgay = DateTime.Now.AddMonths(-1);
            model.DenNgay = DateTime.Now;
            model.ListLoai2 = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = string.Format("{0} ({1})", c.MoTa, c.MaHanhTrinh);
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            var _item = new SelectListItem();
            _item.Text = "Chọn hành trình";
            _item.Value = "0";
            model.ListLoai2.Insert(0, _item);
            return View(model);
        }
        [HttpPost]
        public ActionResult BaoCaoDoanhThuTuyen(DataSourceRequest command, DateTime tuNgay, DateTime denNgay, int HanhTrinhId)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(HanhTrinhId, _workContext.NhaXeId).ToList();
            var items = _phoiveService.GetDoanhThuTheoTuyen(tuNgay, denNgay, lichtrinhs.Select(lt => lt.Id).ToList());

            var doanhthus = items.Select(c =>
            {
                var model = new BaoCaoNhaXeModel.BaoCaoDoanhThuXeTungNgayModel();
                model.NgayBan = c.Nhan;
                model.TongDoanhThu = c.GiaTri;
                model.NgayBan = c.ItemDataDate.ToString("yyyy-MM-dd");
                model.SoLuong = c.SoLuong;
                return model;
            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = doanhthus,
                Total = doanhthus.Count
            };
            return Json(gridModel);
        }
        public ActionResult _ChiTietDoanhThuTheoChang(int NguonVeId, string NgayBan)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel.BaoCaoDoanhThuNhanVienModel();
            model.NguonVeId = NguonVeId;
            model.NgayBan = NgayBan;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _ChiTietDoanhThuTheoChang(DataSourceRequest command, int NhanVienId, string NgayBan)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            DateTime _NgayBan = Convert.ToDateTime(NgayBan);
            var items = _phoiveService.GetDetailDoanhThu(_workContext.NhaXeId, _NgayBan, NhanVienId).Select(c =>
            {
                var model = new BaoCaoNhaXeModel.KhachHangMuaVeModel();
                model.CustomerId = c.CustomerId;
                model.NguonVeXeId = c.NguonVeXeId;
                model.KyHieuGhe = c.KyHieuGhe;
                model.isChonVe = c.isChonVe;
                model.GiaVe = c.GiaVe;
                model.TrangThaiPhoiVeId = c.TrangThaiPhoiVeId;
                if (c.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
                    model.TrangThaiPhoiVeText = "Chưa thanh toán";
                if (c.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
                    model.TrangThaiPhoiVeText = "Đã thanh toán";
                model.NgayDi = c.NgayDi;
                model.SoDienThoai = c.SoDienThoai;
                model.TenKhachHang = c.TenKhachHang;
                model.ThongTinChuyenDi = c.ThongTinChuyenDi;
                return model;

            }).ToList();
            var gridModel = new DataSourceResult
            {
                Data = items,
                Total = items.Count
            };

            return Json(gridModel);
        }

        #endregion
        #region Bao cao ky gui hang hoa
        public ActionResult DoanhThuKyGuiHangNgay()
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();
            var modeldoanhthu = new BaoCaoNhaXeModel();
            modeldoanhthu.TuNgay = DateTime.Now.AddMonths(-1);
            modeldoanhthu.DenNgay = DateTime.Now;
            PrepareListVanPhongModel(modeldoanhthu);
            return View(modeldoanhthu);
        }
        [HttpPost]
        public ActionResult DoanhThuKyGuiHangNgay(DataSourceRequest command, DateTime tuNgay, DateTime denNgay, int VanPhongId)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var items = _phieuguihangService.GetDoanhThuNhanvien(tuNgay, denNgay, _workContext.NhaXeId, VanPhongId);
            var doanhthus = items.Select(c =>
            {
                var _doanhthu = new BaoCaoNhaXeModel.BaoCaoDoanhThuNhanVienModel();
                PrepareThongKeItemToModel(_doanhthu, c);
                _doanhthu.NhanVienId = c.ItemId;
                _doanhthu.ItemDataDate = Convert.ToDateTime(c.ItemDataDay + "-" + c.ItemDataMonth + "-" + c.ItemDataYear);
                _doanhthu.NgayBan = _doanhthu.ItemDataDate.ToString("yyyy-MM-dd");

                _doanhthu.TenNhanVien = _nhanvienService.GetById(_doanhthu.NhanVienId).HoVaTen;
                _doanhthu.TongDoanhThu = c.GiaTri;
                return _doanhthu;
            }).ToList();


            var gridModel = new DataSourceResult
            {
                Data = doanhthus,
                Total = doanhthus.Count
            };

            return Json(gridModel);
        }
        public ActionResult _ChiTietDoanhThuKyGui(int NhanVienId, string NgayThu, string NotPay)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel.BaoCaoDetailDoanhThuKiGuiModel();
            model.NhanVienId = NhanVienId;
            model.NgayBan = NgayThu;
            model.NotPay = NotPay;
            return PartialView(model);
        }


        [HttpPost]
        public ActionResult _ChiTietDoanhThuKyGui(DataSourceRequest command, int NhanVienId, string NgayThu, string NotPay)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            DateTime _NgayBan = Convert.ToDateTime(NgayThu);
            var items = new List<PhieuGuiHang>();
            if (NotPay == "null")
            {
                items = _phieuguihangService.GetAllByNhanVien(_workContext.NhaXeId, NhanVienId, _NgayBan);
            }

            else
            {
                items = _phieuguihangService.GetDetailDoanhThuKiGuiNotPay(_workContext.NhaXeId, NhanVienId, _NgayBan);
            }

            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var hanghoas = _hanghoaService.GetAllHangHoaByPhieuGuiHangId(x.Id);
                    var m = x.ToModel(_localizationService, _priceFormatter, hanghoas);
                    return m;
                }),
                Total = items.Count
            };
            return Json(gridModel);
        }
        public ActionResult DoanhThuKyGuiHangNgayNotPay()
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var modeldoanhthu = new BaoCaoNhaXeModel();
            modeldoanhthu.TuNgay = DateTime.Now.AddMonths(-1);
            modeldoanhthu.DenNgay = DateTime.Now;
            PrepareListVanPhongModel(modeldoanhthu);
            return View(modeldoanhthu);
        }

        [HttpPost]
        public ActionResult _DoanhThuKyGuiHangNgayNotPay(DataSourceRequest command, DateTime tuNgay, DateTime denNgay, int VanPhongId)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                return AccessDeniedView();

            var items = _phieuguihangService.GetDoanhThuKiGuiNotPay(tuNgay, denNgay, _workContext.NhaXeId, VanPhongId);
            var doanhthus = items.Select(c =>
            {
                var _doanhthu = new BaoCaoNhaXeModel.BaoCaoDoanhThuNhanVienModel();
                PrepareThongKeItemToModel(_doanhthu, c);
                _doanhthu.NhanVienId = c.ItemId;
                _doanhthu.ItemDataDate = Convert.ToDateTime(c.ItemDataDay + "-" + c.ItemDataMonth + "-" + c.ItemDataYear);
                _doanhthu.NgayBan = _doanhthu.ItemDataDate.ToString("yyyy-MM-dd");
                _doanhthu.TenNhanVien = _nhanvienService.GetById(_doanhthu.NhanVienId).HoVaTen;
                _doanhthu.TongDoanhThu = c.GiaTri;
                return _doanhthu;
            }).ToList();


            var gridModel = new DataSourceResult
            {
                Data = doanhthus,
                Total = doanhthus.Count
            };

            return Json(gridModel);
        }

        // thống kê theo doanh thu
        public ActionResult HangHoaTheoDoanhThu()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var modeldoanhthu = new BaoCaoNhaXeModel();
            modeldoanhthu.ListLoai1 = GetListChuKyThoiGian();
            PrepareListNgayThangNam(modeldoanhthu);
            return View(modeldoanhthu);
        }
        [HttpPost]
        public ActionResult HangHoaTheoDoanhThu(DataSourceRequest command, BaoCaoNhaXeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            //_phieuguihangService.InsertPhieuGuiHang(phieugui);
            var items = _phieuguihangService.GetAllPhieuGuiHangByCuoc(model.ThangId, model.NamId, _workContext.NhaXeId, (ENBaoCaoChuKyThoiGian)model.Loai1Id);
            return Json(items);
        }
        public ActionResult HangHoaTheoVanPhong()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var modeldoanhthu = new BaoCaoNhaXeModel();
            modeldoanhthu.ListLoai1 = GetListLoaiThoiGian();
            PrepareListNgayThangNam(modeldoanhthu);
            PrepareListQuy(modeldoanhthu);

            return View(modeldoanhthu);
        }
        [HttpPost]
        public ActionResult HangHoaTheoVanPhong(DataSourceRequest command, BaoCaoNhaXeModel model)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var _doanhthu = new ThongKeItem();
                _doanhthu.Nhan = c.TenVanPhong;
                int _sl;
                var listnhavien = _nhanvienService.GetAllByVanPhongId(c.Id);
                _doanhthu.GiaTri = _phieuguihangService.HangHoaDoanhThuVanPhong(listnhavien.Select(nv => nv.Id).ToList(), model.ThangId, model.NamId, (ENBaoCaoQuy)model.QuyId, (ENBaoCaoLoaiThoiGian)model.Loai1Id, out _sl);
                _doanhthu.SoLuong = _sl;
                return _doanhthu;
            }).ToList();

            return Json(vanphongs);
        }
        #endregion

        #region Bao cao theo lai xe, phu xe, doanh thu, chuyen di
        public ActionResult BaoCaoLaiXePhuXe()
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoLaiXePhuXeListModel();
            model.TuNgay = DateTime.Now.NgayDauThang();
            model.DenNgay = DateTime.Now.NgayCuoiThang();
            return View(model);
        }
        [HttpPost]
        public ActionResult BaoCaoLaiXePhuXe(BaoCaoLaiXePhuXeListModel model)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            //lay thong tin cau hinh
            decimal _tienluotlaixe = _nhaxeService.GetGiaTriCauHinh(_workContext.NhaXeId, ENNhaXeCauHinh.SO_TIEN_LUOT_CHO_LAIXE);
            decimal _tienluotphuxe = _nhaxeService.GetGiaTriCauHinh(_workContext.NhaXeId, ENNhaXeCauHinh.SO_TIEN_LUOT_CHO_PHUXE);
            decimal _tileluongdoanhthu = _nhaxeService.GetGiaTriCauHinh(_workContext.NhaXeId, ENNhaXeCauHinh.TI_LE_TINH_LUONG_LAIPHUXE);
            //lay thong tin lai phu xe
            var nhanviens = _nhanvienService.GetAllLaiXePhuXeByNhaXeId(_workContext.NhaXeId, model.LaiPhuxe).OrderBy(c => c.TenVaHo).ToList();


            var items = new List<BaoCaoLaiXePhuXeListModel.BaoCaoLaiXePhuXeModel>();
            foreach (var nv in nhanviens)
            {
                var item = new BaoCaoLaiXePhuXeListModel.BaoCaoLaiXePhuXeModel();
                item.Id = nv.Id;
                item.HoVaTen = nv.HoVaTen;
                item.DienThoai = nv.DienThoai;
                item.CMT_Id = nv.CMT_Id;
                item.KieuNhanVienText = nv.KieuNhanVien.ToCVEnumText(_localizationService);
                ////lay thong tin chuyen di trong khoang thoi gian
                var chuyendinhanviens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0,null, new int[] { nv.Id }, model.TuNgay, model.DenNgay);
                item.TongChuyenDi = chuyendinhanviens.Count;
                //lay thong tin doanh thu
                item.TongDoanhThu = 0;
                foreach(var cd in chuyendinhanviens)
                    item.TongDoanhThu += _baocaoService.GetTongDoanhThuChuyenDi(new int[] { cd.NguonVeId }, cd.NgayDi, cd.NgayDi);
                //tinh theo luong
                if (nv.KieuNhanVien == ENKieuNhanVien.LaiXe)
                {
                    item.TongLuong = item.TongChuyenDi * _tienluotlaixe + item.TongDoanhThu * _tileluongdoanhthu;
                }
                else
                    if (nv.KieuNhanVien == ENKieuNhanVien.PhuXe)
                    {
                        item.TongLuong = item.TongChuyenDi * _tienluotphuxe + item.TongDoanhThu * _tileluongdoanhthu;
                    }
                items.Add(item);
            }
            var gridModel = new DataSourceResult
            {
                Data = items,
                Total = items.Count
            };

            return Json(gridModel);
        }
        public ActionResult _ChiTietBaoCaoLaiPhuXe(int NhanVienId, string TuNgay, string DenNgay)
        {
            if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var _tungay=Convert.ToDateTime(TuNgay);
            var _denngay=Convert.ToDateTime(DenNgay);
            var chuyendinhanviens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0,null, new int[] { NhanVienId }, _tungay, _denngay);
            var items = new List<BaoCaoLaiXePhuXeListModel.BaoCaoXeXuatBen>();
            foreach (var cd in chuyendinhanviens)
            {
                var item = new BaoCaoLaiXePhuXeListModel.BaoCaoXeXuatBen();
                item.Id = cd.Id;
                if (cd.NguonVeInfo != null)
                    item.TuyenXeChay = cd.NguonVeInfo.GetHanhTrinh();
                item.NgayDi = cd.NgayDi;
                item.LaiXe = cd.ThongTinLaiPhuXe(0, true);
                item.PhuXe = cd.ThongTinLaiPhuXe(1, true);
                if (cd.xevanchuyen != null)
                    item.BienSo = cd.xevanchuyen.BienSo;
                item.TongDoanhThu=_baocaoService.GetTongDoanhThuChuyenDi(new int[] {cd.NguonVeId}, cd.NgayDi, cd.NgayDi);
                items.Add(item);
            }
            return PartialView(items);
        }
        #endregion
        #region Khach hang
       public ActionResult BaoCaoKhachHang()
       {
           if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
               return AccessDeniedView();
           var model = new BaoCaoNhaXeModel();
           model.TuNgay = DateTime.Now.AddMonths(-1);
           model.DenNgay = DateTime.Now;
           model.ListLoai2 = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId).Select(c =>
           {
               var item = new SelectListItem();
               item.Text = string.Format("{0} ({1})", c.MoTa, c.MaHanhTrinh);
               item.Value = c.Id.ToString();
               return item;
           }).ToList();
           var _item = new SelectListItem();
           _item.Text = "Chọn hành trình";
           _item.Value = "0";
           model.ListLoai2.Insert(0, _item);
           return View(model);
       }
       [HttpPost]
       public ActionResult BaoCaoKhachHang(DataSourceRequest command, DateTime tuNgay, DateTime denNgay, int HanhTrinhId)
       {
           if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
               return AccessDeniedView();
           var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(HanhTrinhId, _workContext.NhaXeId).ToList();
           var items = _phoiveService.GetKhachHangMuaVeTheoTuyen(_workContext.NhaXeId, tuNgay, denNgay, lichtrinhs.Select(lt => lt.Id).ToList());
           var khachhangs = items.Select(c => new BaoCaoNhaXeModel.KhachHangMuaVeModel
           {
               CustomerId=c.CustomerId,
               TenKhachHang = c.TenKhachHang,
               SoDienThoai = c.SoDienThoai,
               ThongTinChuyenDi = c.ThongTinChuyenDi,
               SoLuot = c.SoLuot
           }).ToList();
           var gridModel = new DataSourceResult
           {
               Data = khachhangs,
               Total = khachhangs.Count
           };
           return Json(gridModel);
       }
       public ActionResult _ChiTietKhachHangLuotDi(int KhachHangId, DateTime tuNgay, DateTime denNgay, int HanhTrinhId)
       {
           if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
               return AccessDeniedView();
           var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(HanhTrinhId, _workContext.NhaXeId).ToList();
           var items = _phoiveService.GetKhachHangMuaVeTheoTuyen(_workContext.NhaXeId, tuNgay, denNgay, lichtrinhs.Select(lt => lt.Id).ToList(),KhachHangId);
           return PartialView(items);
       }
        #endregion
       public ActionResult ThongKeVeTiepVien()
       {
           if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
               return AccessDeniedView();
           var model = new ThongKeVeDaSuDungModel();
           model.TinhTrangQuyens = this.GetCVEnumSelectList<ENTinhTrangQuyen>(_localizationService, model.TinhTrangQuyenId);
           model.ThangId = DateTime.Now.Month;
           model.NamId = DateTime.Now.Year;
           for (int i = 2015; i <= DateTime.Now.Year; i++)
           {
               model.ListYear.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = (i == model.NamId) });
           }
           for (int i = 1; i <= 12; i++)
           {
               model.ListMonth.Add(new SelectListItem { Text = "Tháng " + i.ToString(), Value = i.ToString(), Selected = (i == model.ThangId) });
           }
           return View(model);
       }
       [AcceptVerbs(HttpVerbs.Get)]
       public ActionResult _ThongKeVeTiepVien(int TinhTrangQuyenId, int ThangId, int NamId, string ThongTin)
       {
           if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
               return AccessDeniedPartialView();
           var model = new ThongKeVeDaSuDungModel();
           var iscon = false;
           if ((ENTinhTrangQuyen)TinhTrangQuyenId == ENTinhTrangQuyen.DANG_SU_DUNG)
               iscon = true;
           model.VeTiepVienDaDung = _giaodichkeveService.GetVeTiepVienTheoQuyen(_workContext.NhaXeId, iscon, ThangId, NamId, ThongTin);
           return PartialView(model);
       }

       public ActionResult ExportExcelVeTiepVienSuDung(int TinhTrangQuyenId, int ThangId, int NamId, string ThongTin)
       {
           if (!this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao) && !this.isRightAccess(_permissionService, StandardPermissionProvider.CVHoatDongBanVe))
               return AccessDeniedView();
           var iscon = false;
           if ((ENTinhTrangQuyen)TinhTrangQuyenId == ENTinhTrangQuyen.DANG_SU_DUNG)
               iscon = true;
           try
           {

               byte[] bytes;
               using (var stream = new MemoryStream())
               {
                   var dt = _giaodichkeveService.GetVeTiepVienTheoQuyen(_workContext.NhaXeId, iscon, ThangId, NamId, ThongTin);
                //   _exportManager.ExportTongHopVeTiepVienXlsx(stream, dt);
                   bytes = stream.ToArray();
               }
               return File(bytes, "text/xls", "VeTiepVienSuDung_" + ThangId + ".xlsx");
           }
           catch (Exception exc)
           {
               ErrorNotification(exc);
               return RedirectToAction("List");
           }
       }
     
    }
}