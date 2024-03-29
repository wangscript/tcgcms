﻿/* 
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
using System.Text;


namespace TCG.Entity
{
    public class SheifSourceInfo : EntityBase
    {
        private string _id;
        private string _sourcename;
        private string _sourceurl;
        private string _charset;
        private string _listarearole;
        private string _topiclistrole;
        private string _topiclistdatarole;
        private string _topicrole;
        private string _topicdatarole;
        private string _topicpagerold;
        private string _topicpagertemp;
        private bool _isrsss = false;


        /// <summary>
        /// 源的名称
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }


        /// <summary>
        /// 源的名称
        /// </summary>
        public string SourceName
        {
            set { _sourcename = value; }
            get { return _sourcename; }
        }

        /// <summary>
        /// 源的地址
        /// </summary>
        public string SourceUrl
        {
            set { _sourceurl = value; }
            get { return _sourceurl; }
        }

        /// <summary>
        /// 编码格式
        /// </summary>
        public string CharSet
        {
            set { _charset = value; }
            get { return _charset; }
        }

        /// <summary>
        /// 列表区域规则
        /// </summary>
        public string ListAreaRole
        {
            set { _listarearole = value; }
            get { return _listarearole; }
        }

        /// <summary>
        /// 文章列表的规则
        /// </summary>
        public string TopicListRole
        {
            set { _topiclistrole = value; }
            get { return _topiclistrole; }
        }

        /// <summary>
        /// 文章列表中的文章的属性匹配的规则
        /// </summary>
        public string TopicListDataRole
        {
            set { _topiclistdatarole = value; }
            get { return _topiclistdatarole; }
        }

        /// <summary>
        /// 文章内容匹配规则
        /// </summary>
        public string TopicRole
        {
            set { _topicrole = value; }
            get { return _topicrole; }
        }

        /// <summary>
        /// 文章的属性匹配的规则
        /// </summary>
        public string TopicDataRole
        {
            set { _topicdatarole = value; }
            get { return _topicdatarole; }
        }

        /// <summary>
        /// 文章分页的替换前
        /// </summary>
        public string TopicPagerOld
        {
            set { _topicpagerold = value; }
            get { return _topicpagerold; }
        }

        /// <summary>
        /// 文章分页的替换过程量
        /// </summary>
        public string TopicPagerTemp
        {
            set { _topicpagertemp = value; }
            get { return _topicpagertemp; }
        }

        /// <summary>
        /// 是否是RSS
        /// </summary>
        public bool IsRss
        {
            set { _isrsss = value; }
            get { return _isrsss; }
        }
    }
}