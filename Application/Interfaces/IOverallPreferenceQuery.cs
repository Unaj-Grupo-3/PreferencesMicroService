using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOverallPreferenceQuery
    {
        Task<IEnumerable<OverallPreference>> GetAll();
        Task<IEnumerable<OverallPreference>> GetByListId(List<int>userIds);
        Task<OverallPreference> GetByUserId(int UserId);
    }
}
