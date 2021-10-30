using Repository.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class FollowRepository : EntityRepositoryBase<Follow>, IFollowRepository
    {
        public FollowRepository(TwitterContext context) : base(context) { }
        public override Follow Get(int id)
        {
            return this._twitterContext.Follows
                .Include(f => f.User)
                .ToList()
                .Where(f => f.ID == id)
                .FirstOrDefault();
        }

        public override List<Follow> GetAll()
        {
            return this._twitterContext.Follows
                .Include(f => f.User)
                .ToList();
        }
        public List<Follow> Followers(String nick)
        {

            return (from u in _twitterContext.Users
                    join f in _twitterContext.Follows on u.ID equals f.UserID
                    orderby f.At descending
                    where (u.Nick == nick)
                    select f).Include(f => f.Followed).Include(f => f.User).ToList();
        }
        public List<Follow> Followings(String nick)
        {

            return (from u in _twitterContext.Users 
                    join f in _twitterContext.Follows on u.ID equals f.FollowedID
                    orderby f.At descending
                    where (u.Nick == nick)
                    select f).Include(f => f.Followed).Include(f => f.User).ToList();
        }
        public bool Toggle(Follow follow)
        {
            var followRecord = this._twitterContext.Follows.Where(f => f.UserID == follow.UserID && f.FollowedID == follow.FollowedID).FirstOrDefault();

            if (followRecord != null)
            {
                Delete(followRecord);
                return false;
            }
            else
            {
                Add(follow);
                return true;
            }

        }

        public bool DoesItFollow(Follow follow)
        {
            var followRecord = this._twitterContext.Follows.Where(f => f.UserID == follow.UserID && f.FollowedID == follow.FollowedID).FirstOrDefault();
            return followRecord != null ? true : false;
        }


    }
}
