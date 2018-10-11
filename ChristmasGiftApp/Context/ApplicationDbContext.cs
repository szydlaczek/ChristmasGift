using ChristmasGiftApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChristmasGiftApp.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeGift> EmployeeGifts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().
                Property(f => f.FirstName)
                .HasMaxLength(255)
                .IsRequired();
            modelBuilder.Entity<Employee>().
                Property(f => f.LastName)
                .HasMaxLength(255)
                .IsRequired();
            modelBuilder.Entity<Employee>().
                Property(f => f.EmailAddress)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<EmployeeGift>()
                .HasOne<Employee>(s => s.Employee)
                .WithMany(e => e.Gifts)
                .HasForeignKey(g => g.EmployeeId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeGift>()
                .HasKey(i => new { i.EmployeeId, i.TargetEmployeeId, i.Year });

            modelBuilder.Entity<Employee>().HasData(
               new Employee { Id = 1, FirstName = "Guy", LastName = "Gilbert", EmailAddress = "g.gilbert@company.com" },
               new Employee { Id = 2, FirstName = "Kevin", LastName = "Brown", EmailAddress = "k.brown@company.com" },
               new Employee { Id = 3, FirstName = "Roberto", LastName = "Tamburello", EmailAddress = "r.tamburello@company.com" },
               new Employee { Id = 4, FirstName = "Rob", LastName = "Walters", EmailAddress = "r.walters@company.com" },
               new Employee { Id = 5, FirstName = "David", LastName = "Bradley", EmailAddress = "d.bradley@company.com" },
               new Employee { Id = 6, FirstName = "Ruth", LastName = "Ellerbrock", EmailAddress = "r.ellerbrock@company.com" },
               new Employee { Id = 7, FirstName = "Geil", LastName = "Erickson", EmailAddress = "g.erickson@company.com" },
               new Employee { Id = 8, FirstName = "Steven", LastName = "Selikoff", EmailAddress = "steven0@adventure-works.com" },
               new Employee { Id = 9, FirstName = "Peter", LastName = "Krebs", EmailAddress = "peter0@adventure-works.com" },
               new Employee { Id = 10, FirstName = "Stuart", LastName = "Munson", EmailAddress = "stuart0@adventure-works.com" }
                );
        }
    }
}