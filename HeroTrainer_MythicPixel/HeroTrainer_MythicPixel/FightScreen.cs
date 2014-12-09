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
		private Sce.PlayStation.HighLevel.UI.Label							fightLabel;
		
		private Warrior 													hero;
		private Enemy														monster;
		private Timer														tDamage; 
			
		private const int		LABEL_COUNT = 6;
		
		public FightScreen () : base()
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
			monster   = new Enemy(gameScene, hero);
			
			//Set the ui scene.
			uiScene 	 	= new Sce.PlayStation.HighLevel.UI.Scene();
			
			Panel topPanel  = new Panel();
			topPanel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			topPanel.Height = Director.Instance.GL.Context.GetViewport().Height;
			
			Panel leftPanel  = new Panel();
			leftPanel.Width  = 0;
			leftPanel.Height = Director.Instance.GL.Context.GetViewport().Height/2;
			
			
			Panel bottomPanel = new Panel();
			bottomPanel.Width = Director.Instance.GL.Context.GetViewport().Width/2;
			bottomPanel.Height = Director.Instance.GL.Context.GetViewport().Height/2;
			
			
			//Monster health label.
			healthLabel = new Sce.PlayStation.HighLevel.UI.Label();
			healthLabel.HorizontalAlignment = HorizontalAlignment.Left;
			healthLabel.VerticalAlignment = VerticalAlignment.Top;
			//healthLabel.Font 	   = theGame.BodyText;
			//healthLabel.TextShadow = theGame.BodyTextShadow;

			healthLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - healthLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - healthLabel.Height/2);
			
			//healthLabel.Text = "Enemy Health " + monster.Health.ToString("#.#");
			healthLabel.Text = "Enemy Health " + (int)monster.Health;
			
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
				//heroLabel[i].Font 		= theGame.BodyText;
				//heroLabel[i].TextShadow = theGame.BodyTextShadow;
				heroLabel[i].SetPosition(25,
					Director.Instance.GL.Context.GetViewport().Height/4 + (healthLabel.Height * i) + 15);
			}
			
			//Fight labels
			fightLabel = new Sce.PlayStation.HighLevel.UI.Label();
			fightLabel.HorizontalAlignment = HorizontalAlignment.Center;
			fightLabel.VerticalAlignment = VerticalAlignment.Middle;
			fightLabel.Width = 960;
			fightLabel.SetPosition(0, 400);
			
			UpdateStatLabels();
			
			topPanel.AddChildLast(healthLabel);
			for(int i = 0; i < LABEL_COUNT; i++)
			{
				leftPanel.AddChildLast(heroLabel[i]);
			}
			
			bottomPanel.AddChildLast(fightLabel);
			
			uiScene.RootWidget.AddChildFirst(topPanel);
			uiScene.RootWidget.AddChildLast(leftPanel);
			uiScene.RootWidget.AddChildLast(bottomPanel);
			UISystem.SetScene(uiScene);	
		}
		
		public override void UnloadContent()
		{
			hero.Dispose();
			monster.Dispose();
			
		}
		
		public override void Combat()
		{
			if(tDamage.Milliseconds() > (hero.Haste))
			{
				fightLabel.Text = "";
				Console.WriteLine ("Health before line 108 " + monster.Health);
				monster.Health -= ResolveDamage();

				//healthLabel.Text = "Enemy Health  " + monster.Health.ToString("#.#");
				
				Console.WriteLine ("Health after line 108 = " + monster.Health);

				healthLabel.Text = "Enemy Health  " + (int)monster.Health;
				
				UpdateStatLabels();
								
				tDamage.Reset();
			}
						
			if (monster.Health <= 0)
					KillMonster();
		}
		
		public float ResolveDamage()
		{
			float damage;
			damage = hero.Strength;
			
			bool critCheck = CheckIfCrit();
			
			if (critCheck)
				damage = damage * (2 + (hero.Opportunity / 10));
			
			return damage;
			
		}
		
		public bool CheckIfCrit()
		{
			Random critCheck = new Random();
			
			float critFloat = critCheck.Next(100);
			
			if (critFloat <= hero.Luck)
			{
				fightLabel.Text = ("You critically hit the monster!");
		
				return true;
			}	
			else
				return false;
			
			
		}	
		
		public float GetHeroPower()
		{
			//Calculates the power of the hero based on its upgrades
			float heroPower = ((hero.Strength + hero.Luck + (999 - hero.Haste / 5) + hero.Opportunity) / 325);
			
			return heroPower;		
		}	
		
		public void KillMonster()
		{
			//Reset monster status
			monster.Health = monster.ScaleHealth();	
			Console.WriteLine ("Health Reset");
			
			//Award gold to player
			hero.Gold += GoldReward();
		}	
		
		public int GoldReward()
		{
			Random tempGold = new Random();
			
			int goldInt = tempGold.Next(9) + 1;
			
			fightLabel.Text = ("You killed the monster! It dropped " + goldInt + " gold!");
			
			return goldInt;		
		}	
		
		
		public override void Update()
		{		
			//Update the hero
			hero.Update(0.0f);
			//Update the enemy
			monster.Update(0.0f);
		}
			
		
		public void UpdateStatLabels()
		{
			float tempHaste;
			
			tempHaste = (1000 - hero.Haste);
			
			heroLabel[0].Text = "Name " + hero.Name;
			heroLabel[1].Text = "Strength " + hero.Strength.ToString();
			heroLabel[2].Text = "Luck " + hero.Luck.ToString();
			heroLabel[3].Text = "Haste " + tempHaste.ToString();
			heroLabel[4].Text = "Opportunity " + hero.Opportunity.ToString();
			heroLabel[5].Text = "Gold " + hero.Gold.ToString();
			
		}
			
		
	}
}

