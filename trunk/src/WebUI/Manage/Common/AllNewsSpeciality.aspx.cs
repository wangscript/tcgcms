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

using TCG.Utils;
using TCG.Handlers;
using TCG.Entity;



public partial class Common_AllNewsSpeciality : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string skinid = objectHandlers.Get("skinid");
            Response.ContentType = "application/x-javascript";
            string cg = base.handlerService.GetJsEntitys(base.handlerService.skinService.specialityHandlers.GetAllNewsSpecialityEntityBySkinId(skinid), typeof(Speciality));
            Response.Write(cg);
            Response.End();

        }
    }
}