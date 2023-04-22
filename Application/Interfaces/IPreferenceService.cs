using Application.Models;

namespace Application.Interfaces
{
    public interface IPreferenceService
    {
        Task<PreferenceResponse> Insert(PreferenceReq request);
        Task<IEnumerable<PreferenceResponse>> GetAll();
    }
}
