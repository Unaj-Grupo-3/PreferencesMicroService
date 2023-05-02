using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class OverallPreferenceCommand: IOverallPreferenceCommand
    {
        private readonly AppDbContext _context;

        public OverallPreferenceCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task Insert(OverallPreference request)
        {
            _context.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task Update(OverallPreference request)
        {
            try
            {
                _context.OverallPreferenceDb.Update(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        public async Task Delete(OverallPreference request)
        {
            try
            {
                _context.OverallPreferenceDb.Remove(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
