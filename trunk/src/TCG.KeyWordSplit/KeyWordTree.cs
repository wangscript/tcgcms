using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TCG.KeyWordSplit
{
    /// <summary>
    /// 键树
    /// </summary>
    public class KeyWordTree
    {
        private static KeyWordTreeNode _Root = new KeyWordTreeNode();    //根节点
        /// <summary>
        /// 根节点
        /// </summary>
        public static KeyWordTreeNode Root
        {
            get { return _Root; }
            set { _Root = value; }
        }


        /// <summary>
        /// 添加如关键词
        /// </summary>
        /// <param name="keyWord">关键词</param>
        public static void AddKeyWord(string keyWord)
        {
            List<KeyWordTreeNode> tmpRoot = Root.ChildList;
            for (int iCount = 0; iCount < keyWord.Length; iCount++)
            {
                int tmpIndex = FindIndex(tmpRoot, keyWord[iCount]);
                if (tmpIndex == -1)
                {
                    KeyWordTreeNode tmpTreeNode = new KeyWordTreeNode(keyWord[iCount]);
                    tmpRoot.Add(tmpTreeNode);
                    tmpRoot = tmpTreeNode.ChildList;
                }
                else
                {
                    tmpRoot = tmpRoot[tmpIndex].ChildList;
                }

            }

        }

        /// <summary>
        /// 分词,键树查找
        /// </summary>
        /// <param name="strText">分词内容</param>
        /// <returns>分词结果</returns>
        public static string FindKeyWord(string strText, string splitstr)
        {

            List<KeyWordTreeNode> tmpRoot = Root.ChildList;
            StringBuilder strBuilder = new StringBuilder();
            int CC = 0;    //已经查到字符数 ,为了找不到的时候，判断是否退回一个字符
            for (int iCount = 0; iCount < strText.Length; iCount++)
            {

                int tmpIndex = FindIndex(tmpRoot, strText[iCount]);
                if (tmpIndex == -1)
                {
                    if (CC == 0)
                    {
                        strBuilder.Append(strText[iCount] + splitstr);
                    }
                    else
                    {
                        iCount -= 1;
                        strBuilder.Append(splitstr);
                    }
                    tmpRoot = Root.ChildList;
                    CC = 0;
                }
                else
                {
                    strBuilder.Append(strText[iCount]);
                    tmpRoot = tmpRoot[tmpIndex].ChildList;
                    CC++;
                }

            }
            return strBuilder.ToString();

        }



        /// <summary>
        /// 得到关键词ID号
        /// </summary>
        /// <param name="tmpRoot">支点</param>
        /// <param name="NoteValue">结点值</param>
        /// <returns>ID号</returns>
        private static int FindIndex(List<KeyWordTreeNode> tmpRoot, char NoteValue)
        {
            if (tmpRoot.Count == 0) return -1;
            KeyWordTreeNode TmpNode = tmpRoot[0];

            for (int iCount = 0; iCount < tmpRoot.Count; iCount++)
            {
                if (NoteValue == tmpRoot[iCount].TreeNode) return iCount;
            }
            return -1;

        }
    }
}
