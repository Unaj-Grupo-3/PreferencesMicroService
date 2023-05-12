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

        public async Task<OverallPreferenceResponse> GetByUserId(int UserId)
        {            
            var overallPreference = await _query.GetByUserId(UserId);

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

        public async Task<OverallPreferenceResponse> Insert(OverallPreferenceReq request)
        {            
            var responsePreference = await _query.GetByUserId(request.UserId);

            OverallPreference preference = new OverallPreference
            {                
                UserId = request.UserId,
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

        public async Task<OverallPreferenceResponse> Update(OverallPreferenceReq request)
        {
            OverallPreference preference = await _query.GetByUserId(request.UserId);

            if (preference != null)
            {
                //preference.UserId = request.UserId;
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
        public async Task<OverallPreferenceResponse> Delete(int UserId)
        {
            try
            {
                OverallPreference preference = await _query.GetByUserId(UserId);

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
