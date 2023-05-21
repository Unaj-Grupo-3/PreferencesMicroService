using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCases
{
    public class InterestService : IInterestService
    {
        private readonly IInterestQuery _query;
        private readonly IInterestCategoryQuery _categoryQuery;
        private readonly IInterestCommand _command;

        public InterestService(IInterestQuery query, IInterestCommand command, IInterestCategoryQuery categoryQuery)
        {
            _query = query;
            _command = command;
            _categoryQuery = categoryQuery;
        }
        //public async Task<IEnumerable<InterestResponse>> GetAll()
        //{
        //    List<InterestResponse> interestResponses = new List<InterestResponse>();
        //    var lista = await _query.GetAll();

        //    foreach (var item in lista)
        //    {
        //        InterestResponse interes = new InterestResponse
        //        {
        //            Id = item.InterestId,
        //            Description = item.Description,
        //            InterestCategory = new InterestCategoryResponse 
        //            { 
        //                Id = item.InterestCategory.InterestCategoryId, 
        //                Description = item.InterestCategory.Description 
        //            }
        //        };
        //        interestResponses.Add(interes);
        //    }

        //    return interestResponses;
        //}
        public async Task<IEnumerable<InterestCategoryResponse_1>> GetAll()
        {
            var interestCategories = await _categoryQuery.GetAll();

            List<InterestCategoryResponse_1> interestCategoryResponses = new List<InterestCategoryResponse_1>();

            foreach (var category in interestCategories)
            {
                var interests = await _query.GetAllByCategory(category.InterestCategoryId);

                var interestResponses = interests.Select(interest => new InterestResponse_1
                {
                    Id = interest.InterestId,
                    Description = interest.Description
                }).ToList();

                InterestCategoryResponse_1 interes = new InterestCategoryResponse_1
                {
                    Id = category.InterestCategoryId,
                    Description = category.Description,
                    Interes = interestResponses
                };

                interestCategoryResponses.Add(interes);
            }
            return interestCategoryResponses;
        }
        public async Task<InterestCategoryResponse_1> GetByIdCategory(int id)
        {
            var interestCategory = await _categoryQuery.GetById(id);

            var interests = await _query.GetAllByCategory(interestCategory.InterestCategoryId);

            var interestResponses = interests.Select(interest => new InterestResponse_1
            {
                Id = interest.InterestId,
                Description = interest.Description
            }).ToList();

            InterestCategoryResponse_1 interesCategoria = new InterestCategoryResponse_1
            {
                Id = interestCategory.InterestCategoryId,
                Description = interestCategory.Description,
                Interes = interestResponses
            };
            return interesCategoria;
        }
        //no tiene uso
        public async Task<IEnumerable<InterestResponse>> GetAllByCategory(int interestCategoryId)
        {
            List<InterestResponse> interestResponses = new List<InterestResponse>();
            var category = await _categoryQuery.GetById(interestCategoryId);

            if (category != null)
            {
                var lista = await _query.GetAllByCategory(interestCategoryId);

                foreach (var item in lista)
                {
                    InterestResponse interes = new InterestResponse
                    {
                        Id = item.InterestId,
                        Description = item.Description,
                        InterestCategory = new InterestCategoryResponse { Id = item.InterestCategory.InterestCategoryId, Description = item.InterestCategory.Description }
                    };
                    interestResponses.Add(interes);
                }

                return interestResponses;
            }

            return interestResponses;
        }

        public async Task<InterestResponse> Insert(InterestReq request)
        {
            var category = await _categoryQuery.GetById(request.InterestCategoryId);

            if (category != null)
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
                    InterestCategory = new InterestCategoryResponse { Id = category.InterestCategoryId, Description = category.Description }
                };

                return response;
            }

            return null;
        }

        public async Task<InterestResponse> Update(InterestReq request, int id)
        {
            var category = await _categoryQuery.GetById(request.InterestCategoryId);

            if (category != null)
            {
                Interest interest = await _query.GetById(id);

                if (interest != null)
                {
                    interest.Description = request.Description;
                    interest.InterestCategoryId = request.InterestCategoryId;

                    await _command.Update(interest);

                    InterestResponse response = new InterestResponse
                    {
                        Id = interest.InterestId,
                        Description = interest.Description,
                        InterestCategory = new InterestCategoryResponse { Id = category.InterestCategoryId, Description = category.Description }
                    };

                    return response;
                }

                return null;
            }

            return null;
        }

        public async Task<InterestResponse> Delete(int id)
        {
            try
            {
                var interestResponse = await _query.GetById(id);

                if (interestResponse != null)
                {
                    await _command.Delete(interestResponse);

                    var category = await _categoryQuery.GetById(interestResponse.InterestCategoryId);

                    InterestResponse response = new InterestResponse
                    {
                        Id = interestResponse.InterestId,
                        Description = interestResponse.Description,
                        InterestCategory = new InterestCategoryResponse { Id = category.InterestCategoryId, Description = category.Description }
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

        public async Task<InterestResponse> GetById(int id)
        {
            Interest interest = await _query.GetById(id);

            if (interest != null)
            {
                InterestResponse response = new InterestResponse
                {
                    Id = interest.InterestId,
                    Description = interest.Description,
                    InterestCategory = new InterestCategoryResponse { Id = interest.InterestCategoryId, Description = interest.InterestCategory.Description }
                };
                return response;
            }
            return null;
        }
    }
}
