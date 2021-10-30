
using Business.Abstract;
using Entities.Concrete;
using Entities.DTO;
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
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private UserDetail _userDetail;
        public UsersController(IUserService userService, UserDetail userDetail)
        {
            _userService = userService;
            _userDetail = userDetail;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            int userId = _userDetail.GetLoggedInUserId(HttpContext);
            if (_userService.Get(userId) != null)
                return Ok(_userService.Get(userId));
            else
                return NotFound();
        }


        //[HttpGet("profile/{userName}")]
        //public IActionResult MyProfileUserName()
        //{
        //    int userName = _userDetail.GetUsername(usersController);
        //    if (_userService.Get(userName) != null)
        //        return Ok(_userService.Get(userName));
        //    else
        //        return NotFound();
        //}

        [HttpPut]
        public User Put([FromBody] User user)
        {
            user.ID = _userDetail.GetLoggedInUserId(HttpContext);
            return _userService.Update(user);
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            var userId = _userDetail.GetLoggedInUserId(HttpContext);
            var user = _userService.Get(userId);
            if (user != null)
                return Ok(_userService.Delete(userId));
            else
                return NotFound();
        }

    }
}
