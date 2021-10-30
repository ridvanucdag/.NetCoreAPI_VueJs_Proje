using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FollowController : ControllerBase
    {

        private IFollowService _followService;
        private UserDetail _userDetail;
        public FollowController(IFollowService followService, UserDetail userDetail)
        {
            _followService = followService; 
            _userDetail = userDetail;
        }

        [HttpGet("{id}")]
        public Follow Get(int id)
        {
            return _followService.Get(id);
        }

        [HttpGet]
        public List<Follow> Get()
        {
            return _followService.GetAll();
        }

        [HttpGet("Followers")]
        public List<Follow> Followers(String nick)
        {
            int userId = _userDetail.GetLoggedInUserId(HttpContext);
            return _followService.Followers(nick);
        }
        [HttpGet("Followings")]
        public List<Follow> Followings (String nick)
        {
            int userId = _userDetail.GetLoggedInUserId(HttpContext);
            return _followService.Followings(nick);
        }

        [HttpPost]
        public bool Post([FromBody] Follow follow)
        {
             follow.UserID = _userDetail.GetLoggedInUserId(HttpContext);
            return _followService.Toggle(follow);
        }

        [HttpPost("doesitfollow")]
        public bool DoesItFollow([FromBody] Follow follow)
        {
            follow.UserID = _userDetail.GetLoggedInUserId(HttpContext);
            return _followService.DoesItFollow(follow);
        }
    }
}
