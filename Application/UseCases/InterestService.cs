using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCases
{
    public class InterestService: IInterestService
    {
        private readonly IInterestQuery _query;
        private readonly IInterestCommand _command;

        public InterestService(IInterestQuery query, IInterestCommand command)
        {
            _query = query;
            _command = command;
        }

        public async Task<IEnumerable<InterestResponse>> GetAll()
        {
            List<InterestResponse> interestResponses = new List<InterestResponse>();
            var lista = await _query.GetAll();

            foreach (var item in lista)
            {
                InterestResponse interes = new InterestResponse
                {
                    Id = item.InterestId,
                    Description = item.Description,
                    InterestCategory = new InterestCategoryResponse { Id = item.InterestCategory.InterestCategoryId , Description = item.InterestCategory.Description }
                };
                interestResponses.Add(interes);
            }

            return interestResponses;
        }

        public async Task<InterestResponse> Insert(InterestReq request)
        {
            Interest interest = new Interest
            {
                Description = request.Description,
                InterestCategoryId = request.InterestCategoryId
            };

            await _command.Insert(interest);

            InterestResponse response = new InterestResponse
            {
                Id = interest.InterestId,
                Description = interest.Description,
                InterestCategory = new InterestCategoryResponse { Id = interest.InterestCategoryId, Description = "" }
            };

            return response;
        }

        public async Task<InterestResponse> GetById(int userId)
        {
            Interest interest = await _query.GetById(userId);

            if (interest != null)
            {
                InterestResponse response = new InterestResponse
                {
                    Id = interest.InterestId,
                    Description = interest.Description,
                    InterestCategory = new InterestCategoryResponse { Id = interest.InterestCategoryId, Description = "" }
                };

                return response;
            }

            return null;

        }
    }
}
