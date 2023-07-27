using Microsoft.EntityFrameworkCore;
using MinimalUserApi.Models;

namespace MinimalUserApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers => Set<Customer>();
    }
}
