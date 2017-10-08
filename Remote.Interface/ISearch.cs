using System.Collections.Generic;
using EnjoyShare.Framework.Models;
using Remote.Model;

namespace Remote.Interface
{
    public interface ISearch
    {        /// <summary>
             /// 分页获取图书信息数据
             /// </summary>
             /// <param name="pageIndex"></param>
             /// <param name="pageSize"></param>
             /// <param name="keyword"></param>
             /// <param name="regionId">类别id的集合 可为null</param>
             /// <param name="priceFilter">[13,50]  13,50且包含13到50   {13,50}  13,50且不包含13到50</param>
             /// <param name="priceOrderBy">price desc   price asc</param>
             /// <returns>PageResult BookModel tojson</returns>
        PageResult<BookModel> QueryCommodityPage(int pageIndex, int pageSize, string keyword, List<int> regionId, string priceFilter, string priceOrderBy);
    }
}
