using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using TCG.Entity;

namespace TCG.Handlers
{
    public interface IFeedBackHandlers
    {
        DataTable GeFeedBackListPager(ref int curPage, ref int pageCount, ref int count, int page, int pagesize, string order, string strCondition);

        int DelFeedBackById(string ids);

        int CreateFeedBack(FeedBack feedback);
    }
}
