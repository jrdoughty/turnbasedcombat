using Godot;
using System;

[GlobalClass]
public partial class Player : Resource, ICharacter
{
	[Export] public string name { get; set; }
	[Export] public int health { get; set; }
	[Export] public int mana { get; set; }
	[Export] public int level { get; set; }
	[Export] public int experience { get; set; }
	[Export] public int attack { get; set; }
	[Export] public int defense { get; set; }
	[Export] public int speed { get; set; }
	[Export] public int magicDefense { get; set; }
	[Export] public int magicAttack { get; set; }
	[Export] public string they { get; set; }
	[Export] public string them { get; set; }
	[Export] public string theirs { get; set; }
	[Export] public string their { get; set; }
	[Export] public string selves { get; set; }

	[Export] public Texture2D playerSprite { get; set; }
	[Export] public Godot.Collections.Array playerActions { get; set; } = new Godot.Collections.Array();

	public int getNextLevelExperience()
	{
		return (int)(Math.Pow(level, 2) * 4 + 10);
	}
	public int getExperience(int lvl)//meant to be run for each challenger in battle
	{
		return (int)(Math.Pow(lvl, 1.5) * 2.6 + 10);
	}
}
