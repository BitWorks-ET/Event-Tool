using ET.Shared.DTOs;
using ET_Frontend.Models.Event;

namespace ET_Frontend.Mapping;

/// <summary>
/// Stellt Methoden bereit, um <see cref="EventListDto"/> in <see cref="EventViewModel"/> zu konvertieren.
/// </summary>
public static class EventListViewMapper
{
    /// <summary>
    /// Konvertiert ein <see cref="EventListDto"/> in ein <see cref="EventViewModel"/>.
    /// </summary>
    /// <param name="dto">Das DTO mit Event-Informationen.</param>
    /// <returns>Ein neues <see cref="EventViewModel"/> mit den Daten aus dem DTO.</returns>
    public static EventViewModel ToViewModel(EventListDto dto)
    {
        return new EventViewModel
        {
            EventId = dto.EventId,
            Name = dto.Name,
            Description = dto.Description,
            Participants = dto.Participants,
            MaxParticipants = dto.MaxParticipants,
            IsOrganizer = dto.IsOrganizer,
            IsSubscribed = dto.IsSubscribed
        };
    }
}