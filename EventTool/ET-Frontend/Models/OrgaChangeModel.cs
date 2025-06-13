using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace ET_Frontend.Models;

/// <summary>
/// ViewModel zur Bearbeitung einer Organisation im Frontend.
/// </summary>
public class OrgaChangeModel
{
    /// <summary>
    /// Die ID der Organisation.
    /// </summary>
    public int OrganizationId { get; set; }

    /// <summary>
    /// Der Name der Organisation.
    /// </summary>
    [Required(ErrorMessage = "Organisationsname ist erforderlich.")]
    public string orgaName { get; set; }

    /// <summary>
    /// Die Beschreibung der Organisation.
    /// </summary>
    public string description { get; set; }

    /// <summary>
    /// Die Domain der Organisation (z. B. "example.org").
    /// </summary>
    [Required(ErrorMessage = "Domain ist erforderlich.")]
    public string domain { get; set; }

    /// <summary>
    /// Das hochgeladene Logo der Organisation als Datei.
    /// </summary>
    public IBrowserFile orgaPic { get; set; }

    /// <summary>
    /// Das Logo der Organisation als Base64-kodierter String.
    /// </summary>
    public string orgaPicBase64 { get; set; }
}
