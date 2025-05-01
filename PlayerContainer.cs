using Godot;
using System;

public partial class PlayerContainer : Node2D
{
    public Label PlayerName { get; set; }
    public ProgressBar PlayerHealth { get; set; }
    public Label PlayerLevel { get; set; }
    public Sprite2D PlayerSprite { get; set; }
    public Control PlayerControls { get; set; }
    public Guid PlayerID { get; set; }

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
        PlayerID = Guid.NewGuid();
    }

    public void Spell1()
    {
        // Handle spell 1 action
        GD.Print("Spell 1 casted!");
        TBAction action = new TBAction();
        action.ActionType = "Spell";
        action.ActionName = "Spell 1";
        action.ActionDescription = "Spell 1 is cast!";
        Effect e = new Effect();
        e.EffectType = "damage";
        e.EffectName = "Dagger";
        e.EffectValue = 2;
        action.Effects.Add(e);

        GetParent<Game>().Actions.Add(action);
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
