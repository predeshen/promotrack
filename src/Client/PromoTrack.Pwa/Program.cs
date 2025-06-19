using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using PromoTrack.Pwa;
using PromoTrack.Pwa.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// --- THIS IS THE CRITICAL FIX ---
// Configure HttpClient to talk to the API at its correct HTTP address and port.

if (builder.HostEnvironment.IsDevelopment())
{
    builder.Services.AddScoped(sp => new HttpClient
    {
        BaseAddress = new Uri("http://localhost:8081/")
    });
}
else
{
    builder.Services.AddScoped(sp => new HttpClient
    {
        BaseAddress = new Uri("http://15.207.86.151/")
    });
}

// The rest of the file remains the same.
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();