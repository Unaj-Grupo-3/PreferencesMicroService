using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IInterestService
    {
        Task<InterestResponse> Insert(InterestReq request);
        Task<IEnumerable<InterestResponse>> GetAll();
        Task<InterestResponse> GetById(int id);
    }
}
