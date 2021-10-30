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
    public class LikeRepository : EntityRepositoryBase<Like>, ILikeRepository
    {
        public LikeRepository(TwitterContext context) : base(context) { }

        public List<Like> Toggle(Like like)
        {
            var likeRecord = _twitterContext.Likes.Where(l => l.UserID == like.UserID && l.TweetID == like.TweetID).FirstOrDefault();
            if (likeRecord != null)
            {
                Count(like.TweetID, state: true);
                Delete(likeRecord);
            }
            else
            {
                Count(like.TweetID, state: false);
                Add(like);
            }

            return _twitterContext.Tweets.Include(t => t.Likes).ThenInclude(l => l.User).Where(t => t.ID == like.TweetID).FirstOrDefault().Likes.ToList();
        }
        public void Count(int id, bool state)
        {
            var tweet = _twitterContext.Tweets.Find(id);
            if (state) //down
                tweet.LikeCount -= 1;
            else //up
                tweet.LikeCount += 1;
        }
        public override Like Get(int id)
        {
            return _twitterContext.Likes.Include(x => x.User).Where(x => x.ID == id).FirstOrDefault();
        }
    }
}
