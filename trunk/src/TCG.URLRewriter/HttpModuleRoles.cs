using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Data;


namespace TCG.URLRewriter
{
    /// <summary>
    /// 配置重写设置的操作类
    /// </summary>
    public class HttpModuleRoles
    {
        /// <summary>
        /// 加载所有缓存
        /// </summary>
        //public static void LoadHttpRoles() { LoadingAllCaches.Loading(Based.Skey_LoadingCache); }
        /// <summary>
        /// 直接指向地址重写
        /// </summary>
        /// <param name="Application">HttpApplication 对象</param>
        /// <param name="Context">HttpContext 对象</param>
        /// <returns>bool</returns>
        public static bool OverrideUrl(HttpApplication Application, HttpContext Context)
        {
            LoadingRewrites LoadingRewrite = new LoadingRewrites();
            return LoadingRewrite.IsRewriteUrl(Application, Context);
        }
    }
}
