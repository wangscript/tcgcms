using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Threading;


using TCG.Handlers;

using TCG.Utils;
using TCG.Entity;
//using TCG.Release;

using TCG.Data;

using System.Reflection;
using System.ComponentModel;
using TCG.Data;
using TCG.DBHelper;

public partial class Test : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //IConnection _conn = new TCG.DBHelper.AccEss();
        //_conn.SetConnStr(HttpContext.Current.Server.MapPath("/database/#ZS_data.mdb"));

        //员工园地
        //DataTable dt = _conn.DataTable("select * from health where  H_zt<>10000 and H_check=10015 order by H_send_time");

        //媒体聚焦
        //DataTable dt = _conn.DataTable("select * from HN_window where win_type='媒体聚焦' and win_zt<>10000 and win_check=10015 order by win_send_time ");
        //公司动态
         //DataTable dt = _conn.DataTable("select * from HN_window where win_type='公司动态' and win_zt<>10000 and win_check=10015 order by win_send_time ");
        //行业动态
        //DataTable dt = _conn.DataTable("select * from medicine_info where  info_zt<>10000 and info_check=10015 order by info_send_time");

        //眼科用药
        //DataTable dt = _conn.DataTable("select * from dissertation where dissertation_f='消化系统' and dissertation_zt<>10000 and dissertation_pass=10015 order by dissertation_send_time");

        //临时公告
        //DataTable dt = _conn.DataTable("select * from HN_window where win_type='定期报告' and win_zt<>10000 and win_check=10015 order by win_send_time desc  ");

        //招聘信息
        //DataTable dt = _conn.DataTable("select * from job where recdr = 0 order by order_id  ");

        //产品变更公告
        //DataTable dt = _conn.DataTable("select * from proclaim where  zt<>10000 and check=10015 order by check_time desc ");
        //产品
        //DataTable dt = _conn.DataTable("select * from production where prod_company='广东众生药业股份有限公司' and prod_type='滴眼液'  order by prod_order,prod_id asc");
        //Dictionary<string, EntityBase> ps = base.handlerService.skinService.propertiesHandlers.GetPropertiesByCIdEntity("2");

        //文体
        //DataTable dt = _conn.DataTable("select * from culture where s_id=101 and RecDr = 0 order by id");

        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        DataRow row = dt.Rows[i];

        //        Resources res = new Resources();


                #region
                //员工园地
                
                //res.Id = (objectHandlers.ToInt(row["H_id"].ToString()) + 750).ToString();
                //res.vcAuthor = row["H_origin"].ToString();
                //res.vcTitle = row["H_title"].ToString();
                //res.vcKeyWord = row["H_title"].ToString();
                //res.dAddDate = objectHandlers.ToTime(row["H_date"].ToString());
                //res.vcContent = row["H_content"].ToString();
                //res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("39ce6488-bda9-4481-902a-5ae2c8626f49");
                //res.vcEditor = row["H_origin"].ToString();

                //int rtn = -1;
                //rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(res);

                //string txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                //Response.Write(row["H_title"].ToString() + "-----------" + txt + "<br/>");


                //媒体聚焦
                //res.Id = (objectHandlers.ToInt(row["win_id"].ToString()) + 0).ToString();
                //res.vcAuthor = row["win_origin"].ToString();
                //res.vcTitle = row["win_title"].ToString();
                //res.vcKeyWord = row["win_title"].ToString();
                //res.dAddDate = objectHandlers.ToTime(row["win_date"].ToString());
                //res.vcContent = row["win_content"].ToString();
                //res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("e0733fed-61df-4ced-b65e-601bc49ddaf7");
                //res.vcEditor = row["win_origin"].ToString();

                //int rtn = -1;
                //rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(res);

                //string txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                //Response.Write(row["win_title"].ToString() + "-----------" + txt + "<br/>");

                //公司动态
                //res.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                //res.vcAuthor = row["win_origin"].ToString();
                //res.vcTitle = row["win_title"].ToString();
                //res.vcKeyWord = row["win_title"].ToString();
                //res.dAddDate = objectHandlers.ToTime(row["win_send_time"].ToString());
                //res.dUpDateDate = objectHandlers.ToTime(row["win_send_time"].ToString()); ;
                //res.vcContent = row["win_content"].ToString();
                //res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("36fab2c8-a5f2-4e6b-a9ff-c514b3c51c69");
                //res.vcEditor = row["win_origin"].ToString();

                //int rtn = -1;
                //rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(res);

                //string txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                //Response.Write(row["win_title"].ToString() + "-----------" + txt + "<br/>");


                //行业动态
                //res.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                //res.vcAuthor = row["info_origin"].ToString();
                //res.vcTitle = row["info_title"].ToString();
                //res.vcKeyWord = row["info_title"].ToString();
                //res.dAddDate = objectHandlers.ToTime(row["info_send_time"].ToString());
                //res.dUpDateDate = objectHandlers.ToTime(row["info_send_time"].ToString());
                //res.vcContent = row["info_content"].ToString();
                //res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("a4264578-1723-46c3-86f6-86fa6b669e54");
                //res.vcEditor = row["info_origin"].ToString();

                //int rtn = -1;
                //rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(res);

                //string txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                //Response.Write(row["info_title"].ToString() + "-----------" + txt + "<br/>");

                //眼科用药
                //res.Id = (objectHandlers.ToInt(row["dissertation_id"].ToString()) + 1750).ToString();
                //res.vcAuthor = row["dissertation_origin"].ToString();
                //if (string.IsNullOrEmpty(row["dissertation_send_time"].ToString()))
                //{
                //    res.dAddDate = objectHandlers.ToTime(row["dissertation_time"].ToString());
                //} else{
                //    res.dAddDate = objectHandlers.ToTime(row["dissertation_send_time"].ToString());
                //}

                //res.dUpDateDate = res.dAddDate;
                //res.vcTitle = row["dissertation_title"].ToString();
                //res.vcKeyWord = row["dissertation_title"].ToString();
                
                //res.vcContent = row["dissertation_content"].ToString();
                //res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("f2aad798-225b-470c-8ea3-202a4bb09263");
                //res.vcEditor = row["dissertation_check"].ToString();

                //int rtn = -1;
                //rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(res);

                //string txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                //Response.Write(row["dissertation_title"].ToString() + "-----------" + txt + "<br/>");

                //临时公告
                //res.Id = (objectHandlers.ToInt(row["win_id"].ToString()) + 1750).ToString();
                //res.vcAuthor = row["win_origin"].ToString();
                //res.vcTitle = row["win_title"].ToString();
                //res.vcUrl = row["win_content"].ToString();
                //res.vcKeyWord = row["win_title"].ToString();
                //res.dAddDate = objectHandlers.ToTime(row["win_date"].ToString());
                //res.vcContent = row["win_content"].ToString();
                //res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("18d8de5a-9e61-4a45-abac-a8494009e4db");
                //res.vcEditor = row["win_check"].ToString();

                //int rtn = -1;
                //rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(res);

                //string txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                //Response.Write(row["win_title"].ToString() + "---" + row["win_content"].ToString() + "-----------" + txt + "<br/>");
                #endregion


                //产品变更公告
                //res.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                //res.vcAuthor = row["edit"].ToString();
                //res.vcTitle = row["title"].ToString();
                //res.vcKeyWord = row["title"].ToString();
                //res.dAddDate = objectHandlers.ToTime(row["send_time"].ToString());
                //res.dUpDateDate = objectHandlers.ToTime(row["send_time"].ToString());
                //res.vcContent = row["content"].ToString();
                //res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("1c339f2d-282b-47e4-a598-0735352a0995");
                //res.vcEditor = "-";

                //int rtn = -1;
                //rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(res);

                //string txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                //Response.Write(row["title"].ToString() + "-----------" + txt + "<br/>");

                #region//招聘信息
                //res.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                //res.vcAuthor = "-";
                //res.vcTitle = row["position"].ToString();
                //res.vcUrl = "-";
                //res.vcKeyWord =  row["position"].ToString();
                //res.vcContent = "-";
                //res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("67eed636-a078-4568-a081-b7ba6e35bc97");
                //res.vcEditor = "-";
                //res.PropertiesCategorieId = 1;

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

                //    if (pp.PropertieId == 21)
                //    {
                //        pp.PropertieValue = row["age"].ToString();
                //    }
                //    else if (pp.PropertieId == 25)   //招聘人数
                //    {
                //        pp.PropertieValue = row["num"].ToString();
                //    }
                //    else if (pp.PropertieId == 26)  //专业要求
                //    {
                //        pp.PropertieValue = row["specialized"].ToString();
                //    }
                //    else if (pp.PropertieId == 27)
                //    {
                //        string p = row["sex"].ToString();
                //        if (p == "1")
                //        {
                //            pp.PropertieValue = "男";
                //        }
                //        else if (p == "2")
                //        {
                //            pp.PropertieValue = "女";
                //        }
                //        else
                //        {
                //            pp.PropertieValue = "不限";
                //        }
                //    }
                //    else if (pp.PropertieId == 28)
                //    {
                //        pp.PropertieValue = row["title"].ToString();
                //    }
                //    else if (pp.PropertieId == 29)
                //    {
                //        pp.PropertieValue = row["record"].ToString();
                //    }
                //    else if (pp.PropertieId == 30)
                //    {
                //        pp.PropertieValue = row["experience"].ToString();
                //    }
                //    else if (pp.PropertieId == 31)
                //    {
                //        pp.PropertieValue = row["department"].ToString();
                //    }
                //    else if (pp.PropertieId == 32)
                //    {
                //        pp.PropertieValue = row["other"].ToString();
                //    }
                //    else if (pp.PropertieId == 63)
                //    {
                //        pp.PropertieValue = row["other"].ToString();
                //    }


                //    pp.iOrder = ppti.iOrder;
                //    pp.ResourceId = res.Id;

                //    rtn = base.handlerService.resourcsService.resourcesHandlers.ResourcePropertiesManage(base.adminInfo, pp);
                //    txt = rtn == 1 ? "属性[" + pp.PropertieName + "]添加成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                //    Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;·" + txt + "<br/>");

                //}


                //txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                //Response.Write(row["position"].ToString() + "-----------" + txt + "<br/>");
                #endregion

                #region
                //产品
                //res.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                //res.vcAuthor = "-";
                //res.vcTitle = row["prod_name"].ToString();
                //res.vcUrl = "-";
                //res.vcKeyWord =  row["prod_name"].ToString();
                //res.vcContent = "-";
                //res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("1056a870-1366-44e5-94fa-e7a29209b8ee");
                //res.vcEditor = "-";
                //res.PropertiesCategorieId = 2;
                //res.vcSmallImg = "/filePatch/" + row["prod_img"].ToString();
                //res.vcBigImg = "/filePatch/" + row["prod_Bimg"].ToString();

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
                //Response.Write(row["prod_name"].ToString() + "-----------" + txt + "<br/>");
                #endregion


                //文体

                //res.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                //res.vcAuthor = "-";
                //res.vcTitle = row["name"].ToString();
                //res.vcKeyWord = row["name"].ToString();
                //res.vcContent = "-";
                //res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("dc9f041d-9137-4b43-af2a-bd219d69c5dd");
                //res.vcEditor = "-";
                //res.vcSmallImg = "/filePatch/small/" + row["img"].ToString() + ".jpg";
                //res.vcBigImg = "/filePatch/" + row["img_link"].ToString() + ".jpg";
                //res.vcSpeciality = "8";

                //int rtn = -1;
                //rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(res);

                //string txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                //Response.Write(row["name"].ToString() + "-----------" + txt + "<br/>");

        //    }
        //    Response.Write("记录总数:" + dt.Rows.Count + "<br/>");
        //}

        //Response.Write(Guid.NewGuid().ToString());
        //Resources res = (Resources)CachingService.Get("0555408c-bb8e-429a-ab01-5232c5b30e43");
        //Response.Write(Guid.NewGuid().ToString());


        //批量生成资讯
        Dictionary<string, EntityBase> res = null;
        int curpage = objectHandlers.ToInt(objectHandlers.Get("page"));
        int pagecount = 0;
        int count = 0;

        res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curpage, ref pagecount, ref count,
                   curpage, 100, "Id DESC", "1=1");

        if (res != null)
        {
            foreach (KeyValuePair<string, EntityBase> entity in res)
            {
                Resources ppti = (Resources)entity.Value;

                string errtxt = "";
                base.handlerService.tagService.CreateResourcHtmlById(ref  errtxt, objectHandlers.ToInt(ppti.Id));
            }
        }

        if (curpage < pagecount)
        {
            Response.Redirect("test.aspx?page=" + (curpage + 1).ToString());
        }


    }

    private void ReadExl()
    {
        //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=e:\\发黛西.xls;" + "Extended Properties=Excel 8.0;";
        //OleDbConnection conn = new OleDbConnection(strConn);
        //conn.Open();
        //string strExcel = "";
        //OleDbDataAdapter myCommand = null;
        //DataSet ds = null;
        //strExcel = "select * from [sheet1$]";
        //myCommand = new OleDbDataAdapter(strExcel, strConn);
        //ds = new DataSet();
        //myCommand.Fill(ds, "table1");

        //DataTable dt = ds.Tables[0];
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    for (int n = 0; n < dt.Columns.Count; n++)
        //    {
        //        Response.Write(dt.Rows[i][n].ToString() + "   ");
        //    }
        //    Response.Write("\r\n");
        //}
        //conn.Close();
    }
}