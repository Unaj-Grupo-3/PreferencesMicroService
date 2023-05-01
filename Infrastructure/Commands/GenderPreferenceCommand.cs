using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class GenderPreferenceCommand : IGenderPreferenceCommand
    {
        private readonly AppDbContext _context;

        public GenderPreferenceCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task Insert(GenderPreference request)
        {
            _context.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(GenderPreference request)
        {
            _context.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}
