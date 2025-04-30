using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node2D
{

    public GameStates gameStateMachine = new GameStates();

    public List<Team> Teams;
    public List<PlayerContainer> Players = new List<PlayerContainer>();

    public override void _Ready()
    {
        // Initialize the game state
        GD.Print("Game is ready!");
        Teams = new List<Team>{
            new Team(),
            new Team()
        };
        Teams[0].TeamName = "Team 1";
        Teams[0].TeamId = 1;
        Teams[0].Players = new List<Player>();
        Teams[0].Players.Add(GD.Load<Player>("res://Rouge.tres"));
        Teams[1].Players = new List<Player>();
        Teams[1].Players.Add(GD.Load<Player>("res://Barbarian.tres"));
        Teams[1].TeamName = "Team 2";
        Teams[1].TeamId = 2;
        Players.Add(GetNode<PlayerContainer>("Player1"));
        Players.Add(GetNode<PlayerContainer>("Player2"));
        int x = 0;
        foreach (var player in Players)
        {
            Player pData = Teams[x].Players[0];
            player.PlayerName.Text = pData.name;
            player.PlayerHealth.Value = pData.health;
            player.PlayerHealth.MaxValue = pData.health;
            player.PlayerLevel.Text = pData.level.ToString();
            player.PlayerSprite.Texture = pData.playerSprite;
            x++;
        }
        gameStateMachine.SetContainers(Players);
        gameStateMachine.SetTeams(Teams);
        gameStateMachine.SetTextBox(GetNode<RichTextLabel>("GameText"));
        AddChild(gameStateMachine);
        gameStateMachine.ChangeState("Menu");
        GD.Print($"Game state: {gameStateMachine.GameState.state}");
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

}
