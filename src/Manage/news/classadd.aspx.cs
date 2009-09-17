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
using TCG.Controls.HtmlControls;
using TCG.Manage.Kernel;
using TCG.Template.Handlers;
using TCG.Template.Utils;
using TCG.Manage.Utils;
using TCG.News.Handlers;

public partial class news_classadd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.Init();
        }
        else
        {
            string classname = Fetch.Post("iClassName");
            string lname = Fetch.Post("iName");
            string dir = Fetch.Post("iDirectory");
            string url = Fetch.Post("iUrl");
            int parentid = Bases.ToInt(Fetch.Post("iClassId"));
            int sTemplate = (int)Convert.ChangeType(Fetch.Post("sTemplate"),typeof(int));
            int slTemplate = (int)Convert.ChangeType(Fetch.Post("slTemplate"), typeof(int));
            int order = Bases.ToInt(Fetch.Post("iOrder"));
            if (string.IsNullOrEmpty(classname) || string.IsNullOrEmpty(lname))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            if (parentid != 0)
            {
                if (sTemplate < 0 || slTemplate < 0)
                {
                    base.AjaxErch("-1");
                    base.Finish();
                    return;
                }
            }

            classHandlers chdl = new classHandlers();
            int rtn = chdl.AddNewsClass(base.conn, base.admin.adminInfo.vcAdminName, classname, lname, parentid, sTemplate, slTemplate, dir, url, order);
            Caching.Remove("AllNewsClass");
            base.AjaxErch(rtn.ToString());
            chdl = null;
            base.Finish();
        }
    }

    private void Init()
    {
        int iParent = Bases.ToInt(Fetch.Get("iParentId"));
        this.iClassId.Value = iParent.ToString();
        TemplateHandlers nthdl = new TemplateHandlers();
        DataSet ds = nthdl.GetTemplatesBySystemTypAndType(base.conn, TemplateConstant.InfoType, TemplateConstant.SystemType_News,false);
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow Row = ds.Tables[0].Rows[i];
                    this.sTemplate.Items.Add(new ListItem(Row["vcTempName"].ToString(), Row["iId"].ToString()));
                }
            }
            
            ds.Clear();
        }

        
        ds = nthdl.GetTemplatesBySystemTypAndType(base.conn, TemplateConstant.ListType, TemplateConstant.SystemType_News,false);
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow Row = ds.Tables[0].Rows[i];
                    this.slTemplate.Items.Add(new ListItem(Row["vcTempName"].ToString(), Row["iId"].ToString()));
                }
            }
        }
    }
}
