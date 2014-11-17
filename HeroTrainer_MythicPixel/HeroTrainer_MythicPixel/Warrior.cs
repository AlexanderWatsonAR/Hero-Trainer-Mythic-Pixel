using System;
using System.IO;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace HeroTrainer_MythicPixel
{
	public class Warrior
	{
		struct Stats
		{
			public float strength;
			public float luck;
			public float haste;
			public float opportunity;
		};
		
		private static SpriteUV		bob;
		private static TextureInfo	bobInfo;
		private static Stats		hero;
		
		//Accessor
		public Stats Statistics { get{return hero;} set{hero = value;} }
		
		public Warrior (Scene gameScene)
		{
			StringReader reader = new StringReader("/Application/statistics/character.txt");
			bobInfo 			= new TextureInfo("/Application/textures/bob.png");
			hero 				= new Stats();
			bob					= new SpriteUV(bobInfo);
			bob.Position		= new Vector2(Director.Instance.GL.Context.GetViewport().Width/2,
			                            	  Director.Instance.GL.Context.GetViewport().Height/2);
			
			while(reader.ReadLine() != null)
			{
				// The file reader code broke so I got rid of it
			}
			
			gameScene.AddChild(bob);	
		}
		
		public void Dispose()
		{
			bobInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{
		}
	}
}

