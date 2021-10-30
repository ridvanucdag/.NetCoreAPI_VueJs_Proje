using Business.Abstract;
using Entities.DTO;
using Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProfileManager : IProfileService
    {
        private IUserRepository _userRepository;
        private ITweetRepository _tweetRepository;
        public ProfileManager(ITweetRepository tweetRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _tweetRepository = tweetRepository;

        }

        public List<ProfileDetail> GetAllMyProfileTweet(string userName, int ofset, int limit)
        {
            return _tweetRepository.GetAllMyProfileTweet(userName, ofset, limit);
        }

        public ProfileDetail GetAllMyProfileUsername(string userName)
        {
            return _userRepository.GetAllMyProfileUsername(userName);
        }
    }
}
