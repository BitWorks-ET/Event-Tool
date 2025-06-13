using ET_Backend.Repository.Event;
using ET_Backend.Repository.Organization;
using ET_Backend.Repository.Person;
using FluentResults;

namespace ET_Backend.Services.Event;
/// <summary>
/// Service für die Verarbeitung und Verwaltung von Event-bezogenen Vorgängen.
/// Nutzt Repositorys für Events, Organisationen und Accounts.
/// </summary>
public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IAccountRepository _accountRepository;
    /// <summary>
    /// Initialisiert eine neue Instanz des <see cref="EventService"/> mit den angegebenen Repositorys.
    /// </summary>
    public EventService(IEventRepository eventRepository, IOrganizationRepository organizationRepository, IAccountRepository accountRepository)
    {
        _eventRepository = eventRepository;
        _organizationRepository = organizationRepository;
        _accountRepository = accountRepository;
    }
    /// <summary>
    /// Gibt alle Events einer Organisation anhand ihrer ID zurück.
    /// </summary>
    /// <param name="organizationId">Die ID der Organisation.</param>
    /// <returns>Liste von Events oder ein Fehlerresultat.</returns>
    public async Task<Result<List<Models.Event>>> GetEventsFromOrganization(int organizationId)
    {
        return await _eventRepository.GetEventsByOrganization(organizationId);
    }
    /// <summary>
    /// Gibt alle Events einer Organisation anhand ihrer Domain zurück.
    /// </summary>
    /// <param name="domain">Die Domain der Organisation (z.B. "demo.org").</param>
    /// <returns>Liste von Events oder ein Fehlerresultat.</returns>
    public async Task<Result<List<Models.Event>>> GetEventsFromOrganization(String domain)
    {
        Result<Models.Organization> organization = await _organizationRepository.GetOrganization(domain);
        return await GetEventsFromOrganization(organization.Value.Id);
    }
    /// <summary>
    /// Fügt einen Teilnehmer zu einem Event hinzu.
    /// </summary>
    /// <param name="accountId">Die ID des Accounts.</param>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>Erfolgs- oder Fehlerresultat.</returns>
    public async Task<Result> SubscribeToEvent(int accountId, int eventId)
        => await _eventRepository.AddParticipant(accountId, eventId);
    /// <summary>
    /// Entfernt einen Teilnehmer von einem Event.
    /// </summary>
    /// <param name="accountId">Die ID des Accounts.</param>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>Erfolgs- oder Fehlerresultat.</returns>
    public async Task<Result> UnsubscribeToEvent(int accountId, int eventId)
        => await _eventRepository.RemoveParticipant(accountId, eventId);
    /// <summary>
    /// Erstellt ein neues Event für eine Organisation.
    /// </summary>
    /// <param name="newEvent">Das zu erstellende Event.</param>
    /// <param name="organizationId">Die ID der Organisation.</param>
    /// <returns>Das erstellte Event oder ein Fehlerresultat.</returns>
    public async Task<Result<Models.Event>> CreateEvent(Models.Event newEvent, int organizationId)
    {
        return await _eventRepository.CreateEvent(newEvent, organizationId);
    }
    /// <summary>
    /// Löscht ein Event anhand seiner ID.
    /// </summary>
    /// <param name="eventId">Die ID des zu löschenden Events.</param>
    /// <returns>Erfolgs- oder Fehlerresultat.</returns>
    public async Task<Result> DeleteEvent(int eventId)
    {
        return await _eventRepository.DeleteEvent(eventId);
    }
    /// <summary>
    /// Gibt die Details eines bestimmten Events anhand seiner ID zurück.
    /// </summary>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>Event-Details oder ein Fehlerresultat.</returns>
    public async Task<Result<Models.Event>> GetEvent(int eventId)
        => await _eventRepository.GetEvent(eventId); 
}