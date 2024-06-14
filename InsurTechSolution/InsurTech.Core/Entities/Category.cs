using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<InsurancePlan> InsurancePlans { get; set; } = new List<InsurancePlan>();
        public virtual ICollection<Question> QuestionPlans { get; set; } = new List<Question>();
    }
}
