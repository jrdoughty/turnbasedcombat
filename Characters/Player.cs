using Godot;
using System;

[GlobalClass]
public partial class Player : Resource
{
    [Export] public string name;
    [Export] public int health;
    [Export] public int mana;
    [Export] public int level;
    [Export] public int experience;
    [Export] public int attack;
    [Export] public int defense;
    [Export] public int speed;
    [Export] public int magicDefense;
    [Export] public int magicAttack;

    [Export] public Texture2D playerSprite;
    [Export] public Godot.Collections.Array playerActions = new Godot.Collections.Array();

    public int getNextLevelExperience()
    {
        return (int)(Math.Pow(level, 2) * 4 + 10);
    }
    public int getExperience(int lvl)//meant to be run for each challenger in battle
    {
        return (int)(Math.Pow(lvl, 1.5) * 2.6 + 10);
    }
}
