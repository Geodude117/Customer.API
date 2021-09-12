using System;
using System.Net.Http;

namespace Customer.HttpClient
{
    public interface IHttpConnectionFactory
    {
        HttpResponseMessage DeleteAsync(Uri baseURI, string requestURI);
        HttpResponseMessage GetAsync(Uri baseURI, string requestURI);
        HttpResponseMessage PostAsync(Uri baseURI, string requestURI, object requestModel);
        HttpResponseMessage PutAsync(Uri baseURI, string requestURI, object requestModel);
    }
}