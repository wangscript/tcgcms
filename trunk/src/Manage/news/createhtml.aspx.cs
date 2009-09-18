using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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
using TCG.Controls.HtmlControls;
using TCG.Pages;
using TCG.Manage.Utils;
using TCG.Data;
using TCG.News.Handlers;
using TCG.News.Entity;

using TCG.Template.Entity;
using TCG.Template.Handlers;
using TCG.Template.Utils;
using TCG.TCGTagReader.Handlers;
using TCG.KeyWordSplit;


public partial class news_createhtml : adminMain
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
                case "Create":
                    this.Create();
                    break;
                case "KeyWordLoad":
                    this.KeyWordLoad();
                    break;
            }
        }
    }

    private void KeyWordLoad()
    {
        if (KeyWordTree.Root.ChildList.Count == 0)
        {
            string path = Fetch.MapPath(ConfigurationManager.ConnectionStrings["CKeyWordsFile"].ToString());
            if (File.Exists(path))
            {
                StreamReader Reader = new StreamReader(path, Encoding.Default);
                List<KeyWordTreeNode> tmpRoot = KeyWordTree.Root.ChildList;

                while (Reader.Peek() != -1)
                {
                    string[] tmp = Reader.ReadLine().Split('|');
                    KeyWordTree.AddKeyWord(tmp[0]);

                }

                Reader.Close();
                Reader.Dispose();

                if (KeyWordTree.Root.ChildList.Count == 0)
                {
                    base.Finish();
                    base.AjaxErch("-1000000070");
                    return;
                }
            }
            else
            {
                base.Finish();
                base.AjaxErch("-1000000070");
                return;
            }
        }

        base.Finish();
        base.AjaxErch("加载关键词成功！");
    }

    private void Create()
    {
        int ClassId = Bases.ToInt(Fetch.Post("tClassId"));
        int id = Bases.ToInt(Fetch.Post("iId"));
        string filepath = Fetch.Post("iFilePath");
        string Created = Fetch.Post("tCreated");

        classHandlers chdl = new classHandlers();
        ClassInfo cif = chdl.GetClassInfoById(base.conn, ClassId, false);
        chdl = null;
        if (cif == null) { base.AjaxErch(""); return; }
        TemplateHandlers tlhd = new TemplateHandlers();
        TemplateInfo tif = tlhd.GetTemplateInfoByID(base.conn, cif.iTemplate);
        if (tif == null) { base.AjaxErch(""); return; }
        tlhd = null;

        newsInfoHandlers nihdl = new newsInfoHandlers();
        NewsInfo item = new NewsInfo();
        item = nihdl.GetNewsInfoById(base.conn, id);


        string text1 = "";
        if (item == null)
        {
            text1 = "<a>读取文章信息失败...ID:" + id.ToString() + "</a>";
            base.AjaxErch(text1);
            return;
        }
       
        item.cCreated = "Y";
        item.vcTitle = Text.ToDBC(Text.GetTextWithoutHtml(item.vcTitle));
        item.vcShortContent = Text.Left(Text.JSEncode(Text.GetTextWithoutHtml(item.vcContent)), 100, false);
        item.vcKeyWord = KeyWordTree.FindKeyWord(item.vcTitle, ",");

        int rtn = nihdl.UpdateNewsInfo(base.conn, base.config["FileExtension"], item, ref id, ref filepath);
        if (rtn < 0)
        {
            text1 = "<a>更新文章信息失败...ID:" + id.ToString() + "</a>";
            base.AjaxErch(text1);
            return;
        }

        TCGTagHandlers tcgthl = new TCGTagHandlers();
        tcgthl.Template = tif.vcContent.Replace("_$Id$_", id.ToString());
        tcgthl.FilePath = Server.MapPath("~" + filepath);
        tcgthl.Replace(base.conn, base.config);

        if (tcgthl.PagerInfo.PageCount > 1)
        {

            for (int i = 1; i <= tcgthl.PagerInfo.PageCount; i++)
            {
                string num = (i == 1) ? "" : i.ToString();
                text1 += "<a href='.." + filepath + "' target='_blank'>生成成功:" + filepath.Substring(0, filepath.LastIndexOf(".")) + num +
                    filepath.Substring(filepath.LastIndexOf("."), filepath.Length - filepath.LastIndexOf(",")) + "...</a>";
            }
        }
        else
        {
            text1 = "<a href='.." + filepath + "' target='_blank'>生成成功:" + filepath + "...</a>";
        }

        base.Finish();
        base.AjaxErch(text1);
        tcgthl = null;
        cif = null;
        tif = null;
    }

    private void Search()
    {
        base.conn.Dblink = DBLinkNums.News;
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "T_News_NewsInfo";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("iId");
        arrshowfied.Add("iClassId");
        arrshowfied.Add("cCreated");
        arrshowfied.Add("vcFilePath");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("iId DESC");
        sItem.arrSortField = arrsortfield;

        sItem.page = Bases.ToInt(Fetch.Post("page"));
        sItem.pageSize = Bases.ToInt(base.config["PageSize"]);

        sItem.strCondition = "cChecked='Y'";

        int create = Bases.ToInt(Fetch.Post("Creat"));
        if (create == 2)
        {
            sItem.strCondition += " AND cCreated = 'N'";
        }

        int iStypeCheck = Bases.ToInt(Fetch.Post("StypeCheck"));
        if (iStypeCheck == 1)
        {
            DateTime dStartTime = Bases.ToTime(Fetch.Post("iStartTime"));
            DateTime dEndTime = Bases.ToTime(Fetch.Post("iEndTime"));
            int iTimeType = Bases.ToInt(Fetch.Post("iTimeFeild"));
            if (iTimeType == 1)
            {
                sItem.strCondition += " AND (dAddDate BETWEEN '" + dStartTime.ToString() + "' AND '" + dEndTime.ToString() + "')";
            }
            else
            {
                sItem.strCondition += " AND (dUpdateDate BETWEEN '" + dStartTime.ToString() + "' AND '" + dEndTime.ToString() + "')";
            }
        }
        else if (iStypeCheck == 2)
        {
            int iClassId = Bases.ToInt(Fetch.Post("iClassId"));
            classHandlers chdl = new classHandlers();
            string allchild = chdl.GetAllChildClassIdByClassId(base.conn, iClassId, false);
            chdl = null;
            sItem.strCondition += " AND iClassID in (" + allchild + ")";
        }

        string Condition = Fetch.Post("iCondition");
        if (!string.IsNullOrEmpty(Condition))
        {
            sItem.strCondition += " AND " + Condition;
        }

        int curPage = 0;
        int pageCount = 0;
        int count = 0;
        DataSet ds = new DataSet();
        int rtn = DBHandlers.GetPage(sItem, base.conn, ref curPage, ref pageCount, ref count, ref ds);
        if (rtn < 0)
        {
            this.Throw(rtn, null, true);
        }

        if (pageCount < sItem.page)
        {
            base.AjaxErch("");
            base.Finish();
            return;
        }

        if (ds != null)
        {
            if (ds.Tables.Count == 1)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string text = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow Row = ds.Tables[0].Rows[i];
                        string text1 = (text == "") ? "" : ",";
                        text += text1 + "{Id:" + Row["iID"].ToString() + ",ClassId:" + Row["iClassId"].ToString() + ",Created:\"" +
                            Row["cCreated"].ToString() + "\",FilePath:\"" + Row["vcFilePath"].ToString() + "\"}";
                    }
                    base.AjaxErch(text);
                    base.Finish();
                    return;
                }
            }
        }

        base.Finish();
        base.AjaxErch("");
    }
}
