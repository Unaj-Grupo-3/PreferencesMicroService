using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCases
{
    public class GenderPreferenceService : IGenderPreferenceService
    {
        private readonly IGenderPreferenceQuery _query;
        private readonly IGenderPreferenceCommand _command;
        private readonly IUserApiService _userService;

        public GenderPreferenceService(IGenderPreferenceQuery query, IGenderPreferenceCommand command, IUserApiService userService)
        {
            _query = query;
            _command = command;
            _userService = userService;

        }
        public async Task<IEnumerable<GenderPreferenceResponse>> GetAllByUserId(Guid userId)
        {
            List<GenderPreferenceResponse> listaResponse = new List<GenderPreferenceResponse>();
            var lista = await _query.GetAllByUserId(userId);
            var listaGenders = await _userService.GetAllGenders();

            if (lista.Any() && listaGenders.Any())
            {
                foreach (var item in lista)
                {
                    GenderPreferenceResponse response = new GenderPreferenceResponse
                    {
                        UserId = item.UserId,
                        GenderId = item.GenderId,
                        GenderName = listaGenders.Where(g => g.GenderId == item.GenderId).Select(g => g.Description).FirstOrDefault()
                    };
                    listaResponse.Add(response);
                }
            }

            return listaResponse;
        }

        public async Task<GenderPreferenceResponse> Insert(GenderPreferenceReq request)
        {
            var gender = await _userService.GetGenderById(request.GenderId);

            if (gender != null)       
            {
                GenderPreference genderPreference = new GenderPreference
                {
                    UserId = request.UserId,
                    GenderId = request.GenderId
                };

                var responsePreference = await _query.GetById(request.UserId, request.GenderId);

                if (responsePreference == null)
                {
                    await _command.Insert(genderPreference);
                }
                else
                {
                    return null;
                }

                GenderPreferenceResponse response = new GenderPreferenceResponse
                {
                    UserId = genderPreference.UserId,
                    GenderId = genderPreference.GenderId,
                    GenderName = gender.Description
                };

                return response;
            }

            return null;
        }

        public async Task<GenderPreferenceResponse> Delete(GenderPreferenceReq request)
        {
            var gender = await _userService.GetGenderById(request.GenderId);

            if (gender != null)
            {
                GenderPreference genderPreference = new GenderPreference
                {
                    UserId = request.UserId,
                    GenderId = request.GenderId
                };

                var responsePreference = await _query.GetById(request.UserId, request.GenderId);

                if (responsePreference != null)
                {
                    await _command.Delete(responsePreference);
                }
                else
                {
                    return null;
                }

                GenderPreferenceResponse response = new GenderPreferenceResponse
                {
                    UserId = genderPreference.UserId,
                    GenderId = genderPreference.GenderId,
                    GenderName = gender.Description
                };

                return response;
            }

            return null;
        }
    }
}
