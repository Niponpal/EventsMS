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
    public DbSet<PaymentHistory> PaymentHistories { get; set; }
    public DbSet<Registration> Registrations { get; set; }
}
