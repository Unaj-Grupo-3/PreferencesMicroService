using Application.Interfaces;
using Application.Models;

namespace Application.UseCases
{
    public class UserPreferencesService : IUserPreferencesService
    {
        private readonly IPreferenceService _preferenceService;
        private readonly IOverallPreferenceService _overallPreferenceService;
        private readonly IGenderPreferenceService _genderPreferenceService;

        public UserPreferencesService(IPreferenceService preferenceService, IOverallPreferenceService overallPreferenceService, IGenderPreferenceService genderPreferenceService)
        {
            _preferenceService = preferenceService;
            _overallPreferenceService = overallPreferenceService;
            _genderPreferenceService = genderPreferenceService;
        }

        public async Task<IEnumerable<UserPreferencesResponse>> GetAll(string urluser)
        {
            List<UserPreferencesResponse> usersPreferences = new List<UserPreferencesResponse>();

            var overallPreferenceResponses = await _overallPreferenceService.GetAll();

            foreach (var overallPreference in overallPreferenceResponses)
            {
                List<int> interests = new List<int>();
                List<int> genders = new List<int>();

                var preferenceResponse = await _preferenceService.GetAllByUserId(overallPreference.UserId);
                var genderPreferenceResponse = await _genderPreferenceService.GetAllByUserId(urluser, overallPreference.UserId);

                if (overallPreference != null)
                {

                    if (preferenceResponse.Any())
                    {
                        foreach (var item in preferenceResponse)
                        {
                            if (item.Like)
                            {
                                interests.Add(item.Interest.Id);
                            }
                        }
                    }

                    if (genderPreferenceResponse.Any())
                    {
                        foreach (var item in genderPreferenceResponse)
                        {
                            genders.Add(item.GenderId);
                        }
                    }


                    UserPreferencesResponse response = new UserPreferencesResponse
                    {
                        UserId = overallPreference.UserId,
                        SinceAge = overallPreference.SinceAge,
                        UntilAge = overallPreference.UntilAge,
                        Distance = overallPreference.Distance,
                        GendersPreferencesId = genders,
                        InterestPreferencesId = interests
                    };

                    usersPreferences.Add(response);
                }               
            }

            return usersPreferences;
        }

        public async Task<UserPreferencesResponse> GetByUserId(string urluser, int UserId)
        {
            List<int> interests = new List<int>();
            List<int> genders = new List<int>();  

            var preferenceResponse = await _preferenceService.GetAllByUserId(UserId);
            var overallPreferenceResponse = await _overallPreferenceService.GetByUserId(UserId);
            var genderPreferenceResponse = await _genderPreferenceService.GetAllByUserId(urluser, UserId);

            if (overallPreferenceResponse != null)
            {

                if(preferenceResponse.Any())
                {
                    foreach (var item in preferenceResponse)
                    {
                        if(item.Like)
                        {
                            interests.Add(item.Interest.Id);
                        }
                    }
                }

                if (genderPreferenceResponse.Any())
                {
                    foreach (var item in genderPreferenceResponse)
                    {
                        genders.Add(item.GenderId);
                    }
                }


                UserPreferencesResponse response = new UserPreferencesResponse
                {
                    UserId = UserId,
                    SinceAge = overallPreferenceResponse.SinceAge,
                    UntilAge = overallPreferenceResponse.UntilAge,
                    Distance = overallPreferenceResponse.Distance,
                    GendersPreferencesId = genders,
                    InterestPreferencesId = interests
                };

                return response;
            }

            return null;
        }
    }
}
