using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EventsMS.Auth_IdentityModel.IdentityModel;

namespace EventsMS.Data.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new Role
        {
            Id = 1,
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR",
            Description = "Default role assigned to all employees."

        }, new Role
        {
            Id = 2,
            Name = "Student",
            NormalizedName = "STUDENT",
            Description = "Default role assigned to all employees."
        }, new Role
        {
            Id = 3,
            Name = "Mangement",
            NormalizedName = "MANGEMENT",
            Description = "Default role assigned to all customers."
        }

        );
    }
}

