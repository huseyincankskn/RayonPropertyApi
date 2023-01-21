using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Helper
{
    public static class StringExtension
    {
        public static double ToDouble(this string str)
        {
            double value = 0;
            if (string.IsNullOrEmpty(str))
                return value;

            str = str.Replace(",", ".");

            var lastIndex = str.LastIndexOf(".");
            for (int i = 0; i < str.Count(x => x == '.'); i++)
            {
                int index = str.IndexOf(".");
                if (index != lastIndex)
                    str = str.Remove(index, 1);
            }

            double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
            return value;
        }

        public static decimal ToDecimal(this string str)
        {
            decimal value = 0;
            if (string.IsNullOrEmpty(str))
                return value;

            str = str.Replace(",", ".");

            var lastIndex = str.LastIndexOf(".");
            for (int i = 0; i < str.Count(x => x == '.'); i++)
            {
                int index = str.IndexOf(".");
                if (index != lastIndex)
                    str = str.Remove(index, 1);
            }

            decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
            return value;
        }

        public static DateTime ToDateTime(this string str, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTime.ParseExact(str, format, null);
        }

        /// <summary>
        /// convert to html string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string HtmlWhiteSpace(this string value)
        {
            return value
                .Replace("'", "")
                .Replace("\"", "")
                .Replace("<", "&gt")
                .Replace(">", "&lt")
                .Replace('\r', ' ')
                .Replace('\n', ' ');
        }

        /// <summary>
        /// null or blank checks
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value) { return value == null || value.Replace("&nbsp;", "").Trim() == string.Empty; }

        /// <summary>
        /// null or blank checks
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string input)
        {
            return (input != null && !String.IsNullOrEmpty(input.Replace("&nbsp;", "")));
        }

        public static bool IsZeroOrEmpty(this string str)
        {
            return str.Replace("&nbsp;", "0").Replace("0", "").Replace(".", "").Replace(",", "") == "";
        }

        public static bool IsZeroOrNullOrEmpty(this string str)
        {
            return str == null || str.Replace("&nbsp;", "0").Replace("0", "").Replace(".", "").Replace(",", "").Trim() == string.Empty;
        }

        public static bool IsNotZeroOrNullOrEmpty(this string str)
        {
            return !str.IsZeroOrNullOrEmpty();
        }

        /// <summary>
        /// The first character large
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UpperCaseFirst(this string value)
        {
            //testing 1,000,000 iterations of these functions in a loop over the string "michael". This test is in my ASP.NET application, so the environment is the same as it will be in my ASP.NET app as it is updated on the Internet.
            // UppercaseFirst method:           0.1636 seconds
            // UppercaseFirst with ToCharArray: 0.0920 seconds [faster]

            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            char[] a = value.ToCharArray();
            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }

        /// <summary>
        /// Every sentence begins with a capital letter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UpperCaseFirstExt(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            string temp = "";

            foreach (var item in value.Split('.', '\r', '\n', ':'))
            {
                temp = item.UpperCaseFirst();
            }

            return temp;
        }

        /// <summary>
        /// The first character is always capitalized
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UppercaseWords(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            string temp = "";

            foreach (var item in value.Split(' '))
            {
                temp = item.UpperCaseFirst();
            }

            return temp;
        }

        public static bool IsValidUrl(this string text)
        {
            var regex = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return regex.IsMatch(text);
        }

        /// <summary>
        /// Url syntax check
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EMailIsValid(this string value)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(value);
        }

        /// <summary>
        /// Truncates the string to a specified length and replace the truncated to a ...
        /// </summary>
        /// <param name="text">string that will be truncated</param>
        /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string text, int maxLength)
        {
            // replaces the truncated string to a ...
            const string suffix = "...";
            string truncatedString = text;

            if (maxLength <= 0) return truncatedString;
            int strLength = maxLength - suffix.Length;

            if (strLength <= 0) return truncatedString;

            if (text == null || text.Length <= maxLength) return truncatedString;

            truncatedString = text.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }

        /// <summary>
        /// Strong type parse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Parse<T>(this string value)
        {
            /// int i = "123".Parse<int>();
            /// int? inull = "123".Parse<int?>();
            /// DateTime d = "01/12/2008".Parse<DateTime>();
            /// DateTime? dn = "01/12/2008".Parse<DateTime?>();

            // Get default value for type so if string
            // is empty then we can return default value.
            T result = default;
            if (!string.IsNullOrEmpty(value))
            {
                // we are not going to handle exception here
                // if you need SafeParse then you should create
                // another method specially for that.
                var tc = TypeDescriptor.GetConverter(typeof(T));
                result = (T)tc.ConvertFrom(value);
            }
            return result;
        }

        /// <summary>
        /// Converts the string representation of a Id to its Id
        /// equivalent. A return value indicates whether the operation
        /// succeeded.
        /// </summary>
        /// <param name="s">A string containing a Id to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the Id value equivalent to
        /// the Id contained in <paramref name="s"/>, if the conversion
        /// succeeded, or <see cref="Id.Empty"/> if the conversion failed.
        /// The conversion fails if the <paramref name="s"/> parameter is a
        /// <see langword="null" /> reference (<see langword="Nothing" /> in
        /// Visual Basic), or is not of the correct format.
        /// </param>
        /// <value>
        /// <see langword="true" /> if <paramref name="s"/> was converted
        /// successfully; otherwise, <see langword="false" />.
        /// </value>
        /// <exception cref="ArgumentNullException">
        ///        Thrown if <pararef name="s"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Original code at https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=94072&wa=wsignin1.0#tabs
        ///
        /// </remarks>
        public static bool IsId(this string s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            var regex = new Regex(
                "^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
            var match = regex.Match(s);

            return match.Success;
        }

        /// <summary>
        /// Format string value
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        /// <param name="additionalArgs"></param>
        /// <returns></returns>
        public static string Format(this string format, object arg, params object[] additionalArgs)
        {
            if (additionalArgs == null || additionalArgs.Length == 0)
            {
                return string.Format(format, arg);
            }
            else
            {
                return string.Format(format, new object[] { arg }.Concat(additionalArgs).ToArray());
            }
        }

        /// <summary>
        /// numeric checks
        /// </summary>
        /// <param name="theValue"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string theValue)
        {
            return long.TryParse(theValue, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out _);
        }

        /// <summary>
        /// Returns an enumerable collection of the specified type containing the substrings in this instance that are delimited by elements of a specified Char array
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="separator">
        /// An array of Unicode characters that delimit the substrings in this instance, an empty array containing no delimiters, or null.
        /// </param>
        /// <typeparam name="T">
        /// The type of the elemnt to return in the collection, this type must implement IConvertible.
        /// </typeparam>
        /// <returns>
        /// An enumerable collection whose elements contain the substrings in this instance that are delimited by one or more characters in separator.
        /// </returns>
        public static IEnumerable<T> SplitTo<T>(this string str, params char[] separator) where T : IConvertible
        {
            foreach (var s in str.Split(separator, StringSplitOptions.None))
                yield return (T)Convert.ChangeType(s, typeof(T));
        }

        public static string Nl2BrHTML(this string s)
        {
            return s.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }

        /// <summary>
        /// Send an email using the supplied string.
        /// </summary>
        /// <param name="body">String that will be used i the body of the email.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="sender">The email address from which the message was sent.</param>
        /// <param name="recipient">The receiver of the email.</param>
        /// <param name="server">The server from which the email will be sent.</param>
        /// <returns>A boolean value indicating the success of the email send.</returns>
        public static bool SendEmail(this string body, string subject, string sender, string recipient, string server)
        {
            try
            {
                // To
                MailMessage mailMsg = new MailMessage();
                mailMsg.To.Add(recipient);

                // From
                MailAddress mailAddress = new MailAddress(sender);
                mailMsg.From = mailAddress;

                // Subject and Body
                mailMsg.Subject = subject;
                mailMsg.Body = body;

                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient(server);
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// strong password check
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsStrongPassword(this string s)
        {
            bool isStrong = Regex.IsMatch(s, @"[\d]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[a-z]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[A-Z]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[\s~!@#\$%\^&\*\(\)\{\}\|\[\]\\:;'?,.`+=<>\/]");
            if (isStrong) isStrong = s.Length > 7;
            return isStrong;
        }

        /// <summary>
        /// if the string is empty, converts it to null.
        /// </summary>
        /// <param name="inString">The in string.</param>
        /// <returns></returns>
        public static string TurnNullThenEmpty(this string inString)
        {
            if (inString.IsNullOrEmpty())
                return null;
            else
                return inString;
        }

        public static int? TurnNullThenEmpty(this int? inValue)
        {
            if (inValue == null || inValue == 0)
                return null;
            else
                return inValue;
        }

        public static float? TurnNullThenEmpty(this float? inValue)
        {
            if (inValue == null || inValue == 0)
                return null;
            else
                return inValue;
        }

        public static long? TurnNullThenEmpty(this long? inValue)
        {
            if (inValue == null || inValue == 0)
                return null;
            else
                return inValue;
        }

        public static short? TurnNullThenEmpty(this short? inValue)
        {
            if (inValue == null || inValue == 0)
                return null;
            else
                return inValue;
        }

        public static decimal? TurnNullThenEmpty(this decimal? inValue)
        {
            if (inValue == null || inValue == 0)
                return null;
            else
                return inValue;
        }

        public static byte? TurnNullThenEmpty(this byte? inValue)
        {
            if (inValue == null || inValue == 0)
                return null;
            else
                return inValue;
        }

        public static double? TurnNullThenEmpty(this double? inValue)
        {
            if (inValue == null || inValue == 0)
                return null;
            else
                return inValue;
        }

        public static DateTime? TurnNullThenEmpty(this DateTime? inValue)
        {
            if (inValue == null || inValue == new DateTime(1, 1, 1))
                return null;
            else
                return inValue;
        }

        public static bool? TurnNullThenEmpty(this bool? inValue)
        {
            if (inValue == null)
                return null;
            else
                return inValue;
        }

        /// <summary>
        /// if the string is NULL, converts it to string.empty. Helpful when trying to avoid null conditions.
        /// </summary>
        /// <param name="inString"></param>
        /// <returns></returns>
        public static string TurnEmptyThenNull(this string inString)
        {
            if (inString == null)
                return null;
            else
                return inString.Trim() == "" ? null : inString;
        }

        /// <summary>
        /// date checked
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDate(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                DateTime dt;
                return (DateTime.TryParse(input, out dt));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Read and get MD5 hash value of any given filename.
        /// </summary>
        /// <param name="filename">full path and filename</param>
        /// <returns>lowercase MD5 hash value</returns>
        public static string GetMD5FromFile(this string filename)
        {
            string hashData;

            FileStream fileStream;
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();

            fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            byte[] arrByteHashValue = md5Provider.ComputeHash(fileStream);
            fileStream.Close();

            hashData = BitConverter.ToString(arrByteHashValue).Replace("-", "").ToLower();

            return hashData;
        }

        /// <summary>
        /// ip checked
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidIPAddress(this string s)
        {
            return Regex.IsMatch(s,
                    @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }

        /// <summary>
        /// Convert a (A)RGB string to a Color object
        /// </summary>
        /// <param name="argb">An RGB or an ARGB string</param>
        /// <returns>a Color object</returns>
        public static Color ToColor(this string argb)
        {
            argb = argb.Replace("#", "");
            byte a = Convert.ToByte("ff", 16);
            byte pos = 0;
            if (argb.Length == 8)
            {
                a = Convert.ToByte(argb.Substring(pos, 2), 16);
                pos = 2;
            }
            byte r = Convert.ToByte(argb.Substring(pos, 2), 16);
            pos += 2;
            byte g = Convert.ToByte(argb.Substring(pos, 2), 16);
            pos += 2;
            byte b = Convert.ToByte(argb.Substring(pos, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// JavaScript style Eval for simple calculations
        /// http://www.osix.net/modules/article/?id=761
        /// This is a safe evaluation.  IE will not allow for injection.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string CalculateByEvaluate(this string e)
        {
            static bool VerifyAllowed(string e1)
            {
                string allowed = "0123456789+-*/()%.,";
                for (int i = 0; i < e1.Length; i++)
                {
                    if (allowed.IndexOf("" + e1[i]) == -1)
                    {
                        return false;
                    }
                }
                return true;
            }

            if (e.Length == 0) { return string.Empty; }
            if (!VerifyAllowed(e)) { return "String contains illegal characters"; }
            if (e[0] == '-') { e = "0" + e; }
            string res = "";
            try
            {
                res = Calculate(e).ToString();
            }
            catch
            {
                return "The call caused an exception";
            }
            return res;
        }

        /// <summary>
        /// JavaScript Eval Calculations for simple calculations
        /// http://www.osix.net/modules/article/?id=761
        /// This is an unsafe calculation. IE may allow for injection.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static double Calculate(this string e)
        {
            e = e.Replace(".", ",");
            if (e.IndexOf("(") != -1)
            {
                int a = e.LastIndexOf("(");
                int b = e.IndexOf(")", a);
                double middle = Calculate(e.Substring(a + 1, b - a - 1));
                return Calculate(e.Substring(0, a) + middle.ToString() + e.Substring(b + 1));
            }
            double result = 0;
            string[] plus = e.Split('+');
            if (plus.Length > 1)
            {
                // there were some +
                result = Calculate(plus[0]);
                for (int i = 1; i < plus.Length; i++)
                {
                    result += Calculate(plus[i]);
                }
                return result;
            }
            else
            {
                // no +
                string[] minus = plus[0].Split('-');
                if (minus.Length > 1)
                {
                    // there were some -
                    result = Calculate(minus[0]);
                    for (int i = 1; i < minus.Length; i++)
                    {
                        result -= Calculate(minus[i]);
                    }
                    return result;
                }
                else
                {
                    // no -
                    string[] mult = minus[0].Split('*');
                    if (mult.Length > 1)
                    {
                        // there were some *
                        result = Calculate(mult[0]);
                        for (int i = 1; i < mult.Length; i++)
                        {
                            result *= Calculate(mult[i]);
                        }
                        return result;
                    }
                    else
                    {
                        // no *
                        string[] div = mult[0].Split('/');
                        if (div.Length > 1)
                        {
                            // there were some /
                            result = Calculate(div[0]);
                            for (int i = 1; i < div.Length; i++)
                            {
                                result /= Calculate(div[i]);
                            }
                            return result;
                        }
                        else
                        {
                            // no /
                            string[] mod = mult[0].Split('%');
                            if (mod.Length > 1)
                            {
                                // there were some %
                                result = Calculate(mod[0]);
                                for (int i = 1; i < mod.Length; i++)
                                {
                                    result %= Calculate(mod[i]);
                                }
                                return result;
                            }
                            else
                            {
                                // no %
                                return double.Parse(e);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Convert a input string to a byte array and compute the hash.
        /// </summary>
        /// <param name="value">Input string.</param>
        /// <returns>Hexadecimal string.</returns>
        public static string ToMd5Hash(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] originalBytes = ASCIIEncoding.Default.GetBytes(value);
                byte[] encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes).Replace("-", string.Empty);
            }
        }

        public static bool IsTrue(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            value = value.Trim().ToLower();
            return (value == "1" || value == "var" || value == "yes" || value == "ok" || value == "evet" || value == "true");
        }

        /// <summary>
        /// Determines the value is in list.
        /// </summary>
        /// <param name="val">The value to be wanted.</param>
        /// <param name="list">The list to seek.</param>
        /// <returns>Returns the value is in the list.</returns>
        public static bool IsIn(this string val, params string[] list)
        {
            return list.Contains(val);
        }

        public static string ToSlug(this string text)
        {
            text = text.ToLower();
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);
            string str = Encoding.ASCII.GetString(bytes).ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }

        public static void WriteOnConsole(this string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static string GetRequestRolePath(this string text)
        {
            var str = text.Split('/')[2];
            return str;
        }
    }
}