using ET.Shared.DTOs.Enums;
using ET.Shared.DTOs.Validation;
using System.Text.Json.Serialization;

namespace ET.Shared.DTOs;

public record EventDto(
    int Id,
    string Name,
    string EventType,
    string Description,
    string Location,
    List<EventParticipantDto> Participants,
    [param: MinElements(1, 
        ErrorMessage = "Mindestens ein Verwalter muss ausgewählt sein.")]
    List<string> Organizers,
    List<string> ContactPersons,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    int? ProcessId,
    DateOnly StartDate,
    DateOnly EndDate,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int MinParticipants,
    int MaxParticipants,
    DateOnly RegistrationStart,
    DateOnly RegistrationEnd,
    EventStatus Status,
    bool IsBlueprint
);