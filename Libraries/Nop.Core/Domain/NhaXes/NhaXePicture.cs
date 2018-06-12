
using Nop.Core.Domain.Media;

namespace Nop.Core.Domain.NhaXes
{
    /// <summary>
    /// Represents a product picture mapping
    /// </summary>
    public partial class NhaXePicture : BaseEntity
    {
       
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int NhaXe_Id { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public int Picture_Id { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
        
    }

}
