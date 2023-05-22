using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class PreferenceCommand : IPreferenceCommand
    {
        private readonly AppDbContext _context;

        public PreferenceCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task Insert(Preference request)
        {
            _context.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Preference request)
        {
            try
            {
                _context.PreferenceDb.Update(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        public async Task Delete(Preference request)
        {
            try
            {
                _context.PreferenceDb.Remove(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
