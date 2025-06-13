using ET_Frontend.Models.AccountManagement;
using ET.Shared.DTOs;

namespace ET_Frontend.Mapping;
/// <summary>
/// Stellt eine statische Hilfsklasse zum Konvertieren von <see cref="MembershipDto"/>
/// in <see cref="MembershipViewModel"/> bereit.
/// </summary>
public static class DtoMembershipMapper
{
    /// <summary>
    /// Konvertiert ein <see cref="MembershipDto"/> in ein <see cref="MembershipViewModel"/>.
    /// </summary>
    /// <param name="dto">Das DTO mit Mitgliedschaftsdaten.</param>
    /// <returns>Ein neues <see cref="MembershipViewModel"/> basierend auf dem DTO.</returns>
    public static MembershipViewModel FromDto(MembershipDto dto) => new()
    {
        AccountId        = dto.AccountId,
        OrganisationName = dto.OrganisationName,
        Email            = dto.Email,
        OrganisationId   = dto.OrganisationId
    };
}