using Globomantics.Client;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<AccessTokenHandler>();

builder.Services.AddHttpClient(
    "api",
    client =>
    {
        client.BaseAddress = new Uri("https://localhost:5002");
    }).AddHttpMessageHandler<AccessTokenHandler>();

var host = builder.Build();
host.Run();