using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookpuModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
             
            if(request.UserId == 1)
            {
                output.FirstName = "Gauri";
                output.LastName = "Bindu";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Preeti";
                output.LastName = "Bindu";
            }
            else
            {
                output.FirstName = "Prachi";
                output.LastName = "Bindu";
            }

            return Task.FromResult(output);

        }

        public override async Task GetNewCustomers(
            NewCustomerRequest request, 
            IServerStreamWriter<CustomerModel> responseStream, 
            ServerCallContext context)
        {
            var customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Gauri",
                    LastName = "Bindu",
                    Age = 30,
                    EmailAddress = "gaurisbindu@gmail.com",
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Preeti",
                    LastName = "Bindu",
                    Age = 27,
                    EmailAddress = "preetisbindu@gmail.com",
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Prachi",
                    LastName = "Bindu",
                    Age = 20,
                    EmailAddress = "prachisbindu@gmail.com",
                    IsAlive = true
                }
            };

            foreach (var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }
        }

    }
}
