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

        public Task<IEnumerable<PreferenceResponse>> GetAll()
        {
            throw new NotImplementedException();
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
                Preference preference = new Preference
                {
                    UserId = request.UserId,
                    InterestId = request.InterestId,
                    OwnInterest = request.OwnInterest,
                    Like = request.Like
                };

                await _command.Insert(preference);

                PreferenceResponse response = new PreferenceResponse
                {
                    Id = preference.PreferenceId,
                    UserId = preference.UserId,
                    Interest = new InterestResponse { Id = interest.Id, Description = interest.Description },
                    OwnInterest = preference.OwnInterest,
                    Like = preference.Like
                };

                return response;
            }            
        }
    }
}
