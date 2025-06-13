namespace ET.Shared.DTOs;

/// <summary>
/// Repräsentiert die Benutzerdaten eines registrierten Nutzers.
/// </summary>
/// <param name="Id">Die eindeutige Benutzer-ID.</param>
/// <param name="FirstName">Vorname des Benutzers.</param>
/// <param name="LastName">Nachname des Benutzers.</param>
/// <param name="Password">Passwort des Benutzers (im DTO enthalten).</param>
public record UserDto(int Id, string FirstName, string LastName, string Password);