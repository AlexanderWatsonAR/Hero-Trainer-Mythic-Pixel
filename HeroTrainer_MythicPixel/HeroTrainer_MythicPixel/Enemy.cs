using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace HeroTrainer_MythicPixel
{
	public class Enemy
	{
		private static SpriteUV		enemy;
		private static TextureInfo	enemyInfo;
		private static float 		health;
		
		public float Health { get{return health;} set{health = value;} }
		
		public Enemy (Scene gameScene)
		{
			enemyInfo 			= new TextureInfo("/Application/textures/monster.png");
			enemy				= new SpriteUV(enemyInfo);
			enemy.Position		= new Vector2((Director.Instance.GL.Context.GetViewport().Width/2) + 75,
			                            	  Director.Instance.GL.Context.GetViewport().Height/2);
			health = 50.0f;
			enemy.Scale = enemyInfo.TextureSizef;
			gameScene.AddChild(enemy);
		}
		
		public void Dispose()
		{
			enemyInfo.Dispose();
		}
		public void Update(float deltaTime)
		{
		}
	}
}

