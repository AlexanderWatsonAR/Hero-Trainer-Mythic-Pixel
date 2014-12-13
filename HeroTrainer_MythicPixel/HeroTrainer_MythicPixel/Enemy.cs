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
		private static Warrior		hero;
		
		public float Health { get{return health;} set{health = value;} }
		
		public Enemy (Scene gameScene, Warrior w)
		{
			enemyInfo 			= new TextureInfo("/Application/textures/enemy.png");
			enemy				= new SpriteUV(enemyInfo);
			enemy.Position		= new Vector2((Director.Instance.GL.Context.GetViewport().Width/2) + 75,
			                            	  Director.Instance.GL.Context.GetViewport().Height/2);
			hero = w;
			health = ScaleHealth();
			enemy.Scale = enemyInfo.TextureSizef;
			gameScene.AddChild(enemy);
		}
		
		public void Dispose()
		{
			enemyInfo.Dispose();
		}
		
		public float ScaleHealth()
		{
			float monsterHealth = (hero.Strength + hero.Luck + ((999 - hero.Haste) / 5) + hero.Opportunity);
			
			Console.WriteLine("Monster Scaled Health = " + monsterHealth);
			
			return monsterHealth;
		}
		
	
		public void Update(float deltaTime)
		{
		}
	}
}

