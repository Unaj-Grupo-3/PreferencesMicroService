using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class InterestCategoryCommand : IInterestCategoryCommand
    {
        private readonly AppDbContext _context;

        public InterestCategoryCommand(AppDbContext context)
        {
            _context = context;
        }
        public async Task Insert(InterestCategory request)
        {
            try
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        public async Task Update(InterestCategory request)
        {
            try
            {
                _context.InterestCategoryDb.Update(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        public async Task Delete(InterestCategory request)
        {
            try
            {
                _context.InterestCategoryDb.Remove(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
