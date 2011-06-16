/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using TCG.Utils;
using TCG.Entity;

namespace TCG.CMS.WebUi
{
    public partial class Interface_aspx_resources : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                string work = objectHandlers.Get("w");
                switch (work)
                {
                    case "getresourcelist":
                        this.GetResourceList();
                        break;
                    case "getresourceproperties":
                        this.GetResourceProperties();
                        break;
                    case "getresourcecount":
                        this.GetResourceCount();
                        break;
                }
            }
        }

        private void GetResourceProperties()
        {
            string rid = objectHandlers.Get("nid");
            Response.ContentType = "application/x-javascript";
            string cg = base.handlerService.GetJsEntitys(base.handlerService.resourcsService.resourcesHandlers.GetResourcePropertiesByRIdEntity(rid), typeof(ResourceProperties));
            Response.Write(cg);
            Response.End();
        }



        private void GetResourceList()
        {
            int page = objectHandlers.ToInt(objectHandlers.Get("page"));
            int pageSize = objectHandlers.ToInt(objectHandlers.Get("PageSize"));


            if (pageSize == 0)
            {
                pageSize = objectHandlers.ToInt(ConfigServiceEx.baseConfig["PageSize"]);
            }
            string iClassId = objectHandlers.Get("iClassId");
            if (string.IsNullOrEmpty(iClassId)) iClassId = "0";

            string skinId = objectHandlers.Get("SkinId");
            if (string.IsNullOrEmpty(skinId))
            {
                skinId = ConfigServiceEx.DefaultSkinId;
            }

            string allchild = string.Empty;

            if (iClassId == "0")
            {
                allchild = base.handlerService.skinService.categoriesHandlers.GetAllCategoriesIndexBySkinId(skinId);
            }
            else
            {
                allchild = base.handlerService.skinService.categoriesHandlers.GetCategoriesChild(iClassId);
            }

            string strCondition = "iClassID in (" + allchild + ")";


            string keywords = objectHandlers.Get("keywords");
            if (!string.IsNullOrEmpty(keywords))
            {
                strCondition += " AND vcTitle like '%" + keywords + "%'";
            }

            strCondition += " AND cDel ='N' AND cChecked='Y' AND cCreated = 'Y'";


            string speciality = objectHandlers.Get("sp");


            if (!string.IsNullOrEmpty(speciality))
            {
                if (speciality.IndexOf(",") > -1)
                {

                    string[] aaa = speciality.Split(',');
                    string text323 = string.Empty;
                    for (int n = 0; n < aaa.Length; n++)
                    {
                        if (!string.IsNullOrEmpty(aaa[n]))
                        {
                            string text = n == 0 ? "" : " OR ";
                            text323 += text + " vcSpeciality like '%" + aaa[n] + "%'";
                        }
                    }
                    if (!string.IsNullOrEmpty(text323))
                    {
                        strCondition += " AND (";
                        strCondition += text323;
                        strCondition += " )";
                    }
                }
                else
                {
                    strCondition += " AND vcSpeciality like '%" + speciality + "%' ";
                }
            }


            int curPage = 0;
            int pageCount = 0;
            int count = 0;


            Dictionary<string, EntityBase> res = null;
            try
            {
                res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curPage, ref pageCount, ref count,
                       page, pageSize, "Id DESC", strCondition);
            }
            catch (Exception ex)
            {

            }

            Response.ContentType = "application/x-javascript";
            string cg = base.handlerService.GetJsEntitys(res, typeof(Resources));
            Response.Write(cg);
            Response.Write("var curPage=" + curPage.ToString() + ";");
            Response.Write("var pageCount=" + pageCount.ToString() + ";");
            Response.Write("var count=" + count.ToString() + ";");
            Response.Write("var pageSize=" + pageSize.ToString() + ";");
            Response.Write("var bkeywords='" + keywords + "';");
            Response.End();
        }

        private void GetResourceCount()
        {
            int rid = objectHandlers.ToInt(objectHandlers.Get("rid"));
            int count = 0;
            if (rid > 0)
            {
                Resources res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesById(rid);
                if (res != null)
                {
                    count = res.iCount + 1;
                    res.iCount = count;
                    base.handlerService.resourcsService.resourcesHandlers.UpdateResources(res);
                }
            }

            Response.ContentType = "application/x-javascript";
            Response.Write("document.write(" + count + ");");
            Response.End();
        }
    }
}