using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PromoTrack.Pwa;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Configure an HttpClient to talk to our API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7276/") // Use your API's HTTPS URL
});

await builder.Build().RunAsync();