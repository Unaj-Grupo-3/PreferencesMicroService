using Application.Models;

namespace Application.Interfaces
{
    public interface IGenderPreferenceService
    {
        Task<PreferenceResponse> Insert(PreferenceReq request);
        Task<IEnumerable<PreferenceResponse>> GetAllByUserId(int UserId);
    }
}
