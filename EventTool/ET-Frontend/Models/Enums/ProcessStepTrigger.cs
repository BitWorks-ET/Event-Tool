namespace ET_Frontend.Models.Enums;

/// <summary>
/// Repräsentiert verschiedene Trigger, die einen Prozessschritt auslösen können.
/// </summary>
public enum ProcessStepTrigger
{
    /// <summary>
    /// Wird ausgelöst, wenn die Mindestanzahl an Teilnehmenden erreicht ist.
    /// </summary>
    MinParticipantsReached,

    /// <summary>
    /// Wird ausgelöst, wenn die maximale Teilnehmeranzahl erreicht ist.
    /// </summary>
    MaxParticipantsReached,

    /// <summary>
    /// Wird ausgelöst, wenn sich der Status des Events ändert (z. B. auf „abgesagt“).
    /// </summary>
    StatusChanged,

    /// <summary>
    /// Wird ausgelöst, wenn ein bestimmtes Datum erreicht ist.
    /// </summary>
    DateArrived,

    /// <summary>
    /// Wird beim Start des Event-Check-ins ausgelöst.
    /// </summary>
    StartOfEventLogins,

    /// <summary>
    /// Wird beim Ende des Event-Check-ins ausgelöst.
    /// </summary>
    EndOfEventLogins
}
