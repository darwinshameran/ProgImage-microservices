using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Transformation.Models.DTO;

namespace ProgImage.Transformation.Helpers
{
    public static class HttpHelper
    {
        private static readonly HttpClient Client = new HttpClient();
        
        public static async Task<Image> PostImageAsync(IFormFile imageFile, string url)
        {
            MultipartFormDataContent multiContent = new MultipartFormDataContent
            {
                {new ByteArrayContent(imageFile.ToBytes()), "image", "_"}
            };
            
            HttpResponseMessage response = await Client.PostAsync(url, multiContent);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                
                throw new Exception();
            }
            
            string content = await response.Content.ReadAsStringAsync();

            return content.ToObject<Image>();
        }
    }
}