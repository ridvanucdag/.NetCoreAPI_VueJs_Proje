using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITweetService : IEntityServiceBase<Tweet>
    {
        List<Tweet> FetchTweetsOfPeopleFollow(int userId, int ofset, int limit);
        List<Tweet> GetAllbyTweetUserId(int userId, int ofset, int limit);
        int TweetCount(string nick);
    }
}
