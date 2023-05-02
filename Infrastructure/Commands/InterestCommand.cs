using Application.Interfaces;
using Azure.Core;
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

        public async Task Update(Interest request)
        {
            try
            {
                _context.Update(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        public async Task Delete(Interest request)
        {
            try
            {
                _context.InterestDb.Remove(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
