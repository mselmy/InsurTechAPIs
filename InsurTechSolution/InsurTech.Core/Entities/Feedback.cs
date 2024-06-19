using InsurTech.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public enum Rating
    {
        Great, Good, Poor
    }
    public class Feedback : BaseEntity
    {
        public Rating Rating { get; set; }
        public string Comment { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int InsurancePlanId { get; set; }
        public virtual InsurancePlan InsurancePlan { get; set; }
    }

}
