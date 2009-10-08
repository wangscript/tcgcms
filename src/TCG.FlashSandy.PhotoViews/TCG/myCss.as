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