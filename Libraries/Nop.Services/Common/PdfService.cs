// RTL Support provided by Credo inc (www.credo.co.il  ||   info@credo.co.il)

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Core.Html;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Stores;
using Nop.Core.Domain.Chonves;
using Nop.Services.NhaXes;
using System.Text;
using Nop.Core.Domain.Customers;

namespace Nop.Services.Common
{
    /// <summary>
    /// PDF service
    /// </summary>
    public partial class PdfService : IPdfService
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly IWorkContext _workContext;
        private readonly IOrderService _orderService;
        private readonly IHanhTrinhService _hanhtrinhService;
        private readonly INhaXeService _nhaxeService;
        private readonly IXeInfoService _xeinfoService;
        private readonly IDiaChiService _diachiService;
        private readonly IPaymentService _paymentService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPriceFormatter _priceFormatter;
        private readonly ICurrencyService _currencyService;
        private readonly IMeasureService _measureService;
        private readonly IPictureService _pictureService;
        private readonly IProductService _productService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IStoreService _storeService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingContext;
        private readonly IWebHelper _webHelper;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;

        private readonly CatalogSettings _catalogSettings;
        private readonly CurrencySettings _currencySettings;
        private readonly MeasureSettings _measureSettings;
        private readonly PdfSettings _pdfSettings;
        private readonly TaxSettings _taxSettings;
        private readonly AddressSettings _addressSettings;

        #endregion

        #region Ctor

        public PdfService(ILocalizationService localizationService,
            ILanguageService languageService,
            IWorkContext workContext,
            INhaXeService nhaxeService,
             IDiaChiService diachiService,
            IHanhTrinhService hanhtrinhService,
            IOrderService orderService,
            IXeInfoService xeinfoService,
            IPaymentService paymentService,
            IDateTimeHelper dateTimeHelper,
            IPriceFormatter priceFormatter,
            ICurrencyService currencyService,
            IMeasureService measureService,
            IPictureService pictureService,
            IProductService productService,
            IProductAttributeParser productAttributeParser,
            IStoreService storeService,
            IStoreContext storeContext,
            ISettingService settingContext,
            IWebHelper webHelper,
            IAddressAttributeFormatter addressAttributeFormatter,
            CatalogSettings catalogSettings,
            CurrencySettings currencySettings,
            MeasureSettings measureSettings,
            PdfSettings pdfSettings,
            TaxSettings taxSettings,
            AddressSettings addressSettings)
        {
            this._localizationService = localizationService;
            this._languageService = languageService;
            this._nhaxeService = nhaxeService;
            this._xeinfoService = xeinfoService;
            this._diachiService = diachiService;
            this._workContext = workContext;
            this._hanhtrinhService = hanhtrinhService;
            this._orderService = orderService;
            this._paymentService = paymentService;
            this._dateTimeHelper = dateTimeHelper;
            this._priceFormatter = priceFormatter;
            this._currencyService = currencyService;
            this._measureService = measureService;
            this._pictureService = pictureService;
            this._productService = productService;
            this._productAttributeParser = productAttributeParser;
            this._storeService = storeService;
            this._storeContext = storeContext;
            this._settingContext = settingContext;
            this._webHelper = webHelper;
            this._addressAttributeFormatter = addressAttributeFormatter;
            this._currencySettings = currencySettings;
            this._catalogSettings = catalogSettings;
            this._measureSettings = measureSettings;
            this._pdfSettings = pdfSettings;
            this._taxSettings = taxSettings;
            this._addressSettings = addressSettings;
        }

        #endregion

        #region Utilities

        protected virtual Font GetFont()
        {
            //nopCommerce supports unicode characters
            //nopCommerce uses Free Serif font by default (~/App_Data/Pdf/FreeSerif.ttf file)
            //It was downloaded from http://savannah.gnu.org/projects/freefont
            string fontPath = Path.Combine(_webHelper.MapPath("~/App_Data/Pdf/"), _pdfSettings.FontFileName);
            var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            var font = new Font(baseFont, 10, Font.NORMAL);
            return font;
        }

        /// <summary>
        /// Get font direction
        /// </summary>
        /// <param name="lang">Language</param>
        /// <returns>Font direction</returns>
        protected virtual int GetDirection(Language lang)
        {
            return lang.Rtl ? PdfWriter.RUN_DIRECTION_RTL : PdfWriter.RUN_DIRECTION_LTR;
        }

        /// <summary>
        /// Get element alignment
        /// </summary>
        /// <param name="lang">Language</param>
        /// <param name="isOpposite">Is opposite?</param>
        /// <returns>Element alignment</returns>
        protected virtual int GetAlignment(Language lang, bool isOpposite = false)
        {
            //if we need the element to be opposite, like logo etc`.
            if (!isOpposite)
                return lang.Rtl ? Element.ALIGN_RIGHT : Element.ALIGN_LEFT;

            return lang.Rtl ? Element.ALIGN_LEFT : Element.ALIGN_RIGHT;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Print an order to PDF
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="languageId">Language identifier; 0 to use a language used when placing an order</param>
        /// <returns>A path of generates file</returns>
        public virtual string PrintOrderToPdf(PhoiVe _phoive, int languageId)
        {
            if (_phoive == null)
                throw new ArgumentNullException("order");
            var order = _orderService.GetOrderById(_phoive.OrderId);
            string fileName = string.Format("Ve{0}_{1}.pdf", order.OrderGuid, CommonHelper.GenerateRandomDigitCode(4));
            string filePath = Path.Combine(_webHelper.MapPath("~/content/files/ExportImport"), fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {

                PrintOrdersToPdf(fileStream, _phoive, languageId);
            }
            return filePath;
        }
       

        /// <summary>
        /// Print orders to PDF
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="orders">Orders</param>
        /// <param name="languageId">Language identifier; 0 to use a language used when placing an order</param>
        public virtual void PrintOrdersToPdf(Stream stream, PhoiVe _phoive, int languageId = 0)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (_phoive == null)
                throw new ArgumentNullException("Không có vé");
            //get thong tin nguon ve
            var order = _orderService.GetOrderById(_phoive.OrderId);
            if (order == null)
                throw new ArgumentNullException("Không có đơn hàng");
            var nguonvexe = _hanhtrinhService.GetNguonVeXeById(_phoive.NguonVeXeId);
            
            if (nguonvexe.ParentId > 0)
            {
                nguonvexe = _hanhtrinhService.GetNguonVeXeById(_phoive.NguonVeXeConId);
            }
            //get thong tin nha xe
            var nhaxe=_nhaxeService.GetNhaXeById(nguonvexe.NhaXeId);
           
             var diachi = _diachiService.GetById(nhaxe.DiaChiID);
            //get thong tin xe xuan ben
            var xexuatbenInfo = _nhaxeService.GetHistoryXeXuatBenByNguonVeId(nguonvexe.Id,_phoive.NgayDi);
            var TenXe="";
            var BienSo=""; 
            if (xexuatbenInfo != null)
            {
                var xeInfo = _xeinfoService.GetXeInfoById(xexuatbenInfo.XeVanChuyenId.GetValueOrDefault(0));
                TenXe = xeInfo.TenXe;
                BienSo = xeInfo.BienSo;
            }
            
                       
            //logo
            var logoPicture = _pictureService.GetPictureById(nhaxe.LogoID);
                var logoExists = logoPicture != null;           
            var doc = new Document();      
            var pdfWriter = PdfWriter.GetInstance(doc, stream);
            doc.Open();           
            //fonts
            var titleFont = GetFont();

            titleFont.Size = BaseFont.SUPERSCRIPT_SIZE;
            //font ten nha xe
            var TenNhaXe = GetFont();
            TenNhaXe.SetStyle("initial");
           
            //font thong tin
            var InfoFont= GetFont();
                      
            InfoFont.Size = 14;
            var font = GetFont();
            font.Size = 13;
           
            var pdfSettingsByStore = _settingContext.LoadSetting<PdfSettings>(order.StoreId);
            var lang = _workContext.WorkingLanguage;
           
            //header
            float[] widths = { 3,7 };
            var HeaderVeIn = new PdfPTable(widths);
            HeaderVeIn.DefaultCell.Border = Rectangle.NO_BORDER;
          
           if(logoExists)
           {
               var logoFilePath = _pictureService.GetThumbLocalPath(logoPicture, 0, false);
                    var logo = Image.GetInstance(logoFilePath);
                    logo.ScaleToFit(200, 100);
                   
                    PdfPCell CellLeft = new PdfPCell(logo);
                    CellLeft.Border = Rectangle.NO_BORDER;
                    CellLeft.FixedHeight = 80f;
                    HeaderVeIn.AddCell(CellLeft);
           }
           PdfPCell CellRight = new PdfPCell();
           CellRight.Border = Rectangle.NO_BORDER;
           var ParaTenNhaXe = new Paragraph(string.Format(_localizationService.GetResource("PDFInvoice.TenNhaXe", lang.Id),
             nhaxe.TenNhaXe), font);
           ParaTenNhaXe.Alignment = Element.ALIGN_RIGHT;
           CellRight.AddElement(ParaTenNhaXe);
           var ParaDiaChi = new Paragraph(string.Format(_localizationService.GetResource("PDFInvoice.DiaChiNhXe", lang.Id),
             diachi.DiaChi1 + " " + diachi.DiaChi2), font);
           ParaDiaChi.Alignment = Element.ALIGN_RIGHT;
           CellRight.AddElement(ParaDiaChi);
           var ParaSdt = new Paragraph(string.Format(_localizationService.GetResource("PDFInvoice.SDTNhaXe", lang.Id),
            nhaxe.DienThoai), font);
           ParaSdt.Alignment = Element.ALIGN_RIGHT;
           CellRight.AddElement(ParaSdt);
           HeaderVeIn.AddCell(CellRight);
           doc.Add(HeaderVeIn);
           doc.Add(new Paragraph(""));
           doc.Add(new Paragraph(""));
            //add line
            var line=new iTextSharp.text.pdf.draw.LineSeparator(0,100,BaseColor.GRAY,Element.ALIGN_CENTER,1);
            doc.Add(new Chunk(line));
            //content
           Paragraph vexekhach = new Paragraph(_localizationService.GetResource("PDFInvoice.VeXeKhach", lang.Id),titleFont);
           vexekhach.Alignment = Element.ALIGN_CENTER;          
           doc.Add(vexekhach);
           doc.Add(new Paragraph(""));
           doc.Add(new Paragraph(""));
            var Table1 = new PdfPTable(2);
            float[] widthTable1 = new float[] { 1f, 2f };
            Table1.SetWidths(widthTable1);
            Table1.RunDirection = GetDirection(lang);
            Table1.DefaultCell.Border = Rectangle.NO_BORDER;            
            Table1.AddCell(new Paragraph(_localizationService.GetResource("PDFInvoice.Tuyen", lang.Id), font));
            Table1.AddCell(new Paragraph(nguonvexe.TenDiemDon + " -- " + nguonvexe.TenDiemDen, InfoFont));
            Table1.AddCell(new Paragraph(_localizationService.GetResource("PDFInvoice.GiaVe", lang.Id), font));
            Table1.AddCell(new Paragraph(nguonvexe.LichTrinhInfo.GiaVeToanTuyen.ToString() + " VNĐ", InfoFont));
            doc.Add(Table1);
            //add ghe,xe
            var Table2 = new PdfPTable(4);
             float[] widthTable2 = new float[] { 1f, 2f, 1f, 2f };
             Table2.SetWidths(widthTable2);
             Table2.RunDirection = GetDirection(lang);
             Table2.DefaultCell.Border = Rectangle.NO_BORDER;
             Table2.AddCell(new Paragraph(_localizationService.GetResource("PDFInvoice.SoXe", lang.Id), font));
             Table2.AddCell(new Paragraph(TenXe + "/" + BienSo, InfoFont));
             Table2.AddCell(new Paragraph(_localizationService.GetResource("PDFInvoice.SoGhe", lang.Id), font));
             Table2.AddCell(new Paragraph(_phoive.sodoghexequytac.Val + " ( Tầng" + _phoive.sodoghexequytac.Tang + ")", InfoFont));
             doc.Add(Table2);
            //add thoi gian khoi hanh
             var Table3 = new PdfPTable(4);
             float[] widthTable3 = new float[] { 2f, 1f, 1f, 2f };
             Table3.SetWidths(widthTable3);
             Table3.RunDirection = GetDirection(lang);
             Table3.DefaultCell.Border = Rectangle.NO_BORDER;
             Table3.AddCell(new Paragraph(_localizationService.GetResource("PDFInvoice.ThoiGianDi", lang.Id), font));
             Table3.AddCell(new Paragraph(nguonvexe.ThoiGianDi.ToString("HH:mm"), InfoFont));
             Table3.AddCell(new Paragraph(_localizationService.GetResource("PDFInvoice.NgayDi", lang.Id), font));
             Table3.AddCell(new Paragraph(_phoive.NgayDi.ToString("dd/MM/yyyy"), InfoFont));
             doc.Add(Table3);
            //add footer
            var FooterNgayBan = new Paragraph("Ngày "+DateTime.Now.Day+" tháng "+DateTime.Now.Month+" năm "+DateTime.Now.Year, font);
            FooterNgayBan.Alignment = Element.ALIGN_RIGHT;
            var FooterNguoiBanTitle = new Paragraph("Người bán vé", font);
            FooterNguoiBanTitle.Alignment = Element.ALIGN_RIGHT;
            var firtname = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
            var lastname = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
            var FooterTenNguoiBan = new Paragraph(firtname + " " + lastname, font);
            FooterTenNguoiBan.Alignment = Element.ALIGN_RIGHT;
            var Table4 = new PdfPTable(1);
            Table4.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPCell CellFooter = new PdfPCell();           
            CellFooter.Border = Rectangle.NO_BORDER;
            CellFooter.AddElement(FooterNgayBan);
            CellFooter.AddElement(FooterNguoiBanTitle);
            CellFooter.AddElement(FooterTenNguoiBan);
            Table4.AddCell(CellFooter);
            doc.Add(Table4);
            doc.Add(new Paragraph(" "));
            doc.NewPage();
            doc.Close();
          
          
               
            

            
        }

    

        /// <summary>
        /// Print packaging slips to PDF
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="shipments">Shipments</param>
        /// <param name="languageId">Language identifier; 0 to use a language used when placing an order</param>
        public virtual void PrintPackagingSlipsToPdf(Stream stream, IList<Shipment> shipments, int languageId = 0)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (shipments == null)
                throw new ArgumentNullException("shipments");

            var lang = _languageService.GetLanguageById(languageId);
            if (lang == null)
                throw new ArgumentException(string.Format("Cannot load language. ID={0}", languageId));

            var pageSize = PageSize.A4;

            if (_pdfSettings.LetterPageSizeEnabled)
            {
                pageSize = PageSize.LETTER;
            }

            var doc = new Document(pageSize);
            PdfWriter.GetInstance(doc, stream);
            doc.Open();

            //fonts
            var titleFont = GetFont();
            titleFont.SetStyle(Font.BOLD);
            titleFont.Color = BaseColor.BLACK;
            var font = GetFont();
            var attributesFont = GetFont();
            attributesFont.SetStyle(Font.ITALIC);

            int shipmentCount = shipments.Count;
            int shipmentNum = 0;

            foreach (var shipment in shipments)
            {
                var order = shipment.Order;

                if (languageId == 0)
                {
                    lang = _languageService.GetLanguageById(order.CustomerLanguageId);
                    if (lang == null || !lang.Published)
                        lang = _workContext.WorkingLanguage;
                }

                var addressTable = new PdfPTable(1);
                if (lang.Rtl)
                    addressTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                addressTable.DefaultCell.Border = Rectangle.NO_BORDER;
                addressTable.WidthPercentage = 100f;

                addressTable.AddCell(new Paragraph(String.Format(_localizationService.GetResource("PDFPackagingSlip.Shipment", lang.Id), shipment.Id), titleFont));
                addressTable.AddCell(new Paragraph(String.Format(_localizationService.GetResource("PDFPackagingSlip.Order", lang.Id), order.Id), titleFont));

                if (!order.PickUpInStore)
                {
                    if (order.ShippingAddress == null)
                        throw new NopException(string.Format("Shipping is required, but address is not available. Order ID = {0}", order.Id));

                    if (_addressSettings.CompanyEnabled && !String.IsNullOrEmpty(order.ShippingAddress.Company))
                        addressTable.AddCell(new Paragraph(String.Format(_localizationService.GetResource("PDFPackagingSlip.Company", lang.Id),
                                    order.ShippingAddress.Company), font));

                    addressTable.AddCell(new Paragraph(String.Format(_localizationService.GetResource("PDFPackagingSlip.Name", lang.Id),
                                order.ShippingAddress.FirstName + " " + order.ShippingAddress.LastName), font));
                    if (_addressSettings.PhoneEnabled)
                        addressTable.AddCell(new Paragraph(String.Format(_localizationService.GetResource("PDFPackagingSlip.Phone", lang.Id),
                                    order.ShippingAddress.PhoneNumber), font));
                    if (_addressSettings.StreetAddressEnabled)
                        addressTable.AddCell(new Paragraph(String.Format(_localizationService.GetResource("PDFPackagingSlip.Address", lang.Id),
                                    order.ShippingAddress.Address1), font));

                    if (_addressSettings.StreetAddress2Enabled && !String.IsNullOrEmpty(order.ShippingAddress.Address2))
                        addressTable.AddCell(new Paragraph(String.Format(_localizationService.GetResource("PDFPackagingSlip.Address2", lang.Id),
                                    order.ShippingAddress.Address2), font));

                    if (_addressSettings.CityEnabled || _addressSettings.StateProvinceEnabled || _addressSettings.ZipPostalCodeEnabled)
                        addressTable.AddCell(new Paragraph(String.Format("{0}, {1} {2}", order.ShippingAddress.City, order.ShippingAddress.StateProvince != null
                                        ? order.ShippingAddress.StateProvince.GetLocalized(x => x.Name, lang.Id)
                                        : "", order.ShippingAddress.ZipPostalCode), font));

                    if (_addressSettings.CountryEnabled && order.ShippingAddress.Country != null)
                        addressTable.AddCell(new Paragraph(String.Format("{0}", order.ShippingAddress.Country != null
                                        ? order.ShippingAddress.Country.GetLocalized(x => x.Name, lang.Id)
                                        : ""), font));

                    //custom attributes
                    var customShippingAddressAttributes = _addressAttributeFormatter.FormatAttributes(order.ShippingAddress.CustomAttributes);
                    if (!String.IsNullOrEmpty(customShippingAddressAttributes))
                    {
                        addressTable.AddCell(new Paragraph(HtmlHelper.ConvertHtmlToPlainText(customShippingAddressAttributes, true, true), font));
                    }
                }

                addressTable.AddCell(new Paragraph(" "));

                addressTable.AddCell(new Paragraph(String.Format(_localizationService.GetResource("PDFPackagingSlip.ShippingMethod", lang.Id), order.ShippingMethod), font));
                addressTable.AddCell(new Paragraph(" "));
                doc.Add(addressTable);

                var productsTable = new PdfPTable(3);
                productsTable.WidthPercentage = 100f;
                if (lang.Rtl)
                {
                    productsTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    productsTable.SetWidths(new[] { 20, 20, 60 });
                }
                else
                {
                    productsTable.SetWidths(new[] { 60, 20, 20 });
                }

                //product name
                var cell = new PdfPCell(new Phrase(_localizationService.GetResource("PDFPackagingSlip.ProductName", lang.Id), font));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                productsTable.AddCell(cell);

                //SKU
                cell = new PdfPCell(new Phrase(_localizationService.GetResource("PDFPackagingSlip.SKU", lang.Id), font));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                productsTable.AddCell(cell);

                //qty
                cell = new PdfPCell(new Phrase(_localizationService.GetResource("PDFPackagingSlip.QTY", lang.Id), font));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                productsTable.AddCell(cell);

                foreach (var si in shipment.ShipmentItems)
                {
                    var productAttribTable = new PdfPTable(1);
                    if (lang.Rtl)
                        productAttribTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    productAttribTable.DefaultCell.Border = Rectangle.NO_BORDER;

                    //product name
                    var orderItem = _orderService.GetOrderItemById(si.OrderItemId);
                    if (orderItem == null)
                        continue;

                    var p = orderItem.Product;
                    string name = p.GetLocalized(x => x.Name, lang.Id);
                    productAttribTable.AddCell(new Paragraph(name, font));
                    //attributes
                    if (!String.IsNullOrEmpty(orderItem.AttributeDescription))
                    {
                        var attributesParagraph = new Paragraph(HtmlHelper.ConvertHtmlToPlainText(orderItem.AttributeDescription, true, true), attributesFont);
                        productAttribTable.AddCell(attributesParagraph);
                    }
                    //rental info
                    if (orderItem.Product.IsRental)
                    {
                        var rentalStartDate = orderItem.RentalStartDateUtc.HasValue ? orderItem.Product.FormatRentalDate(orderItem.RentalStartDateUtc.Value) : "";
                        var rentalEndDate = orderItem.RentalEndDateUtc.HasValue ? orderItem.Product.FormatRentalDate(orderItem.RentalEndDateUtc.Value) : "";
                        var rentalInfo = string.Format(_localizationService.GetResource("Order.Rental.FormattedDate"),
                            rentalStartDate, rentalEndDate);

                        var rentalInfoParagraph = new Paragraph(rentalInfo, attributesFont);
                        productAttribTable.AddCell(rentalInfoParagraph);
                    }
                    productsTable.AddCell(productAttribTable);

                    //SKU
                    var sku = p.FormatSku(orderItem.AttributesXml, _productAttributeParser);
                    cell = new PdfPCell(new Phrase(sku ?? String.Empty, font));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    productsTable.AddCell(cell);

                    //qty
                    cell = new PdfPCell(new Phrase(si.Quantity.ToString(), font));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    productsTable.AddCell(cell);
                }
                doc.Add(productsTable);

                shipmentNum++;
                if (shipmentNum < shipmentCount)
                {
                    doc.NewPage();
                }
            }


            doc.Close();
        }

        /// <summary>
        /// Print product collection to PDF
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="products">Products</param>
        public virtual void PrintProductsToPdf(Stream stream, IList<Product> products)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (products == null)
                throw new ArgumentNullException("products");

            var lang = _workContext.WorkingLanguage;

            var pageSize = PageSize.A4;

            if (_pdfSettings.LetterPageSizeEnabled)
            {
                pageSize = PageSize.LETTER;
            }

            var doc = new Document(pageSize);
            PdfWriter.GetInstance(doc, stream);
            doc.Open();

            //fonts
            var titleFont = GetFont();
            titleFont.SetStyle(Font.BOLD);
            titleFont.Color = BaseColor.BLACK;
            var font = GetFont();

            int productNumber = 1;
            int prodCount = products.Count;

            foreach (var product in products)
            {
                string productName = product.GetLocalized(x => x.Name, lang.Id);
                string productDescription = product.GetLocalized(x => x.FullDescription, lang.Id);

                var productTable = new PdfPTable(1);
                productTable.WidthPercentage = 100f;
                productTable.DefaultCell.Border = Rectangle.NO_BORDER;
                if (lang.Rtl)
                {
                    productTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                }

                productTable.AddCell(new Paragraph(String.Format("{0}. {1}", productNumber, productName), titleFont));
                productTable.AddCell(new Paragraph(" "));
                productTable.AddCell(new Paragraph(HtmlHelper.StripTags(HtmlHelper.ConvertHtmlToPlainText(productDescription, decode: true)), font));
                productTable.AddCell(new Paragraph(" "));

                if (product.ProductType == ProductType.SimpleProduct)
                {
                    //simple product
                    //render its properties such as price, weight, etc
                    var priceStr = string.Format("{0} {1}", product.Price.ToString("0.00"), _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode);
                    if (product.IsRental)
                        priceStr = _priceFormatter.FormatRentalProductPeriod(product, priceStr);
                    productTable.AddCell(new Paragraph(String.Format("{0}: {1}", _localizationService.GetResource("PDFProductCatalog.Price", lang.Id), priceStr), font));
                    productTable.AddCell(new Paragraph(String.Format("{0}: {1}", _localizationService.GetResource("PDFProductCatalog.SKU", lang.Id), product.Sku), font));

                    if (product.IsShipEnabled && product.Weight > Decimal.Zero)
                        productTable.AddCell(new Paragraph(String.Format("{0}: {1} {2}", _localizationService.GetResource("PDFProductCatalog.Weight", lang.Id), product.Weight.ToString("0.00"), _measureService.GetMeasureWeightById(_measureSettings.BaseWeightId).Name), font));

                    if (product.ManageInventoryMethod == ManageInventoryMethod.ManageStock)
                        productTable.AddCell(new Paragraph(String.Format("{0}: {1}", _localizationService.GetResource("PDFProductCatalog.StockQuantity", lang.Id), product.GetTotalStockQuantity()), font));

                    productTable.AddCell(new Paragraph(" "));
                }
                var pictures = _pictureService.GetPicturesByProductId(product.Id);
                if (pictures.Count > 0)
                {
                    var table = new PdfPTable(2);
                    table.WidthPercentage = 100f;
                    if (lang.Rtl)
                    {
                        table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    }

                    foreach (var pic in pictures)
                    {
                        var picBinary = _pictureService.LoadPictureBinary(pic);
                        if (picBinary != null && picBinary.Length > 0)
                        {
                            var pictureLocalPath = _pictureService.GetThumbLocalPath(pic, 200, false);
                            var cell = new PdfPCell(Image.GetInstance(pictureLocalPath));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = Rectangle.NO_BORDER;
                            table.AddCell(cell);
                        }
                    }

                    if (pictures.Count % 2 > 0)
                    {
                        var cell = new PdfPCell(new Phrase(" "));
                        cell.Border = Rectangle.NO_BORDER;
                        table.AddCell(cell);
                    }

                    productTable.AddCell(table);
                    productTable.AddCell(new Paragraph(" "));
                }


                if (product.ProductType == ProductType.GroupedProduct)
                {
                    //grouped product. render its associated products
                    int pvNum = 1;
                    foreach (var associatedProduct in _productService.GetAssociatedProducts(product.Id, showHidden: true))
                    {
                        productTable.AddCell(new Paragraph(String.Format("{0}-{1}. {2}", productNumber, pvNum, associatedProduct.GetLocalized(x => x.Name, lang.Id)), font));
                        productTable.AddCell(new Paragraph(" "));

                        //uncomment to render associated product description
                        //string apDescription = associatedProduct.GetLocalized(x => x.ShortDescription, lang.Id);
                        //if (!String.IsNullOrEmpty(apDescription))
                        //{
                        //    productTable.AddCell(new Paragraph(HtmlHelper.StripTags(HtmlHelper.ConvertHtmlToPlainText(apDescription)), font));
                        //    productTable.AddCell(new Paragraph(" "));
                        //}

                        //uncomment to render associated product picture
                        //var apPicture = _pictureService.GetPicturesByProductId(associatedProduct.Id).FirstOrDefault();
                        //if (apPicture != null)
                        //{
                        //    var picBinary = _pictureService.LoadPictureBinary(apPicture);
                        //    if (picBinary != null && picBinary.Length > 0)
                        //    {
                        //        var pictureLocalPath = _pictureService.GetThumbLocalPath(apPicture, 200, false);
                        //        productTable.AddCell(Image.GetInstance(pictureLocalPath));
                        //    }
                        //}

                        productTable.AddCell(new Paragraph(String.Format("{0}: {1} {2}", _localizationService.GetResource("PDFProductCatalog.Price", lang.Id), associatedProduct.Price.ToString("0.00"), _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode), font));
                        productTable.AddCell(new Paragraph(String.Format("{0}: {1}", _localizationService.GetResource("PDFProductCatalog.SKU", lang.Id), associatedProduct.Sku), font));

                        if (associatedProduct.IsShipEnabled && associatedProduct.Weight > Decimal.Zero)
                            productTable.AddCell(new Paragraph(String.Format("{0}: {1} {2}", _localizationService.GetResource("PDFProductCatalog.Weight", lang.Id), associatedProduct.Weight.ToString("0.00"), _measureService.GetMeasureWeightById(_measureSettings.BaseWeightId).Name), font));

                        if (associatedProduct.ManageInventoryMethod == ManageInventoryMethod.ManageStock)
                            productTable.AddCell(new Paragraph(String.Format("{0}: {1}", _localizationService.GetResource("PDFProductCatalog.StockQuantity", lang.Id), associatedProduct.GetTotalStockQuantity()), font));

                        productTable.AddCell(new Paragraph(" "));

                        pvNum++;
                    }
                }

                doc.Add(productTable);

                productNumber++;

                if (productNumber <= prodCount)
                {
                    doc.NewPage();
                }
            }

            doc.Close();
        }

        #endregion
    }
}