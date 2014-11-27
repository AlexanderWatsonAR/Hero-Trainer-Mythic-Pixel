using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace HeroTrainer_MythicPixel
{
	public class FightScreen: GameState 
	{
		private Sce.PlayStation.HighLevel.UI.Label							healthLabel;
		private Sce.PlayStation.HighLevel.UI.Label[]						heroLabel;
		
		private Warrior 													hero;
		private Enemy														monster;
		private Timer														tDamage; 
			
		private const int		LABEL_COUNT = 5;
		
		public FightScreen (AppMain instance) : base(instance)
		{
		}
		public override void LoadContent()
		{
			tDamage = new Timer();
			tDamage.Reset();
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			hero 	  = new Warrior(gameScene);
			monster   = new Enemy(gameScene);
			
			//Set the ui scene.
			uiScene 	 	= new Sce.PlayStation.HighLevel.UI.Scene();
			
			Panel topPanel  = new Panel();
			topPanel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			topPanel.Height = Director.Instance.GL.Context.GetViewport().Height;
			
			Panel leftPanel  = new Panel();
			leftPanel.Width  = 0;
			leftPanel.Height = Director.Instance.GL.Context.GetViewport().Height/2;
			
			//Monster health label.
			healthLabel = new Sce.PlayStation.HighLevel.UI.Label();
			healthLabel.HorizontalAlignment = HorizontalAlignment.Left;
			healthLabel.VerticalAlignment = VerticalAlignment.Top;
			healthLabel.Font 	   = theGame.BodyText;
			healthLabel.TextShadow = theGame.BodyTextShadow;

			healthLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - healthLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - healthLabel.Height/2);
			
			healthLabel.Text = "Enemy Health  50";
			
			//Hero labels.
			heroLabel = new Sce.PlayStation.HighLevel.UI.Label[LABEL_COUNT];
			
			for(int i = 0; i < LABEL_COUNT; i++)
			{
				heroLabel[i] = new Sce.PlayStation.HighLevel.UI.Label();
			}
			
			for(int i = 0; i < LABEL_COUNT; i++)
			{
				heroLabel[i].HorizontalAlignment = HorizontalAlignment.Left;
				heroLabel[i].VerticalAlignment 	 = VerticalAlignment.Middle;			
				heroLabel[i].Font 		= theGame.BodyText;
				heroLabel[i].TextShadow = theGame.BodyTextShadow;
				heroLabel[i].SetPosition(25,
					Director.Instance.GL.Context.GetViewport().Height/4 + (healthLabel.Height * i) + 15);
			}
			
			heroLabel[0].Text = "Name " + hero.Name;
			heroLabel[1].Text = "Strength " + hero.Strength.ToString();
			heroLabel[2].Text = "Luck " + hero.Luck.ToString();
			heroLabel[3].Text = "Haste " + hero.Haste.ToString();
			heroLabel[4].Text = "Opportunity " + hero.Opportunity.ToString();
			
			topPanel.AddChildLast(healthLabel);
			for(int i = 0; i < LABEL_COUNT; i++)
			{
				leftPanel.AddChildLast(heroLabel[i]);
			}
			
			uiScene.RootWidget.AddChildFirst(topPanel);
			uiScene.RootWidget.AddChildLast(leftPanel);
			UISystem.SetScene(uiScene);	
		}
		
		public override void UnloadContent()
		{
			hero.Dispose();
			monster.Dispose();
		}
		
		public void Combat()
		{
			if(tDamage.Seconds() > hero.Haste)
			{
				monster.Health -= hero.Strength / 2;		
				healthLabel.Text = "Enemy Health  " + monster.Health.ToString();
				tDamage.Reset();
			}
			
			if(monster.Health <= 0)
			{
				//Go to next scene.
			}			
		}
		
		public override void Update()
		{
			Combat();			
			//Update the hero
			hero.Update(0.0f);
			//Update the enemy
			monster.Update(0.0f);
		}
	}
}

