using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface ILikeRepository : IEntityRepositoryBase<Like>
    {
        List<Like> Toggle(Like like);
    }
}
