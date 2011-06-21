using System;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Controls.HtmlControls;


using TCG.Data;
using TCG.Handlers;
using TCG.Entity;

using TCG.Data;
using TCG.DBHelper;

namespace TCG.CMS.WebUi
{
    public partial class taobaodate : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                string work = objectHandlers.Post("work");
                switch (work)
                {
                    case "AddNew":

                        break;
                }
            }
            else
            {
                //IConnection _conn = new TCG.DBHelper.AccEss();
                //_conn.SetConnStr(HttpContext.Current.Server.MapPath("/database/taobao.csv"));

                //DataTable dt = _conn.DataTable("select * from HN_window where win_type='媒体聚焦' and win_zt<>10000 and win_check=10015 order by win_send_time ");

                //string strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties='text;HDR=Yes;FMT=Delimited';Data Source=" + HttpContext.Current.Server.MapPath("/database/"));
                //OleDbConnection conn = new OleDbConnection(strConn);
                //DataTable dt1 = new DataTable();
                //string sql = "select * from taobao.csv";
                //try
                //{
                //    conn.Open();
                //    OleDbDataAdapter dr = new OleDbDataAdapter(sql, conn);
                //    DataSet ds = new DataSet();
                //    dr.Fill(ds, "taobao");
                //    dt1 = ds.Tables["taobao"];

                //    if (dt1 != null && dt1.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dt1.Rows.Count; i++)
                //        {
                //            DataRow row =  dt1.Rows[i];
                //            Response.Write(row[2].ToString() + " - " + "<br/>");
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                //finally
                //{
                //    conn.Close();
                //}

                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("/database/taobao.csv"), FileMode.Open, FileAccess.Read);
                //FileStream fs = new FileStream("c:\\sample.xls", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string t = sr.ReadToEnd();

                string text = "<table border=\"0\" cellpadding=\"1\" cellspacing=\"1\" bgcolor=\"#006600\" width=\"4770\" >";
                string[] arr = t.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                for (int i = 0; i < arr.Length; i++)
                {

                    string[] arr1 = arr[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                    if (i < 15)
                    {
                        text += "<tr>";

                        string ext = "bgcolor=\"#FFFFFF\"";
                        if (i == 0)
                        {
                            ext = "bgcolor=\"#009900\"";
                            for (int n = 0; n < arr1.Length; n++)
                            {
                                string text2 =  "(" + n.ToString() + ")" + arr1[n];
                                text += "<td width=\"100\"  height=\"25\" " + ext + ">" + text2 + "</td>";

                            }
                        }
                        else
                        {
                            for (int n = 0; n < arr1.Length; n++)
                            {
                                string text2 = (n == 24) ? "" : arr1[n];
                                text += "<td width=\"100\"  height=\"25\" " + ext + ">" + text2 + "</td>";

                            }
                        }
                        text += "</tr>";

                    }

                    int curPage = 0;
                    int pageCount = 0;
                    int count = 0;

                    Dictionary<string, EntityBase> ress = null;
                    try
                    {
                        ress = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curPage, ref pageCount, ref count,
                               0, 15, "dAddDate DESC,ID DESC", "SheifUrl='"+arr1[2]+"'");
                    }
                    catch (Exception ex)
                    {

                    }

                    Resources res = new Resources();

                    if (ress != null && ress.Count == 1)
                    {

                    }
                    else
                    {

                        res.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                        res.vcAuthor = "-";
                        res.vcTitle = arr1[0];
                        res.vcUrl = "-";
                        res.vcKeyWord = arr1[0];
                        res.vcContent = arr1[24];
                        res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("1056a870-1366-44e5-94fa-e7a29209b8ee");
                        res.vcEditor = "-";
                        res.PropertiesCategorieId = 1;

                        res.vcSmallImg = "/filePatch/taobao/" + arr1[35].Split(';')[0].Split('|')[0].Split(':')[0] + ".jpg";

                        //int rtn = -1;
                        //rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(res);
                        //string txt = "";

                        //foreach (KeyValuePair<string, EntityBase> entity in ps)
                        //{
                        //    Properties ppti = (Properties)entity.Value;
                        //    ResourceProperties pp = new ResourceProperties();
                        //    pp.Id = "";
                        //    pp.PropertieId = objectHandlers.ToInt(ppti.Id);
                        //    pp.PropertieName = ppti.ProertieName;


                        //    if (pp.PropertieId == 22) //公　　司
                        //    {
                        //        pp.PropertieValue = row["prod_company"].ToString();
                        //    }
                        //    else if (pp.PropertieId == 23) //曾用名（别称）
                        //    {
                        //        pp.PropertieValue = row["prod_cname"].ToString();
                        //    }
                        //    else if (pp.PropertieId == 24) //英文名称
                        //    { pp.PropertieValue = row["prod_ename"].ToString(); }
                        //    else if (pp.PropertieId == 33) //汉语拼音
                        //    { pp.PropertieValue = row["prod_pname"].ToString(); }
                        //    else if (pp.PropertieId == 34) //说明书类别
                        //    { pp.PropertieValue = ""; }
                        //    else if (pp.PropertieId == 35) //产品介绍
                        //    { pp.PropertieValue = row["prod_intro"].ToString(); }
                        //    else if (pp.PropertieId == 36) //产品特点
                        //    { pp.PropertieValue = row["prod_td"].ToString(); }
                        //    else if (pp.PropertieId == 37) //成　　份
                        //    { pp.PropertieValue = row["prod_cf"].ToString(); }
                        //    else if (pp.PropertieId == 38) //性　　状
                        //    { pp.PropertieValue = row["prod_xz"].ToString(); }
                        //    else if (pp.PropertieId == 39) //适　应　症
                        //    { pp.PropertieValue = row["prod_syz"].ToString(); }
                        //    else if (pp.PropertieId == 40) //规　　格
                        //    { pp.PropertieValue = row["prod_gg"].ToString(); }
                        //    else if (pp.PropertieId == 41) //作用类别
                        //    { pp.PropertieValue = row["prod_zylb"].ToString(); }
                        //    else if (pp.PropertieId == 42) //功能主治
                        //    { pp.PropertieValue = row["prod_cure"].ToString(); }
                        //    else if (pp.PropertieId == 43) //用法用量
                        //    { pp.PropertieValue = row["prod_yfyl"].ToString(); }
                        //    else if (pp.PropertieId == 44) //不良反应
                        //    { pp.PropertieValue = row["prod_blfy"].ToString(); }
                        //    else if (pp.PropertieId == 45) //禁　　忌
                        //    { pp.PropertieValue = row["prod_jj"].ToString(); }
                        //    else if (pp.PropertieId == 46) //注意事项
                        //    { pp.PropertieValue = row["prod_zy"].ToString(); }
                        //    else if (pp.PropertieId == 47) //孕妇及哺乳期妇女用药
                        //    { pp.PropertieValue = row["prod_yun"].ToString(); }
                        //    else if (pp.PropertieId == 48) //儿童用药
                        //    { pp.PropertieValue = row["prod_child"].ToString(); }
                        //    else if (pp.PropertieId == 49) //老年用药
                        //    { pp.PropertieValue = row["prod_old"].ToString(); }
                        //    else if (pp.PropertieId == 50) //药物相互作用
                        //    { pp.PropertieValue = row["prod_xkzy"].ToString(); }
                        //    else if (pp.PropertieId == 51) //药物过量
                        //    { pp.PropertieValue = row["prod_ywgl"].ToString(); }
                        //    else if (pp.PropertieId == 52) //药理毒理
                        //    { pp.PropertieValue = row["prod_yldl"].ToString(); }
                        //    else if (pp.PropertieId == 53) //药代动力学
                        //    {
                        //        pp.PropertieValue = row["prod_yddl"].ToString(); 
                        //    }
                        //    else if (pp.PropertieId == 54) //贮　　藏
                        //    { pp.PropertieValue = row["prod_cc"].ToString(); }
                        //    else if (pp.PropertieId == 55) //包　　装
                        //    { pp.PropertieValue = row["prod_bz"].ToString(); }
                        //    else if (pp.PropertieId == 56) //有 效 期
                        //    { pp.PropertieValue = row["prod_yxq"].ToString(); }
                        //    else if (pp.PropertieId == 57) //执行标准
                        //    { pp.PropertieValue = row["prod_bzh"].ToString(); }
                        //    else if (pp.PropertieId == 58) //说明书修订日期
                        //    { pp.PropertieValue = row["prod_sms"].ToString(); }
                        //    else if (pp.PropertieId == 59) //批准文号
                        //    { pp.PropertieValue = row["prod_pzwh"].ToString(); }
                        //    else if (pp.PropertieId == 60) //社　保　类
                        //    {

                        //        //国保类|省保类|省保及国保|先不指定
                        //        string txt12 = row["prod_sb"].ToString();
                        //        if(txt12 =="111")
                        //        {
                        //            pp.PropertieValue = "省保及国保";
                        //        }  else if(txt12=="110")
                        //        {
                        //            pp.PropertieValue = "国保类";
                        //        } else if(txt12=="101")
                        //        {
                        //            pp.PropertieValue = "省保类";
                        //        } 

                        //    }
                        //    else if (pp.PropertieId == 61) //附加说名
                        //    { pp.PropertieValue = row["prod_yddl"].ToString(); }
                        //    else if (pp.PropertieId == 62) //药物作用
                        //    { pp.PropertieValue = row["prod_yl"].ToString(); }

                        //    if (string.IsNullOrEmpty(pp.PropertieValue)) continue;
                        //    pp.iOrder = ppti.iOrder;
                        //    pp.ResourceId = res.Id;

                        //    pp.PropertieValue = pp.PropertieValue.Replace("'", "''");

                        //    rtn = base.handlerService.resourcsService.resourcesHandlers.ResourcePropertiesManage(base.adminInfo, pp);
                        //    txt = rtn == 1 ? "属性[" + pp.PropertieName + "]添加成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                        //    Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;·" + txt + "<br/>");
                        //}

                        //txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                    }
                }

                text += "</table>";
                dataveiw.InnerHtml = text;
            }
        }
    }
}