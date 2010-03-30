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
using TCG.Pages;

using TCG.Handlers;
using TCG.Entity;
using TCG.Data;

public partial class Template_templatemdy : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string templateid = objectHandlers.Get("templateid");
        
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

            Template item = base.handlerService.skinService.templateHandlers.GetTemplateByID(templateid);
            if (item == null)
            {
                base.Finish();
                return;
            }

            this.vcTempName.Value = item.vcTempName;
            this.vcContent.Value = item.Content;
            this.vcUrl.Value = item.vcUrl;
            this.iSiteId.Value = item.SkinId.ToString();
            this.iParentid.Value = item.iParentId.ToString();
            this.SytemType.Value = item.iSystemType.ToString();

            foreach (Option option in base.configService.templateTypes.Values)            
            {
                this.tType.Items.Add(new ListItem(option.Text, option.Value));
                int i = objectHandlers.ToInt(option.Value);
                if (i == (int)item.TemplateType)
                {
                    this.tType.SelectedIndex = i;
                }
            }
            item = null;
            base.Finish();
        }
        else
        {

            Template item = new Template();
            item.vcTempName = objectHandlers.Post("vcTempName");

            item.TemplateType = objectHandlers.GetTemplateType(objectHandlers.ToInt(this.tType.Value));
            item.vcUrl = objectHandlers.Post("vcUrl");
            item.Content = objectHandlers.Post("vcContent");
            item.SkinId = objectHandlers.Post("iSiteId");
            item.iParentId = objectHandlers.Post("iParentId");

            if (string.IsNullOrEmpty(item.iParentId)) item.iParentId = "0";

            if (string.IsNullOrEmpty(item.vcTempName) || string.IsNullOrEmpty(item.Content))
            {
                base.ajaxdata = "{state:false,message:\"模板内容和名称不能为空！\"}";
                base.AjaxErch(base.ajaxdata);
                base.Finish();
                return;
            }

            if ((int)item.TemplateType == 0 && string.IsNullOrEmpty(item.vcUrl))
            {
                base.ajaxdata = "{state:false,message:\"" + errHandlers.GetErrTextByErrCode(-1000000024, base.configService.baseConfig["ManagePath"]) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;

            }
            item.Id = templateid;

            ///执行修改操作
            int rtn = 0;
            try
            {
                rtn = base.handlerService.skinService.templateHandlers.MdyTemplate(item,base.adminInfo);
                CachingService.Remove(CachingService.CACHING_All_TEMPLATES);
                CachingService.Remove(CachingService.CACHING_All_TEMPLATES_ENTITY);
            }
            catch (Exception ex)
            {
                base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }
            if (rtn < 0)
            {
                base.AjaxErch("{state:false,message:'" + errHandlers.GetErrTextByErrCode(rtn, base.configService.baseConfig["ManagePath"]) + "'}");
            }
            else
            {

                string filepath = string.Empty;
                filepath = item.vcUrl.IndexOf(".") > -1 ? item.vcUrl : item.vcUrl + base.configService.baseConfig["FileExtension"];
                try
                {
                    filepath = Server.MapPath("~" + filepath);
                    TCGTagHandlers tcgthdl = base.tagService.TCGTagHandlers;
                    tcgthdl.Template = item.Content;
                    tcgthdl.FilePath = filepath;
                    tcgthdl.configService = base.configService;
                    tcgthdl.conn = base.conn;
                    tcgthdl.Replace();
                }
                catch (Exception ex)
                {
                    base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                    base.AjaxErch(base.ajaxdata);
                    return;
                }

                base.AjaxErch("{state:true,message:'模板修改成功！'}");
            }
            

        }
    }
}
