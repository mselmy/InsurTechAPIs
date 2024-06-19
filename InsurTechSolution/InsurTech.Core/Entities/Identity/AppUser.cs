using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities.Identity
{
    public enum UserType
    {
        Customer, Company, Admin
    }
    public enum IsApprove
	{
       pending , approved, rejected
    }
    public class AppUser : IdentityUser
    {
        public UserType UserType { get; set; }
        public IsApprove IsApprove { get; set; } = IsApprove.pending;
        public bool IsDeleted { get; set; } = false;
        public string Name { get; set; }
/*		public string? NationalID { get; set; }
		public DateOnly? BirthDate { get; set; }*/


		public const int MaxPlainPasswordLength = 128;
        public const int MaxEmailAddressLength = 120;
        public const int MaxNameLength = 20;
        public const int MaxPhoneNumberLength = 11;

	}
    public class Company : AppUser
    {
        public string TaxNumber { get; set; }
        public string Location { get; set; }
        public virtual ICollection<InsurancePlan> InsurancePlans { get; set; } = new List<InsurancePlan>();
		
    }
    public class Customer : AppUser
    {
        public string NationalID { get; set; }
        
        public DateOnly BirthDate { get; set; }
		public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
