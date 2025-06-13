namespace ET_Frontend.Models.Enums;

/// <summary>
/// Gibt an, welche Aktion in einem Prozessschritt ausgeführt werden soll.
/// </summary>
public enum ProcessStepType
{
    /// <summary>
    /// Versendet eine E-Mail an die betroffenen Benutzer.
    /// </summary>
    SendEMail,

    /// <summary>
    /// Ändert den Status des zugehörigen Events.
    /// </summary>
    ChangeStatus
}
