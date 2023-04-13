using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Project : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string ProjectNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //public short RoomCount { get; set; }
        public string SaloonCount { get; set; }
        public decimal Price { get; set; }
        public decimal PriceEur { get; set; }
        public decimal PriceDinar { get; set; }
        public short GrossArea { get; set; }
        public short NetArea { get; set; }
        public short SeeClose { get; set; }
        public byte ProjectStatus { get; set; }
        public byte ProjectTye { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public short Floor { get; set; }
        public short Year { get; set; }
        //public bool IsEmpty { get; set; }
        public short BathroomCount { get; set; }
        public DateTime ProjectDate { get; set; }
        //public string PaymnetType { get; set; } = string.Empty;
        public bool IsSold { get; set; }
        public bool IsFavourite { get; set; }
        public short CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int TownId { get; set; }
        public virtual Town Town { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public int StreetId { get; set; }
        public virtual Street Street { get; set; }
        public List<ProjectFiles> ProjectFiles { get; set; }
        public List<ProjectFeature> ProjectFeatures { get; set; }
        public List<Feature> Features { get; set; }


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
