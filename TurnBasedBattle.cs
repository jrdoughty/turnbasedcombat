using Godot;
using System;
using System.Collections.Generic;


public partial class TurnBasedBattle : Node2D
{

    public GameStates gameStateMachine = new GameStates();

    public List<Team> Teams;
    public List<TurnBasedCharacter> Players = new List<TurnBasedCharacter>();
    public List<TBAction> Actions = new List<TBAction>();

    public TurnBasedCharacter ActivePlayer { get; set; }
    public Team ActiveTeam { get; set; }

    public BattleConditions BattleConditions { get; set; } = new BattleConditions();
    public override void _Ready()
    {
        // Initialize the game state
        GD.Print("Game is ready!");
        Teams = new List<Team>{
            new Team(),
            new Team()
        };

        Players.Add(GetNode<TurnBasedCharacter>("Player1"));
        Players.Add(GetNode<TurnBasedCharacter>("Player2"));
        Players[0].IsPlayerContolled = true;
        
        if(ResourceLoader.Exists($"user://SaveData/Party/Rouge.tres"))
            Players[0].CharacterData = ResourceLoader.Load<Player>($"user://SaveData/Party/Rouge.tres");
        else
            Players[0].CharacterData = ResourceLoader.Load<Player>($"res://Characters/Rouge.tres");
        Players[0].dataLoaded = true;
        Players[0].InitializeData();
        Teams[0].AddPlayer(Players[0]);
        Teams[1].AddPlayer(Players[1]);
        foreach (var player in Players)
        {
            Player pData = player.CharacterData;

            player.CharacterName.Text = pData.CharacterName;
            if (pData.CurrentHealth < 0) //if current health is not set, set it to max health
            {
                pData.CurrentHealth = pData.Health;
            }
            player.CharacterHealth.Value = pData.CurrentHealth;
            player.CharacterHealth.MaxValue = pData.Health;
            player.CharacterLevel.Text = pData.Level.ToString();
            //player.CharacterSprite.SpriteFrames = pData.PlayerSprite;
        }
        ActiveTeam = Teams[0];
        gameStateMachine.SetContainers(Players);
        gameStateMachine.SetTeams(Teams);
        gameStateMachine.SetTextBox(GetNode<RichTextLabel>("GameText"));
        gameStateMachine.SetActions(Actions);
        AddChild(gameStateMachine);
        GD.Print($"Game state: {gameStateMachine.GameState.state}");
        GetNode<Queue>("Queue").StateChangedHandler = gameStateMachine.ChangeState;
        gameStateMachine.setNextCharacter(GetNode<Queue>("Queue").GetNextCharacter);

        GetNode<Queue>("Queue").Initialize(new List<TurnBasedCharacter>() { Players[0], Players[1] });
        GetNode<Queue>("Queue").ActiveCharacterHandler = SetActivePlayer;
        gameStateMachine.setGetBattleConditions(GetActiveBattleConditions);
        BattleConditions.Teams = Teams;
    }

    public override void _Process(double delta)
    {
        gameStateMachine.GameState.UpdateState();
    }

    public override void _Input(InputEvent @event)
    {
        // Handle user input
        if (@event is InputEventKey keyEvent)
        { 
            if(keyEvent.IsPressed() && keyEvent.Keycode == Key.Escape)
            {
                GD.Print("Escape key pressed!");
                GetTree().Quit();
            }
            else if (keyEvent.IsPressed() && keyEvent.Keycode == Key.Q)
            {
                GD.Print($"Key pressed: {keyEvent.Keycode.ToString()}");
                GD.Print($"Game state: {gameStateMachine.GameState.state}");
                GD.Print($"Teams: {Teams.Count}");
                GD.Print($"Game states: {gameStateMachine.States.Count}");
                gameStateMachine.ChangeState("Casting");
            }
        }
    }

    public void SetActivePlayer(TurnBasedCharacter player)
    {
        if (ActivePlayer != null)
        {
            ActivePlayer.IsActive = false;
        }
        ActivePlayer = player;
        player.IsActive = true;
    }
    public BattleConditions GetActiveBattleConditions()
    {
        return BattleConditions;
    }
}
