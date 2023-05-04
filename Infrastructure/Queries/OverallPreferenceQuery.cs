using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class OverallPreferenceQuery: IOverallPreferenceQuery
    {
        private readonly AppDbContext _context;

        public OverallPreferenceQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OverallPreference>> GetAll()
        {
            var lista = await _context.OverallPreferenceDb.ToListAsync();
            return lista;
        }

        public async Task<OverallPreference> GetByUserId(Guid UserId)
        {
            var lista = await _context.OverallPreferenceDb.Where(i => i.UserId == UserId).FirstOrDefaultAsync();
            return lista;
        }
    }
}
