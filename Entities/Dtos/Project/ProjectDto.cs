using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ProjectNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public short RoomCount { get; set; }
        public short SaloonCount { get; set; }
        public short BedCount { get; set; }
        public string Price { get; set; }
        public byte ProjectStatus { get; set; }
        public byte ProjectType { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public short Floor { get; set; }
        public short Year { get; set; }
        public bool IsEmpty { get; set; }
        public short BathroomCount { get; set; }
        //public string PaymnetType { get; set; } = string.Empty;
        public short GrossArea { get; set; }
        public short NetArea { get; set; }
        public short SeeClose { get; set; }
        public List<short> CheckBoxField { get; set; }
        public short CurrencyId { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public int DistrictId { get; set; }
        public int StreetId { get; set; }
    }
}
