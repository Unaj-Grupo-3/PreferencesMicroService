using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGenderPreferenceQuery
    {
        Task<IEnumerable<GenderPreference>> GetAllByUserId(int userId);
        Task<GenderPreference> GetById(int userId, int genderId);
    }
}
