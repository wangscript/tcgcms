/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using TCG.Utils;
using TCG.Entity;

public partial class Interface_aspx_feedback : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string work = objectHandlers.Get("w");
            switch (work)
            {
                case "createfeedback":
                    this.CreateFeedBack();
                    break;
            }
        }
    }

    private void CreateFeedBack()
    {
        string vcodie = objectHandlers.Post("vcodie");
        string vercoide = SessionState.Get("verification").ToString();

        if (vcodie.ToLower() != vercoide)
        {
            base.AjaxErch(-1000000900, "");
            return;
        }

        FeedBack feedback = new FeedBack();

        feedback.UserName = objectHandlers.Post("UserName");
        feedback.SkinId = objectHandlers.Post("SkinId");
        feedback.Tel = objectHandlers.Post("Tel");
    
        feedback.QQ = objectHandlers.Post("QQ");
        feedback.Content = objectHandlers.Post("Content");
        feedback.Title = objectHandlers.Post("Title");
        feedback.Email = objectHandlers.Post("Email");

        feedback.Ip = objectHandlers.GetIP();

        int rtn = 0;
        try
        {
            rtn = base.handlerService.feedBackHandlers.CreateFeedBack(feedback);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch(rtn, "您的留言添加成功！谢谢！");
    }
}