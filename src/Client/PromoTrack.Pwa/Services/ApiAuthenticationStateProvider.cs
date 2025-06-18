using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromoTrack.Pwa.Services;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private readonly HttpClient _httpClient;
    private const string TokenKey = "authToken";

    // This field will cache the result of the first authentication check.
    private Task<AuthenticationState>? _authenticationStateTask;

    public ApiAuthenticationStateProvider(IJSRuntime jsRuntime, HttpClient httpClient)
    {
        _jsRuntime = jsRuntime;
        _httpClient = httpClient;
    }

    // This is the main method that Blazor calls.
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // On the very first call, the task will be null, so we create it.
        // On subsequent calls, we return the cached task, preventing re-entrancy issues.
        _authenticationStateTask ??= GetAuthenticationStateFromStorageAsync();
        return _authenticationStateTask;
    }

    // This is our new private method that does the actual work.
    private async Task<AuthenticationState> GetAuthenticationStateFromStorageAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);

        if (string.IsNullOrWhiteSpace(token))
        {
            // User is not logged in.
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        try
        {
            // User has a token, so we'll try to use it.
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }
        catch
        {
            // If the token is invalid for any reason, treat the user as logged out.
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    // This method is called by our AuthService after a successful login.
    public void NotifyUserAuthentication(string token)
    {
        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);
        var authState = Task.FromResult(new AuthenticationState(user));

        // This tells Blazor to re-render with the new authenticated state.
        NotifyAuthenticationStateChanged(authState);
    }

    // This method is called by our AuthService after logout.
    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        // This tells Blazor to re-render with the new logged-out state.
        NotifyAuthenticationStateChanged(authState);
    }

    // The helper methods below are unchanged.
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        try
        {
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            if (keyValuePairs != null)
            {
                keyValuePairs.TryGetValue(ClaimTypes.Role, out object? roles);
                if (roles != null)
                {
                    if (roles.ToString()!.Trim().StartsWith("["))
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);
                        if (parsedRoles != null) { claims.AddRange(parsedRoles.Select(r => new Claim(ClaimTypes.Role, r))); }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
                    }
                }
                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));
            }
        }
        catch (Exception) { /* Fail silently */ }
        return claims;
    }
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4) { case 2: base64 += "=="; break; case 3: base64 += "="; break; }
        return Convert.FromBase64String(base64);
    }
}