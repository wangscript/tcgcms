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

using TCG.Data;
using TCG.Entity;


public partial class resources_resourcesthief : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

            string vwork = objectHandlers.Post("work");
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
        string ListPage = objectHandlers.Post("iListPage");
        int spage = objectHandlers.ToInt(objectHandlers.Post("iListPageStart"));
        int epage = objectHandlers.ToInt(objectHandlers.Post("iListPageEnd"));

        string ListArea = objectHandlers.Post("iListArea");
        string ListLink = objectHandlers.Post("iListLink");

        string iCharSet = objectHandlers.Post("iCharSet");

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
        string iCharSet = objectHandlers.Post("iCharSet");

        if (string.IsNullOrEmpty(iCharSet))
        {
            iCharSet = "gb2312";
        }

        string TopicWebPath = objectHandlers.Post("iTopicWebPath");
        string ListPage = objectHandlers.Post("iListPage");

        TopicWebPath = TxtReader.GetFileWebPath(ListPage, TopicWebPath);
        string TopicArea = objectHandlers.Post("iTopicArea");

        string ListPageHtml = TxtReader.GetRequestText(TopicWebPath, iCharSet);

        Match item = Regex.Match(ListPageHtml, TopicArea, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        if (item.Success)
        {
            Resources nif = new Resources();

            nif.vcTitle = item.Result("$1");
            nif.vcAuthor = base.adminInfo.vcNickName;
            nif.vcKeyWord = nif.vcTitle;
            nif.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(objectHandlers.Post("iClassId"));

            nif.cChecked = "Y";
            nif.cCreated = "Y";
            nif.vcEditor = base.adminInfo.vcAdminName;

            if (base.handlerService.resourcsService.resourcesHandlers.CheckThiefTopic(base.conn, nif.Categorie.Id, nif.vcTitle) == 1)
            {
                nif.vcContent = base.handlerService.fileService.fileInfoHandlers.ImgPatchInit(item.Result("$2"), TopicWebPath,
                    base.adminInfo.vcAdminName, objectHandlers.ToInt(base.configService.baseConfig["NewsFileClass"]));

                nif.vcShortContent = objectHandlers.Left(objectHandlers.GetTextWithoutHtml(nif.vcContent), 200);

                int newid = 0; string filepath = "";
                int rtn = 0;
                try
                {
                    //待续
                    //rtn = base.handlerService.resourcsService.resourcesHandlers.AddNewsInfoForSheif(base.conn, base.configService.baseConfig["FileExtension"], nif);
                }
                catch { }
            }
            
            nif = null;

        }

        item = null;
        base.AjaxErch("<a>正在抓取:" + TopicWebPath + "</a>");
        base.Finish();
        return;
    }
}
