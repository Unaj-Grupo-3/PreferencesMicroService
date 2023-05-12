using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCases
{
    public class PreferenceService: IPreferenceService
    {
        private readonly IPreferenceQuery _query;
        private readonly IPreferenceCommand _command;
        private readonly IInterestService _interestService;

        public PreferenceService(IPreferenceQuery query, IPreferenceCommand command, IInterestService interestService)
        {
            _query = query;
            _command = command;
            _interestService = interestService;
        }

        public async Task<IEnumerable<PreferenceResponse>> GetAll()
        {
            List<PreferenceResponse> listaResponse = new List<PreferenceResponse>();
            var lista = await _query.GetAll();

            if(lista.Any())
            {
                foreach (var item in lista)
                {
                    PreferenceResponse response = new PreferenceResponse
                    {
                        UserId = item.UserId,
                        Interest = new InterestResponse { Id = item.Interest.InterestId, Description = item.Interest.Description,
                            InterestCategory = new InterestCategoryResponse
                            {
                                Id = item.Interest.InterestCategory.InterestCategoryId,
                                Description = item.Interest.InterestCategory.Description
                            }
                        },
                        OwnInterest = item.OwnInterest,
                        Like = item.Like
                    };
                    listaResponse.Add(response);
                }
            }

            return listaResponse;
        }

        public async Task<IEnumerable<PreferenceResponse>> GetAllByUserId(int UserId)
        {
            List<PreferenceResponse> listaResponse = new List<PreferenceResponse>();
            var lista = await _query.GetAllByUserId(UserId);

            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    PreferenceResponse response = new PreferenceResponse
                    {
                        UserId = item.UserId,
                        Interest = new InterestResponse { Id = item.Interest.InterestId, Description = item.Interest.Description,
                            InterestCategory = new InterestCategoryResponse
                            {
                                Id = item.Interest.InterestCategory.InterestCategoryId,
                                Description = item.Interest.InterestCategory.Description
                            }
                        },
                        OwnInterest = item.OwnInterest,
                        Like = item.Like
                    };
                    listaResponse.Add(response);
                }
            }

            return listaResponse;
        }
        //Utilizo para busqueda "FULL"
        public async Task<IEnumerable<PreferenceResponseFull>> GetAllByUserIdFull(int UserId)
        {
            List<PreferenceResponseFull> listaResponse = new List<PreferenceResponseFull>();
            var lista = await _query.GetAllByUserId(UserId);

            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    PreferenceResponseFull response = new PreferenceResponseFull
                    {
                        //UserId = item.UserId,
                        Interest = new InterestResponse
                        {
                            Id = item.Interest.InterestId,
                            Description = item.Interest.Description,
                            InterestCategory = new InterestCategoryResponse
                            {
                                Id = item.Interest.InterestCategory.InterestCategoryId,
                                Description = item.Interest.InterestCategory.Description
                            }
                        },
                        OwnInterest = item.OwnInterest,
                        Like = item.Like
                    };
                    listaResponse.Add(response);
                }
            }

            return listaResponse;
        }

        public async Task<PreferenceResponse> GetByid(int UserId, int InterestId)
        {
            var responsePreference = await _query.GetById(UserId, InterestId);

            PreferenceResponse response = new PreferenceResponse
            {
                UserId = responsePreference.UserId,
                Interest = new InterestResponse { Id = responsePreference.Interest.InterestId, Description = responsePreference.Interest.Description,
                    InterestCategory = new InterestCategoryResponse
                    {
                        Id = responsePreference.Interest.InterestCategory.InterestCategoryId,
                        Description = responsePreference.Interest.InterestCategory.Description
                    }
                },
                OwnInterest = responsePreference.OwnInterest,
                Like = responsePreference.Like
            };

            return response;
        }

        public async Task<PreferenceResponse> Insert(PreferenceReq request)
        {
            var interest = await _interestService.GetById(request.InterestId);

            if (interest == null)
            {
                return null;
            }
            else
            {
                var responsePreference = await _query.GetById(request.UserId, request.InterestId);

                Preference preference = new Preference
                {
                    UserId = request.UserId,
                    InterestId = request.InterestId,
                    OwnInterest = request.OwnInterest,
                    Like = request.Like
                };

                if (responsePreference == null)
                {

                    await _command.Insert(preference);
                }
                else
                {
                    return null;
                }
                //else
                //{
                //    responsePreference.OwnInterest = request.OwnInterest;
                //    responsePreference.Like = request.Like;
                //    await _command.Update(responsePreference);
                //}

                PreferenceResponse response = new PreferenceResponse
                {
                    UserId = preference.UserId,
                    Interest = new InterestResponse { Id = interest.Id, Description = interest.Description,
                        InterestCategory = new InterestCategoryResponse
                        {
                            Id = interest.InterestCategory.Id,
                            Description = interest.InterestCategory.Description
                        }
                    },
                    OwnInterest = preference.OwnInterest,
                    Like = preference.Like
                };

                return response;
            }            
        }

        public async Task<PreferenceResponse> Update(PreferenceReq request)
        {
            Preference preference = await _query.GetById(request.UserId, request.InterestId);
            var interest = await _interestService.GetById(request.InterestId);

            if (preference != null && interest != null)
            {
                preference.UserId = request.UserId;
                preference.InterestId = request.InterestId;
                preference.OwnInterest = request.OwnInterest;
                preference.Like = request.Like;

                await _command.Update(preference);

                PreferenceResponse response = new PreferenceResponse
                {
                    UserId = request.UserId,
                    Interest = new InterestResponse
                    {
                        Id = interest.Id,
                        Description = interest.Description,
                        InterestCategory = new InterestCategoryResponse
                        {
                            Id = interest.InterestCategory.Id,
                            Description = interest.InterestCategory.Description
                        }
                    },
                    OwnInterest = request.OwnInterest,
                    Like = request.Like
                };

                return response;
            }

            return null;
        }

        public async Task<PreferenceResponse> Delete(PreferenceReqId request)
        {
            try
            {
                var preferenceResponse = await _query.GetById(request.UserId, request.InterestId);

                if (preferenceResponse != null)
                {
                    await _command.Delete(preferenceResponse);

                    var interest = await _interestService.GetById(request.InterestId);

                    PreferenceResponse response = new PreferenceResponse
                    {
                        UserId = preferenceResponse.UserId,
                        Interest = new InterestResponse
                        {
                            Id = interest.Id,
                            Description = interest.Description,
                            InterestCategory = new InterestCategoryResponse
                            {
                                Id = interest.InterestCategory.Id,
                                Description = interest.InterestCategory.Description
                            }
                        },
                        OwnInterest = preferenceResponse.OwnInterest,
                        Like = preferenceResponse.Like
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
