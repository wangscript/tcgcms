using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TCG.Handlers;
using TCG.Entity;

namespace TCG.Handlers.Imp.AccEss
{
    public class FeedBackHandlers : IFeedBackHandlers
    {

        public DataTable GeFeedBackListPager(ref int curPage, ref int pageCount, ref int count, int page, int pagesize, string order, string strCondition)
        {
            return AccessFactory.conn.ExecutePager(curPage, pagesize, " * ", "feedback", strCondition, order, out pageCount, out count);
        }


        public int DelFeedBackById(string ids)
        {
            AccessFactory.conn.DataTable("DELETE FROM FeedBack WHERE id in (" + ids + ")");
            return 1;
        }

        public int CreateFeedBack(FeedBack feedback)
        {
            AccessFactory.conn.Execute("INSERT INTO FeedBack(TITLE,Email,UserName,Tel,QQ ,Content ,Ip ,SkinId )"
                    + "VALUES('" + feedback.Title + "','" + feedback.Email + "','" + feedback.UserName + "','" + feedback.Tel + "','" + feedback.QQ + "','" 
                    + feedback.Content + "','" + feedback.Ip + "','" + feedback.SkinId + "')");
            return 1;
        }
    }
}
