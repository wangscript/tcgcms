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
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;

namespace TCG.URLRewriter
{
    /// <summary>
    /// 定义地址重写或对单个文件进行可控制操作的接口
    /// </summary>
    public class HttpWriter : IHttpModule
    {
        /// <summary>
        /// 释放重载
        /// </summary>
        public void Dispose() { }
        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="Application">HttpApplication对象</param>
        public void Init(HttpApplication Application)
        {
            Application.BeginRequest += new EventHandler(this.BeginRequest);
            Application.EndRequest += new EventHandler(this.EndRequest);
            Application.Error += new EventHandler(this.Error);
        }
        /// <summary>
        /// 设置通用的信息
        /// </summary>
        /// <param name="sender">sender对象</param>
        /// <param name="e">EventArgs e</param>
        private void BeginRequest(object sender, EventArgs e)
        {
            HttpApplication Application = (HttpApplication)sender;
            HttpContext Context = Application.Context;
            HttpRequest Request = Application.Request;
            HttpResponse Response = Application.Response;
            //判断是否需要使用地址重写
            //HttpModuleRoles.LoadHttpRoles();
            HttpModuleRoles.OverrideUrl(Application, Context);
        }
        /// <summary>
        /// 结束请求
        /// </summary>
        /// <param name="sender">sender对象</param>
        /// <param name="e">EventArgs e</param>
        private void EndRequest(object sender, EventArgs e) { }
        /// <summary>
        /// 错误请求
        /// </summary>
        /// <param name="sender">object Object</param>
        /// <param name="e">EventArgs e</param>
        private void Error(object sender, EventArgs e) { this.Dispose(); }
    };
}
