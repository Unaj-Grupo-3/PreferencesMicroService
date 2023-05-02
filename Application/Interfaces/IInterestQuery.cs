using Domain.Entities;

namespace Application.Interfaces
{
    public interface IInterestQuery
    {
        Task<IEnumerable<Interest>> GetAll();
        Task<IEnumerable<Interest>> GetAllByCategory(int interestCategoryId);
        Task<Interest> GetById(int id);
    }
}
