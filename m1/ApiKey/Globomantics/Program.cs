using Globomantics.Client.ApiServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Registering Typed Clients
builder.Services.AddHttpClient<IConferenceApiService, ConferenceApiService>(client =>
{
    client.DefaultRequestHeaders.Add("XApiKey", "secret");
    client.BaseAddress = new Uri("https://localhost:5002");
});

builder.Services.AddHttpClient<IProposalApiService, ProposalApiService>(client =>
{
    client.DefaultRequestHeaders.Add("XApiKey", "secret");
    client.BaseAddress = new Uri("https://localhost:5002");
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Conference}/{action=Index}/{id?}");

app.Run();
