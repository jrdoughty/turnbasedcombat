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
        PlayerControls.GetNode<Button>("Button").Pressed += Spell1;
        PlayerControls.GetNode<Button>("Button2").Pressed += Spell2;
        PlayerControls.GetNode<Button>("Button3").Pressed += Spell3;
    }

    public void Spell1()
    {
        // Handle spell 1 action
        GD.Print("Spell 1 casted!");
        GetParent<Game>().gameStateMachine.ChangeState("Casting");
    }
    public void Spell2()
    {
        // Handle spell 2 action
        GD.Print("Spell 2 casted!");
        GetParent<Game>().gameStateMachine.ChangeState("Casting");
    }
    public void Spell3()
    {
        // Handle spell 3 action
        GD.Print("Spell 3 casted!");
        GetParent<Game>().gameStateMachine.ChangeState("Casting");
    }
}
