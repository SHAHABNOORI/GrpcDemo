using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcDemo.Server.Protos;
using GrpcDemo.Server.Services;

namespace GrpcDemo.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = new HelloRequest() { Name = "Shahab Noori Goodarzi" };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var greeterClient = new Greeter.GreeterClient(channel);
            var greeterReply = await greeterClient.SayHelloAsync(input);
            Console.WriteLine(greeterReply.Message);


            var customerClient = new Customer.CustomerClient(channel);
            var customerReply = await customerClient.GetCustomerInfoAsync(new CustomerLookupModel() { UserId = 1 });
            Console.WriteLine($"{customerReply.FirstName} {customerReply.LastName}");

            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext(CancellationToken.None))
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} {currentCustomer.EmailAddress}");
                }
            }
            Console.ReadKey();
        }
    }
}
