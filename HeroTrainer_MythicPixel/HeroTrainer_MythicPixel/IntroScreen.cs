using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace HeroTrainer_MythicPixel
{
	public class IntroScreen: GameState
	{
		private Sce.PlayStation.HighLevel.UI.Label	titleLabel;
		private Sce.PlayStation.HighLevel.UI.Label	tapLabel;
		private SpriteUV 							background;
		private TextureInfo							backgroundInfo;
		
		private static Sce.PlayStation.HighLevel.UI.UIFont					bodyText;
		private static Sce.PlayStation.HighLevel.UI.TextShadowSettings		bodyTextShadow;
		
		private static Sce.PlayStation.HighLevel.UI.UIFont					headText;
		private static Sce.PlayStation.HighLevel.UI.TextShadowSettings		headTextShadow;
		private SoundPlayer player;
		
		public IntroScreen () : base()
		{
		}
		
		public override void LoadContent()
		{
			player = new Sound("/Application/sounds/growl.wav").CreatePlayer();
			player.Play();
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			//start screen label.
			Font ();
			titleLabel = new Sce.PlayStation.HighLevel.UI.Label();
			titleLabel.HorizontalAlignment = HorizontalAlignment.Center;
			titleLabel.VerticalAlignment = VerticalAlignment.Middle;
			titleLabel.Font 	  = headText;
			titleLabel.TextShadow = headTextShadow;
			titleLabel.Width = 500;
			titleLabel.Height = 100;
			
			tapLabel = new Sce.PlayStation.HighLevel.UI.Label();
			tapLabel.HorizontalAlignment = HorizontalAlignment.Center;
			tapLabel.VerticalAlignment = VerticalAlignment.Middle;
			tapLabel.Font 	  	= bodyText;
			tapLabel.TextShadow = bodyTextShadow;
			tapLabel.Width = 200;
			tapLabel.Height = 300;
			
			backgroundInfo 			= new TextureInfo("/Application/textures/background.png");
			background				= new SpriteUV(backgroundInfo);
			background.Position		= new Vector2(0,0);
			
			background.Scale = backgroundInfo.TextureSizef;
			gameScene.AddChild(background);

			titleLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - titleLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - titleLabel.Height/2);
			
			titleLabel.Text = "Hero Trainer";
			
			tapLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - tapLabel.Width/2,
				(Director.Instance.GL.Context.GetViewport().Height/2)-30);
			
			tapLabel.Text = "Tap anywhere     to    start";
			
			uiScene.RootWidget.AddChildFirst(titleLabel);
			uiScene.RootWidget.AddChildLast(tapLabel);
			UISystem.SetScene(uiScene);
		}
		public static void Font()
		{
			//Assign details for font.		
			bodyText 	= new UIFont("/Application/fonts/8bitOperatorPlus8-Regular.ttf", 36,
			              Sce.PlayStation.Core.Imaging.FontStyle.Regular);
			bodyTextShadow 		 			= new TextShadowSettings();
			bodyTextShadow.Color 			= new UIColor(0,0,1,1);
			bodyTextShadow.HorizontalOffset = 2.0f;
			bodyTextShadow.VerticalOffset   = 2.0f;
			
			
			headText 	= new UIFont("/Application/fonts/8bitOperatorPlus8-Regular.ttf", 50,
			              Sce.PlayStation.Core.Imaging.FontStyle.Regular);
			headTextShadow 		 			= new TextShadowSettings();
			headTextShadow.Color 			= new UIColor(1,0,0,1);
			headTextShadow.HorizontalOffset = -2.0f;
			headTextShadow.VerticalOffset   = 2.0f;
		}
		
		public override void Update ()
		{
			Input();
		}
		
		public override void UnloadContent()
		{
			backgroundInfo.Dispose();
		}
		
		public void Input()
		{
			var touches = Touch.GetData(0);
			if(touches.Count > 0)
			{
				GameStateManager.Instance.ChangeState(new FightScreen());
				player.Stop();
			}	
		}
	}
}

