using GrpcService1.Services;

using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

var listenport = int.Parse(Environment.GetEnvironmentVariable("LISTEN_PORT")
    ?? throw new ApplicationException("Environment variable LISTEN_PORT is not set."));
builder.WebHost
    .UseKestrel(options =>
    {
        options.ListenAnyIP(listenport, listenOptions =>
        {
            listenOptions.Protocols = HttpProtocols.Http2;
        });
    });
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
