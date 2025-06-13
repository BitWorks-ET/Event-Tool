namespace ET_Backend.Models;

/// <summary>
/// Repräsentiert ein Benutzerkonto mit zugehöriger Organisation, Benutzer, Rolle und Events.
/// </summary>
public class Account
{
    /// <summary>
    /// Eindeutige ID des Accounts.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// E-Mail-Adresse des Benutzers.
    /// </summary>
    public string EMail { get; set; }

    /// <summary>
    /// ID des zugehörigen Benutzers.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Verweis auf den Benutzer, dem dieses Konto gehört.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Verweis auf die Organisation, zu der das Konto gehört.
    /// </summary>
    public Organization Organization { get; set; }

    /// <summary>
    /// Gibt an, ob das Konto verifiziert ist.
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// Rolle des Benutzers innerhalb der Organisation.
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Liste der Events, für die dieser Account zuständig ist.
    /// </summary>
    public List<Event> Events { get; set; } = new List<Event>();
}
