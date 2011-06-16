using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using TCG.Utils;

using TCG.Entity;

namespace TCG.CMS.WebUi
{
    public partial class Manage_AjaxMethod_PropertiesDel : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int id = objectHandlers.ToInt(objectHandlers.Get("id"));
            int rtn = 1;
            if (id > 0)
            {
                rtn = base.handlerService.skinService.propertiesHandlers.PropertiesDEL(base.adminInfo, id);
            }

            base.AjaxErch(1, "删除属性成功");
        }
    }
}