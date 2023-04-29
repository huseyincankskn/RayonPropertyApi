
using Business.Abstract;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Results;
using Entities.VMs;
using Nest;
using Newtonsoft.Json;
using RestSharp;
using System.Xml;

namespace Business.Concrete
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICacheManager _cacheManager;

        public CurrencyService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public IDataResult<CurrencyRateVm> GetCurrencyRates()
        {
            try
            {
                var isCached = _cacheManager.IsAdd("Currency");
                if (isCached)
                {
                    var currrencyCache = _cacheManager.Get<CurrencyRateVm>("Currency");
                    return new SuccessDataResult<CurrencyRateVm>(currrencyCache);
                }

                var client = new RestClient("https://www.tcmb.gov.tr");
                var request = new RestRequest("kurlar/today.xml");
                var response = client.Get(request)?.Content;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                string jsonText = JsonConvert.SerializeXmlNode(doc).Replace("@", "");

                var currencies = JsonConvert.DeserializeObject<CurrencyListExtVm.CurrencyList>(jsonText).TarihDate.Currency;


                var USDRate = currencies.FirstOrDefault(x => x.CurrencyCode == "USD")?.ForexBuying;
                var EurRate = currencies.FirstOrDefault(x => x.CurrencyCode == "EUR")?.ForexBuying;

                var UsdDec = Decimal.TryParse(USDRate, out decimal UsdDecimal);
                var EurDec = Decimal.TryParse(EurRate, out decimal EurDecimal);


                var UsdEurRate = Math.Round(EurDecimal / UsdDecimal, 3);
                var result = new CurrencyRateVm()
                {
                    CurrencyRate = UsdEurRate
                };
                _cacheManager.Add("Currency", result, 60);
                return new SuccessDataResult<CurrencyRateVm>(result);
            }
            catch (Exception)
            {
                return new SuccessDataResult<CurrencyRateVm>(new CurrencyRateVm() { CurrencyRate = (decimal)1.1 });
            }

        }
    }
}
