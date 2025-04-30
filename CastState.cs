using Godot;
using System;

public partial class CastState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        RTL.Text = "Casting...";
        // Handle entering the state
    }
    public override void ExitState()
    {
        base.ExitState();
        // Handle exiting the state
    }
}