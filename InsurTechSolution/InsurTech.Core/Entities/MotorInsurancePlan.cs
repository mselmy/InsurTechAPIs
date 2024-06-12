using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public class MotorInsurancePlan : InsurancePlan
    {
        public decimal PersonalAccident { get; set; }
        public decimal Theft { get; set; }
        public decimal ThirdPartyLiability { get; set; }
        public decimal OwnDamage { get; set; }
        public decimal LegalExpenses { get; set; }
    }
}
