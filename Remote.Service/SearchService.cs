using System;
using System.Collections.Generic;
using EnjoyShare.Framework.Models;
using Remote.Interface;
using Remote.Model;

namespace Remote.Service
{
    public class SearchService : ISearch
    {
        public PageResult<BookModel> QueryCommodityPage(int pageIndex, int pageSize, string keyword, List<int> regionId, string priceFilter,
            string priceOrderBy)
        {
            //todo 调用搜索服务
            throw new NotImplementedException();
        }
    }
}
