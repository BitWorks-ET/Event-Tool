using System.Security.Claims;
using ET_Backend.Services.Helper.Authentication;
using Microsoft.AspNetCore.Mvc;
using ET.Shared.DTOs;
using ET_Backend.Services.Person;
using FluentResults;
using Microsoft.AspNetCore.Authorization;

namespace ET_Backend.Controllers;
/// <summary>
/// Stellt API-Endpunkte zur Benutzerverwaltung bereit.
/// </summary>
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthenticateService _authenticateService;
    /// <summary>
    /// Konstruktor für <see cref="UserController"/>.
    /// </summary>
    public UserController(IUserService userService, IAuthenticateService authenticateService)
    {
        _userService = userService;
        _authenticateService = authenticateService;
    }

    /// <summary>
    /// Aktualisiert Benutzerdaten.
    /// </summary>
    /// <param name="dto">Benutzerdaten zur Aktualisierung.</param>
    /// <returns>200 OK bei Erfolg, sonst 400 BadRequest.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto dto)
    {
        var result = await _userService.UpdateUserAsync(dto);

        if (result.IsSuccess)
            return Ok();

        return BadRequest(result.Errors);
    }
    /// <summary>
    /// Ruft einen Benutzer anhand der ID ab.
    /// </summary>
    /// <param name="id">Benutzer-ID.</param>
    /// <returns>Benutzerinformationen oder 404 NotFound.</returns>
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var result = await _userService.GetUserAsync(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }
    /// <summary>
    /// Gibt alle Mitgliedschaften eines Benutzers zurück.
    /// </summary>
    /// <param name="id">Benutzer-ID.</param>
    /// <returns>Liste von <see cref="MembershipDto"/> Objekten.</returns>
    [HttpGet("{id:int}/memberships")]
    [Authorize]
    public async Task<ActionResult<List<MembershipDto>>> GetMemberships(int id)
    {
        var result = await _userService.GetMembershipsAsync(id);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
    /// <summary>
    /// Aktualisiert die E-Mail-Adresse eines Benutzerkontos.
    /// </summary>
    /// <param name="accountId">ID des Benutzerkontos.</param>
    /// <param name="newEmail">Neue E-Mail-Adresse.</param>
    /// <returns>200 OK bei Erfolg, sonst 400 BadRequest.</returns>
    [HttpPut("memberships/{accountId:int}/email")]
    [Authorize]
    public async Task<IActionResult> UpdateEmail(int accountId, [FromBody] string newEmail)
    {
        var res = await _userService.UpdateEmailAsync(accountId, newEmail);
        return res.IsSuccess ? Ok() : BadRequest(res.Errors);
    }
    /// <summary>
    /// Löscht eine Mitgliedschaft eines Benutzers in einer Organisation.
    /// </summary>
    /// <param name="accountId">Konto-ID des Benutzers.</param>
    /// <param name="orgId">ID der Organisation.</param>
    /// <returns>200 OK bei Erfolg, sonst 400 BadRequest.</returns>
    [HttpDelete("memberships/{accountId:int}/{orgId:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteMembership(int accountId, int orgId)
    {
        var res = await _userService.DeleteMembershipAsync(accountId, orgId);
        return res.IsSuccess ? Ok() : BadRequest(res.Errors);
    }
    /// <summary>
    /// Fügt dem aktuellen Benutzer eine neue Mitgliedschaft hinzu.
    /// </summary>
    /// <param name="email">E-Mail des Zielbenutzers.</param>
    /// <returns>200 OK bei Erfolg, sonst 400 BadRequest.</returns>
    [HttpPost("memberships/add")]
    [Authorize]
    public async Task<IActionResult> AddMembership([FromBody] string email)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _authenticateService.AddMembership(userId, email);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
    /// <summary>
    /// Löscht den aktuell angemeldeten Benutzer.
    /// </summary>
    /// <returns>204 NoContent bei Erfolg, sonst 400 BadRequest.</returns>
    [HttpDelete]                 
    [Authorize]  
    public async Task<IActionResult> DeleteCurrent()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var result = await _userService.DeleteUserAsync(userId);
        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }
}