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
		for(int i = 0; i < PlayerData.playerActions.Count; i++)
		{
			string action = PlayerData.playerActions[i].ToString().Trim('"');
			GD.Print($"action: {action}");
			PlayerControls.GetNode<Button>($"Button{i+1}").Pressed += () =>
			{
				PerformAction((string)action);
			};
		}
		PlayerID = Guid.NewGuid();
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
		if(PlayerCurrentHealth <= 0)
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
