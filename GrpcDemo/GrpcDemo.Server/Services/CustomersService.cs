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
            CustomerModel output = new CustomerModel();
            if (request.UserId == 1)
            {
                output.FirstName = "Shahab";
                output.LastName = "Noori Goodarzi";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Hirad";
                output.LastName = "Noori Goodarzi";
            }
            else
            {
                output.FirstName = "Hooman";
                output.LastName = "Noori Goodarzi";
            }
            return Task.FromResult(output);
        }
    }
}