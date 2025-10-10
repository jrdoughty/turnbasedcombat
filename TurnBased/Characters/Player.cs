using Godot;
using System;

[GlobalClass]
public partial class Player : Resource, ICharacter
{
	[Export] public string CharacterName { get; set; }
	[Export] public int Health { get; set; }
    [Export] public int CurrentHealth { get; set; } = -1;//default to -1 to check if it has been set
	[Export] public int Mana { get; set; }
    [Export] public int Level { get; set; } = 0;
	[Export] public int Experience { get; set; }
	[Export] public int Attack { get; set; }
	[Export] public int Defense { get; set; }
	[Export] public int Speed { get; set; }
	[Export] public int MagicDefense { get; set; }
	[Export] public int MagicAttack { get; set; }
	[Export] public string They { get; set; }
	[Export] public string Them { get; set; }
	[Export] public string Theirs { get; set; }
	[Export] public string Their { get; set; }
	[Export] public string Selves { get; set; }

	[Export] public PackedScene PlayerSprite { get; set; }
	[Export] public Godot.Collections.Array<string> PlayerActions { get; set; } = new Godot.Collections.Array<string>();

	public int getNextLevelExperience()
	{
		return (int)(Math.Pow(Level, 2) * 4 + 10);
	}
	public int getExperience(int lvl)//meant to be run for each challenger in battle
	{
		return (int)(Math.Pow(lvl, 1.5) * 2.6 + 10);
	}
}
