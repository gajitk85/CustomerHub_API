

namespace CustomerHub.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        //private readonly ILogger<CustomersController> _logger;
        private readonly CustomerService _customerService;
        private readonly IHttpClientFactory _clientFactory;

        public CustomersController(CustomerService customerService,IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
          
            _customerService = customerService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{age:int}")]
        public async Task<IActionResult> Get(int age)
        {
            var customers = await _customerService.GetCustomerOfAnAge(age);
            return Ok(customers);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult>Post([FromBody] Customer customer)
        {

            var validationErrors = ModelValidation.Validate(customer);
            if (validationErrors != null && validationErrors.Count>0)
            {
                return BadRequest(new { errors = validationErrors });
            }

             var outCust= await _customerService.CreateCustomerAsync(customer);


            // Get an instance of HttpClient from the factory
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"https://ui-avatars.com/api/?name={customer.FullName}/avatar-svg");
            if (response.IsSuccessStatusCode)
            {
                 var svgData = await response.Content.ReadAsStringAsync();
                 outCust.SvgImage = svgData;
            }
            else { customer.SvgImage =string.Empty; }


            await _customerService.UpdateCustomerAsync(outCust);

            return Ok(outCust);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] Customer customer)
        {

            var validationErrors = ModelValidation.Validate(customer);
            if (validationErrors != null && validationErrors.Count > 0)
            {
                return BadRequest(new { errors = validationErrors });
            }

            customer.CustomerId = id;
            await _customerService.UpdateCustomerAsync(customer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(Guid id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }

        
    }
}
