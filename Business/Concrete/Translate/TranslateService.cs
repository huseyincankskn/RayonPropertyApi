using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.VMs;
using System.Text;

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

        public string GenerateTranslateKey()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            var result = new StringBuilder(8);

            for (int i = 0; i < 8; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }

        public string GenerateUniqueTranslateKey()
        {
            var translateKeyList = _translateRepository.GetAllForOdata()
                                                       .Select(x => x.TranslateKey).ToList();
            var outTranslateKey = "";

            for (int i = 0; i < 599; i++)
            {
                var result = GenerateTranslateKey();
                if (!translateKeyList.Contains(result))
                {
                    outTranslateKey = result;
                    break;
                }
            }
            return outTranslateKey;
        }

        public IDataResult<IQueryable<TranslateVm>> GetList()
        {
            var entityList = _translateRepository.GetAll();
            var vmList = _mapper.ProjectTo<TranslateVm>(entityList);
            return new SuccessDataResult<IQueryable<TranslateVm>>(vmList);
        }
    }
}
