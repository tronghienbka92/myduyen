using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using Nop.Services.Common;
using Nop.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.NhaXes
{
    public class NhaXeCustomerService : INhaXeCustomerService
    {
        private readonly IRepository<NhaXeCustomer> _NhaXeCustomerRepository;
        private readonly IRepository<NhanVienPhuTrachChuyen> _phutrachchuyenRepository;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        public NhaXeCustomerService(IRepository<NhaXeCustomer> nhaxecustomerRepository,
            IRepository<NhanVienPhuTrachChuyen> phutrachchuyenRepository
            , ICustomerService customerService
            , IGenericAttributeService genericAttributeService
            )
        {
            this._genericAttributeService = genericAttributeService;
            this._NhaXeCustomerRepository = nhaxecustomerRepository;
            this._phutrachchuyenRepository = phutrachchuyenRepository;
            this._customerService = customerService;
        }
        #region nhaxe customer
        Customer CapNhatKhachHangCustomer(string TenKhachHang, string SoDienThoai, Customer customer)
        {
            if (customer == null)
            {
                customer = new Customer();
                customer.Email = Guid.NewGuid().ToString() + "_" + SoDienThoai + "@cv.vn";
                customer.CreatedOnUtc = DateTime.Now;
                customer.LastActivityDateUtc = DateTime.Now;
                customer.Active = false;
                _customerService.InsertCustomer(customer);
                var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
                var newCustomerRoles = new List<CustomerRole>();
                foreach (var customerRole in allCustomerRoles)
                {
                    if (customerRole.SystemName == SystemCustomerRoleNames.Registered)
                        newCustomerRoles.Add(customerRole);
                }
                //customerrole
                foreach (var _customerRole in newCustomerRoles)
                {
                    customer.CustomerRoles.Add(_customerRole);
                }
                _customerService.UpdateCustomer(customer);
            }
            string[] arrname = TenKhachHang.Split(' ');
            int n = arrname.Count();
            var FirstName = arrname[n - 1];
            var LastName = "";
            for (int i = 0; i < n - 1; i++)
            {
                LastName = LastName + " " + arrname[i];
            }
            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, FirstName);
            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, LastName);
            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, SoDienThoai);
            return customer;
        }
        void InsertNhaXeCustomer(NhaXeCustomer _item)
        {
            if (_item == null)
                throw new ArgumentNullException("NhaXeCustomer");
            //tao thong tin customer
            var customer = CapNhatKhachHangCustomer(_item.HoTen, _item.DienThoai, null);
            _item.SearchInfo = string.Format("{0} - {1}", _item.HoTen, _item.DienThoai);
            _item.CustomerId = customer.Id;
            _NhaXeCustomerRepository.Insert(_item);
        }
        public virtual NhaXeCustomer CreateNew(int NhaXeId, string TenKhachHang, string SoDienThoai, string DiaChiLienHe)
        {
            var khachhang = new NhaXeCustomer();
            khachhang.NhaXeId = NhaXeId;
            khachhang.HoTen = TenKhachHang;
            khachhang.DienThoai = SoDienThoai;
            khachhang.DiaChiLienHe = DiaChiLienHe;
            InsertNhaXeCustomer(khachhang);
            return khachhang;
        }
        public virtual void UpdateNhaXeCustomer(NhaXeCustomer _item)
        {
            if (_item == null)
                throw new ArgumentNullException("NhaXeCustomer");
            _item.SearchInfo = string.Format("{0} - {1}", _item.HoTen, _item.DienThoai);
            var customer = _customerService.GetCustomerById(_item.CustomerId);
            customer = CapNhatKhachHangCustomer(_item.HoTen, _item.DienThoai, customer);
            _item.CustomerId = customer.Id;
            _NhaXeCustomerRepository.Update(_item);
        }
        public virtual void DeleteNhaXeCustomer(NhaXeCustomer _item)
        {
            if (_item == null)
                throw new ArgumentNullException("NhaXeCustomer");
            _NhaXeCustomerRepository.Delete(_item);
        }
        public virtual NhaXeCustomer GetNhaXeCustomerById(int nhaxecustomerId)
        {
            if (nhaxecustomerId == 0)
                return null;
            return _NhaXeCustomerRepository.GetById(nhaxecustomerId);
        }
        public virtual NhaXeCustomer GetNhaXeCustomerByCustomerId(int customerId)
        {
            if (customerId == 0)
                return null;
            var query = _NhaXeCustomerRepository.Table.Where(c => c.CustomerId == customerId);
            if (query.Count() > 0)
            {
                var item = query.First();
                return GetNhaXeCustomerById(item.Id);
            }
            //neu ko ton tai thi lay thong tin khach hang từ bảng customer
            var _cus = _customerService.GetCustomerById(customerId);
            var kh = new NhaXeCustomer();
            kh.CustomerId = customerId;
            kh.HoTen = _cus.GetFullName();
            kh.DienThoai = _cus.GetPhone();
            kh.DiaChiLienHe = _cus.GetAddress();
            return kh;
        }

        #endregion
        #region phu trach chuyen
        public virtual void InsertPhuTrachChuyen(NhanVienPhuTrachChuyen _item)
        {
            if (_item == null)
                throw new ArgumentNullException("NhaXeCustomer");

            _phutrachchuyenRepository.Insert(_item);
        }

        public virtual NhanVienPhuTrachChuyen GetPhuTrachChuyenById(int PhuTrachChuyenId)
        {
            if (PhuTrachChuyenId == 0)
                return null;
            return _phutrachchuyenRepository.GetById(PhuTrachChuyenId);
        }
        public virtual NhanVienPhuTrachChuyen GetPhuTrachChuyenByNhanVienId(int NhanVienId, int NguonVeId)
        {
            if (NhanVienId == 0)
                return null;
            var query = _phutrachchuyenRepository.Table.Where(c => c.NhanVienID == NhanVienId && c.NguonVeXeID == NguonVeId);
            if (query.Count() > 0)
            {
                var item = query.First();
                return GetPhuTrachChuyenById(item.Id);
            }
            return null;
        }
        public virtual List<NhanVienPhuTrachChuyen> GetAllPhuTrachChuyenByNguonVeId(int NguonVeId)
        {
            if (NguonVeId == 0)
                return null;
            var query = _phutrachchuyenRepository.Table.Where(c => c.NguonVeXeID == NguonVeId);
            return query.ToList();
        }
        public virtual void DeletePhuTrachChuyen(int NguonVeId)
        {
            var query = _phutrachchuyenRepository.Table.Where(c => c.NguonVeXeID == NguonVeId).ToList();
            if (query.Count > 0)
                _phutrachchuyenRepository.Delete(query);
        }

        public virtual PagedList<NhaXeCustomer> GetAllCustomer(int nhaXeId, string tenkh = "",
          int pageIndex = 0,
          int pageSize = int.MaxValue)
        {
            var query = _NhaXeCustomerRepository.Table;
            if (!String.IsNullOrWhiteSpace(tenkh))
                query = query.Where(m => m.HoTen.Contains(tenkh) || m.DienThoai.Contains(tenkh));
            query = query.Where(m => m.NhaXeId == nhaXeId);
            query = query.OrderBy(m => m.Id);
            return new PagedList<NhaXeCustomer>(query, pageIndex, pageSize);
        }
        #endregion
    }
}
