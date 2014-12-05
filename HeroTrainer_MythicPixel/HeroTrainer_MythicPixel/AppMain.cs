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
	public static class AppMain
	{	
		private static GameState currentState;
		//private static AppMain theGame = new AppMain();
		
		private static Sce.PlayStation.HighLevel.UI.UIFont					bodyText;
		private static Sce.PlayStation.HighLevel.UI.TextShadowSettings		bodyTextShadow;
		
		//Accessors
		public static Sce.PlayStation.HighLevel.UI.UIFont BodyText{ get{return bodyText;} }
		public static Sce.PlayStation.HighLevel.UI.TextShadowSettings BodyTextShadow{ get{return bodyTextShadow;} }
		public static GameState State { get{return currentState;} set{currentState = value;} }
		
		//public static AppMain(){}
		
		public static void Main(string[] args)
		{	
			//currentState = new FightScreen();
			Initialize();
			GameStateManager.Instance.ChangeState(new FightScreen());
			
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
			UnloadContent();
			Director.Terminate();
		}
		
		public static void Initialize ()
		{
			// Set up the graphics system.
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			Font ();
			LoadContent();
		}
		
		public static void Font()
		{
			//Assign details for font.		
			bodyText 	= new UIFont("/Application/fonts/8bitlim.ttf", 28,
			              Sce.PlayStation.Core.Imaging.FontStyle.Regular);
			bodyTextShadow 		 			= new TextShadowSettings();
			bodyTextShadow.Color 			= new UIColor(0.0f,0.0f,1.0f,1.0f);
			bodyTextShadow.HorizontalOffset = 2.0f;
			bodyTextShadow.VerticalOffset   = 2.0f;
		}
		
		public static void LoadContent()
		{		
		}
		
		public static void UnloadContent()
		{
		}
		
		public static void Update ()
		{
			GameStateManager.Instance.Update();
		}
	}
}
