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
		public static void Main(string[] args)
		{	
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
			Director.Terminate();
		}
		
		public static void Initialize ()
		{
			// Set up the graphics system.
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
		}
		
		public static void Update ()
		{
			GameStateManager.Instance.Update();
		}
	}
}
