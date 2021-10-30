using Business.Abstract;
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
    public class MailController : ControllerBase
    {
        private IMailService mailService;
        private IAuthenticationService authService;
        public MailController(IMailService mailService, IAuthenticationService authService)
        {
            this.mailService = mailService;
            this.authService = authService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendMail(string mail)
        {
            try
            {
                if (authService.IsMail(mail))
                {
                    await mailService.SendEmailAsync(mail);
                    return Ok();
                }
                return BadRequest("Mail adresi sistemde kayıtlı değildir");

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
