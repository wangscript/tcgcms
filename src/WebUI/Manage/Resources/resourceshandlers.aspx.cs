using System;
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


using TCG.Data;
using TCG.Handlers;
using TCG.Entity;


public partial class resources_resourceshandlers : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();
        this.ltfileclassid.Text = ConfigServiceEx.baseConfig["NewsFileClass"];
        if (!Page.IsPostBack)
        {
            int newsid = objectHandlers.ToInt(objectHandlers.Get("newsid"));
            string categorieid = objectHandlers.Get("iClassId");
            this.iSkinId.Value = ConfigServiceEx.DefaultSkinId;

            if (newsid==0)
            {
                this.iClassId.Value = categorieid;
                this.iSpeciality.Value = "0";
                return;
            }

            Resources item = base.handlerService.resourcsService.resourcesHandlers.GetResourcesById(newsid);
            this.iClassId.Value = item.Categorie.Id.ToString();

            this.iTitle.Value = item.vcTitle;
            this.iUrl.Value = item.vcUrl;
            this.iKeyWords.Value = item.vcKeyWord;
            this.iContent.Value = item.vcContent;
            this.iAuthor.Value = item.vcAuthor;
            this.work.Value = "MdyNews";
            this.iNewsId.Value = item.Id.ToString();
            this.iSpeciality.Value = item.vcSpeciality;
            this.iSmallImg.Value = item.vcSmallImg;
            this.iBigImg.Value = item.vcBigImg;
            this.iTitleColor.Value = item.vcTitleColor;
            this.iStrong.Checked = (item.cStrong == "Y") ? true : false;
            this.iShortContent.Value = item.vcShortContent;
            item = null;


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
            }

            this.NewsManage(ismdy);
        }
    }

    private void NewsManage(bool ismdy)
    {
        string categorieid = objectHandlers.Post("iClassId");
        Resources item = new Resources();
        if (ismdy)
        {
            item = base.handlerService.resourcsService.resourcesHandlers.GetResourcesById(objectHandlers.ToInt(objectHandlers.Post("iNewsId")));
        }

        item.vcTitle = objectHandlers.Post("iTitle");
        item.vcUrl = objectHandlers.Post("iUrl");
        item.vcContent = iContent.Value;// objectHandlers.Post("iContent$content");
        item.vcAuthor = objectHandlers.Post("iAuthor");
        item.vcKeyWord = objectHandlers.Post("iKeyWords");

        if (item.Categorie.Id != categorieid)
        {
            item.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(categorieid);
        }

        item.vcSpeciality = objectHandlers.Post("iSpeciality");
        item.vcBigImg = objectHandlers.Post("iBigImg");
        item.vcSmallImg = objectHandlers.Post("iSmallImg");
        item.vcTitleColor = objectHandlers.Post("sTitleColor");
        item.cStrong = objectHandlers.Post("iStrong");
        item.vcShortContent = objectHandlers.Post("iShortContent");

        if (string.IsNullOrEmpty(item.vcTitle))
        {
            base.AjaxErch(-1000000039,"");
            return;
        }

        if (string.IsNullOrEmpty(item.vcKeyWord))
        {
            base.AjaxErch(-1000000043,"");
            return;
        }


        if (string.IsNullOrEmpty(item.Categorie.Id))
        {
            base.AjaxErch(-1000000056,"");
            return;
        }

        item.cChecked = "Y";
        item.cCreated = "Y";
        item.vcEditor = base.adminInfo.vcAdminName;

        string filepath = "";
        string errText = string.Empty;
        int rtn = 0;

        try
        {
            if (!ismdy)
            {
                rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(item);
            }
            else
            {
                rtn = base.handlerService.resourcsService.resourcesHandlers.UpdateResources(item);
            }

            rtn = base.handlerService.tagService.CreateResourcHtmlById(ref errText, objectHandlers.ToInt(item.Id));
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        if (ismdy)
        {
            base.AjaxErch(rtn, "文章修改成功！");
        }
        else
        {
            base.AjaxErch(rtn, "文章添加成功，请继续添加！", "NewsAddPostBack()");
        }
    }
}