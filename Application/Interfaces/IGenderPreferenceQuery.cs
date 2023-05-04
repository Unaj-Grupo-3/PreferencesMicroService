using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGenderPreferenceQuery
    {
        Task<IEnumerable<GenderPreference>> GetAllByUserId(Guid userId);
        Task<GenderPreference> GetById(Guid userId, int genderId);
    }
}
