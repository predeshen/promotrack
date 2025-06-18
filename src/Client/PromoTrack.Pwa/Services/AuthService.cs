using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PromoTrack.Pwa.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PromoTrack.Pwa.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly ApiAuthenticationStateProvider _authenticationStateProvider;
    private const string TokenKey = "authToken";

    public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _authenticationStateProvider = (ApiAuthenticationStateProvider)authenticationStateProvider;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
        if (response.IsSuccessStatusCode)
        {
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (loginResponse?.Token != null)
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, loginResponse.Token);
                _authenticationStateProvider.NotifyUserAuthentication(loginResponse.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
            }
            return loginResponse;
        }
        return null;
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        _authenticationStateProvider.NotifyUserLogout();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}