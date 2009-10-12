using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_Ctrl_Editor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    /// <summary>
    /// 文本内容属性
    /// </summary>
    public string Value
    {
        set
        {
            this.content.Value = value;
        }
        get
        {
            return this.content.Value;
        }
    }

}
