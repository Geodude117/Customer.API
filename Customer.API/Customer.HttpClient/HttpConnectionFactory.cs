using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


namespace Customer.HttpClient
{
    public class HttpConnectionFactory : IHttpConnectionFactory
    {
        public HttpResponseMessage GetAsync(Uri baseUrl, string requestUrl)
        {
            using var httpClient = new System.Net.Http.HttpClient();
            httpClient.BaseAddress = baseUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient.GetAsync(requestUrl).Result;
        }
      

        public HttpResponseMessage PostAsync(Uri baseUrl, string requestUrl, object requestModel)
        {
            using var httpClient = new System.Net.Http.HttpClient();
            httpClient.BaseAddress = baseUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var httpContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

            return httpClient.PostAsync(requestUrl, httpContent).Result;
        }
       
        public HttpResponseMessage PutAsync(Uri baseUrl, string requestUrl, object requestModel)
        {
            using var httpClient = new System.Net.Http.HttpClient();
            httpClient.BaseAddress = baseUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var httpContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

            return httpClient.PutAsync(requestUrl, httpContent).Result;
        }
        public HttpResponseMessage DeleteAsync(Uri baseUrl, string requestUrl)
        {
            using var httpClient = new System.Net.Http.HttpClient();
            httpClient.BaseAddress = baseUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient.DeleteAsync(requestUrl).Result;
        }
    }
}
