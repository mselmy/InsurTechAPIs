using InsurTech.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InsurTech.Repository.Configuration
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            // Set the primary key
            builder.HasKey(f => f.Id);

            // Configure the Comment property to be required and have a maximum length of 1000
            builder.Property(f => f.Comment)
                   .IsRequired()
                   .HasMaxLength(1000);

            // Configure the relationship between Feedback and Customer (through AppUser)
            builder.HasOne(f => f.Customer)
                   .WithMany(c => c.Feedbacks)
                   .HasForeignKey(f => f.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Feedback and InsurancePlan
            builder.HasOne(f => f.InsurancePlan)
                   .WithMany(ip => ip.Feedbacks)
                   .HasForeignKey(f => f.InsurancePlanId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
