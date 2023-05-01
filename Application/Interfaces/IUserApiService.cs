using Application.Models;
using System.Text.Json;

namespace Application.Interfaces
{
    public interface IUserApiService
    {
        Task<bool> ValidateUser(int UserId);
        Task<int> ValidateUserToken(string token);
        Task<IEnumerable<GenderResponse>> GetAllGenders();
        Task<GenderResponse> GetGenderById(int genderId);
        Task<bool> ValidateGender(int genderId);
        string GetMessage();
        JsonDocument GetResponse();
        int GetStatusCode();
    }
}
