using Repository.Abstract;
using Repository.Concrete;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repository.Concrete
{
    public class EntityRepositoryBase<TEntity> : IEntityRepositoryBase<TEntity> where TEntity : IEntity, new()
    {
        protected TwitterContext _twitterContext;
        public EntityRepositoryBase(TwitterContext twitterContext)
        {
            _twitterContext = twitterContext;
        }
        public virtual TEntity Add(TEntity entity)
        {
            _twitterContext.Set<TEntity>().Add(entity);
            _twitterContext.SaveChanges();

            return entity;
        }

        public virtual TEntity Delete(int id)
        {
            var a = Get(id);
            Delete(a);
            return a;
        }

        public TEntity Delete(TEntity entity)
        {
            _twitterContext.Set<TEntity>().Remove(entity);
            _twitterContext.SaveChanges();

            return entity;
        }

        public virtual TEntity Get(int id)
        {
            return _twitterContext.Set<TEntity>().Find(id);

        }

        public virtual List<TEntity> GetAll()
        {
            return _twitterContext.Set<TEntity>().ToList();
        }

        public virtual TEntity Update(TEntity entity)
        {


            foreach (var e in _twitterContext.ChangeTracker.Entries())
            {
                e.State = EntityState.Detached;
            }


            _twitterContext.Set<TEntity>().Update(entity);
            _twitterContext.SaveChanges();
            return entity;

        }

    }
}
