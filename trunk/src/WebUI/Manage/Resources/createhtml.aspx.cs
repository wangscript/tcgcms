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

using TCG.Handlers;
using TCG.Entity;

namespace TCG.CMS.WebUi
{
    public partial class news_createhtml : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.iSkinId.Value = ConfigServiceEx.DefaultSkinId;

            if (Page.IsPostBack)
            {
                //检测管理员登录
                base.handlerService.manageService.adminHandlers.CheckAdminLogin();

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
                    case "CreateSingeTemplate":
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
                rtn = base.handlerService.tagService.CreateSingeTemplateToHtml(iTemplate, ref text);
            }
            catch (Exception ex)
            {
                base.AjaxErch(1, "<a>" + ex.Message.ToString() + "</a>", "CreateBack2");
                return;
            }

            base.AjaxErch(1, text, "CreateBack2");
        }

        private void CreateClassList()
        {
            string tClassID = objectHandlers.Post("tClassId");
            string text = string.Empty;
            int page = objectHandlers.ToInt(objectHandlers.Post("page"));
            int rtn = 0;
            int pagecount = 1;
            try
            {
                rtn = base.handlerService.tagService.CreateClassList(tClassID, page, ref pagecount, ref text);
            }
            catch (Exception ex)
            {
                base.AjaxErch(1, "<a>" + ex.Message.ToString() + "</a>", "CreateBack3({page:1,message:'<a>数据操作错误，请联系开发人员</a>'})");
                return;
            }

            base.AjaxErch(1, text, "CreateBack3({page:" + pagecount + ",message:'<a>" + text + "</a>'})");
        }

        private void Create()
        {
            string ClassId = objectHandlers.Post("tClassId");
            int id = objectHandlers.ToInt(objectHandlers.Post("iId"));
            string filepath = objectHandlers.Post("iFilePath");
            string Created = objectHandlers.Post("tCreated");

            Resources item = new Resources();
            string text1 = "";

            int rtn = 0;
            try
            {
                rtn = base.handlerService.tagService.CreateResourcHtmlById(ref text1, id);
                text1 = "<a href='" + filepath + "' target='_blank'>生成成功:" + filepath + "...</a>";
            }
            catch (Exception ex)
            {
                text1 = "<a><font color='red'>" + objectHandlers.JSEncode(ex.Message.ToString()) + "</font></a>";
            }

            base.AjaxErch(1, "<a>" + text1 + "</a>", "CreateBack");
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
                        strCondition += " AND (dAddDate BETWEEN " + objectHandlers.GetDataTimeSqlStr(dStartTime.ToString()) + " AND " + objectHandlers.GetDataTimeSqlStr(dEndTime.ToString()) + ")";
                    }
                    else
                    {
                        strCondition += " AND (dUpdateDate BETWEEN " + objectHandlers.GetDataTimeSqlStr(dStartTime.ToString()) + " AND " + objectHandlers.GetDataTimeSqlStr(dEndTime.ToString()) + ")";
                    }
                }
                else if (iStypeCheck == 2)
                {
                    string iClassId = objectHandlers.Post("iClassId");

                    strCondition += " AND iClassID = '" + iClassId + "' ";
                }

                string Condition = objectHandlers.Post("iCondition");
                if (!string.IsNullOrEmpty(Condition))
                {
                    strCondition += " AND " + Condition;
                }

                res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curPage, ref pageCount, ref count,
                    page, objectHandlers.ToInt(ConfigServiceEx.baseConfig["PageSize"]), "Id DESC", strCondition);

            }
            catch (Exception ex)
            {
                base.AjaxErch(1, objectHandlers.JSEncode(ex.Message.ToString()));
                return;
            }

            if (pageCount < page)
            {
                base.AjaxErch(1, "已经生成了所有分页！开始生成列表", "PageInit()");
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
                return;
            }

            base.AjaxErch(1, "搜索完成！");
        }
    }
}