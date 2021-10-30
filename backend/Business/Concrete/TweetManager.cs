using Business.Abstract;
using Repository.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Concrete;
using Entities.DTO;

namespace Business.Concrete
{
    public class TweetManager : EntityManagerBase<Tweet, ITweetRepository>, ITweetService
    {
        public TweetManager(ITweetRepository tweetRepository) : base(tweetRepository) { }
        public List<Tweet> FetchTweetsOfPeopleFollow(int userId, int ofset, int limit)
        {
            return _repositoryBase.FetchTweetsOfPeopleFollow(userId, ofset, limit);
        }

        public List<Tweet> GetAllbyTweetUserId(int userId, int ofset, int limit)
        {
            return _repositoryBase.GetAllbyTweetUserId(userId, ofset, limit);
        }
        public int TweetCount(string nick)
        {
            return _repositoryBase.TweetCount(nick);

        }
    }
}

