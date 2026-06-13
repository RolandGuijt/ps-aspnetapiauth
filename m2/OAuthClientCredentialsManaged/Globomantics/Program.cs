using Duende.AccessTokenManagement;
using Globomantics.ApiServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IConferenceApiService, ConferenceApiService>();
builder.Services.AddScoped<IProposalApiService, ProposalApiService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddClientCredentialsTokenManagement()
    .AddClient("globoapi.client", client =>
    {
        client.TokenEndpoint = new Uri("https://localhost:5001/connect/token");

        client.ClientId = ClientId.Parse("m2m.client");
        client.ClientSecret = ClientSecret.Parse("511536EF-F270-4058-80CA-1C89C192F69A");

        client.Scope = Scope.Parse("globoapi");
    });

builder.Services.AddClientCredentialsHttpClient("globoapi",
    ClientCredentialsClientName.Parse("globoapi.client"), client =>
{
    client.BaseAddress = new Uri("https://localhost:5002");
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Conference}/{action=Index}/{id?}");


app.Run();
