using InsurTech.Core.Entities;
using InsurTech.Core.Service;
using InsurTech.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Service
{
    public class RequestService:IRequestService
    {
        private readonly InsurtechContext _context;

        public RequestService(InsurtechContext context)
        {
            _context = context;
        }

        public async Task<List<UserRequest>> GetRequestsByCompanyId(string companyId)
        {
            // Get the insurance plans for the specified company

            var insurancePlans = await _context.InsurancePlans
                .Where(p => p.CompanyId == companyId)
                .Select(p => p.Id)
                .ToListAsync();

            //Get the requests for those insurance plans


           var requests = await _context.Requests
               .Where(r => insurancePlans.Contains(r.InsurancePlanId))
               .ToListAsync();

            return requests;
            #region  we can use lazy load here ..

            //return await _context.Requests
            //    .Include(r => r.InsurancePlan)
            //    .Where(r => r.InsurancePlan.CompanyId == companyId)
            //    .ToListAsync();
            #endregion
        }



        public async Task<UserRequest> GetRequestByIdAsync(string id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task UpdateRequestAsync(UserRequest request)
        {
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
