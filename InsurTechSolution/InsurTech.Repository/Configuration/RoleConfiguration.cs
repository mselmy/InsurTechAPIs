using InsurTech.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {

            builder.HasData(
                               new IdentityRole
                               {
                                   Id = "1",
                                   Name = Roles.Admin,
                                   NormalizedName = Roles.Admin.ToUpper()
                               },
                               new IdentityRole
                               {
                                   Id = "2",
                                   Name = Roles.Company,
                                   NormalizedName = Roles.Company.ToUpper()
                               },
                               new IdentityRole
                               {
                                   Id = "3",
                                   Name = Roles.Customer,
                                   NormalizedName = Roles.Customer.ToUpper()
                               }
                               );
        }
    }
}
