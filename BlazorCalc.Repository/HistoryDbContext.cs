using BlazorCalc.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorCalc.Repository
{
    public class HistoryDbContext : DbContext
    {
        private readonly DbContextOptions _options;

        public HistoryDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<History> History { get; set; }
    }
}