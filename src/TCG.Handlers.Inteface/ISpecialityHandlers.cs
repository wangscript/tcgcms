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
    public interface ISpecialityHandlers
    {
        /// <summary>
        /// 添加新的资讯类型
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="nif"></param>
        /// <returns></returns>
        int NewsSpecialityAdd(Speciality nif);

        /// <summary>
        /// 修改资讯类型
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="nif"></param>
        /// <returns></returns>
        int NewsSpecialityMdy(Speciality nif);


        /// <summary>
        /// 删除特性
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        int NewSpecialityDel(string ids);


        /// <summary>
        /// 获得所有资源特性
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        DataTable GetAllNewsSpecialityInfo(string skinid);
    }
}
