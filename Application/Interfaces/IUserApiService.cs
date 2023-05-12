using Application.Models;
using System.Text.Json;

namespace Application.Interfaces
{
    public interface IUserApiService
    {
        Task<bool> ValidateUser(string urluser, string token);
        Task<int> ValidateUserToken(string token);
        Task<IEnumerable<GenderResponse>> GetAllGenders(string urluser);
        Task<GenderResponse> GetGenderById(string urluser, int genderId);
        Task<bool> ValidateGender(string urluser, int genderId);
        string GetMessage();
        JsonDocument GetResponse();
        int GetStatusCode();
    }
}
