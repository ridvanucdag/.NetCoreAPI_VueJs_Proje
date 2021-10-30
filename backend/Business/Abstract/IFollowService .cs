using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFollowService : IEntityServiceBase<Follow>
    {
        List<Follow> Followers(String nick);
        List<Follow> Followings(String nick);
        bool Toggle(Follow follow);
        bool DoesItFollow(Follow follow);
    }
}
