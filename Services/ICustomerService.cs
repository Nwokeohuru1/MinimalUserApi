using MinimalUserApi.Models;

namespace MinimalUserApi.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        Task<Customer> GetCustomer(int id);
        bool CreateCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);

    }
}
