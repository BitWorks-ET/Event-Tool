using ET.Shared.DTOs;
using ET_Backend.Services.Processes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ET_Backend.Controllers;

/// <summary>API-Endpunkte zum Abrufen und Speichern des Prozesses eines Events.</summary>
[ApiController]
[Route("api/process")]
public class ProcessController : ControllerBase
{
    private readonly IProcessService _svc;
    public ProcessController(IProcessService svc) => _svc = svc;

    [HttpGet("{eventId:int}")]
    [Authorize]
    public async Task<IActionResult> Get(int eventId)
        => Ok(await _svc.GetForEvent(eventId));

    [HttpPut("{eventId:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int eventId, [FromBody] ProcessDto dto)
    {
        var res = await _svc.UpdateForEvent(eventId, dto);

        return res.IsSuccess
            ? NoContent()
            : BadRequest(res.Errors);
    }
    
    [HttpPost("{eventId:int}")]
    [Authorize]
    public async Task<IActionResult> Create(int eventId, [FromBody] ProcessDto dto)
    {
        var res = await _svc.CreateForEvent(eventId, dto);

        return res.IsSuccess
            ? CreatedAtAction(nameof(Get), new { eventId }, res.Value)   // 201 + DTO zurück
            : BadRequest(res.Errors);
    }

}