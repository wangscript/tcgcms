using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TCG.Handlers;
using TCG.Entity;

namespace TCG.Handlers.Imp.MsSql
{
    public class FeedBackHandlers : IFeedBackHandlers
    {

        public DataTable GeFeedBackListPager(ref int curPage, ref int pageCount, ref int count, int page, int pagesize, string order, string strCondition)
        {
            return MsSqlFactory.conn.ExecutePager(curPage, pagesize, " * ", "feedback", strCondition, order, out pageCount, out count);
        }


        public int DelFeedBackById(string ids)
        {
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText,"DELETE FROM FeedBack WHERE id in (" + ids + ")");
        }

        public int CreateFeedBack(FeedBack feedback)
        {
            string Sql = "INSERT INTO FeedBack(TITLE,Email,UserName,Tel,QQ ,Content ,Ip ,SkinId )"
                    + "VALUES('" + feedback.Title + "','" + feedback.Email + "','" + feedback.UserName + "','" + feedback.Tel + "','" + feedback.QQ + "','" 
                    + feedback.Content + "','" + feedback.Ip + "','" + feedback.SkinId + "')";
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, Sql);
        }
    }
}
