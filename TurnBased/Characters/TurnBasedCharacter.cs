using Godot;
using System;
using System.Collections.Generic;

public partial class TurnBasedCharacter : Node2D
{
	public Label CharacterName { get; set; }
	public ProgressBar CharacterHealth { get; set; }
	public Label CharacterLevel { get; set; }
	public AnimatedSprite2D CharacterSprite { get; set; }
	public Node2D CharacterSpriteAnchor { get; set; }
	public Control CharacterControls { get; set; }
	public Guid CharacterID { get; set; }
	public List<Effect> PlayerEffects { get; set; } = new List<Effect>();
	public bool IsActive { get; set; } = false;
	public bool IsDefeated { get; set; } = false;
	public bool IsPlayerContolled { get; set; } = false;
	public int QueueVal { get; set; } = 0;
	[Export] public Player CharacterData { get; set; }
	public Team team { get; set; }
    public bool Selected { get; set; } = false;
    public bool Picking { get; set; } = false;

	public override void _Ready()
    {
        // Initialize the player container
        CharacterName = GetNode<Label>("PlayerName");
        CharacterHealth = GetNode<ProgressBar>("PlayerHealth");
        CharacterLevel = GetNode<Label>("PlayerLevel");
        CharacterSpriteAnchor = GetNode<Node2D>("SpriteAnchor");
        CharacterControls = GetNode<Control>("PlayerControls");

        CharacterID = Guid.NewGuid();
        
        if(CharacterData != null)
        {
            InitializeData();
        }
        else
        {
            GD.PrintErr("No Character Data assigned to TurnBasedCharacter");
        }
    }

    public void InitializeData()
    {
        CharacterData = CharacterData.Duplicate() as Player;//make sure we have a unique instance of the player data

        for (int i = 0; i < CharacterData.PlayerActions.Count; i++)
        {
            string action = CharacterData.PlayerActions[i].ToString().Trim('"');
            CharacterControls.GetNode<Button>($"Button{i + 1}").Pressed += () =>
            {
                PerformAction((string)action);
            };
            CharacterControls.GetNode<Button>($"Button{i + 1}").Text = action;
        }
        CharacterSprite = CharacterData.PlayerSprite.Instantiate() as AnimatedSprite2D;
        CharacterSpriteAnchor.AddChild(CharacterSprite);

        string path = $"res://TurnBased/Characters/Levels/{CharacterData.CharacterName}{CharacterData.Level}.tres";
        if (CharacterData.Level > 0 && ResourceLoader.Exists(path)) //load level data if level is set and data was not loaded from file
        {
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
        CharacterName.Text = CharacterData.CharacterName;

        if (CharacterData.CurrentHealth < 0) //if current health is set to a negative, set it to max health
        {
            CharacterData.CurrentHealth = CharacterData.Health;
        }
        CharacterHealth.Value = CharacterData.CurrentHealth;
        CharacterHealth.MaxValue = CharacterData.Health;
        CharacterLevel.Text = CharacterData.Level.ToString();
    }

	public void PerformAction(String actionName)
    {
        TBAction action = Registry.ActionData[actionName].Duplicate() as TBAction;
        action.Actor = this;
        action.Initialize();
        foreach (var player in GetParent<TurnBasedBattle>().Players)
        {
            if (player != this && player.IsPlayerContolled != this.IsPlayerContolled)
            {
                action.Target = player;
            }
        }
        GetParent<TurnBasedBattle>().SetActivePlayer(this);
        GetParent<TurnBasedBattle>().Actions.Add(action);
        GetParent<TurnBasedBattle>().gameStateMachine.ChangeState("Target");
    }
	public void updateConditions()// this will need to be elaborated on later
	{
		if(CharacterData.CurrentHealth <= 0)
		{
			CharacterSprite.Visible = false;
			IsActive = false;
			IsDefeated = true;
            team.CheckStatus();
		}
		else
		{
			//PlayerSprite.Visible = true;
			//IsActive = true;
			//IsDefeated = false;
		}
	}
    public void OnArea2DInputEvent(Viewport viewport, InputEvent @event, int shape_idx)
    {
        if(Picking && @event is InputEventMouseButton mouseEvent)
        {
            if(mouseEvent.Pressed)
            {
                Selected = true;
            }
        }
    }
    public void OnArea2DMouseEntered()
    {
        if(Picking)
        {
            GetNode<Node2D>("Picker").Visible = true;
        }
    }
    public void OnArea2DMouseExited()
    {
        GetNode<Node2D>("Picker").Visible = false;
    }
}
