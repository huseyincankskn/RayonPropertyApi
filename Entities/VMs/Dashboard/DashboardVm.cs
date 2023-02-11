namespace Entities.VMs
{
    public class DashboardVm
    {
        public int TotalProjectCount { get; set; }
        public int RentProjectCount { get; set; }
        public int ActiveRentProjectCount { get; set; }
        public int InActiveRentProjectCount { get; set; }
        public int SellProjectCount { get; set; }
        public int ActiveSellProjectCount { get; set; }
        public int InActiveSellProjectCount { get; set; }
        public int TotalBlogCount { get; set; }
        public int ActiveBlogCount { get; set; }
        public int InActiveBlogCount { get; set; }
        public int TotalProjectImageCount { get; set; }
    }
}
