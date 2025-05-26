﻿using ET_Backend.Repository.Event;
using ET_Backend.Repository.Organization;
using ET_Backend.Repository.Person;
using FluentResults;

namespace ET_Backend.Services.Event;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IAccountRepository _accountRepository;

    public EventService(IEventRepository eventRepository, IOrganizationRepository organizationRepository, IAccountRepository accountRepository)
    {
        _eventRepository = eventRepository;
        _organizationRepository = organizationRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Result<List<Models.Event>>> GetEventsFromOrganization(int organizationId)
    {
        return await _eventRepository.GetEventsByOrganization(organizationId);
    }

    public async Task<Result<List<Models.Event>>> GetEventsFromOrganization(String domain)
    {
        Result<Models.Organization> organization = await _organizationRepository.GetOrganization(domain);
        return await GetEventsFromOrganization(organization.Value.Id);
    }

    public async Task<Result> SubscribeToEvent(int accountId, int eventId)
        => await _eventRepository.AddParticipant(accountId, eventId);

    public async Task<Result> UnsubscribeToEvent(int accountId, int eventId)
        => await _eventRepository.RemoveParticipant(accountId, eventId);

    public async Task<Result<Models.Event>> CreateEvent(Models.Event newEvent, int organizationId)
    {
        return await _eventRepository.CreateEvent(newEvent, organizationId);
    }

    public async Task<Result> DeleteEvent(int eventId)
    {
        return await _eventRepository.DeleteEvent(eventId);
    }

    public async Task<Result<Models.Event>> GetEvent(int eventId)
        => await _eventRepository.GetEvent(eventId); 
}