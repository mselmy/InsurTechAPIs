using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public class HealthInsurancePlan : InsurancePlan
    {
        public string MedicalNetwork { get; set; }
        public decimal ClinicsCoverage { get; set; }
        public decimal HospitalizationAndSurgery { get; set; }
        public decimal OpticalCoverage { get; set; }
        public decimal DentalCoverage { get; set; }
    }
}
