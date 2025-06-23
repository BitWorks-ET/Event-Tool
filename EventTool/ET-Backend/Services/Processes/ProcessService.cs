using ET.Shared.DTOs;
using ET_Backend.Models;
using ET_Backend.Repository.Processes;
using ET_Backend.Services.Mapping;
using FluentResults;

namespace ET_Backend.Services.Processes;

/// <summary>
/// Fachlogik rund um Event-gebundene Prozesse.
/// Alle Datenbankzugriffe laufen über <see cref="IProcessRepository"/>.
/// </summary>
public class ProcessService : IProcessService
{
    private readonly IProcessRepository _repo;

    public ProcessService(IProcessRepository repo) => _repo = repo;

    /* ------------------------------------------------------------
       GET  – Prozess zu einem Event abrufen
    ------------------------------------------------------------ */
    public async Task<ProcessDto?> GetForEvent(int eventId)
    {
        var dbRes = await _repo.GetByEvent(eventId);
        return dbRes.IsSuccess
            ? ProcessMapper.ToDto(dbRes.Value)
            : null;                               // NotFound → null
    }

    /* ------------------------------------------------------------
       POST – neuen Prozess anlegen
    ------------------------------------------------------------ */
    public async Task<Result<ProcessDto>> CreateForEvent(int eventId, ProcessDto dto)
    {
        var model  = ProcessMapper.ToModel(dto);
        var dbRes  = await _repo.CreateAsync(eventId, model);

        return dbRes.IsSuccess
            ? Result.Ok(ProcessMapper.ToDto(dbRes.Value))
            : Result.Fail(dbRes.Errors);
    }

    /* ------------------------------------------------------------
       PUT  – bestehenden Prozess aktualisieren
    ------------------------------------------------------------ */
    public async Task<Result> UpdateForEvent(int eventId, ProcessDto dto)
    {
        var model = ProcessMapper.ToModel(dto);
        var dbRes = await _repo.UpdateAsync(eventId, model);

        return dbRes.IsSuccess
            ? Result.Ok()
            : Result.Fail(dbRes.Errors);
    }
}