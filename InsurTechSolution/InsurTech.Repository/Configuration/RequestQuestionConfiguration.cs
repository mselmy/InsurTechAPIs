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
    public class RequestQuestionConfiguration : IEntityTypeConfiguration<RequestQuestion>
    {
        public void Configure(EntityTypeBuilder<RequestQuestion> builder)
        {
            builder.HasIndex(a => new { a.QuestionId, a.UserRequestId }).IsUnique();
        }
    }
}
