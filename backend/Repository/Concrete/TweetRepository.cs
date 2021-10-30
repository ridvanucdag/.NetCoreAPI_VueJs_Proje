using Repository.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTO;

namespace Repository.Concrete
{
    public class TweetRepository : EntityRepositoryBase<Tweet>, ITweetRepository
    {
        public TweetRepository(TwitterContext context) : base(context) { }
        public List<Tweet> FetchTweetsOfPeopleFollow(int userId, int ofset, int limit)
        {
            var sql = @"select * from (
                            select* from tweets
                               where UserID in(
                                   select FollowedID from Follows where UserID = {0}
	                            )
	                         union
                             select* from tweets where UserID = {0}
                             ) t order by At desc
                             OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY";

            return _twitterContext.Tweets.FromSqlRaw(sql, userId, ofset, limit).AsNoTracking().Include(x => x.Likes).ThenInclude(z => z.User).Include(y => y.User).OrderByDescending(x => x.At).ToList();
        }
        public override Tweet Get(int tweetID)
        {
            return this._twitterContext.Tweets.Include(t => t.User).Include(t => t.Likes).ThenInclude(l => l.User).ToList().Where(x => x.ID == tweetID).FirstOrDefault();
        }

        public List<Tweet> GetAllbyTweetUserId(int userId, int ofset, int limit)
        {

            return (from t in _twitterContext.Tweets
                    where t.UserID == userId
                    orderby t.At descending
                    select t).Include(t => t.User).Include(t => t.Likes).ThenInclude(l => l.User).Skip(ofset).Take(limit).ToList();
        }

        public List<ProfileDetail> GetAllMyProfileTweet(string userName, int ofset, int limit)
        {
            var profileTweet = from t in _twitterContext.Tweets.Include(x => x.User).Include(x => x.Likes).ThenInclude(l => l.User)
                               join u in _twitterContext.Users on t.UserID equals u.ID
                               where u.Nick == userName
                               orderby t.At descending
                               select new ProfileDetail()
                               {
                                   Tweet = t,
                                   User = t.User,
                                   Like = t.Likes,


                               };
            var b = profileTweet.ToList();

            return profileTweet.Skip(ofset).Take(limit).ToList();

        }

        public List<SearchDetail> Search(string search, int ofset, int limit)
        {
            var tweets = from t in _twitterContext.Tweets.Where(t => t.Content.Contains(search)).Include(x => x.User)
                         select new SearchDetail()
                         {
                             ID = t.ID,
                             User = null,
                             Tweet = t,
                             Type = Enum.GetName(typeof(SearchType), SearchType.Tweet)
                         };
            return tweets.Skip(ofset).Take(limit).ToList();
        }
        public override Tweet Update(Tweet entity)
        {
            var gettweet = Get(entity.ID);
            gettweet.Content = entity.Content;
            _twitterContext.Tweets.Update(gettweet);
            _twitterContext.SaveChanges();
            return this.Get(entity.ID);
        }

        public int TweetCount(string nick)
        {

            return (from t in _twitterContext.Tweets
                    join u in _twitterContext.Users on t.UserID equals u.ID
                    where u.Nick == nick
                    select t).Count();
        }
    }
}