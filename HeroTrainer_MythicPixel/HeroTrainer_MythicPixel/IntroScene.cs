using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace HeroTrainer_MythicPixel
{
	public class IntroScene: GameState
	{
		private Sce.PlayStation.HighLevel.UI.Label							introLabel;
		
		public IntroScene (AppMain instance) : base(instance)
		{
		}
		
		public override void LoadContent()
		{
			//Monster health label.
			introLabel = new Sce.PlayStation.HighLevel.UI.Label();
			introLabel.HorizontalAlignment = HorizontalAlignment.Center;
			introLabel.VerticalAlignment = VerticalAlignment.Middle;
			introLabel.Font 	  = theGame.BodyText;
			introLabel.TextShadow = theGame.BodyTextShadow;

			introLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - introLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - introLabel.Height/2);
			
			introLabel.Text = " Intro Scene.";
		}
		
		public override void Update ()
		{
		}
	}
}

