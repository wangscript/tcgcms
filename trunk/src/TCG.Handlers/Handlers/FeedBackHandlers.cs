using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers
{
    public class FeedBackHandlers
    {
        public Dictionary<string, EntityBase> GeFeedBackListPager(ref int curPage, ref int pageCount, ref int count, int page, int pagesize, string order, string strCondition)
        {

            Dictionary<string, EntityBase> res = null;

            DataTable dt = DataBaseFactory.feedBackHandlers.GeFeedBackListPager(ref curPage, ref pageCount, ref count, page, pagesize, order, strCondition);

            if (dt != null && dt.Rows.Count > 0)
            {
                res = HandlersFactory.GetEntitysObjectFromTable(dt, typeof(FeedBack));
            }

            return res;
        }
    }
}
