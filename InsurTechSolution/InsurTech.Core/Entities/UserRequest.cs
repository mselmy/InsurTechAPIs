using InsurTech.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class UserRequest : BaseEntity
    {
        public string CustomerId { get; set; }
        public virtual AppUser Customer { get; set; }
        public int InsurancePlanId { get; set; }
        public virtual InsurancePlan InsurancePlan { get; set; }
        public virtual ICollection<RequestQuestion> RequestQuestions { get; set; } = new List<RequestQuestion>();
        public RequestStatus Status { get; set; }
    }
}
