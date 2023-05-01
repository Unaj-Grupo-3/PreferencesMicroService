using Domain.Entities;

namespace Application.Interfaces
{
    public interface IInterestCategoryQuery
    {
        Task<IEnumerable<InterestCategory>> GetAll();
        Task<InterestCategory> GetById(int categoryId);
    }
}
