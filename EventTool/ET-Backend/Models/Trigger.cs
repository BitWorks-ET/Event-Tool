namespace ET_Backend.Models;

/// <summary>
/// Repräsentiert einen Auslöser (Trigger), der bei bestimmten Bedingungen einen Prozessschritt starten kann.
/// </summary>
public class Trigger
{
    /// <summary>
    /// Die eindeutige ID des Triggers.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Das Attribut, das diesen Trigger beschreibt (z. B. eine Bedingung oder ein Ereignis).
    /// </summary>
    public string Attribute { get; set; }
}
