using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Services.Common;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.Common;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Models.NhaXes;
using System.Globalization;
using Nop.Web.Models.VeXeKhach;
using Nop.Core.Domain.Chonves;
using Nop.Services.NhaXes;
using Nop.Web.Controllers;
using Nop.Services.Catalog;
using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Web.Models.ChuyenPhatNhanh;
using Nop.Core;

namespace Nop.Web.Extensions
{
    public static class MappingExtensions
    {
        //category
        public static CategoryModel ToModel(this Category entity)
        {
            if (entity == null)
                return null;

            var model = new CategoryModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(x => x.Name),
                Description = entity.GetLocalized(x => x.Description),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle),
                SeName = entity.GetSeName(),
            };
            return model;
        }

        //manufacturer
        public static ManufacturerModel ToModel(this Manufacturer entity)
        {
            if (entity == null)
                return null;

            var model = new ManufacturerModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(x => x.Name),
                Description = entity.GetLocalized(x => x.Description),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle),
                SeName = entity.GetSeName(),
            };
            return model;
        }


        //address
        /// <summary>
        /// Prepare address model
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="address">Address</param>
        /// <param name="excludeProperties">A value indicating whether to exclude properties</param>
        /// <param name="addressSettings">Address settings</param>
        /// <param name="localizationService">Localization service (used to prepare a select list)</param>
        /// <param name="stateProvinceService">State service (used to prepare a select list). null to don't prepare the list.</param>
        /// <param name="addressAttributeService">Address attribute service. null to don't prepare the list.</param>
        /// <param name="addressAttributeParser">Address attribute parser. null to don't prepare the list.</param>
        /// <param name="addressAttributeFormatter">Address attribute formatter. null to don't prepare the formatted custom attributes.</param>
        /// <param name="loadCountries">A function to load countries  (used to prepare a select list). null to don't prepare the list.</param>
        /// <param name="prePopulateWithCustomerFields">A value indicating whether to pre-populate an address with customer fields entered during registration. It's used only when "address" parameter is set to "null"</param>
        /// <param name="customer">Customer record which will be used to pre-populate address. Used only when "prePopulateWithCustomerFields" is "true".</param>
        public static void PrepareModel(this AddressModel model,
            Address address, bool excludeProperties, 
            AddressSettings addressSettings,
            ILocalizationService localizationService = null,
            IStateProvinceService stateProvinceService = null,
            IDiaChiService diachiService =null,
            IAddressAttributeService addressAttributeService = null,
            IAddressAttributeParser addressAttributeParser = null,
            IAddressAttributeFormatter addressAttributeFormatter = null,
            Func<IList<Country>> loadCountries = null,
            bool prePopulateWithCustomerFields = false,
            Customer customer = null)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (addressSettings == null)
                throw new ArgumentNullException("addressSettings");

            if (!excludeProperties && address != null)
            {
                model.Id = address.Id;
                model.FirstName = address.FirstName;
                model.LastName = address.LastName;
                model.FullName = string.Format("{0} {1}", address.FirstName, address.LastName);
                model.Email = address.Email;
                model.Company = address.Company;
                model.CountryId = NhaXesController.CountryID;
                model.CountryName = address.Country != null 
                    ? address.Country.GetLocalized(x => x.Name) 
                    : null;
                model.StateProvinceId = address.StateProvinceId;
                model.StateProvinceName = address.StateProvince != null 
                    ? address.StateProvince.GetLocalized(x => x.Name)
                    : null;
                model.QuanHuyenId = address.QuanHuyenId;
                model.City = address.City;
                model.Address1 = address.Address1;
                model.Address2 = address.Address2;
                model.ZipPostalCode = address.ZipPostalCode;
                model.PhoneNumber = address.PhoneNumber;
                model.FaxNumber = address.FaxNumber;
            }

            if (address == null && prePopulateWithCustomerFields)
            {
                if (customer == null)
                    throw new Exception("Customer cannot be null when prepopulating an address");
                model.Email = customer.Email;
                model.FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                model.LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
                model.Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company);
                model.Address1 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress);
                model.Address2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2);
                model.ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode);
                model.City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City);
                //ignore country and state for prepopulation. it can cause some issues when posting pack with errors, etc
                //model.CountryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId);
                //model.StateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId);
                model.PhoneNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                model.FaxNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax);
            }

            //countries and states
            if (addressSettings.CountryEnabled && loadCountries != null)
            {
                if (localizationService == null)
                    throw new ArgumentNullException("localizationService");

                //model.AvailableCountries.Add(new SelectListItem { Text = localizationService.GetResource("Address.SelectCountry"), Value = "0" });
                foreach (var c in loadCountries())
                {
                    model.AvailableCountries.Add(new SelectListItem
                    {
                        Text = c.GetLocalized(x => x.Name),
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.CountryId
                    });
                }

                if (addressSettings.StateProvinceEnabled)
                {
                    //states
                    if (stateProvinceService == null)
                        throw new ArgumentNullException("stateProvinceService");
                    
                    var states = stateProvinceService
                        .GetStateProvincesByCountryId(loadCountries().First().Id)
                        .ToList();
                    if (states.Count > 0)
                    {
                        //model.AvailableStates.Add(new SelectListItem { Text = localizationService.GetResource("Address.SelectState"), Value = "0" });

                        foreach (var s in states)
                        {
                            model.AvailableStates.Add(new SelectListItem
                            {
                                Text = s.GetLocalized(x => x.Name),
                                Value = s.Id.ToString(), 
                                Selected = (s.Id == model.StateProvinceId)
                            });
                        }
                    }
                    else
                    {
                        bool anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);
                        model.AvailableStates.Add(new SelectListItem
                        {
                            Text = localizationService.GetResource(anyCountrySelected ? "Address.OtherNonUS" : "Address.SelectState"),
                            Value = "0"
                        });
                    }
                    if(model.StateProvinceId.GetValueOrDefault(0)>0)
                    {
                        var quanhuyens = diachiService.GetQuanHuyenByProvinceId(model.StateProvinceId.GetValueOrDefault(0));

                        foreach (var s in quanhuyens)
                        {
                            model.AvailableQuanHuyens.Add(new SelectListItem
                            {
                                Text = s.Ten,
                                Value = s.Id.ToString(),
                                Selected = (s.Id == model.QuanHuyenId)
                            });
                        }
                    }
                        

                }
            }

            //form fields
            model.CompanyEnabled = addressSettings.CompanyEnabled;
            model.CompanyRequired = addressSettings.CompanyRequired;
            model.StreetAddressEnabled = addressSettings.StreetAddressEnabled;
            model.StreetAddressRequired = addressSettings.StreetAddressRequired;
            model.StreetAddress2Enabled = addressSettings.StreetAddress2Enabled;
            model.StreetAddress2Required = addressSettings.StreetAddress2Required;
            model.ZipPostalCodeEnabled = addressSettings.ZipPostalCodeEnabled;
            model.ZipPostalCodeRequired = addressSettings.ZipPostalCodeRequired;
            model.CityEnabled = addressSettings.CityEnabled;
            model.CityRequired = addressSettings.CityRequired;
            model.CountryEnabled = addressSettings.CountryEnabled;
            model.StateProvinceEnabled = addressSettings.StateProvinceEnabled;
            model.PhoneEnabled = addressSettings.PhoneEnabled;
            model.PhoneRequired = addressSettings.PhoneRequired;
            model.FaxEnabled = addressSettings.FaxEnabled;
            model.FaxRequired = addressSettings.FaxRequired;

            //customer attribute services
            if (addressAttributeService != null && addressAttributeParser != null)
            {
                PrepareCustomAddressAttributes(model, address, addressAttributeService, addressAttributeParser);
            }
            if (addressAttributeFormatter != null && address != null)
            {
                model.FormattedCustomAddressAttributes = addressAttributeFormatter.FormatAttributes(address.CustomAttributes);
            }
        }
        private static void PrepareCustomAddressAttributes(this AddressModel model, 
            Address address,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeParser addressAttributeParser)
        {
            if (addressAttributeService == null)
                throw new ArgumentNullException("addressAttributeService");

            if (addressAttributeParser == null)
                throw new ArgumentNullException("addressAttributeParser");

            var attributes = addressAttributeService.GetAllAddressAttributes();
            foreach (var attribute in attributes)
            {
                var attributeModel = new AddressAttributeModel
                {
                    Id = attribute.Id,
                    Name = attribute.GetLocalized(x => x.Name),
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = addressAttributeService.GetAddressAttributeValues(attribute.Id);
                    foreach (var attributeValue in attributeValues)
                    {
                        var attributeValueModel = new AddressAttributeValueModel
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.GetLocalized(x => x.Name),
                            IsPreSelected = attributeValue.IsPreSelected
                        };
                        attributeModel.Values.Add(attributeValueModel);
                    }
                }

                //set already selected attributes
                var selectedAddressAttributes = address != null ? address.CustomAttributes : null;
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.Checkboxes:
                        {
                            if (!String.IsNullOrEmpty(selectedAddressAttributes))
                            {
                                //clear default selection
                                foreach (var item in attributeModel.Values)
                                    item.IsPreSelected = false;

                                //select new values
                                var selectedValues = addressAttributeParser.ParseAddressAttributeValues(selectedAddressAttributes);
                                foreach (var attributeValue in selectedValues)
                                    foreach (var item in attributeModel.Values)
                                        if (attributeValue.Id == item.Id)
                                            item.IsPreSelected = true;
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //do nothing
                            //values are already pre-set
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            if (!String.IsNullOrEmpty(selectedAddressAttributes))
                            {
                                var enteredText = addressAttributeParser.ParseValues(selectedAddressAttributes, attribute.Id);
                                if (enteredText.Count > 0)
                                    attributeModel.DefaultValue = enteredText[0];
                            }
                        }
                        break;
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    default:
                        //not supported attribute control types
                        break;
                }

                model.CustomAddressAttributes.Add(attributeModel);
            }
        }
        public static Address ToEntity(this AddressModel model, bool trimFields = true)
        {
            if (model == null)
                return null;

            var entity = new Address();
            return ToEntity(model, entity, trimFields);
        }
        public static Address ToEntity(this AddressModel model, Address _destination, bool trimFields = true)
        {
            if (model == null)
                return _destination;

            if (trimFields)
            {
                if (model.FirstName != null)
                    model.FirstName = model.FirstName.Trim();
                if (model.LastName != null)
                    model.LastName = model.LastName.Trim();
                if (model.Email != null)
                    model.Email = model.Email.Trim();
                if (model.Company != null)
                    model.Company = model.Company.Trim();
                if (model.City != null)
                    model.City = model.City.Trim();
                if (model.Address1 != null)
                    model.Address1 = model.Address1.Trim();
                if (model.Address2 != null)
                    model.Address2 = model.Address2.Trim();
                if (model.ZipPostalCode != null)
                    model.ZipPostalCode = model.ZipPostalCode.Trim();
                if (model.PhoneNumber != null)
                    model.PhoneNumber = model.PhoneNumber.Trim();
                if (model.FaxNumber != null)
                    model.FaxNumber = model.FaxNumber.Trim();
            }

            Address destination = _destination;
            if (destination == null)
                destination = new Address();
            destination.Id = model.Id;
            
            destination.FirstName = model.FirstName;
            destination.LastName = model.LastName;
            if(!String.IsNullOrWhiteSpace(model.FullName))
            {
                int _pos = model.FullName.LastIndexOf(' ');
                if(_pos>0)
                {
                    destination.FirstName = model.FullName.Substring(0, _pos).Trim();
                    destination.LastName = model.FullName.Substring(_pos).Trim();
                }
                else
                {
                    destination.FirstName = model.FullName;
                    destination.LastName = model.FullName;
                }
            }
            destination.Email = model.Email;
            destination.Company = model.Company;
            destination.CountryId = NhaXesController.CountryID;
            destination.StateProvinceId = model.StateProvinceId;
            destination.QuanHuyenId = model.QuanHuyenId;
            destination.City = model.City;
            destination.Address1 = model.Address1;
            destination.Address2 = model.Address2;
            destination.ZipPostalCode = model.ZipPostalCode;
            destination.PhoneNumber = model.PhoneNumber;
            destination.FaxNumber = model.FaxNumber;

            return destination;
        }
        public static string ToCVEnumText<T>(this T enumValue, ILocalizationService localizationService) where T : struct
        {
            if (localizationService == null)
                throw new ArgumentNullException("localizationService");
            //localized value
            string resourceName = string.Format("Enums.{0}.{1}",
                typeof(T).ToString(),
                enumValue.ToString());
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            return localizationService.GetResource(resourceName);


        }
        /// <summary>
        /// Chuyen doi gia tri string thanh decimal
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(this String val)
        {
            return Convert.ToDecimal(val, new CultureInfo("en-US"));
        }
        
        public static DiaChi ToEntity(this DiaChiInfoModel model, DiaChi destination)
        {
            
            if (model == null)
                return null;
            if (destination == null)
                destination = new DiaChi();
            destination.Id = model.Id;
            destination.DiaChi1 = model.DiaChi1;
            destination.DiaChi2 = model.DiaChi2;
            destination.DienThoai = model.DienThoai;
            destination.Fax = model.Fax;
            destination.MaBuuDien = model.MaBuuDien;
            destination.ProvinceID = model.ProvinceID;
            destination.QuanHuyenID = model.QuanHuyenID;
            destination.Latitude = model.Latitude.ToDecimal();
            destination.Longitude = model.Longitude.ToDecimal();
            return destination;
        }
        public static DiaChiInfoModel ToModel(this DiaChi model)
        {
            if (model == null)
                return new DiaChiInfoModel();
            DiaChiInfoModel destination = new DiaChiInfoModel();
            destination.Id = model.Id;
            destination.DiaChi1 = model.DiaChi1;
            destination.DiaChi2 = model.DiaChi2;
            destination.DienThoai = model.DienThoai;
            destination.Fax = model.Fax;
            destination.MaBuuDien = model.MaBuuDien;
            destination.ProvinceID = model.ProvinceID;
            destination.QuanHuyenID = model.QuanHuyenID;
            destination.Latitude = model.Latitude.ToString().Replace(",", ".");
            destination.Longitude = model.Longitude.ToString().Replace(",", ".");
            return destination;
        }
        
        
        public static DiemDonModel ToModel(this DiemDon nvfrom, ILocalizationService _localizationService)
        {
            var nvto = new DiemDonModel();
            if (nvfrom == null)
                return nvto;
            nvto.Id = nvfrom.Id;
            nvto.TenDiemDon = nvfrom.TenDiemDon;
            nvto.LoaiDiemDonId = nvfrom.LoaiDiemDonId;
            nvto.LoaiDiemDonText = nvfrom.LoaiDiemDon.ToCVEnumText(_localizationService);
            nvto.VanPhongId = nvfrom.VanPhongId.GetValueOrDefault(0);
            if (nvfrom.vanphong!=null)
            {
                nvto.VanPhongText = nvfrom.vanphong.TenVanPhong;
            }

            nvto.DiaChiId = nvfrom.DiaChiId;
            nvto.BenXeId = nvfrom.BenXeId.GetValueOrDefault(0);
            if (nvfrom.benxe !=null)
            {
                nvto.BenXeText = nvfrom.benxe.TenBenXe;
            }

            return nvto;
        }
        public static DiaDiemModel ToModel(this DiaDiem nvfrom, ILocalizationService _localizationService)
        {
            var nvto = new DiaDiemModel();
            if (nvfrom == null)
                return nvto;
            nvto.Id = nvfrom.Id;
            nvto.Ten = nvfrom.Ten;
            nvto.NguonId = nvfrom.NguonId;
            nvto.LoaiId = nvfrom.LoaiId;
            nvto.LoaiText = nvfrom.Loai.ToCVEnumText(_localizationService);
            return nvto;
        }
        public static NguonVeXeModel ToModel(this NguonVeXe e, IPriceFormatter priceFormatter)
        {
           var m = new NguonVeXeModel();
            m.Id = e.Id;
            m.ParentId = e.ParentId;
            //nhaxe info
            m.NhaXeInfo = new NguonVeXeModel.NhaXeBasicModel();
            m.NhaXeInfo.Id = e.NhaXeId;
            m.NhaXeInfo.TenNhaXe = e.TenNhaXe;

            m.DiemDonId = e.DiemDonId;
            m.DiemDenId = e.DiemDenId;
            m.LichTrinhId = e.LichTrinhId;
            m.TimeCloseOnline = e.TimeCloseOnline;
            m.TimeOpenOnline = e.TimeOpenOnline;
            m.ThoiGianDi = e.ThoiGianDi;
            m.ThoiGianDen = e.ThoiGianDen;
            m.GiaVeMoi = e.ProductInfo.Price;
            m.GiaVeMoiText = m.GiaVeMoi.ToTien(priceFormatter);
            m.GiaVeCu = e.ProductInfo.OldPrice;
            m.GiaVeCuText = m.GiaVeCu.ToTien(priceFormatter);

            m.LoaiXeId = e.LoaiXeId;
            m.TenDiemDon = e.TenDiemDon;
            m.TenDiemDen = e.TenDiemDen;
            m.TenLoaiXe = e.TenLoaiXe;
            m.HienThi = e.HienThi;
            m.ToWeb = e.ToWeb;
            return m;
        }
        public static PhieuGuiHangModel.HangHoaModel ToModel(this HangHoa nvfrom, ILocalizationService localizationService, IPriceFormatter priceFormatter)
        {
            var nvto = new PhieuGuiHangModel.HangHoaModel();
            nvto.Id = nvfrom.Id;
            nvto.TenHangHoa = nvfrom.TenHangHoa;
            nvto.LoaiHangHoaId = nvfrom.LoaiHangHoaId;
            nvto.CanNang = nvfrom.CanNang;
            nvto.LoaiHangHoa = nvfrom.LoaiHangHoa.ToCVEnumText(localizationService);
            nvto.GiaTri = nvfrom.GiaTri;
            nvto.GiaCuoc = nvfrom.GiaCuoc;
            nvto.GhiChu = nvfrom.GhiChu;
            nvto.SoLuong = nvfrom.SoLuong;
            nvto.CanNangText = string.Format("{0} {1}", nvfrom.CanNang, "kg");
            nvto.GiaTriText = nvfrom.GiaTri.ToTien(priceFormatter);
            nvto.GiaCuocText = nvfrom.GiaCuoc.ToTien(priceFormatter);
            return nvto;
        }
        public static PhieuGuiHangModel ToModel(this PhieuGuiHang nvfrom,ILocalizationService localizationService, IPriceFormatter priceFormatter,List<HangHoa> hanghoas=null,bool isChiTiet=false)
        {
            var nvto=new PhieuGuiHangModel();
            nvto.Id = nvfrom.Id;
            nvto.MaPhieu = nvfrom.MaPhieu;
            //van phong gui
            nvto.VanPhongGui.Id = nvfrom.VanPhongGuiId;
            nvto.VanPhongGui.TenVanPhong = nvfrom.VanPhongGui.TenVanPhong;
            //van phong nhan
            nvto.VanPhongNhan.Id = nvfrom.VanPhongNhanId;
            nvto.VanPhongNhan.TenVanPhong = nvfrom.VanPhongNhan.TenVanPhong;
            //nguoi gui
            nvto.NguoiGui.Id = nvfrom.NguoiGuiId;
            nvto.NguoiGui.HoTen = nvfrom.NguoiGui.HoTen;
            nvto.NguoiGui.SoDienThoai = nvfrom.NguoiGui.DienThoai;
            nvto.NguoiGui.DiaChi = nvfrom.NguoiGui.DiaChiLienHe;
            //nguoi nhan
            nvto.NguoiNhan.Id = nvfrom.NguoiNhanId;
            nvto.NguoiNhan.HoTen = nvfrom.NguoiNhan.HoTen;
            nvto.NguoiNhan.SoDienThoai = nvfrom.NguoiNhan.DienThoai;
            nvto.NguoiNhan.DiaChi = nvfrom.NguoiNhan.DiaChiLienHe;

            nvto.NguoiKiemTraHangId = nvfrom.NguoiKiemTraHangId;
            if (nvfrom.NguoiKiemTraHang != null)
                nvto.TenNguoiKiemTraHang = nvfrom.NguoiKiemTraHang.HoVaTen;
            else
                nvto.TenNguoiKiemTraHang = nvfrom.nguoitao.HoVaTen;

            nvto.TinhTrangVanChuyen = nvfrom.TinhTrangVanChuyen.ToCVEnumText(localizationService);
            nvto.DaThuCuoc = nvfrom.DaThuCuoc;
            if (nvto.DaThuCuoc)
                nvto.ThanhToan = "Đã thanh toán";
            else
                nvto.ThanhToan = "Chưa thanh toán";
            nvto.NgayNhan = nvfrom.NgayUpdate;
            if(nvfrom.NhanVienThuTien!=null)
            {
                nvto.NgayThanhToan = nvfrom.NgayThanhToan;
                nvto.NhanVienThuTienId = nvfrom.NhanVienThuTienId;
            }
            nvto.NgayTao = nvfrom.NgayTao;
            if (nvfrom.XeXuatBen != null)
            {
                nvto.NgayDi = nvfrom.XeXuatBen.NgayDi;
                nvto.TenXeXuatBen = string.Format("{0}-{1}-{2}-{3} ({4})", nvfrom.XeXuatBen.NguonVeInfo.ThoiGianDi.ToString("HH:mm"),
                 nvfrom.XeXuatBen.NguonVeInfo.ThoiGianDen.ToString("HH:mm"), nvfrom.XeXuatBen.NgayDi.ToString("dd/MM/yyyy"),
                 nvfrom.XeXuatBen.xevanchuyen.TenXe, nvfrom.XeXuatBen.xevanchuyen.BienSo);                  
                if (isChiTiet)
                    nvto.XeXuatBen = nvfrom.XeXuatBen;
            }
            else
                nvto.NgayDi = nvfrom.NgayTao;
            nvto.XeXuatBenId = nvfrom.XeXuatBenId;
            nvto.GhiChu = nvfrom.GhiChu;
            nvto.TenNguoiKiemTraHang = nvfrom.NguoiKiemTraHang.ThongTin();
            if (hanghoas!=null)
            {
                nvto.HangHoaInfo = "";                 
                nvto.SoLuongHang = hanghoas.Count();
                if (nvto.SoLuongHang == 1)
                {
                    nvto.HangHoa = hanghoas.First().ToModel(localizationService,priceFormatter);
                }
                //var items = _hanghoaService.GetAllHangHoaByPhieuGuiHangId(nvfrom.Id);
                decimal tonggiatrihanghoa = 0;
                decimal tongcuocphi = 0;
                decimal tongcannang = 0;
                decimal tongsoluong = 0;
                foreach (var _item in hanghoas)
                {
                    if (string.IsNullOrEmpty(nvto.HangHoaInfo))
                        nvto.HangHoaInfo = _item.TenHangHoa;
                    else
                        nvto.HangHoaInfo = nvto.HangHoaInfo + "," + _item.TenHangHoa;

                    var _model = _item.ToModel(localizationService, priceFormatter);
                    tonggiatrihanghoa = tonggiatrihanghoa + _item.GiaTri * _item.SoLuong;
                    tongcuocphi = tongcuocphi + _item.GiaCuoc;
                    tongcannang = tongcannang + _item.CanNang;
                    tongsoluong = tongsoluong + _item.SoLuong;
                    if (isChiTiet)
                        nvto.ListHangHoaInPhieuGui.Add(_model);
                }
                nvto.TotalPackage = string.Format("{0} {1}", tongsoluong, "Kiện");
                nvto.TongGiaTriHangHoa = tonggiatrihanghoa.ToTien(priceFormatter);
                nvto.TongCuocPhi = tongcuocphi.ToTien(priceFormatter);
                nvto.TongCanNang = tongcannang.ToSoNguyen();
            }

            return nvto;
        }
        public static VeXeItemModel toModel(this VeXeItem entity, ILocalizationService localizationService)
        {
            var model = new VeXeItemModel();
            model.Id = entity.Id;
            model.MauVe = entity.MauVe;
            model.KyHieu = entity.KyHieu;
            model.SoSeri = entity.SoSeri;
            model.isVeDi = entity.isVeDi;
            model.isVeDiText = "Vé đi";
            if (!model.isVeDi)
                model.isVeDiText = "Vé về";
            model.NhanVienId = entity.NhanVienId;
            if (entity.nhanvien != null)
                model.TenNhanVien = entity.nhanvien.HoVaTen;
            model.NgayTao = entity.NgayTao;
            model.NgayNhap = entity.NgayNhap;
            model.NgayGiaoVe = entity.NgayGiaoVe;
            model.NguonVeXeId = entity.NguonVeXeId;
            model.ChangId = entity.ChangId;
            if (entity.changgiave != null)
            {
                model.TenChang = string.Format("{0} -> {1}", entity.changgiave.DiemDon.TenDiemDon, entity.changgiave.DiemDen.TenDiemDon);
            }
            model.NgayBan = entity.NgayBan;
            model.TrangThaiId = entity.TrangThaiId;
            model.TrangThaiText = entity.TrangThai.ToCVEnumText(localizationService);
            model.MenhGiaId = entity.MenhGiaId;
            model.MenhGia = entity.menhgia.MenhGia;
            model.ThongTinXuatBen = entity.xexuatben.toMoTa();
            return model;
        }
        static void XeXuatBentoModel(XeXuatBenItemModel model, HistoryXeXuatBen entity, ILocalizationService localizationService)
        {
            model.Id = entity.Id;
            model.NguonVeId = entity.NguonVeId;
            model.XeVanChuyenId = entity.XeVanChuyenId.GetValueOrDefault(0);
            if (entity.xevanchuyen != null)
                model.BienSo = entity.xevanchuyen.BienSo;
            else
                model.BienSo = "----";
            model.TrangThai = entity.TrangThai;
            model.TrangThaiText = entity.TrangThai.ToCVEnumText(localizationService);
            model.NgayDi = entity.NgayDi;
            model.SoNguoi = entity.SoNguoi;
            model.SoGhe = entity.NguonVeInfo.LichTrinhInfo.loaixeinfo.sodoghe.SoLuongGhe;
            model.NgayTao = entity.NgayTao;
            model.NguoiTaoId = entity.NguoiTaoId;
            if (entity.NguoiTao != null)
                model.TenNguoiTao = entity.NguoiTao.HoVaTen;
            model.GhiChu = entity.GhiChu;
            model.HanhTrinhId = entity.HanhTrinhId;
            if (entity.NguonVeInfo != null)
            {
                model.TuyenXeChay = entity.NguonVeInfo.GetHanhTrinh();
                model.GioDi = entity.NgayDi.ToString("HH:mm");
                model.GioDen = entity.NgayDi.AddHours(4).ToString("HH:mm");
            }
            model.laivaphuxes = entity.LaiPhuXes.Select(c =>
            {
                return new XeXuatBenItemModel.NhanVienLaiPhuXe(c.NhanVien_Id, c.nhanvien.HoVaTen);
            }).ToList();
            if (model.laivaphuxes.Count > 0)
            {
                model.LaiXeId = model.laivaphuxes[0].Id;
                model.TenLaiXe = model.laivaphuxes[0].TenLaiXe;
                if (model.laivaphuxes.Count > 1)
                {
                    model.NTVId = model.laivaphuxes[1].Id;
                    model.TenNTV = model.laivaphuxes[1].TenLaiXe;
                }
            }
            if (model.BienSo.Length >= 4)
                model.BienSoXe3So = model.BienSo.Substring(model.BienSo.Length - 4);
            else
                model.BienSoXe3So = "----";
            model.ThongTinLaiPhuXe = entity.ThongTinLaiPhuXes();
            if (string.IsNullOrEmpty(model.ThongTinLaiPhuXe))
                model.ThongTinLaiPhuXe = "---------";
            if (model.laivaphuxes.Count > 1)
                model.PhuXeId = model.laivaphuxes[1].Id;
            model.nhatkys = entity.NhatKys.Select(c =>
            {
                var item = new XeXuatBenItemModel.NhatKyXeXuatBen();
                item.Id = c.Id;
                item.NgayTao = c.NgayTao;
                item.NguoiTaoId = c.NguoiTaoId;
                if (c.NguoiTao != null)
                    item.TenNguoiTao = c.NguoiTao.HoVaTen;
                item.GhiChu = c.GhiChu;
                return item;
            }).ToList();
            //khong dc edit
            if (model.TrangThai == ENTrangThaiXeXuatBen.KET_THUC || model.TrangThai == ENTrangThaiXeXuatBen.HUY)
                model.isEdit = false;
        }
        public static XeXuatBenItemModel toModel(this HistoryXeXuatBen entity, ILocalizationService localizationService)
        {
            var model = new XeXuatBenItemModel();
            if (entity == null)
                return model;
            XeXuatBentoModel(model, entity, localizationService);
            return model;
        }
        public static VeXeChuyenDiVeModel toModelVeChuyen(this HistoryXeXuatBen entity, ILocalizationService localizationService)
        {
            var model = new VeXeChuyenDiVeModel();
            if (entity == null)
                return model;
            XeXuatBentoModel(model, entity, localizationService);
            return model;
        }
        public static PhoiVeModel toModel(this PhoiVe e)
        {
            PhoiVeModel m = new PhoiVeModel();
            m.Id = e.Id;
            m.NguonVeXeId = e.NguonVeXeId;
            //neu co nguon ve xe con, tuc la khach hang dang chon tuyen con de dat ve
            //chuyen doi thong tin gia ve gia ve cua tuyen con
            m.GiaVe = e.GiaVeHienTai;
            m.GiaVeText = e.GiaVeHienTai.ToSoNguyen();
            m.NgayDi = e.NgayDi;
            var nguonve = e.getNguonVeXe();
            m.TenHanhTrinh = string.Format(" {0}- {1}", nguonve.TenDiemDon, nguonve.TenDiemDen);
            m.TenLichTrinh = string.Format("{0} - {1}", nguonve.ThoiGianDi.ToString("HH:mm"), nguonve.ThoiGianDen.ToString("HH:mm"));
            m.TrangThaiId = e.TrangThaiId;
            m.CustomerId = e.CustomerId;
            m.SoDoGheXeQuyTacId = e.SoDoGheXeQuyTacId;
            m.KyHieuGhe = e.sodoghexequytac.Val;
            m.Tang = e.sodoghexequytac.Tang;
            m.isChonVe = e.isChonVe;
            m.NgayTao = e.NgayTao;
            m.NgayUpd = e.NgayUpd;
            m.SessionId = e.SessionId;
            m.ViTriLenXe = e.ViTriLenXe;
            m.ViTriXuongXe = e.ViTriXuongXe;
            m.GhiChu = e.GhiChu;
            //get thong tin khach hang
           
           

            return m;
        }
        #region chuyen phat nhanh
        public static PhieuChuyenPhatModel ToModel(this PhieuChuyenPhat nvfrom, ILocalizationService localizationService, IPriceFormatter priceFormatter)
        {
            var nvto = new PhieuChuyenPhatModel();
            nvto.Id = nvfrom.Id;
            nvto.NhaXeId = nvfrom.NhaXeId;
            nvto.MaPhieu = nvfrom.MaPhieu;
            //van phong gui
            nvto.VanPhongGuiId = nvfrom.VanPhongGuiId;
            nvto.VanPhongGuiText = nvfrom.VanPhongGui != null ? nvfrom.VanPhongGui.TenVanPhong : "";
            //van phong nhan
            nvto.VanPhongNhanId = nvfrom.VanPhongNhanId;
            nvto.VanPhongNhanText = nvfrom.VanPhongNhan != null ? nvfrom.VanPhongNhan.TenVanPhong : "";
            nvto.KhuVucText = nvfrom.VanPhongNhan != null && nvfrom.VanPhongNhan.khuvuc != null ? nvfrom.VanPhongNhan.khuvuc.TenKhuVuc : "";
            //nguoi gui
            nvto.NguoiGuiId = nvfrom.NguoiGuiId;
            nvto.NguoiGuiText = nvfrom.NguoiGui.toText();
            nvto.NguoiGui = nvfrom.NguoiGui;
            //nguoi nhan
            nvto.NguoiNhanId = nvfrom.NguoiNhanId;
            nvto.NguoiNhanText = nvfrom.NguoiNhan.toText();
            nvto.NguoiNhan = nvfrom.NguoiNhan;

            nvto.NhanVienGiaoDichId = nvfrom.NhanVienGiaoDichId;
            nvto.TenNhanvienGiaoDich = nvfrom.NVGiaoDich != null ? nvfrom.NVGiaoDich.HoVaTen : "";
            nvto.TrangThai = nvfrom.TrangThai;
            nvto.TrangThaiText = nvfrom.TrangThai.ToCVEnumText(localizationService);
            nvto.TenHang = nvfrom.TenHang;
            nvto.CuocCapToc = nvfrom.CuocCapToc;
            nvto.CuocGiaTri = nvfrom.CuocGiaTri;
            nvto.CuocVuotTuyen = nvfrom.CuocVuotTuyen;
            nvto.CuocPhi = nvfrom.CuocPhi;
            nvto.CuocTanNoi = nvfrom.CuocTanNoi;
            nvto.CuocNhanTanNoi = nvfrom.CuocNhanTanNoi;
            nvto.TongCuocDaThanhToan = nvfrom.TongCuocDaThanhToan;
            nvto.PhieuVanChuyenId = nvfrom.PhieuVanChuyenId.GetValueOrDefault(0);
            nvto.SoLenh = nvfrom.phieuvanchuyen != null ? nvfrom.phieuvanchuyen.SoLenh : "";

            nvto.NgayKetThuc = nvfrom.NgayKetThuc;
            nvto.NgayDenVanPhongNhan = nvfrom.NgayDenVanPhongNhan;
            nvto.NgayTao = nvfrom.NgayTao;
            nvto.NgayUpdate = nvfrom.NgayUpdate;
            nvto.NgayNhanHang = nvfrom.NgayNhanHang;
            nvto.GhiChu = nvfrom.GhiChu;
            nvto.ToVanChuyenNhanId = nvfrom.ToVanChuyenNhanId.GetValueOrDefault(0);
            nvto.TenToVanChuyenNhan = nvfrom.tovanchuyennhan != null ? nvfrom.tovanchuyennhan.TenTo : "";
            nvto.NguoiVanChuyenNhanId = nvfrom.NguoiVanChuyenNhanId.GetValueOrDefault(0);
            nvto.TenNguoiVanChuyenNhan = nvfrom.nguoivanchuyennhan != null ? nvfrom.nguoivanchuyennhan.HoVaTen : "";

            nvto.ToVanChuyenTraId = nvfrom.ToVanChuyenTraId.GetValueOrDefault(0);
            nvto.TenToVanChuyenTra = nvfrom.tovanchuyentra != null ? nvfrom.tovanchuyentra.TenTo : "";
            nvto.NguoiVanChuyenTraId = nvfrom.NguoiVanChuyenTraId.GetValueOrDefault(0);
            nvto.TenNguoiVanChuyenTra = nvfrom.nguoivanchuyentra != null ? nvfrom.nguoivanchuyentra.HoVaTen : "";

            nvto.DaSMS = nvfrom.DaSMS;
            nvto.TongTienCuoc = nvfrom.TongTienCuoc;
            nvto.LoaiPhieuId = nvfrom.LoaiPhieuId;
            //count ngay ton : ngay hom nay-ngay hang den van phong nhan, ngay that lac =ngay hom nay -ngay nhan hang
            if (nvfrom.NgayDenVanPhongNhan != null)
            {
                nvto.SoNgayTon = Convert.ToInt32((DateTime.Now - nvfrom.NgayDenVanPhongNhan.Value).TotalDays);
            }
            else
                nvto.SoNgayTon = Convert.ToInt32((DateTime.Now - nvfrom.NgayNhanHang).TotalDays);

            if (nvfrom.phieuvanchuyen != null)
            {
                if (nvfrom.phieuvanchuyen.LoaiPhieuVanChuyen == ENLoaiPhieuVanChuyen.VuotTuyen)
                {
                    nvto.isVuotTuyen = true;
                }
            }
            //nhat ky van chuyen
            foreach (var nk in nvfrom.nhatkyvanchuyens)
            {
                var nvkcmodel = new PhieuChuyenPhatModel.PhieuChuyenPhatVanChuyenModel();
                nvkcmodel.ChuyenDiId = nk.ChuyenDiId;
                if (nk.chuyendi != null)
                {
                    nvkcmodel.BienSo = nk.chuyendi.xevanchuyen != null ? nk.chuyendi.xevanchuyen.BienSo : "";
                    nvkcmodel.LaiXe = nk.chuyendi.ThongTinLaiPhuXe(0, true);
                    nvkcmodel.PhuXe = nk.chuyendi.ThongTinLaiPhuXe(1, true);
                    nvkcmodel.NgayDi = nk.chuyendi.NgayDi;
                    nvkcmodel.ThongTinChuyen = string.Format("{0};{1};{2};{3}", nvkcmodel.BienSo, nvkcmodel.LaiXe, nvkcmodel.PhuXe, nvkcmodel.NgayDi.ToString("HH:mm"));
                    if (nvto.ThongTinXe == null)
                        nvto.ThongTinXe = nvkcmodel.ThongTinChuyen;
                    else
                        nvto.ThongTinXe = string.Format("[{0}]\n[{1}]", nvto.ThongTinXe, nvkcmodel.ThongTinChuyen);
                }
                nvkcmodel.CuocVuotTuyen = nk.CuocVuotTuyen;
                nvkcmodel.HanhTrinhId = nk.HanhTrinhId;
                nvkcmodel.hanhtrinhText = nk.hanhtrinh != null ? nk.hanhtrinh.MoTa : "";
                nvkcmodel.Id = nk.Id;
                nvkcmodel.KhuVucId = nk.KhuVucId;
                nvkcmodel.khuvucText = nk.khuvuc != null ? nk.khuvuc.TenKhuVuc : "";
                nvkcmodel.PhieuChuyenPhatId = nk.PhieuChuyenPhatId;
                nvkcmodel.PhieuVanChuyenId = nk.PhieuVanChuyenId;
                nvkcmodel.PhieuVanChuyenText = nk.phieuvanchuyen.SoLenh;
                nvkcmodel.TongCuoc = nk.TongCuoc;
                nvkcmodel.TuyenId = nk.TuyenId.GetValueOrDefault(0);
                nvkcmodel.tuyenText = nk.tuyen != null ? nk.tuyen.Ten : "";
                nvkcmodel.VanPhongGuiId = nk.VanPhongGuiId;
                nvkcmodel.vanphongguiText = nk.vanphonggui != null ? nk.vanphonggui.TenVanPhong : "";
                nvkcmodel.VanPhongNhanId = nk.VanPhongNhanId;
                nvkcmodel.vanphongnhanText = nk.vanphongnhan != null ? nk.vanphongnhan.TenVanPhong : "";
                nvto.nhatkyvanchuyens.Add(nvkcmodel);
            }
            //thong tin chuyen di

            return nvto;
        }
        public static PhieuVanChuyenModel ToModel(this PhieuVanChuyen nvfrom, ILocalizationService localizationService, IPriceFormatter priceFormatter, bool isNhatKy)
        {
            var nvto = new PhieuVanChuyenModel();
            nvto.Id = nvfrom.Id;
            nvto.NhaXeId = nvfrom.NhaXeId;
            nvto.SoLenh = nvfrom.SoLenh;
            nvto.VanPhongId = nvfrom.VanPhongId;
            nvto.KhuVucDenId = nvfrom.KhuVucDenId;
            nvto.TrangThai = nvfrom.TrangThai;
            nvto.TrangThaiText = nvto.TrangThai.ToCVEnumText(localizationService);
            nvto.NgayTao = nvfrom.NgayTao;
            nvto.TenVanPhong = nvfrom.vanphong != null ? nvfrom.vanphong.TenVanPhong : "";
            nvto.MaVanPhong = nvfrom.vanphong != null ? nvfrom.vanphong.Ma : "";
            nvto.LoaiPhieuVanChuyen = nvfrom.LoaiPhieuVanChuyen;
            nvto.LoaiPhieuVanChuyenText = nvfrom.LoaiPhieuVanChuyen.ToCVEnumText(localizationService);
            // vidu: HP->HD
            nvto.TenChang = gettenchang(nvfrom);
            //tong tien di kem=tong tien da thanh toan
            if (nvfrom.phieuchuyenphats != null)
                nvto.TongCuocDiKem = nvfrom.phieuchuyenphats.Sum(c => c.TongCuocDaThanhToan);

            //nhat ky van chuyen
            foreach (var nk in nvfrom.nhatkyvanchuyens)
            {
                var nvkcmodel = new PhieuVanChuyenModel.PhieuVanChuyenLogModel();
                nvkcmodel.ChuyenDiId = nk.ChuyenDiId;
                if (nk.chuyendi != null)
                {
                    nvkcmodel.BienSo = nk.chuyendi.xevanchuyen != null ? nk.chuyendi.xevanchuyen.BienSo : "";
                    nvkcmodel.LaiXe = nk.chuyendi.ThongTinLaiPhuXe(0, true);
                    nvkcmodel.NgayDi = nk.chuyendi.NgayDi;
                    nvkcmodel.NgayDiText = nk.chuyendi.NgayDi.ToString("HH:mm");
                }
                nvkcmodel.HanhTrinhId = nk.HanhTrinhId;
                nvkcmodel.hanhtrinhText = nk.hanhtrinh != null ? nk.hanhtrinh.MoTa : "";
                nvkcmodel.Id = nk.Id;
                nvkcmodel.KhuVucId = nk.KhuVucId;
                nvkcmodel.khuvucText = nk.khuvuc != null ? nk.khuvuc.TenKhuVuc : "";
                nvkcmodel.PhieuVanChuyenId = nk.PhieuVanChuyenId;
                nvkcmodel.TongCuoc = nk.TongCuoc;
                nvkcmodel.TuyenId = nk.TuyenId.GetValueOrDefault(0);
                nvkcmodel.tuyenText = nk.tuyen != null ? nk.tuyen.Ten : "";
                nvkcmodel.VanPhongGuiId = nk.VanPhongGuiId;
                nvkcmodel.vanphongguiText = nk.vanphonggui != null ? nk.vanphonggui.TenVanPhong : "";
                nvkcmodel.VanPhongNhanId = nk.VanPhongNhanId;
                nvkcmodel.vanphongnhanText = nk.vanphongnhan != null ? nk.vanphongnhan.TenVanPhong : "";
                nvkcmodel.NguoiGiaoId = nk.NguoiGiaoId;
                nvkcmodel.NguoiGiaoText = nk.NguoiGiao != null ? nk.NguoiGiao.HoVaTen : "";
                nvkcmodel.NguoiNhanId = nk.NguoiNhanId;
                nvkcmodel.NguoiNhanText = nk.NguoiNhan != null ? nk.NguoiNhan.HoVaTen : "";
                nvkcmodel.NgayTao = nk.NgayTao;
                nvkcmodel.GhiChu = nk.GhiChu;
                nvto.nhatkyvanchuyens.Add(nvkcmodel);
            }
            if (isNhatKy && nvto.nhatkyvanchuyens.Count > 0)
            {
                nvto.NhatKyVanChuyenHienTai = nvto.nhatkyvanchuyens.Last();
            }
            return nvto;
        }
        public static string gettenchang(PhieuVanChuyen item)
        {
            string TenKhuVucGui = "";
            string TenKhuVucNhan = "";
            if (item.VanPhongId > 0)
            {
                TenKhuVucGui = item.vanphong.khuvuc != null ? item.vanphong.khuvuc.TenVietTat : "";
            }
            TenKhuVucNhan = item.KhuVucDen != null ? item.KhuVucDen.TenVietTat : "";
            return string.Format("{0} -> {1}", TenKhuVucGui, TenKhuVucNhan);
        }
        #endregion

    }
}