using InsurTech.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        public Task AddAsync(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);
        public Task AddListAsync(List<T> entity);


	}
}
