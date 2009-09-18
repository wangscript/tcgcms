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
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Pages;
using TCG.Manage.Utils;

public partial class AjaxMethod_Text_GetErrText : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string ErrCode = Fetch.Get("ErrCode", CheckGetEnum.Safety);
            if (string.IsNullOrEmpty(ErrCode))
            {
                base.AjaxErch("错误代码请求错误！");
                base.Finish();
                return;
            }

            if (ErrCode == "1")
            {
                base.AjaxErch("操作成功！");
                base.Finish();
                return;
            }

            string ErrTexts = TxtReader.Read(ConfigurationManager.ConnectionStrings["ErrTxtPath"].ToString());
            string errText = "";
            if (!string.IsNullOrEmpty(ErrTexts))
            {
                string patten = ErrCode.ToString() + @"@@@([A-Z_a-z|]+)@@@([^-\r\n]+)";
                Match mt = Regex.Match(ErrTexts, patten, RegexOptions.Singleline);
                if (mt.Success)
                {
                    errText = mt.Result("$2");
                }
                mt = null;
            }

            base.AjaxErch(errText);
            base.Finish();
            return;
        }
    }
}