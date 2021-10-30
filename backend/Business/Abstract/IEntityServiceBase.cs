using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEntityServiceBase<T> where T : IEntity, new()
    {
        T Add(T entity);
        T Get(int id);
        List<T> GetAll();
        T Delete(int id);
        T Update(T entity);
    }
}
