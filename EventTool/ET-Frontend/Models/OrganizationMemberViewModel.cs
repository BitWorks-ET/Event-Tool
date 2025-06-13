namespace ET_Frontend.Models;

/// <summary>
/// ViewModel zur Darstellung eines Organisationsmitglieds im Frontend.
/// </summary>
public class OrganizationMemberViewModel
{
    /// <summary>
    /// Die E-Mail-Adresse des Mitglieds.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Der Nachname des Mitglieds.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Die Rolle des Mitglieds innerhalb der Organisation.
    /// Als <c>int</c> gespeichert – kann auf ein <c>enum</c> gemappt werden.
    /// </summary>
    public int Role { get; set; }

    /// <summary>
    /// Parameterloser Konstruktor für das Model Binding.
    /// </summary>
    public OrganizationMemberViewModel() { }

    /// <summary>
    /// Erstellt ein neues <see cref="OrganizationMemberViewModel"/> mit E-Mail, Nachname und Rolle.
    /// </summary>
    /// <param name="email">Die E-Mail-Adresse.</param>
    /// <param name="lastName">Der Nachname.</param>
    /// <param name="role">Die Rolle als Ganzzahl.</param>
    public OrganizationMemberViewModel(string email, string lastName, int role)
    {
        Email = email;
        LastName = lastName;
        Role = role;
    }
}
