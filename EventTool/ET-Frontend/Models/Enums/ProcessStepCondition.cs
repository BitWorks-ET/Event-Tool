namespace ET_Frontend.Models.Enums;

/// <summary>
/// Definiert Bedingungen, unter denen ein Prozessschritt ausgelöst wird.
/// </summary>
public enum ProcessStepCondition
{
    /// <summary>
    /// Wird ausgelöst, wenn die Teilnehmerzahl des Events über dem Mindestwert liegt.
    /// </summary>
    ParticipantsOverMin,

    /// <summary>
    /// Es ist keine Bedingung definiert.
    /// </summary>
    none
}
