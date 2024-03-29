using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCases
{
    public class OverallPreferenceService : IOverallPreferenceService
    {
        private readonly IOverallPreferenceQuery _query;
        private readonly IOverallPreferenceCommand _command;

        public OverallPreferenceService(IOverallPreferenceQuery query, IOverallPreferenceCommand command)
        {
            _query = query;
            _command = command;
        }

        public async Task<IEnumerable<OverallPreferenceResponse>> GetAll()
        {
            List<OverallPreferenceResponse> listaResponse = new List<OverallPreferenceResponse>();
            var lista = await _query.GetAll();

            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    OverallPreferenceResponse response = new OverallPreferenceResponse
                    {
                        Id = item.OverallPreferenceId,
                        UserId = item.UserId,
                        SinceAge = item.SinceAge,
                        UntilAge = item.UntilAge,
                        Distance = item.Distance
                    };
                    listaResponse.Add(response);
                }
            }

            return listaResponse;
        }

        public async Task<OverallPreferenceResponse> GetByUserId(int userId)
        {
            var overallPreference = await _query.GetByUserId(userId);

            if (overallPreference != null)
            {
                OverallPreferenceResponse response = new OverallPreferenceResponse
                {
                    Id = overallPreference.OverallPreferenceId,
                    UserId = overallPreference.UserId,
                    SinceAge = overallPreference.SinceAge,
                    UntilAge = overallPreference.UntilAge,
                    Distance = overallPreference.Distance
                };

                return response;
            }

            return null;
        }

        public async Task<OverallPreferenceResponse> Insert(OverallPreferenceReq request, int userId)
        {
            if (request.SinceAge<18)
            {
                throw new ArgumentException("La edad debe ser mayor a 18 a�os");
            }
            if (request.UntilAge<request.SinceAge)
            {
                throw new ArgumentException("La edad :Hasta debe ser mayor a edad :Desde");
            }
            var responsePreference = await _query.GetByUserId(userId);

            OverallPreference preference = new OverallPreference
            {
                UserId = userId,
                SinceAge = request.SinceAge,
                UntilAge = request.UntilAge,
                Distance = request.Distance
            };

            if (responsePreference == null)
            {
                await _command.Insert(preference);
            }
            else
            {
                return null;
            }

            OverallPreferenceResponse response = new OverallPreferenceResponse
            {
                Id = preference.OverallPreferenceId,
                UserId = preference.UserId,
                SinceAge = preference.SinceAge,
                UntilAge = preference.UntilAge,
                Distance = preference.Distance
            };

            return response;
        }

        public async Task<OverallPreferenceResponse> Update(OverallPreferenceReq request, int userId)
        {
            if (request.SinceAge < 18)
            {
                throw new ArgumentException("La edad debe ser mayor a 18 a�os");
            }
            if (request.UntilAge < request.SinceAge)
            {
                throw new ArgumentException("La edad :Hasta debe ser mayor a edad :Desde");
            }
            if (request.Distance <= 0)
            {
                throw new ArgumentException("La distancia debe ser mayor a 0 km");
            }


            OverallPreference preference = await _query.GetByUserId(userId);

            if (preference != null)
            {
                
                preference.SinceAge = request.SinceAge;
                preference.UntilAge = request.UntilAge;
                preference.Distance = request.Distance;

                await _command.Update(preference);

                OverallPreferenceResponse response = new OverallPreferenceResponse
                {
                    Id = preference.OverallPreferenceId,
                    UserId = preference.UserId,
                    SinceAge = preference.SinceAge,
                    UntilAge = preference.UntilAge,
                    Distance = preference.Distance
                };

                return response;
            }

            return null;
        }

        public async Task<OverallPreferenceResponse> Delete(int userId)
        {
            try
            {
                OverallPreference preference = await _query.GetByUserId(userId);

                if (preference != null)
                {
                    await _command.Delete(preference);

                    OverallPreferenceResponse response = new OverallPreferenceResponse
                    {
                        Id = preference.OverallPreferenceId,
                        UserId = preference.UserId,
                        SinceAge = preference.SinceAge,
                        UntilAge = preference.UntilAge,
                        Distance = preference.Distance
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

        public async Task<IEnumerable<OverallPreferenceResponse>> GetByListId(List<int> userIds)
        {
            List<OverallPreferenceResponse> listaResponse = new List<OverallPreferenceResponse>();
            var lista = await _query.GetByListId(userIds);

            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    OverallPreferenceResponse response = new OverallPreferenceResponse
                    {
                        Id = item.OverallPreferenceId,
                        UserId = item.UserId,
                        SinceAge = item.SinceAge,
                        UntilAge = item.UntilAge,
                        Distance = item.Distance
                    };
                    listaResponse.Add(response);
                }
            }

            return listaResponse;
        }
    }
}
