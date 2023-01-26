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
        public decimal Price { get; set; }
        public string MinArea { get; set; } = string.Empty;
        public string MaxArea { get; set; } = string.Empty;
        public byte ProjectStatus { get; set; }
        public byte ProjectTye { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public short Floor { get; set; }
        public short Year { get; set; }
        public bool IsEmpty { get; set; }
        public short BathroomCount { get; set; }
        //public string PaymnetType { get; set; } = string.Empty;
        public string MinSeeClose { get; set; } = string.Empty;
        public string MaxSeeClose { get; set; } = string.Empty;
        public byte Features { get; set; }
        public List<byte> FeaturesList { get; set; }
        public short CurrencyId { get; set; }
    }
}
