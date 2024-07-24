using Microsoft.EntityFrameworkCore;

namespace HMC_Project.Models
{
    public class HMCDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public HMCDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(_configuration.GetConnectionString(""));
        }
    }
}
