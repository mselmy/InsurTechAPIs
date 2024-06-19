using InsurTech.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public enum InsurancePlanLevel
    {
        basic, Standard, Premium
    }
    public class InsurancePlan : BaseEntity
    {
        public decimal YearlyCoverage { get; set; }
        
        public InsurancePlanLevel Level { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public decimal Quotation { get; set; }
        public string CompanyId { get; set; }
        public virtual Company Company { get; set; } 
        public virtual ICollection<UserRequest> Requests { get; set; }
        public bool AvailableInsurance { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

    }
}
