using Microsoft.EntityFrameworkCore;
using MinimalUserApi.Data;
using MinimalUserApi.Models;

namespace MinimalUserApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _Context;
        public CustomerService(DataContext context)
        {
            _Context = context;
        }
        public bool CreateCustomer(Customer customer)
        {
            _Context.Customers.Add(customer);
            _Context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer =  await GetCustomer(id);
            _Context.Remove(customer);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer =  await _Context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            
            return customer;
            
        }

        public List<Customer> GetCustomers()
        {
            return _Context.Customers.ToList();
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var customerToUpdate = await _Context.Customers.Where(c => c.Id == customer.Id).FirstOrDefaultAsync();
            customerToUpdate.Name = customer.Name;
            customerToUpdate.Email = customer.Email;
            customerToUpdate.Password = customer.Password;

            await _Context.SaveChangesAsync();

            return true;

        }
    }
}
