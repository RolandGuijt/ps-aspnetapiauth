using Globomantics.Client.ApiServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IConferenceApiService, ConferenceApiService>();
builder.Services.AddScoped<IProposalApiService, ProposalApiService>();

// Registering Typed Clients
builder.Services.AddHttpClient<IConferenceApiService, ConferenceApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5002");
});

builder.Services.AddHttpClient<IProposalApiService, ProposalApiService>(client =>
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
