using Godot;
using System;
using System.Collections.Generic;


public partial class State : Node
{

    public string state { get; set; }
    public List<Team> Teams;
    public List<PlayerContainer> Players = new List<PlayerContainer>();
    public RichTextLabel RTL { get; set; }

    public List<TBAction> Actions = new List<TBAction>();

    public Action<string> StateChangedHandler;
    public override void _Ready()
    {
    }
    public virtual void UpdateState()
    {
        //GD.Print($"Updating state: {state}");
        // Handle state updates
    }
    public virtual void EnterState()
    {
        GD.Print($"Entering state: {state}");
        // Handle entering the state
    }
    public virtual void ExitState()
    {
        GD.Print($"Exiting state: {state}");
        // Handle exiting the state
    }
    
    public void SetTeams(List<Team> teams)
    {
        Teams = teams;
    }

    public void SetContainers(List<PlayerContainer> containers)
    {
        // Set player containers if needed
        Players = containers;
    }

    public void SetTextBox(RichTextLabel rtl)
    {
        RTL = rtl;
    }

}
