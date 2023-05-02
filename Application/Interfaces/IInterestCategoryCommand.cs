using Domain.Entities;

namespace Application.Interfaces
{
    public interface IInterestCategoryCommand
    {
        Task Insert(InterestCategory request);
        Task Update(InterestCategory request);
        Task Delete(InterestCategory request);
    }
}
