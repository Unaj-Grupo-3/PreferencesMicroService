using Application.Models;

namespace Application.Interfaces
{
    public interface IPreferenceService
    {
        Task<PreferenceResponse> Insert(PreferenceReq request, int userId);
        Task<IEnumerable<PreferenceResponse>> GetAll();
        Task<IEnumerable<PreferenceResponse>> GetAllByUserId(int userId);
        Task<PreferenceResponse> GetByid(int userId, int InterestId);
        Task<PreferenceResponse> Update(PreferenceReq request, int userId);
        Task<PreferenceResponse> Delete(PreferenceReqId request, int userId);
    }
}
