using InsurTech.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public class Article:BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateOnly Date { get; set; }
        public string ArticleImg { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
