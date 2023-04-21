using Application.Models;

namespace Application.Interfaces
{
    public interface IInterestCategoryService
    {
        Task<InterestCategoryResponse> Insert(InterestCategoryReq request);
        Task<IEnumerable<InterestCategoryResponse>> GetAll();
    }
}
