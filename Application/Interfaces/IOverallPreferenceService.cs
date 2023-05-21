using Application.Models;

namespace Application.Interfaces
{
    public interface IOverallPreferenceService
    {
        Task<IEnumerable<OverallPreferenceResponse>> GetAll();
        Task<IEnumerable<OverallPreferenceResponse>> GetByListId(List<int> userIds);

        Task<OverallPreferenceResponse> GetByUserId(int userId);
        Task<OverallPreferenceResponse> Insert(OverallPreferenceReq request, int userId);
        Task<OverallPreferenceResponse> Update(OverallPreferenceReq request, int userId);
        Task<OverallPreferenceResponse> Delete(int userId);
    }
}
