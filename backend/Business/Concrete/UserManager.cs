using Business.Abstract;
using Repository.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : EntityManagerBase<User, IUserRepository>, IUserService
    {
        public UserManager(IUserRepository userRepository) : base(userRepository) { }

        public override User Update(User entity)
        {
            User user = this._repositoryBase.Get(entity.ID);
            entity.Password = user.Password;
            return _repositoryBase.Update(entity);
        }

    }
}
