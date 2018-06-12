using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Chonves;

namespace Nop.Services.Chonves
{
    public partial interface IChonVeService
    {
        #region "Hop Dong"
        /// <summary>
        /// Lay thong tin hop dong
        /// </summary>
        /// <param name="tenhopdong"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="showHidden"></param>
        /// <param name="OwnerID"></param>
        /// <returns></returns>
        PagedList<HopDong> GetAllHopDong(string mahopdong = "", 
            string tenhopdong = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false,
            int OwnerID = 0,
            ENTrangThaiHopDong trangthai = ENTrangThaiHopDong.TatCa);
        /// <summary>
        /// Gets a HopDong
        /// </summary>
        /// <param name="HopDongId">HopDong identifier</param>
        /// <returns>HopDong</returns>
        HopDong GetHopDongById(int HopDongId);

        /// <summary>
        /// Inserts HopDong
        /// </summary>
        /// <param name="HopDong">HopDong</param>

        
        
        void InsertHopDong(HopDong _item);

        /// <summary>
        /// Updates the HopDong
        /// </summary>
        /// <param name="HopDong">HopDong</param>
        void UpdateHopDong(HopDong _item);
        /// <summary>
        /// Delete HopDong
        /// </summary>
        /// <param name="HopDong">HopDong</param>
        void DeleteHopDong(HopDong _item);
        List<int> GetNguoiTaoIds();
        
        #endregion
        
    }
}
