using System;
using System.IO;
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
using TCG.Files.Entity;
using TCG.Files.Utils;
using TCG.Pages;

public partial class upload_uploadfile : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int iMaxNum = Bases.ToInt(Fetch.Get("MaxNum"));
            this.sMaxNum.Text = iMaxNum.ToString();

            string CallBack = Fetch.Get("CallBack");
            this.sCallBack.Text = CallBack;

            int ClassId = Bases.ToInt(Fetch.Get("ClassId"));
            this.iClassId.Value = ClassId.ToString();

            this.stype.Text = FilesConst.alowFileType;
        }
    }
}