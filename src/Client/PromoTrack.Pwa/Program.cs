using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using PromoTrack.Pwa;
using PromoTrack.Pwa.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// --- This is the part that needs to change ---
builder.Services.AddScoped(sp => new HttpClient
{
    // Use the port we exposed for the API in docker-compose.yml
    BaseAddress = new Uri("https://localhost:8081/")
});


// The rest of the file remains the same
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();