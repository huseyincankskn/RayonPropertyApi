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
        public short SaloonCount { get; set; }
        public short BedCount { get; set; }
        public decimal Price { get; set; }
        public short GrossArea { get; set; }
        public short NetArea { get; set; }
        public byte ProjectStatus { get; set; }
        public byte ProjectType { get; set; }
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
        public List<string> PhotoUrls { get; set; }
        public List<string> FeatureNames { get; set; }
        public int StreetId { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string StreetName { get; set; }
        public string TownName { get; set; }
        public string StatusValue { get; set; }
        public string ProjectTypeValue { get; set; }
    }
}
