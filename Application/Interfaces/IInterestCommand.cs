using Domain.Entities;

namespace Application.Interfaces
{
    public interface IInterestCommand
    {
        Task Insert(Interest request);
        Task Update(Interest request);
        Task Delete(Interest request);
    }
}
