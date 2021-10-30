using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class UserDetail
    {
       
        public int GetLoggedInUserId(HttpContext httpContext)
        {
            return Convert.ToInt32(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        //public int GetUsername(UsersController usersController)
        //{
        //    if(usersController.User.)
        //}

    }
}
