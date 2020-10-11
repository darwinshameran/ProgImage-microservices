using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ProgImage.Transformation.Helpers
{
    public static class Helpers
    {
        public static T ToObject<T>(this byte[] bytes)
        {
            var bytesToString = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<T>(bytesToString);
        }

        public static T ToObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        
        public static byte[] ToBytes(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return Encoding.UTF8.GetBytes(json);
        }
        
        public static byte[] ToBytes(this IFormFile file)
        { 
            using MemoryStream memoryStream = new MemoryStream(); 
            
            file.CopyToAsync(memoryStream); 
            
            byte[] imageBytes = memoryStream.ToArray();

            return imageBytes;
        }
        
        public static string ToString<T>(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return json;
        }
    }
}