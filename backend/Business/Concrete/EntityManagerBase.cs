using Business.Abstract;
using Entities.Abstract;
using Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EntityManagerBase<TEntity, TRepository> : IEntityServiceBase<TEntity> where TEntity : IEntity, new() where TRepository : IEntityRepositoryBase<TEntity>
    {
        protected TRepository _repositoryBase;
        public EntityManagerBase(TRepository repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }
        public TEntity Add(TEntity entity)
        {

            return _repositoryBase.Add(entity);
        }

        public virtual TEntity Delete(int id)
        {
            return _repositoryBase.Delete(id);
        }


        public TEntity Get(int id)
        {
            return _repositoryBase.Get(id);
        }

        public List<TEntity> GetAll()
        {
            return _repositoryBase.GetAll();
        }

        public virtual TEntity Update(TEntity entity)
        {
            return _repositoryBase.Update(entity);
        }
    }
}
