using Godot;
using System;
using System.Collections.Generic;

public partial class GameStates : Node2D
{
    public State GameState { get; set; }
    public Dictionary<String,State> States = new Dictionary<string, State>{ { "Menu", new State() }, { "Casting", new State() }, { "Effect", new State() }, { "Decision", new State() } };

    public override void _Ready()
    {
        foreach (var state in States.Keys)
        {
            States[state].state = state;
        }
    }

    public void ChangeState(string newState)
    {
        if (States.ContainsKey(newState))
        {
            if (GameState != null)
            {
                GameState.ExitState();
            }
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