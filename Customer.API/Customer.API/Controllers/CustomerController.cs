using Customer.Business.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        //Search for customer by forename, surname, postcode, or email address

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Models.Customer>>> Search([FromQuery] string forename, [FromQuery] string surename, [FromQuery] string postcode, [FromQuery] string emailAddress, [FromServices] ICustomerBusiness customerBusiness)
        {
            try
            {
                if (string.IsNullOrEmpty(forename) && string.IsNullOrEmpty(surename) &&
                   string.IsNullOrEmpty(postcode) && string.IsNullOrEmpty(emailAddress))
                {
                    return new NotFoundObjectResult("No search parameters specified");
                }

                var searchResults = await customerBusiness.SearchAsync(forename, surename, postcode, emailAddress);

                return new OkObjectResult(searchResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


            return new NotFoundObjectResult("No search parameters specified");
        }

        //Retrieve customer by ID
        [HttpGet("{guid}")]
        public async Task<ActionResult<Models.Customer>> Get(Guid guid, [FromServices] ICustomerBusiness customerBusiness)
        {
            var customer = await customerBusiness.GetAsync(guid);
            return new OkObjectResult(customer);
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
