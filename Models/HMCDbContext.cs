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

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.UserAddresses)
            //    .WithOne()
            //    .HasForeignKey("UserId");

            modelBuilder.Entity<DepartmentAddress>()
                .HasKey(da => new { da.DepartmentID, da.AddressID });

            modelBuilder.Entity<EmployeeAddress>()
                .HasKey(da => new { da.EmployeeID, da.AddressID });

            modelBuilder.Entity<UserAddress>()
                .HasKey(da => new { da.UserID, da.UserAddressID });

            modelBuilder.Entity<DepartmentAddress>()
                .HasOne(da => da.Department)
                .WithMany(d => d.DepartmentAddresses)
                .HasForeignKey(da => da.DepartmentID);

            modelBuilder.Entity<EmployeeAddress>()
                .HasOne(da => da.Employee)
                .WithMany(d => d.EmployeeAddresses)
                .HasForeignKey(da => da.EmployeeID);

            modelBuilder.Entity<UserAddress>()
                .HasOne(da => da.User)
                .WithMany(d => d.UserAddresses)
                .HasForeignKey(da => da.UserID);

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
