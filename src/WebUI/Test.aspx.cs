using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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


using System.Reflection;
using System.ComponentModel;

using TCG.DBHelper;

namespace TCG.CMS.WebUi
{

    public partial class Test : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int page = objectHandlers.ToInt(objectHandlers.Get("page"), 1);
            TCG.Handlers.Imp.AccEss.AccessFactory.conn.SetConnStr(HttpContext.Current.Server.MapPath("/database/TCG_DB111104.mdb"));

            int curpage = 0, count = 0, pagecount = 0, pagesize = 20;
            Dictionary<string, EntityBase> resources = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curpage,
                ref pagecount, ref count, page, pagesize, " ID DESC ", "iClassID='0ecdbbdd-fe89-48d6-a2a0-d9b5f29ffdd0' AND id>2621 ");

            //DELETE FROM resources WHERE iClassId = '6b110384-8a7e-4516-b9df-f92a433743dd'
            if (resources != null && resources.Count > 0)
            {
                foreach (KeyValuePair<string, EntityBase> entity in resources)
                {
                    Resources restemp = (Resources)entity.Value;

                    TCG.Handlers.Imp.AccEss.AccessFactory.conn.SetConnStr(HttpContext.Current.Server.MapPath("/database/TCG_DB111104.mdb"));
                    Dictionary<string, EntityBase> ress = base.handlerService.resourcsService.resourcesHandlers.GetResourcePropertiesByRIdEntity(restemp.Id);

                    TCG.Handlers.Imp.AccEss.AccessFactory.conn.SetConnStr(HttpContext.Current.Server.MapPath("/database/TCG_DB.mdb"));

                    restemp.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                    restemp.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("0ecdbbdd-fe89-48d6-a2a0-d9b5f29ffdd0");

                    if (restemp.PropertiesCategorieId == 1)
                    {
                        restemp.PropertiesCategorieId = 3;
                    }
                    else if (restemp.PropertiesCategorieId == 2)
                    {
                        restemp.PropertiesCategorieId = 4;
                    }

                    if (restemp.vcSpeciality == "10")
                    {
                        restemp.vcSpeciality = "18";
                    }
                    else if (restemp.vcSpeciality == "11")
                    {
                        restemp.vcSpeciality = "19";
                    }

                    else if (restemp.vcSpeciality == "6")
                    {
                        restemp.vcSpeciality = "14";
                    }
                    else if (restemp.vcSpeciality == "7")
                    {
                        restemp.vcSpeciality = "15";
                    }

                    else if (restemp.vcSpeciality == "8")
                    {
                        restemp.vcSpeciality = "16";
                    }

                    int rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(restemp);

                    if (rtn == 1)
                    {
                        Response.Write(" - 导入文章[" + restemp.vcTitle + "][" + restemp.Categorie.vcClassName + "]成功!<br/>");
                        if (ress != null && ress.Count > 0)
                        {
                            foreach (KeyValuePair<string, EntityBase> entity1 in ress)
                            {
                                ResourceProperties restemp1 = (ResourceProperties)entity1.Value;
                                restemp1.ResourceId = restemp.Id;
                                restemp1.Id = "";
                                restemp1.PropertieValue = restemp1.PropertieValue.Replace("'", "''");

                                #region 属性ID转换
                                if (restemp1.PropertieId == 21)
                                {
                                    restemp1.PropertieId = 64;
                                }
                                else if (restemp1.PropertieId == 25)
                                {
                                    restemp1.PropertieId = 65;
                                }
                                else if (restemp1.PropertieId == 26)
                                {
                                    restemp1.PropertieId = 66;
                                }
                                else if (restemp1.PropertieId == 27)
                                {
                                    restemp1.PropertieId = 67;
                                }
                                else if (restemp1.PropertieId == 28)
                                {
                                    restemp1.PropertieId = 68;
                                }
                                else if (restemp1.PropertieId == 29)
                                {
                                    restemp1.PropertieId = 69;
                                }
                                else if (restemp1.PropertieId == 30)
                                {
                                    restemp1.PropertieId = 70;
                                }
                                else if (restemp1.PropertieId == 31)
                                {
                                    restemp1.PropertieId = 71;
                                }
                                else if (restemp1.PropertieId == 32)
                                {
                                    restemp1.PropertieId = 72;
                                }
                                else if (restemp1.PropertieId == 63)
                                {
                                    restemp1.PropertieId = 73;
                                }
                                else if (restemp1.PropertieId == 22)
                                {
                                    restemp1.PropertieId = 74;
                                }
                                else if (restemp1.PropertieId == 23)
                                {
                                    restemp1.PropertieId = 75;
                                }
                                else if (restemp1.PropertieId == 24)
                                {
                                    restemp1.PropertieId = 76;
                                }
                                else if (restemp1.PropertieId == 33)
                                {
                                    restemp1.PropertieId = 77;
                                }
                                else if (restemp1.PropertieId == 34)
                                {
                                    restemp1.PropertieId = 78;
                                }
                                else if (restemp1.PropertieId == 35)
                                {
                                    restemp1.PropertieId = 79;
                                }
                                else if (restemp1.PropertieId == 36)
                                {
                                    restemp1.PropertieId = 80;
                                }
                                else if (restemp1.PropertieId == 37)
                                {
                                    restemp1.PropertieId = 81;
                                }
                                else if (restemp1.PropertieId == 38)
                                {
                                    restemp1.PropertieId = 82;
                                }
                                else if (restemp1.PropertieId == 39)
                                {
                                    restemp1.PropertieId = 83;
                                }
                                else if (restemp1.PropertieId == 40)
                                {
                                    restemp1.PropertieId = 84;
                                }
                                else if (restemp1.PropertieId == 41)
                                {
                                    restemp1.PropertieId = 85;
                                }
                                else if (restemp1.PropertieId == 42)
                                {
                                    restemp1.PropertieId = 86;
                                }
                                else if (restemp1.PropertieId == 43)
                                {
                                    restemp1.PropertieId = 87;
                                }
                                else if (restemp1.PropertieId == 44)
                                {
                                    restemp1.PropertieId = 88;
                                }
                                else if (restemp1.PropertieId == 45)
                                {
                                    restemp1.PropertieId = 89;
                                }
                                else if (restemp1.PropertieId == 46)
                                {
                                    restemp1.PropertieId = 90;
                                }
                                else if (restemp1.PropertieId == 47)
                                {
                                    restemp1.PropertieId = 91;
                                }
                                else if (restemp1.PropertieId == 48)
                                {
                                    restemp1.PropertieId = 92;
                                }
                                else if (restemp1.PropertieId == 49)
                                {
                                    restemp1.PropertieId = 93;
                                }
                                else if (restemp1.PropertieId == 50)
                                {
                                    restemp1.PropertieId = 94;
                                }
                                else if (restemp1.PropertieId == 51)
                                {
                                    restemp1.PropertieId = 95;
                                }
                                else if (restemp1.PropertieId == 52)
                                {
                                    restemp1.PropertieId = 96;
                                }
                                else if (restemp1.PropertieId == 53)
                                {
                                    restemp1.PropertieId = 97;
                                }
                                else if (restemp1.PropertieId == 54)
                                {
                                    restemp1.PropertieId = 98;
                                }
                                else if (restemp1.PropertieId == 55)
                                {
                                    restemp1.PropertieId = 99;
                                }
                                else if (restemp1.PropertieId == 56)
                                {
                                    restemp1.PropertieId = 100;
                                }
                                else if (restemp1.PropertieId == 57)
                                {
                                    restemp1.PropertieId = 101;
                                }
                                else if (restemp1.PropertieId == 58)
                                {
                                    restemp1.PropertieId = 102;
                                }
                                else if (restemp1.PropertieId == 59)
                                {
                                    restemp1.PropertieId = 103;
                                }
                                else if (restemp1.PropertieId == 60)
                                {
                                    restemp1.PropertieId = 104;
                                }
                                else if (restemp1.PropertieId == 61)
                                {
                                    restemp1.PropertieId = 105;
                                }
                                else if (restemp1.PropertieId == 62)
                                {
                                    restemp1.PropertieId = 106;
                                }

                                #endregion


                                rtn = base.handlerService.resourcsService.resourcesHandlers.ResourcePropertiesManage(base.adminInfo, restemp1);
                                if (rtn == 1)
                                {
                                    Response.Write(" -- 导入文章[" + restemp1.PropertieName + "][" + restemp1.PropertieValue + "]成功!<br/>");
                                }
                            }
                        }

                    }
                }
            }

            TCG.Handlers.Imp.AccEss.AccessFactory.conn.SetConnStr(HttpContext.Current.Server.MapPath("/database/TCG_DB.mdb"));

            if (page < pagecount)
            {
                Response.Redirect("test.aspx?page=" + (page + 1));
            }




            /*
            int page = objectHandlers.ToInt(objectHandlers.Get("page"),1);
            TCG.Handlers.Imp.AccEss.AccessFactory.conn.SetConnStr(  HttpContext.Current.Server.MapPath("/database/TCG_DB111031.mdb"));

            int curpage = 0, count = 0, pagecount = 0, pagesize = 20;
           Dictionary<string, EntityBase> resources =  base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curpage,
               ref pagecount, ref count, page, pagesize, " ID DESC ", "iClassID='dc9f041d-9137-4b43-af2a-bd219d69c5dd'");

            //DELETE FROM resources WHERE iClassId = '6b110384-8a7e-4516-b9df-f92a433743dd'
           if (resources != null && resources.Count > 0)
           {
               foreach (KeyValuePair<string, EntityBase> entity in resources)
               {
                   Resources restemp = (Resources)entity.Value;


                   
                   TCG.Handlers.Imp.AccEss.AccessFactory.conn.SetConnStr(  HttpContext.Current.Server.MapPath("/database/TCG_DB.mdb"));

                   restemp.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                   restemp.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("86726cbc-aa3d-41ca-b427-7c3870e1510f");

                   if (restemp.vcSpeciality == "6")
                   {
                       restemp.vcSpeciality = "14";
                   }
                   else if (restemp.vcSpeciality == "7")
                   {
                       restemp.vcSpeciality = "15";
                   }

                   else if (restemp.vcSpeciality == "8")
                   {
                       restemp.vcSpeciality = "16";
                   }

                   int rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(restemp);

                   if (rtn == 1)
                   {
                       Response.Write(" - 导入文章[" + restemp.vcTitle + "][" + restemp.Categorie.vcClassName + "]成功!<br/>");
                   }
               }
           }

           TCG.Handlers.Imp.AccEss.AccessFactory.conn.SetConnStr(HttpContext.Current.Server.MapPath("/database/TCG_DB.mdb"));

           if (page < pagecount)
           {
               Response.Redirect("test.aspx?page=" + (page + 1));
           }
             */


            /*
            TCG.Handlers.Imp.AccEss.AccessFactory.conn.SetConnStr(HttpContext.Current.Server.MapPath("/database/TCG_DB111031.mdb"));
            Dictionary<string, EntityBase> proerties = base.handlerService.skinService.propertiesHandlers.GetPropertiesByCIdEntity("2");
            if (proerties != null && proerties.Count > 0)
            {
                foreach (KeyValuePair<string, EntityBase> entity in proerties)
                {
                    Properties restemp = (Properties)entity.Value;
                    TCG.Handlers.Imp.AccEss.AccessFactory.conn.SetConnStr(HttpContext.Current.Server.MapPath("/database/TCG_DB.mdb"));

                    restemp.Id = (base.handlerService.skinService.propertiesHandlers.GetMaxProperties() + 1).ToString();
                    restemp.PropertiesCategorieId = "4";

                    int rtn = base.handlerService.skinService.propertiesHandlers.PropertiesManage(base.adminInfo, restemp);
                    Response.Write(" - 导入属性[" + restemp.ProertieName + "]成功!<br/>");
                }
            }

            TCG.Handlers.Imp.AccEss.AccessFactory.conn.SetConnStr(HttpContext.Current.Server.MapPath("/database/TCG_DB.mdb"));
             */
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
}