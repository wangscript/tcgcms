﻿using System;
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

using TCG.Data;
using TCG.Handlers;
using TCG.Entity;

using TCG.KeyWordSplit;


public partial class news_createhtml : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.iSkinId.Value = base.configService.DefaultSkinId;

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
                case "Create":
                    this.Create();
                    break;
            }
        }
    }

    private void Create()
    {
        string ClassId = objectHandlers.Post("tClassId");
        string id = objectHandlers.Post("iId");
        string filepath = objectHandlers.Post("iFilePath");
        string Created = objectHandlers.Post("tCreated");

        Resources item = new Resources();
        item = base.handlerService.resourcsService.resourcesHandlers.GetResourcesById(id);

        string text1 = "";
        if (item == null)
        {
            text1 = "<a>读取文章信息失败...ID:" + id.ToString() + "</a>";
            base.AjaxErch(text1);
            return;
        }
       
        item.cCreated = "Y";
        item.vcTitle = objectHandlers.ToDBC(objectHandlers.GetTextWithoutHtml(item.vcTitle));
        if (string.IsNullOrEmpty(item.vcShortContent))
        {
            item.vcShortContent = objectHandlers.Left(objectHandlers.JSEncode(objectHandlers.GetTextWithoutHtml(item.vcContent)), 100);
        }
        //如果没有关键字，自动分词
        if (string.IsNullOrEmpty(item.vcKeyWord))
        {
            item.vcKeyWord = KeyWordTree.FindKeyWord(item.vcTitle, ",");
        }

        int rtn = base.handlerService.resourcsService.resourcesHandlers.UpdateResources(item);
        if (rtn < 0)
        {
            text1 = "<a>更新文章信息失败...ID:" + item.Id + "</a>";
            base.AjaxErch(text1);
            return;
        }

        TCGTagHandlers tcgthl = base.tagService.TCGTagHandlers;
        tcgthl.Template = item.Categorie.ResourceTemplate.Content.Replace("_$Id$_", id.ToString());
        tcgthl.FilePath = Server.MapPath("~" + filepath);
        tcgthl.NeedCreate = base.configService.baseConfig["IsReWrite"] != "True" ? true : false;
        tcgthl.configService = base.configService;
        tcgthl.conn = base.conn;
        tcgthl.Replace();

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

    }

    private void Search()
    {

        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "Resources";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("iId");
        arrshowfied.Add("iClassId");
        arrshowfied.Add("cCreated");
        arrshowfied.Add("vcFilePath");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("iId DESC");
        sItem.arrSortField = arrsortfield;

        sItem.page = objectHandlers.ToInt(objectHandlers.Post("page"));
        sItem.pageSize = objectHandlers.ToInt(base.configService.baseConfig["PageSize"]);

        sItem.strCondition = "cChecked='Y'";

        int create = objectHandlers.ToInt(objectHandlers.Post("Creat"));
        if (create == 2)
        {
            sItem.strCondition += " AND cCreated = 'N'";
        }

        int iStypeCheck = objectHandlers.ToInt(objectHandlers.Post("StypeCheck"));
        if (iStypeCheck == 1)
        {
            DateTime dStartTime = objectHandlers.ToTime(objectHandlers.Post("iStartTime"));
            DateTime dEndTime = objectHandlers.ToTime(objectHandlers.Post("iEndTime"));
            int iTimeType = objectHandlers.ToInt(objectHandlers.Post("iTimeFeild"));
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
            string iClassId = objectHandlers.Post("iClassId");

            //待续,查询分类
           // sItem.strCondition += " AND iClassID in (" + allchild + ")";
        }

        string Condition = objectHandlers.Post("iCondition");
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
            return;
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
                        text += text1 + "{Id:\"" + Row["iID"].ToString() + "\",ClassId:" + Row["iClassId"].ToString() + ",Created:\"" +
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
