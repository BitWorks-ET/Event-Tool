namespace ET_Backend.Models;

/// <summary>
/// Repräsentiert eine Organisation mit Name, Beschreibung, Domain und zugehörigen Elementen.
/// </summary>
public class Organization
{
    /// <summary>
    /// Eindeutige ID der Organisation.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name der Organisation.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Beschreibung der Organisation.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Domain der Organisation (z. B. für E-Mail-Zuordnung).
    /// </summary>
    public string Domain { get; set; }

    /// <summary>
    /// Base64-kodiertes Bild der Organisation (optional).
    /// </summary>
    public string OrgaPicAsBase64 { get; set; } = string.Empty;

    /// <summary>
    /// Liste der Events, die zu dieser Organisation gehören.
    /// </summary>
    public List<Event> Events { get; set; } = new List<Event>();

    /// <summary>
    /// Alle Accounts, die Mitglieder dieser Organisation sind.
    /// </summary>
    public List<Account> Accounts { get; set; } = new List<Account>();

    /// <summary>
    /// Prozesse, die dieser Organisation zugewiesen sind.
    /// </summary>
    public List<Process> Processes { get; set; } = new List<Process>();

    /// <summary>
    /// Prozessschritte, die dieser Organisation zugewiesen sind.
    /// </summary>
    public List<ProcessStep> ProcessSteps { get; set; } = new List<ProcessStep>();
}
