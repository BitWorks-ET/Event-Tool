namespace ET.Shared.DTOs;
/// <summary>
/// Stellt eine kurze Übersicht zu einem Event dar, z. B. für Listenansichten.
/// </summary>
/// <param name="EventId">Eindeutige ID des Events.</param>
/// <param name="Name">Name des Events.</param>
/// <param name="Description">Kurze Beschreibung des Events.</param>
/// <param name="Participants">Aktuelle Anzahl der Teilnehmenden.</param>
/// <param name="MaxParticipants">Maximal mögliche Anzahl an Teilnehmenden.</param>
/// <param name="IsOrganizer">Gibt an, ob der aktuelle Benutzer Organisator ist.</param>
/// <param name="IsSubscribed">Gibt an, ob der Benutzer für dieses Event angemeldet ist.</param>
public record EventListDto(int EventId, String Name, String Description, int Participants, int MaxParticipants, bool IsOrganizer, bool IsSubscribed);