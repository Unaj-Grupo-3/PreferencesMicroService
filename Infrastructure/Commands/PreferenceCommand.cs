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

                throw;
            }
        }
    }
}
