namespace Globomantics.Client;

public class Worker(IHttpClientFactory clientFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var client = clientFactory.CreateClient("api");
            await client.GetAsync("conference", stoppingToken);

            await Task.Delay(1000, stoppingToken);
        }
    }
}