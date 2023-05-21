using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGenderPreferenceCommand
    {
        Task Insert(GenderPreference request);
        Task Delete(GenderPreference request);
    }
}
