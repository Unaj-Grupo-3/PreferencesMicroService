using Application.Models;

namespace Application.Interfaces
{
    public interface IPreferenceService
    {
        Task<PreferenceResponse> Insert(PreferenceReq request);
        Task<IEnumerable<PreferenceResponse>> GetAll();
        Task<IEnumerable<PreferenceResponse>> GetAllByUserId(Guid userId);
        Task<PreferenceResponse> GetByid(Guid userId, int InterestId);
        Task<PreferenceResponse> Update(PreferenceReq request);
        Task<PreferenceResponse> Delete(PreferenceReqId request);
    }
}
