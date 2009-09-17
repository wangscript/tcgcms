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
