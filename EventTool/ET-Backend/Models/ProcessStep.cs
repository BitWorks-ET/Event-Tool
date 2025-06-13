namespace ET_Backend.Models;

/// <summary>
/// Repräsentiert einen einzelnen Schritt innerhalb eines Prozesses.
/// </summary>
public class ProcessStep
{
    /// <summary>
    /// Eindeutige ID des Prozessschritts.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name des Prozessschritts.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ID der Organisation, der dieser Schritt zugeordnet ist.
    /// </summary>
    public int OrganizationId { get; set; }

    /// <summary>
    /// ID des Triggers, der diesen Schritt auslöst.
    /// </summary>
    public int TriggerId { get; set; }
}
