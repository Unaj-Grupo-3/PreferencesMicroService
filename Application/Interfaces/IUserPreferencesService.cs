using Application.Models;

namespace Application.Interfaces
{
    public interface IUserPreferencesService
    {
        Task<UserPreferencesResponse> GetByUserId(string urluser, int UserId);
    }
}
