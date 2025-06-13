namespace ET_Frontend.Models.AccountManagement;

/// <summary>
/// Stellt eine Benutzer-Mitgliedschaft in einer Organisation dar – wird im Frontend angezeigt.
/// </summary>
public class MembershipViewModel
{
    /// <summary>
    /// Die eindeutige ID des Accounts.
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    /// Der Name der Organisation, zu der die Mitgliedschaft gehört.
    /// </summary>
    public string OrganisationName { get; set; } = "";

    /// <summary>
    /// Die E-Mail-Adresse, die mit dem Account verknüpft ist.
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// Die eindeutige ID der Organisation.
    /// </summary>
    public int OrganisationId { get; set; }
}
