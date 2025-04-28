using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node2D
{

    public GameStates gameStateMachine = new GameStates();

    public List<List<Player>> Teams;
    public List<PlayerContainer> Players = new List<PlayerContainer>();

    public override void _Ready()
    {
        // Initialize the game state
        GD.Print("Game is ready!");
        Teams = new List<List<Player>>{
        new List<Player>{ GD.Load<Player>("res://Rouge.tres") },
        new List<Player>{ GD.Load<Player>("res://Barbarian.tres") }
        
        };
        Players.Add(GetNode<PlayerContainer>("Player1"));
        Players.Add(GetNode<PlayerContainer>("Player2"));
        int x = 0;
        foreach (var player in Players)
        {
            Player pData = Teams[x][0];
            player.PlayerName.Text = pData.name;
            player.PlayerHealth.Value = pData.health;
            player.PlayerHealth.MaxValue = pData.health;
            player.PlayerLevel.Text = pData.level.ToString();
            player.PlayerSprite.Texture = pData.playerSprite;
            x++;
        }
        gameStateMachine.ChangeState("Menu");
        GD.Print($"Game state: {gameStateMachine.GameState.state}");
    }

    public override void _Process(double delta)
    {
        gameStateMachine.GameState.UpdateState();
    }

    public override void _PhysicsProcess(double delta)
    {
        // Handle physics-related updates
        //GD.Print("Processing physics...");
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
                GD.Print($"Game state: {GameState}");
                GD.Print($"Teams: {Teams.Count}");
                foreach (var team in Teams)
                {
                    GD.Print($"Team size: {team.Count}");
                    foreach (var player in team)
                    {
                        GD.Print($"Player name: {player.name}");
                        GD.Print($"Player health: {player.health}");
                        GD.Print($"Player mana: {player.mana}");
                        GD.Print($"Player level: {player.level}");
                        GD.Print($"Player experience: {player.experience}");
                        GD.Print($"Player attack: {player.attack}");
                        GD.Print($"Player defense: {player.defense}");
                        GD.Print($"Player speed: {player.speed}");
                        GD.Print($"Player magicDefense: {player.magicDefense}");
                        GD.Print($"Player magicAttack: {player.magicAttack}");
                    }
                }
                GD.Print($"Game states: {GameStates.Count}");
            }
        }
    }

}
