using System.Net.Http.Json;
using Blazored.SessionStorage;
using ET.Shared.DTOs;
using ET_Frontend.Services.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Services.Authentication;

/// <summary>
/// Implementierung des Login-Dienstes für JWT-basierte Authentifizierung.
/// </summary>
public class LoginService : ILoginService
{
    private readonly HttpClient _http;
    private readonly ISessionStorageService _storage;
    private readonly AuthenticationStateProvider _authProvider;
    private readonly NavigationManager _nav;

    private const string TokenKey = "authToken";

    /// <summary>
    /// Initialisiert eine neue Instanz des <see cref="LoginService"/>.
    /// </summary>
    /// <param name="http">Der HTTP-Client für Backend-Anfragen.</param>
    /// <param name="storage">SessionStorage-Service zum Speichern des JWT-Tokens.</param>
    /// <param name="authProvider">Provider zur Benachrichtigung über Authentifizierungsänderungen.</param>
    /// <param name="nav">NavigationManager für Redirects nach Login/Logout.</param>
    public LoginService(HttpClient http, ISessionStorageService storage,
        AuthenticationStateProvider authProvider, NavigationManager nav)
    {
        _http = http;
        _storage = storage;
        _authProvider = authProvider;
        _nav = nav;
    }
    /// <summary>
    /// Führt den Login-Prozess mit den übergebenen Anmeldedaten durch.
    /// </summary>
    /// <param name="dto">Das Login-Datenobjekt mit E-Mail und Passwort.</param>
    /// <returns>
    /// Ein Tupel mit Erfolg (`true`) oder Fehlernachricht (`false`, `string?`).
    /// </returns>
    public async Task<(bool Success, string? Error)> LoginAsync(LoginDto dto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/authenticate/login", dto);

            if (!response.IsSuccessStatusCode)
                return (false, await response.Content.ReadAsStringAsync());

            var token = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[LoginService] Received Token:\n{token}");
            await _storage.SetItemAsStringAsync(TokenKey, token);
            
            if (_authProvider is JwtAuthenticationStateProvider jwt)
                jwt.NotifyAuthenticationStateChanged();

            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, $"[EX] {ex.Message}");
        }
    }
    /// <summary>
    /// Meldet den aktuell eingeloggten Benutzer ab.
    /// </summary>
    /// <returns>Ein <see cref="Task"/> zur Steuerung des Ablaufs.</returns>
    public async Task LogoutAsync()
    {
        await _storage.RemoveItemAsync(TokenKey);

        if (_authProvider is JwtAuthenticationStateProvider jwt)
            jwt.NotifyAuthenticationStateChanged();

        _nav.NavigateTo("/login", forceLoad: true);
    }
}