using Customer.Business.Address;
using Customer.Business.ContactInformation;
using Customer.Business.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        //Search for customer by forename, surname, postcode, or email address


        //Retrieve customer by ID
        [HttpGet("{guid}")]
        public async Task<Models.Customer> Get(Guid guid, [FromServices] ICustomerBusiness customerBusiness)
        {
            var customer = await customerBusiness.GetAsync(guid);
            return customer;
        }

        //Create a new customer
        [HttpPost]
        public async Task<Guid> Insert([FromBody] Models.Customer model, [FromServices] ICustomerBusiness customerBusiness)
        {
            var customer = await customerBusiness.InsertAsync(model);

            return customer;
        }

        //Update an existing customer

        //Delete a customer

    }
}
