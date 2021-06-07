using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorCalc.Repository.Contracts;
using BlazorCalc.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorCalc.Repository.Classes
{
    public class HistoryRepository : IRepository
    {
        private readonly HistoryDbContext _context;

        public HistoryRepository(HistoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetAll()
        {
            var result = await _context.Set<History>().ToListAsync();

            return result
                .OrderByDescending(h => h.CreatedOn)
                .Take(10)
                .Select(history => $"{history.Expression}={history.Result}")
                .ToList();
        }

        public async Task Save(string expression, string result)
        {
            _context.Set<History>().Add(new History
            {
                Expression = expression,
                Result = result,
                CreatedOn = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }
    }
}