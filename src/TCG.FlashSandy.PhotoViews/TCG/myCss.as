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

package  
{
	import flash.text.StyleSheet;
	/**
	 * ...
	 * @author 勤奋猪
	 */
	public class myCss extends StyleSheet
	{
		private var logo:Object = { "color":"#ffffff","fontWeight":"bold","fontSize":"18px","textAlgin" :"center"};
		private var loading:Object = { "color":"#ffffff" };
		private var copyright:Object = { "color":"#ffffff", "textAlgin":"center" };

		public function myCss() 
		{
			this.setStyle(".logo", logo);
			this.setStyle(".loading", loading);
			this.setStyle(".copyright", copyright);
		}
		
	}

}