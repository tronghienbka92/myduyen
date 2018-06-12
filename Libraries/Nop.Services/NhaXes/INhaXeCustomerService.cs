using Nop.Core;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.NhaXes
{
    public partial interface INhaXeCustomerService
    {
        NhaXeCustomer CreateNew(int NhaXeId, string TenKhachHang, string SoDienThoai, string DiaChiLienHe);
        NhaXeCustomer GetNhaXeCustomerById(int nhaxecustomerId);
        void UpdateNhaXeCustomer(NhaXeCustomer _item);
        void DeleteNhaXeCustomer(NhaXeCustomer _item);
        NhaXeCustomer GetNhaXeCustomerByCustomerId(int customerId);

        void DeletePhuTrachChuyen(int NguonVeId);
        void InsertPhuTrachChuyen(NhanVienPhuTrachChuyen _item);
        NhanVienPhuTrachChuyen GetPhuTrachChuyenById(int PhuTrachChuyenId);
        NhanVienPhuTrachChuyen GetPhuTrachChuyenByNhanVienId(int NhanVienId, int NguonVeId);
        List<NhanVienPhuTrachChuyen> GetAllPhuTrachChuyenByNguonVeId(int NguonVeId);

        PagedList<NhaXeCustomer> GetAllCustomer(int nhaXeId, string tenkh = "", 
          int pageIndex = 0,
          int pageSize = int.MaxValue);

    }
}
