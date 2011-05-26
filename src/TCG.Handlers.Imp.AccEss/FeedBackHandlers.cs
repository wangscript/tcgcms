using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TCG.Handlers;

namespace TCG.Handlers.Imp.AccEss
{
    public class FeedBackHandlers : IFeedBackHandlers
    {

        public DataTable GeFeedBackListPager(ref int curPage, ref int pageCount, ref int count, int page, int pagesize, string order, string strCondition)
        {
            return AccessFactory.conn.ExecutePager(curPage, pagesize, " * ", "feedback", strCondition, order, out pageCount, out count);
        }
    }
}
