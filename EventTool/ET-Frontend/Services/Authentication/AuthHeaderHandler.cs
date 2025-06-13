using System.Net.Http.Headers;
using Blazored.SessionStorage;

/// <summary>
/// HTTP-Handler, der automatisch das JWT aus dem SessionStorage als Bearer-Token hinzufügt.
/// </summary>
public class AuthHeaderHandler : DelegatingHandler
{
    private readonly ISessionStorageService _sessionStorage;
    private const string TokenKey = "authToken";

    /// <summary>
    /// Initialisiert eine neue Instanz des <see cref="AuthHeaderHandler"/>.
    /// </summary>
    /// <param name="sessionStorage">Service für Zugriff auf SessionStorage (z. B. für das JWT).</param>
    public AuthHeaderHandler(ISessionStorageService sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }
    /// <summary>
    /// Fügt dem Request automatisch ein Authorization-Header hinzu, wenn ein Token gespeichert ist.
    /// </summary>
    /// <param name="request">Die HTTP-Anfrage, die weitergeleitet werden soll.</param>
    /// <param name="cancellationToken">Ein Token zum Abbrechen des Vorgangs.</param>
    /// <returns>Antwortnachricht vom Server.</returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _sessionStorage.GetItemAsStringAsync(TokenKey);

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}