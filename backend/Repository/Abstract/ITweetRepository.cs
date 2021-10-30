using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface ITweetRepository : IEntityRepositoryBase<Tweet>
    {
        List<Tweet> FetchTweetsOfPeopleFollow(int userId, int ofset, int limit);
        List<SearchDetail> Search(string search, int ofset, int limit);
        List<Tweet> GetAllbyTweetUserId(int userId, int ofset, int limit);
        List<ProfileDetail> GetAllMyProfileTweet(string userName, int ofset, int limit);
        int TweetCount(string nick);
    }
}
