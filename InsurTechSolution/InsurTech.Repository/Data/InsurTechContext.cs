using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Repository.Data
{
    public class InsurtechContext : IdentityDbContext<AppUser>
    {
        public InsurtechContext(DbContextOptions options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<HomeInsurancePlan> HomeInsurancePlans { get; set; }
        public DbSet<MotorInsurancePlan> MotorInsurancePlans { get; set; }
        public DbSet<InsurancePlan> InsurancePlans { get; set; }
        public DbSet<HealthInsurancePlan> HealthInsurancePlans { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserRequest> Requests { get; set; }
        public DbSet<RequestQuestion> RequestQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
