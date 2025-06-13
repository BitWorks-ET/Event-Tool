using System.Net.Http.Headers;
using Blazored.SessionStorage;
using ET_Frontend.Helpers;

namespace ET_Frontend.Services.ApiClients;

/// <summary>
/// Kapselt alle Event-bezogenen HTTP-Aufrufe.
/// </summary>
public class EventApi : IEventApi
{
    private readonly HttpClient _http;
    private readonly ISessionStorageService _session;

    /// <summary>
    /// Erstellt eine neue Instanz der <see cref="EventApi"/>-Klasse.
    /// </summary>
    /// <param name="http">Die HttpClient-Instanz für Serveranfragen.</param>
    /// <param name="session">Der SessionStorage-Service für Zugriff auf das JWT-Token.</param>
    public EventApi(HttpClient http, ISessionStorageService session)
    {
        _http = http;
        _session = session;
    }

    /// <summary>
    /// Baut eine HTTP-Anfrage mit JWT-Authentifizierung.
    /// </summary>
    /// <param name="method">Die HTTP-Methode (z. B. GET, POST, PUT).</param>
    /// <param name="url">Die URL der API-Ressource.</param>
    /// <returns>Eine fertig konfigurierte <see cref="HttpRequestMessage"/>.</returns>
    private async Task<HttpRequestMessage> BuildRequest(HttpMethod method, string url)
    {
        var token = await _session.GetItemAsync<string>("authToken");
        var req = new HttpRequestMessage(method, url);

        if (!string.IsNullOrWhiteSpace(token))
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return req;
    }

    /// <summary>
    /// Meldet den aktuellen Benutzer zu einem Event an.
    /// </summary>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>True, wenn die Anmeldung erfolgreich war, sonst false.</returns>
    public async Task<bool> SubscribeAsync(int eventId)
    {
        var req = await BuildRequest(HttpMethod.Put, $"api/event/subscribe/{eventId}");
        var resp = await _http.SendAsync(req);
        return resp.IsSuccessStatusCode;
    }

    /// <summary>
    /// Meldet den aktuellen Benutzer von einem Event ab.
    /// </summary>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>True, wenn die Abmeldung erfolgreich war, sonst false.</returns>
    public async Task<bool> UnsubscribeAsync(int eventId)
    {
        var req = await BuildRequest(HttpMethod.Put, $"api/event/unsubscribe/{eventId}");
        var resp = await _http.SendAsync(req);
        return resp.IsSuccessStatusCode;
    }
}
