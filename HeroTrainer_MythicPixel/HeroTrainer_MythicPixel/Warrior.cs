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
		//Accessor
		public float  Strength{ get{return strength;} set{strength = value;} }
		public float  Luck{ get{return luck;} set{luck = value;} }
		public float  Haste{ get{return haste;} set{haste = value;} }
		public float  Opportunity{ get{return opportunity;} set{opportunity = value;} }
		public int 	  Gold{ get{return gold;} set{gold = value;} }
		public string Name{ get{return name;} }
		
		private static float  strength;
		private static float  luck;
		private static float  haste;
		private static float  opportunity;
		private static int	  gold;
		private static string name;
		
		private const string path = "/Application/statistics/character.txt";
		
		private static SpriteUV		bob;
		private static TextureInfo	bobInfo;
		
		public Warrior (Scene gameScene)
		{
			bobInfo 			= new TextureInfo("/Application/textures/bob.png");
			bob					= new SpriteUV(bobInfo);
			bob.Position		= new Vector2(Director.Instance.GL.Context.GetViewport().Width/4,
			                            	  Director.Instance.GL.Context.GetViewport().Height/2);
			
			bob.Scale = bobInfo.TextureSizef;
			Load();
			gameScene.AddChild(bob);	
		}
		
		public void Dispose()
		{
			bobInfo.Dispose();
		}
		
		public static void Load()
		{
			Console.WriteLine(File.Exists(path)? "File exists." : "File does not exist.");
			using (StreamReader reader = new StreamReader(path))
			{
				string line = reader.ReadLine();
				int count = int.Parse(line);
				for(int i = 0; i < count; i++)
				{
					if(line.IndexOf('s') == 0)
					{
						line = line.TrimStart('s');
						strength = float.Parse(line);
					}
					
					else if(line.IndexOf('l') == 0)
					{
						line = line.TrimStart('l');
						luck = float.Parse(line);
					}
					
					else if(line.IndexOf('h') == 0)
					{
						line = line.TrimStart('h');
						haste = float.Parse(line);
					}
					
					else if(line.IndexOf('o') == 0)
					{
						line = line.TrimStart('o');
						opportunity = float.Parse(line);
					}
					
					else if(line.IndexOf ('g') == 0)
					{
						line = line.TrimStart ('g');
						gold = int.Parse (line);						
					}
					
					else
					{
						name = line;
					}
					
					line = reader.ReadLine();
				}
				reader.Close();
			}
		}
		
		//public void Save()
		//{
		//	SaveStats();
		//}
		
		//public static void SaveStats()
		//{
			//Checks if the file exists.
		//	Console.WriteLine(File.Exists(path)? "File exists." : "File does not exist.");
			
			//empties the file of everything.
		//	System.IO.File.WriteAllText(@path, string.Empty);
			
			// Each array entry represents a line to be entered into the file.
		//	string[] lines = {"7", "Bob", "s " + strength, "l " + luck,
		//					 "h " + haste, "o " + opportunity, "g " + gold};
			
			// Writes into the file line by line.
		//	System.IO.File.WriteAllLines(@path, lines);
			
		//}
		
		public void Update(float deltaTime)
		{
		}
	}
}

