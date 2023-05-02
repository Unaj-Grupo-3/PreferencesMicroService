using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class InterestCategoryQuery : IInterestCategoryQuery
    {
        private readonly AppDbContext _context;

        public InterestCategoryQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<InterestCategory>> GetAll()
        {
            var lista = await _context.InterestCategoryDb.Include(i => i.Interests).ToListAsync();
            return lista;
        }

        public async Task<InterestCategory> GetById(int categoryId)
        {
            var category = await _context.InterestCategoryDb.Where(c => c.InterestCategoryId == categoryId).FirstOrDefaultAsync();
            return category;
        }
    }
}
