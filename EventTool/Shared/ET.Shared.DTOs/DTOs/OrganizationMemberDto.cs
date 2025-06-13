using System.Text.Json.Serialization;

namespace ET.Shared.DTOs;

/// <summary>
/// Repräsentiert ein Organisationsmitglied mit Rolle, Nachname und E-Mail.
/// </summary>
public class OrganizationMemberDto
{
    /// <summary>
    /// Die E-Mail-Adresse des Mitglieds.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Der Nachname des Mitglieds.
    /// </summary>
    public string Lastname { get; set; }
    /// <summary>
    /// Die Rolle des Mitglieds in der Organisation (numerischer Wert).
    /// </summary>
    public int Role { get; set; }

    // Parameterloser Konstruktor für System.Text.Json
    /// <summary>
    /// Parameterloser Konstruktor für JSON-Deserialisierung.
    /// </summary>
    public OrganizationMemberDto() { }

    // Optional: Hauptkonstruktor
    /// <summary>
    /// Konstruktor zur Initialisierung aller Eigenschaften.
    /// </summary>
    /// <param name="email">E-Mail des Mitglieds.</param>
    /// <param name="lastname">Nachname des Mitglieds.</param>
    /// <param name="role">Rolle des Mitglieds.</param>
    [JsonConstructor]
    public OrganizationMemberDto(string email, string lastname, int role)
    {
        Email = email;
        Lastname = lastname;
        Role = role;
    }
}