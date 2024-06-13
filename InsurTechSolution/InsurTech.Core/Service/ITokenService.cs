using InsurTech.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Service
{
    public interface ITokenService
    {
        public Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
    }
}
