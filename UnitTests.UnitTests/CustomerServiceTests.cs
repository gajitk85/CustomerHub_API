
namespace UnitTests.UnitTests
{
    public class CustomerServiceTests
    {
        private readonly Mock<IRepository<Customer>> _customerRepositoryMock;
        private readonly CustomerService _customerService;

        public

    CustomerServiceTests()
        {
            _customerRepositoryMock = new Mock<IRepository<Customer>>();
            _customerService = new CustomerService(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateCustomerAsync_ShouldCreateCustomer_WhenValidCustomerIsPassed()
        {
            // Arrange
            var customer = new Customer
            {
                FullName = "John Doe",
                DateOfBirth = new DateTime(1980, 1, 1)
            };

            // Act
            await _customerService.CreateCustomerAsync(customer);

            // Assert
            _customerRepositoryMock.Verify(m => m.AddAsync(It.Is<Customer>(c => c.FullName == customer.FullName && c.DateOfBirth == customer.DateOfBirth)));
        }

        [Fact]
        public async Task UpdateCustomerAsync_ShouldUpdateCustomer_WhenValidCustomerIsPassed()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FullName = "John Doe",
                DateOfBirth = new DateTime(1980, 1, 1)
            };

            // Act
            await _customerService.UpdateCustomerAsync(customer);

            // Assert
            _customerRepositoryMock.Verify(m => m.UpdateAsync(customer));
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldDeleteCustomer_WhenValidIdIsPassed()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            await _customerService.DeleteCustomerAsync(id);

            // Assert
            _customerRepositoryMock.Verify(m => m.DeleteAsync(id));
        }

        [Fact]
        public async Task GetAllCustomersAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
        {
            new Customer
            {
                CustomerId = Guid.NewGuid(),
                FullName = "John Doe",
                DateOfBirth = new DateTime(1980, 1, 1)
            },
            new Customer
            {
                CustomerId = Guid.NewGuid(),
                FullName = "Jane Doe",
                DateOfBirth = new DateTime(1985, 2, 2)
            }
        };

            _customerRepositoryMock.Setup(m => m.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var results = await _customerService.GetAllCustomersAsync();

            // Assert
            Assert.Equal(customers, results);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnCustomer_WhenValidIdIsPassed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var customer = new Customer
            {
                CustomerId = id,
                FullName = "John Doe",
                DateOfBirth = new DateTime(1980, 1, 1)
            };

            _customerRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetCustomerByIdAsync(id);

            // Assert
            Assert.Equal(customer, result);
        }

        [Fact]
        public async Task GetCustomerOfAnAge_ShouldReturnCustomersOfThatAge()
        {
            // Arrange
            var age = 30;
            var customers = new List<Customer>
        {
            new Customer
            {
                CustomerId = Guid.NewGuid(),
                FullName = "John Doe",
                DateOfBirth = new DateTime(1993, 1, 1)
            },
            new Customer
            {
                CustomerId = Guid.NewGuid(),
                FullName = "Jane Doe",
                DateOfBirth = new DateTime(1992, 2, 2)
            }
        };

            _customerRepositoryMock.Setup(m => m.GetByAgeAsync(age)).ReturnsAsync(customers);

        }
    }
}
