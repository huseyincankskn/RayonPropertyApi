using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.VMs
{
    public class ProjectVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ProjectNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public short RoomCount { get; set; }
        public string SaloonCount { get; set; }
        public decimal Price { get; set; }
        public decimal PriceEur { get; set; }
        public decimal PriceDinar { get; set; }
        public short GrossArea { get; set; }
        public short NetArea { get; set; }
        public byte ProjectStatus { get; set; }
        public byte ProjectType { get; set; }
        public byte ProjectTye { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public short Floor { get; set; }
        public short Year { get; set; }
        public bool IsEmpty { get; set; }
        public short BathroomCount { get; set; }
        public short SeeClose { get; set; }
        public List<short> CheckBoxField { get; set; }
        public short CurrencyId { get; set; }
        public DateTime AddDate { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public int DistrictId { get; set; }
        public bool IsSold { get; set; }
        public bool IsFavourite { get; set; }
        public List<string> PhotoUrls { get; set; }
        public List<string> FeatureNames { get; set; }
        public List<short> FeatureIds { get; set; }
        public int StreetId { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string StreetName { get; set; }
        public string TownName { get; set; }
        public string StatusValue { get; set; }
        public string ProjectTypeValue { get; set; }
        public DateTime ProjectDate { get; set; }
        public bool IsActive { get; set; }

        #region Translate
        public string TitleDe { get; set; } = string.Empty;
        public string TitleRu { get; set; } = string.Empty;
        public string DescriptionDe { get; set; } = string.Empty;
        public string DescriptionRu { get; set; } = string.Empty;
        public string TitleTranslateKey { get; set; } = string.Empty;
        public string DescriptionTranslateKey { get; set; } = string.Empty;
        #endregion
    }
}
