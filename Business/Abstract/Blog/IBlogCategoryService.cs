using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Abstract
{
    public interface IBlogCategoryService
    {
        IDataResult<IQueryable<BlogCategoryVm>> GetListQueryableOdata();

        IDataResult<BlogCategoryVm> GetById(Guid id);

        IDataResult<BlogCategory> Add(BlogCategoryAddDto dto);

        IResult Update(BlogCategoryUpdateDto dto);

        IResult Delete(Guid id);

        IDataResult<List<BlogCategoryVm>> GetListForWebSite();
    }
}
