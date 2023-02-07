using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class ProjectFeaturesRepository : GenericConstantRepository<ProjectFeature>, IProjectFeaturesRepository
    {
        public ProjectFeaturesRepository(RayonPropertyContext context) : base(context)
        {
        }
    }
}
