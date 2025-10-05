using Godot;
using System;
using System.Collections.Generic;

public partial class TurnBasedCharacter : Node2D
{
	public Label CharacterName { get; set; }
	public ProgressBar CharacterHealth { get; set; }
	public Label CharacterLevel { get; set; }
	public Sprite2D CharacterSprite { get; set; }
	public Control CharacterControls { get; set; }
	public Guid CharacterID { get; set; }
	public List<Effect> PlayerEffects { get; set; } = new List<Effect>();
	public bool IsActive { get; set; } = false;
	public bool IsDefeated { get; set; } = false;
	public bool IsPlayerContolled { get; set; } = false;
	public int QueueVal { get; set; } = 0;
	[Export] public Player CharacterData { get; set; }
	public Team team { get; set; }
    public bool dataLoaded = false;

	public override void _Ready()
    {
        // Initialize the player container
        GD.Print("PlayerContainer getting ready!");
        CharacterName = GetNode<Label>("PlayerName");
        CharacterHealth = GetNode<ProgressBar>("PlayerHealth");
        CharacterLevel = GetNode<Label>("PlayerLevel");
        CharacterSprite = GetNode<Sprite2D>("PlayerSprite");
        CharacterControls = GetNode<Control>("PlayerControls");

        CharacterID = Guid.NewGuid();
        
        if(CharacterData != null)
        {
            InitializeData();
        }
    }
    
    public void InitializeData()
    {
        CharacterData = CharacterData.Duplicate() as Player;//make sure we have a unique instance of the player data
        for (int i = 0; i < CharacterData.PlayerActions.Count; i++)
        {
            string action = CharacterData.PlayerActions[i].ToString().Trim('"');
            GD.Print($"action: {action}");
            CharacterControls.GetNode<Button>($"Button{i + 1}").Pressed += () =>
            {
                PerformAction((string)action);
            };
            CharacterControls.GetNode<Button>($"Button{i + 1}").Text = action;
        }

        try
        {
            if (CharacterData.Level > 0 && !dataLoaded) //load level data if level is set and data was not loaded from file
            {
                string path = $"res://Characters/Levels/{CharacterData.CharacterName}{CharacterData.Level}.tres";
                GD.Print($"Loading level data from {path}");
                Level levelData = ResourceLoader.Load<Level>(path);
                if (levelData != null)
                {
                    CharacterData.Attack += levelData.attack;
                    CharacterData.Defense += levelData.defense;
                    CharacterData.Speed += levelData.speed;
                    CharacterData.Health += levelData.health;
                    CharacterData.Mana += levelData.mana;
                    CharacterData.MagicAttack += levelData.magicAttack;
                    CharacterData.MagicDefense += levelData.magicDefense;
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
		if(CharacterData.CurrentHealth <= 0)
		{
			CharacterSprite.Visible = false;
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
