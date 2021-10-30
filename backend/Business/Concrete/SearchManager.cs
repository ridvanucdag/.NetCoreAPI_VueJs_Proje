using AutoMapper;
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
    public class SearchManager : ISearchService
    {
        private ITweetRepository _tweetRepository;
        private IUserRepository _userRepository; 
        public SearchManager(ITweetRepository tweetRepository, IUserRepository userRepository)
        {
            _tweetRepository = tweetRepository;
            _userRepository = userRepository;

        }
        public List<SearchDetail> Search(string search, int ofset, int limit)
        {
            return _tweetRepository.Search(search, ofset, limit).Concat(_userRepository.Search(search, ofset, limit)).ToList();
        }

    }
}
