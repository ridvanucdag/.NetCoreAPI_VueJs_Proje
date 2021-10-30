using Business.Abstract;
using Repository.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Concrete;

namespace Business.Concrete
{
    public class FollowManager :EntityManagerBase<Follow,IFollowRepository>, IFollowService
    {
        public FollowManager(IFollowRepository followRepository) : base(followRepository) { }
        public List<Follow> Followers(String nick)
        {
            return _repositoryBase.Followers(nick);
        }
        public List<Follow> Followings(String nick)
        {
            return _repositoryBase.Followings(nick);
        }



        public bool Toggle(Follow follow)
        {
            return  _repositoryBase.Toggle(follow);
        }
        public bool DoesItFollow(Follow follow)
        {
            return _repositoryBase.DoesItFollow(follow);
        }
    }
}
