using EventsMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsMS.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<FoodToken> FoodTokens { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Payment>  payments { get; set; }
    public DbSet<PaymentHistory> paymentHistories { get; set; }
    public DbSet<StudentRegistration> studentRegistrations { get; set; }
}
