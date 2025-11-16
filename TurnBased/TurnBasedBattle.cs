using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;


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
        
        foreach( Team team in Teams)
        {
            foreach(TurnBasedCharacter character in team.Players)
            {
                Players.Add(character);
            }
        }

        gameStateMachine.SetContainers(Players);
        gameStateMachine.SetTeams(Teams);
        gameStateMachine.SetTextBox(GetNode<RichTextLabel>("GameText"));
        gameStateMachine.SetActions(Actions);
        AddChild(gameStateMachine);
        //GD.Print($"Game state: {gameStateMachine.GameState.state}");
        GetNode<Queue>("Queue").StateChangedHandler = gameStateMachine.ChangeState;
        gameStateMachine.setNextCharacter(GetNode<Queue>("Queue").GetNextCharacter);

        GetNode<Queue>("Queue").Initialize(Players);
        GetNode<Queue>("Queue").ActiveCharacterHandler = SetActivePlayer;
        gameStateMachine.SetCharacterQueue(GetNode<Queue>("Queue"));
        gameStateMachine.setGetBattleConditions(GetActiveBattleConditions);
        BattleConditions.Teams = Teams;
    }

    public void SetTeams(List<Player> players, List<Player> enemies)
    {
        Teams = new List<Team>{
                new Team(),
                new Team()
            };
        List<TurnBasedCharacter> tempPlayers = new List<TurnBasedCharacter>();
        tempPlayers.Add(GetNode<TurnBasedCharacter>("Player1"));
        tempPlayers.Add(GetNode<TurnBasedCharacter>("Player2"));
        bool first = true;
        TurnBasedCharacter lastCharacter = null;
        foreach (var p in players)
        {
            TurnBasedCharacter tbc = GD.Load<PackedScene>("res://TurnBased/Characters/TurnBasedCharacter.tscn").Instantiate<TurnBasedCharacter>();
            tbc.CharacterData = p;
            tbc.IsPlayerContolled = true;
            Teams[0].AddPlayer(tbc);
            Players.Add(tbc);
            if(first)
            {
                tbc.MoveLocalX(tempPlayers[0].Position.X);
                tbc.MoveLocalY(tempPlayers[0].Position.Y);
                RemoveChild(tempPlayers[0]);
                first = false;
            }
            else
            {
                tbc.MoveLocalX(lastCharacter.Position.X + 60);
                tbc.MoveLocalY(lastCharacter.Position.Y);
            }
            AddChild(tbc);
            lastCharacter = tbc;
            first = false;
        }
        first = true;
        foreach (var e in enemies)
        {
            TurnBasedCharacter tbc = GD.Load<PackedScene>("res://TurnBased/Characters/TurnBasedCharacter.tscn").Instantiate<TurnBasedCharacter>();
            tbc.CharacterData = e;
            tbc.IsPlayerContolled = false;
            Teams[1].AddPlayer(tbc);
            Players.Add(tbc);
            if(first)
            {
                tbc.MoveLocalX(tempPlayers[1].Position.X);
                tbc.MoveLocalY(tempPlayers[1].Position.Y);
                RemoveChild(tempPlayers[1]);
                first = false;
            }
            else
            {
                tbc.MoveLocalX(lastCharacter.Position.X + 60);
                tbc.MoveLocalY(lastCharacter.Position.Y);
            }
            AddChild(tbc);
            lastCharacter = tbc;
            first = false;
        }
        ActiveTeam = Teams[0];
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
                GetTree().Quit();
            }
            else if (keyEvent.IsPressed() && keyEvent.Keycode == Key.Q)
            {
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
