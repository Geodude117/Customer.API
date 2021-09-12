using Customer.API.Validators;
using Customer.Business.Customer;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        //Search for customer by forename, surname, postcode, or email address
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Models.Customer>>> GetAll([FromServices] ICustomerBusiness customerBusiness)
        {
            try
            {
                var getAllResult = await customerBusiness.GetAllAsync();

                return new OkObjectResult(getAllResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

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
        }

        //Retrieve customer by ID
        [HttpGet("guid")]
        public async Task<ActionResult<Models.Customer>> Get(Guid? guid, [FromServices] ICustomerBusiness customerBusiness)
        {
            try
            {
                if (guid != null)
                {
                    var customer = await customerBusiness.GetAsync(guid.Value);

                    return new OkObjectResult(customer);
                }
                else
                {
                    return StatusCode(400, "No GUID was provided");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Create a new customer
        [HttpPost]
        public async Task<ActionResult<Guid?>> Insert([FromBody] Models.Customer model, [FromServices] ICustomerBusiness customerBusiness, 
            [FromServices] IValidator<Models.Customer> customerValidator)
        {
            try
            {
                var result = customerValidator.Validate(model);

                if (result.IsValid)
                {
                    var customer = await customerBusiness.InsertAsync(model);
                    return new OkObjectResult(customer.Value);
                }
                else
                {
                    return StatusCode(400, result.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Delete a customer
        [HttpDelete]
        public async Task<ActionResult<Models.Customer>> Delete(Guid? guid, [FromServices] ICustomerBusiness customerBusiness)
        {
            try
            {
                if (guid != null)
                {
                    var customer = await customerBusiness.DeleteAsync(guid.Value);

                    return new OkObjectResult(customer);
                }
                else
                {
                    return StatusCode(400, "No GUID was provided");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Update a customer
        [HttpPut]
        public async Task<ActionResult<Models.Customer>> Update([FromBody] Models.Customer model, [FromServices] ICustomerBusiness customerBusiness,
            [FromServices] IValidator<Models.Customer> customerValidator)
        {
            try
            {
                var result = customerValidator.Validate(model);

                if (result.IsValid)
                {
                    var customer = await customerBusiness.UpdateAsync(model);
                    return new OkObjectResult(customer);
                }
                else
                {
                    return StatusCode(400, result.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
