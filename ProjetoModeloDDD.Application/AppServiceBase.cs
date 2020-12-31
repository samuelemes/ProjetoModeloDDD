
using ProjetoModeloDDD.Application.Interfaces;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Application
{
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }


        public void Add(TEntity model)
        {
            _serviceBase.Add(model);
        }

        public void Dispose()
        {
            _serviceBase.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _serviceBase.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _serviceBase.GetById(id);
        }

        public void Remove(TEntity model)
        {
            _serviceBase.Remove(model);
        }

        public void Update(TEntity model)
        {
            _serviceBase.Update(model);
        }
    }
}
