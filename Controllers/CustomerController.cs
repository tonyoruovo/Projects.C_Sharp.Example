
using Microsoft.AspNetCore.Mvc;
using Example.Repos;

namespace Example.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customer;
        public CustomerController(ICustomerRepository customer)
        {
            _customer = customer;
        }

        // [HttpGet("{number}")]//api/customer/2348075312071
        // public IActionResult GetNumberCountry([FromRoute]string number)
        [HttpGet]//api/customer?number=2348075312071
        // public IActionResult GetNumberCountry([FromQuery]string number)
        public async Task<IActionResult> GetNumberCountry(string number)
        {
            try
            {
                return Ok(await _customer.GetCustomer(number));
            }
            catch (System.Exception)
            {
                return BadRequest();
                // return Ok($"Hello world! Number: {number}");
            }
        }
    }
}