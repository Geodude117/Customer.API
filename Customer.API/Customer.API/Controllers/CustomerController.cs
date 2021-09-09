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

        [HttpGet]
        public async Task<IEnumerable<Models.Customer>> Get([FromServices] IAddressBusiness addressBusiness, [FromServices] IContactInfomationBusiness contactInfomationBusiness, [FromServices]  ICustomerBusiness customerBusiness)
        {
            var allContactInformation = await contactInfomationBusiness.GetAllAsync();
            var contactInformation = await contactInfomationBusiness.GetAsync(Guid.Parse("D3DACD64-738D-4DC8-94BC-AA308067AF36"));

            Models.ContactInformation entity = new Models.ContactInformation();

            entity.Value = "Test";
            entity.Type = "Geo";
            entity.CustomerId = Guid.Parse("D3DACD64-738D-4DC8-94BC-AA308067AF36");

            //var c = await contactInfomationBusiness.InsertAsync(entity);

            var allCustomer = await customerBusiness.GetAllAsync();
            var customer = await customerBusiness.GetAsync(Guid.Parse("D3DACD64-738D-4DC8-94BC-AA308067AF36"));

            return allCustomer;
        }
    }
}
