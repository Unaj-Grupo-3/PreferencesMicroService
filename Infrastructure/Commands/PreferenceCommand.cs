using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
