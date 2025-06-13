namespace ET.Shared.DTOs;
/// <summary>
/// Repräsentiert eine Mitgliedschaft eines Accounts in einer Organisation.
/// </summary>
/// <param name="AccountId">Die ID des Benutzerkontos.</param>
/// <param name="OrganisationId">Die ID der Organisation.</param>
/// <param name="OrganisationName">Der Name der Organisation.</param>
/// <param name="Email">Die E-Mail-Adresse des Mitglieds innerhalb der Organisation.</param>
public record MembershipDto(
    int    AccountId,
    int    OrganisationId,
    string OrganisationName,
    string Email);