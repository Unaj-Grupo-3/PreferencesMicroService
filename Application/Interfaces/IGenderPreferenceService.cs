﻿using Application.Models;

namespace Application.Interfaces
{
    public interface IGenderPreferenceService
    {
        Task<GenderPreferenceResponse> Insert(GenderPreferenceReq request);
        Task<IEnumerable<GenderPreferenceResponse>> GetAllByUserId(Guid userId);
        Task<GenderPreferenceResponse> Delete(GenderPreferenceReq request);
    }
}
