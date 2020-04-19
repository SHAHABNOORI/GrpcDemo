using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcDemo.Server.Protos;
using Microsoft.Extensions.Logging;

namespace GrpcDemo.Server.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            var output = new CustomerModel();
            switch (request.UserId)
            {
                case 1:
                    output.FirstName = "Shahab";
                    output.LastName = "Noori Goodarzi";
                    break;
                case 2:
                    output.FirstName = "Hirad";
                    output.LastName = "Noori Goodarzi";
                    break;
                default:
                    output.FirstName = "Hooman";
                    output.LastName = "Noori Goodarzi";
                    break;
            }
            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(
            NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>()
          {
              new CustomerModel()
              {
                  FirstName = "Shahab",
                  LastName = "Noori Goodarzi",
                  Age = 32,
                  EmailAddress = "NooriGoodarzi@gmail.com",
                  IsAlive = true
              },
              new CustomerModel()
              {
                  FirstName = "Mina",
                  LastName = "Zarnaqi",
                  Age = 31,
                  EmailAddress = "MinaZarnaqi@gmail.com",
                  IsAlive = true
              },
              new CustomerModel()
              {
                  FirstName = "Hirad",
                  LastName = "Noori Goodarzi",
                  Age = 3,
                  EmailAddress = "HiradNooriGoodarzi@gmail.com",
                  IsAlive = true
              },
              new CustomerModel()
              {
                  FirstName = "Hooman",
                  LastName = "Noori Goodarzi",
                  Age = 3,
                  EmailAddress = "HoomanNooriGoodarzi@gmail.com",
                  IsAlive = true
              }
          };

            foreach (var customerModel in customers)
            {
                //await Task.Delay(3500);
                await responseStream.WriteAsync(customerModel);
            }
        }
    }
}