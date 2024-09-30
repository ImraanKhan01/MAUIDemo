using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SecureCitizen.Demo.Core.Helpers;
namespace SecureCitizen.Demo.Data.Repositories;

public class BaseRepository
{
     protected async Task<T> GetAsync<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        protected async Task<T> PostAsync<T>(string url, object body)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Bearer", Session.AccessToken);

                var bodyString = JsonConvert.SerializeObject(body);

                HttpResponseMessage response = await client.PostAsync(url, new StringContent(bodyString, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
}