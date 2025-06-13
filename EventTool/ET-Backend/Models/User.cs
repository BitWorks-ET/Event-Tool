namespace ET_Backend.Models;

/// <summary>
/// Repräsentiert einen Benutzer mit persönlichen Informationen und zugehörigen Accounts.
/// </summary>
public class User
{
    /// <summary>
    /// Die eindeutige ID des Benutzers.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Der Nachname des Benutzers.
    /// </summary>
    public string Lastname { get; set; }

    /// <summary>
    /// Der Vorname des Benutzers.
    /// </summary>
    public string Firstname { get; set; }

    /// <summary>
    /// Das verschlüsselte Passwort des Benutzers.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Die Liste der Accounts, die diesem Benutzer zugeordnet sind.
    /// </summary>
    public List<Account> Accounts { get; set; } = new List<Account>();
}
