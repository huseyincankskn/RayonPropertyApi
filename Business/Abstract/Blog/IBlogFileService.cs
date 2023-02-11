using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IBlogFileService
    {
        IDataResult<BlogFile> SaveImage(IFormFile image);
    }
}
