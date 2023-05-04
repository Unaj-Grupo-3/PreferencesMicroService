using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPreferenceQuery
    {
        Task<IEnumerable<Preference>> GetAll();
        Task<Preference> GetById(Guid userId, int InterestId);
        Task<IEnumerable<Preference>> GetAllByUserId(Guid userId);
    }
}
