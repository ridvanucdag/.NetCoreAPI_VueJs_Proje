using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IUserRepository : IEntityRepositoryBase<User>
    {
        List<SearchDetail> Search(string search, int ofset, int limit);
        ProfileDetail GetAllMyProfileUsername(string userName);
        bool IsNickUsed(User user);
        bool IsMail(string mail);
        User GetByMail(string mail);
        User UpdatePassword(User entity);
    }
}


