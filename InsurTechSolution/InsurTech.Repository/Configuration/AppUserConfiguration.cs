using InsurTech.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace InsurTech.Repository.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(a => a.Email).HasAnnotation("RegularExpression", @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            builder.Property(a => a.PhoneNumber).HasMaxLength(11).HasAnnotation("RegularExpression", @"^01(0|1|2|5)[0-9]{8}$");
            //builder.Property(a => a.p).HasAnnotation("RegularExpression", @"^(?=.[A-Za-z])(?=.\d)[A-Za-z\d]{8,}$");
            builder.Property(a => a.UserName).HasAnnotation("MinLength", 3).HasMaxLength(20).HasAnnotation("RegularExpression", @"^[a-zA-Z][a-zA-Z0-9]*$");
            builder.HasIndex(a => a.Email).IsUnique();
            builder.UseTphMappingStrategy();



            var hasher = new PasswordHasher<AppUser>();

            builder.HasData(
                               new AppUser
                               {
                                   Id = "1",
                                   EmailConfirmed = true,
                                   NormalizedEmail = "ASMAA_ASH@GMAIL.COM",
                                   NormalizedUserName = "ADMIN",
                                   UserName = "Admin",
                                   Email = "asmaa_ash@gmail.com",
                                   PhoneNumber = "01211236779",
                                   Name = "Asmaa Ashraf",
                                   UserType = UserType.Admin,
                                   IsApprove = IsApprove.approved,
                                   PasswordHash = hasher.HashPassword(null, "Ash@1234")
                                   
                               });
        }
    }
}
