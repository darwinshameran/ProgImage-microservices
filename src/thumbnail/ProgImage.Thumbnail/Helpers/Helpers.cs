using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProgImage.Resize.Models;
using Serilog;

namespace ProgImage.Resize.Helpers
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
        
        public static string ToString<T>(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return json;
        }
    }
}