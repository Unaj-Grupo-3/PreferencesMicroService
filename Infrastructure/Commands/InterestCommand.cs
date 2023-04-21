using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class InterestCommand: IInterestCommand
    {
        private readonly AppDbContext _context;

        public InterestCommand(AppDbContext context)
        {
            _context = context;
        }
        public async Task Insert(Interest request)
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
    }
}
