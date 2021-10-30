using Business.Abstract;
using Repository.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Concrete;

namespace Business.Concrete
{
    public class LikeManager : EntityManagerBase<Like, ILikeRepository>, ILikeService
    {
        public LikeManager(ILikeRepository likeRepository) : base(likeRepository) { }

        public List<Like> Toggle(Like like)
        {
            return _repositoryBase.Toggle(like);
        }
    }
}
