﻿using Domain.Entities;

namespace Application.Interfaces
{
    public interface IInterestQuery
    {
        Task<IEnumerable<Interest>> GetAll();
        Task<Interest> GetById(int id);
    }
}
