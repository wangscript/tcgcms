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
               

                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("/database/taobao1.csv"), FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string t = sr.ReadToEnd();

                string text = "<table border=\"0\" cellpadding=\"1\" cellspacing=\"1\" bgcolor=\"#006600\" width=\"4770\" >";
                string[] arr = t.Split(new string[] { "\n" }, StringSplitOptions.None);

                Dictionary<string, EntityBase> ps = base.handlerService.skinService.propertiesHandlers.GetPropertiesByCIdEntity("1");
                for (int i = 0; i < arr.Length; i++)
                {

                    string[] arr1 = arr[i].Split(new string[] { "\t" }, StringSplitOptions.None);

                    if (arr1.Length>=45)
                    {
                        if (i < 15)
                        {
                            text += "<tr>";

                            string ext = "bgcolor=\"#FFFFFF\"";
                            if (i == 0)
                            {
                                ext = "bgcolor=\"#009900\"";
                                for (int n = 0; n < arr1.Length; n++)
                                {
                                    string text2 = "(" + n.ToString() + ")" + arr1[n];
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
                                   0, 15, "dAddDate DESC,ID DESC", "SheifUrl='" + arr1[44] + "'");
                        }
                        catch (Exception ex)
                        {

                        }

                        Resources res = new Resources();

                        if (ress != null && ress.Count == 1)
                        {
                            foreach (KeyValuePair<string, EntityBase> entity1 in ress)
                            {
                                res = (Resources)entity1.Value;

                            }

                            res.vcTitle = objectHandlers.GetTextWithoutHtml(arr1[0]).Replace("<","").Replace(">","");
                            res.vcKeyWord = objectHandlers.GetTextWithoutHtml(arr1[0]).Replace("<", "").Replace(">", ""); ;
                            res.vcContent = arr1[24].Replace("\"\"", "'");
                            res.SheifUrl = arr1[43];
                            res.vcSmallImg = "/filePatch/taobao/" + arr1[35].Split(';')[0].Split('|')[0].Split(':')[0].Replace("\"", "") + ".jpg";

                            int rtn = -1;
                            rtn = base.handlerService.resourcsService.resourcesHandlers.UpdateResources(res);

                            string txt = "";

                            Dictionary<string, EntityBase> aps = base.handlerService.resourcsService.resourcesHandlers.GetResourcePropertiesByRIdEntity(res.Id);

                            foreach (KeyValuePair<string, EntityBase> entity in aps)
                            {
                                ResourceProperties pp = (ResourceProperties)entity.Value;

                                if (pp.PropertieId == 1) //价格
                                {
                                    pp.PropertieValue = objectHandlers.ToFloat(arr1[7]).ToString("0.00");
                                }
                                else if (pp.PropertieId == 2) //所在地
                                {
                                    pp.PropertieValue = arr1[4] + arr1[5];
                                }
                                else if (pp.PropertieId == 3) //QQ交谈
                                {
                                    pp.PropertieValue = "644139466";
                                }
                                else if (pp.PropertieId == 4) //旺旺
                                {
                                    pp.PropertieValue = "%E9%99%B6%E8%A8%80%E8%B9%8A";
                                }
                                else if (pp.PropertieId == 5) //展示图片1
                                {
                                    if (arr1[35].Split(';').Length >= 3)
                                    {
                                        pp.PropertieValue = arr1[35].Split(';')[1].Split('|')[1].Replace("\"","");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (pp.PropertieId == 6) //展示图片2
                                {
                                    if (arr1[35].Split(';').Length >= 4)
                                    {
                                        pp.PropertieValue = arr1[35].Split(';')[2].Split('|')[1].Replace("\"", "");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (pp.PropertieId == 7) //展示图片3
                                {
                                    if (arr1[35].Split(';').Length >= 5)
                                    {
                                        pp.PropertieValue = arr1[35].Split(';')[3].Split('|')[1].Replace("\"", "");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (pp.PropertieId == 8) //展示图片4
                                {
                                    if (arr1[35].Split(';').Length >= 6)
                                    {
                                        pp.PropertieValue = arr1[35].Split(';')[4].Split('|')[1].Replace("\"", "");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                //<a target="_blank" href="http://www.taobao.com/webww/ww.php?ver=3&touid=sanyungui&siteid=cntaobao&status=1&charset=utf-8"><img border="0" src="http://amos.alicdn.com/online.aw?v=2&uid=sanyungui&site=cntaobao&s=1&charset=utf-8" alt="点击这里给我发消息" /></a>
                                //<a target="_blank" href="http://www.taobao.com/webww/ww.php?ver=3&touid=%E9%99%B6%E8%A8%80%E8%B9%8A&siteid=cntaobao&status=1&charset=utf-8"><img border="0" src="http://amos.alicdn.com/online.aw?v=2&uid=%E9%99%B6%E8%A8%80%E8%B9%8A&site=cntaobao&s=1&charset=utf-8" alt="点击这里给我发消息" /></a>

                                pp.PropertieValue = pp.PropertieValue.Replace("'", "''");

                                rtn = base.handlerService.resourcsService.resourcesHandlers.ResourcePropertiesManage(base.adminInfo, pp);
                                txt = rtn == 1 ? "属性[" + pp.PropertieName + "]修改成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                                Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;·" + txt + "<br/>");

                            }
                        }
                        else
                        {

                            res.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                            res.vcAuthor = "-";
                            res.vcTitle = objectHandlers.GetTextWithoutHtml(arr1[0]).Replace("<", "").Replace(">", ""); ;
                            res.vcKeyWord = objectHandlers.GetTextWithoutHtml(arr1[0]).Replace("<", "").Replace(">", ""); ;
                            res.vcContent = arr1[24].Replace("\"\"", "'");
                            res.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById("cfc0175d-04d9-45ad-a1ac-5762bb304521");
                            res.vcEditor = "-";
                            res.PropertiesCategorieId = 1;
                            res.SheifUrl = arr1[44];

                            res.vcSmallImg = "/filePatch/taobao/" + arr1[35].Split(';')[0].Split('|')[0].Split(':')[0].Replace("\"","") + ".jpg";

                            int rtn = -1;
                            rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(res);
                            string txt = "";

                            foreach (KeyValuePair<string, EntityBase> entity in ps)
                            {
                                Properties ppti = (Properties)entity.Value;
                                ResourceProperties pp = new ResourceProperties();
                                pp.Id = "";
                                pp.PropertieId = objectHandlers.ToInt(ppti.Id);
                                pp.PropertieName = ppti.ProertieName;
                                pp.ResourceId = res.Id;


                                if (pp.PropertieId == 1) //价格
                                {
                                    pp.PropertieValue =  objectHandlers.ToFloat( arr1[7]).ToString("0.00");
                                }
                                else if (pp.PropertieId == 2) //所在地
                                {
                                    pp.PropertieValue = arr1[4] + arr1[5];
                                }
                                else if (pp.PropertieId == 3) //QQ交谈
                                {
                                    pp.PropertieValue = "644139466";
                                }
                                else if (pp.PropertieId == 4) //旺旺
                                {
                                    pp.PropertieValue = "%E9%99%B6%E8%A8%80%E8%B9%8A";
                                }
                                else if (pp.PropertieId == 5) //展示图片1
                                {
                                    if (arr1[35].Split(';').Length >= 3)
                                    {
                                        pp.PropertieValue = arr1[35].Split(';')[1].Split('|')[1].Replace("\"", "");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (pp.PropertieId == 6) //展示图片2
                                {
                                    if (arr1[35].Split(';').Length >= 4)
                                    {
                                        pp.PropertieValue = arr1[35].Split(';')[2].Split('|')[1].Replace("\"", "");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (pp.PropertieId == 7) //展示图片3
                                {
                                    if (arr1[35].Split(';').Length >= 5)
                                    {
                                        pp.PropertieValue = arr1[35].Split(';')[3].Split('|')[1].Replace("\"", "");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (pp.PropertieId == 8) //展示图片4
                                {
                                    if (arr1[35].Split(';').Length >= 6)
                                    {
                                        pp.PropertieValue = arr1[35].Split(';')[4].Split('|')[1].Replace("\"", "");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                //<a target="_blank" href="http://www.taobao.com/webww/ww.php?ver=3&touid=sanyungui&siteid=cntaobao&status=1&charset=utf-8"><img border="0" src="http://amos.alicdn.com/online.aw?v=2&uid=sanyungui&site=cntaobao&s=1&charset=utf-8" alt="点击这里给我发消息" /></a>
                                //<a target="_blank" href="http://www.taobao.com/webww/ww.php?ver=3&touid=%E9%99%B6%E8%A8%80%E8%B9%8A&siteid=cntaobao&status=1&charset=utf-8"><img border="0" src="http://amos.alicdn.com/online.aw?v=2&uid=%E9%99%B6%E8%A8%80%E8%B9%8A&site=cntaobao&s=1&charset=utf-8" alt="点击这里给我发消息" /></a>

                                pp.PropertieValue = pp.PropertieValue.Replace("'", "''");

                                rtn = base.handlerService.resourcsService.resourcesHandlers.ResourcePropertiesManage(base.adminInfo, pp);
                                txt = rtn == 1 ? "属性[" + pp.PropertieName + "]添加成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                                Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;·" + txt + "<br/>");
                            }

                            txt = rtn == 1 ? "导入成功！" : errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]);
                        }
                    }
                }

                text += "</table>";
                dataveiw.InnerHtml = text;
            }
        }
    }
}