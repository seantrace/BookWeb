using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.Client.Shared
{
    public class ApiHelpers
    {
        public static async Task<T2> MakeRequestAsync<T1, T2>(HttpClient client, string httpMethod, string route, T1 body = default(T1), Dictionary<string, string> postParams = null, Dictionary<string, string> queryParams = null)
        {
            string url;
            if (queryParams != null)
            {
                queryParams.Add("library", "1");
                url = QueryHelpers.AddQueryString(route, queryParams);
            }
            else
            {
                url = QueryHelpers.AddQueryString(route, "library", "1");
            }

            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(httpMethod), $"{url}");

            // This is where your content gets added to the request body
            if (body != null)
            {
                var json = JsonConvert.SerializeObject(body);
                requestMessage.Content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            }

            else if (postParams != null)
            {
                requestMessage.Content = new FormUrlEncodedContent(postParams);
            }

            HttpResponseMessage response = await client.SendAsync(requestMessage);

            string apiResponse = await response.Content.ReadAsStringAsync();
            try
            {
                // Attempt to deserialise the reponse to the desired type, otherwise throw an expetion with the response from the api.
                if (apiResponse != "")
                    return JsonConvert.DeserializeObject<T2>(apiResponse);
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred while calling the API. It responded with the following message: {response.StatusCode} {response.ReasonPhrase}");
            }

        }
    }
}

