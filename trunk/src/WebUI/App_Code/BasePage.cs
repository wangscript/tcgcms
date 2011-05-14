using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;

using TCG.Handlers;
using TCG.Utils;
using TCG.Entity;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
	public BasePage()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    /// <summary>
    /// 提供系统操作方法的服务
    /// </summary>
    protected HandlerService handlerService
    {
        get
        {
            if (this._handlerservice == null)
            {                                            
                this._handlerservice = new HandlerService();
            }
            return this._handlerservice;
        }
    }
    private HandlerService _handlerservice = null;

    protected void AjaxErch(string str)
    {
        Response.Write(str);
        Response.End();
    }

    /// <summary>
    /// 判断数据库执行参数
    /// </summary>
    /// <param name="rtn"></param>
    /// <param name="okmessage"></param>
    protected void AjaxErch(int rtn, string okmessage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{state:");
        if (rtn < 0)
        {
            sb.Append("false,message:\"" + errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]) + "\"");
        }
        else
        {
            sb.Append("true,message:'" + okmessage + "'");
        }
        sb.Append("}");
        Response.ContentType = "application/x-javascript";
        Response.Write(sb.ToString());
        Response.End();
    }

    protected void AjaxErch(int rtn, string okmessage, string callback)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{state:");
        if (rtn < 0)
        {
            sb.Append("false,message:\"" + errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]) + "\"");
        }
        else
        {
            sb.Append("true,message:\"" + okmessage + "\",callback:\"" + callback + "\"");
        }
        sb.Append("}");
        Response.Write(sb.ToString());
        Response.End();
    }

    /// <summary>
    /// AJAX输出字符串
    /// </summary>
    public string ajaxdata
    {
        get
        {
            return this._ajaxdata;
        }
        set
        {
            this._ajaxdata = value;
        }
    }

    private string _ajaxdata = string.Empty;

    protected Admin adminInfo
    {
        get
        {
            if (this._admininfo == null)
            {
                this._admininfo = this.handlerService.manageService.adminHandlers.GetAdminInfo();
                //this.handlerService.tagService.tcgTagHandlers.adminInfo = this._admininfo;
            }
            return this._admininfo;
        }
    }
    private Admin _admininfo = null;
}