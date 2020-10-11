using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace ProgImage.Storage.Helpers
{
    public static class Extensions
    {
        
        public static byte[] ToBytes(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return Encoding.UTF8.GetBytes(json);
        }
        
        public static string ToTitleCase(this string str)
        {
            return str switch
            {
                null => throw new ArgumentNullException(nameof(str)),
                "" => throw new ArgumentException($"{nameof(str)} is invalid: cannot be empty.", nameof(str)),
                _ => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(str.ToLower())
            };
        }
    }
}