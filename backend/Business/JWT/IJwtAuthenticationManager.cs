using Business.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.JWT
{
    public interface IJwtAuthenticationManager
    {
        JwtResponse CreateToken(JwtRequest jwtRequest);
    }
}
