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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCG.Entity;

namespace TCG.Handlers
{
    public interface IFileHandlers
    {

        /// <summary>
        /// 获得所有文章分类实体
        /// </summary>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetAllFileCategoriesEntity();

        /// <summary>
        /// 根据ID获得文章分类信息
        /// </summary>
        /// <param name="filecateorieid"></param>
        /// <returns></returns>
        FileCategories GetFileCategories(string filecateorieid);

        /// <summary>
        /// 根据父亲获得文件夹
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="prarentid"></param>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetFileCategories(int prarentid);

        string GetFilesPathByClassId(int classid);

        /// <summary>
        /// 添加新的分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="fcif"></param>
        /// <returns></returns>
        int AddFileClass(string adminname, FileCategories fcif);


        /// <summary>
        /// 添加新的分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="fcif"></param>
        /// <returns></returns>
        int AddFileInfoByAdmin(string adminname, FileResources fif);


        /// <summary>
        ///根据ID获得文件信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        FileResources GetFileInfosById(string id);

        string UrlCheck(string url, Resources resource);

        /// <summary>
        /// 获取文章中间外部网站的图片
        /// </summary>
        /// <param name="content"></param>
        /// <param name="url"></param>
        /// <param name="adminname"></param>
        /// <param name="fileclassid"></param>
        /// <returns></returns>
        string ImgPatchInit(Resources resource, string url, string adminname, int fileclassid);

        /// <summary>
        /// 判断远程文件是否存在
        /// </summary>
        /// <param name="curl"></param>
        /// <returns></returns>
        int GetUrlError(string curl);

        string GetFilePath(string filename, int fileclassid);

        int UploadFile(byte[] _bytes, string adminname, string imagetype, int fileclassid, ref string imagepath);

        /// <summary>
        /// 根据后缀获取图片格式
        /// </summary>
        /// <param name="filePath">图片路径</param>
        ImageFormat GetImgFormat(string filePath);
    }
}
