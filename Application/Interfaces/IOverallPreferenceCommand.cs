using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOverallPreferenceCommand
    {
        Task Insert(OverallPreference request);
        Task Update(OverallPreference request);
        Task Delete(OverallPreference request);
    }
}
