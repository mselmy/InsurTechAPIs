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
    public class MotorInsurancePlanConfiguration : IEntityTypeConfiguration<MotorInsurancePlan>
    {
        public void Configure(EntityTypeBuilder<MotorInsurancePlan> builder)
        {
            builder.Property(a => a.PersonalAccident).HasColumnType("decimal(18,2)");
            builder.Property(a => a.Theft).HasColumnType("decimal(18,2)");
            builder.Property(a => a.ThirdPartyLiability).HasColumnType("decimal(18,2)");
            builder.Property(a => a.OwnDamage).HasColumnType("decimal(18,2)");
            builder.Property(a => a.LegalExpenses).HasColumnType("decimal(18,2)");
        }
    }
}
