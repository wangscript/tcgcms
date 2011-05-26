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
using System.Data;
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

        int GetMaxResourceId();


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
        DataTable GetResourcesById(int resourceid);



        DataTable GetDelNewsInfoList(string ids);


        DataTable GetAllResuresFromDataBase();

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
        DataTable GetResourcesList(string sqlsb);


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
        DataTable GetResourcesListPager(ref int curPage, ref int pageCount, ref int count, int page, int pagesize, string order, string strCondition);

        

        /// <summary>
        /// 就会或删除资源
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        int SaveOrDelResource(string ids, string action);

        /// <summary>
        /// 获取分类下最新的一篇文章
        /// </summary>
        /// <param name="ategorie"></param>
        /// <returns></returns>
        DataTable GetNewsResourcesAtCategorie(string ategorie);

        /// <summary>
        /// 文章属性管理
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="cp"></param>
        /// <returns></returns>
        int ResourcePropertiesManage(ResourceProperties cp);

        DataTable GetResourcePropertiesByRIdEntity(string rid);

        int DelResourcesProperties(string resid);

        int DelResourcesPropertiesOnIds(string resid, string ids);

    }
}
