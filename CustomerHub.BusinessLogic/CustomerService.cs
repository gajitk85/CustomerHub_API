using CustomerHub.Core.Entities;
using CustomerHub.DataAccess.Repositories;


namespace CustomerHub.BusinessLogic
{
    public class CustomerService
    {
        

        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {

            return await _customerRepository.AddAsync(new Customer
            {
                CustomerId = Guid.NewGuid(),
                FullName = customer.FullName,
                DateOfBirth = customer.DateOfBirth,
                SvgImage = customer.SvgImage

            });
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetCustomerOfAnAge(int age)
        {
            return await _customerRepository.GetByAgeAsync(age);
        }
    }      
    
}
