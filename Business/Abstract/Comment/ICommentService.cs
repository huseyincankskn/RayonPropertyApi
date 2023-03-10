using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IDataResult<IQueryable<CommentVm>> GetListQueryableOdata();

        IDataResult<CommentVm> GetById(Guid id);

        IDataResult<Comment> Add(CommentAddDto dto);

        IResult Update(CommentUpdateDto dto);

        IResult Delete(Guid id);

        IDataResult<IQueryable<CommentVm>> GetListForWebSite();

        IDataResult<CommentVm> GetByIdForWebSite(Guid id);
    }
}
