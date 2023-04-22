using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.Concrete;
using Entities.VMs;
using Nest;
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
            var resultString = result.ToString();
            resultString = char.ToLower(resultString[0]) + resultString[1..];
            return resultString;
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

        public IDataResult<Dictionary<string, string>> GetTranslateDictionary(string locale)
        {
            var entityList = _translateRepository.GetAll();
            var vmList = _mapper.ProjectTo<TranslateVm>(entityList);

            var dictionary = new Dictionary<string, string>();

            if (locale == "en")
            {
                foreach (var item in vmList)
                {
                    dictionary.Add(item.TranslateKey, item.Key);
                }
            }
            else if (locale == "ru")
            {
                foreach (var item in vmList)
                {
                    dictionary.Add(item.TranslateKey, item.KeyRu);
                }
            }
            else if (locale == "ge")
            {
                foreach (var item in vmList)
                {
                    dictionary.Add(item.TranslateKey, item.KeyDe);
                }
            }
            else
            {
                foreach (var item in vmList)
                {
                    dictionary.Add(item.TranslateKey, item.Key);
                }
            }

            return new SuccessDataResult<Dictionary<string, string>>(dictionary);
        }
    }
}
