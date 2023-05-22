using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class InterestQuery : IInterestQuery
    {
        private readonly AppDbContext _context;

        public InterestQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Interest>> GetAll()
        {
            var lista = await _context.InterestDb.Include(i => i.InterestCategory).ToListAsync();
            return lista;
        }

        public async Task<IEnumerable<Interest>> GetAllByCategory(int interestCategoryId)
        {
            var lista = await _context.InterestDb.Where(i => i.InterestCategoryId == interestCategoryId).Include(i => i.InterestCategory).ToListAsync();
            return lista;
        }

        public async Task<Interest> GetById(int id)
        {
            var interes = await _context.InterestDb.Include(i => i.InterestCategory).Where(i => i.InterestId == id).FirstOrDefaultAsync();
            return interes;
        }
    }
}
