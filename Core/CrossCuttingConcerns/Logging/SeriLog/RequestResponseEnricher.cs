using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.SeriLog
{
    public static class RequestResponseEnricher
    {
        public static async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering(bufferThreshold: 1024 * 1024, bufferLimit: 1024 * 1024);

            var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);

            return requestBody;
        }

        public static async Task<string> GetResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return responseBody;
        }
    }
}