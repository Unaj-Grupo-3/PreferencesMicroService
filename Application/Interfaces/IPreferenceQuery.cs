using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPreferenceQuery
    {
        Task<IEnumerable<Preference>> GetAll();
        Task<Preference> GetById(int UserId, int InterestId);
        Task<IEnumerable<Preference>> GetAllByUserId(int UserId);
    }
}
