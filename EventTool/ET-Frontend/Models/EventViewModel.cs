namespace ET_Frontend.Models.Event;

/// <summary>
/// Repräsentiert die Ansichtsdaten eines Events im Frontend.
/// </summary>
public class EventViewModel
{
    /// <summary>
    /// Die eindeutige ID des Events.
    /// </summary>
    public int EventId { get; set; }

    /// <summary>
    /// Der Name des Events.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Eine Beschreibung des Events.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Anzahl der aktuell angemeldeten Teilnehmer.
    /// </summary>
    public int Participants { get; set; }

    /// <summary>
    /// Maximale Anzahl an Teilnehmern.
    /// </summary>
    public int MaxParticipants { get; set; }

    /// <summary>
    /// Gibt an, ob der aktuelle Benutzer ein Organisator des Events ist.
    /// </summary>
    public bool IsOrganizer { get; set; }

    /// <summary>
    /// Gibt an, ob der aktuelle Benutzer für das Event angemeldet ist.
    /// </summary>
    public bool IsSubscribed { get; set; }
}
