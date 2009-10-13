using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

namespace TCG.URLRewriter
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpModule : IHttpModule
    {
        public void Dispose()
        {
        }

        private HttpWorkerRequest GetWorkerRequest()
        {
            //获取隐含的 HttpWorkerRequest
            return (HttpWorkerRequest)((IServiceProvider)HttpContext.Current).GetService(typeof(HttpWorkerRequest));
        }

        public void Init(HttpApplication iHttpApplication)
        {
            //初始化信息
            iHttpApplication.BeginRequest += new EventHandler(this.MicroSoft_Web_HttpUpload_BeginRequest);
            iHttpApplication.EndRequest += new EventHandler(this.MicroSoft_Web_HttpUpload_EndRequest);
            iHttpApplication.Error += new EventHandler(this.MicroSoft_Web_HttpUpload_Error);
        }

        private void MicroSoft_Web_HttpUpload_BeginRequest(object Object, EventArgs e)
        {
            //设置通用的信息
            HttpApplication iHttpApplication = Object as HttpApplication;
            HttpContext Context = iHttpApplication.Context;
            HttpRequest Request = iHttpApplication.Request;
            HttpResponse Response = iHttpApplication.Response;

            //判断是否需要使用地址重写
            HttpRequestHandler hrhl = new HttpRequestHandler();
            hrhl.Do(iHttpApplication, Context, Response, Request);
            hrhl = null;
        }

        private void MicroSoft_Web_HttpUpload_EndRequest(object sender, EventArgs e)
        {
        }

        private void MicroSoft_Web_HttpUpload_Error(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
