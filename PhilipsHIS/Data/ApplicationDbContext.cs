using Microsoft.EntityFrameworkCore;
using PhilipsHIS.Models;

namespace PhilipsHIS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<List> Lists { get; set; }
    }
}
