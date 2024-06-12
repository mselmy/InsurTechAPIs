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
        rejected, approved, pending
    }
    public class AppUser : IdentityUser
    {
        public UserType UserType { get; set; }
        public IsApprove IsApprove { get; set; }

    }
}
