using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerHub.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerHub.DataAccess.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> AddAsync(Customer entity)
        {
            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Customer entity)
        {
            try
            {
                // Perform update or delete operation
                _context.Customers.Update(entity);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    entry.Reload(); // Reload the entity from the database
                }
                // Retry the operation
                _context.Customers.Update(entity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetByAgeAsync(int age)
        {
            return _context.Customers.Where(a => (a.DateOfBirth.Date <= DateTime.Now.AddYears(-age)));
        }
    }
}
