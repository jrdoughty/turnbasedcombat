using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerContainer : Node2D
{
	public Label PlayerName { get; set; }
	public ProgressBar PlayerHealth { get; set; }
	public Label PlayerLevel { get; set; }
	public Sprite2D PlayerSprite { get; set; }
	public Control PlayerControls { get; set; }
	public Guid PlayerID { get; set; }
	public List<Effect> PlayerEffects { get; set; } = new List<Effect>();
	public bool IsActive { get; set; } = false;
	public bool IsDefeated { get; set; } = false;
	public bool IsPlayerContolled { get; set; } = false;
	public int QueueVal { get; set; } = 0;
	[Export] public Player PlayerData { get; set; }
	public Team team { get; set; }

	public override void _Ready()
	{
		// Initialize the player container
		GD.Print("PlayerContainer getting ready!");
		PlayerName = GetNode<Label>("PlayerName");
		PlayerHealth = GetNode<ProgressBar>("PlayerHealth");
		PlayerLevel = GetNode<Label>("PlayerLevel");
		PlayerSprite = GetNode<Sprite2D>("PlayerSprite");
		PlayerControls = GetNode<Control>("PlayerControls");
        PlayerData = PlayerData.Duplicate() as Player;//make sure we have a unique instance of the player data
        for (int i = 0; i < PlayerData.playerActions.Count; i++)
        {
            string action = PlayerData.playerActions[i].ToString().Trim('"');
            GD.Print($"action: {action}");
            PlayerControls.GetNode<Button>($"Button{i + 1}").Pressed += () =>
            {
                PerformAction((string)action);
            };
            PlayerControls.GetNode<Button>($"Button{i + 1}").Text = action;
        }
        
        PlayerID = Guid.NewGuid();

        try
        {
            if (PlayerData.level > 0)
            {
                Level levelData = ResourceLoader.Load<Level>($"res://Characters/Levels/{PlayerData.name}{PlayerData.level}.tres");
                if (levelData != null)
                {
                    PlayerData.attack += levelData.attack;
                    PlayerData.defense += levelData.defense;
                    PlayerData.speed += levelData.speed;
                    PlayerData.health += levelData.health;
                    PlayerData.mana += levelData.mana;
                    PlayerData.magicAttack += levelData.magicAttack;
                    PlayerData.magicDefense += levelData.magicDefense;
                }
            }
        }
        catch (Exception e)
        {
            //GD.PrintErr("Error getting level: " + e.Message);
        }
        
	}

	public void PerformAction(String actionName)
	{
		TBAction action = Registry.ActionData[actionName].Duplicate() as TBAction;
		action.Actor = this;
		action.Initialize();
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
	public void updateConditions()// this will need to be elaborated on later
	{
		if(PlayerData.currentHealth <= 0)
		{
			PlayerSprite.Visible = false;
			IsActive = false;
			IsDefeated = true;
			team.IsDefeated = true;// will need to change later
		}
		else
		{
			//PlayerSprite.Visible = true;
			//IsActive = true;
			//IsDefeated = false;
		}
	}
}
