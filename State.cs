using Godot;
using System.Collections.Generic;


public partial class State : Node
{

    public string state { get; set; }
    private List<List<Player>> Teams;
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
    
    public void SetTeams(List<List<Player>> teams)
    {
        Teams = teams;
    }

}