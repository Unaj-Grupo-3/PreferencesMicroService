using System.Text.Json;

namespace Application.Interfaces
{
    public interface IUserApiService
    {
        Task<bool> ValidateUser(int UserId);
        string GetMessage();
        JsonDocument GetResponse();
        int GetStatusCode();
    }
}
