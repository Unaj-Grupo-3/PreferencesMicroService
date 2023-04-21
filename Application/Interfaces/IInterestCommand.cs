using Domain.Entities;

namespace Application.Interfaces
{
    public interface IInterestCommand
    {
        Task Insert(Interest request);
    }
}
