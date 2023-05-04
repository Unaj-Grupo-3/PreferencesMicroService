using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class GenderPreferenceQuery : IGenderPreferenceQuery
    {
        private readonly AppDbContext _context;

        public GenderPreferenceQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenderPreference>> GetAllByUserId(Guid userId)
        {
            var lista = await _context.GenderPreferenceDb.Where(i => i.UserId == userId).ToListAsync();
            return lista;
        }

        public async Task<GenderPreference> GetById(Guid userId, int genderId)
        {
            var gender = await _context.GenderPreferenceDb.Where(i => i.UserId == userId && i.GenderId == genderId).FirstOrDefaultAsync();
            return gender;
        }
    }
}
