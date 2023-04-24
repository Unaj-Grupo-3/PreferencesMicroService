using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Queries
{
    public class PreferenceQuery : IPreferenceQuery
    {
        private readonly AppDbContext _context;

        public PreferenceQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Preference>> GetAll()
        {
            var lista = await _context.PreferenceDb.Include(i => i.Interest).ThenInclude(i => i.InterestCategory).ToListAsync();
            return lista;
        }

        public async Task<IEnumerable<Preference>> GetAllByUserId(int UserId)
        {
            var lista = await _context.PreferenceDb.Include(i => i.Interest).ThenInclude(i => i.InterestCategory).Where(i => i.UserId == UserId).ToListAsync();
            return lista;
        }

        public async Task<Preference> GetById(int UserId, int InterestId)
        {
            var preference = await _context.PreferenceDb.Include(i => i.Interest).ThenInclude(i => i.InterestCategory).Where(i => i.InterestId == InterestId && i.UserId == UserId).FirstOrDefaultAsync();
            return preference;
        }
    }
}
