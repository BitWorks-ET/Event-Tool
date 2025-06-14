using ET_Backend.Models;
using FluentResults;

namespace ET_Backend.Repository.Processes;
/// <summary>
/// Implementierung für den Datenzugriff auf Prozesse.
/// </summary>
public class ProcessRepository : IProcessRepository
{
    public async Task<Result<Process>> GetProcess(int id)
    {
        return null;
    }
}