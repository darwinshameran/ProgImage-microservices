using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ProgImage.Blur.Models.DTO;

namespace ProgImage.Blur.Helpers
{
    public static class HttpHelper
    {
        private static readonly HttpClient Client = new HttpClient();
        public static async Task<byte[]> GetImageAsync(string url)
        {
            HttpResponseMessage response = await Client.GetAsync(url);

            if (response.StatusCode != HttpStatusCode.OK)
            { 
                throw new Exception();
            }
            
            byte[] content = await response.Content.ReadAsByteArrayAsync();

            return content;
        }

        public static async Task<Image> PostImageAsync(byte[] image)
        {
            MultipartFormDataContent multiContent = new MultipartFormDataContent
            {
                {new ByteArrayContent(image), "image", "_"}
            };
            
            HttpResponseMessage response = await Client.PostAsync("http://progimage-storage:8080/api/v1/progimage/storage", multiContent);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                
                throw new Exception();
            }
            
            string content = await response.Content.ReadAsStringAsync();

            return content.ToObject<Image>();
        }
    }

}