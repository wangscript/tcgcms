/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ�������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾����������(���ƹ�)�޹ء� 
  *  
  *    �����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
  */

using System;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Data;

namespace TCG.Pages
{
    public class FilesMain : Origin
    {


        protected void AjaxErch(string str)
        {
            Response.Write(str);
            Response.End();
        }

        protected void Finish()
        {
            if ((this.conn != null) && this.conn.Connected) { this.conn.Close(); }    
        }
    }
}