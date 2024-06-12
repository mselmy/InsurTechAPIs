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
    public class InsurancePlanConfiguration : IEntityTypeConfiguration<InsurancePlan>
    {
        public void Configure(EntityTypeBuilder<InsurancePlan> builder)
        {
            builder.Property(a => a.YearlyCoverage).HasColumnType("decimal(18,2)");
            builder.Property(a => a.Quotation).HasColumnType("decimal(18,2)");

            builder.UseTptMappingStrategy();



            builder.HasOne(a => a.Category)
              .WithMany(u => u.InsurancePlans)
              .HasForeignKey(a => a.CategoryId);
        }
    }
}
