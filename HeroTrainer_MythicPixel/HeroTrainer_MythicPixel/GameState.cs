using System;

namespace HeroTrainer_MythicPixel
{
	public class GameState
	{	
		protected static Sce.PlayStation.HighLevel.GameEngine2D.Scene 		gameScene;
		protected static Sce.PlayStation.HighLevel.UI.Scene 				uiScene;
		
		//Accessor
		public Sce.PlayStation.HighLevel.GameEngine2D.Scene GameScene{get{return gameScene;}}
		public Sce.PlayStation.HighLevel.UI.Scene UIScene{get{return uiScene;}}
		
		
   		public GameState()
		{
			LoadContent();
		}
		
		public virtual void LoadContent() {}
		
		public virtual void UnloadContent(){}
		
		public virtual void Combat(){}
		
   		public virtual void Update(){}
	}
}


