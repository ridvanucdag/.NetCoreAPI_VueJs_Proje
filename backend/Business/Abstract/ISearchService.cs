using Entities.DTO;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ISearchService
    {
        List<SearchDetail> Search(string search, int ofset, int limit);
    }
}
