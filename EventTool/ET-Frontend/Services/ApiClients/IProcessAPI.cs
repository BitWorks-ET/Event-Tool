using ET_Frontend.Models;


namespace ET_Frontend.Services.ApiClients
{
    public interface IProcessApi
    {
        Task<ProcessViewModel> GetAsync(int eventId);
        Task<bool> UpdateAsync(int eventId, ProcessViewModel vm);
        Task<bool> CreateAsync(int eventId, ProcessViewModel vm);
        Task<bool> CreateOrUpdateAsync(int eventId, ProcessViewModel vm);
    }
}