using ET_Backend.Models;
using ET.Shared.DTOs;

namespace ET_Backend.Services.Mapping;

public static class EventMapper
{
    /* -----------------------------------------------------------
       dto  ➜  Domain-Model
       ----------------------------------------------------------- */
    public static Models.Event ToModel(
        EventDto            dto,
        Models.Organization org,
        Process?            process         = null,
        List<Account>?      organizers      = null,
        List<Account>?      contactPersons  = null,
        List<Account>?      participants    = null)
    {
        if (process == null && dto.ProcessId is > 0)
            process = new Process { Id = dto.ProcessId.Value };

        return new Models.Event
        {
            Name              = dto.Name,
            EventType         = dto.EventType,
            Description       = dto.Description,
            Location          = dto.Location,

            Organizers        = organizers     ?? new(),
            ContactPersons    = contactPersons ?? new(),
            Participants      = participants   ?? new(),

            Organization      = org,
            Process           = process,

            StartDate         = dto.StartDate,
            EndDate           = dto.EndDate,
            StartTime         = dto.StartTime,
            EndTime           = dto.EndTime,

            MinParticipants   = dto.MinParticipants,
            MaxParticipants   = dto.MaxParticipants,

            RegistrationStart = dto.RegistrationStart,
            RegistrationEnd   = dto.RegistrationEnd,

            Status            = dto.Status,
            IsBlueprint       = dto.IsBlueprint
        };
    }

    /* -----------------------------------------------------------
       Domain-Model  ➜  dto
       ----------------------------------------------------------- */
    public static EventDto ToDto(Models.Event e)
    {
        return new EventDto(
            e.Id,
            e.Name,
            e.EventType,
            e.Description,
            e.Location,

            e.Participants
             .Select(p => new EventParticipantDto(
                 p.Id,
                 p.User?.Firstname ?? string.Empty,
                 p.User?.Lastname  ?? string.Empty,
                 p.EMail))
             .ToList(),

            e.Organizers
             .Select(o => o.EMail)
             .ToList(),

            e.ContactPersons
             .Select(c => c.EMail)
             .ToList(),

            e.Process?.Id,

            e.StartDate,
            e.EndDate,
            e.StartTime,
            e.EndTime,
            e.MinParticipants,
            e.MaxParticipants,
            e.RegistrationStart,
            e.RegistrationEnd,
            e.Status,
            e.IsBlueprint
        );
    }
}