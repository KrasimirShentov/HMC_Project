using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("HMC"));
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DepartmentAddress> DepartmentAddresses { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Training)
                .WithMany()
                .HasForeignKey("TrainingId")
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey("DepartmentId");
                

            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserID);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserID);

            modelBuilder.Entity<DepartmentAddress>()
                .HasKey(da => new { da.DepartmentID, da.AddressID });

            modelBuilder.Entity<EmployeeAddress>()
                .HasKey(da => new { da.EmployeeID, da.AddressID });


            modelBuilder.Entity<DepartmentAddress>()
                .HasOne(da => da.Department)
                .WithMany(d => d.DepartmentAddresses)
                .HasForeignKey(da => da.DepartmentID);

            modelBuilder.Entity<EmployeeAddress>()
                .HasOne(da => da.Employee)
                .WithMany(d => d.EmployeeAddresses)
                .HasForeignKey(da => da.EmployeeID);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Addresses)
                .WithOne(c => c.Company)
                .HasForeignKey(a => a.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Company)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Training>(entity =>
            {
                entity.HasKey(t => t.ID);
                entity.Property(t => t.Type).IsRequired();
                entity.Property(t => t.PositionName).IsRequired();
                entity.Property(t => t.Description).IsRequired();
                entity.Property(t => t.TrainingHours).IsRequired();
            });
        }
    }
}