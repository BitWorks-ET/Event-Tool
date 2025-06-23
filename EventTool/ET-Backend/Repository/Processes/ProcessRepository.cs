using System.Data;
using Dapper;
using ET_Backend.Models;
using FluentResults;

namespace ET_Backend.Repository.Processes;

/// <summary>Dapper-Zugriffsschicht für Prozesse + Prozessschritte (event-basiert).</summary>
public class ProcessRepository : IProcessRepository
{
    private readonly IDbConnection _db;
    public ProcessRepository(IDbConnection db) => _db = db;

    // ─────────────────────── Lesen ───────────────────────
    public async Task<Result<Process>> GetByEvent(int eventId)
    {
        var proc = await _db.QuerySingleOrDefaultAsync<Process>(
            "SELECT Id, EventId FROM Processes WHERE EventId = @Evt;",
            new { Evt = eventId });

        if (proc == null)
            return Result.Fail("NotFound");

        proc.ProcessSteps = (await _db.QueryAsync<ProcessStep>(
                "SELECT * FROM ProcessSteps WHERE ProcessId = @Pid;",
                new { Pid = proc.Id }))
            .ToList();

        return Result.Ok(proc);
    }

    /* ------------------------------------------------------------
   CREATE  – wird genutzt, wenn es für das Event noch
             keinen Prozess gibt.
------------------------------------------------------------ */
    public Task<Result<Process>> CreateAsync(int eventId, Process proc)
    {
        proc.EventId = eventId;
        proc.Id = 0; // erzwingt INSERT
        return UpsertAsync(proc);
    }

/* ------------------------------------------------------------
   UPDATE  – wird genutzt, wenn bereits ein Prozess existiert.
------------------------------------------------------------ */
    public Task<Result<Process>> UpdateAsync(int eventId, Process proc)
    {
        proc.EventId = eventId; // Sicherheit
        return UpsertAsync(proc);
    }

/* ------------------------------------------------------------
   interne Up-/Insert-Methode
------------------------------------------------------------ */
    private async Task<Result<Process>> UpsertAsync(Process proc)
    {
        using var tx = _db.BeginTransaction();
        try
        {
            /* ---------- 1) Prozesskopf ---------- */
            if (proc.Id == 0)
            {
                proc.Id = await _db.InsertAndGetIdAsync(
                    "INSERT INTO Processes (EventId) VALUES (@Evt);",
                    new { Evt = proc.EventId }, tx);
            }
            else
            {
                // hier evtl. weitere Kopf-Infos updaten …
            }

            /* ---------- 2) Steps neu schreiben --- */
            await _db.ExecuteAsync(
                "DELETE FROM ProcessSteps WHERE ProcessId = @Pid;",
                new { Pid = proc.Id }, tx);

            var idMap = new Dictionary<int, int>(); // alt ➜ neu

            foreach (var s in proc.ProcessSteps)
            {
                int newId = await _db.InsertAndGetIdAsync(@"
                INSERT INTO ProcessSteps
                      (Name, [Trigger], [Action], [Offset],
                       TriggeredByStepId, Subject, Body, ProcessId)
                VALUES (@Name, @Trig, @Act, @Off,
                       NULL, @Subj, @Body, @Pid);",
                    new
                    {
                        s.Name,
                        Trig = (int)s.Trigger,
                        Act = (int)s.Action,
                        Off = s.Offset,
                        Subj = s.Subject,
                        Body = s.Body,
                        Pid = proc.Id
                    }, tx);

                idMap[s.Id] = newId;
                s.Id = newId; // zurück an Aufrufer
            }

            /* ---------- 3) Verkettungen nachziehen */
            foreach (var s in proc.ProcessSteps.Where(p => p.TriggeredByStepId.HasValue))
            {
                if (idMap.TryGetValue(s.TriggeredByStepId!.Value, out int newParent))
                {
                    await _db.ExecuteAsync(
                        "UPDATE ProcessSteps SET TriggeredByStepId = @Parent WHERE Id = @Id;",
                        new { Parent = newParent, Id = s.Id }, tx);
                }
            }

            tx.Commit();
            return Result.Ok(proc);
        }
        catch (Exception ex)
        {
            tx.Rollback();
            return Result.Fail(ex.Message);
        }
    }
}