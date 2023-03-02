using AutoMapper;
using Business.Abstract.ContactRequest;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.VMs;

namespace Business.Concrete
{
    public class ContactRequestService : IContactRequestService
    {
        private readonly IContactRequestRepository _contactRequestRepository;
        private readonly IMapper _mapper;

        public ContactRequestService(IContactRequestRepository contactRequestRepository,
                                     IMapper mapper)
        {
            _contactRequestRepository = contactRequestRepository;
            _mapper = mapper;
        }

        public IDataResult<IQueryable<ContactRequestEntityVm>> GetListQueryableOdata()
        {
            var entityList = _contactRequestRepository.GetAllForOdataWithPassive();
            var vmList = _mapper.ProjectTo<ContactRequestEntityVm>(entityList);
            return new SuccessDataResult<IQueryable<ContactRequestEntityVm>>(vmList);
        }

        public IResult Delete(Guid id)
        {
            var entity = _contactRequestRepository.GetByIdWithPassive(id);
            if (entity == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            var modelActive = !entity.IsActive;
            entity.IsActive = modelActive;
            _contactRequestRepository.Update(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }
    }
}
