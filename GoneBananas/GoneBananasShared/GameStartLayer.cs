using System;
using System.Collections.Generic;
using CocosSharp;

namespace GoneBananas
{
	public class GameStartLayer : CCLayer
	{
		float sx, sy;

		public GameStartLayer (float sx, float sy) : base ()
		{
			this.sx = sx;
			this.sy = sy;

			var touchListener = new CCEventListenerTouchAllAtOnce ();

			touchListener.OnTouchesEnded = (touches, ccevent) => {
				var transitionToGame = new CCTransitionRotoZoom (0.7f, GameLayer.GameScene (Window));
				Window.DefaultDirector.ReplaceScene (transitionToGame);
			};

			AddEventListener (touchListener, this);
		}

		protected override void AddedToScene ()
		{
			base.AddedToScene ();

			Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.NoBorder;

			var label = new CCLabel ("Tap Screen to Go Bananas!", "arial", 22) { //TODO: Scale font size (64 looks good on iPhone 6+)
				Position = VisibleBoundsWorldspace.Center,
				Color = CCColor3B.Black,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle
			};

			AddBackground ();
			AddChild (label);
		}

		void AddBackground ()
		{
			var grass = new CCSprite ("grass");
			grass.ScaleY = sy;
			grass.ScaleX = sx;
			grass.Position = VisibleBoundsWorldspace.Center;
			AddChild (grass);
		}

		public static CCScene GameStartLayerScene (CCWindow mainWindow)
		{
			var scene = new CCScene (mainWindow);

			var winSize = mainWindow.WindowSizeInPixels;
			const float wbase = 640.0f;
			const float hbase = 1136.0f;

			var layer = new GameStartLayer (winSize.Width / wbase, winSize.Height / hbase);

			scene.AddChild (layer);

			return scene;
		}
	}
}