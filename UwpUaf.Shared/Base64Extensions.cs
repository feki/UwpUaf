using System;
using System.Text;
using Newtonsoft.Json;

namespace UwpUaf.Shared
{
    public static class Base64Extensions
    {
        public static string ConvertToBase64UrlString(this byte[] obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            
            var base64UrlString = Convert.ToBase64String(obj)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');

            return base64UrlString;
        }

        public static string ConvertToBase64UrlString(this string obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var array = Encoding.UTF8.GetBytes(obj);
            var base64UrlString = array.ConvertToBase64UrlString();

            return base64UrlString;
        }

        public static string SerializeToBase64UrlString(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var json = JsonConvert.SerializeObject(obj);
            var base64UrlString = json.ConvertToBase64UrlString();

            return base64UrlString;
        }

        public static string ConvertFromBase64UrlString(this string base64UrlString)
        {
            var base64String = base64UrlString
                .Replace('_', '/')
                .Replace('-', '+')
                .PadRight(base64UrlString.Length + (4 - base64UrlString.Length % 4) % 4, '=');

            var array = Convert.FromBase64String(base64String);
            var ret = Encoding.UTF8.GetString(array);

            return ret;
        }

        public static TResult DeserializeFromBase64UrlString<TResult>(this string base64UrlString)
        {
            if (base64UrlString == null)
            {
                throw new ArgumentNullException(nameof(base64UrlString));
            }

            var json = base64UrlString.ConvertFromBase64UrlString();
            var obj = JsonConvert.DeserializeObject<TResult>(json);

            return obj;
        }
    }
}
