using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IBlogService
    {
        IDataResult<IQueryable<BlogVm>> GetListQueryableOdata();

        IDataResult<BlogVm> GetById(Guid id);

        IDataResult<Blog> Add(IFormFile file,BlogAddDto dto);

        Core.Utilities.Results.IResult Update(IFormFile? file, BlogUpdateDto dto);

        Core.Utilities.Results.IResult Delete(Guid id);
    }
}
