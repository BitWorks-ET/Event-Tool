namespace ET_Backend.Models;

/// <summary>
/// Repräsentiert eine Veranstaltung innerhalb einer Organisation.
/// </summary>
public class Event
{
    /// <summary>
    /// Eindeutige ID des Events.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name des Events.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Beschreibung des Events.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Liste aller Teilnehmer des Events.
    /// </summary>
    public List<Account> Participants { get; set; } = new List<Account>();

    /// <summary>
    /// Liste aller Organisatoren des Events.
    /// </summary>
    public List<Account> Organizers { get; set; } = new List<Account>();

    /// <summary>
    /// Liste der Ansprechpartner für das Event.
    /// </summary>
    public List<Account> ContactPersons { get; set; } = new List<Account>();

    /// <summary>
    /// Die Organisation, der das Event zugeordnet ist.
    /// </summary>
    public Organization Organization { get; set; }

    /// <summary>
    /// Der zugehörige Ablaufprozess des Events.
    /// </summary>
    public Process Process { get; set; }

    /// <summary>
    /// Startdatum des Events.
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Enddatum des Events.
    /// </summary>
    public DateOnly EndDate { get; set; }

    /// <summary>
    /// Startzeit des Events.
    /// </summary>
    public TimeOnly StartTime { get; set; }

    /// <summary>
    /// Endzeit des Events.
    /// </summary>
    public TimeOnly EndTime { get; set; }

    /// <summary>
    /// Ort, an dem das Event stattfindet.
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// Minimale Anzahl an Teilnehmern.
    /// </summary>
    public int MinParticipants { get; set; }

    /// <summary>
    /// Maximale Anzahl an Teilnehmern.
    /// </summary>
    public int MaxParticipants { get; set; }

    /// <summary>
    /// Beginn des Anmeldezeitraums.
    /// </summary>
    public DateOnly RegistrationStart { get; set; }

    /// <summary>
    /// Ende des Anmeldezeitraums.
    /// </summary>
    public DateOnly RegistrationEnd { get; set; }

    /// <summary>
    /// Gibt an, ob es sich um eine Event-Vorlage (Blueprint) handelt.
    /// </summary>
    public bool IsBlueprint { get; set; }
}
