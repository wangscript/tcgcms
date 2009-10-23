﻿using System;
using System.Data;
using System.IO;
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

public partial class news_newsAdd : adminMain
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int newsid = objectHandlers.ToInt(objectHandlers.Get("newsid"));
            if (newsid == 0)
            {
                int ClassId = objectHandlers.ToInt(objectHandlers.Get("iClassId"));
                this.iClassId.Value = ClassId.ToString();
                this.iSpeciality.Value = "0";
            }
            else
            {
                NewsInfo item = base.handlerService.newsInfoHandlers.GetNewsInfoById(base.conn, newsid);
                this.iClassId.Value = item.ClassInfo.iId.ToString();
                this.iFrom.Value = item.FromInfo.iId.ToString();
                this.iTitle.Value = item.vcTitle;
                this.iUrl.Value = item.vcUrl;
                this.iKeyWords.Value = item.vcKeyWord;
                this.iContent.Value = item.vcContent;
                this.iAuthor.Value = item.vcAuthor;
                this.work.Value = "MdyNews";
                this.iNewsId.Value = item.iId.ToString();
                this.iSpeciality.Value = item.vcSpeciality;
                this.iSmallImg.Value = item.vcSmallImg;
                this.iBigImg.Value = item.vcBigImg;
                this.iTitleColor.Value = item.vcTitleColor;
                this.iStrong.Checked = (item.cStrong == "Y") ? true : false;
                this.isContent.Value = item.vcShortContent;
                item = null;

            }
        }
        else
        {
            string work = objectHandlers.Post("work");
            bool ismdy = false;
            switch (work)
            {
                case "AddNew":
                    ismdy = false;
                    break;
                case "MdyNews":
                    ismdy = true;
                    break;
                case "KeyWordLoad":
                    this.KeyWordLoad();
                    break;
            }

            this.NewsManage(ismdy);
        }
    }

    private void KeyWordLoad()
    {
        if (KeyWordTree.Root.ChildList.Count == 0)
        {
            string path = objectHandlers.MapPath(ConfigurationManager.ConnectionStrings["CKeyWordsFile"].ToString());
            if (File.Exists(path))
            {
                StreamReader Reader = new StreamReader(path, Encoding.Default);
                List<KeyWordTreeNode> tmpRoot = KeyWordTree.Root.ChildList;

                while (Reader.Peek() != -1)
                {
                    string[] tmp = Reader.ReadLine().ToLower().Split('|');
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

    private void NewsManage(bool ismdy)
    {
        NewsInfo item = base.handlerService.newsInfoHandlers.GetNewsInfoById(base.conn, objectHandlers.ToInt(objectHandlers.Post("iNewsId")));
        item.vcTitle = objectHandlers.Post("iTitle");
        item.vcUrl = objectHandlers.Post("iUrl");
        item.vcContent = objectHandlers.Post("iContent$content");
        item.vcAuthor = objectHandlers.Post("iAuthor");
        item.vcKeyWord = objectHandlers.Post("iKeyWords");
        item.ClassInfo = new NewsClassHandlers().GetClassInfoById(base.conn, objectHandlers.ToInt(objectHandlers.Post("iClassId")),false);
        item.FromInfo.iId = objectHandlers.ToInt(objectHandlers.Post("iFrom"));
        item.vcSpeciality = objectHandlers.Post("iSpeciality");
        item.vcBigImg = objectHandlers.Post("iBigImg");
        item.vcSmallImg = objectHandlers.Post("iSmallImg");
        item.vcTitleColor = objectHandlers.Post("sTitleColor");
        item.cStrong = objectHandlers.Post("iStrong");
        item.vcShortContent = objectHandlers.Post("isContent");

        if (string.IsNullOrEmpty(item.vcTitle))
        {
            base.AjaxErch("-1000000039");
            base.Finish();
            return;
        }

        if (string.IsNullOrEmpty(item.vcKeyWord))
        {
            base.AjaxErch("-1000000043");
            base.Finish();
            return;
        }

        if (item.FromInfo.iId == 0)
        {
            base.AjaxErch("-1000000042");
            base.Finish();
            return;
        }
        if (item.ClassInfo.iId == 0)
        {
            base.AjaxErch("-1000000056");
            base.Finish();
            return;
        }

        item.cChecked = "Y";
        item.cCreated = "Y";
        item.vcEditor = base.adminInfo.vcAdminName;
        int newid = 0; string filepath = "";
        int rtn = 0;
        if (!ismdy)
        {
            rtn = base.handlerService.newsInfoHandlers.AddNewsInfo(base.conn, base.configService.baseConfig["FileExtension"], item, ref newid);
        }
        else
        {
            rtn = base.handlerService.newsInfoHandlers.UpdateNewsInfo(base.conn, base.configService.baseConfig["FileExtension"], item, ref newid);
        }

        item.iId = newid;
        filepath = Server.MapPath("~" + item.vcFilePath);
        if (rtn == 1)
        {
            if (base.configService.baseConfig["IsReWrite"] != "True")
            {
                ClassInfo cif = base.handlerService.newsClassHandlers.GetClassInfoById(base.conn, item.ClassInfo.iId, false);

                TemplateInfo titem = base.handlerService.templateHandlers.GetTemplateInfoByID(base.conn, cif.iTemplate,false);
                cif = null;

                TCGTagHandlers tcgth = base.handlerService.TCGTagHandlers;
                tcgth.Template = titem.vcContent.Replace("_$Id$_", item.iId.ToString());
                titem = null;
                tcgth.FilePath = filepath;
                tcgth.Replace(base.conn, base.configService.baseConfig);
                tcgth = null;
            }

        }
        base.AjaxErch(rtn.ToString());
        base.Finish();
        item = null;

    }
}