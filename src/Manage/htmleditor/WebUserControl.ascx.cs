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

public partial class htmleditor_WebUserControl : System.Web.UI.UserControl
{

    /// <summary>
    /// ±à¼­Æ÷µÄÄÚÈÝ
    /// </summary>
    public string Value
    {
        get { return HtmlEditorContent.Value; }
        set { HtmlEditorContent.Value = value; }
    }


    public Config config { get { return this._config; } }
    private Config _config = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        this._config = new Config();
    }
}
