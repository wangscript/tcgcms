/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三晕鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.KeyWordSplit
{
    /// <summary>
    ///  广义表 表示树 的节点
    /// </summary>
    public class KeyWordTreeNode
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public KeyWordTreeNode()
        { }

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="NodeValue">树节点值</param>
        public KeyWordTreeNode(char NodeValue)
        {
            _TreeNode = NodeValue;

        }


        private char _TreeNode;    //树节点值
        /// <summary>
        /// 树节点值
        /// </summary>
        public char TreeNode
        {
            get { return _TreeNode; }
            set { _TreeNode = value; }
        }


        private List<KeyWordTreeNode> _ChildList = new List<KeyWordTreeNode>(); //子树

        /// <summary>
        /// 子树
        /// </summary>
        public List<KeyWordTreeNode> ChildList
        {
            get { return _ChildList; }
            set { _ChildList = value; }
        }
    }
}
