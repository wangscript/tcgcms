using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TCG.Utils;
using TCG.Entity;
using TCG.Handlers;


public partial class Common_AllFileClass : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Write("var AllFileClass = new Array();\r\n");

             Dictionary<string, EntityBase> categories =base.handlerService.fileService.fileHandlers.GetAllFileCategoriesEntity();

             if (categories != null && categories.Count > 0)
             {
                 int i = 0;
                 foreach (KeyValuePair<string, EntityBase> entity in categories)
                 {
                     FileCategories tempfilecategories = (FileCategories)entity.Value;
                     Response.Write("AllFileClass[" + i.ToString() + "]=[" + tempfilecategories.Id + "," + tempfilecategories.iParentId + ",\"" +
                         tempfilecategories.vcFileName + "\",\"" + tempfilecategories.vcMeno + "\"];\r\n");
                     i++;
                 }
             }
        }
    }
}
