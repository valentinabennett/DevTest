using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeveloperTest.Business
{
    public interface ICustomerService
    {
        Task<List<CustomerModel>> GetAllCustomers();
        Task<CustomerModel> GetCustomer(Guid customerId);
        Task<CustomerModel> CreateCustomer(BaseCustomerModel model);
    }
    public class CustomerService : ICustomerService
    {

        private readonly ApplicationDbContext context;

        public CustomerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<CustomerModel>> GetAllCustomers()
        {
            var customers =  await context.Customers.Select(x => x).ToListAsync();
            return customers.Select(x => new CustomerModel
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type
            }).ToList();
        }

        public async Task<CustomerModel> GetCustomer(Guid customerId)
        {
            var customer = await context.Customers.SingleOrDefaultAsync(x => x.Id == customerId);
            return new CustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Type = customer.Type
            };
        }

        public async Task<CustomerModel> CreateCustomer(BaseCustomerModel model)
        {
            var addedCustomer = await context.Customers.AddAsync(new Customer
            {
                Name = model.Name,
                Type = model.Type
            });

            context.SaveChanges();

            return new CustomerModel
            {
                Id = addedCustomer.Entity.Id,
                Name = addedCustomer.Entity.Name,
                Type = addedCustomer.Entity.Type
            };
        }
    }
}
