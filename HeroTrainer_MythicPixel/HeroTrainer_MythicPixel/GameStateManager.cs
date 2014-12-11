using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace HeroTrainer_MythicPixel
{
	public class GameStateManager
	{
	   private static GameStateManager instance;
	
	   private GameStateManager() {}
	   private static GameState currentState;
		
	   // public read only property.
	   public static GameStateManager Instance
	   {
	      get 
	      {
	         if (instance == null)
	         {
	            instance = new GameStateManager();
	         }
				
	         return instance;
	      }
	   }
		
	   public void Update()
	   {
	   	  currentState.Update();
	   }
		
	   public void ChangeState(GameState newState)
	   {
	   	  if(currentState != null)
	   	  {
			 currentState.UnloadContent();
			 currentState = newState;
			 Director.Instance.ReplaceScene(currentState.GameScene);
	   	  }
	   	  else
	   	  {
			 currentState = newState;
			 Director.Instance.RunWithScene(currentState.GameScene, true);
	   	  }
	   }
	}
}