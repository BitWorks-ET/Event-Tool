namespace ET.Shared.DTOs;

/// <summary>
/// Repräsentiert die Registrierungsdaten eines neuen Benutzers.
/// </summary>
/// <param name="FirstName">Vorname des Benutzers.</param>
/// <param name="LastName">Nachname des Benutzers.</param>
/// <param name="EMail">E-Mail-Adresse des Benutzers.</param>
/// <param name="Password">Passwort für den Account.</param>
public record RegisterDto(string FirstName, string LastName, string EMail, string Password);