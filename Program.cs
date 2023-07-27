
using Microsoft.EntityFrameworkCore;
using MinimalUserApi.Data;
using MinimalUserApi.Models;
using MinimalUserApi.Services;

namespace MinimalUserApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //var conn = builder.Configuration.GetConnectionString("SqlConn");
            builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlConn")));
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapGet("/customer/GetAllCustomer", (ICustomerService service) =>
            {
                return service.GetCustomers();


            });
            app.MapGet("/customer/GetCustomer/{id}", async (ICustomerService service, int id) =>
            {
                var customer =  await service.GetCustomer(id);
                if (customer == null) return Results.NotFound("customer not found");
               
                return Results.Ok(customer);
            });


            app.MapPost("/customer/CreateCustomer", (ICustomerService service, Customer customer) =>
            {
                service.CreateCustomer(customer);
                return Results.Ok("Customer Created");
               
            });
            app.MapPost("/customer/UpdateCustomer", async (ICustomerService service, Customer customer) =>
            {
                var result = await service.GetCustomer(customer.Id);
                if (result == null)return Results.BadRequest("Customer not found");
                await service.UpdateCustomer(customer);

                return Results.Ok("Update Done");

            });
            app.MapPost("/customerr/DeleteCustomer/{id}", async (ICustomerService service, int id) =>
            {
                var result = await service.GetCustomer(id);
                if (result == null)return Results.BadRequest("Customer not found");
                await service.DeleteCustomer(id);

                return Results.Ok("Customer Deleted");
            });


            app.Run();

        }
    }
   
  
}