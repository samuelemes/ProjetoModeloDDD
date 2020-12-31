
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjetoModeloDDD.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected ProjetoModeloContext DbContext = new ProjetoModeloContext();
        public void Add(TEntity model)
        {
            DbContext.Set<TEntity>().Add(model);
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity model)
        {
            DbContext.Set<TEntity>().Remove(model);
            DbContext.SaveChanges();
        }

        public void Update(TEntity model)
        {
            DbContext.Entry(model).State = EntityState.Modified;
            DbContext.SaveChanges();
        }
    }
}
