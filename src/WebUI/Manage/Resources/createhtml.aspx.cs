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
                case "CreateClassList":
                    this.CreateClassList();
                    break;
                case "CreateSingeTemplate" :
                    this.CreateSingeTemplate();
                    break;
            }
        }
    }

    private void CreateSingeTemplate()
    {
        string iTemplate = objectHandlers.Post("tClassId");
        string text = string.Empty;
        int rtn = 0;
        try
        {
            rtn = base.handlerService.skinService.templateHandlers.CreateSingeTemplateToHtml(iTemplate, base.tagService.TCGTagHandlers, ref text);
        }
        catch (Exception ex)
        {
            base.AjaxErch(1, "<a>" + ex.Message.ToString() + "</a>", "CreateBack2");
            base.Finish();
            return;
        }

        base.AjaxErch(rtn, text, "CreateBack2");
    }

    private void CreateClassList()
    {
        string tClassID = objectHandlers.Post("tClassId");
        string text = string.Empty;
        int rtn = 0;
        try
        {
            rtn = base.handlerService.skinService.categoriesHandlers.CreateCategoriesListHtml(tClassID, base.tagService.TCGTagHandlers, ref text);
        }
        catch (Exception ex)
        {
            base.AjaxErch(1, "<a>" + ex.Message.ToString() + "</a>", "CreateBack1");
            base.Finish();
            return;
        }

        base.AjaxErch(rtn, text, "CreateBack1");
    }

    private void Create()
    {
        string ClassId = objectHandlers.Post("tClassId");
        string id = objectHandlers.Post("iId");
        string filepath = objectHandlers.Post("iFilePath");
        string Created = objectHandlers.Post("tCreated");

        Resources item = new Resources();
        string text1 = "";

        try
        {
            item = base.handlerService.resourcsService.resourcesHandlers.GetResourcesById(id);

            if (item == null)
            {
                text1 = "<a>读取文章信息失败...ID:" + id.ToString() + "</a>";
            }
            else
            {

                if (!string.IsNullOrEmpty(item.vcUrl))
                {
                    text1 = "<a>页面资源为跳转资源不需要生成...ID:" + item.Id + "</a>";
                }
                else
                {

                    if (item.cCreated != "Y" || Created == "Y")
                    {
                        item.cCreated = "Y";
                        int rtn = base.handlerService.resourcsService.resourcesHandlers.UpdateResources(item);
                        if (rtn < 0)
                        {
                            text1 = "<a>更新文章信息失败...ID:" + item.Id + "</a>";
                        }
                        else
                        {
                            TCGTagHandlers tcgthl = base.tagService.TCGTagHandlers;
                            tcgthl.Template = item.Categorie.ResourceTemplate.Content.Replace("_$Id$_", id.ToString());
                            tcgthl.FilePath = Server.MapPath("~" + filepath);
                            tcgthl.configService = base.configService;
                            tcgthl.conn = base.conn;
                            if (tcgthl.Replace())
                            {
                                text1 = "<a href='" + filepath + "' target='_blank'>生成成功:" + filepath + "...</a>";
                            }
                            else
                            {
                                text1 = "<a><font color='red'>生成失败:" + filepath + "...</font></a>";
                            }
                        }
                    }
                    else
                    {
                        text1 = "<a><font color='green'>文章编号...ID:" + item.Id + " 已经生成，无须再生成！</font></a>";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            text1 = "<a><font color='red'>d" + objectHandlers.JSEncode(ex.Message.ToString()) + "</font></a>";
        }

        base.AjaxErch(1, text1, "CreateBack");
        base.Finish();
    }

    private void Search()
    {
        Dictionary<string, EntityBase> res = null;
        string strCondition = string.Empty;
        int curPage = 0;
        int pageCount = 0;
        int count = 0;
        int page = objectHandlers.ToInt(objectHandlers.Post("page"));
        string text = "";

        try
        {
            strCondition = "cChecked='Y'";

            int create = objectHandlers.ToInt(objectHandlers.Post("Creat"));
            if (create == 2)
            {
                strCondition += " AND cCreated = 'N'";
            }

            int iStypeCheck = objectHandlers.ToInt(objectHandlers.Post("StypeCheck"));
            //根据时间搜索
            if (iStypeCheck == 1)
            {
                DateTime dStartTime = objectHandlers.ToTime(objectHandlers.Post("iStartTime"));
                DateTime dEndTime = objectHandlers.ToTime(objectHandlers.Post("iEndTime"));
                int iTimeType = objectHandlers.ToInt(objectHandlers.Post("iTimeFeild"));
                if (iTimeType == 1)
                {
                    strCondition += " AND (dAddDate BETWEEN '" + dStartTime.ToString() + "' AND '" + dEndTime.ToString() + "')";
                }
                else
                {
                    strCondition += " AND (dUpdateDate BETWEEN '" + dStartTime.ToString() + "' AND '" + dEndTime.ToString() + "')";
                }
            }
            else if (iStypeCheck == 2)
            {
                string iClassId = objectHandlers.Post("iClassId");

                strCondition += " AND iClassID = '" + iClassId + "')";
            }

            string Condition = objectHandlers.Post("iCondition");
            if (!string.IsNullOrEmpty(Condition))
            {
                strCondition += " AND " + Condition;
            }

            res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curPage, ref pageCount, ref count,
                page, objectHandlers.ToInt(base.configService.baseConfig["PageSize"]), "iId DESC", strCondition);
  
        }
        catch (Exception ex)
        {
            base.AjaxErch(1, objectHandlers.JSEncode(ex.Message.ToString()));
            return;
        }

        if (pageCount < page)
        {
            base.AjaxErch(1, "已经生成了所有分页！");
            base.Finish();
            return;
        }

        if (res != null)
        {
            foreach (KeyValuePair<string, EntityBase> entity in res)
            {
                Resources restemp = (Resources)entity.Value;
                string text1 = (text == "") ? "" : ",";
                text += text1 + "{Id:'" + restemp.Id + "',ClassId:'" + restemp.Categorie.Id + "',Created:'" +
                    restemp.cCreated + "',FilePath:'" + objectHandlers.JSEncode(restemp.vcFilePath) + "',Url:'" + objectHandlers.JSEncode(restemp.vcUrl) + "'}";
            }

            base.AjaxErch(1, text, "SearchBack");
            base.Finish();
            return;
        }

        base.Finish();
        base.AjaxErch(1,"搜索完成！");
    }
}
