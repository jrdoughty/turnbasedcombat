using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerContainer : Node2D
{
    public Label PlayerName { get; set; }
    public ProgressBar PlayerHealth { get; set; }
    public int PlayerCurrentHealth { get; set; }
    public Label PlayerLevel { get; set; }
    public Sprite2D PlayerSprite { get; set; }
    public Control PlayerControls { get; set; }
    public Player PlayerData { get; set; }
    public Guid PlayerID { get; set; }
    public List<Effect> PlayerEffects { get; set; } = new List<Effect>();
    public bool IsActive { get; set; } = false;
    public bool IsPlayerContolled { get; set; } = false;
    public int QueueVal { get; set; } = 0;

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
        action.ActionType = "Attack";
        action.ActionName = "Dagger";
        action.ActionDescription = "She swings her dagger at the enemy.";
        Effect e = new Effect();
        e.EffectType = "Damage";
        e.EffectName = "Dagger";
        e.EffectValue = 2;
        action.Actor = this;
        action.Effects.Add(e);
        foreach (var player in GetParent<Game>().Players)
        {
            if (player != this)
            {
                action.Target = player;
            }
        }
        GetParent<Game>().SetActivePlayer(this);
        GetParent<Game>().Actions.Add(action);
        GetParent<Game>().gameStateMachine.ChangeState("Casting");
    }
    public void Spell2()
    {
        // Handle spell 3 action
        GD.Print("Spell 3 casted!");
        TBAction action = new TBAction();
        action.ActionType = "Spell";
        action.ActionName = "Heal";
        action.ActionDescription = "She casts 'Heal Self.'";
        Effect e = new Effect();
        e.EffectType = "Heal";
        e.EffectName = "Self Heal";
        e.EffectValue = 2;
        action.Actor = this;
        action.Effects.Add(e);
        foreach (var player in GetParent<Game>().Players)
        {
            if (player != this)
            {
                action.Target = player;
            }
        }
        GetParent<Game>().SetActivePlayer(this);
        GetParent<Game>().Actions.Add(action);
        GetParent<Game>().gameStateMachine.ChangeState("Casting");
    }
    public void Spell3()
    {
        // Handle spell 3 action
        GD.Print("Spell 3 casted!");
        TBAction action = new TBAction();
        action.ActionType = "Spell";
        action.ActionName = "Regeneration";
        action.ActionDescription = "She casts 'Regeneration.'";
        Effect e = new Effect();
        e.EffectType = "Heal Over Time";
        e.EffectName = "Regeneration";
        e.EffectDuration = 3;
        e.EffectValue = 1;
        action.Actor = this;
        action.Effects.Add(e);
        foreach (var player in GetParent<Game>().Players)
        {
            if (player != this)
            {
                action.Target = player;
            }
        }
        GetParent<Game>().SetActivePlayer(this);
        GetParent<Game>().Actions.Add(action);
        GetParent<Game>().gameStateMachine.ChangeState("Casting");
    }
}
