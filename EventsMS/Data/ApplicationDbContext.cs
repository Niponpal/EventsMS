using EventsMS.Auth_IdentityModel;
using EventsMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace EventsMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<
        IdentityModel.User,
        IdentityModel.Role,
        long,
        IdentityModel.UserClaim,
        IdentityModel.UserRole,
        IdentityModel.UserLogin,
        IdentityModel.RoleClaim,
        IdentityModel.UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // ==============================
        // DbSets
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FoodToken> FoodTokens { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<StudentRegistration> studentRegistrations { get; set; }

        // ==============================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------- Category ↔ Event (One-to-Many) --------
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // -------- Event ↔ StudentRegistration (One-to-Many) --------
            modelBuilder.Entity<StudentRegistration>()
                .HasOne(sr => sr.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(sr => sr.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // -------- StudentRegistration ↔ FoodToken (One-to-Many) --------
            modelBuilder.Entity<FoodToken>()
                .HasOne(ft => ft.Registration)
                .WithMany(sr => sr.foodTokens)
                .HasForeignKey(ft => ft.RegistrationId)
                .OnDelete(DeleteBehavior.Restrict);

            // -------- StudentRegistration ↔ Payment (One-to-One) --------
            modelBuilder.Entity<StudentRegistration>()
                .HasOne(sr => sr.Payment)
                .WithOne(p => p.Registration)
                .HasForeignKey<Payment>(p => p.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade);

            // -------- Payment ↔ PaymentHistory (One-to-Many) --------
            modelBuilder.Entity<PaymentHistory>()
                .HasOne(ph => ph.Payment)
                .WithMany(p => p.PaymentHistories)
                .HasForeignKey(ph => ph.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            // -------- Menu ↔ Menu (Self-referencing) --------
            modelBuilder.Entity<Menu>()
                .HasMany(m => m.Children)
                .WithOne(m => m.Parent)
                .HasForeignKey(m => m.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // -------- Unique Slug for Event --------
            modelBuilder.Entity<Event>()
                .HasIndex(e => e.Slug)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
            // Automatically apply configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        // ==============================
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Ignore pending model warnings
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

            // Debug logging
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseLoggerFactory(new LoggerFactory(new[] {
                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
            }));
        }
    }
}