/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾���������(���ƹ�)�޹ء� 
  *  
  *    ����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
  */

using System;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Data;
using TCG.Files.Utils;

namespace TCG.Pages
{
    public class FilesMain : Origin
    {
        private Connection _conn;

        protected Connection conn
        {
            get
            {
                if (this._conn == null)
                {
                    this._conn = new Connection();
                    this._conn.Dblink = FilesConst.FilesDbLinks[0];
                }
                return this._conn;
            }
        }

        protected void AjaxErch(string str)
        {
            Response.Write(str);
            Response.End();
        }

        protected void Finish()
        {
            if ((this._conn != null) && this._conn.Connected) { this._conn.Close(); }    
        }
    }
}
