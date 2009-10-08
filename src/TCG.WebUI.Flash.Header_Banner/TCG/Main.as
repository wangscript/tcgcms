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

package
{
   import flash.display.*;
   import flash.net.URLRequest;
 
   import flash.events.*;
   import flash.ui.*;
   import sandy.core.Scene3D;
   import sandy.core.data.*;
   import sandy.core.scenegraph.*;
   import sandy.materials.*;
   import sandy.materials.attributes.*;
   import sandy.primitive.*;
   import sandy.util.*;
   import sandy.events.*;

   public class Main extends Sprite 
   {
      private var scene:Scene3D;
	  private var camera:Camera3D;
	  private var queue:LoaderQueue;
	  private var numTree:Number = 50;
	 
	 public function Main():void
     {
	   queue = new LoaderQueue();
       queue.add( "background", new URLRequest("../asset/bg.jpg") );
	   queue.add( "logo", new URLRequest("../asset/logo.png") );
	   queue.addEventListener(SandyEvent.QUEUE_COMPLETE, loadComplete );
       queue.start();
	 }
	 
	  public function loadComplete(event:QueueEvent ):void
      {  
		 // We create the camera
		 camera = new Camera3D( 990, 273 );
		 camera.x = 0;
		 camera.y = 0;
		 camera.z = -300;
		 
		 // We create the "group" that is the tree of all the visible objects
         var root:Group = createScene();
		 
		 // We create a Scene and we add the camera and the objects tree 
	     scene = new Scene3D( "scene", this, camera, root );
		 scene.rectClipping = true;
		 
		 // Listen to the heart beat and render the scene
         addEventListener( Event.ENTER_FRAME, enterFrameHandler );
		 //stage.addEventListener(KeyboardEvent.KEY_DOWN, keyPressedHandler);
		 //stage.addEventListener(MouseEvent.MOUSE_MOVE, mouseMovedHandler);
		 
      }
	 
      // Create the scene graph based on the root Group of the scene
      private function createScene():Group
      {
         // Create the root Group
         var g:Group = new Group();
		 
         // 加载背景
		  var bg:Bitmap = new Bitmap(queue.data["background"].bitmapData);
		  var background:Sprite2D = new Sprite2D("background",bg,1);
		  background.x = 0;
		  background.z = 0;
		  background.y = 0;
		  g.addChild(background);
		  
		  // 加载LOGO
		  var lg:Bitmap = new Bitmap(queue.data["logo"].bitmapData);
		  var logo:Sprite2D = new Sprite2D("logo",lg,1);
		  logo.x = 0;
		  logo.z = 0;
		  logo.y = 0;
		  g.addChild(logo);
		  
		  return g;
      }

      // The Event.ENTER_FRAME event handler tells the Scene3D to render
      private function enterFrameHandler( event : Event ) : void
      {
		 //sphere.pan += 1
		 scene.render();
      }
	  
	  // This function handles the move foreward or backward simultaion
	  private function keyPressedHandler(event:KeyboardEvent):void {
            switch(event.keyCode) {
				case Keyboard.UP:
				    camera.moveForward(5);
					break;
				case Keyboard.DOWN:
				    camera.moveForward(-5);
				    break;
			}
        }
		
	  // This function handles the direction of the similation movement	
	  private function mouseMovedHandler(event:MouseEvent):void {
           camera.pan=(event.stageX-300/2)/10;
		   camera.tilt=(event.stageY-300/2)/20;
		   
        }	
   }
}