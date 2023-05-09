using Application.Models;

namespace Application.Interfaces
{
    public interface IUserPreferencesService
    {
        Task<IEnumerable<UserPreferencesResponse>> GetAll(string urluser);
        Task<UserPreferencesResponse> GetByUserId(string urluser, int UserId);
    }
}
