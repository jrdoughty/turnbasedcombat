using Godot;

[GlobalClass]
public partial class Level : Resource
{
    [Export] public int health { get; set; }
	[Export] public int mana { get; set; }
	[Export] public int attack { get; set; }
	[Export] public int defense { get; set; }
	[Export] public int speed { get; set; }
	[Export] public int magicDefense { get; set; }
	[Export] public int magicAttack { get; set; }
}