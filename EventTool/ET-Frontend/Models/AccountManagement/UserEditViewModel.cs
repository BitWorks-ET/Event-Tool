using System.ComponentModel.DataAnnotations;

namespace ET_Frontend.Models.AccountManagement;

/// <summary>
/// ViewModel zur Bearbeitung von Benutzerdaten im Frontend.
/// Beinhaltet Vorname, Nachname und Passwort.
/// </summary>
public class UserEditViewModel
{
    /// <summary>
    /// Der Vorname des Benutzers.
    /// </summary>
    [Required(ErrorMessage = "Vorname ist erforderlich.")]
    public string FirstName { get; set; }

    /// <summary>
    /// Der Nachname des Benutzers.
    /// </summary>
    [Required(ErrorMessage = "Nachname ist erforderlich.")]
    public string LastName { get; set; }

    /// <summary>
    /// Das neue Passwort des Benutzers.
    /// </summary>
    [Required(ErrorMessage = "Passwort ist erforderlich.")]
    [MinLength(6, ErrorMessage = "Passwort muss mindestens 6 Zeichen lang sein.")]
    public string Password { get; set; }

    /// <summary>
    /// Das wiederholte Passwort zur Validierung.
    /// Muss mit <see cref="Password"/> übereinstimmen.
    /// </summary>
    [Required(ErrorMessage = "Wiederholtes Passwort ist erforderlich.")]
    [Compare(nameof(Password), ErrorMessage = "Passwörter stimmen nicht überein.")]
    public string Reppassword { get; set; }
}
