using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPreferenceCommand
    {
        Task Insert(Preference request);
        Task Update(Preference request);
    }
}
