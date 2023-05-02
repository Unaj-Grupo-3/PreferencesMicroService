using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IInterestService
    {
        Task<InterestResponse> Insert(InterestReq request);
        Task<InterestResponse> Update(InterestReq request, int id);
        Task<IEnumerable<InterestResponse>> GetAll();
        Task<IEnumerable<InterestResponse>> GetAllByCategory(int interestCategoryId);
        Task<InterestResponse> GetById(int id);
        Task<InterestResponse> Delete(int id);
    }
}
