using Application.Models;

namespace Application.Interfaces
{
    public interface IInterestService
    {
        Task<InterestResponse> Insert(InterestReq request);
        Task<IEnumerable<InterestResponse>> GetAll();
    }
}
