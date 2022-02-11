using Grpc.Net.Client;

using GrpcService1;

using Microsoft.AspNetCore.Mvc;

namespace GrpcSampleClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetingController : ControllerBase
    {
        private readonly ILogger<GreetingController> _logger;

        public GreetingController(ILogger<GreetingController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "GetGreeted")]
        public async Task<string> Get(string name)
        {
            var url = Environment.GetEnvironmentVariable("GRPC_SERVICE_URL") ?? String.Empty;

            using var channel= GrpcChannel.ForAddress(url);
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = name });

            return reply.Message;
        }
    }
}