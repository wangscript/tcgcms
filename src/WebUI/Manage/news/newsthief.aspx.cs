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

using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Handlers;
using TCG.Controls.HtmlControls;
using TCG.Pages;
using TCG.Manage.Utils;
using TCG.Data;
using TCG.Handlers;
using TCG.Entity;
using TCG.Files.Utils;


public partial class news_newsthief : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            string vwork = Fetch.Post("work");
            switch (vwork)
            {
                case "Search":
                    this.Search();
                    break;
                case "InsertTopic":
                    this.InsertTopic();
                    break;
            }
        }
    }

    private void Search()
    {
        string str = "";
        string ListPage = Fetch.Post("iListPage");
        int spage = Bases.ToInt(Fetch.Post("iListPageStart"));
        int epage = Bases.ToInt(Fetch.Post("iListPageEnd"));

        string ListArea = Fetch.Post("iListArea");
        string ListLink = Fetch.Post("iListLink");

        string iCharSet = Fetch.Post("iCharSet");

        if (string.IsNullOrEmpty(iCharSet))
        {
            iCharSet = "gb2312";
        }

        if (spage > epage)
        {
            base.AjaxErch("");
            base.Finish();
            return;
        }

        for (int i = spage; i < epage + 1; i++)
        {
            string ListPageHtml = TxtReader.GetRequestText(string.Format(ListPage, i.ToString()), iCharSet);

            Match item = Regex.Match(ListPageHtml, ListArea, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (item.Success)
            {
                MatchCollection matchs = Regex.Matches(item.Value, ListLink, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (matchs.Count > 0)
                {
                    foreach (Match m in matchs)
                    {
                        string txt1 = (str == "") ? "" : ",";
                        str += txt1 + "{WebPath:'" + m.Result("$1") + "'}";
                    }
                }
            }
            
        }


        base.AjaxErch(str);
        base.Finish();
    }

    private void InsertTopic()
    {
        string iCharSet = Fetch.Post("iCharSet");

        if (string.IsNullOrEmpty(iCharSet))
        {
            iCharSet = "gb2312";
        }

        string TopicWebPath = Fetch.Post("iTopicWebPath");
        string ListPage = Fetch.Post("iListPage");

        TopicWebPath = TxtReader.GetFileWebPath(ListPage, TopicWebPath);
        string TopicArea = Fetch.Post("iTopicArea");

        string ListPageHtml = TxtReader.GetRequestText(TopicWebPath, iCharSet);
        NewsInfoHandlers nihdl = new NewsInfoHandlers();

        Match item = Regex.Match(ListPageHtml, TopicArea, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        if (item.Success)
        {
            NewsInfo nif = new NewsInfo();
            FileInfoHandlers fihdl = new FileInfoHandlers();
            nif.vcTitle = item.Result("$1");
            nif.vcAuthor = base.admin.adminInfo.vcNickName;
            nif.vcKeyWord = nif.vcTitle;
            nif.ClassInfo = new NewsClassHandlers().GetClassInfoById(base.conn, Bases.ToInt(Fetch.Post("iClassId")), false);
            nif.FromInfo.iId = 1;
            nif.cChecked = "Y";
            nif.cCreated = "Y";
            nif.vcEditor = base.admin.adminInfo.vcAdminName;

            if (nihdl.CheckThiefTopic(base.conn, nif.ClassInfo.iId, nif.vcTitle) == 1)
            {
                nif.vcContent = fihdl.ImgPatchInit(base.conn, item.Result("$2"), TopicWebPath,
                    base.admin.adminInfo.vcAdminName, objectHandlers.ToInt(base.configService.baseConfig["NewsFileClass"]), base.configService.baseConfig);

                nif.vcShortContent = Text.Left(Text.GetTextWithoutHtml(nif.vcContent), 200, false);

                int newid = 0; string filepath = "";
                int rtn = 0;
                try
                {
                    rtn = nihdl.AddNewsInfoForSheif(base.conn, base.configService.baseConfig["FileExtension"], nif, ref newid);
                }
                catch { }
            }
            
            nif = null;

        }
        nihdl = null;
        item = null;
        base.AjaxErch("<a>正在抓取:" + TopicWebPath + "</a>");
        base.Finish();
        return;
    }
}
