using Godot;
using System;

public partial class PlayerContainer : Node2D
{
    public Label PlayerName { get; set; }
    public ProgressBar PlayerHealth { get; set; }
    public Label PlayerLevel { get; set; }
    public Sprite2D PlayerSprite { get; set; }
    public Control PlayerControls { get; set; }

    public override void _Ready()
    {
        // Initialize the player container
        GD.Print("PlayerContainer is ready!");
        PlayerName = GetNode<Label>("PlayerName");
        PlayerHealth = GetNode<ProgressBar>("PlayerHealth");
        PlayerLevel = GetNode<Label>("PlayerLevel");
        PlayerSprite = GetNode<Sprite2D>("PlayerSprite");
        PlayerControls = GetNode<Control>("PlayerControls");
    }
}
