using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using InsurTech.Core;
using InsurTech.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InsurtechContext _db;
        private Hashtable _repos;
        public UnitOfWork(InsurtechContext db)
        {
            _db = db;
            _repos=new Hashtable();
        }
        public async Task<int> CompleteAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync() => await _db.DisposeAsync();

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;

            if (!_repos.ContainsKey(type))
            {
                var Repo = new GenericRepository<T>(_db);
                _repos.Add(type, Repo);
            }
            return (IGenericRepository<T>)_repos[type];
        }
    }
}
