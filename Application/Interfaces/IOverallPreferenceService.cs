﻿using Application.Models;

namespace Application.Interfaces
{
    public interface IOverallPreferenceService
    {
        Task<IEnumerable<OverallPreferenceResponse>> GetAll();
        Task<IEnumerable<OverallPreferenceResponse>> GetByListId(List<int> userIds);
        Task<OverallPreferenceResponse> GetByUserId(int UserId);
        Task<OverallPreferenceResponse> Insert(OverallPreferenceReq request);
        Task<OverallPreferenceResponse> Update(OverallPreferenceReq request);
        Task<OverallPreferenceResponse> Delete(int UserId);
    }
}
