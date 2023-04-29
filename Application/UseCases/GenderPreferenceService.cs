using Application.Interfaces;
using Application.Models;

namespace Application.UseCases
{
    public class GenderPreferenceService : IGenderPreferenceService
    {
        public Task<IEnumerable<PreferenceResponse>> GetAllByUserId(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<PreferenceResponse> Insert(PreferenceReq request)
        {
            throw new NotImplementedException();
        }
    }
}
