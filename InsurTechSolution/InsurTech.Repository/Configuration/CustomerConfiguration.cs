using InsurTech.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InsurTech.Repository.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Define the relationship between Customer and Feedbacks
            builder.HasMany(c => c.Feedbacks)
                   .WithOne(f => f.Customer)
                   .HasForeignKey(f => f.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
