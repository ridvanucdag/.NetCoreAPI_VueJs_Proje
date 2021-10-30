using Business.Abstract;
using Business.JWT;
using Repository.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Business.Concrete
{
    public class AuthenticationManager : IAuthenticationService
    {
        private IUserRepository _userDal;
        private IJwtAuthenticationManager jwtAuthenticationManager;
        public AuthenticationManager(IUserRepository userDal, IJwtAuthenticationManager _jwtAuthenticationManager)
        {
            _userDal = userDal;
            jwtAuthenticationManager = _jwtAuthenticationManager;
        }

        public User Register(User user)
        {
            user.Password = BCryptNet.HashPassword(user.Password);
            return  _userDal.Add(user);
        }

        public JwtResponse Login(JwtRequest jwtRequest)
        {
            return jwtAuthenticationManager.CreateToken(jwtRequest); 
        }

        public bool IsNickUsed(User user)
        {
            return _userDal.IsNickUsed(user);
        }
        public bool IsMail(string mail)
        {
            return _userDal.IsMail(mail);
        }
        public bool ResetPassword(string mail,string newPassword)
        {
            User user = _userDal.GetByMail(mail);
            if (user != null)
            {
                user.Password = BCryptNet.HashPassword(newPassword);
            }
            _userDal.UpdatePassword(user);
            return _userDal.UpdatePassword(user) != null ? true :false;
        }
    }
}
