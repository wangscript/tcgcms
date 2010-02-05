using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;


using TCG.Entity;
using TCG.Handlers;
using TCG.Utils;

namespace TCG.WebService
{
    /// <summary>
    ///SheifSource 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SheifSourceService : System.Web.Services.WebService
    {

        public SheifSourceService()
        {
            //如果使用设计的组件，请取消注释以下行 
            //InitializeComponent(); 
        }

        [WebMethod]
        public int AddSheifSource(SheifSourceInfo source)
        {

            SheifHandlers sheifhld = new SheifHandlers();
            sheifhld.conn = new TCG.Data.Connection();
            sheifhld.configService = new ConfigService();

            return sheifhld.AddShiefSource(source);
        }

        [WebMethod]
        public List<SheifSourceInfo> GetAllSheifSourceInfos()
        {
            SheifHandlers sheifhld = new SheifHandlers();
            sheifhld.conn = new TCG.Data.Connection();
            sheifhld.configService = new ConfigService();
            List<SheifSourceInfo> sources = null;
            int rtn = sheifhld.GetAllShieSourceInfo(ref sources);
            if (rtn < 0)
            {
                return null;
            }

            return sources;
        }
    }
}

