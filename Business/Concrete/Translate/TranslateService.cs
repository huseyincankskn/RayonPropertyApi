using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.VMs;

namespace Business.Concrete
{
    public class TranslateService : ITranslateService
    {
        private readonly ITranslateRepository _translateRepository;
        private readonly IMapper _mapper;

        public TranslateService(ITranslateRepository translateRepository,
                                IMapper mapper)
        {
            _translateRepository = translateRepository;
            _mapper = mapper;
        }

        public IDataResult<IQueryable<TranslateVm>> GetList()
        {
            var entityList = _translateRepository.GetAll();
            var vmList = _mapper.ProjectTo<TranslateVm>(entityList);
            return new SuccessDataResult<IQueryable<TranslateVm>>(vmList);
        }
    }
}
