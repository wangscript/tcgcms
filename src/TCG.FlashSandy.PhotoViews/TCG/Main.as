package
{
	import flash.display.*;
	import flash.geom.Rectangle;
	import flash.net.URLRequest;
	import flash.net.URLLoader;
	import flash.text.TextField;
	import flash.utils.Dictionary;
	import flash.external.ExternalInterface;
	import org.papervision3d.core.animation.channel.MatrixChannel3D;
	import org.papervision3d.core.math.Sphere3D;
	import sandy.bounds.BBox;
	import sandy.bounds.BSphere;
	import sandy.parser.AParser;
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
		
		private var earth:Sphere;
		private var moon:Sphere;
		
		private var imgxml:XML;
		private var sObj:Shape3D;
		private var myText:TextField = new TextField();
		private var totalFileSize:int;
		private var totalfilecount:int;
		private var logNUm:int;
	
		
		public function Main():void
		{
			stage.scaleMode = "noScale"; 
			stage.align = "LT";
			
			var loader:URLLoader=new URLLoader(new URLRequest("photos.xml"));
			loader.addEventListener(Event.COMPLETE, imgxmlcompleteHandler);
			loader.addEventListener(IOErrorEvent.IO_ERROR, xmlloadError); 
		}
		
		private function imgxmlcompleteHandler(e:Event):void{    
			imgxml=new XML(e.target.data);//此时，外部的xml数据被完全转存到内存myxml文件中  
			totalfilecount = imgxml.descendants("file").length();//得到图片总数,此处为3
			var i:int;//int效率快过Number
			if (totalfilecount <= 0) return;
			queue = new LoaderQueue();
			
			for (i = 0; i < totalfilecount; i++) {
				//@符号是读xml文件中带=号形式的属性的专用语法
				queue.add( imgxml.file[i].@id, new URLRequest(imgxml.file[i].@url) );	
				totalFileSize += parseInt(imgxml.file[i].@size);
			}
			
			myText.width = 300;
			myText.x = stage.stageWidth/2;
			myText.y = stage.stageHeight/2;
			myText.textColor = 0xffcc00; 
			myText.appendText("loading...");
			this.addChild(myText);
			logNUm = 1;
			
			queue.addEventListener(SandyEvent.QUEUE_COMPLETE, loadComplete );
			queue.addEventListener(QueueEvent.QUEUE_RESOURCE_LOADED, queueLoadResource);
			queue.start();
		}
		
		//函数功能：捕获Xml配置文件加载异常（主要是URL错误异常）
		private function xmlloadError(evt:IOErrorEvent):void {
			evt.currentTarget.removeEventListener(IOErrorEvent.IO_ERROR,xmlloadError);
		}
		
		public function queueLoadResource(event:QueueEvent ):void 
		{
			myText.text = "loading..." + logNUm / totalfilecount * 100 +"%";
			logNUm++;
		}

		public function loadComplete(event:QueueEvent ):void
		{  
			
			camera = new Camera3D(stage.stageWidth, stage.stageHeight);
			camera.z = -800;
			var root:Group = createScene();
			scene = new Scene3D( "scene", this, camera, root );
			addEventListener( Event.ENTER_FRAME, enterFrameHandler );
			
			myText.visible = false;
		}

		// Create the scene graph based on the root Group of the scene
		private function createScene():Group
		{
			// Create the root Group
			var g:Group = new Group();
			
			var team:TransformGroup = new TransformGroup("SmllTeam");
			var i:int = 0;
	
			var materialAttr:MaterialAttributes = new MaterialAttributes(  
				new LineAttributes( 0, 0xD7D79D, 0 ), 
				new LightAttributes( true, 0.1) 
            ); 

            var material:Material = new ColorMaterial( 0xD7D79D, 1, materialAttr ); 
            material.lightingEnable = true; 
            var app:Appearance = new Appearance( material ); 
		
			var num:int = 60;    
            var anglePer:Number = ((Math.PI * 2) * 5 ) / num; 
			
			for each (var item:Object in queue.data)
			{
				var plane:Box = new Box(item.name, 40,30,1,"quad");	
				plane.addEventListener(MouseEvent.MOUSE_OVER, imgMouseOverHandler);
				plane.addEventListener(MouseEvent.MOUSE_OUT, imgMouseOutHandler);
				plane.addEventListener(MouseEvent.MOUSE_DOWN, imgMouseDownHandler);
				
				plane.enableBackFaceCulling = false;
				plane.enableEvents = true;
				var btm:BitmapMaterial = new BitmapMaterial(item.bitmapData);
				plane.appearance = app;
				
				plane.aPolygons[0].appearance = new Appearance(btm);
				plane.aPolygons[1].appearance = new Appearance(btm);	
				
				//plane.x = i * (20+1) -115;
				plane.y = - 165 + i*8;
				
				plane.x = Math.cos(i * anglePer ) *100;   
				plane.z = Math.sin(i * anglePer ) * 100;   
				//p.y = 120 * j;//改变y轴坐标 实现圆柱的效果  
				plane.rotateY = (i*anglePer) * (180/Math.PI) + 270;   	
				team.addChild(plane);
				
				i++;
			}
			
			
			g.addChild(team);
			return g;
		}
		
		private function imgMouseOverHandler(pEvt:Shape3DEvent):void
		{
			if (sObj&&sObj.name!= pEvt.shape.name) sObj.rotateY = 0;
			sObj = pEvt.shape;
		}
		
		private function imgMouseOutHandler(pEvt:Shape3DEvent):void
		{
			if (sObj && sObj.name != pEvt.shape.name) pEvt.shape.rotateY = 0;
		}
		
		private function imgMouseDownHandler(pEvt:Shape3DEvent):void
		{
			//pEvt.shape.x += 12;
			
		}

		// The Event.ENTER_FRAME event handler tells the Scene3D to render
		private function enterFrameHandler( event : Event ) : void
		{
			var myScene:Scene3D = this.scene;
			var group:Group = myScene.root;
			
			var m:TransformGroup = (group.getChildByName("SmllTeam") as TransformGroup);
			
			if (m)
			{
				//m.rotateX += 1;
				m.rotateY += 1;
			}
			/*
			var Bbox:Box = (group.getChildByName("BigBox") as Box);
			
			if (Bbox)
			{
				Bbox.rotateX += 1;
				Bbox.rotateY += 1;
			}
			*/
			
			/*
			for each (var item:Object in queue.data)
			{
				var plane:Box = (group.getChildByName(item.name) as Box);
				if( plane )
				{
					//plane.rotateY += 10;
				}
			}
			*/
			
			if (sObj)
			{
				sObj.rotateY += 6;
			}
			
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