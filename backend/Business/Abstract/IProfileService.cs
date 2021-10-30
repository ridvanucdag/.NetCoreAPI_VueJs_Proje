using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProfileService
    {
        ProfileDetail GetAllMyProfileUsername(string userName);
        List<ProfileDetail> GetAllMyProfileTweet(string userName, int ofset, int limit);
    }
}
