using FluentResults;

namespace ET_Backend.Repository.Event;
/// <summary>
/// Definiert die Schnittstelle für alle Event-bezogenen Datenbankoperationen.
/// </summary>
public interface IEventRepository
{
    /// <summary>
    /// Prüft, ob ein Event mit der angegebenen ID existiert.
    /// </summary>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>Ein Result, das angibt, ob das Event existiert.</returns>
    public Task<Result<bool>> EventExists(int eventId);
    /// <summary>
    /// Erstellt ein neues Event und speichert es in der Datenbank.
    /// </summary>
    /// <param name="newEvent">Das zu erstellende Event.</param>
    /// <param name="organizationId">Die ID der Organisation, der das Event zugeordnet wird.</param>
    /// <returns>Ein Result mit dem erstellten Event.</returns>
    public Task<Result<Models.Event>> CreateEvent(Models.Event newEvent, int organizationId);
    /// <summary>
    /// Löscht ein Event samt aller zugehörigen Verknüpfungen.
    /// </summary>
    /// <param name="eventId">Die ID des zu löschenden Events.</param>
    /// <returns>Ein Result, das angibt, ob der Löschvorgang erfolgreich war.</returns>
    public Task<Result> DeleteEvent(int eventId);
    /// <summary>
    /// Ruft ein einzelnes Event anhand seiner ID ab.
    /// </summary>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>Ein Result mit dem gefundenen Event oder ein Fehler, falls nicht vorhanden.</returns>
    public Task<Result<Models.Event>> GetEvent(int eventId);
    /// <summary>
    /// Ruft alle Events einer bestimmten Organisation ab.
    /// </summary>
    /// <param name="organizationId">Die ID der Organisation.</param>
    /// <returns>Ein Result mit einer Liste der zugehörigen Events.</returns>
    public Task<Result<List<Models.Event>>> GetEventsByOrganization(int organizationId);
    /// <summary>
    /// Aktualisiert ein bestehendes Event in der Datenbank.
    /// </summary>
    /// <param name="currentEvent">Das aktualisierte Event-Objekt.</param>
    /// <returns>Ein Result, das den Erfolg der Operation angibt.</returns>
    public Task<Result> EditEvent(Models.Event currentEvent);
    /// <summary>
    /// Fügt einen Account als Teilnehmer zu einem Event hinzu.
    /// </summary>
    /// <param name="accountId">Die ID des Accounts.</param>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>Ein Result, das den Erfolg der Operation angibt.</returns>
    public Task<Result> AddParticipant(int accountId, int eventId);
    /// <summary>
    /// Entfernt einen Account als Teilnehmer aus einem Event.
    /// </summary>
    /// <param name="accountId">Die ID des Accounts.</param>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>Ein Result, das den Erfolg der Operation angibt.</returns>
    public Task<Result> RemoveParticipant(int accountId, int eventId);
}