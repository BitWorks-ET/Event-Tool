using System.Net;
using System.Net.Http.Json;
using ET_Frontend.Mapping;
using ET_Frontend.Models;
using ET.Shared.DTOs;

namespace ET_Frontend.Services.ApiClients;

/// <summary>Client für die Prozess-Endpunkte.</summary>
public class ProcessApi : IProcessApi
{
    private readonly HttpClient _http;
    public ProcessApi(HttpClient http) => _http = http;

    private const string Base = "api/process";  

    /* --------------------------------------------------------
       GET  api/process/{eventId}
    -------------------------------------------------------- */
    public async Task<ProcessViewModel?> GetAsync(int eventId)
    {
        var resp = await _http.GetAsync($"{Base}/{eventId}");

        // 404 ⇒ kein Prozess angelegt ⇒ Frontend zeigt leere Liste
        if (resp.StatusCode == HttpStatusCode.NotFound)
            return null;

        resp.EnsureSuccessStatusCode();

        var dto = await resp.Content.ReadFromJsonAsync<ProcessDto>();
        return dto is null ? null : ProcessViewMapper.ToViewModel(dto);
    }

    /* --------------------------------------------------------
       PUT  api/process/{eventId}
    -------------------------------------------------------- */
    public async Task<bool> UpdateAsync(int eventId, ProcessViewModel vm)
    {
        var dto  = ProcessViewMapper.ToDto(vm);
        var resp = await _http.PutAsJsonAsync($"{Base}/{eventId}", dto);
        return resp.IsSuccessStatusCode;
    }
    
    /* --------------------------------------------------------
   POST  api/process/{eventId}
   Wird aufgerufen, wenn für das Event noch kein Prozess existiert.
-------------------------------------------------------- */
    public async Task<bool> CreateAsync(int eventId, ProcessViewModel vm)
    {
        var dto  = ProcessViewMapper.ToDto(vm);
        var resp = await _http.PostAsJsonAsync($"{Base}/{eventId}", dto);
        return resp.IsSuccessStatusCode;
    }
    
    public async Task<bool> CreateOrUpdateAsync(int eventId, ProcessViewModel vm)
    {
        var existing = await GetAsync(eventId);
        return existing is null
            ? await CreateAsync(eventId, vm)
            : await UpdateAsync(eventId, vm);
    }
}