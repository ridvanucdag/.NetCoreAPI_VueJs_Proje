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
    public class LikeController : ControllerBase
    {

        private ILikeService _likeService;
        private UserDetail _userDetail;
        public LikeController(ILikeService likeService, UserDetail userDetail)
        {
            _likeService = likeService;
            _userDetail = userDetail;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (_likeService.Get(id) != null)
                return Ok(_likeService.Get(id));
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_likeService.GetAll());
        }

        [HttpPost]
        public List<Like> Post([FromBody] Like like)
        {
            like.UserID = _userDetail.GetLoggedInUserId(HttpContext); ;
            return _likeService.Toggle(like);
        }

    }
}
