using Application.Models;
using System.Text.Json;

namespace Application.Interfaces
{
    public interface IUserApiService
    {
        Task<bool> ValidateUser(int UserId);
        Task<IEnumerable<GenderResponse>> GetAllGenders();
        Task<bool> ValidateGender(int genderId);
        string GetMessage();
        JsonDocument GetResponse();
        int GetStatusCode();
    }
}
