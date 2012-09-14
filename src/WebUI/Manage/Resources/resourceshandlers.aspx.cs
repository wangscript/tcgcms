using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Controls.HtmlControls;

using TCG.Handlers;
using TCG.Entity;

namespace TCG.CMS.WebUi
{
    public partial class resources_resourceshandlers : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(18);

            this.ltfileclassid.Text = ConfigServiceEx.baseConfig["NewsFileClass"];

            string skinid = objectHandlers.Get("skinid");
            skinid = string.IsNullOrEmpty(skinid) ? ConfigServiceEx.DefaultSkinId : skinid;
            this.iSkinId.Value = skinid;

            if (!Page.IsPostBack)
            {
                int newsid = objectHandlers.ToInt(objectHandlers.Get("newsid"));
                string categorieid = objectHandlers.Get("iClassId");

                this.scp.Items.Clear();
                this.scp.Items.Add(new ListItem("请选择属性分类", "0"));
                Dictionary<string, EntityBase> allpc = base.handlerService.skinService.propertiesHandlers.GetPropertiesCategoriesEntityBySkinId(skinid);
                if (allpc != null && allpc.Count != 0)
                {
                    foreach (KeyValuePair<string, EntityBase> keyvalue in allpc)
                    {
                        PropertiesCategorie template = (PropertiesCategorie)keyvalue.Value;
                        this.scp.Items.Add(new ListItem(template.CategoriePropertiesName, template.Id));
                    }
                }
                
                if (newsid == 0)
                {
                    this.iClassId.Value = categorieid;
                    this.iSpeciality.Value = "0";
                    return;
                }

                Resources item = base.handlerService.resourcsService.resourcesHandlers.GetResourcesById(newsid);
                this.iClassId.Value = item.Categorie.Id.ToString();
                this.nid.Text = item.Id + "&t=" + DateTime.Now.ToString();

                this.iTitle.Value = item.vcTitle;
                this.iUrl.Value = item.vcUrl;
                this.iKeyWords.Value = item.vcKeyWord;
                this.taContent.Value = item.vcContent;
                this.iAuthor.Value = item.vcAuthor;
                this.work.Value = "MdyNews";
                this.iNewsId.Value = item.Id.ToString();
                this.iSpeciality.Value = item.vcSpeciality;
                this.iSmallImg.Value = item.vcSmallImg;
                this.iBigImg.Value = item.vcBigImg;
                this.iTitleColor.Value = item.vcTitleColor;
                this.iStrong.Checked = (item.cStrong == "Y") ? true : false;
                this.iShortContent.Value = item.vcShortContent;
                this.scp.Value = item.PropertiesCategorieId.ToString();
                this.cid.Text = item.PropertiesCategorieId.ToString() + "&t=" + DateTime.Now.ToString();
                this.ccCheckCateg.Value = item.CCCategories;

                StringBuilder sb = new StringBuilder();
                StringBuilder csb = new StringBuilder();
                Dictionary<string, EntityBase> allskins = base.handlerService.skinService.skinHandlers.GetAllSkinEntity();
                if (allskins != null && allskins.Count != 0)
                {
                    foreach (KeyValuePair<string, EntityBase> keyvalue in allskins)
                    {
                        Skin template = (Skin)keyvalue.Value;
                        sb.Append("<li><a onclick=\"SelectCCSkinC('" + template.Id + "')\" id=\"c_" + template.Id + "\">" + template.Name + "</a></li>");

                        csb.Append("<div class=\"cctree hid\" id=\"ct_" + template.Id + "\">");
                        csb.Append("<script type=\"text/javascript\">\r\n");
                        string csid = "t_" + template.Id.Replace("-","_");
                        csb.Append("\tvar " + csid + " = new dTree('" + csid + "');\r\n");
                        Dictionary<string, EntityBase> allcategates = base.handlerService.skinService.categoriesHandlers.GetAllCategoriesEntitySkinId(template.Id);
                        if (allcategates != null && allcategates.Count > 0)
                        {
                            foreach (KeyValuePair<string, EntityBase> keyvalue1 in allcategates)
                            {
                                Categories categat = (Categories)keyvalue1.Value;
                                string checkecs = item.CCCategories.IndexOf(categat.Id) > -1 ? "checked" : "";
                                string parent = categat.Parent == "0" ? "-1" : categat.Parent;
                                csb.Append("\t" + csid + ".add('" + categat.Id + "','" + parent + "','"
                                 + "<input type=\"checkbox\" name=\"ccCheckBox\" id=\"ck_" + categat.Id + "\" value=\""
                                 + categat.Id + "\" onclick=\"ccCheckBoxChange(this);\" cskin=\"" + categat.SkinInfo.Id + "\" " + checkecs + ">"  
                                 + categat.vcClassName + "');\r\n");
                           } 
                        }
                        csb.Append("\tdocument.write(" + csid + ");\r\n");
                        csb.Append("</script>");
                        csb.Append("</div>");
                    }
                }

                ccskintitle.InnerHtml = sb.ToString();
                ccright.InnerHtml = csb.ToString();

                item = null;
            }
            else
            {
                string work = objectHandlers.Post("work");
                bool ismdy = false;
                switch (work)
                {
                    case "AddNew":
                        ismdy = false;
                        break;
                    case "MdyNews":
                        ismdy = true;
                        break;
                }

                this.NewsManage(ismdy);
            }
        }

        private void NewsManage(bool ismdy)
        {
            string categorieid = objectHandlers.Post("iClassId");
            Resources item = new Resources();

            int pcid = objectHandlers.ToInt(objectHandlers.Post("scp"));
            bool delrpold = false;
            int oldrpoid = 0;
            if (ismdy)
            {
                item = base.handlerService.resourcsService.resourcesHandlers.GetResourcesById(objectHandlers.ToInt(objectHandlers.Post("iNewsId")));
                if (item.PropertiesCategorieId != pcid)
                {
                    delrpold = true;
                    oldrpoid = item.PropertiesCategorieId;
                }
            }
            else
            {
                item.cChecked = "N";
                item.cCreated = "N";
                item.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
            }

            item.vcTitle = objectHandlers.Post("iTitle");
            item.vcUrl = objectHandlers.Post("iUrl");
            item.vcContent = objectHandlers.Post("taContent");
            item.vcAuthor = objectHandlers.Post("iAuthor");
            item.vcKeyWord = objectHandlers.Post("iKeyWords");
            item.CCCategories = objectHandlers.Post("ccCheckBox");

            if (item.Categorie.Id != categorieid)
            {
                item.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(categorieid);
            }

            item.vcSpeciality = objectHandlers.Post("iSpeciality");
            item.vcBigImg = objectHandlers.Post("iBigImg");
            item.vcSmallImg = objectHandlers.Post("iSmallImg");
            item.vcTitleColor = objectHandlers.Post("sTitleColor");
            item.cStrong = objectHandlers.Post("iStrong");
            string s = objectHandlers.Post("iShortContent");
            item.vcShortContent = string.IsNullOrEmpty(s) ? objectHandlers.Left(objectHandlers.Post("iShortContent"), 100) : s;
            item.PropertiesCategorieId = pcid;

            if (string.IsNullOrEmpty(item.vcTitle))
            {
                base.AjaxErch(-1000000039, "");
                return;
            }

            if (string.IsNullOrEmpty(item.vcKeyWord))
            {
                base.AjaxErch(-1000000043, "");
                return;
            }


            if (string.IsNullOrEmpty(item.Categorie.Id))
            {
                base.AjaxErch(-1000000056, "");
                return;
            }

            item.vcEditor = base.adminInfo.vcAdminName;

            string filepath = "";
            string errText = string.Empty;
            int rtn = 0;

            if (!string.IsNullOrEmpty(item.vcUrl))
            {
                item.cCreated = "Y";
                item.cChecked = "Y";
            }

            try
            {
                if (!ismdy)
                {
                    rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(item);
                    //处理
                    if (!string.IsNullOrEmpty(item.CCCategories))
                    {
                        string mid = item.Id;
                        item.CCCategories = item.CCCategories + ",";
                        if (item.CCCategories.IndexOf(",") > -1)
                        {
                            string[] ccs = item.CCCategories.Split(',');
                            for (int i = 0; i < ccs.Length; i++)
                            {
                                string txg = ccs[i];
                                if (!string.IsNullOrEmpty(txg))
                                {
                                    item.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                                    item.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(txg);
                                    item.SheifUrl = mid;
                                    item.CCCategories = "";
                                    rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(item);
                                }
                            }
                        }
                    }
                }
                else
                {
                    rtn = base.handlerService.resourcsService.resourcesHandlers.UpdateResources(item);
                    if (delrpold)
                    {
                        rtn = base.handlerService.resourcsService.resourcesHandlers.DelResourcesProperties(item.Id);
                    }

                    //处理
                    if (!string.IsNullOrEmpty(item.CCCategories))
                    {
                        string mid1 = item.Id;
                        item.CCCategories = item.CCCategories + ",";
                        if (item.CCCategories.IndexOf(",") > -1)
                        {
                            string[] ccs = item.CCCategories.Split(',');
                            for (int i = 0; i < ccs.Length; i++)
                            {
                                string txg1 = ccs[i];
                                if (!string.IsNullOrEmpty(txg1))
                                {
                                    int num3 = 0, num2 = 0, num1 = 0;
                                    Dictionary<string, EntityBase> res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref num3,
                                        ref num2, ref num1, 1, 1, "ID desc", "SheifUrl='" + mid1 + "' AND iClassid = '" + txg1 + "'");
                                    if (res != null && res.Count == 1)
                                    {
                                        Resources res1 = null;
                                        foreach (KeyValuePair<string, EntityBase> entity in res)
                                        {
                                            res1 = (Resources)entity.Value;
                                        }
                                        item.Id = res1.Id;
                                        item.Categorie = res1.Categorie;
                                        item.SheifUrl = res1.SheifUrl;
                                        item.CCCategories = "";
                                        rtn = base.handlerService.resourcsService.resourcesHandlers.UpdateResources(item);
                                    }
                                    else
                                    {
                                        item.Id = (base.handlerService.resourcsService.resourcesHandlers.GetMaxResourceId() + 1).ToString();
                                        item.Categorie = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(txg1);
                                        item.SheifUrl = mid1;
                                        item.CCCategories = "";
                                        rtn = base.handlerService.resourcsService.resourcesHandlers.CreateResources(item);
                                    }
                                }
                            }
                        }
                    }
                }

                if (rtn == 1)
                {

                    foreach (string key in Request.Form.AllKeys)
                    {
                        if (key == string.Empty)
                        {
                            string sdgsg = "";
                        }
                        string[] keys = key.Split('_');

                        if (key.IndexOf("rpvalue_") > -1 && !string.IsNullOrEmpty(objectHandlers.Post(key)))
                        {
                            ResourceProperties rps = new ResourceProperties();
                            rps.Id = objectHandlers.Post("rpid_" + keys[1]);
                            rps.PropertieName = objectHandlers.Post("ptname_" + keys[1]);
                            rps.ResourceId = item.Id;
                            rps.PropertieValue = objectHandlers.Post("rpvalue_" + keys[1]);
                            rps.PropertieId = objectHandlers.ToInt(objectHandlers.Post("cpid_" + keys[1]));
                            rps.iOrder = objectHandlers.ToInt(objectHandlers.Post("rporder_" + keys[1]));
                            rtn = base.handlerService.resourcsService.resourcesHandlers.ResourcePropertiesManage(base.adminInfo, rps);
                        }
                        else if (key.IndexOf("rpvalue_") > -1 && string.IsNullOrEmpty(objectHandlers.Post(key)))
                        {

                            string Id = objectHandlers.Post("rpid_" + keys[1]);
                            if (!string.IsNullOrEmpty(Id))
                            {
                                rtn = base.handlerService.resourcsService.resourcesHandlers.DelResourcesPropertiesOnIds(item.Id, Id);
                            }
                        }
                    }
                }

                if (item.cChecked == "Y")
                {
                    rtn = base.handlerService.tagService.CreateResourcHtmlById(ref errText, objectHandlers.ToInt(item.Id));
                }

                //更新列表分页第一页
                int pagecount = 0;
                if (item.cChecked == "Y" && item.cCreated == "Y" && item.cDel == "N")
                {
                    rtn = base.handlerService.tagService.CreateClassList(item.Categorie.Id, 0, ref pagecount, ref errText);
                }
            }
            catch (Exception ex)
            {
                base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }

            if (ismdy)
            {
                base.AjaxErch(rtn, "文章修改成功！");
            }
            else
            {
                base.AjaxErch(rtn, "文章添加成功，请继续添加！", "NewsAddPostBack()");
            }
        }
    }
}