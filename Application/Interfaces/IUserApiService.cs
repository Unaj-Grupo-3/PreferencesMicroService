using Application.Models;
using System.Text.Json;

namespace Application.Interfaces
{
    public interface IUserApiService
    {
        Task<bool> ValidateUser(Guid userId);
        Task<Guid> ValidateUserToken(string token);
        Task<IEnumerable<GenderResponse>> GetAllGenders();
        Task<GenderResponse> GetGenderById(int genderId);
        Task<bool> ValidateGender(int genderId);
        string GetMessage();
        JsonDocument GetResponse();
        int GetStatusCode();
    }
}
