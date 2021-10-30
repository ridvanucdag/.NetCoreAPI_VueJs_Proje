using Business.Abstract;
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
    public class ProfileController : ControllerBase
    {
        IProfileService _profileService;
         public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{userName}")]
        public IActionResult MyProfileUserName(string userName)
        {
            return Ok(_profileService.GetAllMyProfileUsername(userName));
        }
        [HttpGet("{userName},{ofset},{limit}")]
        public IActionResult MyProfileUserNameTweet(string userName,int ofset,int limit)
        {
            return Ok(_profileService.GetAllMyProfileTweet(userName, ofset, limit));
        }
    }
}
