using ET.Shared.DTOs;
using FluentResults;
using System.Security.Claims;

namespace ET_Backend.Services.Event;
/// <summary>
/// Definiert die Schnittstelle für Event-bezogene Service-Operationen.
/// </summary>
public interface IEventService
{
    Task<Result<Models.Event>> CreateEvent(Models.Event newEvent, int organizationId, ClaimsPrincipal user);
    Task<Result> UpdateEventAsync(EventDto dto, ClaimsPrincipal user);
    public Task<Result<List<Models.Event>>> GetEventsFromOrganization(int organizationId);
    /// <summary>
    /// Ruft alle Events einer Organisation anhand ihrer Domain ab.
    /// </summary>
    /// <param name="domain">Die Domain der Organisation (z. B. "demo.org").</param>
    /// <returns>Ein Result mit einer Liste von Events oder einem Fehler.</returns>
    public Task<Result<List<Models.Event>>> GetEventsFromOrganization(String domain);
    /// <summary>
    /// Meldet einen Account zu einem Event an.
    /// </summary>
    /// <param name="accountId">Die ID des Accounts.</param>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>Ein Result mit Erfolg oder Fehlermeldung.</returns>
    public Task<Result> SubscribeToEvent(int accountId, int eventId);
    /// <summary>
    /// Meldet einen Account von einem Event ab.
    /// </summary>
    /// <param name="accountId">Die ID des Accounts.</param>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>Ein Result mit Erfolg oder Fehlermeldung.</returns>
    public Task<Result> UnsubscribeToEvent(int accountId, int eventId);

    public Task<Result> DeleteEvent(int eventId);

    /// <summary>
    /// Ruft ein Event anhand seiner ID ab.
    /// </summary>
    /// <param name="eventId">Die ID des Events.</param>
    /// <returns>Ein Result mit dem Event oder einem Fehler.</returns>
    public Task<Result<Models.Event>> GetEvent(int eventId);
}