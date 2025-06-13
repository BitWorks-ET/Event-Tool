namespace ET.Shared.DTOs;
/// <summary>
/// Repräsentiert die Anmeldedaten eines Benutzers.
/// </summary>
/// <param name="EMail">Die E-Mail-Adresse des Benutzers.</param>
/// <param name="Password">Das Passwort des Benutzers.</param>
public record LoginDto(string EMail, string Password);