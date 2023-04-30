using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Enums;
using Entities.VMs;

namespace Business.Concrete
{
    public class DashboardService : IDashboardService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IProjectFilesRepository _projectFilesRepository;

        public DashboardService(IProjectRepository projectRepository,
                                IBlogRepository blogRepository,
                                IProjectFilesRepository projectFilesRepository)
        {
            _projectRepository = projectRepository;
            _blogRepository = blogRepository;
            _projectFilesRepository = projectFilesRepository;
        }

        public IDataResult<DashboardVm> GetDashboardInfos()
        {
            var totalProjectCount = _projectRepository.GetAllForOdataWithPassive().Count();
            var totalInactiveProjectCount = totalProjectCount - _projectRepository.GetAllForOdata().Count();
            var rentProjectCount = _projectRepository.GetAllForOdataWithPassive().Where(x => x.IsRent).Count();
            var activerentProjectCount = _projectRepository.GetAllForOdata().Where(x => x.IsRent).Count();
            var inActiveRentProjectCount = rentProjectCount - activerentProjectCount;
            var sellProjectCount = _projectRepository.GetAllForOdataWithPassive().Where(x => !x.IsRent).Count();
            var activeSellProjectCount = _projectRepository.GetAllForOdata().Where(x => !x.IsRent).Count();
            var InActiveSellProjectCount = sellProjectCount - activeSellProjectCount;
            var totalBlogCount = _blogRepository.GetAllForOdataWithPassive().Count();
            var activeBlogCount = _blogRepository.GetAllForOdata().Count();
            var inActiveBlogCount = totalBlogCount - activeBlogCount;
            var totalProjectImageCount = _projectFilesRepository.GetAllForOdataWithPassive().Count();

            var Vm = new DashboardVm()
            {
                TotalProjectCount = totalProjectCount,
                RentProjectCount = rentProjectCount,
                ActiveRentProjectCount = activerentProjectCount,
                InActiveRentProjectCount = inActiveRentProjectCount,
                SellProjectCount = sellProjectCount,
                ActiveSellProjectCount = activeSellProjectCount,
                InActiveSellProjectCount = InActiveSellProjectCount,
                TotalBlogCount = totalBlogCount,
                ActiveBlogCount = activeBlogCount,
                InActiveBlogCount = inActiveBlogCount,
                TotalProjectImageCount = totalProjectImageCount,
            };
            return new SuccessDataResult<DashboardVm>(Vm);
        }
    }
}
