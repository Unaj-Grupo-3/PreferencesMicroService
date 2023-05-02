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

        public async Task<InterestCategoryResponse> Update(InterestCategoryReq request, int id)
        {            
            InterestCategory interestCategory = await _query.GetById(id);

            if (interestCategory != null)
            {
                interestCategory.InterestCategoryId = id;
                interestCategory.Description = request.Description;

                await _command.Update(interestCategory);

                InterestCategoryResponse response = new InterestCategoryResponse
                {
                    Id = interestCategory.InterestCategoryId,
                    Description = interestCategory.Description
                };

                return response;
            }

            return null;
        }

        public async Task<InterestCategoryResponse> Delete(int id)
        {
            try
            {
                var interestResponse = await _query.GetById(id);

                if (interestResponse != null)
                {
                    await _command.Delete(interestResponse);

                    InterestCategoryResponse response = new InterestCategoryResponse
                    {
                        Id = interestResponse.InterestCategoryId,
                        Description = interestResponse.Description
                    };

                    return response;
                }

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
