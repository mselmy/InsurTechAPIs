using InsurTech.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public class UserRequest : BaseEntity
    {
        public string CustomerId { get; set; }
        public virtual AppUser Customer { get; set; }
        public int InsurancePlanId { get; set; }
        public virtual InsurancePlan InsurancePlan { get; set; }
        public ICollection<RequestQuestion> RequestQuestions { get; set; } = new List<RequestQuestion>();
    }
}
