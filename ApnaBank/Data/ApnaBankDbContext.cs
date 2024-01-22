using ApnaBank.Models;
using Microsoft.EntityFrameworkCore;

namespace ApnaBank.Data
{
    public class ApnaBankDbContext : DbContext
    {
        public ApnaBankDbContext(DbContextOptions<ApnaBankDbContext> options) : base(options)
        {

        }
        public DbSet<AccountDetails> AccountDetails { get; set; }
    }
}
