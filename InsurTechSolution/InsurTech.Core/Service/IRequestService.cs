using InsurTech.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Service
{
    public interface IRequestService
    {
        Task<List<UserRequest>> GetRequestsByCompanyId(string companyId);
        Task<UserRequest> GetRequestByIdAsync(string id); 
        Task UpdateRequestAsync(UserRequest request); 

    }
}
