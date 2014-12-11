using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace HeroTrainer_MythicPixel
{
	public class IntroScreen: GameState
	{
		private Sce.PlayStation.HighLevel.UI.Label	introLabel;
		
		public IntroScreen () : base()
		{
		}
		
		public override void LoadContent()
		{
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			//Monster health label.
			introLabel = new Sce.PlayStation.HighLevel.UI.Label();
			introLabel.HorizontalAlignment = HorizontalAlignment.Center;
			introLabel.VerticalAlignment = VerticalAlignment.Middle;
			//introLabel.Font 	  = theGame.BodyText;
			//introLabel.TextShadow = theGame.BodyTextShadow;

			introLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - introLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - introLabel.Height/2);
			
			introLabel.Text = "Intro Scene";
			uiScene.RootWidget.AddChildFirst(introLabel);
			UISystem.SetScene(uiScene);
		}
		
		public override void Update ()
		{
			Input();
		}
		
		public void Input()
		{
			var touches = Touch.GetData(0);
			if(touches.Count > 0)
				GameStateManager.Instance.ChangeState(new FightScreen());
				
			
		}
	}
}

