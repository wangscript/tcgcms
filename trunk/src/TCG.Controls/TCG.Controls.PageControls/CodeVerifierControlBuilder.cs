
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

namespace TCG.Controls.PageControls
{
    using System;
    using System.Collections;
    using System.Web.UI;

    public class CodeVerifierControlBuilder : ControlBuilder
    {
        public override Type GetChildControlType(string tagName, IDictionary attributes)
        {
            if (tagName.ToLower() == "textboxattribute")
            {
                return typeof(VerifierTextBoxAttribute);
            }
            if (tagName.ToLower() == "codeimageattribute")
            {
                return typeof(VerifierCodeImageAttribute);
            }
            return null;
        }

    }
}

