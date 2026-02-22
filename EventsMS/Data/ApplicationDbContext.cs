using EventsMS.Auth_IdentityModel;
using EventsMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EventsMS.Data;

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
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<FoodToken> FoodTokens { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Payment> payments { get; set; }
    public DbSet<PaymentHistory> paymentHistories { get; set; }
    public DbSet<StudentRegistration> studentRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ==============================
        // Category ↔ Event
        modelBuilder.Entity<Event>()
            .HasOne(e => e.Category)
            .WithMany(c => c.Events)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Cascade); // safe, no conflict

        // ==============================
        // Event ↔ StudentRegistration
        modelBuilder.Entity<StudentRegistration>()
            .HasOne(sr => sr.Event)
            .WithMany(e => e.Registrations)
            .HasForeignKey(sr => sr.EventId)
            .OnDelete(DeleteBehavior.Cascade); // safe

        // ==============================
        // StudentRegistration ↔ FoodToken (One-to-Many)
        modelBuilder.Entity<FoodToken>()
            .HasOne(ft => ft.Registration)
            .WithMany(sr => sr.foodTokens)
            .HasForeignKey(ft => ft.RegistrationId)
            .OnDelete(DeleteBehavior.Restrict); // avoid multiple cascade paths

        // ==============================
        // StudentRegistration ↔ Payment (One-to-One)
        modelBuilder.Entity<StudentRegistration>()
            .HasOne(sr => sr.Payment)
            .WithOne(p => p.Registration)
            .HasForeignKey<Payment>(p => p.RegistrationId)
            .OnDelete(DeleteBehavior.Cascade); // safe, keep cascade

        // ==============================
        // Payment ↔ PaymentHistory (One-to-One)
        modelBuilder.Entity<PaymentHistory>()
            .HasOne(ph => ph.Payment)
            .WithOne(p => p.PaymentHistory)
            .HasForeignKey<PaymentHistory>(ph => ph.PaymentId)
            .OnDelete(DeleteBehavior.Cascade); // safe

        // ==============================
        // Menu ↔ Menu (Self-referencing)
        modelBuilder.Entity<Menu>()
            .HasMany(m => m.Children)
            .WithOne(m => m.Parent)
            .HasForeignKey(m => m.ParentId)
            .OnDelete(DeleteBehavior.Restrict); // prevent cascade loops

        // ==============================
        // Additional configurations if needed
        // e.g. unique slug for Event
        modelBuilder.Entity<Event>()
            .HasIndex(e => e.Slug)
            .IsUnique();
    }

}
