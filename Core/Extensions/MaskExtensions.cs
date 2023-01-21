using System.Text.RegularExpressions;

namespace Core.Extensions
{
    public static class MaskExtensions
    {
        public static string MaskCardNumber(this string cardNumber)
        {
            var reg = new Regex(@"(?<=\d{4}\d{2})\d{2}\d{4}(?=\d{4})|(?<=\d{4}( |-)\d{2})\d{2}\1\d{4}(?=\1\d{4})");

            return reg.Replace(cardNumber, new MatchEvaluator((m) => new string('*', m.Length)));
        }
    }
}