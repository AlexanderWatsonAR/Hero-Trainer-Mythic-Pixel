using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace HeroTrainer_MythicPixel
{
	public class AppMain
	{
		private static Sce.PlayStation.HighLevel.GameEngine2D.Scene 	gameScene;
		private static Sce.PlayStation.HighLevel.UI.Scene 				uiScene;
		private static Sce.PlayStation.HighLevel.UI.Label				healthLabel;
		
		private static Warrior 											hero;
		private static Enemy											monster;
		private static Timer											tDamage; 
		
		public static void Main (string[] args)
		{
			Initialize();
			tDamage = new Timer();
			tDamage.Reset();
			//Game loop.
			bool quitGame = false;
			while (!quitGame)
			{
				Update();
				
				Director.Instance.Update();
				Director.Instance.Render();
				UISystem.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
		}
		
		public static void Initialize ()
		{
			// Set up the graphics system
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			//Set the ui scene.
			uiScene 	 = new Sce.PlayStation.HighLevel.UI.Scene();
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			
			healthLabel = new Sce.PlayStation.HighLevel.UI.Label();
			healthLabel.HorizontalAlignment = HorizontalAlignment.Left;
			healthLabel.VerticalAlignment = VerticalAlignment.Top;
			
			healthLabel.Font = new UIFont("/Application/fonts/8bitlim.ttf",
			                              28, Sce.PlayStation.Core.Imaging.FontStyle.Regular);
			
			healthLabel.TextShadow 		 = new TextShadowSettings();
			healthLabel.TextShadow.Color = new UIColor(0.0f,1.0f,0.0f,1.0f);
			healthLabel.TextShadow.HorizontalOffset = 2.0f;
			healthLabel.TextShadow.VerticalOffset 	= 2.0f;

			healthLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - healthLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - healthLabel.Height/2);
			
			healthLabel.Text = "Health  50";
			
			panel.AddChildLast(healthLabel);
			uiScene.RootWidget.AddChildLast(panel);
			UISystem.SetScene(uiScene);	
			
			hero 	= new Warrior(gameScene);
			monster = new Enemy(gameScene);
			
			//Run the scene.
			Director.Instance.RunWithScene(gameScene, true);
			
			
		}
		
		public static void Combat()
		{
			if(tDamage.Seconds() > hero.GetHaste)
			{
				monster.GetHealth -= hero.GetStrength / 2;		
				healthLabel.Text = "Health  " + monster.GetHealth.ToString();
				tDamage.Reset();
			}
			
		}
		
		public static void Update ()
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
			
			//Update the hero
			hero.Update(0.0f);
			//Update the enemy
			monster.Update(0.0f);
			Combat();
		}
	}
}
