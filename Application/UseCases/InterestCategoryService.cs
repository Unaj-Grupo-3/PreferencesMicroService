using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCases
{
    public class InterestCategoryService : IInterestCategoryService
    {
        private readonly IInterestCategoryQuery _query;
        private readonly IInterestCategoryCommand _command;

        public InterestCategoryService(IInterestCategoryQuery query, IInterestCategoryCommand command)
        {
            _query = query;
            _command = command;
        }

        public async Task<IEnumerable<InterestCategoryResponse>> GetAll()
        {
            List<InterestCategoryResponse> interestCategoryResponses = new List<InterestCategoryResponse>();
            var lista = await _query.GetAll();

            foreach (var item in lista)
            {
                InterestCategoryResponse interes = new InterestCategoryResponse
                {
                    Id = item.InterestCategoryId,
                    Description = item.Description
                };
                interestCategoryResponses.Add(interes);
            }

            return interestCategoryResponses;
        }

        public async Task<InterestCategoryResponse> Insert(InterestCategoryReq request)
        {
            InterestCategory interestCategory = new InterestCategory
            {
                Description = request.Description
            };

            await _command.Insert(interestCategory);

            InterestCategoryResponse response = new InterestCategoryResponse
            {
                Id = interestCategory.InterestCategoryId,
                Description = interestCategory.Description
            };

            return response;
        }
    }
}
