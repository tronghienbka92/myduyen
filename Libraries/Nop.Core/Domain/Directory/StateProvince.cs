
using Nop.Core.Domain.Localization;

namespace Nop.Core.Domain.Directory
{
    public class TinhThanhConfig
    {
        public static readonly int HA_NOI = 139;
        public static readonly int HO_CHI_MINH = 188;
        public static readonly int DA_NANG = 170;
        public static readonly int NGHE_AN = 165;
        public static readonly int CAN_THO = 197;
        public static readonly int HAI_PHONG = 158;
        public static readonly int LAO_CAI = 144;
    }
    /// <summary>
    /// Represents a state/province
    /// </summary>
    public partial class StateProvince : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        public virtual Country Country { get; set; }
    }

}
