namespace Entities.Dtos
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ProjectNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public short RoomCount { get; set; }
        public string SaloonCount { get; set; }
        public string Price { get; set; }
        public decimal PriceEur { get; set; }
        public byte ProjectStatus { get; set; }
        public byte ProjectType { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public short Floor { get; set; }
        public short FloorCount { get; set; }
        public short Year { get; set; }
        public bool IsEmpty { get; set; }
        public short BathroomCount { get; set; }
        //public string PaymnetType { get; set; } = string.Empty;
        public short GrossArea { get; set; }
        public short NetArea { get; set; }
        public short SeeClose { get; set; }
        public bool IsSold { get; set; }
        public bool IsFavourite { get; set; }
        public List<short> CheckBoxField { get; set; }
        public short CurrencyId { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public int DistrictId { get; set; }
        public int StreetId { get; set; }
        public DateTime? ProjectDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public bool IsRent { get; set; }

        #region Translate
        public string TitleDe { get; set; } = string.Empty;
        public string TitleRu { get; set; } = string.Empty;
        public string DescriptionDe { get; set; } = string.Empty;
        public string DescriptionRu { get; set; } = string.Empty;
        #endregion
    }
}
