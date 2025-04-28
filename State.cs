using Godot;
using System;


public partial class State : Node
{

    public string state { get; set; }
    public override void _Ready()
    {
    }
    public void UpdateState()
    {
        GD.Print($"Updating state: {state}");
        // Handle state updates
    }
    public void EnterState()
    {
        GD.Print($"Entering state: {state}");
        // Handle entering the state
    }
    public void ExitState()
    {
        GD.Print($"Exiting state: {state}");
        // Handle exiting the state
    }

}