using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOverallPreferenceQuery
    {
        Task<IEnumerable<OverallPreference>> GetAll();
        Task<OverallPreference> GetByUserId(Guid userId);
    }
}
