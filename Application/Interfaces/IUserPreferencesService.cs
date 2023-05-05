using Application.Models;

namespace Application.Interfaces
{
    public interface IUserPreferencesService
    {
        Task<UserPreferencesResponse> GetByUserId(int UserId);
    }
}
