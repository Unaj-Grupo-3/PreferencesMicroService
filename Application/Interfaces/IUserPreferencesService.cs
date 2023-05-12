using Application.Models;

namespace Application.Interfaces
{
    public interface IUserPreferencesService
    {
        Task<IEnumerable<UserPreferencesResponse>> GetAll(string urluser);
        Task<UserPreferencesResponse> GetByUserId(string urluser, int UserId);
        Task<IEnumerable<UserPreferencesResponseFull>> GetFullByListId(string urluser, List<int> userIds);
        Task<IEnumerable<UserPreferencesResponse>> GetSimpleByListId(string urluser, List<int> userIds);
    }
}
