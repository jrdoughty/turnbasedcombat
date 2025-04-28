using Godot;
using System;
using System.Collections.Generic;

public partial class GameStates : Node2D
{
    public State GameState { get; set; }
    public Dictionary<String,State> States = new Dictionary<string, State>{ { "Menu", new State() }, { "Casting", new State() }, { "Effect", new State() }, { "Decision", new State() } };

    public override void _Ready()
    {
        GameState = States["Menu"];

        GD.Print($"Game state: {GameState}");
    }

    public void ChangeState(string newState)
    {
        if (States.ContainsKey(newState))
        {
            GameState.ExitState();
            GameState = States[newState];
            GameState.EnterState();
            GD.Print($"Game state changed to: {newState}");
        }
        else
        {
            GD.Print($"Invalid state: {newState}");
        }
    }
}