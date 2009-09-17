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
