using Application.Models;

namespace Application.Interfaces
{
    public interface IOverallPreferenceService
    {
        Task<IEnumerable<OverallPreferenceResponse>> GetAll();
        Task<OverallPreferenceResponse> GetByUserId(Guid userId);
        Task<OverallPreferenceResponse> Insert(OverallPreferenceReq request);
        Task<OverallPreferenceResponse> Update(OverallPreferenceReq request);
        Task<OverallPreferenceResponse> Delete(Guid userId);
    }
}
