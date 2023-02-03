using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Project : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string ProjectNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public short RoomCount { get; set; }
        public short SaloonCount { get; set; }
        public short BedCount { get; set; }
        public decimal Price { get; set; }
        public string Area { get; set; } = string.Empty; 
        public byte ProjectStatus { get; set; }
        public byte ProjectTye { get; set; } 
        public string ZipCode { get; set; } = string.Empty; 
        public short Floor { get; set; }
        public short Year { get; set; }
        public bool IsEmpty { get; set; }
        public short BathroomCount { get; set; }
        //public string PaymnetType { get; set; } = string.Empty;
        public string SeeClose { get; set; } = string.Empty;
        public byte Features { get; set; }
        public short CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public List<ProjectFiles> ProjectFiles { get; set; }
    }
}
