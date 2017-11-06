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
            string result = null;
            try
            {
                using (var client = new RemoteSearchService.SearcherClient())
                {
                     result = client.QueryProduct(keyword);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<PageResult<BookModel>>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return new PageResult<BookModel>();
            }
        }
    }
}
