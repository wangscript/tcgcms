/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCG.Entity;

namespace TCG.Handlers
{
    public interface IResourceHandlers
    {
        /// <summary>
        /// 添加资讯
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        int CreateResources(Resources inf);

        /// <summary>
        /// 添加资讯
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        int CreateResourcesForSheif(Resources inf);

        /// <summary>
        /// 更新资源
        /// </summary>
        /// <param name="inf"></param>
        /// <returns></returns>
        int UpdateResources(Resources inf);

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="resourceid"></param>
        /// <returns></returns>
        Resources GetResourcesById(int resourceid);



        Dictionary<string, EntityBase> GetDelNewsInfoList(string ids);


        /// <summary>
        /// 批量删除资源文件
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="config"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        int DelNewsInfoHtmlByIds(string ids);


        /// <summary>
        /// 获取所有的文章咨询,并放入内存中,不要轻易调用,将消耗大量的时间
        /// </summary>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetAllResurces();

        Dictionary<string, EntityBase> GetAllResuresFromDataBase();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="categories"></param>
        /// <param name="Speciality"></param>
        /// <param name="orders"></param>
        /// <param name="check"></param>
        /// <param name="del"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetResourcesList(int nums, string categories, string Speciality, string orders, bool check, bool del, bool create, bool havechilecategorie);

        /// <summary>
        /// 获得标签中文章的搜索条件
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="Speciality"></param>
        /// <param name="check"></param>
        /// <param name="del"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        string GetTagResourceCondition(string categories, string Speciality, bool check, bool del, bool create, bool havechilecategorie);


        /// <summary>
        /// 获取指定页数
        /// </summary>
        /// <param name="curPage"></param>
        /// <param name="pageCount"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="order"></param>
        /// <param name="strCondition"></param>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetResourcesListPager(ref int curPage, ref int pageCount, ref int count, int page, int pagesize, string order, string strCondition);

        /// <summary>
        /// 生成文章路径
        /// </summary>
        /// <param name="extion"></param>
        /// <param name="title"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        string CreateNewsInfoFilePath(Resources nif);


        /// <summary>
        /// 就会或删除资源
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        int SaveOrDelResource(string ids, string action, Admin adminname);

        /// <summary>
        /// 获取分类下最新的一篇文章
        /// </summary>
        /// <param name="ategorie"></param>
        /// <returns></returns>
        Resources GetNewsResourcesAtCategorie(string ategorie);

        /// <summary>
        /// 文章属性管理
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="cp"></param>
        /// <returns></returns>
        int ResourcePropertiesManage(Admin admin, ResourceProperties cp);

        Dictionary<string, EntityBase> GetResourcePropertiesByRIdEntity(string rid);

    }
}
