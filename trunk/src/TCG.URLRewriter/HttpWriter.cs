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
    /// �����ַ��д��Ե����ļ����пɿ��Ʋ����Ľӿ�
    /// </summary>
    public class HttpWriter : IHttpModule
    {
        /// <summary>
        /// �ͷ�����
        /// </summary>
        public void Dispose() { }
        /// <summary>
        /// ��ʼ����Ϣ
        /// </summary>
        /// <param name="Application">HttpApplication����</param>
        public void Init(HttpApplication Application)
        {
            Application.BeginRequest += new EventHandler(this.BeginRequest);
            Application.EndRequest += new EventHandler(this.EndRequest);
            Application.Error += new EventHandler(this.Error);
        }
        /// <summary>
        /// ����ͨ�õ���Ϣ
        /// </summary>
        /// <param name="sender">sender����</param>
        /// <param name="e">EventArgs e</param>
        private void BeginRequest(object sender, EventArgs e)
        {
            HttpApplication Application = (HttpApplication)sender;
            HttpContext Context = Application.Context;
            HttpRequest Request = Application.Request;
            HttpResponse Response = Application.Response;
            //�ж��Ƿ���Ҫʹ�õ�ַ��д
            //HttpModuleRoles.LoadHttpRoles();
            HttpModuleRoles.OverrideUrl(Application, Context);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender">sender����</param>
        /// <param name="e">EventArgs e</param>
        private void EndRequest(object sender, EventArgs e) { }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender">object Object</param>
        /// <param name="e">EventArgs e</param>
        private void Error(object sender, EventArgs e) { this.Dispose(); }
    };
}
