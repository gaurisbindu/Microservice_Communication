using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest
            //{
            //    Name = "Gauri"
            //};

            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);


            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var customersClient = new Customer.CustomerClient(channel);

            var customerReq = new CustomerLookpuModel
            {
                UserId = 2
            };

            var customer = await customersClient.GetCustomerInfoAsync(customerReq);

            Console.WriteLine($"{customer.FirstName} {customer.LastName}");

            using (var call = customersClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while(await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}  {currentCustomer.EmailAddress}");
                }
            }

            Console.ReadLine();
        }
    }
}
