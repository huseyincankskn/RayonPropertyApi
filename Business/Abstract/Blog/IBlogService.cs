using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Abstract
{
    public interface IBlogService
    {
        IDataResult<IQueryable<BlogVm>> GetListQueryableOdata();

        IDataResult<BlogVm> GetById(Guid id);

        IDataResult<Blog> Add(BlogAddDto dto);

        IResult Update(BlogUpdateDto dto);

        IResult Delete(Guid id);
    }
}
