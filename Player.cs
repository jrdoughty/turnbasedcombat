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
}
