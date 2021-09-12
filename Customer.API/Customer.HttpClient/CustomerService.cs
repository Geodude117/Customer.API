using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Customer.HttpClient
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpConnectionFactory _httpConnectionFactory;

        private string _getAllCustomerUrl, _getCustomerByIdUrl, _getCusotomerSearchUrl, _updateCustomerUrl,
            _insertCustomerUrl, _deleteCustomerUrl;

        private Uri _baseUri;


        public CustomerService()
        {
            _httpConnectionFactory = new HttpConnectionFactory();

            _baseUri = new Uri("https://localhost:44392/");

            _getAllCustomerUrl = "api/Customer";
            _getCustomerByIdUrl = "api/Customer/guid?guid={0}";
            _getCusotomerSearchUrl = "api/Customer/search?forename={0}&surename={1}&postcode={2}&emailAddress={3}";
            _updateCustomerUrl = "api/Customer";
            _insertCustomerUrl = "api/Customer";
            _deleteCustomerUrl = "api/Customer?guid={0}";
        }

        public async Task<IEnumerable<Models.Customer>> GetAllCustomers()
        {
            List<Models.Customer> customers = new List<Models.Customer>();

            try
            {
                HttpResponseMessage httpResponse = _httpConnectionFactory.GetAsync(_baseUri, _getAllCustomerUrl);

                if (httpResponse.IsSuccessStatusCode)
                {
                    customers = JsonConvert.DeserializeObject<List<Models.Customer>>(await httpResponse.Content.ReadAsStringAsync());
                }

            }
            catch (Exception ex)
            {

            }

            return customers;
        }

        public async Task<Models.Customer> GetCustomers(Guid? guid)
        {
            Models.Customer customer = new Models.Customer();

            try
            {
                _getCustomerByIdUrl = string.Format(_getCustomerByIdUrl, guid.ToString());
                HttpResponseMessage httpResponse = _httpConnectionFactory.GetAsync(_baseUri, _getCustomerByIdUrl);

                if (httpResponse.IsSuccessStatusCode)
                {
                    customer = JsonConvert.DeserializeObject<Models.Customer>(await httpResponse.Content.ReadAsStringAsync());
                }

            }
            catch (Exception ex)
            {

            }

            return customer;
        }


        public async Task<IEnumerable<Models.Customer>> SearchCustomerByCriteria(string forename, string surname, string postcode, string emailAddress)
        {
            List<Models.Customer> customers = new List<Models.Customer>();

            try
            {
                _getCusotomerSearchUrl = string.Format(_getCusotomerSearchUrl, forename, surname, postcode, emailAddress);

                HttpResponseMessage httpResponse = _httpConnectionFactory.GetAsync(_baseUri, _getCusotomerSearchUrl);

                if (httpResponse.IsSuccessStatusCode)
                {
                    customers = JsonConvert.DeserializeObject<List<Models.Customer>>(await httpResponse.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {

            }

            return customers;
        }

        public async Task<Guid?> InsertCustomer(Models.Customer customerModel)
        {
            Guid? result = null;

            try
            {
                HttpResponseMessage httpResponse = _httpConnectionFactory.PostAsync(_baseUri, _insertCustomerUrl, customerModel);

                if (httpResponse.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<Guid?>(await httpResponse.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<bool> UpdateCustomer(Models.Customer customerModel)
        {
            bool result = false;

            try
            {
                HttpResponseMessage httpResponse = _httpConnectionFactory.PutAsync(_baseUri, _updateCustomerUrl, customerModel);

                if (httpResponse.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<bool>(await httpResponse.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<bool> DeleteCustomer(Guid? guid)
        {
            bool result = false;

            try
            {
                 _deleteCustomerUrl = String.Format(_deleteCustomerUrl, guid);

                HttpResponseMessage httpResponse = _httpConnectionFactory.DeleteAsync(_baseUri, _deleteCustomerUrl);

                if (httpResponse.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<bool>(await httpResponse.Content.ReadAsStringAsync());
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
