using Business.Abstract;
using Business.JWT;
using Entities.Concrete;
using Entities.DTO;
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
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService authenticationService;
        private IUserService userService;
        public AuthenticationController(IAuthenticationService _authenticationService, IUserService userService)
        {
            authenticationService = _authenticationService;
            userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            var user = new User {
                Bio = registerRequest.Bio,
                Password = registerRequest.Password,
                Mail = registerRequest.Mail,
                Name = registerRequest.Name,
                SurName = registerRequest.SurName,
                Nick = registerRequest.Nick,
                Phone = registerRequest.Phone
            };
            var userNickControl = authenticationService.IsNickUsed(user);
            if (!userNickControl)
            {
                var userMailControl = authenticationService.IsMail(user.Mail);
                if (!userMailControl)
                    return Ok(authenticationService.Register(user));
                else
                    return BadRequest("Girdiğiniz mail adresi sistemimizde kayıtlıdır.");
            }
            else
                return BadRequest("Girdiğiniz kullanıcı adı daha önceden kullanılmıştır.Başka kullanıcı adı giriniz.");

        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] JwtRequest jwtRequest)
        {
            var response = authenticationService.Login(jwtRequest);
            if (response != null)
                return Ok(response);
            else
                return BadRequest("Kullanıcı bulunamadı.");
        }

        [HttpPost("resetPassword")]
        public IActionResult ResetPassword(string mail,string password)
        {
            var response = authenticationService.ResetPassword(mail,password);
            if (response)
                return Ok(response);
            else
                return BadRequest("Şifre değiştirilemedi");
        }

    }
}
