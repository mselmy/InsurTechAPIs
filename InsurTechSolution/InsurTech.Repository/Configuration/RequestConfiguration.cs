using InsurTech.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Repository.Configuration
{
    public class RequestConfiguration : IEntityTypeConfiguration<UserRequest>
    {
        public void Configure(EntityTypeBuilder<UserRequest> builder)
        {
            builder.HasOne(r => r.InsurancePlan)
                .WithMany(ip => ip.Requests)
                .HasForeignKey(r => r.InsurancePlanId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
