namespace ET.Shared.DTOs;
/// <summary>
/// Repräsentiert ein Event mit allen relevanten Eigenschaften.
/// </summary>
/// <param name="Name">Name des Events.</param>
/// <param name="Description">Beschreibung des Events.</param>
/// <param name="Location">Veranstaltungsort.</param>
/// <param name="Organizers">Liste der Organisatoren.</param>
/// <param name="ContactPersons">Liste der Kontaktpersonen.</param>
/// <param name="ProcessId">Zugehöriger Prozess (z. B. Ablauf- oder Genehmigungsprozess).</param>
/// <param name="StartDate">Startdatum des Events.</param>
/// <param name="EndDate">Enddatum des Events.</param>
/// <param name="StartTime">Startzeit am jeweiligen Tag.</param>
/// <param name="EndTime">Endzeit am jeweiligen Tag.</param>
/// <param name="MinParticipants">Minimale Teilnehmerzahl.</param>
/// <param name="MaxParticipants">Maximale Teilnehmerzahl.</param>
/// <param name="RegistrationStart">Startdatum für die Anmeldung.</param>
/// <param name="RegistrationEnd">Enddatum für die Anmeldung.</param>
/// <param name="IsBlueprint">Gibt an, ob es sich um eine Vorlage (Blueprint) handelt.</param>

public record EventDto(
    String Name, 
    String Description, 
    String Location, 
    List<string> Organizers,
    List<string> ContactPersons,
    int ProcessId, 
    DateOnly StartDate, 
    DateOnly EndDate,
    TimeOnly StartTime,
    TimeOnly EndTime, 
    int MinParticipants, 
    int MaxParticipants,
    DateOnly RegistrationStart,
    DateOnly RegistrationEnd, 
    bool IsBlueprint
    );