namespace ET_Frontend.Models;
using Models.Enums;

/// <summary>
/// ViewModel zur Darstellung eines Prozessschritts im Frontend.
/// </summary>
public class ProcessStepViewModel
{
    /// <summary>
    /// Die eindeutige ID des Prozessschritts.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Der Anzeigename oder die Beschreibung des Schritttyps.
    /// </summary>
    public string TypeName { get; set; }

    /// <summary>
    /// Der Typ des Prozessschritts (z. B. E-Mail senden, Status ändern).
    /// </summary>
    public ProcessStepType Type { get; set; }

    /// <summary>
    /// Der Auslöser für diesen Schritt (z. B. Min. Teilnehmerzahl erreicht).
    /// </summary>
    public ProcessStepTrigger Trigger { get; set; }

    /// <summary>
    /// Eine Bedingung, die zusätzlich erfüllt sein muss (z. B. Teilnehmer > Mindestanzahl).
    /// </summary>
    public ProcessStepCondition Condition { get; set; }

    /// <summary>
    /// Zeitliche Verschiebung (Offset) in Stunden relativ zum Trigger-Ereignis.
    /// </summary>
    public int OffsetInHours { get; set; }

    /// <summary>
    /// Parameterloser Konstruktor (wird z. B. fürs Binding verwendet).
    /// </summary>
    public ProcessStepViewModel() { }

    /// <summary>
    /// Erstellt eine neue Instanz von <see cref="ProcessStepViewModel"/>.
    /// </summary>
    /// <param name="Id">Die ID des Prozessschritts.</param>
    /// <param name="TypeName">Der Anzeigename des Typs.</param>
    /// <param name="Type">Der Typ des Schritts.</param>
    /// <param name="Trigger">Der Auslöser.</param>
    /// <param name="Condition">Die zusätzliche Bedingung.</param>
    /// <param name="OffsetInHours">Zeitlicher Offset in Stunden.</param>
    public ProcessStepViewModel(int Id, string TypeName, ProcessStepType Type, ProcessStepTrigger Trigger, ProcessStepCondition Condition, int OffsetInHours)
    {
        this.Id = Id;
        this.TypeName = TypeName;
        this.Type = Type;
        this.Trigger = Trigger;
        this.Condition = Condition;
        this.OffsetInHours = OffsetInHours;
    }
}
