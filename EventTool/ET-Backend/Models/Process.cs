namespace ET_Backend.Models;

/// <summary>
/// Repräsentiert einen Prozess innerhalb einer Organisation.
/// </summary>
public class Process
{
    /// <summary>
    /// Eindeutige ID des Prozesses.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name des Prozesses.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ID der Organisation, zu der dieser Prozess gehört.
    /// </summary>
    public int OrganizationId { get; set; }
}
