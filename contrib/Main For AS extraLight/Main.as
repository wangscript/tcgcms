package
{
   import flash.display.*; 
   import flash.events.*;
   import flash.ui.*;
   import flash.net.URLRequest;
   import sandy.core.light.Light3D;
   
   import sandy.core.Scene3D;
   import sandy.core.data.*;
   import sandy.core.scenegraph.*;
   import sandy.materials.*;
   import sandy.materials.attributes.*;
   import sandy.primitive.*;
   import sandy.parser.*;
   import sandy.util.*;
   import sandy.events.*;
   import sandy.view.*;
   import sandy.materials.extraLight.XtraOLightMat;
   import sandy.materials.extraLight.BoardLoader;
   import sandy.materials.extraLight.Precision;

   public class Main extends Sprite 
   {
		private var _sandySurface:Sprite;
		private var _resolution:int;
		
		private var _group:Group;
		private var _transformGroup: TransformGroup;
		private var _parser:IParser;
		private var _camera:Camera3D;

		private var _xtraMaterial:XtraOLightMat;
		private var _appear:Appearance;
		private var _mesh:Shape3D;

		private var _aLightSprite:Array;
		private const _nLights:int = 5;
		private const _boardsFolderURL:String = "meshs/model/map/alpha";
		private const _ambientMapURL:String = "meshs/model/map/ambient/ambient.png";
		private const _diffuseMapURL:String = "meshs/model/map/diffuse/diffuse.png";

		private var scene:Scene3D;
		private var camera:Camera3D;

		private var queue:LoaderQueue;
		private var parserStack:ParserStack;
		private var speed:Number = 0;
		private var angle:Number = 0;
		private var one:Shape3D;
		private var _boardLoader:BoardLoader;

		
      public function Main()
      { 
			var parser:IParser = Parser.create("meshs/model/ase/model.ase", Parser.ASE, 1);
			parserStack = new ParserStack();
			parserStack.add("meshParser",parser);
			parserStack.addEventListener(ParserStack.COMPLETE, parserComplete );
			parserStack.start();
	  }
	  
	  private function parserComplete(pEvt:Event ):void
	  {
		_mesh = parserStack.getGroupByName("meshParser").children[0] as Shape3D;
		loadSkins();
	  }
	  
	  
	  private function loadSkins():void
	  {
		queue = new LoaderQueue();

		queue.add( "ambientMapURL", new URLRequest(_ambientMapURL) );
		queue.add( "diffuseMapURL", new URLRequest(_diffuseMapURL) );

		queue.addEventListener(SandyEvent.QUEUE_COMPLETE, loadComplete );
		queue.start();
	  }
      
	  private function loadBoards():void
		{
			// we create a new BoardLoader object with 4 parameters
			_boardLoader = new BoardLoader(Precision.PRECISION_2, 256, 256, false);
			// - nframes is the number of frames the board contains
			//      don't worry about this, just use the static Precision class
			//      with the precision value matching the "precision sampling"
			//      value we set in the Extralight exporter and all will be OK
			//
			// - frameWidth and frameHeight are the dimensions of a single texture
			//      just put "Single texture size" we set in the Extralight exporter
			//
			// - alpha allows alpha int he board
			//      the BoardLoader manages the alpha for generality purpose
			//      but always put false when work with Extralight
			
			// listen to the COMPLETE event (we could listen to the PROGRESS too)
			_boardLoader.addEventListener(Event.COMPLETE, onBoardComplete);
			
			// launch the load with the URL of the folder containing the boards
			// this is the folder we set as "folder destination" in the Extralight exporter
			_boardLoader.load(_boardsFolderURL);
		}
		
		private function onBoardComplete(evt:Event):void
		{
			camera = new Camera3D(stage.stageWidth,stage.stageHeight);
			camera.y = 40;
			
			
			
		 //camera.viewport = new ViewPort(400,300);
		 var root:Group = createScene();
		 scene = new Scene3D( "scene", this, camera, root );
		 
		 // Listen to the heart beat and render the scene
		 addEventListener( Event.ENTER_FRAME, enterFrameHandler );
		
		 
		}
		
	  // Create the scene graph based on the root Group of the scene
      private function loadComplete(event:QueueEvent):void
      {
		  loadBoards()
		 
		}
	  
	  private function createScene():Group
	  {
		 // Create the root Group
		 var g:Group = new Group();
		 
			_transformGroup = new TransformGroup("myTransformGroup");
			
			// creation of a XtraOLightMat material with 6 parameters
			_xtraMaterial = new XtraOLightMat(2, 256, 256, true, 0, 300);
			// - precision matchs the "Precision sampling value" we set in the Extralight exporter
			// - colorTexture dimensions are the dimension of both the ambient and diffuses textures (must equals)
			// - allowDistance sets if the lights loose energy with distance
			// - distances values set the range distance of non null intensity (seems to work well but need deep tests)
			
			
			
			// now it's time to give the textures to the XtraOLightMat:
			// so we give directly the boardloader
			_xtraMaterial.boardLoader = _boardLoader;
			// and the ambient and diffuse textures
			_xtraMaterial.setColorTextures(queue.data["ambientMapURL"].bitmapData
											 ,queue.data["diffuseMapURL"].bitmapData);
			
			
			// we create a Sandy appearance
			_appear = new Appearance(_xtraMaterial);
			
			// and set the appearance to the mesh
			_mesh.appearance = _appear;
			
			// the XtraOLightMat must know himself the mesh where he's applied (to perform calculations)
			_xtraMaterial.owner = _mesh;
			
			
			// Sandy way to manage the scenegraph
			_transformGroup.addChild(_mesh);
			_mesh.x = 0;
			_mesh.z = 0;
			_mesh.y = 0;
			
			// we need candidates to play lights roles in the scene:
			// some Sprite2D will work perfectly
			// (and it could be any Sandy ATransformable object)
			_aLightSprite = new Array();
			var i:int;
			for (i=0; i<_nLights; i++) {
				_aLightSprite[i] = new Sprite2D("light" + i, new Sprite());
				Sprite(_aLightSprite[i].container).graphics.beginFill(0xFFFFFF);
				Sprite(_aLightSprite[i].container).graphics.drawCircle(0, 0, 2);
				Sprite(_aLightSprite[i].container).graphics.endFill();
				
				// here we register our light to the XtraOLightMat
				_xtraMaterial.addEmissiveObject(_aLightSprite[i]);
				
				_transformGroup.addChild(_aLightSprite[i]);
			}
			
		 g.addChild( _transformGroup );
		 
		 return g;
      }

       // The Event.ENTER_FRAME event handler tells the world to render
      private function enterFrameHandler( event : Event ) : void
      {
			var myMouseXOffset:Number = mouseX - stage.stageWidth / 2;
			var myMouseYOffset:Number = mouseY - stage.stageHeight / 2;
			
			// OK so just move all these lights in a cool manner (so here I done it simply)
			var i:int;
			for (i=0; i<_nLights; i++) {
				_aLightSprite[i].x = Math.sin((myMouseXOffset + i*myMouseXOffset)/100)*50;
				_aLightSprite[i].z = Math.cos((myMouseXOffset + i*myMouseXOffset)/100)*50;
				_aLightSprite[i].y = (myMouseYOffset + i*myMouseYOffset)/10;
			}
			
			camera.x = 200 * Math.cos(myMouseXOffset/200);
			camera.z = 200 * Math.sin(myMouseXOffset/200);
			camera.y = myMouseYOffset/2;
			camera.lookAt(0, 0, 0);
			
			// don't forget to update the XtraOLightMat, results will be better ;)
			_xtraMaterial.update();
			
			// update the Sandy world
			
			scene.render();
      }
	  
	  
   }
}