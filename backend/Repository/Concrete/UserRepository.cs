using Repository.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.DTO;
using Microsoft.EntityFrameworkCore;

namespace Repository.Concrete
{
    public class UserRepository : EntityRepositoryBase<User>, IUserRepository
    {
        public UserRepository(TwitterContext context) : base(context) { }

        public ProfileDetail GetAllMyProfileUsername(string userName)
        {
            var profileUsers = from u in _twitterContext.Users
                               where u.Nick == userName
                               select new ProfileDetail()
                               {
                                   User = u,
                               };
            return profileUsers.FirstOrDefault();
        }

        public List<SearchDetail> Search(string search, int ofset, int limit)
        {
            var users = from u in _twitterContext.Users.Where(u => u.Name.Contains(search) || u.SurName.Contains(search) || u.Nick.Contains(search))
                        select new SearchDetail()
                        {
                            ID = u.ID,
                            User = u,
                            Tweet = null,
                            Type = Enum.GetName(typeof(SearchType), SearchType.User)
                        };
            return users.Skip(ofset).Take(limit).ToList();
        }

        public bool IsNickUsed(User user)
        {
            return _twitterContext.Users.Where(u => u.Nick == user.Nick).FirstOrDefault() == null ? false : true;
        }

        public bool IsMail(string mail)
        {
            return _twitterContext.Users.Where(u => u.Mail == mail).FirstOrDefault() == null ? false : true;
        }

        public User GetByMail(string mail)
        {
            return _twitterContext.Users.Where(u => u.Mail == mail).FirstOrDefault();
        }
        public User UpdatePassword(User entity)
        {
            foreach (var e in _twitterContext.ChangeTracker.Entries())
            {
                e.State = EntityState.Detached;
            }
            _twitterContext.Set<User>().Update(entity);
            _twitterContext.SaveChanges();
            return entity;
        }
    }
}
