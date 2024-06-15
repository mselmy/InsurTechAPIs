using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using InsurTech.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly InsurtechContext _db;
        public GenericRepository(InsurtechContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);

        }

        public async Task Update(T entity)
        {
            _db.Set<T>().Update(entity);

        }

        public async Task Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public async Task AddListAsync(List<T> entity)
        {
			 _db.Set<T>().AddRangeAsync(entity);
		}

    }
}
