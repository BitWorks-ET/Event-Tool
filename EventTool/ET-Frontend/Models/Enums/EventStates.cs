namespace ET_Frontend.Models.Enums;

/// <summary>
/// Definiert mögliche Zustände eines Events im Event-Tool.
/// </summary>
public enum EventStates
{
    /// <summary>
    /// Das Event ist offen und kann besucht oder bearbeitet werden.
    /// </summary>
    Open,

    /// <summary>
    /// Das Event ist abgeschlossen oder nicht mehr verfügbar.
    /// </summary>
    Closed,

    /// <summary>
    /// Das Event wurde abgesagt.
    /// </summary>
    Canceled,

    /// <summary>
    /// Das Event dient als Vorlage (Blueprint) für andere Events.
    /// </summary>
    Template,

    /// <summary>
    /// Das Event wurde archiviert und ist nicht mehr aktiv.
    /// </summary>
    Archived
}
