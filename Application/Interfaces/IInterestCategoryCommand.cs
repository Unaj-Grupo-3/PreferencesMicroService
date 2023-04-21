using Domain.Entities;

namespace Application.Interfaces
{
    public interface IInterestCategoryCommand
    {
        Task Insert(InterestCategory request);
    }
}
