using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.NhaXes;

namespace Nop.Services.NhaXes
{
    public partial interface INhanVienService
    {
        PagedList<NhanVien> GetAll(int NhaXeId = 0, string tennhanvien = "",
           int pageIndex = 0,
           int pageSize = int.MaxValue);
        List<NhanVien> GetAllLaiXePhuXeByNhaXeId(int NhaXeId,string ThongTin ="");
        NhanVien GetById(int itemId);
        NhanVien GetByCustomerId(int CustomerId);
        void Insert(NhanVien _item);
        void Update(NhanVien _item);
        void Delete(NhanVien _item);
        List<NhanVien> GetAllByVanPhongId(int VanPhongId,string TenNhanVien="");
        List<NhanVien> GetAllForGiaoDichKeVe(int VanPhongId, string TenNhanVien = "", int NhaXeId = 0);        
    }
}
