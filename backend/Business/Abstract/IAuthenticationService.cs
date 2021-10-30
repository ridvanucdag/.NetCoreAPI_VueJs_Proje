using Business.Concrete;
using Business.JWT;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthenticationService
    {
        bool IsNickUsed(User user);
        bool IsMail(string mail);
        User Register(User user);
        JwtResponse Login(JwtRequest jwtRequest);

        bool ResetPassword(string mail, string newPassword);
    }
}
