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
using System.Data;
using System.Reflection;
using System.IO;
using Nop.Services.ExportImport;
using Nop.Services.ChuyenPhatNhanh;
using Nop.Core.Domain.ChuyenPhatNhanh;

namespace Nop.Web.Controllers
{
    public class BaoCaoController : BaseNhaXeController
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
        private readonly IExportManager _exportManager;
        private readonly IGiaoDichKeVeXeService _giaodichkeveService;
        private readonly IKeToanService _ketoanService;
        private readonly IPhieuChuyenPhatService _phieuchuyenphatService;

        public BaoCaoController(IGiaoDichKeVeXeService giaodichkeveService,
            IExportManager exportManager,
            IStateProvinceService stateProvinceService,
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
            IBaoCaoService baocaoService,
             IPhieuChuyenPhatService phieuchuyenphatService,
            IKeToanService ketoanService
            )
        {
            this._giaodichkeveService = giaodichkeveService;
            this._exportManager = exportManager;
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
            this._ketoanService = ketoanService;
             this._phieuchuyenphatService = phieuchuyenphatService;

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
            for (int i = 2016; i <= DateTime.Now.Year; i++)
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
                Text = string.Format("{0}-{1}", c.ThoiGianDi.ToString("HH:mm"), c.ThoiGianDen.ToString("HH:mm")),
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

        void PrepareListXeModel(BaoCaoNhaXeModel model, bool isChonXe = false)
        {
            var xevanchuyen = _xeinfoService.GetAllXeVanChuyenByNhaXeId(_workContext.NhaXeId);

            var ddls = xevanchuyen.Select(c => new SelectListItem
            {
                Text = c.BienSo,
                Value = c.Id.ToString(),
            }).ToList();
            if (isChonXe)
                ddls.Insert(0, new SelectListItem { Text = "--------Chọn--------", Value = "0" });
            model.Xe = ddls;
        }
        void PrepareListLoaiXeModel(BaoCaoNhaXeModel model, bool isChonXe = false)
        {
            var loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId);

            var ddls = loaixes.Select(c => new SelectListItem
            {
                Text = c.TenLoaiXe,
                Value = c.Id.ToString(),
            }).ToList();
            if (isChonXe)
                ddls.Insert(0, new SelectListItem { Text = "--------Chọn--------", Value = "0" });
            model.LoaiXes = ddls;
        }
        void PrepareListHanhTrinhModel(BaoCaoNhaXeModel model, bool isAll = true, bool isChonHanhTrinh = false)
        {
            List<HanhTrinh> hanhtrinhs = new List<HanhTrinh>();
            if (isAll)
                hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId);
            else
                hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, _workContext.CurrentVanPhong.Id);
            var ddls = hanhtrinhs.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = string.Format("{0} ({1})", c.MoTa, c.MaHanhTrinh);
                item.Value = c.Id.ToString();
                return item;
            }).ToList();

            if (isChonHanhTrinh)
                ddls.Insert(0, new SelectListItem { Text = "--------Chọn--------", Value = "0" });
            model.HanhTrinhs = ddls;
        }
        void PrepareListTuyenModel(BaoCaoNhaXeModel model, bool isChonTuyen = true)
        {
            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            var ddls = tuyens.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.Ten;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();

            if (isChonTuyen)
                ddls.Insert(0, new SelectListItem { Text = "--------Chọn--------", Value = "0" });
            model.ListTuyens = ddls;
        }
        #endregion
        #region DataTable handle
        public static DataTable ToDataTable<T>(IEnumerable<T> items, String[] headers)
        {
            var table = CreateDataTableForPropertiesOfType<T>();
            PropertyInfo[] piT = typeof(T).GetProperties();
            foreach (var item in items)
            {
                var dr = table.NewRow();
                for (int property = 0; property < table.Columns.Count; property++)
                {
                    if (piT[property].CanRead)
                    {
                        var value = piT[property].GetValue(item, null);
                        if (piT[property].PropertyType.IsGenericType)
                        {
                            if (value == null)
                            {
                                dr[property] = DBNull.Value;
                            }
                            else
                            {
                                dr[property] = piT[property].GetValue(item, null);
                            }
                        }
                        else
                        {
                            dr[property] = piT[property].GetValue(item, null);
                        }
                    }
                }
                table.Rows.Add(dr);
            }
            return table;
        }

        public static DataTable CreateDataTableForPropertiesOfType<T>()
        {
            DataTable dt = new DataTable();
            PropertyInfo[] piT = typeof(T).GetProperties();
            foreach (PropertyInfo pi in piT)
            {
                Type propertyType = null;
                if (pi.PropertyType.IsGenericType)
                {
                    propertyType = pi.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    propertyType = pi.PropertyType;
                }
                DataColumn dc = new DataColumn(pi.Name, propertyType);

                if (pi.CanRead)
                {
                    dt.Columns.Add(dc);
                }
            }
            return dt;
        }

        public static DataTable ToDataTableSimple(int NumOfCol)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < NumOfCol; i++)
            {
                DataColumn dc = new DataColumn("Col" + i.ToString(), typeof(System.String));
                dt.Columns.Add(dc);
            }
            return dt;
        }
        #endregion
        #region Các method sử dụng cho bao cao chung
        private String GetTopPageOfReport()
        {
            var item = _nhaxeService.GetNhaXeCauHinhByCode(_workContext.NhaXeId, ENNhaXeCauHinh.HEADER_BAO_CAO);
            if (item != null)
                return item.GiaTri;
            return "";

        }
        private ActionResult exportToExcel(BaoCaoNhaXeModel model, string filename)
        {
            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                _exportManager.ExportDataTableToExcel(stream, model.headers, model.dataReport, model.Title, model.TitleColSpan, model.topPage, model.isShowSTT, model.addSumRight, model.idxColForSum, model.addSumBottom);
                bytes = stream.ToArray();

            }
            return File(bytes, "text/xls", filename);
        }
        BaoCaoNhaXeModel createDataBaoCaoNhaXeModel(BaoCaoNhaXeModel model)
        {
            switch (model.LoaiBaoCao)
            {
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TONG_HOP_HANH_KHACH:
                    model = createTongHopHanhKhach(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.LUOT_XE_CHAY:
                    model = createLuotXeChay(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.LUOT_XE_CHAY_NAM:
                    model = createLuotXeChayNam(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TONG_HOP_DOANH_THU_THANG:
                    model = createDoanhThuThang(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TONG_HOP_HK_CL_DT:
                    model = createTongHopHK_CL_DT(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.CHI_TIET_CHI_PHI:
                    model = createChiPhiTheoXe(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TONG_HOP_CHI_PHI:
                    model = createTongHopChiPhi(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TONG_HOP_CHUNG:
                    model = createTongHopChung(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.BAO_CAO_DT_CL_NHAN_VIEN:
                    model = createBaoCaoDT_CL_NV(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.SO_QUY_TIEN_MAT:
                    model = createBaoCaoSoQuyTienMat(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.DOANH_THU_CHANG:
                    model = createDoanhThuChang(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.LUOT_NHAN_VIEN:
                    model = createLuotNhanVien(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.HANG_HOA_VAN_PHONG:
                    model = createDoanhThuHangHoaVanPhong(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.HANG_HOA_TONG_HOP:
                    model = createDoanhThuHangHoaTongHop(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.BAO_CAO_PHIEU_VAN_CHUYEN:
                    model = createBaoCaoPhieuVanChuyen(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.PHIEU_VAN_CHUYEN_NGAY:
                    model = createLenhVanChuyenNgay(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.PHIEU_VAN_CHUYEN_THANG:
                    model = createLenhVanChuyenThang(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.CHI_TIEU_THANG:
                    model = createChiTieuThang(model);
                    break;
                case BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TO_VAN_CHUYEN_THANG:
                    model = createToVanchuyenThang(model);
                    break;
            }
            return model;
        }
        [HttpPost]
        public ActionResult _BaoCaoTongHop(BaoCaoNhaXeModel model)
        {
            model = createDataBaoCaoNhaXeModel(model);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ExportBaoCao(BaoCaoNhaXeModel model)
        {
            model = createDataBaoCaoNhaXeModel(model);
            return exportToExcel(model, model.FileNameExport + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls");
        }
        #endregion
        #region Báo cáo tổng hợp số hành khách
        BaoCaoNhaXeModel createTongHopHanhKhach(BaoCaoNhaXeModel model)
        {
            DateTime ngaydauthang = new DateTime(model.NamId, model.ThangId, 1);
            DateTime ngaycuoithang = ngaydauthang.AddMonths(1).AddDays(-1);
            //cau hinh bao cao
            model.isShowSTT = false;
            model.topPage = GetTopPageOfReport();

            model.Title = new string[] { string.Format("BÁO CÁO TỔNG HỢP SỐ HÀNH KHÁCH THÁNG {0} NĂM {1}", model.ThangId, model.NamId) };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add("Ngày");
            //loc thong tin xe di theo hanh trinh,
            //lay tat ca tuyen
            model.TitleColSpan = new List<string[]>();
            //add header colspan ngay
            model.TitleColSpan.Add(new string[] { "", "1", "" });
            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            List<BaoCaoNhaXeModel.TuyenXeVanChuyen> arrTuyenInfo = new List<BaoCaoNhaXeModel.TuyenXeVanChuyen>();
            foreach (var tuyen in tuyens)
            {
                var item = new BaoCaoNhaXeModel.TuyenXeVanChuyen();
                item.tuyen = tuyen;
                //lay tat ca xe di trong tuyen nay bang cach loc tat ca xe da di trong 2 hanh trinh thuoc tuyen
                item.hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, tuyen.Id);
                item.xuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0, item.hanhtrinhs.Select(c => c.Id).ToArray(), null, ngaydauthang, ngaycuoithang);
                item.xevanchuyens = item.xuatbens.Where(c => c.XeVanChuyenId > 0).Select(c => c.xevanchuyen).Distinct().ToList();
                if (item.xevanchuyens.Count > 0)
                {
                    model.TitleColSpan.Add(new string[] { tuyen.Ten, item.xevanchuyens.Count.ToString(), item.xuatbens.Sum(c => c.SoNguoi).ToString() });
                    foreach (var xe in item.xevanchuyens)
                    {
                        headers.Add(xe.BienSo);
                    }
                    arrTuyenInfo.Add(item);
                }
            }

            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);

            for (var dt = ngaydauthang; dt <= ngaycuoithang; dt = dt.AddDays(1))
            {
                var dr = dataReport.NewRow();
                dr[0] = dt.Day.ToString();
                int col = 1;
                foreach (var tuyen in arrTuyenInfo)
                {
                    foreach (var xe in tuyen.xevanchuyens)
                    {
                        dr[col] = tuyen.xuatbens.Where(c => c.XeVanChuyenId == xe.Id && c.NgayDi.Day == dt.Day).Sum(c => c.SoNguoi);
                        col++;
                    }

                }
                dataReport.Rows.Add(dr);
            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult TongHopHanhKhach()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TONG_HOP_HANH_KHACH;
            PrepareListNgayThangNam(model);
            return View(model);
        }

        #endregion
        #region Bao cao luot xe chay
        BaoCaoNhaXeModel createLuotXeChay(BaoCaoNhaXeModel model)
        {
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { string.Format("THỐNG KÊ LƯỢT XE CHẠY THÁNG {0} NĂM {1}", model.ThangId, model.NamId) };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add(@"Xe BKS\Ngày");
            DateTime ngaydauthang = new DateTime(model.NamId, model.ThangId, 1);
            DateTime ngaycuoithang = ngaydauthang.AddMonths(1).AddDays(-1);
            for (var dt = ngaydauthang; dt <= ngaycuoithang; dt = dt.AddDays(1))
            {
                headers.Add(dt.Day.ToString());
            }
            model.headers = headers.ToArray();
            //lay thong tin xe
            //tao du lieu trong bao cao
            var lsxe = _nhaxeService.GetAllBienSoXeByNhaXeId(_workContext.NhaXeId);
            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            int[] hanhtrinhids = null;
            List<HistoryXeXuatBen> xexuatbens = null, xexuatbentemps = null;
            if (model.TuyenId > 0)
            {
                hanhtrinhids = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, model.TuyenId).Select(c => c.Id).ToArray();
                xexuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0, hanhtrinhids, null, ngaydauthang, ngaycuoithang);
                lsxe = xexuatbens.Where(c => c.XeVanChuyenId > 0).Select(c => c.xevanchuyen).Distinct().ToList();
            }

            foreach (var xe in lsxe)
            {
                var dr = dataReport.NewRow();
                dr[0] = xe.BienSo;
                if (xexuatbens != null)
                    xexuatbentemps = xexuatbens.Where(c => c.XeVanChuyenId == xe.Id).ToList();
                else
                    xexuatbentemps = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, xe.Id, hanhtrinhids, null, ngaydauthang, ngaycuoithang);
                for (var dt = ngaydauthang; dt <= ngaycuoithang; dt = dt.AddDays(1))
                {
                    dr[dt.Day] = xexuatbentemps.Count(c => c.NgayDi.Day == dt.Day).ToString();
                }
                dataReport.Rows.Add(dr);
            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult LuotXeChay()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.LUOT_XE_CHAY;
            PrepareListNgayThangNam(model);
            PrepareListTuyenModel(model);
            return View(model);
        }

        #endregion
        #region Bao cao luot xe chay nam
        BaoCaoNhaXeModel createLuotXeChayNam(BaoCaoNhaXeModel model)
        {
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { "THỐNG KÊ LƯỢT XE CHẠY NĂM " + model.NamId.ToString() };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add(@"Xe BKS\Tháng");
            headers.Add(@"Tuyến");
            model.idxColForSum = 2;
            DateTime ngaydaunam = new DateTime(model.NamId, 1, 1);
            DateTime ngaycuoinam = new DateTime(model.NamId, 12, 31);
            for (int t = 1; t <= 12; t++)
            {
                headers.Add(t.ToString());
            }
            model.headers = headers.ToArray();
            //lay thong tin xe
            //tao du lieu trong bao cao
            int[] hanhtrinhids = null;
            List<HistoryXeXuatBen> xexuatbens = null, xexuatbentemps = null;
            var lsxe = _nhaxeService.GetAllBienSoXeByNhaXeId(_workContext.NhaXeId);
            string Tentuyen = "";
            if (model.TuyenId > 0)
            {
                Tentuyen = _hanhtrinhService.GetTuyenById(model.TuyenId).Ten;
                hanhtrinhids = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, model.TuyenId).Select(c => c.Id).ToArray();
                xexuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0, hanhtrinhids, null, ngaydaunam, ngaycuoinam);
                lsxe = xexuatbens.Where(c => c.XeVanChuyenId > 0).Select(c => c.xevanchuyen).Distinct().ToList();
            }

            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            foreach (var xe in lsxe)
            {
                var dr = dataReport.NewRow();
                dr[0] = xe.BienSo;
                dr[1] = Tentuyen;
                if (xexuatbens != null)
                    xexuatbentemps = xexuatbens.Where(c => c.XeVanChuyenId == xe.Id).ToList();
                else
                    xexuatbentemps = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, xe.Id, hanhtrinhids, null, ngaydaunam, ngaycuoinam);

                for (int t = 1; t <= 12; t++)
                {
                    dr[t + 1] = xexuatbentemps.Count(c => c.NgayDi.Month == t).ToString();
                }
                dataReport.Rows.Add(dr);
            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult LuotXeChayNam()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.LUOT_XE_CHAY_NAM;
            PrepareListNgayThangNam(model);
            PrepareListTuyenModel(model);
            return View(model);
        }

        #endregion
        #region Tổng hợp doanh thu tháng
        BaoCaoNhaXeModel createDoanhThuThang(BaoCaoNhaXeModel model)
        {
            DateTime ngaydauthang = new DateTime(model.NamId, model.ThangId, 1);
            DateTime ngaycuoithang = ngaydauthang.AddMonths(1).AddDays(-1);
            //cau hinh bao cao
            model.isShowSTT = false;
            model.topPage = GetTopPageOfReport();

            model.Title = new string[] { string.Format("TỔNG HỢP SỐ DOANH THU THÁNG {0} NĂM {1}", model.ThangId, model.NamId) };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add("Ngày");
            //loc thong tin xe di theo hanh trinh,
            //lay tat ca tuyen
            model.TitleColSpan = new List<string[]>();
            //add header colspan ngay
            model.TitleColSpan.Add(new string[] { "", "1", "" });
            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            List<BaoCaoNhaXeModel.TuyenXeVanChuyen> arrTuyenInfo = new List<BaoCaoNhaXeModel.TuyenXeVanChuyen>();
            foreach (var tuyen in tuyens)
            {
                var item = new BaoCaoNhaXeModel.TuyenXeVanChuyen();
                item.tuyen = tuyen;
                //lay tat ca xe di trong tuyen nay bang cach loc tat ca xe da di trong 2 hanh trinh thuoc tuyen
                item.hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, tuyen.Id);
                item.xuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0, item.hanhtrinhs.Select(c => c.Id).ToArray(), null, ngaydauthang, ngaycuoithang);
                item.xevanchuyens = item.xuatbens.Where(c => c.XeVanChuyenId > 0).Select(c => c.xevanchuyen).Distinct().ToList();
                if (item.xevanchuyens.Count > 0)
                {
                    model.TitleColSpan.Add(new string[] { tuyen.Ten, item.xevanchuyens.Count.ToString(), "" });
                    foreach (var xe in item.xevanchuyens)
                    {
                        headers.Add(xe.BienSo);
                    }
                    arrTuyenInfo.Add(item);
                }
            }

            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);

            for (var dt = ngaydauthang; dt <= ngaycuoithang; dt = dt.AddDays(1))
            {
                var dr = dataReport.NewRow();
                dr[0] = dt.Day.ToString();
                int col = 1;
                foreach (var tuyen in arrTuyenInfo)
                {
                    foreach (var xe in tuyen.xevanchuyens)
                    {
                        var chuyendiids = tuyen.xuatbens.Where(c => c.XeVanChuyenId == xe.Id && c.NgayDi.Day == dt.Day).Select(c => c.Id).ToArray();
                        decimal _dt = _baocaoService.GetTongDoanhThuTheoChuyenDi(chuyendiids);
                        dr[col] = _dt.ToSoNguyen();
                        tuyen.TongGiaTri += _dt;
                        col++;
                    }

                }
                dataReport.Rows.Add(dr);
            }
            for (int i = 0; i < arrTuyenInfo.Count; i++)
            {

                model.TitleColSpan[i + 1][2] = arrTuyenInfo[i].TongGiaTri.ToSoNguyen();
            }
            model.dataReport = dataReport;

            return model;
        }
        public ActionResult DoanhThuThang()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TONG_HOP_DOANH_THU_THANG;
            PrepareListNgayThangNam(model);
            return View(model);
        }

        #endregion
        #region Báo cáo tổng hợp hành khách, chuyến lượt, doanh thu
        BaoCaoNhaXeModel createTongHopHK_CL_DT(BaoCaoNhaXeModel model)
        {
            DateTime ngaydauthang = model.TuNgayH;
            DateTime ngaycuoithang = model.DenNgayH;

            //kha phuc tap lam sau
            //cau hinh bao cao
            model.addSumRight = false;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { string.Format("BÁO CÁO TỔNG HỢP HÀNH KHÁCH, CHUYẾN LƯỢT, DOANH THU TỪ NGÀY {0} ĐẾN {1}", model.TuNgay.ToNgayThangVN(), model.DenNgay.ToNgayThangVN()) };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add("BIỂN SỐ");
            headers.Add("HÀNH TRÌNH");
            headers.Add("GIỜ XE CHẠY");
            headers.Add("HÀNH KHÁCH");
            headers.Add("CHUYẾN LƯỢT");
            headers.Add("DOANH THU");
            model.headers = headers.ToArray();
            //lay thong tin xe
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);




            var hanhtrinhids = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, 0).Select(c => c.Id).ToArray();
            if (model.HanhTrinhId > 0)
                hanhtrinhids = hanhtrinhids.Where(c => c == model.HanhTrinhId).ToArray();
            var xexuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0, hanhtrinhids, null, ngaydauthang, ngaycuoithang);
            var lsxe = xexuatbens.Where(c => c.XeVanChuyenId > 0).Select(c => c.xevanchuyen).Distinct().ToList();
            foreach (var xe in lsxe)
            {
                var nguonves = xexuatbens.Where(c => c.XeVanChuyenId == xe.Id).Select(c => c.NguonVeInfo).Distinct();
                foreach (var nv in nguonves)
                {
                    var dr = dataReport.NewRow();
                    dr[0] = xe.BienSo;
                    dr[1] = nv.LichTrinhInfo.HanhTrinhInfo.MoTa; ;
                    dr[2] = nv.ThoiGianDi.ToString("HH:mm");
                    var chuyendis = xexuatbens.Where(c => c.XeVanChuyenId == xe.Id && c.NguonVeId == nv.Id).ToList();
                    dr[3] = chuyendis.Sum(c => c.SoNguoi).ToSoNguyen();
                    dr[4] = chuyendis.Count;
                    dr[5] = _baocaoService.GetTongDoanhThuTheoChuyenDi(chuyendis.Select(c => c.Id).ToArray()).ToSoNguyen();
                    dataReport.Rows.Add(dr);
                }
            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult TongHopHK_CL_DT()
        {

            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TONG_HOP_HK_CL_DT;
            PrepareListXeModel(model);
            PrepareListHanhTrinhModel(model, true, true);
            model.TuNgayH = DateTime.Now.Date.AddMonths(-1);
            model.DenNgayH = DateTime.Now.Date;
            return View(model);
        }

        #endregion
        #region Báo cáo chi tiết chi phí theo xe
        String getChiPhiChuyenDi(List<ChuyenDiTaiChinh> arrSrc, ENLoaiTaiChinhThuChi loaicp)
        {
            decimal ret = decimal.Zero;
            foreach (var item in arrSrc)
            {
                var cpitem = item.GiaoDichThuChis.Where(c => c.LoaiThuChiId == (int)loaicp).FirstOrDefault();
                if (cpitem != null)
                    ret += cpitem.SoTien;
            }
            return Math.Abs(ret).ToSoNguyen();
        }
        BaoCaoNhaXeModel createChiPhiTheoXe(BaoCaoNhaXeModel model)
        {
            //lay thong tin xe
            var xeinfo = _xeinfoService.GetXeInfoById(model.XeId);
            string _bienso = "";
            if (xeinfo != null)
                _bienso = xeinfo.BienSo;
            model.isShowSTT = false;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { string.Format("CHI TIẾT CHI PHÍ THEO XE THÁNG {0} NĂM {1}", model.ThangId, model.NamId), _bienso };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add("Ngày");
            //lay danh sach cac chi phi
            headers.Add("CP bến");
            headers.Add("CP qua đêm");
            headers.Add("Vé cầu đường ");
            headers.Add("Đăng kiểm");
            headers.Add("Sửa chữa");
            headers.Add("Gửi xe, rửa xe");
            headers.Add("CP Công an");
            headers.Add("CP NVL");
            headers.Add("CP khác");
            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            DateTime ngaydauthang = new DateTime(model.NamId, model.ThangId, 1);
            DateTime ngaycuoithang = ngaydauthang.AddMonths(1).AddDays(-1);
            for (var dt = ngaydauthang; dt <= ngaycuoithang; dt = dt.AddDays(1))
            {
                var dr = dataReport.NewRow();
                dr[0] = dt.Day.ToString();
                //lay thong tin chi phi cua xe theo ngay
                //lay thong tin chuyen di cua xe nay tron ngay
                var xexuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, xeinfo.Id, null, null, dt, dt);
                if (xexuatbens.Count == 0)
                {
                    for (int i = 1; i < dataReport.Columns.Count; i++)
                    {
                        dr[i] = "0";
                    }
                }
                else
                {
                    //lay thong tin chi phi cac chuyen di
                    List<ChuyenDiTaiChinh> arrcdtc = new List<ChuyenDiTaiChinh>();
                    foreach (var xexb in xexuatbens)
                    {
                        var itemtc = _giaodichkeveService.GetChuyenDiTaiChinhByLuotId(xexb.Id);
                        if (itemtc != null)
                        {
                            arrcdtc.Add(itemtc);
                        }
                    }
                    //dua vao thong tin chuyen di tai chinh de loc gia tri chi phi
                    dr[1] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_BEN_XE);
                    dr[2] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.GUI_XE_QUA_DEM);
                    dr[3] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_CAU_DUONG);
                    dr[4] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_DANG_KIEM);
                    dr[5] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_SUA_CHUA_XE);
                    dr[6] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.RUA_XE);
                    dr[7] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_CONG_AN);
                    dr[8] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_TIEN_DAU);
                    dr[9] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_KHAC);
                }
                dataReport.Rows.Add(dr);
            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult ChiPhiTheoXe()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.CHI_TIET_CHI_PHI;
            PrepareListNgayThangNam(model);
            PrepareListXeModel(model);
            return View(model);
        }

        #endregion
        #region Tổng hợp chi phí theo xe
        BaoCaoNhaXeModel createTongHopChiPhi(BaoCaoNhaXeModel model)
        {
            //lay thong tin xe
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { string.Format("CHI TIẾT CHI PHÍ THEO XE TỪ NGÀY {0} ĐẾN {1}", model.TuNgay.ToNgayThangVN(), model.DenNgay.ToNgayThangVN()) };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add("Biển số");
            headers.Add("Tuyến");
            //lay danh sach cac chi phi
            //thiet dat cot de tinh sum
            model.idxColForSum = 2;
            headers.Add("CP bến");
            headers.Add("CP qua đêm");
            headers.Add("Vé cầu đường ");
            headers.Add("Đăng kiểm");
            headers.Add("Sửa chữa");
            headers.Add("Gửi xe, rửa xe");
            headers.Add("CP Công an");
            headers.Add("CP NVL");
            headers.Add("CP khác");
            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            DateTime ngaydauthang = model.TuNgay;
            DateTime ngaycuoithang = model.DenNgay;

            List<TuyenHanhTrinh> lstuyen = new List<TuyenHanhTrinh>();
            if (model.TuyenId > 0)
            {
                var tuyen = _hanhtrinhService.GetTuyenById(model.TuyenId);
                lstuyen.Add(tuyen);
            }
            else
            {
                //lay tat ca cac tuyen
                lstuyen = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            }
            foreach (var tuyen in lstuyen)
            {
                var hanhtrinhids = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, tuyen.Id).Select(c => c.Id).ToArray();
                var xexuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0, hanhtrinhids, null, ngaydauthang, ngaycuoithang);
                var lsxe = xexuatbens.Where(c => c.XeVanChuyenId > 0).Select(c => c.xevanchuyen).Distinct().ToList();
                foreach (var xe in lsxe)
                {
                    var dr = dataReport.NewRow();
                    dr[0] = xe.BienSo;
                    dr[1] = tuyen.Ten;
                    var chuyendis = xexuatbens.Where(c => c.XeVanChuyenId == xe.Id).ToList();

                    if (chuyendis.Count == 0)
                    {
                        for (int i = 2; i < dataReport.Columns.Count; i++)
                        {
                            dr[i] = "0";
                        }
                    }
                    else
                    {
                        //lay thong tin chi phi cac chuyen di
                        List<ChuyenDiTaiChinh> arrcdtc = new List<ChuyenDiTaiChinh>();
                        foreach (var xexb in chuyendis)
                        {
                            var itemtc = _giaodichkeveService.GetChuyenDiTaiChinhByLuotId(xexb.Id);
                            if (itemtc != null)
                            {
                                arrcdtc.Add(itemtc);
                            }
                        }
                        //dua vao thong tin chuyen di tai chinh de loc gia tri chi phi
                        dr[2] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_BEN_XE);
                        dr[3] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.GUI_XE_QUA_DEM);
                        dr[4] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_CAU_DUONG);
                        dr[5] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_DANG_KIEM);
                        dr[6] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_SUA_CHUA_XE);
                        dr[7] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.RUA_XE);
                        dr[8] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_CONG_AN);
                        dr[9] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_TIEN_DAU);
                        dr[10] = getChiPhiChuyenDi(arrcdtc, ENLoaiTaiChinhThuChi.CHI_KHAC);
                    }
                    dataReport.Rows.Add(dr);
                }
            }


            model.dataReport = dataReport;

            return model;
        }
        public ActionResult TongHopChiPhi()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TONG_HOP_CHI_PHI;
            model.TuNgay = DateTime.Now.Date.AddMonths(-1);
            model.DenNgay = DateTime.Now.Date;
            PrepareListNgayThangNam(model);
            PrepareListTuyenModel(model);
            return View(model);
        }

        #endregion
        #region Tổng hợp chung
        BaoCaoNhaXeModel createTongHopChung(BaoCaoNhaXeModel model)
        {
            model.addSumRight = false;
            DateTime ngaydauthang = model.TuNgay;
            DateTime ngaycuoithang = model.DenNgay;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { string.Format("TỔNG HỢP CHUNG TỪ NGÀY {0} ĐẾN {1}", model.TuNgay.ToNgayThangVN(), model.DenNgay.ToNgayThangVN()) };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add("Biển số");
            headers.Add("Tuyến");
            //thiet dat cot de tinh sum
            model.idxColForSum = 2;
            headers.Add("Doanh thu");
            headers.Add("CP chung");
            headers.Add("Lợi nhuận");

            model.headers = headers.ToArray();
            //lay thong tin xe
            //tao du lieu trong bao cao
            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            if (model.TuyenId > 0)
                tuyens = tuyens.Where(c => c.Id == model.TuyenId).ToList();
            List<BaoCaoNhaXeModel.TuyenXeVanChuyen> arrTuyenInfo = new List<BaoCaoNhaXeModel.TuyenXeVanChuyen>();
            foreach (var tuyen in tuyens)
            {
                var item = new BaoCaoNhaXeModel.TuyenXeVanChuyen();
                item.tuyen = tuyen;
                //lay tat ca xe di trong tuyen nay bang cach loc tat ca xe da di trong 2 hanh trinh thuoc tuyen
                item.hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, tuyen.Id);
                item.xuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0, item.hanhtrinhs.Select(c => c.Id).ToArray(), null, ngaydauthang, ngaycuoithang);
                if (model.XeId > 0)
                    item.xevanchuyens = item.xuatbens.Where(c => c.XeVanChuyenId == model.XeId).Select(c => c.xevanchuyen).Distinct().ToList();
                else
                    item.xevanchuyens = item.xuatbens.Where(c => c.XeVanChuyenId > 0).Select(c => c.xevanchuyen).Distinct().ToList();
                if (item.xevanchuyens.Count > 0)
                {
                    arrTuyenInfo.Add(item);
                }
            }

            DataTable dataReport = ToDataTableSimple(model.headers.Length);

            foreach (var tuyen in arrTuyenInfo)
            {

                foreach (var xe in tuyen.xevanchuyens)
                {
                    var dr = dataReport.NewRow();
                    dr[0] = xe.BienSo;
                    dr[1] = tuyen.tuyen.Ten;
                    //doanh thu
                    var chuyendiids = tuyen.xuatbens.Where(c => c.XeVanChuyenId == xe.Id).Select(c => c.Id).ToArray();
                    decimal doanhthu = _baocaoService.GetTongDoanhThuTheoChuyenDi(chuyendiids);
                    dr[2] = doanhthu.ToSoNguyen();
                    //chi phi
                    decimal chiphi = decimal.Zero;
                    List<ChuyenDiTaiChinh> arrcdtc = new List<ChuyenDiTaiChinh>();
                    foreach (var xexbid in chuyendiids)
                    {
                        var itemtc = _giaodichkeveService.GetChuyenDiTaiChinhByLuotId(xexbid);
                        if (itemtc != null)
                        {
                            chiphi = chiphi + itemtc.GiaoDichThuChis.Sum(c => c.SoTien);
                        }
                    }
                    chiphi = Math.Abs(chiphi);
                    dr[3] = chiphi.ToSoNguyen();
                    dr[4] = (doanhthu - chiphi).ToSoNguyen();
                    dataReport.Rows.Add(dr);
                }



            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult TongHopChung()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TONG_HOP_CHUNG;
            PrepareListNgayThangNam(model);
            PrepareListTuyenModel(model);
            PrepareListXeModel(model, true);
            model.TuNgay = DateTime.Now.Date.AddMonths(-1);
            model.DenNgay = DateTime.Now.Date;
            return View(model);
        }

        #endregion
        #region Báo cáo doanh thu, chuyến lượt theo nhân viên
        BaoCaoNhaXeModel createBaoCaoDT_CL_NV(BaoCaoNhaXeModel model)
        {
            model.addSumRight = false;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { string.Format("BÁO CÁO DOANH THU, CHUYẾN LƯỢT THEO NHÂN VIÊN  NGÀY {0} ĐẾN {1}", model.TuNgay.ToNgayThangVN(), model.DenNgay.ToNgayThangVN()) };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add("Mã NV");
            headers.Add("Nhân viên");
            headers.Add("Biển số");
            headers.Add("Tuyến");
            //thiet dat cot de tinh sum
            model.idxColForSum = 4;
            headers.Add("Chuyến lượt");
            headers.Add("Doanh thu");
            model.headers = headers.ToArray();
            //lay thong tin xe
            //tao du lieu trong bao cao            
            DataTable dataReport = ToDataTableSimple(model.headers.Length);

            DateTime ngaydauthang = model.TuNgay;
            DateTime ngaycuoithang = model.DenNgay;

            List<TuyenHanhTrinh> lstuyen = new List<TuyenHanhTrinh>();
            if (model.TuyenId > 0)
            {
                var tuyen = _hanhtrinhService.GetTuyenById(model.TuyenId);
                lstuyen.Add(tuyen);
            }
            else
            {
                //lay tat ca cac tuyen
                lstuyen = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            }
            var nhanhviens = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, null, model.KeySearch);
            foreach (var tuyen in lstuyen)
            {
                var hanhtrinhids = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, tuyen.Id).Select(c => c.Id).ToArray();
                var xexuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0, hanhtrinhids, null, ngaydauthang, ngaycuoithang);
                //lay nhan vien trong chuyen di
                var nhanvienstemp = nhanhviens.Where(c => xexuatbens.Any(x => x.LaiPhuXes.Any(l => l.NhanVien_Id == c.Id))).ToList();
                foreach (var nv in nhanvienstemp)
                {
                    var lsxe = xexuatbens.Where(c => c.XeVanChuyenId > 0 && c.LaiPhuXes.Any(l => l.NhanVien_Id == nv.Id)).Select(c => c.xevanchuyen).Distinct().ToList();
                    foreach (var xe in lsxe)
                    {
                        var dr = dataReport.NewRow();
                        dr[0] = nv.CMT_Id;
                        dr[1] = nv.HoVaTen;
                        dr[2] = xe.BienSo;
                        dr[3] = tuyen.Ten;
                        var chuyendis = xexuatbens.Where(c => c.XeVanChuyenId == xe.Id && c.LaiPhuXes.Any(l => l.NhanVien_Id == nv.Id)).ToList();
                        dr[4] = chuyendis.Count;
                        dr[5] = _baocaoService.GetTongDoanhThuTheoChuyenDi(chuyendis.Select(c => c.Id).ToArray()).ToSoNguyen();
                        dataReport.Rows.Add(dr);
                    }
                }


            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult BaoCaoDT_CL_NV()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.BAO_CAO_DT_CL_NHAN_VIEN;
            PrepareListXeModel(model);
            PrepareListTuyenModel(model);
            model.TuNgay = DateTime.Now.Date.AddMonths(-1);
            model.DenNgay = DateTime.Now.Date;
            return View(model);
        }

        #endregion
        #region doanh thu chang, diem
        BaoCaoNhaXeModel createDoanhThuChang(BaoCaoNhaXeModel model)
        {
            DateTime ngaydauthang = model.TuNgay;
            DateTime ngaycuoithang = model.DenNgay;

            //kha phuc tap lam sau
            //cau hinh bao cao
            model.addSumRight = false;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { string.Format("BÁO CÁO TỔNG HỢP HÀNH KHÁCH THEO CHẶNG TỪ NGÀY {0} ĐẾN {1}", model.TuNgay.ToNgayThangVN(), model.DenNgay.ToNgayThangVN()) };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add(" TUYẾN");
            headers.Add(" LOẠI XE");
            headers.Add("TÊN CHẶNG");
            headers.Add("SỐ KHÁCH");
            headers.Add("DOANH THU");
            model.headers = headers.ToArray();
            //lay thong tin xe
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);




            var hanhtrinhids = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, 0).Select(c => c.Id).ToArray();
            if (model.HanhTrinhId > 0)
                hanhtrinhids = hanhtrinhids.Where(c => c == model.HanhTrinhId).ToArray();
            var xexuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0, hanhtrinhids, null, ngaydauthang, ngaycuoithang);


            //lay thong ti khach di xe
            var changs = _hanhtrinhService.GetallHanhTrinhGiaVe(model.HanhTrinhId, _workContext.NhaXeId, 0, 0, model.LoaiXeId);
            var dtchang = new List<DoanhThuChang>();
            foreach (var item in changs)
            {
                var m = new DoanhThuChang();
                m.ChangId = item.Id;
                string tenloaixe = "";
                if (item.loaixe != null)
                    tenloaixe = item.loaixe.TenLoaiXe;
                m.TenLoaiXe = tenloaixe;
                m.TenHanhTrinh = item.HanhTrinh.MoTa;
                m.TenChang = item.ToMoTaHanhTrinhGiaVe();
                m.SoKhach = _baocaoService.GetTongDoanhThuChangTheoChuyenDi(item.Id, xexuatbens.Select(c => c.Id).ToArray());
                m.DoanhThu = _baocaoService.GetTongDoanhThuChangTheoTuyen(item.Id, xexuatbens.Select(c => c.Id).ToArray());

                dtchang.Add(m);
            }
            dtchang = dtchang.Where(c => c.SoKhach > 0).ToList();

            foreach (var _dt in dtchang)
            {
                var dr = dataReport.NewRow();
                dr[0] = _dt.TenHanhTrinh;
                dr[1] = _dt.TenLoaiXe;
                dr[2] = _dt.TenChang;
                dr[3] = _dt.SoKhach;
                dr[4] = _dt.DoanhThu.ToSoNguyen();

                dataReport.Rows.Add(dr);
            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult DoanhThuTheoChang()
        {

            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.DOANH_THU_CHANG;
            PrepareListHanhTrinhModel(model, true, false);
            PrepareListLoaiXeModel(model, true);
            model.TuNgay = DateTime.Now.Date.AddDays(-7);
            model.DenNgay = DateTime.Now.Date.AddDays(1);
            return View(model);
        }

        #endregion
        #region so quy tien mat
        void ThuChiToThuChiModel(ThuChi nvfrom, ThuChiModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.Ma = nvfrom.Ma;
            nvto.NgayTao = nvfrom.NgayTao;
            nvto.NgayGiaoDich = nvfrom.NgayGiaoDich;
            nvto.ChuyenDiId = nvfrom.ChuyenDiId.GetValueOrDefault(0);
            nvto.thongtinchuyendi = nvfrom.chuyendi != null ? nvfrom.chuyendi.toMoTa() : "";
            nvto.NgayDi = nvfrom.chuyendi != null ? nvfrom.chuyendi.NgayDi : DateTime.Now;
            nvto.LoaiThuChiId = nvfrom.LoaiThuChiId;
            nvto.tenloaithuchi = nvfrom.loaithuchi.Ten;
            nvto.DienGiai = nvfrom.DienGiai;
            nvto.GiaTri = nvfrom.GiaTri;
            nvto.NguoiNopId = nvfrom.NguoiNopId.GetValueOrDefault(0);
            nvto.tennguoinop = nvfrom.nguoinop != null ? nvfrom.nguoinop.HoVaTen : "";
            nvto.NguoiThuId = nvfrom.NguoiThuId.GetValueOrDefault(0);
            nvto.tennguoithu = nvfrom.nguoithu != null ? nvfrom.nguoithu.HoVaTen : "";
            nvto.NguoiTaoId = nvfrom.NguoiTaoId;
            nvto.tennguoitao = nvfrom.nguoitao.HoVaTen;
            nvto.isChi = nvfrom.isChi;
            nvto.isChiText = nvfrom.isChi ? "Chi" : "Thu";
        }
        BaoCaoNhaXeModel createBaoCaoSoQuyTienMat(BaoCaoNhaXeModel model)
        {

            model.addSumRight = false;
            model.addSumBottom = false;

            model.Title = new string[] { string.Format("SỔ QUỸ TIỀN MẶT  NGÀY {0} ĐẾN {1}", model.TuNgay.ToNgayThangVN(), model.DenNgay.ToNgayThangVN()) };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add("Mã");
            headers.Add("Ngày tạo");
            headers.Add("Ngày thực hiện");
            headers.Add("Thông tin xe");
            headers.Add("Mục");
            headers.Add("Diễn giải");
            headers.Add("Thu(VNĐ)");
            headers.Add("Chi(VNĐ)");
            headers.Add("Tồn(VNĐ)");
            model.headers = headers.ToArray();
            //lay thong tin xe
            //tao du lieu trong bao cao            
            DataTable dataReport = ToDataTableSimple(model.headers.Length);

            DateTime ngaydauthang = model.TuNgay;
            DateTime ngaycuoithang = model.DenNgay;

            bool? isChi = null;
            if (model.isChi >= 0)
            {
                isChi = model.isChi == 1 ? true : false;
            }
            var tondauki = _ketoanService.GetTonDauKy(_workContext.NhaXeId, model.TuNgay);
            var danhsachs = _ketoanService.GetAllThuChi(_workContext.NhaXeId, 0, isChi, model.TuNgay, model.DenNgay, model.KeySearch);
            var thuchis = danhsachs.Select(c =>
              {
                  var m = new ThuChiModel();
                  ThuChiToThuChiModel(c, m);
                  return m;
              }).ToList();
            var tondaukifirst = tondauki;
            foreach (var item in thuchis)
            {
                if (item.isChi)
                {
                    tondauki = tondauki - item.GiaTri;
                }
                else
                {
                    tondauki = tondauki + item.GiaTri;
                }
                var dr = dataReport.NewRow();
                dr[0] = item.Ma;
                dr[1] = item.NgayTao.ToString("dd/MM/yyyy HH:mm");
                dr[2] = item.NgayDi.ToString("dd/MM/yyyy");
                dr[3] = item.thongtinchuyendi;
                dr[4] = item.tenloaithuchi;
                dr[5] = item.DienGiai;
                string thu = "";
                string chi = "";
                if (item.isChi)
                {
                    chi = item.GiaTri.ToSoNguyen();
                }
                else
                {
                    thu = item.GiaTri.ToSoNguyen();
                }
                dr[6] = thu;
                dr[7] = chi;
                dr[8] = tondauki;
                dataReport.Rows.Add(dr);
            }
            model.topPage = "Tồn đầu kì: " + tondaukifirst.ToSoNguyen() + "   Tồn cuối kì: " + tondauki.ToSoNguyen();
            model.dataReport = dataReport;

            return model;
        }
        #endregion
        #region Bao cao luot xe chay
        BaoCaoNhaXeModel createLuotNhanVien(BaoCaoNhaXeModel model)
        {
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { string.Format("THỐNG KÊ LƯỢT ĐI LÁI/PHỤ XE THÁNG {0} NĂM {1}", model.ThangId, model.NamId) };
            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add(@"Tên lái/phụ xe\Ngày");
            DateTime ngaydauthang = new DateTime(model.NamId, model.ThangId, 1);
            DateTime ngaycuoithang = ngaydauthang.AddMonths(1).AddDays(-1);
            for (var dt = ngaydauthang; dt <= ngaycuoithang; dt = dt.AddDays(1))
            {
                headers.Add(dt.Day.ToString());
            }
            model.headers = headers.ToArray();
            //lay thong tin xe
            //tao du lieu trong bao cao
            var lsnhanvien = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, new ENKieuNhanVien[] { ENKieuNhanVien.LaiXe, ENKieuNhanVien.PhuXe });
            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            List<HistoryXeXuatBen> xexuatbens = null, xexuatbentemps = null;
            xexuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, 0, null, null, ngaydauthang, ngaycuoithang);

            foreach (var nv in lsnhanvien)
            {
                var dr = dataReport.NewRow();
                dr[0] = string.Format("{0} ({1})", nv.HoVaTen, nv.DienThoai);
                xexuatbentemps = xexuatbens.Where(c => c.LaiPhuXes.Any(lx => lx.NhanVien_Id == nv.Id)).ToList();
                for (var dt = ngaydauthang; dt <= ngaycuoithang; dt = dt.AddDays(1))
                {
                    dr[dt.Day] = xexuatbentemps.Count(c => c.NgayDi.Day == dt.Day).ToString();
                }
                dataReport.Rows.Add(dr);
            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult LuotNhanVien()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.LUOT_NHAN_VIEN;
            PrepareListNgayThangNam(model);
            return View(model);
        }

        #endregion
         #region doanh thu hang hoa theo van phong hang ngay
        void prepareTuyen(BaoCaoNhaXeModel model)
        {
            //chon tuyen
            model.ListTuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.Ten;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            var hanhtrinhall = new SelectListItem();
            hanhtrinhall.Text = "Chọn tuyến ";
            hanhtrinhall.Value = "0";
            model.ListTuyens.Insert(0, hanhtrinhall);

        }
        public ActionResult DoanhThuHangHoaVanPhong()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
            {
                if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                    return AccessDeniedView();
            }

            var model = new BaoCaoNhaXeModel();
            model.NgayGuiHang = DateTime.Now;
            //loc tuyen
            prepareTuyen(model);
            //loc van phong
            PrepareListVanPhongModel(model, false);

            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.HANG_HOA_VAN_PHONG;
            return View(model);
        }
       
          void PrepareListVanPhongModel(BaoCaoNhaXeModel model, bool isGetTruSo = true, bool isChonVP = true)
        {
            if (this.isRightAccess(_permissionService, StandardPermissionProvider.CVBaoCao))
            {
                var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
                if (!isGetTruSo)
                    vanphongs = vanphongs.Where(c => c.KieuVanPhong == ENKieuVanPhong.VanPhong).ToList();
                model.VanPhongs = vanphongs.Select(c => new SelectListItem
                {
                    Text = c.TenVanPhong,
                    Value = c.Id.ToString(),
                }).ToList();

                if (isChonVP)
                    model.VanPhongs.Insert(0, new SelectListItem { Value = "0", Text = "----------Tất cả----------" });
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
        BaoCaoNhaXeModel createDoanhThuHangHoaVanPhong(BaoCaoNhaXeModel model)
        {

            //cau hinh bao cao
            model.isShowSTT = true;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { "BÁO CÁO DOANH THU NGÀY " + model.NgayGuiHang.ToString("dd/MM/yyyy") };

            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add("TUYẾN");
            headers.Add("LOẠI PHIẾU");

            headers.Add("VP TRẢ HÀNG");
            headers.Add("SỐ LỆNH");
            headers.Add("SỐ XE");
            headers.Add("GIỜ CHẠY");
            //add by lent
            headers.Add("NGƯỜI GỬI");
            headers.Add("NGƯỜI NHẬN");
            /////
            headers.Add("SL HÀNG");
            model.idxColForSum = 9;
            headers.Add("CƯỚC ĐÃ TT");
            headers.Add("CƯỚC CHƯA TT");
            var tovanchuyen = _phieuchuyenphatService.GetAllToVanChuyen(_workContext.NhaXeId);
            foreach (var item in tovanchuyen)
            {
                headers.Add(item.TenTo);
            }
                headers.Add("CƯỚC VƯỢT TUYẾN");
            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            foreach (var m in tuyens)
            {
                headers.Add("Còn lại " + m.TenVietTat);
            }
            headers.Add("THỰC THU");
            model.addSumBottom = true;
            model.addSumRight = false;
            //loc thong tin xe di theo hanh trinh,
            //lay tat ca tuyen
            model.TitleColSpan = new List<string[]>();
            //add header colspan ngay
            //model.TitleColSpan.Add(new string[] { "", "1", "" });          
            var giaodich = _phieuchuyenphatService.GetAllPhieuChuyenPhatTrongThang(_workContext.NhaXeId, model.VanPhongId, model.NgayGuiHang, model.KeySearch, 0, null, null, 0, 0, null, true, model.TuyenId);


            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);

            foreach (var nv in giaodich)
            {
                var dr = dataReport.NewRow();
                int i = 0;
                string tuyen = "";
                var Nhatki = new PhieuChuyenPhatVanChuyen();
                if (nv.nhatkyvanchuyens.Count > 0)
                {
                    Nhatki = nv.nhatkyvanchuyens.FirstOrDefault();
                    tuyen = Nhatki.chuyendi.HanhTrinh.tuyen.TenVietTat;

                }
                else
                    Nhatki = null;
                dr[i] = tuyen;
                i++;
                dr[i] = nv.LoaiPhieu.ToCVEnumText(_localizationService);
                i++;
                dr[i] = nv.VanPhongNhan.Ma;
                i++;

                dr[i] = nv.phieuvanchuyen != null ? nv.phieuvanchuyen.SoLenh : "";
                i++;
                dr[i] = Nhatki != null ? Nhatki.chuyendi.xevanchuyen.BienSo : "";
                i++;
                dr[i] = Nhatki != null ? Nhatki.chuyendi.NgayDi.ToString("HH:mm") : "";
                i++;

                dr[i] = nv.NguoiGui.toText();
                i++;
                dr[i] = nv.NguoiNhan.toText();
                i++;

                dr[i] = nv.TenHang;
                i++;
                dr[i] = nv.TongCuocDaThanhToan.ToSoNguyen();
                i++;
                dr[i] = (nv.TongTienCuoc - nv.TongCuocDaThanhToan).ToSoNguyen();
                i++;

                foreach (var item in tovanchuyen)
                {
                    decimal cuocvc = 0;
                    if (nv.CuocTanNoi > 0)
                    {
                        if (nv.ToVanChuyenTraId == null)
                        {
                            //add by mai: lay gan to van chuyen trong khu vuc cua van phong nhan
                            var tovc = nv.VanPhongNhan.tovanchuyens.FirstOrDefault();
                            if (tovc != null)
                                nv.ToVanChuyenTraId = tovc.Id;
                        }
                    }
                    if (item.Id == nv.ToVanChuyenTraId)
                        cuocvc = nv.CuocTanNoi;

                    if (item.Id == nv.ToVanChuyenNhanId)
                        cuocvc = nv.CuocNhanTanNoi;
                    dr[i] = cuocvc.ToSoNguyen();
                    i++;
                }
                //cuoc vuot tuyen

                dr[i] = nv.CuocVuotTuyen.ToSoNguyen();
                i++;

                ///////////////////////
                decimal cong = 0;
                foreach (var m in tuyens)
                {
                    decimal cuoctuyen = 0;
                    if (Nhatki != null)
                    {
                        if (Nhatki.phieuvanchuyen.LoaiPhieuVanChuyen == ENLoaiPhieuVanChuyen.TrongTuyen)
                        {
                            if (m.Id == Nhatki.hanhtrinh.TuyenHanhTrinhId)
                            {
                                cuoctuyen = nv.TongTienCuoc - nv.CuocTanNoi - nv.CuocVuotTuyen - nv.CuocNhanTanNoi;
                            }
                        }
                        else
                        {

                            var cuoc1 = (nv.CuocGiaTri + nv.CuocCapToc) / 2;

                            if (m.Id == Nhatki.hanhtrinh.TuyenHanhTrinhId)
                            {
                                //cuoc tuyen 1=tongcuoc-cuocvanchuyen-(cuoccaptoc+cuocgiatri)/2

                                cuoctuyen = nv.TongTienCuoc - nv.CuocTanNoi - nv.CuocVuotTuyen - nv.CuocNhanTanNoi - cuoc1;
                            }
                            if (nv.nhatkyvanchuyens.Count() > 1)
                            {
                                if (m.Id == nv.nhatkyvanchuyens.LastOrDefault().hanhtrinh.TuyenHanhTrinhId)
                                {
                                    //cuoc tuyen 2=cuocvuottuyen+(cuoccaptoc+cuocgiatri)/2
                                    cuoctuyen = nv.CuocVuotTuyen + cuoc1;
                                }
                            }
                        }
                    }
                    dr[i] = cuoctuyen.ToSoNguyen();
                    i++;
                    cong = cong + cuoctuyen;
                }
                dr[i] = cong.ToSoNguyen();
                dataReport.Rows.Add(dr);
            }
            model.dataReport = dataReport;

            return model;
        }
        #endregion
        #region doanh thu hang hoa theo van phong hang thang
        public ActionResult DoanhThuHangHoaTongHop()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            //loc van phong
            PrepareListVanPhongModel(model, false,true);
            model.TuNgay = DateTime.Now.NgayDauThang();
            model.DenNgay = DateTime.Now.AddDays(1);
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.HANG_HOA_TONG_HOP;
            return View(model);
        }


        BaoCaoNhaXeModel createDoanhThuHangHoaTongHop(BaoCaoNhaXeModel model)
        {

            //cau hinh bao cao
            model.isShowSTT = true;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { "BÁO CÁO DOANH THU TỪ NGÀY " + model.TuNgay.ToString("dd/MM/yyyy") + "ĐẾN NGÀY" + model.DenNgay.ToString("dd/MM/yyyy") };

            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            headers.Add("NGÀY");
            headers.Add("TÊN");

            headers.Add("NHÓM PHIẾU");
            headers.Add("DTTB/GD");
            headers.Add("SỐ GD NHẬN");
            headers.Add("TỔNG CƯỚC");

            var tovanchuyen = _phieuchuyenphatService.GetAllToVanChuyen(_workContext.NhaXeId);
            foreach (var item in tovanchuyen)
            {
                headers.Add(item.TenTo);
            }

            headers.Add("CƯỚC VƯỢT TUYẾN");



            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            foreach (var m in tuyens)
            {
                headers.Add("Còn lại " + m.TenVietTat);
            }
            headers.Add("THỰC THU");
            model.addSumBottom = false;
            // model.addSumRight = true;
            //loc thong tin xe di theo hanh trinh,
            //lay tat ca tuyen
            model.TitleColSpan = new List<string[]>();
            //add header colspan ngay



            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            for (var dt = model.TuNgay; dt <= model.DenNgay; dt = dt.AddDays(1))
            {
                CreateDTTongHop(dataReport, false, dt, model, tovanchuyen, tuyens);
            }

            CreateDTTongHop(dataReport, true, DateTime.Now, model, tovanchuyen, tuyens);
            model.dataReport = dataReport;

            return model;
        }
        void CreateDTTongHop(DataTable dataReport, bool isthang, DateTime dt, BaoCaoNhaXeModel model, List<ToVanChuyen> tovanchuyen, List<TuyenHanhTrinh> tuyens)
        {
            var giaodichs = new List<PhieuChuyenPhat>();
            var giaodichtra = new List<PhieuChuyenPhat>();
            if (!isthang)
            {
                giaodichs = _phieuchuyenphatService.GetAllPhieuChuyenPhatTrongThang(_workContext.NhaXeId, model.VanPhongId, dt, model.KeySearch);
                giaodichtra = _phieuchuyenphatService.GetAllPhieuChuyenPhatTrongThang(_workContext.NhaXeId, 0, null, model.KeySearch, model.VanPhongId, null, null, 0, 0, dt);

            }
            else
            {
                giaodichs = _phieuchuyenphatService.GetAllPhieuChuyenPhatTrongThang(_workContext.NhaXeId, model.VanPhongId, null, model.KeySearch, 0, model.TuNgay, model.DenNgay);
                giaodichtra = _phieuchuyenphatService.GetAllPhieuChuyenPhatTrongThang(_workContext.NhaXeId, 0, null, model.KeySearch, model.VanPhongId, model.TuNgay, model.DenNgay, 0, 0, null, false);
            }

            var nhomphieu = this.GetCVEnumSelectList<ENNhomPhieuChuyenPhat>(_localizationService, 0, false).Where(c => c.Value != Convert.ToString((int)ENLoaiPhieuChuyenPhat.All)).ToList(); ;
            DataTable dataReport1 = ToDataTableSimple(model.headers.Length);
            DataTable dataReport2 = ToDataTableSimple(model.headers.Length);
            foreach (var np in nhomphieu)
            {
                var arrgdvp = giaodichs.Where(c => c.NhomPhieuId == Convert.ToInt32(np.Value));
                //var  phieufirst=new PhieuChuyenPhat ();
                //if (arrgdvp.Count() > 0)
                //    phieufirst = arrgdvp.ToList().First().phieuchuyenphat;
                var dr = dataReport.NewRow();
                int i = 0;
                dr[i] = dt.Day;
                if (isthang)
                    dr[i] = "Tổng";

                i++;
                string loaiphieutext = "";
                if (Convert.ToInt32(np.Value) < 20)
                    loaiphieutext = "VP nhận";
                else loaiphieutext = "TN nhận";
                dr[i] = loaiphieutext;
                i++;
                dr[i] = np.Text;
                i++;
                decimal tongcuoc = arrgdvp.Sum(c => c.TongTienCuoc);
                int sogd = arrgdvp.Count();
                decimal tb = 0;
                if (sogd > 0)
                    tb = tongcuoc / sogd;

                dr[i] = tb.ToSoNguyen();
                i++;
                dr[i] = sogd;
                i++;
                dr[i] = tongcuoc.ToSoNguyen();
                i++;

                foreach (var item in tovanchuyen)
                {
                    var cuocvc = arrgdvp.Where(c => c.ToVanChuyenNhanId == item.Id).Sum(c => c.CuocNhanTanNoi);

                    var cuocvctra = arrgdvp.Where(c => c.ToVanChuyenTraId == item.Id).Sum(c => c.CuocTanNoi);

                    cuocvc = cuocvc + cuocvctra;
                    dr[i] = cuocvc.ToSoNguyen();
                    i++;
                }
                //cuoc vuot tuyen               
                decimal cuocvt = arrgdvp.Sum(c => c.CuocVuotTuyen);
                dr[i] = cuocvt.ToSoNguyen();
                i++;
                decimal ThucThu = 0;
                foreach (var m in tuyens)
                {
                    decimal cuoctuyen = 0;
                    var CuocTuyenTong = arrgdvp.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.TongTienCuoc);
                    var CuocPhi = arrgdvp.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocPhi);
                    var CuocTuyenVC = arrgdvp.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocTanNoi);

                    var CuocTuyenVCNhan = arrgdvp.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocNhanTanNoi);

                    cuoctuyen = CuocTuyenTong - CuocTuyenVC - CuocTuyenVCNhan;
                    if (np.Value == ((int)ENNhomPhieuChuyenPhat.TN_VT).ToString() || np.Value == ((int)ENNhomPhieuChuyenPhat.VP_VT).ToString())
                    {
                        // cuoc tuyen 1= cuocphi-cuocvanchuyen+ 1/2 (cuoccaptoc+cuocgiatri)     
                        if (arrgdvp.Any(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id))
                        {
                            var CuocCapToc = arrgdvp.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocCapToc);
                            var CuocTriGia = arrgdvp.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocGiaTri);
                            cuoctuyen = CuocPhi + (CuocCapToc + CuocTriGia) / 2;
                        }
                        // cuoc tuyen 2
                        if (arrgdvp.Any(c => c.nhatkyvanchuyens.Count() > 1 && c.nhatkyvanchuyens.LastOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id))
                        {
                            var CuocCapToc = arrgdvp.Where(c => c.nhatkyvanchuyens.LastOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocCapToc);
                            var CuocTriGia = arrgdvp.Where(c => c.nhatkyvanchuyens.LastOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocGiaTri);
                            var CuocVT = arrgdvp.Where(c => c.nhatkyvanchuyens.LastOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocVuotTuyen);
                            cuoctuyen = CuocVT + (CuocCapToc + CuocTriGia) / 2;
                        }
                    }
                    dr[i] = cuoctuyen.ToSoNguyen();
                    i++;
                    ThucThu = ThucThu + cuoctuyen;
                }
                dr[i] = ThucThu.ToSoNguyen();
                dataReport.Rows.Add(dr);
                if (Convert.ToInt32(np.Value) < 20)
                {
                    dataReport1.ImportRow(dr);
                }

                else
                    dataReport2.ImportRow(dr);

            }
            var loaiphieu = this.GetCVEnumSelectList<ENLoaiPhieuChuyenPhat>(_localizationService, 0, false).Where(c => c.Value != Convert.ToString((int)ENLoaiPhieuChuyenPhat.All)).ToList();
            DataTable dataReport3 = ToDataTableSimple(model.headers.Length);
            foreach (var np in loaiphieu)
            {
                DataTable dataReport4 = dataReport1.Copy();
                if (Convert.ToInt32(np.Value) == (int)ENLoaiPhieuChuyenPhat.ThuTanNoi)
                    dataReport4 = dataReport2.Copy();
                //tong hop
                var arrgdvp = giaodichs.Where(c => c.LoaiPhieuId == Convert.ToInt32(np.Value));

                var dr1 = dataReport.NewRow();
                int i = 0;
                dr1[i] = dt.Day;
                if (isthang)
                    dr1[i] = "Tổng";
                i++;
                dr1[i] = "Cộng";
                i++;
                dr1[i] = np.Text; ;
                i++;
                decimal tongcuoc = arrgdvp.Sum(c => c.TongTienCuoc);
                int sogd = arrgdvp.Count();
                decimal tb = 0;
                if (sogd > 0)
                    tb = tongcuoc / sogd;

                dr1[i] = tb.ToSoNguyen();
                i++;
                dr1[i] = sogd;
                i++;
                dr1[i] = tongcuoc.ToSoNguyen();
                i++;
                foreach (var item in tovanchuyen)
                {
                    decimal sum = 0;
                    //object sumObject;
                    //sumObject = dataReport4.Compute("Sum(Col20)", "");

                    foreach (DataRow dr in dataReport4.Rows)
                    {

                        sum += Convert.ToInt32(dr[i].ToInt());

                    }
                    dr1[i] = sum.ToSoNguyen();
                    i++;
                }
                //cuoc vuot tuyen                

                decimal sumvt2 = 0;
                foreach (DataRow dr in dataReport4.Rows)
                {

                    sumvt2 += Convert.ToInt32(dr[i].ToInt());

                }
                dr1[i] = sumvt2.ToSoNguyen();
                i++;


                foreach (var m in tuyens)
                {
                    decimal sum = 0;
                    foreach (DataRow dr in dataReport4.Rows)
                    {

                        sum += Convert.ToInt32(dr[i].ToInt());

                    }
                    dr1[i] = sum.ToSoNguyen();
                    i++;

                }
                decimal ThucThuTong = 0;
                foreach (DataRow dr in dataReport4.Rows)
                {

                    ThucThuTong += Convert.ToInt32(dr[i].ToInt());

                }
                dr1[i] = ThucThuTong.ToSoNguyen();
                dataReport.Rows.Add(dr1);
                dataReport3.ImportRow(dr1);

            }

            var dr2 = dataReport.NewRow();
            int k = 0;
            dr2[k] = dt.Day;
            if (isthang)
                dr2[k] = "Tổng";
            k++;
            dr2[k] = "Tổng ngày";
            k++;
            dr2[k] = "";
            k++;
            decimal tongcuocngay = giaodichs.Sum(c => c.TongTienCuoc);
            int sogdngay = giaodichs.Count();
            decimal tbcuoc = 0;
            if (sogdngay > 0)
                tbcuoc = tongcuocngay / sogdngay;
            dr2[k] = tbcuoc.ToSoNguyen();
            k++;
            dr2[k] = sogdngay;
            k++;
            dr2[k] = tongcuocngay.ToSoNguyen();
            k++;
            foreach (var item in tovanchuyen)
            {
                decimal sum = 0;
                foreach (DataRow dr in dataReport3.Rows)
                {

                    sum += Convert.ToInt32(dr[k].ToInt());
                }
                dr2[k] = sum.ToSoNguyen();
                k++;
            }
            //cuoc vuot tuyen
            decimal sumvt = 0;
            foreach (DataRow dr in dataReport3.Rows)
            {
                sumvt += Convert.ToInt32(dr[k].ToInt());
            }
            dr2[k] = sumvt.ToSoNguyen();
            k++;
            foreach (var m in tuyens)
            {
                decimal sum = 0;
                foreach (DataRow dr in dataReport3.Rows)
                {
                    sum += Convert.ToInt32(dr[k].ToInt());
                }
                dr2[k] = sum.ToSoNguyen();
                k++;
            }
            decimal ThucThuNgay = 0;
            foreach (DataRow dr in dataReport3.Rows)
            {

                ThucThuNgay += Convert.ToInt32(dr[k].ToInt());

            }
            dr2[k] = ThucThuNgay.ToSoNguyen();
            dataReport.Rows.Add(dr2);
            //giao dich tra hang
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId).Take(16);
            var dr3 = dataReport.NewRow();
            int p = 0;
            dr3[p] = dt.Day;
            if (isthang)
                dr3[p] = "Tổng";
            p++;
            dr3[p] = "GD trả hàng";
            p++;
            dr3[p] = "Văn phòng";
            p++;
            dr3[p] = "Tổng";
            p++;

            foreach (var item in vanphongs)
            {

                dr3[p] = item.Ma;
                p++;
            }

            dataReport.Rows.Add(dr3);
            //count gd tra
            var dr4 = dataReport.NewRow();
            int v = 0;
            dr4[v] = dt.Day;
            v++;
            dr4[v] = "";
            v++;
            dr4[v] = "Số GD";
            v++;
            dr4[v] = giaodichtra.Count();
            v++;

            foreach (var item in vanphongs)
            {
                int sogd = 0;
                sogd = giaodichtra.Where(c => c.VanPhongNhanId == item.Id).Count();
                dr4[v] = sogd;
                v++;
            }
            dataReport.Rows.Add(dr4);
            //tao tong hop to van chuyen nhan
           
        }

        #endregion
        #region doanh thu phieu van chuyen
        public ActionResult BaoCaoPhieuVanChuyen()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
                return AccessDeniedView();
            var model = new BaoCaoNhaXeModel();
            prepareTuyen(model);
            model.TuNgay = DateTime.Now.NgayDauThang();
            model.DenNgay = DateTime.Now.AddDays(1);
            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.BAO_CAO_PHIEU_VAN_CHUYEN;
            return View(model);
        }
        BaoCaoNhaXeModel createBaoCaoPhieuVanChuyen(BaoCaoNhaXeModel model)
        {

            //cau hinh bao cao
            model.isShowSTT = true;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { "BÁO CÁO PHIẾU VẬN CHUYỂN TỪ NGÀY " + model.TuNgay.ToString("dd/MM/yyyy") + "ĐẾN NGÀY" + model.DenNgay.ToString("dd/MM/yyyy") };

            //lay thong tin ngay trong thang thong ke
            //tao header cua bao cao
            var headers = new List<String>();
            if (model.TuyenId == 0)
                headers.Add("Tuyến");
            headers.Add("SỐ XE");
            headers.Add("BẾN");
            headers.Add("GIỜ XB");
            model.idxColForSum = 4;
            headers.Add("SỐ PXN");
            headers.Add("SỐ LVD");
            headers.Add("K. BẾN");
            headers.Add("LÁI XE");
            headers.Add("PHỤ XE");
            headers.Add("GHI CHÚ");

            model.addSumBottom = true;
            model.addSumRight = false;
            //loc thong tin xe di theo hanh trinh,
            //lay tat ca tuyen
            model.TitleColSpan = new List<string[]>();
            //add header colspan ngay
            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            int[] hanhtrinhids = null;
            if (model.TuyenId > 0)
            {
                hanhtrinhids = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, 0, model.TuyenId).Select(c => c.Id).ToArray();
            }
            var xexuatbens = _baocaoService.GetXeXuatBens(_workContext.NhaXeId, -1, hanhtrinhids, null, model.TuNgay, model.DenNgay);
            if (!string.IsNullOrEmpty(model.KeySearch))
            {
                xexuatbens = xexuatbens.Where(c => c.LaiPhuXes.Any(l => l.nhanvien.HoVaTen.Contains(model.KeySearch)) || (c.XeVanChuyenId > 0 && c.xevanchuyen.BienSo.Contains(model.KeySearch))).ToList();
            }
            foreach (var chuyen in xexuatbens)
            {

                var dr = dataReport.NewRow();
                int i = 0;
                if (model.TuyenId == 0)
                {
                    dr[i] = chuyen.HanhTrinh.tuyen.TenVietTat;
                    i++;
                }


                dr[i] = chuyen.xevanchuyen.BienSo;
                i++;
                dr[i] = chuyen.HanhTrinh.DiemDons.First().diemdon.benxe != null ? chuyen.HanhTrinh.DiemDons.First().diemdon.benxe.TenBenXe : chuyen.HanhTrinh.DiemDons.First().diemdon.TenDiemDon;
                i++;


                dr[i] = chuyen.NgayDi.ToString("HH:mm");
                i++;
                //so lenh van danh
                var lenh = _phieuchuyenphatService.GetAllPhieuVanChuyenByChuyenId(chuyen.Id, _workContext.NhaXeId);
                dr[i] = lenh.Sum(c => c.phieuchuyenphats.Count());
                i++;
                dr[i] = lenh.Count();
                i++;
                dr[i] = "";
                i++;

                dr[i] = chuyen.ThongTinLaiPhuXe(0, true);
                i++;
                dr[i] = chuyen.ThongTinLaiPhuXe(1, true);
                i++;
                dr[i] = "";
                dataReport.Rows.Add(dr);
            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult LenhVanChuyenHangNgay()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
            {
                if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                    return AccessDeniedView();
            }

            var model = new BaoCaoNhaXeModel();
            model.NgayGuiHang = DateTime.Now;
            //loc tuyen
            prepareTuyen(model);
            //loc van phong
            PrepareListVanPhongModel(model);

            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.PHIEU_VAN_CHUYEN_NGAY;

            return View(model);
        }


        BaoCaoNhaXeModel createLenhVanChuyenNgay(BaoCaoNhaXeModel model)
        {

            //cau hinh bao cao
            model.isShowSTT = true;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { "BÁO CÁO TỔNG HỢP NGÀY THEO TUYẾN", string.Format("NGÀY {0} THÁNG {1} NĂM {2}", model.NgayGuiHang.Day, model.NgayGuiHang.Month, model.NgayGuiHang.Year) };
            model.TitleColSpan = new List<string[]>();
            model.addSumBottom = true;
            //add header colspan ngay
            var headers = new List<String>();
            //model.TitleColSpan.Add(new string[] { "", "3", "" });
            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            if (model.TuyenId > 0)
            {
                tuyens = tuyens.Where(c => c.Id == model.TuyenId).ToList();
            }
            model.idxColForSum = 0;
            foreach (var tuyen in tuyens)
            {
                model.TitleColSpan.Add(new string[] { tuyen.Ten, "3", "" });
                headers.Add("LVC");
                headers.Add("SỐ XE");
                headers.Add("SỐ TIỀN");
            }
            //lay thong tin ngay trong thang thong ke            
            var LenhVanChuyens = _phieuchuyenphatService.GetAllPhieuChuyenPhatVanChuyen(model.NgayGuiHang, model.NgayGuiHang, model.VanPhongId, 0, model.BienSoXe, model.SoLenh, model.TuyenId)

                .GroupBy(g => new { g.TuyenId, g.phieuvanchuyen.SoLenh, g.chuyendi.xevanchuyen.BienSo })
                .Select(c => new
                {
                    TuyenId = c.Key.TuyenId,
                    SoLenh = c.Key.SoLenh,
                    BienSo = c.Key.BienSo,
                    TongTien = c.Sum(a => a.TongCuoc)
                }).ToList();
            //tao header cua bao cao
            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            //bat dau tao data table
            while (LenhVanChuyens.Count > 0)
            {
                //tao tung row
                var dr = dataReport.NewRow();
                int col = 0;
                foreach (var tuyen in tuyens)
                {
                    //lay phan tu dau tien co tuyen =tuyen.id
                    var item = LenhVanChuyens.Where(c => c.TuyenId == tuyen.Id).FirstOrDefault();
                    if (item != null)
                    {
                        dr[col] = item.SoLenh;
                        dr[col + 1] = item.BienSo;
                        dr[col + 2] = item.TongTien.ToSoNguyen();
                        //loai bo ra khoi danh sach, de tiep tuc tao record moi
                        LenhVanChuyens.Remove(item);
                    }
                    col = col + 3;
                }
                dataReport.Rows.Add(dr);
            }

            model.dataReport = dataReport;

            return model;
        }
        public ActionResult LenhVanChuyenHangThang()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
            {
                if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                    return AccessDeniedView();
            }

            var model = new BaoCaoNhaXeModel();
            PrepareListNgayThangNam(model);
            //loc tuyen
            prepareTuyen(model);
            //loc van phong
            PrepareListVanPhongModel(model);

            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.PHIEU_VAN_CHUYEN_THANG;

            return View(model);
        }


        BaoCaoNhaXeModel createLenhVanChuyenThang(BaoCaoNhaXeModel model)
        {

            //cau hinh bao cao
            model.isShowSTT = true;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { "BÁO CÁO TỔNG HỢP THÁNG THEO TUYẾN", string.Format("THÁNG {0} NĂM {1}", model.ThangId, model.NamId) };
            model.TitleColSpan = new List<string[]>();
            model.addSumBottom = true;

            //add header colspan ngay
            var headers = new List<String>();
            //model.TitleColSpan.Add(new string[] { "", "3", "" });
            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            if (model.TuyenId > 0)
            {
                tuyens = tuyens.Where(c => c.Id == model.TuyenId).ToList();
            }
            model.TitleColSpan.Add(new string[] { "TỔNG HỢP THEO TUYẾN", tuyens.Count().ToString(), "" });
            model.idxColForSum = 0;
            foreach (var tuyen in tuyens)
            {
                headers.Add(tuyen.Ten);
            }
            //lay thong tin ngay trong thang thong ke       
            DateTime dt1 = new DateTime(model.NamId, model.ThangId, 1);
            DateTime dt2 = dt1.AddMonths(1).AddDays(-1);
            var LenhVanChuyens = _phieuchuyenphatService.GetAllPhieuChuyenPhatVanChuyen(dt1, dt2, model.VanPhongId, 0, model.BienSoXe, model.SoLenh, model.TuyenId);
            //tao header cua bao cao
            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            //bat dau tao data table
            for (var dt = dt1; dt <= dt2; dt = dt.AddDays(1))
            {
                var dr = dataReport.NewRow();
                int col = 0;
                foreach (var tuyen in tuyens)
                {
                    //lay phan tu dau tien co tuyen =tuyen.id
                    var item = LenhVanChuyens.Where(c => c.TuyenId == tuyen.Id && c.phieuchuyenphat.NgayNhanHang.Day == dt.Day).ToList();
                    if (item.Count > 0)
                    {
                        dr[col] = item.Sum(c => c.TongCuoc).ToSoNguyen();
                    }
                    col = col + 1;
                }
                dataReport.Rows.Add(dr);
            }
            model.dataReport = dataReport;

            return model;
        }

        #endregion
        #region chi tieu chuyen phat
        public ActionResult ChiTieuChuyenPhatThang()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
            {
                if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                    return AccessDeniedView();
            }

            var model = new BaoCaoNhaXeModel();
            PrepareListNgayThangNam(model);
            //loc tuyen
            prepareTuyen(model);
            //loc van phong
            PrepareListVanPhongModel(model);

            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.CHI_TIEU_THANG;

            return View(model);
        }
        BaoCaoNhaXeModel createChiTieuThang(BaoCaoNhaXeModel model)
        {

            //cau hinh bao cao
            model.isShowSTT = true;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { "CHỈ TIÊU CHUYỂN PHÁT", string.Format("THÁNG {0} NĂM {1}", model.ThangId, model.NamId) };
            model.TitleColSpan = new List<string[]>();
            //add header colspan ngay
            var headers = new List<String>();
            //model.TitleColSpan.Add(new string[] { "", "3", "" });
            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            if (model.TuyenId > 0)
            {
                tuyens = tuyens.Where(c => c.Id == model.TuyenId).ToList();
            }
            model.TitleColSpan.Add(new string[] { "", "2", "" });
            headers.Add("VP");
            headers.Add("Số nhân viên");
            model.TitleColSpan.Add(new string[] { "Xếp loại", "2", "" });
            headers.Add("Xếp loại");
            headers.Add("Hàng");
            model.TitleColSpan.Add(new string[] { "Số giao dịch", "3", "" });
            headers.Add("GD nhận hàng");
            headers.Add("GD trả hàng");
            headers.Add("Tổng GD");
            model.TitleColSpan.Add(new string[] { "Số GDTB/người", "4", "" });
            headers.Add("Số GD");
            headers.Add("Xếp thứ tự");
            headers.Add("Xếp loại");
            headers.Add("Mã hóa xếp loại");
            model.TitleColSpan.Add(new string[] { "Hiệu suất/GD", "4", "" });
            headers.Add("Hiệu suất");
            headers.Add("Xếp thứ tự");
            headers.Add("Xếp loại");
            headers.Add("Mã hóa xếp loại");
            model.TitleColSpan.Add(new string[] { "", "2", "" });
            headers.Add("Doanh thu");
            headers.Add("Doanh thu VC");
            //lay thong tin ngay trong thang thong ke       
            var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);

            //tao header cua bao cao
            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            //bat dau tao data table
            foreach (var item in vanphongs)
            {
                var dr = dataReport.NewRow();
                int col = 0;
                dr[col] = item.Ma;
                col = col + 4;

                var phieunhan = _phieuchuyenphatService.GetAllPhieuChuyenPhatTrongThang(_workContext.NhaXeId, item.Id, null, "", 0, null, null, model.ThangId, model.NamId);
                var GDNhan = phieunhan.Count();
                dr[col] = GDNhan.ToString();
                col++;
                var GDTra = _phieuchuyenphatService.GetAllPhieuChuyenPhatTrongThang(_workContext.NhaXeId, 0, null, "", item.Id, null, null, model.ThangId, model.NamId, null, false).Count();
                dr[col] = GDTra.ToString();
                col++;
                dr[col] = (GDTra + GDNhan).ToString();
                col = col + 9;
                dr[col] = phieunhan.Where(c => c.LoaiPhieu == ENLoaiPhieuChuyenPhat.ThuTaiVanPhong).Sum(c => (c.TongTienCuoc - c.CuocTanNoi - c.CuocNhanTanNoi)).ToSoNguyen();
                col++;
                dr[col] = phieunhan.Where(c => c.LoaiPhieu == ENLoaiPhieuChuyenPhat.ThuTanNoi).Sum(c => (c.TongTienCuoc - c.CuocTanNoi - c.CuocNhanTanNoi)).ToSoNguyen();
                dataReport.Rows.Add(dr);
            }
            model.dataReport = dataReport;

            return model;
        }
        #endregion
        #region Bao cao to van chuyen van phong
        public ActionResult BaoCaoToVanChuyen()
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVBaoCao))
            {
                if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHangHoaKiGui))
                    return AccessDeniedView();
            }

            var model = new BaoCaoNhaXeModel();
            PrepareListNgayThangNam(model);
            //loc tuyen
            prepareTuyen(model);
            //loc van phong
            PrepareListVanPhongModel(model);

            model.LoaiBaoCao = BaoCaoNhaXeModel.EN_LOAI_BAO_CAO.TO_VAN_CHUYEN_THANG;

            return View(model);
        }
        BaoCaoNhaXeModel createToVanchuyenThang(BaoCaoNhaXeModel model)
        {

            //cau hinh bao cao
            model.isShowSTT = true;
            model.topPage = GetTopPageOfReport();
            model.Title = new string[] { "BÁO CÁO TỔ VẬN CHUYỂN NHẬN", string.Format("THÁNG {0} NĂM {1}", model.ThangId, model.NamId) };
            model.TitleColSpan = new List<string[]>();
            model.addSumBottom = true;
            //add header colspan ngay
            var headers = new List<String>();
            headers.Add("TỔ VC");           
            headers.Add("SỐ GD NHẬN");
            headers.Add("TỔNG CƯỚC");

            var tovanchuyen = _phieuchuyenphatService.GetAllToVanChuyen(_workContext.NhaXeId);
            foreach (var item in tovanchuyen)
            {
                headers.Add(item.TenTo);
            }

            headers.Add("CƯỚC VƯỢT TUYẾN");



            var tuyens = _hanhtrinhService.GetAllTuyenByNhaXeId(_workContext.NhaXeId);
            foreach (var m in tuyens)
            {
                headers.Add("Còn lại " + m.TenVietTat);
            }
            headers.Add("THỰC THU");
            //lay thong tin ngay trong thang thong ke       
            var Tovcs = _phieuchuyenphatService.GetAllToVanChuyen(_workContext.NhaXeId);
            //tao header cua bao cao
            model.headers = headers.ToArray();
            //tao du lieu trong bao cao
            DataTable dataReport = ToDataTableSimple(model.headers.Length);
            //bat dau tao data table
            foreach (var item in Tovcs)
            {
                //get vp theo to vc
                var vps = item.tovanchuyenvps.Select(c => c.VanPhongId).ToArray();
                var dr = dataReport.NewRow();
                int col = 0;
                dr[col] = item.TenTo;
                col++;
                var phieunhan = _phieuchuyenphatService.GetAllPhieuChuyenPhatTrongThang(_workContext.NhaXeId, 0, null, "", 0, null, null, model.ThangId, model.NamId).Where(c=>c.LoaiPhieu==ENLoaiPhieuChuyenPhat.ThuTanNoi && vps.Contains(c.VanPhongGuiId)).ToList();
                var GDNhan = phieunhan.Count();
                dr[col] = GDNhan.ToString();
                col++;
                dr[col] = phieunhan.Sum(c => c.TongTienCuoc).ToSoNguyen();
                col++;
                foreach (var _item in tovanchuyen)
                {
                    var cuocvc = phieunhan.Where(c => c.ToVanChuyenNhanId == _item.Id).Sum(c => c.CuocNhanTanNoi);
                    var cuocvctra = phieunhan.Where(c => c.ToVanChuyenTraId == _item.Id).Sum(c => c.CuocTanNoi);
                    cuocvc = cuocvc + cuocvctra;
                    dr[col] = cuocvc.ToSoNguyen();
                    col++;
                }
                //cuoc vuot tuyen               
                decimal cuocvt = phieunhan.Sum(c => c.CuocVuotTuyen);
                dr[col] = cuocvt.ToSoNguyen();
                col++;
                decimal ThucThu = 0;
                var phieuthuong = phieunhan.Where(c => c.NhomPhieu != ENNhomPhieuChuyenPhat.TN_VT);
                foreach (var m in tuyens)
                {
                    decimal cuoctuyen = 0;
                    var CuocTuyenTong = phieuthuong.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.TongTienCuoc);
                    var CuocPhi = phieuthuong.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocPhi);
                    var CuocTuyenVC = phieuthuong.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocTanNoi);
                    var CuocTuyenVCNhan = phieuthuong.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocNhanTanNoi);
                    var PhieuVT = phieunhan.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id && c.NhomPhieu==ENNhomPhieuChuyenPhat.TN_VT);
                    cuoctuyen = CuocTuyenTong - CuocTuyenVC - CuocTuyenVCNhan;
                    decimal cuoctuyenvt = 0;
                    if (PhieuVT.Count()>0)
                    {
                        var CuocPhiVT = PhieuVT.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocPhi);
                        var CuocVT = PhieuVT.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocVuotTuyen);
                        var CuocCapToc = PhieuVT.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocCapToc);
                        var CuocTriGia = PhieuVT.Where(c => c.nhatkyvanchuyens.FirstOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id).Sum(c => c.CuocGiaTri);
                        // cuoc tuyen 1= cuocphi-cuocvanchuyen+ 1/2 (cuoccaptoc+cuocgiatri)                      
                        cuoctuyenvt =  CuocPhiVT + (CuocCapToc + CuocTriGia) / 2;
                        // cuoc tuyen 2
                        if (PhieuVT.Any(c => c.nhatkyvanchuyens.Count() > 1 && c.nhatkyvanchuyens.LastOrDefault().hanhtrinh.TuyenHanhTrinhId == m.Id))
                        {
                            cuoctuyenvt = CuocVT + (CuocCapToc + CuocTriGia) / 2;
                        }
                    }
                    cuoctuyen = cuoctuyen + cuoctuyenvt;
                    dr[col] = cuoctuyen.ToSoNguyen();
                    col++;
                    ThucThu = ThucThu + cuoctuyen;
                }
                dr[col] = ThucThu.ToSoNguyen();
                dataReport.Rows.Add(dr);
            }
            model.dataReport = dataReport;
            return model;
        }
        #endregion
    
    }
}