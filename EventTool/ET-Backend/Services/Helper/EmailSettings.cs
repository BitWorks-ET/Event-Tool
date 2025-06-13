using ET_Backend.Services.Helper;

/// <summary>
/// Konfigurationseinstellungen für den <see cref="EMailService"/>.
/// Werte werden typischerweise über <c>appsettings.json</c> oder Umgebungsvariablen gesetzt.
/// </summary>
public sealed class EmailSettings
{
    /// <summary>
    /// Adresse des SMTP-Servers.
    /// </summary>
    public string SmtpServer { get; init; } = string.Empty;

    /// <summary>
    /// Port, der für den SMTP-Versand verwendet wird (Standard: 25).
    /// </summary>
    public int Port { get; init; } = 25;

    /// <summary>
    /// Gibt an, ob eine SSL-verschlüsselte Verbindung verwendet werden soll.
    /// </summary>
    public bool UseSsl { get; init; }

    /// <summary>
    /// Benutzername für die SMTP-Authentifizierung (optional).
    /// </summary>
    public string? UserName { get; init; }

    /// <summary>
    /// Passwort für die SMTP-Authentifizierung (optional).
    /// </summary>
    public string? Password { get; init; }

    /// <summary>
    /// Absenderadresse, die in ausgehenden E-Mails verwendet wird.
    /// </summary>
    public string FromAddress { get; init; } = "noreply@event-tool.local";

    /// <summary>
    /// Absendername, der in ausgehenden E-Mails angezeigt wird.
    /// </summary>
    public string FromName { get; init; } = "Event-Tool";
}
