using Godot;
using System;

public partial class MenuState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        Players[0].GetNode<Control>("PlayerControls").Visible = true;
        RTL.Visible = false;
        // Handle entering the state
    }
    public override void ExitState()
    {
        base.ExitState();
        Players[0].GetNode<Control>("PlayerControls").Visible = false;
        RTL.Visible = true;
        // Handle exiting the state
    }
}