using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IFollowRepository : IEntityRepositoryBase<Follow>
    {
        List<Follow> Followers(String nick);
        List<Follow> Followings(String nick);
        bool Toggle(Follow follow);
        bool DoesItFollow(Follow follow);

    }
}
