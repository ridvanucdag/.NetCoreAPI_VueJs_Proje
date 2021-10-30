using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TweetController : ControllerBase
    {

        private ITweetService _tweetService;
        private UserDetail _userDetail;
        public TweetController(ITweetService tweetService, UserDetail userDetail)
        {
            _tweetService = tweetService;
            _userDetail = userDetail;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (_tweetService.Get(id) != null)
                return Ok(_tweetService.Get(id));
            else
                return NotFound();
        }

        [HttpGet("myfollowed/{ofset},{limit}")]
        public IActionResult FetchTweetsOfPeopleFollow(int ofset, int limit)
        {
            int userId = _userDetail.GetLoggedInUserId(HttpContext);
            if (_tweetService.FetchTweetsOfPeopleFollow(userId, ofset, limit) != null)
                return Ok(_tweetService.FetchTweetsOfPeopleFollow(userId, ofset, limit));
            else
                return NotFound();
        }
        [HttpPost]
        public IActionResult Post([FromBody] Tweet tweet)
        {
            tweet.UserID = _userDetail.GetLoggedInUserId(HttpContext);
            return Ok(_tweetService.Add(tweet));
        }

        [HttpPut]
        public Tweet Put([FromBody] Tweet tweet)
        {
            return _tweetService.Update(tweet);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var findTweet = _tweetService.Get(id);
            int userId = _userDetail.GetLoggedInUserId(HttpContext);
            if (findTweet != null && findTweet.User.ID == userId)
                return Ok(_tweetService.Delete(id));
            else
                return NotFound();
        }

        [HttpGet("myUserTweet/{ofset},{limit}")]
        public IActionResult MyTweetProfile(int ofset, int limit)
        {
            int userId = _userDetail.GetLoggedInUserId(HttpContext);
            List<Tweet> MyTweets = _tweetService.GetAllbyTweetUserId(userId, ofset, limit);
            return Ok(MyTweets);
        }

        [HttpGet("count")]
        public IActionResult TweetCount(string nick)
        {
            if (_tweetService.TweetCount(nick) != null)
                return Ok(_tweetService.TweetCount(nick));
            else
                return NotFound(0);
        }
    }
}
