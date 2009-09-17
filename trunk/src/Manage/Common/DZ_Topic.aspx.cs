using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TCG.Utils;
using TCG.Data;
using TCG.Template.Handlers;
using TCG.Scripts.Kernel;


using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Aggregation;


public partial class Common_DZ_Topic : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string LoadType = Fetch.Get("LoadType");

            if (string.IsNullOrEmpty(LoadType)) return;

            switch (LoadType)
            {
                case "TOPIC":
                    this.LoadTopic();
                    break;
                case "BLOG":
                    this.loadBlog();
                    break;
            }

            conn.Close();

        }
    }


    private void loadBlog()
    {
        string Type = Fetch.Get("Type");
        conn.Dblink = DBLinkNums.News;
        string SQL = string.Empty;

        if (Type == "1")
        {
            /// <summary>
            /// 推荐空间列表
            /// </summary>
            SpaceConfigInfoExt[] spaceconfigs = AggregationFacade.SpaceAggregation.GetSpaceListFromFile("Spaceindex");

            string text = "";
            foreach (SpaceConfigInfoExt ext in spaceconfigs)
            {
                if (ext.Spacepic != "")
                {
                    
                    text += "<ul class=\"blog\">";
                    text += "    <a href=\"/bbs/space/?uid=" + ext.Userid.ToString() + "\" class=\"blog_img_link\" target=\"_blank\"><img src=\"" + ext.Spacepic + "\" /></a>";
                    text += "   <li>" + ext.Spacetitle + " </li>";
                    text += "   <li><a href=\"/bbs/space/?uid=" + ext.Userid.ToString() + "\" class=\"clickbolg\" target=\"_blank\">点击进入博客</a></li>";
                    text += "</ul>";
                }
            }
            Response.Write(text);
        }
        else if (Type == "2")
        {
            SQL = "SELECT TOP 10 * FROM dbo.dnt_spaceposts ORDER BY postdatetime DESC";
            DataTable dtt = conn.GetDataTable(SQL);
            if (dtt != null)
            {
                if (dtt.Rows.Count > 0)
                {

                    string text2 = "";
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        DataRow Row = dtt.Rows[i];

                        text2 += "<li><a href=\"/bbs/space/viewspacepost.aspx?postid=" + Row["postid"].ToString() + "\" class=\"title\" target=\"_blank\">" + Row["title"].ToString()
                            + "</a><a href=\"/bbs/space/viewspacepost.aspx?postid=" + Row["postid"].ToString() + "\" class=\"name\" target=\"_blank\">[" + Row["author"].ToString() + "]</a></li>";

                    }
                    Response.Write(text2);
                }
            }
        }
    }

    private void LoadTopic()
    {
        string TopicType = Fetch.Get("TopicType");
        if (string.IsNullOrEmpty(TopicType)) return;

        conn.Dblink = DBLinkNums.News;
        string SQL = string.Empty;
        if (TopicType == "1")
        {
            SQL = "SELECT TOP 10 * FROM dnt_topics (NOLOCK) ORDER BY postdatetime DESC";
        }
        else if (TopicType == "2")
        {
            SQL = "SELECT TOP 10 * FROM dnt_topics (NOLOCK) ORDER BY views DESC";
        }
        else if (TopicType == "3")
        {
            SQL = "SELECT TOP 10 * FROM dnt_topics (NOLOCK) ORDER BY replies DESC";
        }

        DataTable dt = conn.GetDataTable(SQL);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {

                string text = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow Row = dt.Rows[i];
                    text += "<li><a href=\"/bbs/showtopic-" + Row["tid"].ToString() + ".aspx\" target=\"_blank\">" + Row["title"].ToString() + "</a></li>";
                }
                
                Response.Write(text);
            }
        }
    }
}
